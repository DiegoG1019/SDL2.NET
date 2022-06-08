using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLGameControllerException : SDLException
{
    public SDLGameControllerException() { }
    public SDLGameControllerException(string message) : base(message) { }
    public SDLGameControllerException(string message, Exception inner) : base(message, inner) { }
    protected SDLGameControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLGameControllerException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLGameControllerException(SDL.SDL_GetError());
    }
}
