using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Red, Green, Blue and Alpha (transparency)
/// </summary>
public struct RGBAColor
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    public byte Alpha { get; }

    public RGBAColor(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public RGBAColor(RGBColor color, byte alpha) : this(color.Red, color.Green, color.Blue, alpha) { }

    public static implicit operator RGBAColor(SDL_Color color)
        => new(color.r, color.g, color.b, color.a);

    public void ToSDL(ref SDL_Color color)
        => color = new()
        {
            a = Alpha,
            r = Red,
            g = Green,
            b = Blue
        };
}

/// <summary>
/// Red, Green and Blue
/// </summary>
public struct RGBColor
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }

    public RGBColor(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public static explicit operator RGBColor(SDL_Color color) //This involves a loss of info
        => new(color.r, color.g, color.b);

    public static explicit operator RGBColor(RGBAColor color)
        => new(color.Red, color.Green, color.Blue);

    public static explicit operator RGBAColor(RGBColor color) //Alpha value might unexpectedly skew colors if the programmer is not careful
        => new(color.Red, color.Green, color.Blue, 0);

    public SDL_Color ToSDL()
        => new()
        {
            r = Red,
            g = Green,
            b = Blue
        };
}
