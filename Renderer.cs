using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace SDL2.NET;

public abstract class Renderer : IDisposable
{
    protected internal readonly IntPtr _handle = IntPtr.Zero;

    protected Renderer(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLRendererCreationException(SDL_GetError());
    }
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL.SDL_DestroyRenderer(_handle);
            disposedValue = true;
        }
    }

    ~Renderer()
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
            throw new ObjectDisposedException(nameof(Renderer));
    }

    #endregion
}
