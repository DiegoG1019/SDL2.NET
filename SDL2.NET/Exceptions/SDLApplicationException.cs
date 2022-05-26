using SDL2.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLApplicationException : SDLException
{
    public SDLApplicationException() { }
    public SDLApplicationException(string message) : base(message) { }
    public SDLApplicationException(string message, Exception inner) : base(message, inner) { }
    protected SDLApplicationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLApplicationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLApplicationException(SDL.SDL_GetError());
    }
}
