using System.Collections;
using System.Runtime.InteropServices;
using static SDL2.Bindings.SDL_mixer;

namespace SDL2.NET.SDLMixer;

public partial class AudioChunk
{
    /// <summary>
    /// An Audio Buffer that allows direct manipulation of the underlying byte stream of the Audio Chunk
    /// </summary>
    /// <remarks>
    /// This will lock the AudioChunk for as long as this class remains in use. When done, call IDispose (Flush WON'T be called automatically). Forgetting to dispose this class will cause the AudioChunk to be unusable. Be warned.
    /// </remarks>
    public sealed class AudioChunkSampleBuffer : Stream, IDisposable
    {
        private readonly object sync = new();
        private readonly IntPtr _handle;
        private readonly object chunkSync;
        private readonly MIX_Chunk chunkDat;

        private byte[] buffer;

        internal AudioChunkSampleBuffer(IntPtr handle, object sync)
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            _handle = handle;
            chunkSync = sync;

            var chunk = Marshal.PtrToStructure<MIX_Chunk>(handle);
            chunkDat = chunk;
            _length = chunk.alen;
            buffer = new byte[_length];

            Marshal.Copy(chunk.abuf, buffer, 0, (int)chunk.alen);
        }

        public override void Flush()
        {
            lock (sync)
            {
                AudioMixer.ThrowIfNotInitAndOpen();
                ThrowIfDisposed();
                lock (chunkSync)
                {
                    Marshal.Copy(buffer, 0, chunkDat.abuf, (int)chunkDat.alen);
                }
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (sync)
            {
                ThrowIfDisposed();
                if (offset + count >= buffer.Length)
                    throw new ArgumentOutOfRangeException(nameof(count));

                int i = 0;
                for (; i < count && i < _length; i++)
                    buffer[offset + i] = this.buffer[position++];

                return i;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            lock (sync)
            {
                ThrowIfDisposed();
                long pos;
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        check(offset);
                        position = offset;
                        break;
                    case SeekOrigin.Current:
                        pos = position + offset;
                        check(pos);
                        position = pos;
                        break;
                    case SeekOrigin.End:
                        pos = Length + offset;
                        check(pos);
                        position = pos;
                        break;
                    default:
                        throw new NotSupportedException($"Unknown SeekOrigin: {origin}");
                }

                return position;
            }

            void check(long value)
            {
                if (value > uint.MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(offset), offset, $"The AudioChunk buffer cannot be longer than {uint.MaxValue} bytes, and thus cannot seek further than that");
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset), offset, "Position cannot be less than 0");
            }
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Setting the length of the buffer is NOT supported.\nAuthor's Note: This would require resizing an array that lives (and is allocated in) SDL's native code; simply replacing it would probably yield a memory leak, and I can't easily free that from managed code (unless I modify SDL's code, probably)");

            lock (sync)
            {
                ThrowIfDisposed();
                uint l = checked((uint)value);
                if (_length == l)
                    return;

                _length = l;
                var nb = new byte[_length];

                for (int i = 0; i < _length; i++)
                    nb[i] = buffer[i];
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            lock (sync)
            {
                ThrowIfDisposed();
                if (offset + count >= buffer.Length)
                    throw new ArgumentOutOfRangeException(nameof(count));

                int i = 0;
                for (; i < count && i < _length; i++)
                    this.buffer[position++] = buffer[offset + i];
            }
        }

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;

        private uint _length;
        public override long Length => _length;

        public override long Position
        {
            get
            {
                ThrowIfDisposed();
                return position;
            }
            set
            {
                lock (sync)
                {
                    ThrowIfDisposed();
                    position = value;
                }
            }
        }

        public ref byte this[uint index] => ref buffer[index];

        private bool disposedValue;
        private long position;

        protected override void Dispose(bool disposing)
        {
            lock (sync)
            {
                base.Dispose(disposing);
                if (!disposedValue)
                {
                    buffer = null;
                    disposedValue = true;
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (disposedValue)
                throw new ObjectDisposedException(nameof(AudioChunkSampleBuffer));
        }
    }
}