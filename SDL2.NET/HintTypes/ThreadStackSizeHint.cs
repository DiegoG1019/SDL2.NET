using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies a variable specifying SDL's threads stack size in bytes or "0" for the backend's default size. This hint has NO EFFECT on .NET
/// </summary>
/// <remarks>
/// By default the backend's default threads stack size is used. Use this hint in case you need to set SDL's threads stack size to other than the default. This is specially useful if you build SDL against a non glibc libc library (such as musl) which provides a relatively small default thread stack size (a few kilobytes versus the default 8 MB glibc uses).
/// </remarks>
public sealed class ThreadStackSizeHint : Hint
{
    internal ThreadStackSizeHint() : base(SDL_HINT_THREAD_STACK_SIZE) { }

    public void SetPriority(uint quality, HintPriority priority)
        => Set(quality.ToString(), priority);

    /// <summary>
    /// Gets or sets the Stack size to use. Set to 0 to use the default
    /// </summary>
    public uint StackSize
    {
        get => uint.Parse(Get() ?? "0");
        set => Set(value.ToString());
    }
}
