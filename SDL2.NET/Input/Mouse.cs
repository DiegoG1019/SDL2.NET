using SDL2.Bindings;
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
    /// Gets the current state of the mouse, relative to the previous state
    /// </summary>
    public static MouseState RelativeMouseState => new(CheckButton(SDL_GetRelativeMouseState(out int x, out int y)), new(x, y));

    /// <summary>
    /// Gets the current state of the mouse
    /// </summary>
    public static MouseState MouseState => new(CheckButton(SDL_GetMouseState(out int x, out int y)), new(x, y));

    /// <summary>
    /// Get the current state of the mouse in relation to the desktop.
    /// </summary>
    /// <remarks>
    /// This works similarly to <see cref="MouseState"/>, but the coordinates will be reported relative to the top-left of the desktop. This can be useful if you need to track the mouse outside of a specific window and <see cref="SetMouseCapture"/> doesn't fit your needs. For example, it could be useful if you need to track the mouse while dragging a window, where coordinates relative to a window might not be in sync at all times. <see cref="MouseState"/> returns the mouse position as SDL understands it from the last pump of the event queue. This method, however, queries the OS for the current mouse position, and as such, might be a slightly less efficient method. Unless you know what you're doing and have a good reason to use this method, you probably want <see cref="MouseState"/> instead
    /// </remarks>
    public static MouseState GlobalMouseState => new(CheckButton(SDL_GetGlobalMouseState(out int x, out int y)), new(x, y));

    /// <summary>
    /// Capture the mouse and to track input outside an SDL window.
    /// </summary>
    /// <remarks>
    /// Capturing enables your app to obtain mouse events globally, instead of just within your window. Not all video targets support this method. When capturing is enabled, the current window will get all mouse events, but unlike relative mode, no change is made to the cursor and it is not restrained to your window.
    /// This method may also deny mouse input to other windows--both those in your application and others on the system--so you should use this method sparingly, and in small bursts. For example, you might want to track the mouse while the user is dragging something, until the user releases a mouse button. It is not recommended that you capture the mouse for long periods of time, such as the entire time your app is running. For that, you should probably use <see cref="RelativeMouseMode"/> or <see cref="Window.MouseGrab"/>, depending on your goals.
    /// </remarks>
    /// <returns>true if the operation was succesful, false if this is not supported by the system</returns>
    public static bool SetMouseCapture(bool enabled)
        => SDL_CaptureMouse(enabled ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE) == 0;

    /// <summary>
    /// Sets the given <see cref="MouseCursor"/> as the current active cursor
    /// </summary>
    /// <remarks>
    /// This function sets the currently active cursor to the specified one. If the cursor is currently visible, the change will be immediately represented on the display.
    /// </remarks>
    public static void SetMouseCursor(this MouseCursor cursor)
    {
        cursor.ThrowIfDisposed();
        SDL_SetCursor(cursor._handle);
    }

    /// <summary>
    /// Toggle whether or not the cursor is shown
    /// </summary>
    public static bool IsCursorVisible
    {
        get
        {
            var r = SDL_ShowCursor(SDL_QUERY);
            SDLMouseException.ThrowIfLessThan(r, 0);
            return r == 1;
        }
        set
        {
            SDLMouseException.ThrowIfLessThan(SDL_ShowCursor(value ? 1 : 0), 0);
        }
    }

    /// <summary>
    /// Forces a cursor redraw, if desired for any reason.
    /// </summary>
    /// <remarks>Calls <see cref="SDL_SetCursor(IntPtr)"/> and passes <see cref="IntPtr.Zero"/> (NULL) to force a cursor redraw, as spec'd by SDL's docs</remarks>
    public static void ForceCursorRedraw()
        => SDL_SetCursor(IntPtr.Zero);

    /// <summary>
    /// Gets or Sets whether Relative Mouse Mode is enabled
    /// </summary>
    /// <remarks>While the mouse is in relative mode, the cursor is hidden, and the driver will try to report continuous motion in the current window. Only relative motion events will be delivered, the mouse position will not change. Note that this method will not be able to provide continuous relative motion when used over Microsoft Remote Desktop, instead motion is limited to the bounds of the screen. This method will flush any pending mouse motion.</remarks>
    public static bool RelativeMouseMode
    {
        get => SDL_GetRelativeMouseMode() is SDL_bool.SDL_TRUE;
        set
        {
            SDLMouseException.ThrowIfLessThan(SDL_SetRelativeMouseMode(value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE), 0);
        }
    }

    /// <summary>
    /// Move the mouse cursor to the given position within the window
    /// </summary>
    /// <param name="window">The window to warp the mouse cursor in</param>
    /// <param name="location">The location within the window</param>
    /// <remarks>This method generates a mouse motion event. Note that this method will appear to succeed, but not actually move the mouse when used over Microsoft Remote Desktop.</remarks>
    public static void WarpMouse(this Window window, Point location)
    {
        SDL_WarpMouseInWindow(window._handle, location.X, location.Y);
    }

    /// <summary>
    /// Move the mouse cursor to the given position within the desktop
    /// </summary>
    /// <param name="location">The location to warp the mouse into</param>
    /// <remarks>This method generates a mouse motion event. Note that this method will appear to succeed, but not actually move the mouse when used over Microsoft Remote Desktop.</remarks>
    public static void WarpMouse(Point location)
    {
        SDLMouseException.ThrowIfLessThan(SDL_WarpMouseGlobal(location.X, location.Y), 0);
    }

    /// <summary>
    /// The id that gets returned when the Mouse is actually a Touch device
    /// </summary>
    public const uint TouchMouseId = SDL_TOUCH_MOUSEID;

    #region Internal

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

    #endregion
}
