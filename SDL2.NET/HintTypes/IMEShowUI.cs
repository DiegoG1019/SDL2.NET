using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.HintTypes;

// TODO: Document this
public sealed class IMEShowUI : BinaryHint
{
    internal IMEShowUI() : base(SDL2.Bindings.SDL.SDL_HINT_IME_SHOW_UI) { }
}
