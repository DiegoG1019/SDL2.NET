using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLWindowCreationException : SDLException
{
    public SDLWindowCreationException() { }
    public SDLWindowCreationException(string message) : base(message) { }
    public SDLWindowCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLWindowCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLWindowCreationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLWindowCreationException(SDL.SDL_GetError());
    }
}
