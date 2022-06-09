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
/// An object that contains a rendering state. <see href="https://wiki.libsdl.org/SDL_Renderer"/>
/// </summary>
/// <remarks>Warning: Forgetting to call <see cref="Present"/> will build up memory usage, which will suddenly drop when <see cref="Present"/> is finally called</remarks>
public abstract class Renderer : IDisposable
{
    internal readonly IntPtr _handle = IntPtr.Zero;

    internal Renderer(IntPtr handle)
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
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawLine(int originX, int originY, int destX, int destY, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLine(_handle, originX, originY, destX, destY), 0);
    }

    /// <summary>
    /// Draw a line on the current rendering target. <see cref="SDL_RenderDrawLine" href="https://wiki.libsdl.org/SDL_RenderDrawLine"/>
    /// </summary>
    /// <param name="origin">The origin point</param>
    /// <param name="destination">The destination point</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawLine(Point origin, Point destination, RGBAColor? color = null) => DrawLine(origin.X, origin.Y, destination.X, destination.Y, color);

    /// <summary>
    /// Draw a line on the current rendering target at subpixel precision. <see cref="SDL_RenderDrawLineF" href="https://wiki.libsdl.org/SDL_RenderDrawLineF"/>
    /// </summary>
    /// <param name="originX">The X coordinate of the origin point</param>
    /// <param name="originY">The Y coordinate of the origin point</param>
    /// <param name="destX">The X coordinate of the destination point</param>
    /// <param name="destY">The Y coordinate of the destination point</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawLine(float originX, float originY, float destX, float destY, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLineF(_handle, originX, originY, destX, destY), 0);
    }

    /// <summary>
    /// Draw a line on the current rendering target. <see cref="SDL_RenderDrawLineF" href="https://wiki.libsdl.org/SDL_RenderDrawLineF"/>
    /// </summary>
    /// <param name="origin">The origin point</param>
    /// <param name="destination">The destination point</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawLine(FPoint origin, FPoint destination, RGBAColor? color = null) => DrawLine(origin.X, origin.Y, destination.X, destination.Y, color);

    /// <summary>
    /// Draw a series of connected lines on the current rendering target. <see cref="SDL_RenderDrawLines" href="https://wiki.libsdl.org/SDL_RenderDrawLines"/>
    /// </summary>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawLines(ReadOnlySpan<Point> points, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        Span<SDL_Point> sdl_p = stackalloc SDL_Point[points.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            points[i].ToSDL(out sdl_p[i]);

        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLines(_handle, sdl_p, points.Length), 0);
    }

    /// <summary>
    /// Draw a series of connected lines on the current rendering target. <see cref="SDL_RenderDrawLinesF" href="https://wiki.libsdl.org/SDL_RenderDrawLinesF"/>
    /// </summary>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawLines(ReadOnlySpan<FPoint> points, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        Span<SDL_FPoint> sdl_p = stackalloc SDL_FPoint[points.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            points[i].ToSDL(out sdl_p[i]);

        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawLinesF(_handle, sdl_p, points.Length), 0);
    }

    /// <summary>
    /// Draw a point on the current rendering target. <see cref="SDL_RenderDrawPoint" href="https://wiki.libsdl.org/SDL_RenderDrawPoint"/>
    /// </summary>
    /// <remarks>
    /// <see cref="DrawPoint(int, int)"/> draws a single point. If you want to draw multiple, use <see cref="DrawPoints(ReadOnlySpan{Point})"/> instead.
    /// </remarks>
    /// <param name="x">The X coordinate for the point to draw in the screen</param>
    /// <param name="y">The Y coordinate for the point to draw in the screen</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawPoint(int x, int y, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawPoint(_handle, x, y), 0);
    }

    /// <summary>
    /// Draw a point on the current rendering target. <see cref="SDL_RenderDrawPoint" href="https://wiki.libsdl.org/SDL_RenderDrawPoint"/>
    /// </summary>
    /// <remarks>
    /// <see cref="DrawPoint(Point)"/> draws a single point. If you want to draw multiple, use <see cref="DrawPoints(ReadOnlySpan{Point})"/> instead.
    /// </remarks>
    /// <param name="point">The point to draw in the screen</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawPoint(Point point, RGBAColor? color = null) => DrawPoint(point.X, point.Y, color);

    /// <summary>
    /// Draw multiple points on the current rendering target. <see cref="SDL_RenderDrawPoints" href="https://wiki.libsdl.org/SDL_RenderDrawPoints"/>
    /// </summary>
    /// <param name="points">The points to draw</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawPoints(ReadOnlySpan<Point> points, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        Span<SDL_Point> sdl_p = stackalloc SDL_Point[points.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            points[i].ToSDL(out sdl_p[i]);
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawPoints(_handle, sdl_p, points.Length), 0);
    }

    /// <summary>
    /// Draw a rectangle on the current rendering target. <see cref="SDL_RenderDrawRect" href="https://wiki.libsdl.org/SDL_RenderDrawRect"/>
    /// </summary>
    /// <param name="rectangle">A <see cref="Rectangle"/> representing the rectangle to draw, or <see cref="null"/> to outline the entire rendering target</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawRectangle(Rectangle? rectangle, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        if (rectangle is Rectangle r)
        {
            r.ToSDL(out var rect);
            TrySetColor(color);
            SDLRendererException.ThrowIfLessThan(SDL_RenderDrawRect(_handle, ref rect), 0);
            return;
        }
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawRect(_handle, IntPtr.Zero), 0);
    }

    /// <summary>
    /// Fill a rectangle on the current rendering target with the drawing color. <see cref="SDL_RenderFillRect" href="https://wiki.libsdl.org/SDL_RenderFillRect"/>
    /// </summary>
    /// <remarks>
    /// The current drawing color is set by <see cref="RenderColor"/>, and the color's alpha value is ignored unless blending is enabled with the appropriate call to <see cref="BlendMode"/>.
    /// </remarks>
    /// <param name="rectangle">The <see cref="Rectangle"/> representing the rectangle to fill, or <see cref="null"/> for the entire rendering target</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void FillRectangle(Rectangle? rectangle, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        if (rectangle is Rectangle r)
        {
            r.ToSDL(out var rect);
            TrySetColor(color);
            SDLRendererException.ThrowIfLessThan(SDL_RenderFillRect(_handle, ref rect), 0);
            return;
        }
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderFillRect(_handle, IntPtr.Zero), 0);
    }

    /// <summary>
    /// Draw some number of rectangles on the current rendering target. <see cref="SDL_RenderDrawRects" href="https://wiki.libsdl.org/SDL_RenderDrawRects"/>
    /// </summary>
    /// <param name="rectangles">A set of <see cref="Rectangle"/>s representing the rectangles to be drawn</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void DrawRectangles(ReadOnlySpan<Rectangle> rectangles, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        Span<SDL_Rect> sdl_p = stackalloc SDL_Rect[rectangles.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            rectangles[i].ToSDL(out sdl_p[i]);
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderDrawRects(_handle, sdl_p, rectangles.Length), 0);
    }

    /// <summary>
    /// Fill some number of rectangles on the current rendering target with the drawing color. <see cref="SDL_RenderFillRects" href="https://wiki.libsdl.org/SDL_RenderFillRects"/>
    /// </summary>
    /// <param name="rectangles">A set of <see cref="Rectangle"/>s representing the rectangles to be filled</param>
    /// <param name="color">The <see cref="RGBAColor"/> to use when drawing this and next elements. Sets <see cref="RenderColor"/></param>
    public void FillRectangles(ReadOnlySpan<Rectangle> rectangles, RGBAColor? color = null)
    {
        ThrowIfDisposed();
        Span<SDL_Rect> sdl_p = stackalloc SDL_Rect[rectangles.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            rectangles[i].ToSDL(out sdl_p[i]);
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderFillRects(_handle, sdl_p, rectangles.Length), 0);
    }

    /// <summary>
    /// Update the screen with any rendering performed since the previous call. <see cref="SDL_RenderPresent" href="https://wiki.libsdl.org/SDL_RenderPresent"/>
    /// </summary>
    /// <remarks>
    /// SDL's rendering functions operate on a backbuffer; that is, calling a rendering function such as <see cref="DrawLine"/> does not directly put a line on the screen, but rather updates the backbuffer. As such, you compose your entire scene and present the composed backbuffer to the screen as a complete picture. 
    /// </remarks>
    public void Present()
    {
        ThrowIfDisposed();
        SDL_RenderPresent(_handle);
    }

    /// <summary>
    /// Clear the current rendering target with the drawing color. <see cref="SDL_RenderClear" href="https://wiki.libsdl.org/SDL_RenderClear"/>
    /// </summary>
    /// <remarks>
    /// This function clears the entire rendering target, ignoring the viewport and the clip rectangle.
    /// </remarks>
    public void Clear(RGBAColor? color = null)
    {
        ThrowIfDisposed();
        TrySetColor(color);
        SDLRendererException.ThrowIfLessThan(SDL_RenderClear(_handle), 0);
    }

    /// <summary>
    /// Read pixels from the current rendering target to an array of pixels. <see cref="SDL_RenderReadPixels" href="https://wiki.libsdl.org/SDL_RenderReadPixels"/>
    /// </summary>
    /// <param name="format">Value of the desired format of the pixel data, or 0 to use the format of the rendering target</param>
    /// <param name="pixels">An array to copy pixel data onto</param>
    /// <param name="pitch">The pitch of the <paramref name="pixels"/> parameter</param>
    /// <remarks><b>WARNING: </b>This is a very slow operation, and should not be used frequently. If you're using this on the main rendering target, it should be called after rendering and before <see cref="Present"/></remarks>
    /// <exception cref="NotImplementedException"></exception>
    public void ReadPixels(PixelFormat format, object[] pixels, int pitch)
    {
        ThrowIfDisposed();
        throw new NotImplementedException();
    }
#warning Not Implemented

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
    private void TrySetColor(RGBAColor? color)
    {
        if (color is RGBAColor c)
            RenderColor = c;
    }

    /// <summary>
    /// Gets or Sets whether or VSync is enabled on <see cref="this"/> <see cref="Renderer"/> or not
    /// </summary>
    public bool IsVSyncEnabled
    {
        get => RendererInfo.Flags.HasFlag(RendererFlags.PresentVSync);
        set
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfNotEquals(SDL_RenderSetVSync(_handle, value ? 1 : 0), 0);
        }
    }

    /// <summary>
    /// Gets or sets the drawing area for rendering on the current target. get: <see cref="SDL_RenderGetViewport" href="https://wiki.libsdl.org/SDL_RenderGetViewport"/>; set: <see cref="SDL_RenderSetViewport" href="https://wiki.libsdl.org/SDL_RenderSetViewport"/>
    /// </summary>
    /// <remarks>When the <see cref="Window"/> is resized, the viewport is reset to fill the entire new <see cref="Window"/> size.</remarks>
    /// <param name="rect">The <see cref="Rectangle"/> representing the drawing area, or <see cref="null"/> to set the viewport to the entire target</param>
    public Rectangle? Viewport
    {
        get
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_RenderGetViewport(_handle, out SDL_Rect rect), 0);
            return rect;
        }
        set
        {
            ThrowIfDisposed();
            if (value is Rectangle r)
            {
                r.ToSDL(out var sdl_r);
                SDLRendererException.ThrowIfLessThan(SDL_RenderSetViewport(_handle, ref sdl_r), 0);
            }
            SDLRendererException.ThrowIfLessThan(SDL_RenderSetViewport(_handle, IntPtr.Zero), 0);
        }
    }

    /// <summary>
    /// Gets or sets device independent resolution for rendering. get: <see cref="SDL_RenderGetLogicalSize" href="https://wiki.libsdl.org/SDL_RenderGetLogicalSize"/>; set: <see cref="SDL_RenderSetLogicalSize" href="https://wiki.libsdl.org/SDL_RenderSetLogicalSize"/>
    /// </summary>
    /// <remarks>
    /// This function uses the viewport and scaling functionality to allow a fixed logical resolution for rendering, regardless of the actual output resolution. If the actual output resolution doesn't have the same aspect ratio the output rendering will be centered within the output display. If the output display is a window, mouse and touch events in the window will be filtered and scaled so they seem to arrive within the logical resolution. The <see cref="HintTypes.MouseRelativeScaling"/> hint controls whether relative motion events are also scaled. If this function results in scaling or subpixel drawing by the rendering backend, it will be handled using the appropriate quality hints.
    /// </remarks>
    public Size LogicalSize
    {
        get
        {
            ThrowIfDisposed();
            SDL_RenderGetLogicalSize(_handle, out int w, out int h);
            return new(w, h);
        }
        set
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_RenderSetLogicalSize(_handle, value.Width, value.Height), 0);
        }
    }

    /// <summary>
    /// Get whether clipping is enabled on the given renderer. <see cref="SDL_RenderIsClipEnabled" href="https://wiki.libsdl.org/SDL_RenderIsClipEnabled"/>
    /// </summary>
    public bool IsClipEnabled
    {
        get
        {
            ThrowIfDisposed();
            return SDL_RenderIsClipEnabled(_handle) is SDL_bool.SDL_TRUE;
        }
    }

    /// <summary>
    /// Gets or sets the clip rectangle for rendering on the specified target, a <see cref="Rectangle"/> filled in with the current clipping area or an <see cref="null"/> if clipping is disabled
    /// </summary>
    public Rectangle? Clip
    {
        get
        {
            ThrowIfDisposed();
            SDL_RenderGetClipRect(_handle, out var rect);
            Rectangle r = (Rectangle)rect;
            return r.Size != default ? r : null;
        }
        set
        {
            ThrowIfDisposed();
            if (value is Rectangle r)
            {
                r.ToSDL(out var sl);
                SDLRendererException.ThrowIfLessThan(SDL_RenderSetClipRect(_handle, ref sl), 0);
                return;
            }
            SDLRendererException.ThrowIfLessThan(SDL_RenderSetClipRect(_handle, IntPtr.Zero), 0);
        }
    }

    /// <summary>
    /// Gets or sets whether to force integer scales for resolution-independent rendering. <see cref="SDL_RenderGetIntegerScale" href="https://wiki.libsdl.org/SDL_RenderGetIntegerScale"/>; set: <see cref="SDL_RenderSetIntegerScale" href="https://wiki.libsdl.org/SDL_RenderSetIntegerScale"/>
    /// </summary>
    public bool IntegerScale
    {
        get
        {
            ThrowIfDisposed();
            return SDL_RenderGetIntegerScale(_handle) is SDL_bool.SDL_TRUE;
        }
        set
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_RenderSetIntegerScale(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE), 0);
        }
    }

    /// <summary>
    /// Gets or sets the drawing scale for rendering on the current target. get: <see cref="SDL_RenderGetScale" href="https://wiki.libsdl.org/SDL_RenderGetScale"/>; set: <see cref="SDL_RenderSetScale" href="https://wiki.libsdl.org/SDL_RenderSetScale"/>
    /// </summary>
    /// <remarks>
    /// The drawing coordinates are scaled by the x/y (width/height) scaling factors before they are used by the renderer. This allows resolution independent drawing with a single coordinate system.
    /// </remarks>
    public FSize Scale
    {
        get
        {
            ThrowIfDisposed();
            SDL_RenderGetScale(_handle, out float sx, out float sy);
            return new(sx, sy);
        }
        set
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_RenderSetScale(_handle, value.Width, value.Height), 0);
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
            ThrowIfDisposed();
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
        get
        {
            ThrowIfDisposed();
            return renderTarget;
        }

        set
        {
            ThrowIfDisposed();
            SDLRendererException.ThrowIfLessThan(SDL_SetRenderTarget(_handle, (renderTarget = value)?._handle ?? IntPtr.Zero), 0);
        }
    }

    /// <summary>
    /// Determine whether a renderer supports the use of render targets. <see cref="SDL_RenderTargetSupported" href="https://wiki.libsdl.org/SDL_RenderTargetSupported"/>
    /// </summary>
    public bool SupportsRenderTargets
    {
        get
        {
            ThrowIfDisposed();
            return SDL_RenderTargetSupported(_handle) is SDL_bool.SDL_TRUE;
        }
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
        ThrowIfDisposed();
        var ptr = SDL_GetRenderTarget(_handle);
        return ptr == IntPtr.Zero
            ? (renderTarget = null)
            : Texture.FetchTexture(ptr) ?? throw new SDLRendererException("This Renderer targets a Texture that is not indexed by this library");
    }

    /// <summary>
    /// Render a list of triangles in <see cref="this"/> <see cref="Renderer"/>, optionally with <paramref name="indices"/> into the vertex array. Color and alpha modulation is done per vertex (<see cref="Color"/> and <see cref="Alpha"/> are ignored). <see cref="SDL_RenderGeometry"/>
    /// </summary>
    /// <remarks>See <see cref="Texture.RenderGeometry(ReadOnlySpan{Vertex}, ReadOnlySpan{int})"/> to use a <see cref="Texture"/> as well</remarks>
    /// <param name="vertices">The list of vertices to use when drawing the triangles</param>
    /// <param name="indices">The indices into the vertex array</param>
    public void RenderGeometry(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices = default)
        => RenderGeometry(this, null, vertices, indices);

    internal static void RenderGeometry(Renderer renderer, Texture? texture, ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices = default)
    {
        Span<SDL_Vertex> sdl_v = stackalloc SDL_Vertex[vertices.Length]; //If the array is too large, maybe use a heap allocated one instead? -- Is there a way to avoid this conversion altogether?
        for (int i = 0; i < sdl_v.Length; i++)
            vertices[i].ToSDL(ref sdl_v[i]);

        SDLRendererException.ThrowIfLessThan(SDL_RenderGeometry(renderer._handle, texture?._handle ?? IntPtr.Zero, sdl_v, sdl_v.Length, indices, indices.Length), 0);
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
            ThrowIfDisposed();
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
