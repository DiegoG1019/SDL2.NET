using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLTextureCreationException : SDLException
{
    public SDLTextureCreationException() { }
    public SDLTextureCreationException(string message) : base(message) { }
    public SDLTextureCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLTextureCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLTextureCreationException(SDL.SDL_GetAndClearError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLTextureCreationException(SDL.SDL_GetAndClearError());
    }
}
