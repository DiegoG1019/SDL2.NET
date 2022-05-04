using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLRendererException : SDLException
{
    public SDLRendererException() { }
    public SDLRendererException(string message) : base(message) { }
    public SDLRendererException(string message, Exception inner) : base(message, inner) { }
    protected SDLRendererException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLRendererException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLRendererException(SDL.SDL_GetError());
    }
}
