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