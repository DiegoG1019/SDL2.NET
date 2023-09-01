using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SDL2.Bindings;
using SDL2.NET.Exceptions;

namespace SDL2.NET;

/// <summary>
/// Provides an abstract interface to stream I/O. Applications can generally ignore the specifics of this structure's internals and treat them as opaque pointers. <see href="https://wiki.libsdl.org/SDL2/SDL_RWops"/>
/// </summary>
/// <remarks>
/// This is meant to be a suitable interface to hook .NET methods to a native SDL_RWops
/// </remarks>
public class RWops : IHandle, IDisposable
{
    /// <summary>
    /// Specifies the position in a RWops object to use for seeking.
    /// </summary>
    public enum RWopSeekMode : int
    {
        /// <summary>
        /// Specifies the beginning of a RWops object
        /// </summary>
        FromBeginning,

        /// <summary>
        /// Specifies the current position within a RWops object
        /// </summary>
        FromCurrent,

        /// <summary>
        /// Specifies the end of a RWops object
        /// </summary>
        FromEnd
    }

    private static readonly ConcurrentDictionary<IntPtr, WeakReference<RWops>> _handleDict = new(2, 10);

    #region Internal delegates

    private unsafe delegate long InternalSizeFunction(nint RWopsHandle);
    private unsafe delegate long InternalSeekFunction(nint RWopsHandle, long offset, int mode);
    private unsafe delegate nint InternalReadFunction(nint RWopsHandle, void* data, nint size, nint count);
    private unsafe delegate nint InternalWriteFunction(nint RWopsHandle, void* data, nint size, nint count);
    private unsafe delegate int InternalCloseFunction(nint RWopsHandle);

    // Forcing a lookup is slower than making this an instance delegate, however, an instance delegate is dangerous because if the object is collected by the GC from .NET, but
    // not deleted from unmanaged (or differently managed) code, a memory access violation exception, protected memory access exception, or memory corruption could occur

    private static readonly InternalSizeFunction sizeFunction = (h) =>
    {
        if (_handleDict.TryGetValue(h, out var wr) is false || wr.TryGetTarget(out var rwop) is false)
        {
            NotFoundError(h);
            return -1;
        }

        try
        {
            return rwop.GetSizeCallback(rwop);
        }
        catch(Exception e)
        {
            SetExceptionError(h, e);
            return -1;
        }
    };

    private static readonly InternalSeekFunction seekFunction = (h, o, m) =>
    {
        if (_handleDict.TryGetValue(h, out var wr) is false || wr.TryGetTarget(out var rwop) is false)
        {
            NotFoundError(h);
            return -1;
        }

        try
        {
            rwop.SeekCallback(rwop, o, (RWopSeekMode)m);
            return 0;
        }
        catch (Exception e)
        {
            SetExceptionError(h, e);
            return -1;
        }
    };

    private static unsafe readonly InternalReadFunction readFunction = (h, d, s, c) =>
    {
        if (_handleDict.TryGetValue(h, out var wr) is false || wr.TryGetTarget(out var rwop) is false)
        {
            NotFoundError(h);
            return -1;
        }

        try
        {
            Span<byte> sp = new(d, (int)(s * c));
            return (nint)rwop.ReadCallback(rwop, sp);
        }
        catch (Exception e)
        {
            SetExceptionError(h, e);
            return -1;
        }
    };

    private static unsafe readonly InternalWriteFunction writeFunction = (h, d, s, c) =>
    {
        if (_handleDict.TryGetValue(h, out var wr) is false || wr.TryGetTarget(out var rwop) is false)
        {
            NotFoundError(h);
            return -1;
        }

        try
        {
            ReadOnlySpan<byte> sp = new(d, (int)(s * c));
            return (nint)rwop.WriteCallback(rwop, sp);
        }
        catch (Exception e)
        {
            SetExceptionError(h, e);
            return -1;
        }
    };

    private static readonly InternalCloseFunction closeFunction = (h) =>
    {
        if (_handleDict.TryGetValue(h, out var wr) is false || wr.TryGetTarget(out var rwop) is false)
        {
            NotFoundError(h);
            return -1;
        }

        try
        {
            rwop.CloseCallback?.Invoke(rwop);
            return 0;
        }
        catch (Exception e)
        {
            SetExceptionError(h, e);
            return -1;
        }
    };

    private static void SetExceptionError(nint pointer, Exception ex)
    {
        var msg = $"Attempting to use RWop ({pointer:X8}) resulted in a .NET exception being thrown: {ex.GetType()}::{ex.Message}\n\n{ex}";
        SDL.SDL_SetError(msg);
        Debug.WriteLine($"Error on SDL native callback: {msg}");
        Debugger.Break();
    }

    private static void NotFoundError(nint pointer)
    {
        var msg = $"The RWop ({pointer:X8}) did not match any living .NET object";
        SDL.SDL_SetError(msg);
        Debug.WriteLine($"Error on SDL native callback: {msg}");
        Debugger.Break();
    }

    #endregion

    #region Public Delegates

    /// <summary>
    /// Gets the size of the RWop
    /// </summary>
    /// <param name="rwop">The rwop that invoked the delegate</param>
    public delegate long GetRWopSize(RWops rwop);

    /// <summary>
    /// Seeks to the position specified by <paramref name="offset"/>
    /// </summary>
    /// <param name="rwop">The rwop that invoked the delegate</param>
    /// <param name="offset"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public delegate long SeekRWop(RWops rwop, long offset, RWopSeekMode mode);

    /// <summary>
    /// Reads data from the RWop into <paramref name="buffer"/>
    /// </summary>
    /// <param name="rwop">The rwop that invoked the delegate</param>
    /// <param name="buffer">The buffer to read the data into</param>
    /// <returns>Returns the number of bytes read, or 0 at end of file</returns>
    public delegate long ReadRWop(RWops rwop, Span<byte> buffer);

    /// <summary>
    /// Writes data from <paramref name="buffer"/> into the RWop
    /// </summary>
    /// <param name="rwop">The rwop that invoked the delegate</param>
    /// <param name="buffer">The buffer containing the data to be written into the RWop</param>
    /// <returns>Returns the number of objects written.</returns>
    public delegate long WriteRWop(RWops rwop, ReadOnlySpan<byte> buffer);

    /// <summary>
    /// Closes the RWop
    /// </summary>
    /// <param name="rwop">The rwop that invoked the delegate</param>
    public delegate void CloseRWop(RWops rwop);

    #endregion

    /// <summary>
    /// The <see cref="RWopObject"/> around which this <see cref="RWops"/> is built
    /// </summary>
    /// <remarks>
    /// If null, this <see cref="RWops"/> was created using delegates directly
    /// </remarks>
    public RWopObject? RWopObject { get; }

    /// <summary>
    /// Creates an <see cref="RWops"/> object that represents <paramref name="stream"/>
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to wrap the newly created <see cref="RWops"/> around</param>
    /// <param name="cleanupStream">if <see langword="true"/>, <paramref name="stream"/> will be disposed when the resulting <see cref="RWops"/> is disposed</param>
    public static RWops CreateFromStream(Stream stream, bool cleanupStream = false)
        => new(
            r => stream.Length,
            (r, o, m) => stream.Seek(o, (SeekOrigin)m), // SDL's enumeration definition is the same as .NET's
            (r, b) => stream.Read(b),
            (r, b) =>
            {
                stream.Write(b);
                return b.Length;
            },
            cleanupStream ? null : (r) => stream.Dispose()
        );
    public static RWops CreateFromMemory<T>( memory)
        where T : unmanaged
        => new(SDL.SDL_RWFromMem(memory.);

    /// <summary>
    /// The callback that obtains the size of the represented object
    /// </summary>
    /// <remarks>
    /// Might be <see langword="null"/> if <see cref="FromNative"/> is <see langword="true"/>
    /// </remarks>
    public GetRWopSize? GetSizeCallback { get; }

    /// <summary>
    /// The callback that seeks in the represented object
    /// </summary>
    /// <remarks>
    /// Might be <see langword="null"/> if <see cref="FromNative"/> is <see langword="true"/>
    /// </remarks>
    public SeekRWop? SeekCallback { get; }

    /// <summary>
    /// The callback that reads from the represented object
    /// </summary>
    /// <remarks>
    /// Might be <see langword="null"/> if <see cref="FromNative"/> is <see langword="true"/>
    /// </remarks>
    public ReadRWop? ReadCallback { get; }

    /// <summary>
    /// The callback that writes into the represented object
    /// </summary>
    /// <remarks>
    /// Might be <see langword="null"/> if <see cref="FromNative"/> is <see langword="true"/>
    /// </remarks>
    public WriteRWop? WriteCallback { get; }

    /// <summary>
    /// The callback that closes the represented object after reading or writing is done
    /// </summary>
    public CloseRWop? CloseCallback { get; }

    /// <summary>
    /// <see langword="true"/> if this <see cref="RWops"/> object is backed directly by SDL or another unmanaged library, <see langword="false"/> if backed by a managed .NET type like a <see cref="StreamRWopObject"/>
    /// </summary>
    public bool FromNative { get; } = false;

    internal readonly nint handle;

    internal RWops(nint handle)
    {
        if (handle == 0)
            throw new SDLRWopCreationException(SDL.SDL_GetAndClearError());

        this.handle = handle;
        FromNative = true;
    }

    /// <summary>
    /// Creates a new RWop
    /// </summary>
    public unsafe RWops(GetRWopSize getSizeCallback, SeekRWop seekCallback, ReadRWop readCallback, WriteRWop writeCallback, CloseRWop? closeCallback)
    {
        nint h = SDL.SDL_AllocRW();
        try
        {
            if (h == 0)
                throw new InsufficientMemoryException();
            handle = h;

            GetSizeCallback = getSizeCallback ?? throw new ArgumentNullException(nameof(getSizeCallback));
            SeekCallback = seekCallback ?? throw new ArgumentNullException(nameof(seekCallback));
            ReadCallback = readCallback ?? throw new ArgumentNullException(nameof(readCallback));
            WriteCallback = writeCallback ?? throw new ArgumentNullException(nameof(writeCallback));
            CloseCallback = closeCallback;

            var rw = (SDL.SDL_RWops*)h;
            rw->close = Marshal.GetFunctionPointerForDelegate(closeFunction);
            rw->read = Marshal.GetFunctionPointerForDelegate(readFunction);
            rw->seek = Marshal.GetFunctionPointerForDelegate(seekFunction);
            rw->size = Marshal.GetFunctionPointerForDelegate(sizeFunction);
            rw->write = Marshal.GetFunctionPointerForDelegate(writeFunction);
            rw->type = 0;

            _handleDict.TryAdd(handle, new WeakReference<RWops>(this));
        }
        catch
        {
            SDL.SDL_FreeRW(h);
            throw;
        }
    }

    IntPtr IHandle.Handle => handle;

    private bool disposedValue;

    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            _handleDict.TryRemove(handle, out _);
            SDL.SDL_FreeRW(handle);

            RWopObject?.Dispose();

            disposedValue = true;
        }
    }

    /// <inheritdoc/>
    ~RWops()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
