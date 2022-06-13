using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL_mixer;

namespace SDL2.NET.SDLMixer;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[Flags]
public enum MixerInitFlags
{
    FLAC = 0x00000001,
    MOD = 0x00000002,
    MP3 = 0x00000008,
    OGG = 0x00000010,
    MID = 0x00000020,
    OPUS = 0x00000040
}

public enum AudioFadeStatus
{
    NotFading = Mix_Fading.MIX_NO_FADING,
    FadingOut = Mix_Fading.MIX_FADING_OUT,
    FadingIn = Mix_Fading.MIX_FADING_IN
}

public enum MusicType
{
    /// <summary>
    /// No music type
    /// </summary>
    None = Mix_MusicType.MUS_NONE,

    /// <summary>
    /// Command Based Music
    /// </summary>
    CommandBased = Mix_MusicType.MUS_CMD,

    /// <summary>
    /// Waveform Audio File Format: <see href="https://en.wikipedia.org/wiki/WAV"/>
    /// </summary>
    /// <remarks>.wav</remarks>
    WAV = Mix_MusicType.MUS_WAV,

    /// <summary>
    /// Module File: <see href="https://en.wikipedia.org/wiki/MOD_%28file_format%29"/>
    /// </summary>
    /// <remarks>.mod</remarks>
    MOD = Mix_MusicType.MUS_MOD,

    /// <summary>
    /// Musical Instrument Digital Interface: <see href="https://en.wikipedia.org/wiki/MIDI"/>
    /// </summary>
    /// <remarks>.mid or .midi</remarks>
    MIDI = Mix_MusicType.MUS_MID,

    /// <summary>
    /// Ogg: <see href="https://en.wikipedia.org/wiki/Ogg"/>
    /// </summary>
    /// <remarks>.ogg</remarks>
    OGG = Mix_MusicType.MUS_OGG,

    /// <summary>
    /// MPEG-1 Audio Layer III or MPEG-2 Audio Layer III: <see href="https://en.wikipedia.org/wiki/MP3"/>
    /// </summary>
    /// <remarks>.mp3</remarks>
    MP3 = Mix_MusicType.MUS_MP3,

    /// <summary>
    /// Free Lossless Audio Codec: <see href="https://en.wikipedia.org/wiki/FLAC"/>
    /// </summary>
    /// <remarks>.flac</remarks>
    FLAC = Mix_MusicType.MUS_FLAC,

    /// <summary>
    /// OPUS: <see href="https://en.wikipedia.org/wiki/Opus_(audio_format)"/>
    /// </summary>
    /// <remarks>.opus</remarks>
    OPUS = Mix_MusicType.MUS_OPUS,
}