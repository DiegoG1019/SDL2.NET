using SDL2.NET.SDLFont.Exceptions;
using static SDL2.Bindings.SDL_ttf;

namespace SDL2.NET.SDLFont;
public static class Font
{
    static Font()
    {
        var v = TTF_LinkedVersion();
        SDLTTFVersion = new(v.major, v.minor, v.patch);
    }

    /// <summary>
    /// The version of SDL_image. <see cref="TTF_LinkedVersion"/>
    /// </summary>
    public static Version SDLTTFVersion { get; }

    public static void SetDirection(int direction)
    {
        SDLFontException.ThrowIfLessThan(TTF_SetDirection(direction), 0);
    }

    public static void SetScript(int script)
    {
        SDLFontException.ThrowIfLessThan(TTF_SetScript(script), 0);
    }
}
