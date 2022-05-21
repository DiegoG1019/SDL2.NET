namespace SDL2.NET.SDLImage;

/// <summary>
/// An animation frame to be used within <see cref="SDLAnimation"/>
/// </summary>
public sealed class SDLAnimationFrame
{
    internal SDLAnimationFrame(Surface surface, int delay)
    {
        Surface = surface;
        Delay = delay;
    }

    /// <summary>
    /// The image data representing the frame
    /// </summary>
    public Surface Surface { get; }

    /// <summary>
    /// The delay in milliseconds
    /// </summary>
    public int Delay { get; }
}
