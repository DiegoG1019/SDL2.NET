using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLDisplayException : SDLException
{
    public SDLDisplayException() { }
    public SDLDisplayException(string message) : base(message) { }
    public SDLDisplayException(string message, Exception inner) : base(message, inner) { }
    protected SDLDisplayException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLDisplayException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLDisplayException(SDL.SDL_GetError());
    }
}
