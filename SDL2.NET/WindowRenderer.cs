using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents a Renderer that is attached to one specific <see cref="Window"/>
/// </summary>
public class WindowRenderer : Renderer
{
    /// <summary>
    /// The Window this Renderer is attached to
    /// </summary>
    public Window AttachedWindow { get; }

    /// <summary>
    /// Creates a new WindowRenderer object that wraps around the native object at <paramref name="handle"/>
    /// </summary>
    /// <param name="handle">The pointer to the native SDL object</param>
    /// <param name="window">The window this Renderer belongs to</param>
    public WindowRenderer(nint handle, Window window) : base(handle)
    {
        AttachedWindow = window;
        window.AttachedRenderer = this;
    }

    /// <summary>
    /// Instantiates a new <see cref="WindowRenderer"/> with a given <see cref="Window"/>
    /// </summary>
    public WindowRenderer(Window window, int index = -1) : this(window, RendererFlags.Accelerated | RendererFlags.PresentVSync, index) { }

    /// <summary>
    /// Instantiates a new <see cref="WindowRenderer"/> with a given <see cref="Window"/> and <see cref="RendererFlags"/>
    /// </summary>
    public WindowRenderer(Window window, RendererFlags flags, int index = -1) : base(SDL_CreateRenderer(window._handle, index, (SDL_RendererFlags)flags))
    {
        AttachedWindow = window;
        window.AttachedRenderer = this;
    }

    /// <summary>
    /// Translates <see cref="Window"/> coordinates into <see cref="Renderer"/> logical coordinates
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public FPoint WindowToLogical(Point point)
    {
        SDL_RenderWindowToLogical(_handle, point.X, point.Y, out float lx, out float ly);
        return new(lx, ly);
    }

    /// <summary>
    /// Translates <see cref="Renderer"/> logical coordinates into <see cref="Window"/> coordinates
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Point LogicalToWindow(FPoint point)
    {
        SDL_RenderLogicalToWindow(_handle, point.X, point.Y, out int lx, out int ly);
        return new(lx, ly);
    }
}
