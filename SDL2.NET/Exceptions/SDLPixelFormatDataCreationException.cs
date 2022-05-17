using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLPixelFormatDataCreationException : SDLException
{
    public SDLPixelFormatDataCreationException() { }
    public SDLPixelFormatDataCreationException(string message) : base(message) { }
    public SDLPixelFormatDataCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLPixelFormatDataCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLPixelFormatDataCreationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLPixelFormatDataCreationException(SDL.SDL_GetError());
    }
}
