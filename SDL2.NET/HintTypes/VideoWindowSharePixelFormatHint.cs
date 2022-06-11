using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies the address of another <see cref="Window"/> that new <see cref="Window"/>s should share pixel format data with, when created with <see cref="SDL2.Bindings.SDL.SDL_CreateWindowFrom"/>
/// </summary>
/// <remarks>
/// By default this hint is not set. For more info, see <see href="https://wiki.libsdl.org/SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT"/>
/// </remarks>
public sealed class VideoWindowSharePixelFormatHint : Hint
{
    internal VideoWindowSharePixelFormatHint() : base(SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT) { }

    private Window? _win;
    public Window? Window
    {
        get => _win;
        set
        {
            _win = value;
            Set(("0x" + _win?._handle.ToString("x")) ?? "0x0");
        }
    }
}
