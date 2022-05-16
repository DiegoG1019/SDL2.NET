using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLOpenGLContextCreationException : SDLException
{
    public SDLOpenGLContextCreationException() { }
    public SDLOpenGLContextCreationException(string message) : base(message) { }
    public SDLOpenGLContextCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLOpenGLContextCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLOpenGLContextCreationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLOpenGLContextCreationException(SDL.SDL_GetError());
    }
}
