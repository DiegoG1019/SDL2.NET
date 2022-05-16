using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// <param name="destination"></param>
    /// <param name="sourceRect"></param>
    /// <param name="destinationRect"></param>
    public void Blit(Surface destination, Rectangle? sourceRect, Rectangle? destinationRect)
    {
        
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