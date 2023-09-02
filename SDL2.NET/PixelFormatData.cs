﻿using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents a Pixel Format and contains relevant pixel information
/// </summary>
public class PixelFormatData : IDisposable, IHandle
{
    IntPtr IHandle.Handle => _handle;
    private static readonly ConcurrentDictionary<IntPtr, WeakReference<PixelFormatData>> _handleDict = new(2, 10);

    internal readonly IntPtr _handle;
    private bool Created;

    internal static PixelFormatData FetchOrNew(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : new(handle);

    internal PixelFormatData(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLSurfaceCreationException(SDL_GetAndClearError());

        var f = Marshal.PtrToStructure<SDL_PixelFormat>(handle);
        Format = (PixelFormat)f.format;
        _pal = f.palette == IntPtr.Zero ? null : Palette.FetchOrNew(f.palette);

        Mask = new(f.BitsPerPixel, f.Rmask, f.Gmask, f.Bmask, f.Amask, f.BytesPerPixel);
        Created = false;
    }

    /// <summary>
    /// Instances a new object of type PixelFormatData from the given PixelFormat
    /// </summary>
    /// <param name="format"></param>
    public PixelFormatData(PixelFormat format)
        : this(SDL_AllocFormat((uint)format)) 
    {
        Created = true;
    }

    /// <summary>
    /// Drops the control over the LifeSpan of this <see cref="PixelFormatData"/> this .NET wrapper object has over the backing native SDL object
    /// </summary>
    /// <remarks>
    /// Calling this method carelessly may result in memory leaks
    /// </remarks>
    public void DropLifeSpanControl()
    {
        Created = false;
    }

    /// <summary>
    /// The <see cref="PixelFormat"/> of this data object
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// The <see cref="Palette"/> associated with this <see cref="PixelFormatData"/> or null if it doesn't have a palette
    /// </summary>
    public Palette? Palette
    {
        get => _pal;
        set
        {
            ThrowIfDisposed();
            SDLPixelFormatDataException.ThrowIfLessThan(SDL_SetPixelFormatPalette(_handle, value?._handle ?? IntPtr.Zero), 0);
            _pal = value;
        }
    }
    private Palette? _pal;

    /// <summary>
    /// Represents the Pixel Mask Data in this Instance
    /// </summary>
    public PixelFormatRGBAMask Mask { get; }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (Created)
                SDL_FreeFormat(_handle);
            _handleDict.TryRemove(_handle, out _);
            disposedValue = true;
        }
    }

    ~PixelFormatData()
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
            throw new ObjectDisposedException(nameof(PixelFormatData));
    }

    #endregion
}
