using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLSurfaceCreationException : SDLException
{
    public SDLSurfaceCreationException() { }
    public SDLSurfaceCreationException(string message) : base(message) { }
    public SDLSurfaceCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLSurfaceCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLSurfaceCreationException(SDL.SDL_GetAndClearError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLSurfaceCreationException(SDL.SDL_GetAndClearError());
    }
}
