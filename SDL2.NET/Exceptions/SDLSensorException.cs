using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLSensorException : SDLException
{
    public SDLSensorException() { }
    public SDLSensorException(string message) : base(message) { }
    public SDLSensorException(string message, Exception inner) : base(message, inner) { }
    protected SDLSensorException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ThrowIfLessThan(int value, int comparison) 
        => value < comparison ? throw new SDLSensorException(SDL.SDL_GetAndClearError()) : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ThrowIfEquals(int value, int comparison) 
        => value == comparison ? throw new SDLSensorException(SDL.SDL_GetAndClearError()) : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ThrowIfNull<T>(T value)
        => value is null ? throw new SDLSensorException(SDL.SDL_GetAndClearError()) : value;
}

