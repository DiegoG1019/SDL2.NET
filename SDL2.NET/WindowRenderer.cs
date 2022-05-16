using SDL2.Bindings;

namespace SDL2.NET;

public class WindowRenderer : Renderer
{
    public Window AttachedWindow { get; }

    public WindowRenderer(Window window, int index = -1) : this(window, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC, index) { }

    public WindowRenderer(Window window, SDL.SDL_RendererFlags flags, int index = -1) : base(SDL.SDL_CreateRenderer(window._handle, index, flags))
    {
        AttachedWindow = window;
    }
}
