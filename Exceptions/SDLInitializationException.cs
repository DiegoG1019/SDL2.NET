using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLInitializationException : SDLException
{
    public SDLInitializationException() { }
    public SDLInitializationException(string message) : base(message) { }
    public SDLInitializationException(string message, Exception inner) : base(message, inner) { }
    protected SDLInitializationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLInitializationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLInitializationException(SDL.SDL_GetError());
    }
}
