using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SDL2.Bindings;
using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

public class PixelFormatData : IDisposable
{
    internal readonly IntPtr _handle;

    internal PixelFormatData(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLSurfaceCreationException(SDL_GetError());

        var f = Marshal.PtrToStructure<SDL_PixelFormat>(handle);
        Format = (PixelFormat)f.format;
        Palette = f.palette == IntPtr.Zero ? null : Palette.FetchOrNew(f.palette);
        BitsPerPixel = f.BitsPerPixel;
        BytesPerPixel = f.BytesPerPixel;
        RedMask = f.Rmask;
        GreenMask = f.Gmask;
        BlueMask = f.Bmask;
        AlphaMask = f.Amask;
    }

    public PixelFormatData(PixelFormat format)
        : this(SDL_AllocFormat((uint)format)) { }

    /// <summary>
    /// The <see cref="PixelFormat"/> of this data object
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// The <see cref="Palette"/> associated with this <see cref="PixelFormatData"/> or null if it doesn't have a palette
    /// </summary>
    public Palette? Palette { get; }

    /// <summary>
    /// The number of significant bits in a pixel value
    /// </summary>
    /// <remarks>i.e.: 8, 15, 16, 24, 32</remarks>
    public byte BitsPerPixel { get; }

    /// <summary>
    /// The number of bytes required to hold a pixel value
    /// </summary>
    /// <remarks>i.e.: 1 (<see cref="byte"/>), 2 (<see cref="ushort"/>), 3 (<see cref="(byte red, byte green, byte blue)"/> or <see cref="RGBColor"/>), 4 (<see cref="uint"/> or <see cref="RGBAColor"/>)</remarks>
    public byte BytesPerPixel { get; }

    /// <summary>
    /// A mask representing the location of the red component of the pixel
    /// </summary>
    public uint RedMask { get; }

    /// <summary>
    /// A mask representing the location of the green component of the pixel
    /// </summary>
    public uint GreenMask { get; }

    /// <summary>
    /// A mask representing the location of the blue component of the pixel
    /// </summary>
    public uint BlueMask { get; }

    /// <summary>
    /// A mask representing the location of the alpha component of the pixel or 0 if the pixel format doesn't have any alpha information
    /// </summary>
    public uint AlphaMask { get; }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_FreeFormat(_handle);
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
