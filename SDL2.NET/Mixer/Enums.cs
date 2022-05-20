using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
