using SDL2.Bindings;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies whether relative motion is affected by renderer scaling. <see cref="SDL_HINT_MOUSE_RELATIVE_SCALING" href="https://wiki.libsdl.org/SDL_HINT_MOUSE_RELATIVE_SCALING"/>
/// </summary>
/// <remarks>
/// By default relative mouse deltas are affected by DPI and renderer scaling
/// </remarks>
public class MouseRelativeScaling : BinaryHint
{
    internal MouseRelativeScaling() : base(SDL.SDL_HINT_MOUSE_RELATIVE_SCALING) { }
}