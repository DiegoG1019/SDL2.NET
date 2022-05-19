using SDL2.Bindings;
using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Font.Exceptions;
public class SDLFontException : SDLException
{
    public SDLFontException() { }
    public SDLFontException(string message) : base(message) { }
    public SDLFontException(string message, Exception inner) : base(message, inner) { }
    protected SDLFontException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLFontException(SDL_ttf.TTF_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLFontException(SDL_ttf.TTF_GetError());
    }
}
