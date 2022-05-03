using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Exceptions;

[Serializable]
public abstract class SDLException : Exception
{
    public SDLException() { }
    public SDLException(string message) : base(message) { }
    public SDLException(string message, Exception inner) : base(message, inner) { }
    protected SDLException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class SDLInitializationException : SDLException
{
    public SDLInitializationException() { }
    public SDLInitializationException(string message) : base(message) { }
    public SDLInitializationException(string message, Exception inner) : base(message, inner) { }
    protected SDLInitializationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLInitializationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLInitializationException(SDL.SDL_GetError());
    }
}

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

[Serializable]
public class SDLWindowCreationException : SDLException
{
    public SDLWindowCreationException() { }
    public SDLWindowCreationException(string message) : base(message) { }
    public SDLWindowCreationException(string message, Exception inner) : base(message, inner) { }
    protected SDLWindowCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLWindowCreationException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLWindowCreationException(SDL.SDL_GetError());
    }
}

[Serializable]
public class SDLWindowException : SDLException
{
    public SDLWindowException() { }
    public SDLWindowException(string message) : base(message) { }
    public SDLWindowException(string message, Exception inner) : base(message, inner) { }
    protected SDLWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLWindowException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLWindowException(SDL.SDL_GetError());
    }
}

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

[Serializable]
public class SDLDisplayException : SDLException
{
    public SDLDisplayException() { }
    public SDLDisplayException(string message) : base(message) { }
    public SDLDisplayException(string message, Exception inner) : base(message, inner) { }
    protected SDLDisplayException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLDisplayException(SDL.SDL_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLDisplayException(SDL.SDL_GetError());
    }
}

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
