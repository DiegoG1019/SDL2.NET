using System.Collections.Concurrent;

namespace SDL2.NET.Mixer;

public class AudioChunk : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, WeakReference<AudioChunk>> _handleDict = new(4, 20);

    internal readonly IntPtr _handle;

    internal static AudioChunk FetchOrNew(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : new(handle);

    internal static AudioChunk? Fetch(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : null;

    internal AudioChunk(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            #error work this out too;
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
#error Free this thing
            (_handle);
            _handleDict.TryRemove(_handle, out _);
            disposedValue = true;
        }
    }

    ~AudioChunk()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(AudioChunk));
    }

    #endregion
}