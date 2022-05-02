namespace SDL2.NET;

public class WindowRenderer : Renderer
{
    protected internal readonly Window AttachedWindow;

    public WindowRenderer(Window window, int index) : this(window, index, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC) { }

    public WindowRenderer(Window window, int index, SDL.SDL_RendererFlags flags) : base(SDL.SDL_CreateRenderer(window._handle, -1, flags))
    {
        AttachedWindow = window;
    }
}
