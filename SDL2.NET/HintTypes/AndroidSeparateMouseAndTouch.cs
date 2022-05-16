using static SDL2.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies a variable to control whether mouse and touch events are to be treated together or separately. <see cref="SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH" href="https://wiki.libsdl.org/SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH"/>
/// </summary>
/// <remarks>
/// The value of this hint is used at runtime, so it can be changed at any time. By default mouse events will be handled as touch events and touch will raise fake mouse events.
/// </remarks>
public sealed class AndroidSeparateMouseAndTouch : BinaryHint
{
    internal AndroidSeparateMouseAndTouch() : base(SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH) { }
}
