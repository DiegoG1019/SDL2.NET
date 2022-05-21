using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.SDLMixer.Exceptions;

[Serializable]
public class MixerSongCreationException : MixerException
{
    public MixerSongCreationException() { }
    public MixerSongCreationException(string message) : base(message) { }
    public MixerSongCreationException(string message, Exception inner) : base(message, inner) { }
    protected MixerSongCreationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new MixerSongCreationException(SDL_mixer.Mix_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new MixerSongCreationException(SDL_mixer.Mix_GetError());
    }
}
