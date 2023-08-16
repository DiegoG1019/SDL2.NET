using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies which render driver to use.
/// </summary>
/// <remarks>
/// By default the first one in the list that is available on the current platform is chosen. If the application doesn't pick a specific renderer to use, this variable specifies the name of the preferred renderer. If the preferred renderer can't be initialized, the normal default renderer is used.
/// </remarks>
public sealed class RenderDriverHint : Hint
{
    internal RenderDriverHint() : base(SDL_HINT_ORIENTATIONS) { }

    public void SetPriority(RenderDriver quality, HintPriority priority)
        => Set(((int)quality).ToString(), priority);

    private RenderDriver? _c;

    /// <summary>
    /// Retrieves the hint string from SDL and parses it, updating the cache
    /// </summary>
    /// <returns></returns>
    public RenderDriver UpdateRenderDrivers()
    {
        var c = Parse(Get() ?? "");
        _c = c;
        return c;
    }

    /// <summary>
    /// Gets or sets the driver to use
    /// </summary>
    /// <remarks>
    /// The currently set driver is cached, and can be forcefully updated with <see cref="UpdateRenderDrivers"/>
    /// </remarks>
    public RenderDriver RenderDrivers
    {
        get => _c ??= Parse(Get() ?? "");
        set => Set(GetString(value));
    }

    private string GetString(RenderDriver driver)
        => driver switch
        {
            RenderDriver.Direct3D => "direct3d",
            RenderDriver.OpenGL => "opengl",
            RenderDriver.OpenGLES2 => "opengles2",
            RenderDriver.OpenGLES => "opengles",
            RenderDriver.Metal => "metal",
            RenderDriver.Software => "software",
            _ => ""
        };

    private RenderDriver Parse(string str)
        => str switch
        {
            "direct3d" => RenderDriver.Direct3D,
            "opengl" => RenderDriver.OpenGL,
            "opengles2" => RenderDriver.OpenGLES2,
            "opengles" => RenderDriver.OpenGLES,
            "metal" => RenderDriver.Metal,
            "software" => RenderDriver.Software,
            _ => 0
        };
}
