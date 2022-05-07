using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

    /// <summary>
    /// Draw a line on the current rendering target. <see cref="SDL_RenderDrawLine" href="https://wiki.libsdl.org/SDL_RenderDrawLine"/>
    /// </summary>
    /// <param name="originX">The X coordinate of the origin point</param>
    /// <param name="originY">The Y coordinate of the origin point</param>
    /// <param name="destX">The X coordinate of the destination point</param>
    /// <param name="destY">The Y coordinate of the destination point</param>
    public void DrawLine(int originX, int originY, int destX, int destY)
        => SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLine(_handle, originX, originY, destX, destY), 0);

    /// <summary>
    /// Draw a line on the current rendering target. <see cref="SDL_RenderDrawLine" href="https://wiki.libsdl.org/SDL_RenderDrawLine"/>
    /// </summary>
    /// <param name="origin">The origin point</param>
    /// <param name="destination">The destination point</param>
    public void DrawLine(Point origin, Point destination) => DrawLine(origin.X, origin.Y, destination.X, destination.Y);

    /// <summary>
    /// Draw a line on the current rendering target at subpixel precision. <see cref="SDL_RenderDrawLineF" href="https://wiki.libsdl.org/SDL_RenderDrawLineF"/>
    /// </summary>
    /// <param name="originX">The X coordinate of the origin point</param>
    /// <param name="originY">The Y coordinate of the origin point</param>
    /// <param name="destX">The X coordinate of the destination point</param>
    /// <param name="destY">The Y coordinate of the destination point</param>
    public void DrawLine(float originX, float originY, float destX, float destY)
        => SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLineF(_handle, originX, originY, destX, destY), 0);

    /// <summary>
    /// Draw a line on the current rendering target. <see cref="SDL_RenderDrawLineF" href="https://wiki.libsdl.org/SDL_RenderDrawLineF"/>
    /// </summary>
    /// <param name="origin">The origin point</param>
    /// <param name="destination">The destination point</param>
    public void DrawLine(FPoint origin, FPoint destination) => DrawLine(origin.X, origin.Y, destination.X, destination.Y);

    /// <summary>
    /// Draw a series of connected lines on the current rendering target. <see cref="SDL_RenderDrawLines" href="https://wiki.libsdl.org/SDL_RenderDrawLines"/>
    /// </summary>
    public void DrawLines(ReadOnlySpan<Point> points)
    {
        Span<SDL_Point> sdl_p = stackalloc SDL_Point[points.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            points[i].ToSDLPoint(ref sdl_p[i]);

        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLines(_handle, sdl_p, points.Length), 0);
    }

    /// <summary>
    /// Draw a series of connected lines on the current rendering target. <see cref="SDL_RenderDrawLinesF" href="https://wiki.libsdl.org/SDL_RenderDrawLinesF"/>
    /// </summary>
    public void DrawLines(ReadOnlySpan<FPoint> points)
    {
        Span<SDL_FPoint> sdl_p = stackalloc SDL_FPoint[points.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            points[i].ToSDLFPoint(ref sdl_p[i]);

        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLinesF(_handle, sdl_p, points.Length), 0);
    }

    /// <summary>
    /// The blend mode used for drawing operations. get: <see cref="SDL_GetRenderDrawBlendMode" href="https://wiki.libsdl.org/SDL_GetRenderDrawBlendMode"/>; set: <see cref="SDL_SetRenderDrawBlendMode" href="https://wiki.libsdl.org/SDL_SetRenderDrawBlendMode"/>
    /// </summary>
    public SDL_BlendMode BlendMode
    {
        get
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_GetRenderDrawBlendMode(_handle, out var mode), 0);
            return mode;
        }
        set
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_SetRenderDrawBlendMode(_handle, value), 0);
        }
    }

    /// <summary>
    /// Get the color used for drawing operations (Rect, Line and Clear). get: <see cref="SDL_GetRenderDrawColor" href="https://wiki.libsdl.org/SDL_GetRenderDrawColor"/>; set: <see cref="SDL_SetRenderDrawColor" href="https://wiki.libsdl.org/SDL_SetRenderDrawColor"/>
    /// </summary>
    public RGBAColor RenderColor
    {
        get
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_GetRenderDrawColor(_handle, out byte r, out byte g, out byte b, out byte a), 0);
            return new(r, g, b, a);
        }
        set
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_SetRenderDrawColor(_handle, value.Red, value.Green, value.Blue, value.Alpha), 0);
        }
    }

    /// <summary>
    /// Get the output size in pixels of a rendering context. <see cref="SDL_GetRendererOutputSize" href="https://wiki.libsdl.org/SDL_GetRendererOutputSize"/>
    /// </summary>
    /// <remarks>Due to high-dpi displays, you might end up with a rendering context that has more pixels than the window that contains it, so use this instead of <see cref="Window.Size"/> to decide how much drawing area you have.</remarks>
    public Size OutputSize
    {
        get
        {
            SDLRendererException.ThrowIfLessThan(SDL_GetRendererOutputSize(_handle, out var w, out var h), 0);
            return new(w, h);
        }
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

    private bool HasRendererInfo;
    private RendererInfo CachedInfo;
    /// <summary>
    /// Get information about a rendering context. <see cref="SDL_GetRendererInfo" href="https://wiki.libsdl.org/SDL_GetRendererInfo"/>
    /// </summary>
    /// <remarks>Due to complications in the marshalling process, the info is only actually requested once. This is a cached property.</remarks>
    public unsafe RendererInfo RendererInfo
    {
        get
        {
            if (HasRendererInfo)
                return CachedInfo;
            SDLRendererException.ThrowIfLessThan(SDL_GetRendererInfo(_handle, out var sdli), 0);
            HasRendererInfo = true;
            return CachedInfo = new RendererInfo(Marshal.PtrToStringAnsi(sdli.name)!, (RendererFlags)sdli.flags, new(sdli.max_texture_width, sdli.max_texture_height), new(sdli.texture_formats, sdli.num_texture_formats));
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
