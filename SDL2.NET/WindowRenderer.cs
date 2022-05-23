using SDL2.Bindings;

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
    /// Instantiates a new <see cref="WindowRenderer"/> with a given <see cref="Window"/>
    /// </summary>
    public WindowRenderer(Window window, int index = -1) : this(window, RendererFlags.Accelerated | RendererFlags.PresentVSync, index) { }

    /// <summary>
    /// Instantiates a new <see cref="WindowRenderer"/> with a given <see cref="Window"/> and <see cref="RendererFlags"/>
    /// </summary>
    public WindowRenderer(Window window, RendererFlags flags, int index = -1) : base(SDL.SDL_CreateRenderer(window._handle, index, (SDL.SDL_RendererFlags)flags))
    {
        AttachedWindow = window;
    }
}
