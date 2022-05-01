using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;
public class Renderer : IDisposable
{
#error Not Implemented
    internal readonly IntPtr _handle = IntPtr.Zero;
    private readonly Window AttachedWindow;

    public Renderer(Window window, int index) : this(window, index, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC) { }

    public Renderer(Window window, int index, SDL.SDL_RendererFlags flags)
    {
        _handle = SDL.SDL_CreateRenderer(window._handle, -1, flags);
        if (_handle == IntPtr.Zero)
            throw new SDLRendererCreationException(SDL.SDL_GetError());
        AttachedWindow = window;
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

    #endregion
}
