using SDL2.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLMouseCursorCreationException : SDLException
{
    public SDLMouseCursorCreationException() { }
    public SDLMouseCursorCreationException(string message) : base(message) { }
    public SDLMouseCursorCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLMouseCursorCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLMouseCursorCreationException(SDL.SDL_GetAndClearError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLMouseCursorCreationException(SDL.SDL_GetAndClearError());
    }
}