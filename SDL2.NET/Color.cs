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
public struct RGBAColor : IEquatable<RGBAColor>
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

    private bool IsBlack
        => Red == 0 && Green == 0 && Blue == 0 && Alpha == 0;

    public uint ToUInt32(PixelFormatData format) 
        => IsBlack ? 0 : SDL_MapRGBA(format._handle, Red, Green, Blue, Alpha);

    public static RGBAColor FromUInt32(uint color, PixelFormatData format)
    {
        if (color == 0)
            return default;
        SDL_GetRGBA(color, format._handle, out var r, out var g, out var b, out var a);
        return new(r, g, b, a);
    }

    public RGBAColor(RGBColor color, byte alpha) : this(color.Red, color.Green, color.Blue, alpha) { }

    public static implicit operator RGBAColor(SDL_Color color)
        => new(color.r, color.g, color.b, color.a);

    public SDL_Color ToSDL()
        => new()
        {
            a = Alpha,
            r = Red,
            g = Green,
            b = Blue
        };

    public void ToSDL(ref SDL_Color color)
        => color = ToSDL();

    public bool Equals(RGBAColor other)
        => other == this;

    public override bool Equals(object? obj) 
        => obj is RGBAColor color && Equals(color);

    public static bool operator ==(RGBAColor left, RGBAColor right)
        => left.Red == right.Red && left.Green == right.Green && left.Blue == right.Blue && left.Alpha == right.Alpha;

    public static bool operator !=(RGBAColor left, RGBAColor right) 
        => !(left == right);

    public override int GetHashCode()
        => HashCode.Combine(Red, Green, Blue, Alpha);
}

/// <summary>
/// Red, Green and Blue
/// </summary>
public struct RGBColor : IEquatable<RGBColor>
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

    public uint ToUInt32(PixelFormatData format) => SDL_MapRGB(format._handle, Red, Green, Blue);
    public static RGBColor FromUInt32(uint color, PixelFormatData format)
    {
        SDL_GetRGB(color, format._handle, out var r, out var g, out var b);
        return new(r, g, b);
    }

    public static explicit operator RGBColor(SDL_Color color) //This involves a loss of info
        => new(color.r, color.g, color.b);

    public static explicit operator RGBColor(RGBAColor color)
        => new(color.Red, color.Green, color.Blue);

    public static explicit operator RGBAColor(RGBColor color) //Alpha value might unexpectedly skew colors if the programmer is not careful
        => new(color.Red, color.Green, color.Blue, 0);

    public bool Equals(RGBColor other)
        => other == this;

    public override bool Equals(object? obj)
        => obj is RGBColor color && Equals(color);

    public static bool operator ==(RGBColor left, RGBColor right)
        => left.Red == right.Red && left.Green == right.Green && left.Blue == right.Blue;

    public static bool operator !=(RGBColor left, RGBColor right)
        => !(left == right);

    public override int GetHashCode()
        => HashCode.Combine(Red, Green, Blue);

    public void ToSDL(ref SDL_Color color)
        => color = new()
        {
            r = Red,
            g = Green,
            b = Blue
        };
}
