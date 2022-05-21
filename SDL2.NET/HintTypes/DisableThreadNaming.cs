using SDL2.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.HintTypes;
public sealed class DisableThreadNaming : BinaryHint
{
    internal DisableThreadNaming() : base(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING)
    {
    }
}
