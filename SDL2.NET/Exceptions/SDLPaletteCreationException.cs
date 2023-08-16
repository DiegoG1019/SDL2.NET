using System.Runtime.CompilerServices;
using SDL2.Bindings;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLPaletteCreationException : SDLException
{
    public SDLPaletteCreationException() { }
    public SDLPaletteCreationException(string message) : base(message) { }
    public SDLPaletteCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLPaletteCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLPaletteCreationException(SDL.SDL_GetAndClearError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLPaletteCreationException(SDL.SDL_GetAndClearError());
    }
}
