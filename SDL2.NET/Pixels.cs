using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Provides a way of accessing SDL's pixel related methods
/// </summary>
public static class Pixels
{
    /// <summary>
    /// Convert a bpp value and RGBA masks to an enumerated pixel format.
    /// </summary>
    public static PixelFormat ToPixelFormat(this RGBAMask mask)
        => (PixelFormat)SDL_MasksToPixelFormatEnum(mask.BitsPerPixel, mask.Red, mask.Green, mask.Blue, mask.Alpha);

    /// <summary>
    /// Convert one of the enumerated pixel formats to a bpp value and RGBA masks
    /// </summary>
    public static RGBAMask ToRGBAMask(this PixelFormat format)
    {
        SDL_PixelFormatEnumToMasks((uint)format, out var bpp, out var r, out var g, out var b, out var a);
        return new(bpp, r, g, b, a);
    }
}
