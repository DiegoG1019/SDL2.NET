using System.Runtime.CompilerServices;
using SDL2.Bindings;
using SDL2.NET.Exceptions;

namespace SDL2.NET.SDLFont.Exceptions;
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr ThrowIfPointerNull(IntPtr ptr)
        => ptr == IntPtr.Zero ? throw new SDLFontException(SDL_ttf.TTF_GetError()) : ptr;
}
