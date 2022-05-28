using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// A set of extensions for <see cref="Scancode"/> and/or <see cref="Keycode"/> related things
/// </summary>
public static class ScancodeAndKeyExtensions
{
    /// <summary>
    /// Obtains the matching <see cref="Keycode"/> from the specified <paramref name="code"/>
    /// </summary>
    /// <param name="code">The <see cref="Scancode"/> to match</param>
    /// <returns>The matched <see cref="Keycode"/></returns>
    public static Keycode ToKeycode(this Scancode code) 
        => (Keycode)SDL_GetKeyFromScancode((SDL_Scancode)code);

    /// <summary>
    /// Obtains the matching <see cref="Scancode"/> from the specified <paramref name="code"/>
    /// </summary>
    /// <param name="code">The <see cref="Keycode"/> to match</param>
    /// <returns>The matched <see cref="Scancode"/></returns>
    public static Scancode ToScancode(this Keycode code)
        => (Scancode)SDL_GetScancodeFromKey((SDL_Keycode)code);

    /// <summary>
    /// Gets the name of the specified <see cref="Scancode"/>
    /// </summary>
    /// <remarks>The string returned by this method is NOT a .NET constant or a cached object, but instead marshalled from SDL's native library and instanced (and thus is a heap allocation) every time it's requested.</remarks>
    public static string GetName(this Scancode code)
        => SDL_GetScancodeName((SDL_Scancode)code);

    /// <summary>
    /// Gets the name of the key
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    /// <remarks>The string returned by this method is NOT a .NET constant or a cached object, but instead marshalled from SDL's native library and instanced (and thus is a heap allocation) every time it's requested.</remarks>
    public static string GetName(this Keycode code)
        => SDL_GetKeyName((SDL_Keycode)code);
}
