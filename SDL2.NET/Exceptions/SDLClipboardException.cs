using SDL2.Bindings;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Exceptions;

[Serializable]
public class SDLClipboardException : SDLException
{
    public SDLClipboardException() { }
    public SDLClipboardException(string message) : base(message) { }
    public SDLClipboardException(string message, Exception inner) : base(message, inner) { }
    protected SDLClipboardException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLDisplayException(SDL.SDL_GetAndClearError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull("value")]
    public static string? ThrowIfEmpty(string? value) 
        => value == "" ? throw new SDLClipboardException(SDL.SDL_GetAndClearError()) : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull("value")]
    public static string? ThrowIfNullOrEmpty(string? value) 
        => string.IsNullOrEmpty(value) ? throw new SDLClipboardException(SDL.SDL_GetAndClearError()) : value;
}
