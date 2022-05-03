using static SDL2.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies whether SDL should not use version 4 of the bitmap header when saving BMPs .<see cref="SDL_HINT_BMP_SAVE_LEGACY_FORMAT" href="https://wiki.libsdl.org/SDL_HINT_BMP_SAVE_LEGACY_FORMAT"/>
/// </summary>
/// <remarks>
/// The bitmap header version 4 is required for proper alpha channel support and SDL will use it when required. Should this not be desired, this hint can force the use of the 40 byte header version which is supported everywhere.
/// </remarks>
public sealed class BMPSaveLegacyFormat : BinaryHint
{
    internal BMPSaveLegacyFormat() : base(SDL_HINT_BMP_SAVE_LEGACY_FORMAT) { }
}