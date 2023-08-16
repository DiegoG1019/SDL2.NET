using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies which render driver to use.
/// </summary>
/// <remarks>
/// By default the first one in the list that is available on the current platform is chosen. If the application doesn't pick a specific renderer to use, this variable specifies the name of the preferred renderer. If the preferred renderer can't be initialized, the normal default renderer is used.
/// </remarks>
public sealed class D3DCompilerHint : Hint
{
    internal D3DCompilerHint() : base(SDL_HINT_VIDEO_WIN_D3DCOMPILER) { }

    public void SetPriority(D3DCompiler quality, HintPriority priority)
        => Set(((int)quality).ToString(), priority);

    private D3DCompiler? _c;

    /// <summary>
    /// Retrieves the hint string from SDL and parses it, updating the cache
    /// </summary>
    /// <returns></returns>
    public D3DCompiler UpdateD3DCompilers()
    {
        var c = Parse(Get() ?? "");
        _c = c;
        return c;
    }

    /// <summary>
    /// Gets or sets the driver to use
    /// </summary>
    /// <remarks>
    /// The currently set driver is cached, and can be forcefully updated with <see cref="UpdateD3DCompilers"/>
    /// </remarks>
    public D3DCompiler D3DCompiler
    {
        get => _c ??= Parse(Get() ?? "");
        set => Set(GetString(value));
    }

    private string GetString(D3DCompiler driver)
        => driver switch
        {
            D3DCompiler.D3DCompiler46 => "d3dcompiler_46.dll",
            D3DCompiler.D3DCompiler43 => "d3dcompiler_43.dll",
            D3DCompiler.None => "none",
            _ => ""
        };

    private D3DCompiler Parse(string str)
        => str switch
        {
            "" or "d3dcompiler_46.dll" => D3DCompiler.D3DCompiler46,
            "d3dcompiler_43.dll" => D3DCompiler.D3DCompiler43,
            "none" => D3DCompiler.None,
            _ => 0
        };
}
