using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL_ttf;

namespace SDL2.NET.Font;

public enum EncodingType
{
    Latin1 = 0,
    Unicode = 1,
    UTF8 = 2
}

public enum UnicodeType
{
    Native = UNICODE_BOM_NATIVE,
    Swapped = UNICODE_BOM_SWAPPED
}

[Flags]
public enum TTFStyle
{
    Normal = TTF_STYLE_NORMAL,
    Bold = TTF_STYLE_BOLD,
    Italic = TTF_STYLE_ITALIC,
    Underline = TTF_STYLE_UNDERLINE,
    Strikethrough = TTF_STYLE_STRIKETHROUGH,
}

public enum TTFHinting
{
    Normal = TTF_HINTING_NORMAL,
    Light = TTF_HINTING_LIGHT,
    Mono = TTF_HINTING_MONO,
    None = TTF_HINTING_NONE,
    LightSubpixel = TTF_HINTING_LIGHT_SUBPIXEL,
}

