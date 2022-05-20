﻿using SDL2.Bindings;
using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Mixer.Exceptions;
public class SDLMixerException : SDLException
{
    public SDLMixerException() { }
    public SDLMixerException(string message) : base(message) { }
    public SDLMixerException(string message, Exception inner) : base(message, inner) { }
    protected SDLMixerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new SDLMixerException(SDL_image.IMG_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new SDLMixerException(SDL_image.IMG_GetError());
    }
}
