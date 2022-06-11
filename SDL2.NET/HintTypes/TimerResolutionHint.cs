using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies the timer resolution in milliseconds.
/// </summary>
/// <remarks>
/// The higher resolution the timer, the more frequently the CPU services timer interrupts, and the more precise delays are, but this takes up power and CPU time. This hint is only used on Windows, but not supported on WinRT. If this variable is set to "0", the system timer resolution is not set. This hint may be set at any time. See this blog post for more information: <see href="http://randomascii.wordpress.com/2013/07/08/windows-timer-resolution-megawatts-wasted/"/>
/// </remarks>
public sealed class TimerResolutionHint : Hint
{
    internal TimerResolutionHint() : base(SDL_HINT_TIMER_RESOLUTION) { }

    public void SetPriority(uint quality, HintPriority priority)
        => Set(quality.ToString(), priority);

    /// <summary>
    /// Gets or sets the Stack size to use. The default value is 1
    /// </summary>
    public uint Resolution
    {
        get => uint.Parse(Get() ?? "0");
        set => Set(value.ToString());
    }
}