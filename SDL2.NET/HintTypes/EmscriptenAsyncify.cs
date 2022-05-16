using static SDL2.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies if SDL should give back control to the browser automatically when running with asyncify. <see cref="SDL_HINT_EMSCRIPTEN_ASYNCIFY" href="https://wiki.libsdl.org/SDL_HINT_EMSCRIPTEN_ASYNCIFY"/>
/// </summary>
/// <remarks>
/// This hint only applies to the Emscripten platform.
/// </remarks>
public sealed class EmscriptenAsyncify : BinaryHint
{
    internal EmscriptenAsyncify() : base(SDL_HINT_EMSCRIPTEN_ASYNCIFY) { }
}