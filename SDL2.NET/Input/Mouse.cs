using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Provides methods and properties to interact with the currently plugged mice
/// </summary>
public static class Mouse
{
    /// <summary>
    /// Move the mouse cursor to the given position within the window
    /// </summary>
    /// <param name="window">The window to warp the mouse cursor in</param>
    /// <param name="location">The location within the window</param>
    /// <remarks>This function generates a mouse motion event. Note that this function will appear to succeed, but not actually move the mouse when used over Microsoft Remote Desktop.</remarks>
    public static void WarpMouse(this Window window, Point location)
    {
        SDL_WarpMouseInWindow(window._handle, location.X, location.Y);
    }

    /// <summary>
    /// Move the mouse cursor to the given position within the desktop
    /// </summary>
    /// <param name="location">The location to warp the mouse into</param>
    /// <remarks>This function generates a mouse motion event. Note that this function will appear to succeed, but not actually move the mouse when used over Microsoft Remote Desktop.</remarks>
    public static void WarpMouse(Point location)
    {
        SDLMouseException.ThrowIfLessThan(SDL_WarpMouseGlobal(location.X, location.Y), 0);
    }

    public const uint TouchMouseId = SDL_TOUCH_MOUSEID;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MouseButton MapButton(uint value)
        => value switch
        {
            SDL_BUTTON_LEFT => MouseButton.Left,
            SDL_BUTTON_MIDDLE => MouseButton.Middle,
            SDL_BUTTON_RIGHT => MouseButton.Right,
            SDL_BUTTON_X1 => MouseButton.X1,
            SDL_BUTTON_X2 => MouseButton.X2,
            _ => 0
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MouseButton CheckIfButtonLeft(uint value)
        => (SDL_BUTTON_LMASK | value) == SDL_BUTTON_LEFT ? MouseButton.Left : 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MouseButton CheckIfButtonMiddle(uint value)
        => (SDL_BUTTON_MMASK | value) == SDL_BUTTON_MIDDLE ? MouseButton.Middle : 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MouseButton CheckIfButtonRight(uint value)
        => (SDL_BUTTON_RMASK | value) == SDL_BUTTON_RIGHT ? MouseButton.Right : 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MouseButton CheckIfButtonX1(uint value)
        => (SDL_BUTTON_X1MASK | value) == SDL_BUTTON_X1 ? MouseButton.X1 : 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MouseButton CheckIfButtonX2(uint value)
        => (SDL_BUTTON_X1MASK | value) == SDL_BUTTON_X2 ? MouseButton.X2 : 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MouseButton CheckButton(uint value)
        => CheckIfButtonLeft(value) | CheckIfButtonMiddle(value) | CheckIfButtonRight(value) | CheckIfButtonX1(value) | CheckIfButtonX2(value);
}
