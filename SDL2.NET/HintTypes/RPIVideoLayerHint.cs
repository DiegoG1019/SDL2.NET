using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies which Dispmanx layer SDL should use on a Raspberry PI.
/// </summary>
/// <remarks>
/// By default the Dispmanx layer is "10000". This is also known as Z-order. The variable can take a negative or positive value.
/// </remarks>
public sealed class RPIVideoLayerHint : Hint
{
    internal RPIVideoLayerHint() : base(SDL_HINT_RPI_VIDEO_LAYER) { }

    public void SetPriority(int quality, HintPriority priority)
        => Set(quality.ToString(), priority);

    /// <summary>
    /// Gets or sets the Dispmanx layer to use
    /// </summary>
    public int RenderDrivers
    {
        get => int.Parse(Get() ?? "0");
        set => Set(value.ToString());
    }
}