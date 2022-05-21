using SDL2.Bindings;

namespace SDL2.NET;

public class WindowRenderer : Renderer
{
    public Window AttachedWindow { get; }

    public WindowRenderer(Window window, int index = -1) : this(window, RendererFlags.Accelerated | RendererFlags.Presentvsync, index) { }

    public WindowRenderer(Window window, RendererFlags flags, int index = -1) : base(SDL.SDL_CreateRenderer(window._handle, index, (SDL.SDL_RendererFlags)flags))
    {
        AttachedWindow = window;
    }
}
