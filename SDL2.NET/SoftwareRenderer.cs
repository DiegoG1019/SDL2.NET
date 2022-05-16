using SDL2.Bindings;

namespace SDL2.NET;

public class SoftwareRenderer : Renderer
{
    protected internal readonly Surface AttachedSurface;

    public SoftwareRenderer(Surface surface) : base(SDL.SDL_CreateSoftwareRenderer(surface._handle))
    {
        AttachedSurface = surface;
    }
}
