using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents information about a specific Display. Only valid if obtained through <see cref="Display.Displays"/>
/// </summary>
/// <remarks>May not be valid after <see cref="Display.DisplayConnected"/>, <see cref="Display.DisplayDisconnected"/> or <see cref="Display.OrientationChanged"/> is fired</remarks>
public struct DisplayInfo
{
    private readonly int index;

    public DisplayInfo()
    {
        index = -1;
    }

    internal DisplayInfo(int index)
    {
        this.index = index;
    }

    /// <summary>
    /// Represents information about the various <see cref="DisplayMode"/>s this Display has available
    /// </summary>
    public DisplayModeCollection DisplayModes => new(index);

    /// <summary>
    /// Get information about the current display mode of the Display
    /// </summary>
    public DisplayMode CurrentMode
    {
        get
        {
            SDLDisplayException.ThrowIfLessThan(SDL_GetCurrentDisplayMode(index, out var mode), 0);
            return (DisplayMode)mode;
        }
    }

    /// <summary>
    /// Get the name of a built in video driver
    /// </summary>
    /// <remarks>The video drivers are presented in the order in which they are normally checked during initialization.</remarks>
    public string VideoDriver => SDL_GetVideoDriver(index);

    /// <summary>
    /// Get the usable desktop area represented by a display.
    /// </summary>
    /// <remarks>
    /// The primary display (displayIndex zero) is always located at 0,0. This is the same area as <see cref="Bounds"/> reports, but with portions reserved by the system removed. For example, on Apple's macOS, this subtracts the area occupied by the menu bar and dock. Setting a window to be fullscreen generally bypasses these unusable areas, so these are good guidelines for the maximum space available to a non-fullscreen <see cref="Window"/>.
    /// </remarks>
    public Rectangle UsableBounds
    {
        get
        {
            SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayUsableBounds(index, out var rect), 0);
            return rect;
        }
    }

    /// <summary>
    /// Get the desktop area represented by a display.
    /// </summary>
    /// <remarks>
    /// The primary display (displayIndex zero) is always located at 0,0.
    /// </remarks>
    public Rectangle Bounds
    {
        get
        {
            SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayBounds(index, out var rect), 0);
            return rect;
        }
    }

    /// <summary>
    /// The orientation of the Display
    /// </summary>
    public DisplayOrientation Orientation => (DisplayOrientation)SDL_GetDisplayOrientation(index);

    /// <summary>
    /// Gets the DPI of the Display
    /// </summary>
    /// <remarks>Dots per inch, in the context of a Video Display; is the number of individual dots that can be placed in a line within the span of 1 inch (2.54 cm). <see href="https://en.wikipedia.org/wiki/Dots_per_inch"/></remarks>
    public DPIInfo DPI
    {
        get
        {
            SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayDPI(index, out var dpi, out var hdpi, out var vdpi), 0);
            return new(dpi, hdpi, vdpi);
        }
    }

    /// <summary>
    /// Information about the desktop's <see cref="DisplayMode"/>
    /// </summary>
    /// <remarks>There's a difference between this property and <see cref="CurrentMode"/> when SDL runs fullscreen and has changed the resolution. In that case this property will return the previous native display mode, and not the current display mode</remarks>
    public DisplayMode DesktopMode
    {
        get
        {
            SDLDisplayException.ThrowIfLessThan(SDL_GetDesktopDisplayMode(index, out var mode), 0);
            return (DisplayMode)mode;
        }
    }

    /// <summary>
    /// Get the name of a display in UTF-8 encoding
    /// </summary>
    public string Name
    {
        get
        {
            var r = SDL_GetDisplayName(index);
            return r is null ? throw new SDLDisplayException(SDL_GetAndClearError()) : r;
        }
    }

    /// <summary>
    /// Get the closest match to the requested display mode.
    /// </summary>
    /// <param name="mode">The desired mode</param>
    /// <returns>The closest <see cref="DisplayMode"/> that matches</returns>
    public DisplayMode GetClosestDisplayMode(DisplayMode mode)
    {
        SDL_DisplayMode m = (SDL_DisplayMode)mode;
        SDL_GetClosestDisplayMode(index, ref m, out var closest);
        return (DisplayMode)closest;
    }
}
