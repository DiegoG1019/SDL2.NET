using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLRendererCreationException : SDLException
{
    public SDLRendererCreationException() { }
    public SDLRendererCreationException(string message) : base(message) { }
    public SDLRendererCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLRendererCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLRendererCreationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLRendererCreationException(SDL.SDL_GetError());
    }
}
