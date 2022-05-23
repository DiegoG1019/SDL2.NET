using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;

/// <summary>
/// A collection of predefined colors
/// </summary>
public static class Colors
{
    #region RGB 

    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #66CDAA. 
    /// </summary>
    public static RGBColor MediumAquamarine { get; } = new(0x66, 0xCD, 0xAA);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #0000CD. 
    /// </summary>
    public static RGBColor MediumBlue { get; } = new(0x00, 0x00, 0xCD);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #BA55D3. 
    /// </summary>
    public static RGBColor MediumOrchid { get; } = new(0xBA, 0x55, 0xD3);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #9370DB. 
    /// </summary>
    public static RGBColor MediumPurple { get; } = new(0x93, 0x70, 0xDB);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #3CB371. 
    /// </summary>
    public static RGBColor MediumSeaGreen { get; } = new(0x3C, 0xB3, 0x71);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #7B68EE. 
    /// </summary>
    public static RGBColor MediumSlateBlue { get; } = new(0x7B, 0x68, 0xEE);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00FA9A. 
    /// </summary>
    public static RGBColor MediumSpringGreen { get; } = new(0x00, 0xFA, 0x9A);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #48D1CC. 
    /// </summary>
    public static RGBColor MediumTurquoise { get; } = new(0x48, 0xD1, 0xCC);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #C71585. 
    /// </summary>
    public static RGBColor MediumVioletRed { get; } = new(0xC7, 0x15, 0x85);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #191970. 
    /// </summary>
    public static RGBColor MidnightBlue { get; } = new(0x19, 0x19, 0x70);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F5FFFA. 
    /// </summary>
    public static RGBColor MintCream { get; } = new(0xF5, 0xFF, 0xFA);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFE4E1. 
    /// </summary>
    public static RGBColor MistyRose { get; } = new(0xFF, 0xE4, 0xE1);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFE4B5. 
    /// </summary>
    public static RGBColor Moccasin { get; } = new(0xFF, 0xE4, 0xB5);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFDEAD. 
    /// </summary>
    public static RGBColor NavajoWhite { get; } = new(0xFF, 0xDE, 0xAD);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #000080. 
    /// </summary>
    public static RGBColor Navy { get; } = new(0x00, 0x00, 0x80);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FDF5E6. 
    /// </summary>
    public static RGBColor OldLace { get; } = new(0xFD, 0xF5, 0xE6);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #808000. 
    /// </summary>
    public static RGBColor Olive { get; } = new(0x80, 0x80, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #800000. 
    /// </summary>
    public static RGBColor Maroon { get; } = new(0x80, 0x00, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #6B8E23. 
    /// </summary>
    public static RGBColor OliveDrab { get; } = new(0x6B, 0x8E, 0x23);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF00FF. 
    /// </summary>
    public static RGBColor Magenta { get; } = new(0xFF, 0x00, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #32CD32. 
    /// </summary>
    public static RGBColor LimeGreen { get; } = new(0x32, 0xCD, 0x32);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFF0F5. 
    /// </summary>
    public static RGBColor LavenderBlush { get; } = new(0xFF, 0xF0, 0xF5);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #7CFC00. 
    /// </summary>
    public static RGBColor LawnGreen { get; } = new(0x7C, 0xFC, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFFACD. 
    /// </summary>
    public static RGBColor LemonChiffon { get; } = new(0xFF, 0xFA, 0xCD);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #ADD8E6. 
    /// </summary>
    public static RGBColor LightBlue { get; } = new(0xAD, 0xD8, 0xE6);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F08080. 
    /// </summary>
    public static RGBColor LightCoral { get; } = new(0xF0, 0x80, 0x80);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #E0FFFF. 
    /// </summary>
    public static RGBColor LightCyan { get; } = new(0xE0, 0xFF, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FAFAD2. 
    /// </summary>
    public static RGBColor LightGoldenrodYellow { get; } = new(0xFA, 0xFA, 0xD2);

    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #D3D3D3. 
    /// </summary>
    public static RGBColor LightGray { get; } = new(0xD3, 0xD3, 0xD3);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #90EE90. 
    /// </summary>
    public static RGBColor LightGreen { get; } = new(0x90, 0xEE, 0x90);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFB6C1. 
    /// </summary>
    public static RGBColor LightPink { get; } = new(0xFF, 0xB6, 0xC1);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFA07A. 
    /// </summary>
    public static RGBColor LightSalmon { get; } = new(0xFF, 0xA0, 0x7A);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #20B2AA. 
    /// </summary>
    public static RGBColor LightSeaGreen { get; } = new(0x20, 0xB2, 0xAA);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #87CEFA. 
    /// </summary>
    public static RGBColor LightSkyBlue { get; } = new(0x87, 0xCE, 0xFA);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #778899. 
    /// </summary>
    public static RGBColor LightSlateGray { get; } = new(0x77, 0x88, 0x99);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #B0C4DE. 
    /// </summary>
    public static RGBColor LightSteelBlue { get; } = new(0xB0, 0xC4, 0xDE);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFFFE0. 
    /// </summary>
    public static RGBColor LightYellow { get; } = new(0xFF, 0xFF, 0xE0);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00FF00. 
    /// </summary>
    public static RGBColor Lime { get; } = new(0x00, 0xFF, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FAF0E6. 
    /// </summary>
    public static RGBColor Linen { get; } = new(0xFA, 0xF0, 0xE6);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFFF00. 
    /// </summary>
    public static RGBColor Yellow { get; } = new(0xFF, 0xFF, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFA500. 
    /// </summary>
    public static RGBColor Orange { get; } = new(0xFF, 0xA5, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #DA70D6. 
    /// </summary>
    public static RGBColor Orchid { get; } = new(0xDA, 0x70, 0xD6);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #C0C0C0. 
    /// </summary>
    public static RGBColor Silver { get; } = new(0xC0, 0xC0, 0xC0);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #87CEEB. 
    /// </summary>
    public static RGBColor SkyBlue { get; } = new(0x87, 0xCE, 0xEB);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #6A5ACD. 
    /// </summary>
    public static RGBColor SlateBlue { get; } = new(0x6A, 0x5A, 0xCD);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #708090. 
    /// </summary>
    public static RGBColor SlateGray { get; } = new(0x70, 0x80, 0x90);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFFAFA. 
    /// </summary>
    public static RGBColor Snow { get; } = new(0xFF, 0xFA, 0xFA);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00FF7F. 
    /// </summary>
    public static RGBColor SpringGreen { get; } = new(0x00, 0xFF, 0x7F);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #4682B4. 
    /// </summary>
    public static RGBColor SteelBlue { get; } = new(0x46, 0x82, 0xB4);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #D2B48C. 
    /// </summary>
    public static RGBColor Tan { get; } = new(0xD2, 0xB4, 0x8C);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #008080. 
    /// </summary>
    public static RGBColor Teal { get; } = new(0x00, 0x80, 0x80);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #D8BFD8. 
    /// </summary>
    public static RGBColor Thistle { get; } = new(0xD8, 0xBF, 0xD8);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF6347. 
    /// </summary>
    public static RGBColor Tomato { get; } = new(0xFF, 0x63, 0x47);

    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #40E0D0. 
    /// </summary>
    public static RGBColor Turquoise { get; } = new(0x40, 0xE0, 0xD0);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #EE82EE. 
    /// </summary>
    public static RGBColor Violet { get; } = new(0xEE, 0x82, 0xEE);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F5DEB3. 
    /// </summary>
    public static RGBColor Wheat { get; } = new(0xF5, 0xDE, 0xB3);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFFFFF. 
    /// </summary>
    public static RGBColor White { get; } = new(0xFF, 0xFF, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F5F5F5. 
    /// </summary>
    public static RGBColor WhiteSmoke { get; } = new(0xF5, 0xF5, 0xF5);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #A0522D. 
    /// </summary>
    public static RGBColor Sienna { get; } = new(0xA0, 0x52, 0x2D);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF4500. 
    /// </summary>
    public static RGBColor OrangeRed { get; } = new(0xFF, 0x45, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFF5EE. 
    /// </summary>
    public static RGBColor SeaShell { get; } = new(0xFF, 0xF5, 0xEE);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F4A460. 
    /// </summary>
    public static RGBColor SandyBrown { get; } = new(0xF4, 0xA4, 0x60);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #EEE8AA. 
    /// </summary>
    public static RGBColor PaleGoldenrod { get; } = new(0xEE, 0xE8, 0xAA);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #98FB98. 
    /// </summary>
    public static RGBColor PaleGreen { get; } = new(0x98, 0xFB, 0x98);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #AFEEEE. 
    /// </summary>
    public static RGBColor PaleTurquoise { get; } = new(0xAF, 0xEE, 0xEE);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #DB7093. 
    /// </summary>
    public static RGBColor PaleVioletRed { get; } = new(0xDB, 0x70, 0x93);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFEFD5. 
    /// </summary>
    public static RGBColor PapayaWhip { get; } = new(0xFF, 0xEF, 0xD5);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFDAB9. 
    /// </summary>
    public static RGBColor PeachPuff { get; } = new(0xFF, 0xDA, 0xB9);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #CD853F. 
    /// </summary>
    public static RGBColor Peru { get; } = new(0xCD, 0x85, 0x3F);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFC0CB. 
    /// </summary>
    public static RGBColor Pink { get; } = new(0xFF, 0xC0, 0xCB);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #DDA0DD. 
    /// </summary>
    public static RGBColor Plum { get; } = new(0xDD, 0xA0, 0xDD);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #B0E0E6. 
    /// </summary>
    public static RGBColor PowderBlue { get; } = new(0xB0, 0xE0, 0xE6);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #800080. 
    /// </summary>
    public static RGBColor Purple { get; } = new(0x80, 0x00, 0x80);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #663399. 
    /// </summary>
    public static RGBColor RebeccaPurple { get; } = new(0x66, 0x33, 0x99);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF0000. 
    /// </summary>
    public static RGBColor Red { get; } = new(0xFF, 0x00, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #BC8F8F. 
    /// </summary>
    public static RGBColor RosyBrown { get; } = new(0xBC, 0x8F, 0x8F);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #4169E1. 
    /// </summary>
    public static RGBColor RoyalBlue { get; } = new(0x41, 0x69, 0xE1);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #8B4513. 
    /// </summary>
    public static RGBColor SaddleBrown { get; } = new(0x8B, 0x45, 0x13);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FA8072. 
    /// </summary>
    public static RGBColor Salmon { get; } = new(0xFA, 0x80, 0x72);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #2E8B57. 
    /// </summary>
    public static RGBColor SeaGreen { get; } = new(0x2E, 0x8B, 0x57);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F0E68C. 
    /// </summary>
    public static RGBColor Khaki { get; } = new(0xF0, 0xE6, 0x8C);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #E6E6FA. 
    /// </summary>
    public static RGBColor Lavender { get; } = new(0xE6, 0xE6, 0xFA);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00FFFF. 
    /// </summary>
    public static RGBColor Cyan { get; } = new(0x00, 0xFF, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #8B008B. 
    /// </summary>
    public static RGBColor DarkMagenta { get; } = new(0x8B, 0x00, 0x8B);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #BDB76B. 
    /// </summary>
    public static RGBColor DarkKhaki { get; } = new(0xBD, 0xB7, 0x6B);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #006400. 
    /// </summary>
    public static RGBColor DarkGreen { get; } = new(0x00, 0x64, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #A9A9A9. 
    /// </summary>
    public static RGBColor DarkGray { get; } = new(0xA9, 0xA9, 0xA9);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #B8860B. 
    /// </summary>
    public static RGBColor DarkGoldenrod { get; } = new(0xB8, 0x86, 0x0B);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #008B8B. 
    /// </summary>
    public static RGBColor DarkCyan { get; } = new(0x00, 0x8B, 0x8B);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00008B. 
    /// </summary>
    public static RGBColor DarkBlue { get; } = new(0x00, 0x00, 0x8B);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFFFF0. 
    /// </summary>
    public static RGBColor Ivory { get; } = new(0xFF, 0xFF, 0xF0);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #DC143C. 
    /// </summary>
    public static RGBColor Crimson { get; } = new(0xDC, 0x14, 0x3C);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFF8DC. 
    /// </summary>
    public static RGBColor Cornsilk { get; } = new(0xFF, 0xF8, 0xDC);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #6495ED. 
    /// </summary>
    public static RGBColor CornflowerBlue { get; } = new(0x64, 0x95, 0xED);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF7F50. 
    /// </summary>
    public static RGBColor Coral { get; } = new(0xFF, 0x7F, 0x50);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #D2691E. 
    /// </summary>
    public static RGBColor Chocolate { get; } = new(0xD2, 0x69, 0x1E);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #556B2F. 
    /// </summary>
    public static RGBColor DarkOliveGreen { get; } = new(0x55, 0x6B, 0x2F);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #7FFF00. 
    /// </summary>
    public static RGBColor Chartreuse { get; } = new(0x7F, 0xFF, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #DEB887. 
    /// </summary>
    public static RGBColor BurlyWood { get; } = new(0xDE, 0xB8, 0x87);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #A52A2A. 
    /// </summary>
    public static RGBColor Brown { get; } = new(0xA5, 0x2A, 0x2A);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #8A2BE2. 
    /// </summary>
    public static RGBColor BlueViolet { get; } = new(0x8A, 0x2B, 0xE2);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #0000FF. 
    /// </summary>
    public static RGBColor Blue { get; } = new(0x00, 0x00, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFEBCD. 
    /// </summary>
    public static RGBColor BlanchedAlmond { get; } = new(0xFF, 0xEB, 0xCD);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #000000. 
    /// </summary>
    public static RGBColor Black { get; } = new(0x00, 0x00, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFE4C4. 
    /// </summary>
    public static RGBColor Bisque { get; } = new(0xFF, 0xE4, 0xC4);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F5F5DC. 
    /// </summary>
    public static RGBColor Beige { get; } = new(0xF5, 0xF5, 0xDC);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F0FFFF. 
    /// </summary>
    public static RGBColor Azure { get; } = new(0xF0, 0xFF, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #7FFFD4. 
    /// </summary>
    public static RGBColor Aquamarine { get; } = new(0x7F, 0xFF, 0xD4);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00FFFF. 
    /// </summary>
    public static RGBColor Aqua { get; } = new(0x00, 0xFF, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FAEBD7. 
    /// </summary>
    public static RGBColor AntiqueWhite { get; } = new(0xFA, 0xEB, 0xD7);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F0F8FF. 
    /// </summary>
    public static RGBColor AliceBlue { get; } = new(0xF0, 0xF8, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #5F9EA0. 
    /// </summary>
    public static RGBColor CadetBlue { get; } = new(0x5F, 0x9E, 0xA0);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF8C00. 
    /// </summary>
    public static RGBColor DarkOrange { get; } = new(0xFF, 0x8C, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #9ACD32. 
    /// </summary>
    public static RGBColor YellowGreen { get; } = new(0x9A, 0xCD, 0x32);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #8B0000. 
    /// </summary>
    public static RGBColor DarkRed { get; } = new(0x8B, 0x00, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #4B0082. 
    /// </summary>
    public static RGBColor Indigo { get; } = new(0x4B, 0x00, 0x82);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #CD5C5C. 
    /// </summary>
    public static RGBColor IndianRed { get; } = new(0xCD, 0x5C, 0x5C);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #9932CC. 
    /// </summary>
    public static RGBColor DarkOrchid { get; } = new(0x99, 0x32, 0xCC);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F0FFF0. 
    /// </summary>
    public static RGBColor Honeydew { get; } = new(0xF0, 0xFF, 0xF0);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #ADFF2F. 
    /// </summary>
    public static RGBColor GreenYellow { get; } = new(0xAD, 0xFF, 0x2F);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #008000. 
    /// </summary>
    public static RGBColor Green { get; } = new(0x00, 0x80, 0x00);

    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #808080.
    /// </summary>
    public static RGBColor Gray { get; } = new(0x80, 0x80, 0x80);

    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #DAA520. 
    /// </summary>
    public static RGBColor Goldenrod { get; } = new(0xDA, 0xA5, 0x20);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFD700. 
    /// </summary>
    public static RGBColor Gold { get; } = new(0xFF, 0xD7, 0x00);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #F8F8FF. 
    /// </summary>
    public static RGBColor GhostWhite { get; } = new(0xF8, 0xF8, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #DCDCDC. 
    /// </summary>
    public static RGBColor Gainsboro { get; } = new(0xDC, 0xDC, 0xDC);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF00FF. 
    /// </summary>
    public static RGBColor Fuchsia { get; } = new(0xFF, 0x00, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #228B22. 
    /// </summary>
    public static RGBColor ForestGreen { get; } = new(0x22, 0x8B, 0x22);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF69B4. 
    /// </summary>
    public static RGBColor HotPink { get; } = new(0xFF, 0x69, 0xB4);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #B22222. 
    /// </summary>
    public static RGBColor Firebrick { get; } = new(0xB2, 0x22, 0x22);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FFFAF0. 
    /// </summary>
    public static RGBColor FloralWhite { get; } = new(0xFF, 0xFA, 0xF0);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #1E90FF. 
    /// </summary>
    public static RGBColor DodgerBlue { get; } = new(0x1E, 0x90, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #696969. 
    /// </summary>
    public static RGBColor DimGray { get; } = new(0x69, 0x69, 0x69);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00BFFF. 
    /// </summary>
    public static RGBColor DeepSkyBlue { get; } = new(0x00, 0xBF, 0xFF);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #FF1493. 
    /// </summary>
    public static RGBColor DeepPink { get; } = new(0xFF, 0x14, 0x93);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #9400D3. 
    /// </summary>
    public static RGBColor DarkViolet { get; } = new(0x94, 0x00, 0xD3);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #00CED1. 
    /// </summary>
    public static RGBColor DarkTurquoise { get; } = new(0x00, 0xCE, 0xD1);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #2F4F4F. 
    /// </summary>
    public static RGBColor DarkSlateGray { get; } = new(0x2F, 0x4F, 0x4F);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #483D8B. 
    /// </summary>
    public static RGBColor DarkSlateBlue { get; } = new(0x48, 0x3D, 0x8B);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #8FBC8B. 
    /// </summary>
    public static RGBColor DarkSeaGreen { get; } = new(0x8F, 0xBC, 0x8B);
    
    /// <summary>
    /// Gets a <see cref="RGBColor"/> with a value of #E9967A. 
    /// </summary>
    public static RGBColor DarkSalmon { get; } = new(0xE9, 0x96, 0x7A);

    #endregion

    #region RGBA

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #66CDAA and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumAquamarine(byte alpha) => new(0x66, 0xCD, 0xAA, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #66CDAA and the given opacity
    /// </summary>
    public static RGBAColor GetMediumAquamarine(double opacity) => new(0x66, 0xCD, 0xAA, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #0000CD and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumBlue(byte alpha) => new(0x00, 0x00, 0xCD, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #0000CD and the given opacity
    /// </summary>
    public static RGBAColor GetMediumBlue(double opacity) => new(0x00, 0x00, 0xCD, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #BA55D3 and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumOrchid(byte alpha) => new(0xBA, 0x55, 0xD3, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #BA55D3 and the given opacity
    /// </summary>
    public static RGBAColor GetMediumOrchid(double opacity) => new(0xBA, 0x55, 0xD3, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9370DB and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumPurple(byte alpha) => new(0x93, 0x70, 0xDB, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9370DB and the given opacity
    /// </summary>
    public static RGBAColor GetMediumPurple(double opacity) => new(0x93, 0x70, 0xDB, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #3CB371 and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumSeaGreen(byte alpha) => new(0x3C, 0xB3, 0x71, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #3CB371 and the given opacity
    /// </summary>
    public static RGBAColor GetMediumSeaGreen(double opacity) => new(0x3C, 0xB3, 0x71, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7B68EE and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumSlateBlue(byte alpha) => new(0x7B, 0x68, 0xEE, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7B68EE and the given opacity
    /// </summary>
    public static RGBAColor GetMediumSlateBlue(double opacity) => new(0x7B, 0x68, 0xEE, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FA9A and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumSpringGreen(byte alpha) => new(0x00, 0xFA, 0x9A, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FA9A and the given opacity
    /// </summary>
    public static RGBAColor GetMediumSpringGreen(double opacity) => new(0x00, 0xFA, 0x9A, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #48D1CC and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumTurquoise(byte alpha) => new(0x48, 0xD1, 0xCC, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #48D1CC and the given opacity
    /// </summary>
    public static RGBAColor GetMediumTurquoise(double opacity) => new(0x48, 0xD1, 0xCC, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #C71585 and the given alpha value
    /// </summary>
    public static RGBAColor GetMediumVioletRed(byte alpha) => new(0xC7, 0x15, 0x85, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #C71585 and the given opacity
    /// </summary>
    public static RGBAColor GetMediumVioletRed(double opacity) => new(0xC7, 0x15, 0x85, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #191970 and the given alpha value
    /// </summary>
    public static RGBAColor GetMidnightBlue(byte alpha) => new(0x19, 0x19, 0x70, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #191970 and the given opacity
    /// </summary>
    public static RGBAColor GetMidnightBlue(double opacity) => new(0x19, 0x19, 0x70, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5FFFA and the given alpha value
    /// </summary>
    public static RGBAColor GetMintCream(byte alpha) => new(0xF5, 0xFF, 0xFA, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5FFFA and the given opacity
    /// </summary>
    public static RGBAColor GetMintCream(double opacity) => new(0xF5, 0xFF, 0xFA, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFE4E1 and the given alpha value
    /// </summary>
    public static RGBAColor GetMistyRose(byte alpha) => new(0xFF, 0xE4, 0xE1, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFE4E1 and the given opacity
    /// </summary>
    public static RGBAColor GetMistyRose(double opacity) => new(0xFF, 0xE4, 0xE1, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFE4B5 and the given alpha value
    /// </summary>
    public static RGBAColor GetMoccasin(byte alpha) => new(0xFF, 0xE4, 0xB5, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFE4B5 and the given opacity
    /// </summary>
    public static RGBAColor GetMoccasin(double opacity) => new(0xFF, 0xE4, 0xB5, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFDEAD and the given alpha value
    /// </summary>
    public static RGBAColor GetNavajoWhite(byte alpha) => new(0xFF, 0xDE, 0xAD, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFDEAD and the given opacity
    /// </summary>
    public static RGBAColor GetNavajoWhite(double opacity) => new(0xFF, 0xDE, 0xAD, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #000080 and the given alpha value
    /// </summary>
    public static RGBAColor GetNavy(byte alpha) => new(0x00, 0x00, 0x80, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #000080 and the given opacity
    /// </summary>
    public static RGBAColor GetNavy(double opacity) => new(0x00, 0x00, 0x80, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FDF5E6 and the given alpha value
    /// </summary>
    public static RGBAColor GetOldLace(byte alpha) => new(0xFD, 0xF5, 0xE6, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FDF5E6 and the given opacity
    /// </summary>
    public static RGBAColor GetOldLace(double opacity) => new(0xFD, 0xF5, 0xE6, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #808000 and the given alpha value
    /// </summary>
    public static RGBAColor GetOlive(byte alpha) => new(0x80, 0x80, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #808000 and the given opacity
    /// </summary>
    public static RGBAColor GetOlive(double opacity) => new(0x80, 0x80, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #800000 and the given alpha value
    /// </summary>
    public static RGBAColor GetMaroon(byte alpha) => new(0x80, 0x00, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #800000 and the given opacity
    /// </summary>
    public static RGBAColor GetMaroon(double opacity) => new(0x80, 0x00, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #6B8E23 and the given alpha value
    /// </summary>
    public static RGBAColor GetOliveDrab(byte alpha) => new(0x6B, 0x8E, 0x23, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #6B8E23 and the given opacity
    /// </summary>
    public static RGBAColor GetOliveDrab(double opacity) => new(0x6B, 0x8E, 0x23, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF00FF and the given alpha value
    /// </summary>
    public static RGBAColor GetMagenta(byte alpha) => new(0xFF, 0x00, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF00FF and the given opacity
    /// </summary>
    public static RGBAColor GetMagenta(double opacity) => new(0xFF, 0x00, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #32CD32 and the given alpha value
    /// </summary>
    public static RGBAColor GetLimeGreen(byte alpha) => new(0x32, 0xCD, 0x32, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #32CD32 and the given opacity
    /// </summary>
    public static RGBAColor GetLimeGreen(double opacity) => new(0x32, 0xCD, 0x32, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFF0F5 and the given alpha value
    /// </summary>
    public static RGBAColor GetLavenderBlush(byte alpha) => new(0xFF, 0xF0, 0xF5, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFF0F5 and the given opacity
    /// </summary>
    public static RGBAColor GetLavenderBlush(double opacity) => new(0xFF, 0xF0, 0xF5, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7CFC00 and the given alpha value
    /// </summary>
    public static RGBAColor GetLawnGreen(byte alpha) => new(0x7C, 0xFC, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7CFC00 and the given opacity
    /// </summary>
    public static RGBAColor GetLawnGreen(double opacity) => new(0x7C, 0xFC, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFACD and the given alpha value
    /// </summary>
    public static RGBAColor GetLemonChiffon(byte alpha) => new(0xFF, 0xFA, 0xCD, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFACD and the given opacity
    /// </summary>
    public static RGBAColor GetLemonChiffon(double opacity) => new(0xFF, 0xFA, 0xCD, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #ADD8E6 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightBlue(byte alpha) => new(0xAD, 0xD8, 0xE6, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #ADD8E6 and the given opacity
    /// </summary>
    public static RGBAColor GetLightBlue(double opacity) => new(0xAD, 0xD8, 0xE6, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F08080 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightCoral(byte alpha) => new(0xF0, 0x80, 0x80, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F08080 and the given opacity
    /// </summary>
    public static RGBAColor GetLightCoral(double opacity) => new(0xF0, 0x80, 0x80, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #E0FFFF and the given alpha value
    /// </summary>
    public static RGBAColor GetLightCyan(byte alpha) => new(0xE0, 0xFF, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #E0FFFF and the given opacity
    /// </summary>
    public static RGBAColor GetLightCyan(double opacity) => new(0xE0, 0xFF, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FAFAD2 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightGoldenrodYellow(byte alpha) => new(0xFA, 0xFA, 0xD2, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FAFAD2 and the given opacity
    /// </summary>
    public static RGBAColor GetLightGoldenrodYellow(double opacity) => new(0xFA, 0xFA, 0xD2, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D3D3D3 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightGray(byte alpha) => new(0xD3, 0xD3, 0xD3, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D3D3D3 and the given opacity
    /// </summary>
    public static RGBAColor GetLightGray(double opacity) => new(0xD3, 0xD3, 0xD3, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #90EE90 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightGreen(byte alpha) => new(0x90, 0xEE, 0x90, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #90EE90 and the given opacity
    /// </summary>
    public static RGBAColor GetLightGreen(double opacity) => new(0x90, 0xEE, 0x90, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFB6C1 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightPink(byte alpha) => new(0xFF, 0xB6, 0xC1, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFB6C1 and the given opacity
    /// </summary>
    public static RGBAColor GetLightPink(double opacity) => new(0xFF, 0xB6, 0xC1, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFA07A and the given alpha value
    /// </summary>
    public static RGBAColor GetLightSalmon(byte alpha) => new(0xFF, 0xA0, 0x7A, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFA07A and the given opacity
    /// </summary>
    public static RGBAColor GetLightSalmon(double opacity) => new(0xFF, 0xA0, 0x7A, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #20B2AA and the given alpha value
    /// </summary>
    public static RGBAColor GetLightSeaGreen(byte alpha) => new(0x20, 0xB2, 0xAA, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #20B2AA and the given opacity
    /// </summary>
    public static RGBAColor GetLightSeaGreen(double opacity) => new(0x20, 0xB2, 0xAA, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #87CEFA and the given alpha value
    /// </summary>
    public static RGBAColor GetLightSkyBlue(byte alpha) => new(0x87, 0xCE, 0xFA, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #87CEFA and the given opacity
    /// </summary>
    public static RGBAColor GetLightSkyBlue(double opacity) => new(0x87, 0xCE, 0xFA, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #778899 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightSlateGray(byte alpha) => new(0x77, 0x88, 0x99, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #778899 and the given opacity
    /// </summary>
    public static RGBAColor GetLightSlateGray(double opacity) => new(0x77, 0x88, 0x99, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B0C4DE and the given alpha value
    /// </summary>
    public static RGBAColor GetLightSteelBlue(byte alpha) => new(0xB0, 0xC4, 0xDE, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B0C4DE and the given opacity
    /// </summary>
    public static RGBAColor GetLightSteelBlue(double opacity) => new(0xB0, 0xC4, 0xDE, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFFE0 and the given alpha value
    /// </summary>
    public static RGBAColor GetLightYellow(byte alpha) => new(0xFF, 0xFF, 0xE0, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFFE0 and the given opacity
    /// </summary>
    public static RGBAColor GetLightYellow(double opacity) => new(0xFF, 0xFF, 0xE0, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FF00 and the given alpha value
    /// </summary>
    public static RGBAColor GetLime(byte alpha) => new(0x00, 0xFF, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FF00 and the given opacity
    /// </summary>
    public static RGBAColor GetLime(double opacity) => new(0x00, 0xFF, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FAF0E6 and the given alpha value
    /// </summary>
    public static RGBAColor GetLinen(byte alpha) => new(0xFA, 0xF0, 0xE6, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FAF0E6 and the given opacity
    /// </summary>
    public static RGBAColor GetLinen(double opacity) => new(0xFA, 0xF0, 0xE6, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFF00 and the given alpha value
    /// </summary>
    public static RGBAColor GetYellow(byte alpha) => new(0xFF, 0xFF, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFF00 and the given opacity
    /// </summary>
    public static RGBAColor GetYellow(double opacity) => new(0xFF, 0xFF, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFA500 and the given alpha value
    /// </summary>
    public static RGBAColor GetOrange(byte alpha) => new(0xFF, 0xA5, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFA500 and the given opacity
    /// </summary>
    public static RGBAColor GetOrange(double opacity) => new(0xFF, 0xA5, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DA70D6 and the given alpha value
    /// </summary>
    public static RGBAColor GetOrchid(byte alpha) => new(0xDA, 0x70, 0xD6, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DA70D6 and the given opacity
    /// </summary>
    public static RGBAColor GetOrchid(double opacity) => new(0xDA, 0x70, 0xD6, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #C0C0C0 and the given alpha value
    /// </summary>
    public static RGBAColor GetSilver(byte alpha) => new(0xC0, 0xC0, 0xC0, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #C0C0C0 and the given opacity
    /// </summary>
    public static RGBAColor GetSilver(double opacity) => new(0xC0, 0xC0, 0xC0, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #87CEEB and the given alpha value
    /// </summary>
    public static RGBAColor GetSkyBlue(byte alpha) => new(0x87, 0xCE, 0xEB, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #87CEEB and the given opacity
    /// </summary>
    public static RGBAColor GetSkyBlue(double opacity) => new(0x87, 0xCE, 0xEB, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #6A5ACD and the given alpha value
    /// </summary>
    public static RGBAColor GetSlateBlue(byte alpha) => new(0x6A, 0x5A, 0xCD, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #6A5ACD and the given opacity
    /// </summary>
    public static RGBAColor GetSlateBlue(double opacity) => new(0x6A, 0x5A, 0xCD, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #708090 and the given alpha value
    /// </summary>
    public static RGBAColor GetSlateGray(byte alpha) => new(0x70, 0x80, 0x90, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #708090 and the given opacity
    /// </summary>
    public static RGBAColor GetSlateGray(double opacity) => new(0x70, 0x80, 0x90, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFAFA and the given alpha value
    /// </summary>
    public static RGBAColor GetSnow(byte alpha) => new(0xFF, 0xFA, 0xFA, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFAFA and the given opacity
    /// </summary>
    public static RGBAColor GetSnow(double opacity) => new(0xFF, 0xFA, 0xFA, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FF7F and the given alpha value
    /// </summary>
    public static RGBAColor GetSpringGreen(byte alpha) => new(0x00, 0xFF, 0x7F, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FF7F and the given opacity
    /// </summary>
    public static RGBAColor GetSpringGreen(double opacity) => new(0x00, 0xFF, 0x7F, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #4682B4 and the given alpha value
    /// </summary>
    public static RGBAColor GetSteelBlue(byte alpha) => new(0x46, 0x82, 0xB4, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #4682B4 and the given opacity
    /// </summary>
    public static RGBAColor GetSteelBlue(double opacity) => new(0x46, 0x82, 0xB4, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D2B48C and the given alpha value
    /// </summary>
    public static RGBAColor GetTan(byte alpha) => new(0xD2, 0xB4, 0x8C, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D2B48C and the given opacity
    /// </summary>
    public static RGBAColor GetTan(double opacity) => new(0xD2, 0xB4, 0x8C, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #008080 and the given alpha value
    /// </summary>
    public static RGBAColor GetTeal(byte alpha) => new(0x00, 0x80, 0x80, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #008080 and the given opacity
    /// </summary>
    public static RGBAColor GetTeal(double opacity) => new(0x00, 0x80, 0x80, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D8BFD8 and the given alpha value
    /// </summary>
    public static RGBAColor GetThistle(byte alpha) => new(0xD8, 0xBF, 0xD8, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D8BFD8 and the given opacity
    /// </summary>
    public static RGBAColor GetThistle(double opacity) => new(0xD8, 0xBF, 0xD8, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF6347 and the given alpha value
    /// </summary>
    public static RGBAColor GetTomato(byte alpha) => new(0xFF, 0x63, 0x47, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF6347 and the given opacity
    /// </summary>
    public static RGBAColor GetTomato(double opacity) => new(0xFF, 0x63, 0x47, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #40E0D0 and the given alpha value
    /// </summary>
    public static RGBAColor GetTurquoise(byte alpha) => new(0x40, 0xE0, 0xD0, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #40E0D0 and the given opacity
    /// </summary>
    public static RGBAColor GetTurquoise(double opacity) => new(0x40, 0xE0, 0xD0, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #EE82EE and the given alpha value
    /// </summary>
    public static RGBAColor GetViolet(byte alpha) => new(0xEE, 0x82, 0xEE, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #EE82EE and the given opacity
    /// </summary>
    public static RGBAColor GetViolet(double opacity) => new(0xEE, 0x82, 0xEE, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5DEB3 and the given alpha value
    /// </summary>
    public static RGBAColor GetWheat(byte alpha) => new(0xF5, 0xDE, 0xB3, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5DEB3 and the given opacity
    /// </summary>
    public static RGBAColor GetWheat(double opacity) => new(0xF5, 0xDE, 0xB3, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFFFF and the given alpha value
    /// </summary>
    public static RGBAColor GetWhite(byte alpha) => new(0xFF, 0xFF, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFFFF and the given opacity
    /// </summary>
    public static RGBAColor GetWhite(double opacity) => new(0xFF, 0xFF, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5F5F5 and the given alpha value
    /// </summary>
    public static RGBAColor GetWhiteSmoke(byte alpha) => new(0xF5, 0xF5, 0xF5, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5F5F5 and the given opacity
    /// </summary>
    public static RGBAColor GetWhiteSmoke(double opacity) => new(0xF5, 0xF5, 0xF5, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #A0522D and the given alpha value
    /// </summary>
    public static RGBAColor GetSienna(byte alpha) => new(0xA0, 0x52, 0x2D, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #A0522D and the given opacity
    /// </summary>
    public static RGBAColor GetSienna(double opacity) => new(0xA0, 0x52, 0x2D, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF4500 and the given alpha value
    /// </summary>
    public static RGBAColor GetOrangeRed(byte alpha) => new(0xFF, 0x45, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF4500 and the given opacity
    /// </summary>
    public static RGBAColor GetOrangeRed(double opacity) => new(0xFF, 0x45, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFF5EE and the given alpha value
    /// </summary>
    public static RGBAColor GetSeaShell(byte alpha) => new(0xFF, 0xF5, 0xEE, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFF5EE and the given opacity
    /// </summary>
    public static RGBAColor GetSeaShell(double opacity) => new(0xFF, 0xF5, 0xEE, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F4A460 and the given alpha value
    /// </summary>
    public static RGBAColor GetSandyBrown(byte alpha) => new(0xF4, 0xA4, 0x60, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F4A460 and the given opacity
    /// </summary>
    public static RGBAColor GetSandyBrown(double opacity) => new(0xF4, 0xA4, 0x60, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #EEE8AA and the given alpha value
    /// </summary>
    public static RGBAColor GetPaleGoldenrod(byte alpha) => new(0xEE, 0xE8, 0xAA, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #EEE8AA and the given opacity
    /// </summary>
    public static RGBAColor GetPaleGoldenrod(double opacity) => new(0xEE, 0xE8, 0xAA, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #98FB98 and the given alpha value
    /// </summary>
    public static RGBAColor GetPaleGreen(byte alpha) => new(0x98, 0xFB, 0x98, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #98FB98 and the given opacity
    /// </summary>
    public static RGBAColor GetPaleGreen(double opacity) => new(0x98, 0xFB, 0x98, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #AFEEEE and the given alpha value
    /// </summary>
    public static RGBAColor GetPaleTurquoise(byte alpha) => new(0xAF, 0xEE, 0xEE, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #AFEEEE and the given opacity
    /// </summary>
    public static RGBAColor GetPaleTurquoise(double opacity) => new(0xAF, 0xEE, 0xEE, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DB7093 and the given alpha value
    /// </summary>
    public static RGBAColor GetPaleVioletRed(byte alpha) => new(0xDB, 0x70, 0x93, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DB7093 and the given opacity
    /// </summary>
    public static RGBAColor GetPaleVioletRed(double opacity) => new(0xDB, 0x70, 0x93, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFEFD5 and the given alpha value
    /// </summary>
    public static RGBAColor GetPapayaWhip(byte alpha) => new(0xFF, 0xEF, 0xD5, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFEFD5 and the given opacity
    /// </summary>
    public static RGBAColor GetPapayaWhip(double opacity) => new(0xFF, 0xEF, 0xD5, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFDAB9 and the given alpha value
    /// </summary>
    public static RGBAColor GetPeachPuff(byte alpha) => new(0xFF, 0xDA, 0xB9, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFDAB9 and the given opacity
    /// </summary>
    public static RGBAColor GetPeachPuff(double opacity) => new(0xFF, 0xDA, 0xB9, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #CD853F and the given alpha value
    /// </summary>
    public static RGBAColor GetPeru(byte alpha) => new(0xCD, 0x85, 0x3F, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #CD853F and the given opacity
    /// </summary>
    public static RGBAColor GetPeru(double opacity) => new(0xCD, 0x85, 0x3F, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFC0CB and the given alpha value
    /// </summary>
    public static RGBAColor GetPink(byte alpha) => new(0xFF, 0xC0, 0xCB, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFC0CB and the given opacity
    /// </summary>
    public static RGBAColor GetPink(double opacity) => new(0xFF, 0xC0, 0xCB, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DDA0DD and the given alpha value
    /// </summary>
    public static RGBAColor GetPlum(byte alpha) => new(0xDD, 0xA0, 0xDD, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DDA0DD and the given opacity
    /// </summary>
    public static RGBAColor GetPlum(double opacity) => new(0xDD, 0xA0, 0xDD, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B0E0E6 and the given alpha value
    /// </summary>
    public static RGBAColor GetPowderBlue(byte alpha) => new(0xB0, 0xE0, 0xE6, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B0E0E6 and the given opacity
    /// </summary>
    public static RGBAColor GetPowderBlue(double opacity) => new(0xB0, 0xE0, 0xE6, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #800080 and the given alpha value
    /// </summary>
    public static RGBAColor GetPurple(byte alpha) => new(0x80, 0x00, 0x80, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #800080 and the given opacity
    /// </summary>
    public static RGBAColor GetPurple(double opacity) => new(0x80, 0x00, 0x80, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #663399 and the given alpha value
    /// </summary>
    public static RGBAColor GetRebeccaPurple(byte alpha) => new(0x66, 0x33, 0x99, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #663399 and the given opacity
    /// </summary>
    public static RGBAColor GetRebeccaPurple(double opacity) => new(0x66, 0x33, 0x99, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF0000 and the given alpha value
    /// </summary>
    public static RGBAColor GetRed(byte alpha) => new(0xFF, 0x00, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF0000 and the given opacity
    /// </summary>
    public static RGBAColor GetRed(double opacity) => new(0xFF, 0x00, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #BC8F8F and the given alpha value
    /// </summary>
    public static RGBAColor GetRosyBrown(byte alpha) => new(0xBC, 0x8F, 0x8F, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #BC8F8F and the given opacity
    /// </summary>
    public static RGBAColor GetRosyBrown(double opacity) => new(0xBC, 0x8F, 0x8F, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #4169E1 and the given alpha value
    /// </summary>
    public static RGBAColor GetRoyalBlue(byte alpha) => new(0x41, 0x69, 0xE1, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #4169E1 and the given opacity
    /// </summary>
    public static RGBAColor GetRoyalBlue(double opacity) => new(0x41, 0x69, 0xE1, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8B4513 and the given alpha value
    /// </summary>
    public static RGBAColor GetSaddleBrown(byte alpha) => new(0x8B, 0x45, 0x13, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8B4513 and the given opacity
    /// </summary>
    public static RGBAColor GetSaddleBrown(double opacity) => new(0x8B, 0x45, 0x13, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FA8072 and the given alpha value
    /// </summary>
    public static RGBAColor GetSalmon(byte alpha) => new(0xFA, 0x80, 0x72, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FA8072 and the given opacity
    /// </summary>
    public static RGBAColor GetSalmon(double opacity) => new(0xFA, 0x80, 0x72, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #2E8B57 and the given alpha value
    /// </summary>
    public static RGBAColor GetSeaGreen(byte alpha) => new(0x2E, 0x8B, 0x57, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #2E8B57 and the given opacity
    /// </summary>
    public static RGBAColor GetSeaGreen(double opacity) => new(0x2E, 0x8B, 0x57, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0E68C and the given alpha value
    /// </summary>
    public static RGBAColor GetKhaki(byte alpha) => new(0xF0, 0xE6, 0x8C, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0E68C and the given opacity
    /// </summary>
    public static RGBAColor GetKhaki(double opacity) => new(0xF0, 0xE6, 0x8C, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #E6E6FA and the given alpha value
    /// </summary>
    public static RGBAColor GetLavender(byte alpha) => new(0xE6, 0xE6, 0xFA, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #E6E6FA and the given opacity
    /// </summary>
    public static RGBAColor GetLavender(double opacity) => new(0xE6, 0xE6, 0xFA, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FFFF and the given alpha value
    /// </summary>
    public static RGBAColor GetCyan(byte alpha) => new(0x00, 0xFF, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FFFF and the given opacity
    /// </summary>
    public static RGBAColor GetCyan(double opacity) => new(0x00, 0xFF, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8B008B and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkMagenta(byte alpha) => new(0x8B, 0x00, 0x8B, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8B008B and the given opacity
    /// </summary>
    public static RGBAColor GetDarkMagenta(double opacity) => new(0x8B, 0x00, 0x8B, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #BDB76B and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkKhaki(byte alpha) => new(0xBD, 0xB7, 0x6B, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #BDB76B and the given opacity
    /// </summary>
    public static RGBAColor GetDarkKhaki(double opacity) => new(0xBD, 0xB7, 0x6B, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #006400 and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkGreen(byte alpha) => new(0x00, 0x64, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #006400 and the given opacity
    /// </summary>
    public static RGBAColor GetDarkGreen(double opacity) => new(0x00, 0x64, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #A9A9A9 and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkGray(byte alpha) => new(0xA9, 0xA9, 0xA9, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #A9A9A9 and the given opacity
    /// </summary>
    public static RGBAColor GetDarkGray(double opacity) => new(0xA9, 0xA9, 0xA9, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B8860B and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkGoldenrod(byte alpha) => new(0xB8, 0x86, 0x0B, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B8860B and the given opacity
    /// </summary>
    public static RGBAColor GetDarkGoldenrod(double opacity) => new(0xB8, 0x86, 0x0B, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #008B8B and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkCyan(byte alpha) => new(0x00, 0x8B, 0x8B, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #008B8B and the given opacity
    /// </summary>
    public static RGBAColor GetDarkCyan(double opacity) => new(0x00, 0x8B, 0x8B, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00008B and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkBlue(byte alpha) => new(0x00, 0x00, 0x8B, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00008B and the given opacity
    /// </summary>
    public static RGBAColor GetDarkBlue(double opacity) => new(0x00, 0x00, 0x8B, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFFF0 and the given alpha value
    /// </summary>
    public static RGBAColor GetIvory(byte alpha) => new(0xFF, 0xFF, 0xF0, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFFF0 and the given opacity
    /// </summary>
    public static RGBAColor GetIvory(double opacity) => new(0xFF, 0xFF, 0xF0, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DC143C and the given alpha value
    /// </summary>
    public static RGBAColor GetCrimson(byte alpha) => new(0xDC, 0x14, 0x3C, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DC143C and the given opacity
    /// </summary>
    public static RGBAColor GetCrimson(double opacity) => new(0xDC, 0x14, 0x3C, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFF8DC and the given alpha value
    /// </summary>
    public static RGBAColor GetCornsilk(byte alpha) => new(0xFF, 0xF8, 0xDC, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFF8DC and the given opacity
    /// </summary>
    public static RGBAColor GetCornsilk(double opacity) => new(0xFF, 0xF8, 0xDC, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #6495ED and the given alpha value
    /// </summary>
    public static RGBAColor GetCornflowerBlue(byte alpha) => new(0x64, 0x95, 0xED, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #6495ED and the given opacity
    /// </summary>
    public static RGBAColor GetCornflowerBlue(double opacity) => new(0x64, 0x95, 0xED, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF7F50 and the given alpha value
    /// </summary>
    public static RGBAColor GetCoral(byte alpha) => new(0xFF, 0x7F, 0x50, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF7F50 and the given opacity
    /// </summary>
    public static RGBAColor GetCoral(double opacity) => new(0xFF, 0x7F, 0x50, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D2691E and the given alpha value
    /// </summary>
    public static RGBAColor GetChocolate(byte alpha) => new(0xD2, 0x69, 0x1E, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #D2691E and the given opacity
    /// </summary>
    public static RGBAColor GetChocolate(double opacity) => new(0xD2, 0x69, 0x1E, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #556B2F and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkOliveGreen(byte alpha) => new(0x55, 0x6B, 0x2F, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #556B2F and the given opacity
    /// </summary>
    public static RGBAColor GetDarkOliveGreen(double opacity) => new(0x55, 0x6B, 0x2F, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7FFF00 and the given alpha value
    /// </summary>
    public static RGBAColor GetChartreuse(byte alpha) => new(0x7F, 0xFF, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7FFF00 and the given opacity
    /// </summary>
    public static RGBAColor GetChartreuse(double opacity) => new(0x7F, 0xFF, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DEB887 and the given alpha value
    /// </summary>
    public static RGBAColor GetBurlyWood(byte alpha) => new(0xDE, 0xB8, 0x87, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DEB887 and the given opacity
    /// </summary>
    public static RGBAColor GetBurlyWood(double opacity) => new(0xDE, 0xB8, 0x87, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #A52A2A and the given alpha value
    /// </summary>
    public static RGBAColor GetBrown(byte alpha) => new(0xA5, 0x2A, 0x2A, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #A52A2A and the given opacity
    /// </summary>
    public static RGBAColor GetBrown(double opacity) => new(0xA5, 0x2A, 0x2A, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8A2BE2 and the given alpha value
    /// </summary>
    public static RGBAColor GetBlueViolet(byte alpha) => new(0x8A, 0x2B, 0xE2, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8A2BE2 and the given opacity
    /// </summary>
    public static RGBAColor GetBlueViolet(double opacity) => new(0x8A, 0x2B, 0xE2, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #0000FF and the given alpha value
    /// </summary>
    public static RGBAColor GetBlue(byte alpha) => new(0x00, 0x00, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #0000FF and the given opacity
    /// </summary>
    public static RGBAColor GetBlue(double opacity) => new(0x00, 0x00, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFEBCD and the given alpha value
    /// </summary>
    public static RGBAColor GetBlanchedAlmond(byte alpha) => new(0xFF, 0xEB, 0xCD, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFEBCD and the given opacity
    /// </summary>
    public static RGBAColor GetBlanchedAlmond(double opacity) => new(0xFF, 0xEB, 0xCD, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #000000 and the given alpha value
    /// </summary>
    public static RGBAColor GetBlack(byte alpha) => new(0x00, 0x00, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #000000 and the given opacity
    /// </summary>
    public static RGBAColor GetBlack(double opacity) => new(0x00, 0x00, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFE4C4 and the given alpha value
    /// </summary>
    public static RGBAColor GetBisque(byte alpha) => new(0xFF, 0xE4, 0xC4, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFE4C4 and the given opacity
    /// </summary>
    public static RGBAColor GetBisque(double opacity) => new(0xFF, 0xE4, 0xC4, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5F5DC and the given alpha value
    /// </summary>
    public static RGBAColor GetBeige(byte alpha) => new(0xF5, 0xF5, 0xDC, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F5F5DC and the given opacity
    /// </summary>
    public static RGBAColor GetBeige(double opacity) => new(0xF5, 0xF5, 0xDC, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0FFFF and the given alpha value
    /// </summary>
    public static RGBAColor GetAzure(byte alpha) => new(0xF0, 0xFF, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0FFFF and the given opacity
    /// </summary>
    public static RGBAColor GetAzure(double opacity) => new(0xF0, 0xFF, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7FFFD4 and the given alpha value
    /// </summary>
    public static RGBAColor GetAquamarine(byte alpha) => new(0x7F, 0xFF, 0xD4, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #7FFFD4 and the given opacity
    /// </summary>
    public static RGBAColor GetAquamarine(double opacity) => new(0x7F, 0xFF, 0xD4, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FFFF and the given alpha value
    /// </summary>
    public static RGBAColor GetAqua(byte alpha) => new(0x00, 0xFF, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00FFFF and the given opacity
    /// </summary>
    public static RGBAColor GetAqua(double opacity) => new(0x00, 0xFF, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FAEBD7 and the given alpha value
    /// </summary>
    public static RGBAColor GetAntiqueWhite(byte alpha) => new(0xFA, 0xEB, 0xD7, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FAEBD7 and the given opacity
    /// </summary>
    public static RGBAColor GetAntiqueWhite(double opacity) => new(0xFA, 0xEB, 0xD7, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0F8FF and the given alpha value
    /// </summary>
    public static RGBAColor GetAliceBlue(byte alpha) => new(0xF0, 0xF8, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0F8FF and the given opacity
    /// </summary>
    public static RGBAColor GetAliceBlue(double opacity) => new(0xF0, 0xF8, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #5F9EA0 and the given alpha value
    /// </summary>
    public static RGBAColor GetCadetBlue(byte alpha) => new(0x5F, 0x9E, 0xA0, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #5F9EA0 and the given opacity
    /// </summary>
    public static RGBAColor GetCadetBlue(double opacity) => new(0x5F, 0x9E, 0xA0, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF8C00 and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkOrange(byte alpha) => new(0xFF, 0x8C, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF8C00 and the given opacity
    /// </summary>
    public static RGBAColor GetDarkOrange(double opacity) => new(0xFF, 0x8C, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9ACD32 and the given alpha value
    /// </summary>
    public static RGBAColor GetYellowGreen(byte alpha) => new(0x9A, 0xCD, 0x32, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9ACD32 and the given opacity
    /// </summary>
    public static RGBAColor GetYellowGreen(double opacity) => new(0x9A, 0xCD, 0x32, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8B0000 and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkRed(byte alpha) => new(0x8B, 0x00, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8B0000 and the given opacity
    /// </summary>
    public static RGBAColor GetDarkRed(double opacity) => new(0x8B, 0x00, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #4B0082 and the given alpha value
    /// </summary>
    public static RGBAColor GetIndigo(byte alpha) => new(0x4B, 0x00, 0x82, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #4B0082 and the given opacity
    /// </summary>
    public static RGBAColor GetIndigo(double opacity) => new(0x4B, 0x00, 0x82, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #CD5C5C and the given alpha value
    /// </summary>
    public static RGBAColor GetIndianRed(byte alpha) => new(0xCD, 0x5C, 0x5C, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #CD5C5C and the given opacity
    /// </summary>
    public static RGBAColor GetIndianRed(double opacity) => new(0xCD, 0x5C, 0x5C, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9932CC and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkOrchid(byte alpha) => new(0x99, 0x32, 0xCC, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9932CC and the given opacity
    /// </summary>
    public static RGBAColor GetDarkOrchid(double opacity) => new(0x99, 0x32, 0xCC, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0FFF0 and the given alpha value
    /// </summary>
    public static RGBAColor GetHoneydew(byte alpha) => new(0xF0, 0xFF, 0xF0, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F0FFF0 and the given opacity
    /// </summary>
    public static RGBAColor GetHoneydew(double opacity) => new(0xF0, 0xFF, 0xF0, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #ADFF2F and the given alpha value
    /// </summary>
    public static RGBAColor GetGreenYellow(byte alpha) => new(0xAD, 0xFF, 0x2F, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #ADFF2F and the given opacity
    /// </summary>
    public static RGBAColor GetGreenYellow(double opacity) => new(0xAD, 0xFF, 0x2F, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #008000 and the given alpha value
    /// </summary>
    public static RGBAColor GetGreen(byte alpha) => new(0x00, 0x80, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #008000 and the given opacity
    /// </summary>
    public static RGBAColor GetGreen(double opacity) => new(0x00, 0x80, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #80808 and the given alpha value
    /// </summary>
    public static RGBAColor GetGray(byte alpha) => new(0x80, 0x80, 0x80, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #80808 and the given opacity
    /// </summary>
    public static RGBAColor GetGray(double opacity) => new(0x80, 0x80, 0x80, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DAA520 and the given alpha value
    /// </summary>
    public static RGBAColor GetGoldenrod(byte alpha) => new(0xDA, 0xA5, 0x20, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DAA520 and the given opacity
    /// </summary>
    public static RGBAColor GetGoldenrod(double opacity) => new(0xDA, 0xA5, 0x20, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFD700 and the given alpha value
    /// </summary>
    public static RGBAColor GetGold(byte alpha) => new(0xFF, 0xD7, 0x00, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFD700 and the given opacity
    /// </summary>
    public static RGBAColor GetGold(double opacity) => new(0xFF, 0xD7, 0x00, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F8F8FF and the given alpha value
    /// </summary>
    public static RGBAColor GetGhostWhite(byte alpha) => new(0xF8, 0xF8, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #F8F8FF and the given opacity
    /// </summary>
    public static RGBAColor GetGhostWhite(double opacity) => new(0xF8, 0xF8, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DCDCDC and the given alpha value
    /// </summary>
    public static RGBAColor GetGainsboro(byte alpha) => new(0xDC, 0xDC, 0xDC, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #DCDCDC and the given opacity
    /// </summary>
    public static RGBAColor GetGainsboro(double opacity) => new(0xDC, 0xDC, 0xDC, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF00FF and the given alpha value
    /// </summary>
    public static RGBAColor GetFuchsia(byte alpha) => new(0xFF, 0x00, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF00FF and the given opacity
    /// </summary>
    public static RGBAColor GetFuchsia(double opacity) => new(0xFF, 0x00, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #228B22 and the given alpha value
    /// </summary>
    public static RGBAColor GetForestGreen(byte alpha) => new(0x22, 0x8B, 0x22, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #228B22 and the given opacity
    /// </summary>
    public static RGBAColor GetForestGreen(double opacity) => new(0x22, 0x8B, 0x22, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF69B4 and the given alpha value
    /// </summary>
    public static RGBAColor GetHotPink(byte alpha) => new(0xFF, 0x69, 0xB4, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF69B4 and the given opacity
    /// </summary>
    public static RGBAColor GetHotPink(double opacity) => new(0xFF, 0x69, 0xB4, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B22222 and the given alpha value
    /// </summary>
    public static RGBAColor GetFirebrick(byte alpha) => new(0xB2, 0x22, 0x22, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #B22222 and the given opacity
    /// </summary>
    public static RGBAColor GetFirebrick(double opacity) => new(0xB2, 0x22, 0x22, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFAF0 and the given alpha value
    /// </summary>
    public static RGBAColor GetFloralWhite(byte alpha) => new(0xFF, 0xFA, 0xF0, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FFFAF0 and the given opacity
    /// </summary>
    public static RGBAColor GetFloralWhite(double opacity) => new(0xFF, 0xFA, 0xF0, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #1E90FF and the given alpha value
    /// </summary>
    public static RGBAColor GetDodgerBlue(byte alpha) => new(0x1E, 0x90, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #1E90FF and the given opacity
    /// </summary>
    public static RGBAColor GetDodgerBlue(double opacity) => new(0x1E, 0x90, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #696969 and the given alpha value
    /// </summary>
    public static RGBAColor GetDimGray(byte alpha) => new(0x69, 0x69, 0x69, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #696969 and the given opacity
    /// </summary>
    public static RGBAColor GetDimGray(double opacity) => new(0x69, 0x69, 0x69, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00BFFF and the given alpha value
    /// </summary>
    public static RGBAColor GetDeepSkyBlue(byte alpha) => new(0x00, 0xBF, 0xFF, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00BFFF and the given opacity
    /// </summary>
    public static RGBAColor GetDeepSkyBlue(double opacity) => new(0x00, 0xBF, 0xFF, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF1493 and the given alpha value
    /// </summary>
    public static RGBAColor GetDeepPink(byte alpha) => new(0xFF, 0x14, 0x93, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #FF1493 and the given opacity
    /// </summary>
    public static RGBAColor GetDeepPink(double opacity) => new(0xFF, 0x14, 0x93, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9400D3 and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkViolet(byte alpha) => new(0x94, 0x00, 0xD3, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #9400D3 and the given opacity
    /// </summary>
    public static RGBAColor GetDarkViolet(double opacity) => new(0x94, 0x00, 0xD3, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00CED1 and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkTurquoise(byte alpha) => new(0x00, 0xCE, 0xD1, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #00CED1 and the given opacity
    /// </summary>
    public static RGBAColor GetDarkTurquoise(double opacity) => new(0x00, 0xCE, 0xD1, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #2F4F4F and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkSlateGray(byte alpha) => new(0x2F, 0x4F, 0x4F, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #2F4F4F and the given opacity
    /// </summary>
    public static RGBAColor GetDarkSlateGray(double opacity) => new(0x2F, 0x4F, 0x4F, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #483D8B and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkSlateBlue(byte alpha) => new(0x48, 0x3D, 0x8B, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #483D8B and the given opacity
    /// </summary>
    public static RGBAColor GetDarkSlateBlue(double opacity) => new(0x48, 0x3D, 0x8B, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8FBC8B and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkSeaGreen(byte alpha) => new(0x8F, 0xBC, 0x8B, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #8FBC8B and the given opacity
    /// </summary>
    public static RGBAColor GetDarkSeaGreen(double opacity) => new(0x8F, 0xBC, 0x8B, opacity);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #E9967A and the given alpha value
    /// </summary>
    public static RGBAColor GetDarkSalmon(byte alpha) => new(0xE9, 0x96, 0x7A, alpha);

    /// <summary>
    /// Gets a <see cref="RGBAColor"/> with a value of #E9967A and the given opacity
    /// </summary>
    public static RGBAColor GetDarkSalmon(double opacity) => new(0xE9, 0x96, 0x7A, opacity);

    #endregion
}
