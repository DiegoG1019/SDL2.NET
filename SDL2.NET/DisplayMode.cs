using System.Text.Json.Serialization;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents information on a Display's display mode
/// </summary>
public struct DisplayMode
{
    public DisplayMode(PixelFormat format, int width, int height, int refreshRate)
    {
        Format = format;
        Width = width;
        Height = height;
        RefreshRate = refreshRate;
    }

    /// <summary>
    /// The Pixel format used by the display
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// The Width of the display in screen coordinates
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// The Height of the display in screen coordinates
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// The refresh rate of the display in Hz, or 0 if unspecified
    /// </summary>
    public int RefreshRate { get; }

    /// <summary>
    /// The Size of the desktop screen
    /// </summary>
    [JsonIgnore]
    public Size Size => new(Width, Height);

    public static explicit operator DisplayMode(SDL_DisplayMode mode)
        => new((PixelFormat)mode.format, mode.w, mode.h, mode.refresh_rate);

    public static explicit operator SDL_DisplayMode(DisplayMode mode)
        => new()
        {
            w = mode.Width,
            h = mode.Height,
            format = (uint)mode.Format,
            refresh_rate = mode.RefreshRate
        };
}
