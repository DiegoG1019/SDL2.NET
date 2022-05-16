using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLOpenGLContextException : SDLException
{
    public SDLOpenGLContextException() { }
    public SDLOpenGLContextException(string message) : base(message) { }
    public SDLOpenGLContextException(string message, Exception inner) : base(message, inner) { }
    protected SDLOpenGLContextException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLOpenGLContextException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLOpenGLContextException(SDL.SDL_GetError());
    }
}
