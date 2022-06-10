using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLJoystickException : SDLException
{
    public SDLJoystickException() { }
    public SDLJoystickException(string message) : base(message) { }
    public SDLJoystickException(string message, Exception inner) : base(message, inner) { }
    protected SDLJoystickException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison, string defError = "")
    {
        if (value < comparison)
            throw new SDLJoystickException(SDL.SDL_GetAndClearError() ?? defError);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison, string defError = "")
    {
        if (value == comparison)
            throw new SDLJoystickException(SDL.SDL_GetAndClearError() ?? defError);
    }
}
