using SDL2.Bindings;
using System.Runtime.CompilerServices;

namespace SDL2.NET.Mixer.Exceptions;

[Serializable]
public class MixerAudioChunkException : MixerException
{
    public MixerAudioChunkException() { }
    public MixerAudioChunkException(string message) : base(message) { }
    public MixerAudioChunkException(string message, Exception inner) : base(message, inner) { }
    protected MixerAudioChunkException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(int value, int comparison)
    {
        if (value < comparison)
            throw new MixerAudioChunkException(SDL_mixer.Mix_GetError());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfEquals(int value, int comparison)
    {
        if (value == comparison)
            throw new MixerAudioChunkException(SDL_mixer.Mix_GetError());
    }
}