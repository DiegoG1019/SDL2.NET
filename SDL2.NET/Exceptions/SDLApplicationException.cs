using System.Runtime.CompilerServices;
using SDL2.Bindings;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLApplicationException : SDLException
{
    public SDLApplicationException() { }
    public SDLApplicationException(string message) : base(message) { }
    public SDLApplicationException(string message, Exception inner) : base(message, inner) { }
    protected SDLApplicationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLApplicationException(SDL.SDL_GetAndClearError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLApplicationException(SDL.SDL_GetAndClearError());
    }
}
