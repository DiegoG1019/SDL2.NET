using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL_mixer;

namespace SDL2.NET.Mixer;

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