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
    private Texture? renderTarget;
    /// <summary>
    /// The current render target or <see cref="null"/> for the default render target. get: Cached in .NET memory; set: <see cref="SDL_SetRenderTarget" href="https://wiki.libsdl.org/SDL_SetRenderTarget"/>
    /// </summary>
    /// <remarks>
    /// The Render Target is assumed to be <see cref="null"/> when the object is created. From then onwards, assignment of this property will result in the <see cref="Texture"/> object being cached; if you want to use SDL's default method for this, see <see cref="FetchRenderTarget"/>. This involves indexing of an internal static <see cref="System.Collections.Concurrent.ConcurrentDictionary{IntPtr, WeakReference{Texture}}"/>
    /// </remarks>
    public Texture? RenderTarget
    {
        get => renderTarget;
        set => SDLRendererException.ThrowIfLessThan(SDL_SetRenderTarget(_handle, (renderTarget = value)?._handle ?? IntPtr.Zero), 0);
    }

    /// <summary>
    /// Fetches the Texture from SDL and queries the returned pointer, use in case you don't trust <see cref="RenderTarget"/> to have a valid reference to the appropriate <see cref="Texture"/> for some reason. <see cref="SDL_GetRenderTarget" href="https://wiki.libsdl.org/SDL_GetRenderTarget"/>
    /// </summary>
    /// <remarks>
    /// This method also automatically refreshses the cache for <see cref="get_RenderTarget"/>
    /// </remarks>
    /// <returns>The Texture object, after it has been assigned to the renderTarget cache</returns>
    public Texture? FetchRenderTarget()
    {
        var ptr = SDL_GetRenderTarget(_handle);
        return ptr == IntPtr.Zero
            ? (renderTarget = null)
            : Texture.FetchTexture(ptr) ?? throw new SDLRendererException("This Renderer targets a Texture that is not indexed by this library");
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
