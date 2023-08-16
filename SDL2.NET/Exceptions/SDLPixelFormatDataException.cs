using System.Runtime.CompilerServices;
using SDL2.Bindings;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLPixelFormatDataException : SDLException
{
    public SDLPixelFormatDataException() { }
    public SDLPixelFormatDataException(string message) : base(message) { }
    public SDLPixelFormatDataException(string message, Exception inner) : base(message, inner) { }
    protected SDLPixelFormatDataException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLPixelFormatDataException(SDL.SDL_GetAndClearError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLPixelFormatDataException(SDL.SDL_GetAndClearError());
    }
}
