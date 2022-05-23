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
    private const int ARGBAlphaShift = 24;
    private const int ARGBRedShift = 16;
    private const int ARGBGreenShift = 8;
    private const int ARGBBlueShift = 0;

    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    public byte Alpha { get; }
    public double AlphaPercentage => Alpha / 255f;

    /// <summary>
    /// Creates a new RGBA color
    /// </summary>
    /// <param name="red">The red value of the color</param>
    /// <param name="green">The green value of the color</param>
    /// <param name="blue">The blue value of the color</param>
    /// <param name="alpha">The alpha value of the color, from 0 (transparent) to 255 (opaque)</param>
    public RGBAColor(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    /// <summary>
    /// Creates a new RGBA color
    /// </summary>
    /// <param name="red">The red value of the color</param>
    /// <param name="green">The green value of the color</param>
    /// <param name="blue">The blue value of the color</param>
    /// <param name="alpha">The alpha value of the color, from 0 (transparent) to 1 (opaque)</param>
    public RGBAColor(byte red, byte green, byte blue, double alpha)
        : this(red, green, blue, (byte)(255 * Math.Clamp(alpha, 0, 1))) { }

    /// <summary>
    /// Whether this color is pure black
    /// </summary>
    public bool IsBlack
        => Red == 0 && Green == 0 && Blue == 0;

    /// <summary>
    /// Whether this color is pure white
    /// </summary>
    public bool IsWhite
        => Red == 255 && Green == 255 && Blue == 255;

    /// <summary>
    /// Whether this color is a shade of gray
    /// </summary>
    /// <remarks>Whether or not all color values are the same</remarks>
    public bool IsGray
        => Red == Green && Green == Blue;

    /// <summary>
    /// Whether this color is completely transparent
    /// </summary>
    public bool IsTransparent
        => Alpha == 0;

    /// <summary>
    /// Whether this color is completely opaque
    /// </summary>
    public bool IsOpaque
        => Alpha == 255;

    /// <summary>
    /// Converts the given <see cref="RGBColor"/> to an uint value specified by <paramref name="format"/>
    /// </summary>
    /// <returns></returns>
    public uint ToUInt32(PixelFormatData format) 
        => IsBlack ? 0 : SDL_MapRGBA(format._handle, Red, Green, Blue, Alpha);

    /// <summary>
    /// Converts the given <see cref="RGBColor"/> to an uint value
    /// </summary>
    /// <returns></returns>
    public uint ToUInt32()
        => unchecked((uint)(Red << ARGBRedShift |
                            Green << ARGBGreenShift |
                            Blue << ARGBBlueShift |
                            Alpha << ARGBAlphaShift)) & 0xffffffff;

    /// <summary>
    /// Converts the given <see cref="uint"/> to a color value
    /// </summary>
    public static RGBAColor FromUInt32(uint color)
    {
        color &= 0xffffffff;
        return new RGBAColor(
            (byte)((color & 0x00FF00000) >> ARGBRedShift),
            (byte)((color & 0x0000FF00) >> ARGBGreenShift),
            (byte)((color & 0x000000FF) >> ARGBBlueShift),
            (byte)((color & 0xFF000000) >> ARGBAlphaShift)
            );
    }

    /// <summary>
    /// Converts the given <see cref="uint"/> to an RGBAColor value specified by <paramref name="format"/>
    /// </summary>
    /// <returns></returns>
    public static RGBAColor FromUInt32(uint color, PixelFormatData format)
    {
        if (color == 0)
            return default;
        SDL_GetRGBA(color, format._handle, out var r, out var g, out var b, out var a);
        return new(r, g, b, a);
    }

    /// <summary>
    /// Converts the given <see cref="uint"/> to an RGBAColor value
    /// </summary>
    /// <param name="color"></param>
    /// <param name="alpha"></param>
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
    private const int RGBRedShift = 16;
    private const int RGBGreenShift = 8;
    private const int RGBBlueShift = 0;

    /// <summary>
    /// The Red portion of the color
    /// </summary>
    public byte Red { get; }

    /// <summary>
    /// The Green portion of the color
    /// </summary>
    public byte Green { get; }

    /// <summary>
    /// The Blue portion of the color
    /// </summary>
    public byte Blue { get; }

    /// <summary>
    /// Instances a new RGB color
    /// </summary>
    /// <param name="red">The Red portion of the color</param>
    /// <param name="green">The Green portion of the color</param>
    /// <param name="blue">The Blue portion of the color</param>
    public RGBColor(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    /// <summary>
    /// Converts the given <see cref="RGBColor"/> to an uint value specified by the <paramref name="format"/>
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public uint ToUInt32(PixelFormatData format) => SDL_MapRGB(format._handle, Red, Green, Blue);

    ///// <summary>
    ///// Converts the given <see cref="RGBColor"/> to an uint value
    ///// </summary>
    ///// <returns></returns>
    //public uint ToUInt32() 
    //    => unchecked((uint)(Red << RGBRedShift |
    //                     Green << RGBGreenShift |
    //                     Blue << RGBBlueShift)) & 0xffffffff;

    /// <summary>
    /// Converts the given <see cref="RGBColor"/> to an uint value
    /// </summary>
    /// <returns></returns>
    public uint ToUInt32()
        => unchecked(((uint)Red | (((uint)Green) << 8) | (((uint)Blue) << 16)) & 0x00FFFFFF);

    /// <summary>
    /// Converts the given <see cref="uint"/> to a color value
    /// </summary>
    public static RGBColor FromUInt32(uint color)
    {
        color &= 0xffffffff;
        return new RGBColor(
            (byte)((color & 0x00FF00000) >> RGBRedShift),
            (byte)((color & 0x0000FF00) >> RGBGreenShift),
            (byte)((color & 0x000000FF) >> RGBBlueShift)
            );
    }

    /// <summary>
    /// Converts the given <see cref="uint"/> to a color value specified by the <paramref name="format"/>
    /// </summary>
    public static RGBColor FromUInt32(uint color, PixelFormatData format)
    {
        SDL_GetRGB(color, format._handle, out var r, out var g, out var b);
        return new(r, g, b);
    }

    /// <summary>
    /// Whether this color is pure black
    /// </summary>
    public bool IsBlack
        => Red == 0 && Green == 0 && Blue == 0;

    /// <summary>
    /// Whether this color is pure white
    /// </summary>
    public bool IsWhite
        => Red == 255 && Green == 255 && Blue == 255;

    /// <summary>
    /// Whether this color is a shade of gray
    /// </summary>
    /// <remarks>Whether or not all color values are the same</remarks>
    public bool IsGray
        => Red == Green && Green == Blue;

    public static explicit operator RGBColor(SDL_Color color) //This involves a loss of info
        => new(color.r, color.g, color.b);

    public static explicit operator RGBColor(RGBAColor color)
        => new(color.Red, color.Green, color.Blue);

    public static implicit operator RGBAColor(RGBColor color)
        => new(color.Red, color.Green, color.Blue, 255);

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
