using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;

public enum FullscreenMode : uint
{
    Windowed = 0,
    Fullscreen = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
    DesktopFullscreen = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP,
}
