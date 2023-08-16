using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies a variable controlling which orientations are allowed on iOS.
/// </summary>
/// <remarks>By default all orientations are allowed.</remarks>
public sealed class HintOrientations : Hint
{
    internal HintOrientations() : base(SDL_HINT_ORIENTATIONS) { }

    public void SetPriority(AllowedOrientation quality, HintPriority priority)
        => Set(((int)quality).ToString(), priority);

    private AllowedOrientation? _c = AllowedOrientation.LandscapeRight | AllowedOrientation.LandscapeLeft | AllowedOrientation.Portrait | AllowedOrientation.PortraitUpsideDown;

    /// <summary>
    /// Retrieves the hint string from SDL and parses it, updating the cache
    /// </summary>
    /// <returns></returns>
    public AllowedOrientation UpdateAllowedOrientations()
    {
        var c = Parse(Get() ?? "");
        _c = c;
        return c;
    }

    /// <summary>
    /// Gets or sets the allowed orientations set in this hint
    /// </summary>
    /// <remarks>
    /// The currently allowed orientations are cached, and can be forcefully updated with <see cref="UpdateAllowedOrientations"/>
    /// </remarks>
    public AllowedOrientation AllowedOrientations
    {
        get => _c ??= Parse(Get() ?? "");
        set => Set((_c = value)!.ToString().Replace(",", ""));
    }

    private AllowedOrientation Parse(string str)
    {
        AllowedOrientation a = 0;
        foreach (var i in str.Split(' '))
            a |= Enum.Parse<AllowedOrientation>(i);
        return a;
    }
}
