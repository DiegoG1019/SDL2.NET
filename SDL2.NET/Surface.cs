using SDL2.Bindings;
using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// An object that contains a collection of pixels used in software blitting. <see href="https://wiki.libsdl.org/SDL_Surface"/>
/// </summary>
/// <remarks>THIS OBJECT IS NOT FULLY IMPLEMENTED. It is missing the fields the SDL object has. This is a work in progress</remarks>
#warning not fully implemented
public class Surface : IDisposable
{
    protected internal readonly IntPtr _handle = IntPtr.Zero;

    private Surface(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLSurfaceCreationException(SDL_GetError());
    }

    /// <summary>
    /// Creates a new RGB surface
    /// </summary>
    /// <param name="width">The width of the surface</param>
    /// <param name="height">The Height of the surface</param>
    /// <param name="depth">The Depth of the surface in bits</param>
    /// <param name="rmask">The red mask for the pixels</param>
    /// <param name="gmask">The green mask for the pixels</param>
    /// <param name="bmask">The blue mask for the pixels</param>
    /// <param name="amask">The alpha mask for the pixels</param>
    public Surface(int width, int height, int depth, uint rmask, uint gmask, uint bmask, uint amask, PixelFormat? format = null)
        : this(SDL_CreateRGBSurface(0, width, height, depth, rmask, gmask, bmask, amask)) { }

    /// <summary>
    /// Creates a new RGB surface. <see cref="SDL_CreateRGBSurfaceWithFormat" href="https://wiki.libsdl.org/SDL_CreateRGBSurfaceWithFormat"/>
    /// </summary>
    /// <param name="width">The width of the surface</param>
    /// <param name="height">The Height of the surface</param>
    /// <param name="depth">The Depth of the surface in bits</param>
    /// <param name="format">The pixel format to use</param>
    public Surface(int width, int height, int depth, PixelFormat format)
        : this(SDL_CreateRGBSurfaceWithFormat(0, width, height, depth, (uint)format)) { }

#warning missing https://wiki.libsdl.org/SDL_CreateRGBSurfaceFrom

    /// <summary>
    /// The format of the pixels stored in the surface
    /// </summary>
    public PixelFormatData PixelFormatData { get; }

    /// <summary>
    /// Copy an existing <see cref="Surface"/> to a new <see cref="Surface"/> of the specified format. <see cref="SDL_ConvertSurfaceFormat" href="https://wiki.libsdl.org/SDL_ConvertSurfaceFormat"/>
    /// </summary>
    /// <param name="format">the <see cref="PixelFormat"/> that the new surface is optimized for</param>
    /// <returns>Returns the new <see cref="Surface"/> structure that is created</returns>
    public Surface Convert(PixelFormat format) 
        => new(SDL_ConvertSurfaceFormat(_handle, (uint)format, 0));

    /// <summary>
    /// Copy an existing <see cref="Surface"/> to a new <see cref="Surface"/> of the specified format. <see cref="SDL_ConvertSurfaceFormat" href="https://wiki.libsdl.org/SDL_ConvertSurfaceFormat"/>
    /// </summary>
    /// <param name="format">the <see cref="PixelFormat"/> that the new surface is optimized for</param>
    /// <returns>Returns the new <see cref="Surface"/> structure that is created</returns>
    public Surface Convert(PixelFormatData format)
        => new(SDL_ConvertSurface(_handle, format._handle, 0));

    /// <summary>
    /// Perform a fast fill of a <see cref="Rectangle"/> with a specific <see cref="RGBAColor"/>.
    /// </summary>
    /// <param name="rect">The <see cref="Rectangle"/> representing the area to fill, or <see cref="null"/> to fill the entire surface</param>
    /// <param name="color">The color to fill with</param>
    public void FillRectangle(RGBAColor color, Rectangle? rect = null)
    {
        if (rect is Rectangle re)
        {
            SDL_Rect r = default;
            re.ToSDL(ref r);
            SDLSurfaceException.ThrowIfLessThan(SDL_FillRect(_handle, ref r, color.ToUInt32(PixelFormatData)), 0);
        }
        SDLSurfaceException.ThrowIfLessThan(SDL_FillRect(_handle, IntPtr.Zero, color.ToUInt32(PixelFormatData)), 0);
    }

    /// <summary>
    /// Performs a fast surface copy to a destination surface.
    /// </summary>
    /// <remarks>You should call <see cref="Blit"/> unless you know exactly how SDL blitting works internally and how to use the other blit functions. The blit function should not be called on a locked <see cref="Surface"/>.</remarks>
    /// <param name="destination">The blit target</param>
    /// <param name="sourceRect">The <see cref="Rectangle"/> representing the area to be copied, or <see cref="null"/> to copy the entire surface</param>
    /// <param name="destinationRect">The <see cref="Rectangle"/> representing the area to be copied into, or <see cref="null"/> to copy the entire surface. <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> are ignored, and only <see cref="Rectangle.X"/> and <see cref="Rectangle.Y"/> are taken into account</param>
    public void Blit(Surface destination, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        try
        {
            if (sourceRect is Rectangle src)
            {
                srect = Marshal.AllocHGlobal(Marshal.SizeOf(src));
                Marshal.StructureToPtr(src, srect, false);
            }

            if (destinationRect is Rectangle dst)
            {
                drect = Marshal.AllocHGlobal(Marshal.SizeOf(dst));
                Marshal.StructureToPtr(dst, drect, false);
            }

            SDLTextureException.ThrowIfLessThan(SDL_BlitSurface(_handle, srect, destination._handle, drect), 0);
        }
        finally
        {
            if (srect != IntPtr.Zero)
                Marshal.FreeHGlobal(srect);
            if (drect != IntPtr.Zero)
                Marshal.FreeHGlobal(drect);
        }
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL.SDL_FreeSurface(_handle);
            disposedValue = true;
        }
    }

    ~Surface()
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
            throw new ObjectDisposedException(nameof(Surface));
    }

    #endregion
}

#error Not Implemented