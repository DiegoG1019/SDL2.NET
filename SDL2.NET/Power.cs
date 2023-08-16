using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents a way to access the system's power status
/// </summary>
public static class Power
{
    /// <summary>
    /// Get the current power supply details.
    /// </summary>
    public static PowerStatus PowerState => new((PowerState)SDL_GetPowerInfo(out int secs, out int perc), TimeSpan.FromSeconds(secs), perc);
}
