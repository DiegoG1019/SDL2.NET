using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL_ttf;

namespace SDL2.NET.Font;
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
}
