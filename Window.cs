using SDL2.NET.Exceptions;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SDL2.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents an SDL Window object
/// </summary>
public class Window : IDisposable
{
    internal static readonly ConcurrentDictionary<IntPtr, WeakReference<Window>> _handleDict = new();
    protected internal readonly IntPtr _handle = IntPtr.Zero;

    public static WindowRenderer CreateWindowAndRenderer(string title, int width, int height, int rendererIndex = -1, SDL_WindowFlags flags = SDL_WindowFlags.SDL_WINDOW_RESIZABLE, int? centerPointX = null, int? centerPointY = null)
    {
        var win = new Window(title, width, height, flags, centerPointX, centerPointY);
        return new WindowRenderer(win, rendererIndex);
    }

    public Window(string title, int width, int height, SDL_WindowFlags flags = SDL_WindowFlags.SDL_WINDOW_RESIZABLE, int? centerPointX = null, int? centerPointY = null)
    {
        _handle = SDL_CreateWindow(
            title,
            centerPointX ?? SDL_WINDOWPOS_CENTERED,
            centerPointY ?? SDL_WINDOWPOS_CENTERED,
            width,
            height,
            flags
        );
        if (_handle == IntPtr.Zero)
            throw new SDLWindowCreationException(SDL_GetError());
        _handleDict[_handle] = new(this);

        _hitTestSupported = SDL_SetWindowHitTest(_handle, htcallback, IntPtr.Zero) == 0;

        // local function
        SDL_HitTestResult htcallback(IntPtr win, IntPtr area, IntPtr data)
            => hitTestCallback is null ? SDL_HitTestResult.SDL_HITTEST_NORMAL : hitTestCallback(this, Marshal.PtrToStructure<SDL_Point>(area), hitTestCallbackData).ToSDL();
    }

    /// <summary>
    /// Gets or Sets the <see cref="SDL"/> <see cref="Window"/> <see cref="Title"/>. get: <see cref="SDL_GetWindowTitle" href="https://wiki.libsdl.org/SDL_GetWindowTitle"/>; set: <see cref="SDL_SetWindowTitle" href="https://wiki.libsdl.org/SDL_SetWindowTitle"/>
    /// </summary>
    public string Title
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowTitle(_handle);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowTitle(_handle, value);
        }
    }

    /// <summary>
    /// Gets or Sets the <see cref="Window"/>'s <see cref="Opacity"/>. get: <see cref="SDL_GetWindowOpacity" href="https://wiki.libsdl.org/SDL_GetWindowOpacity"/>; set: <see cref="SDL_SetWindowOpacity" href="https://wiki.libsdl.org/SDL_SetWindowOpacity"/>
    /// </summary>
    /// <remarks>
    /// If transparency isn't supported on this platform, <see cref="Opacity"/> will be reported as 1.0f without error. The value will be clamped internally between 0.0f (transparent) and 1.0f (opaque).
    /// </remarks>
    /// <exception cref="SDLWindowException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    public float Opacity
    {
        get
        {
            ThrowIfDisposed();
            SDLWindowException.ThrowIfLessThan(SDL_GetWindowOpacity(_handle, out var opacity), 0);
            return opacity;
        }
        set
        {
            ThrowIfDisposed();
            var error = SDL_SetWindowOpacity(_handle, value);
            if (error is -1)
                throw new PlatformNotSupportedException(SDL_GetError());
            SDLWindowException.ThrowIfLessThan(error, 0);
        }
    }

    /// <summary>
    /// Gets the <see cref="Window"/>'s Brightness. get: <see cref="SDL_GetWindowBrightness" href="https://wiki.libsdl.org/SDL_GetWindowBrightness"/>
    /// </summary>
    /// <remarks>
    /// Despite the name, this method retrieves the brightness of the entire <see cref="Display"/>, not an individual <see cref="Window"/>. A <see cref="Window"/> is considered to be owned by the <see cref="Display"/> that contains the <see cref="Window"/>'s center pixel. (The index of this display can be retrieved with <see cref="DisplayIndex"/>.)
    /// </remarks>
    /// <returns></returns>
    public float GetBrightness
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowBrightness(_handle);
        }
    }

    /// <summary>
    /// Sets <see cref="this"/> <see cref="Window"/> as another <see cref="Window"/>'s modal. <see cref="SDL_SetWindowModalFor" href="https://wiki.libsdl.org/SDL_SetWindowModalFor"/>
    /// </summary>
    /// <param name="parent">The parent <see cref="Window"/> to set this window as a modal for</param>
    public void SetAsModalFor(Window parent)
    {
        ThrowIfDisposed();
        if (ReferenceEquals(this, parent))
            throw new InvalidOperationException("Cannot set a Window as a modal of itself");
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowModalFor(_handle, parent._handle), 0);
    }

    /// <summary>
    /// Sets <paramref name="modal"/> as <see cref="this"/> <see cref="Window"/>'s modal.
    /// </summary>
    /// <param name="parent">The parent <see cref="Window"/> to set this <see cref="Window"/> as a modal for</param>
    public void SetAsModal(Window modal)
    {
        ThrowIfDisposed();
        modal.SetAsModalFor(this);
    }

    /// <summary>
    /// Raise a <see cref="Window"/> above other <see cref="Window"/>s and set the input focus. <see cref="SDL_RaiseWindow" href="https://wiki.libsdl.org/SDL_RaiseWindow"/>
    /// </summary>
    public void Raise()
    {
        ThrowIfDisposed();
        SDL_RaiseWindow(_handle);
    }

    /// <summary>
    /// Shows the <see cref="Window"/>. <see cref="SDL_ShowWindow" href="https://wiki.libsdl.org/SDL_ShowWindow"/>
    /// </summary>
    public void Show()
    {
        ThrowIfDisposed();
        SDL_ShowWindow(_handle);
    }

    /// <summary>
    /// Hides the <see cref="Window"/>. <see cref="SDL_HideWindow" href="https://wiki.libsdl.org/SDL_HideWindow"/>
    /// </summary>
    public void Hide()
    {
        ThrowIfDisposed();
        SDL_HideWindow(_handle);
    }

    /// <summary>
    /// Sets this Window as the OS's input focus. <see cref="SDL_SetWindowInputFocus" href="https://wiki.libsdl.org/SDL_SetWindowInputFocus"/>
    /// </summary>
    /// <remarks>
    /// You almost certainly want <see cref="Raise"/> instead of this method. Use this with caution, as you might give focus to a window that is completely obscured by other windows.
    /// </remarks>
    public void SetInputFocus()
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowInputFocus(_handle), 0);
    }

    /// <summary>
    /// Gets this Window's display index. <see cref="SDL_GetWindowDisplayIndex" href="https://wiki.libsdl.org/SDL_GetWindowDisplayIndex"/>
    /// </summary>
    /// <remarks>
    /// The index of the display containing the center of the window on success
    /// </remarks>
    public int DisplayIndex
    {
        get
        {
            ThrowIfDisposed();
            var r = SDL_GetWindowDisplayIndex(_handle);
            SDLWindowException.ThrowIfLessThan(r, 0);
            return r;
        }
    }

    /// <summary>
    /// Gets or Sets the Display mode of this window. Getter never returns null. get: <see cref="SDL_GetWindowDisplayMode" href="https://wiki.libsdl.org/SDL_GetWindowDisplayMode"/>; set: <see cref="SDL_SetWindowDisplayMode" href="https://wiki.libsdl.org/SDL_SetWindowDisplayMode"/>
    /// </summary>
    /// <remarks>
    /// This only affects the display mode used when the window is fullscreen. To change the window size when the window is not fullscreen, use <see cref="Size" />.
    /// </remarks>
    public SDL_DisplayMode? DisplayMode
    {
        [return: NotNull]
        get
        {
            ThrowIfDisposed();
            SDLWindowException.ThrowIfLessThan(SDL_GetWindowDisplayMode(_handle, out var mode), 0);
            return mode;
        }
        set
        {
            ThrowIfDisposed();
            if (value is SDL_DisplayMode mode)
            {
                SDLWindowException.ThrowIfLessThan(SDL_SetWindowDisplayMode(_handle, ref mode), 0);
                return;
            }
            SDLWindowException.ThrowIfLessThan(SDL_SetWindowDisplayMode(_handle, IntPtr.Zero), 0);
        }
    }

    private FullscreenMode _fs;

    /// <summary>
    /// Gets or sets this Window as fullscreen with the passed flags. get: Stored in memory on the C# side, may not be representative of SDL's actual state; set: <see cref="SDL_SetWindowFullscreen" href="https://wiki.libsdl.org/SDL_SetWindowFullscreen"/>
    /// </summary>
    public FullscreenMode FullscreenMode
    {
        get => _fs;
        set
        {
            ThrowIfDisposed();
            SDLWindowException.ThrowIfLessThan(SDL_SetWindowFullscreen(_handle, (uint)(_fs = value)), 0);
        }
    }

    /// <summary>
    /// Sets this window's gamma ramp. <see cref="SDL_SetWindowGammaRamp" href="https://wiki.libsdl.org/SDL_SetWindowGammaRamp"/>
    /// </summary>
    /// <param name="red">The gamma translation table for the red channel. Should contain 256 16-bit quantities</param>
    /// <param name="green">The gamma translation table for the green channel. Should contain 256 16-bit quantities</param>
    /// <param name="blue">The gamma translation table for the blue channel. Should contain 256 16-bit quantities</param>
    /// <remarks>
    /// Set the gamma translation table for the red, green, and blue channels of the video hardware. Each table is an array of 256 16-bit quantities, representing a mapping between the input and output for that channel. The input is the index into the array, and the output is the 16-bit gamma value at that index, scaled to the output color precision.
    /// </remarks>
    public void SetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowGammaRamp(_handle, red, green, blue), 0);
    }

    /// <summary>
    /// Gets or sets whether this window is grabbed. get: <see cref="SDL_GetWindowGrab" href="https://wiki.libsdl.org/SDL_GetWindowGrab"/>; set: <see cref="SDL_SetWindowGrab" href="https://wiki.libsdl.org/SDL_SetWindowGrab"/>
    /// </summary>
    /// <remarks>
    /// When input is grabbed, the mouse is confined to the window. This function will also grab the keyboard if SDL_HINT_GRAB_KEYBOARD is set. To grab the keyboard without also grabbing the mouse, use SDL_SetWindowKeyboardGrab().
    /// </remarks>
    public bool Grab
    {
        get => SDL_GetWindowGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Gets or sets whether this window is grabbing the keyboard. get: <see cref="SDL_GetWindowKeyboardGrab" href="https://wiki.libsdl.org/SDL_GetWindowKeyboardGrab"/>; set: <see cref="SDL_SetWindowKeyboardGrab" href="https://wiki.libsdl.org/SDL_SetWindowKeyboardGrab"/>
    /// </summary>
    /// <remarks>
    /// Keyboard grab enables capture of system keyboard shortcuts like Alt+Tab or the Meta/Super key. Note that not all system keyboard shortcuts can be captured by applications (one example is Ctrl+Alt+Del on Windows).
    /// </remarks>
    public bool KeyboardGrab
    {
        get => SDL_GetWindowKeyboardGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowKeyboardGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Gets or sets whether this <see cref="Window"/> is grabbing the mouse. get: <see cref="SDL_GetWindowMouseGrab" href="https://wiki.libsdl.org/SDL_GetWindowMouseGrab"/>; set: <see cref="SDL_SetWindowMouseGrab" href="https://wiki.libsdl.org/SDL_SetWindowMouseGrab"/>
    /// </summary>
    /// <remarks>
    /// Mouse grab confines the mouse cursor to the window.
    /// </remarks>
    public bool MouseGrab
    {
        get => SDL_GetWindowMouseGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowMouseGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Sets this <see cref="Window"/>'s icon. <see cref="SDL_SetWindowIcon" href="https://wiki.libsdl.org/SDL_SetWindowIcon"/>
    /// </summary>
    /// <param name="icon"></param>
    public void SetIcon(Image icon)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(icon); // Rather than throwing if null in a property with a nullable type; is it possible to remove a Window's Icon by setting it to IntPtr.Zero?
        SDL_SetWindowIcon(_handle, icon._handle);
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s maximum size. get: <see cref="SDL_GetWindowMaximumSize" href="https://wiki.libsdl.org/SDL_GetWindowMaximumSize"/>; set: <see cref="SDL_SetWindowMaximumSize" href="https://wiki.libsdl.org/SDL_SetWindowMaximumSize"/>
    /// </summary>
    public Size MaximumSize
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowMaximumSize(_handle, out var w, out var h);
            return new(w, h);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowMaximumSize(_handle, value.Width, value.Height);
        }
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s minimum size. get: <see cref="SDL_GetWindowMinimumSize" href="https://wiki.libsdl.org/SDL_GetWindowMinimumSize"/>; set: <see cref="SDL_SetWindowMinimumSize" href="https://wiki.libsdl.org/SDL_SetWindowMinimumSize"/>
    /// </summary>
    public Size MinimumSize
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowMinimumSize(_handle, out var w, out var h);
            return new(w, h);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowMinimumSize(_handle, value.Width, value.Height);
        }
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s actual size. get: <see cref="SDL_GetWindowMinimumSize" href="https://wiki.libsdl.org/SDL_GetWindowMinimumSize"/>; set: <see cref="SDL_SetWindowMinimumSize" href="https://wiki.libsdl.org/SDL_SetWindowMinimumSize"/>
    /// </summary>
    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowSize(_handle, out var w, out var h);
            return new(w, h);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowSize(_handle, value.Width, value.Height);
        }
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s position in the screen. get: <see cref="SDL_GetWindowPosition" href="https://wiki.libsdl.org/SDL_GetWindowPosition"/> set: <see cref="SDL_SetWindowPosition" href="https://wiki.libsdl.org/SDL_SetWindowPosition"/>
    /// </summary>
    /// <remarks>
    /// The <see cref="Window"/>'s coordinate origin is the upper left of the <see cref="Display"/>.
    /// </remarks>
    public Point Position
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowPosition(_handle, out var x, out var y);
            return new(x, y);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowPosition(_handle, value.X, value.Y);
        }
    }

    /// <summary>
    /// Gets the <see cref="Window"/>'s border sizes. <see cref="SDL_GetWindowBordersSize" href="https://wiki.libsdl.org/SDL_GetWindowBordersSize"/>
    /// </summary>
    /// <remarks>
    /// This function may fail on systems where the window has not yet been decorated by the display server (for example, immediately after instantiating a <see cref="new"/> <see cref="Window"/>). It is recommended that you wait at least until the window has been presented and composited, so that the window system has a chance to decorate the window and provide the border dimensions to SDL.
    /// </remarks>
    /// <param name="top">The size of the top border</param>
    /// <param name="left">The size of the left border</param>
    /// <param name="bottom">The size of the bottom border</param>
    /// <param name="right">The size of the right border</param>
    public void GetBorderSize(out int top, out int left, out int bottom, out int right)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_GetWindowBordersSize(_handle, out top, out left, out bottom, out right), 0);
    }

    /// <summary>
    /// Configures several settings for this <see cref="Window"/>. Calls several <see cref="SDL"/> functions at once.
    /// </summary>
    /// <param name="hasBorder">Whether the window is borderless or not. <see cref="SDL_SetWindowBordered" href="https://wiki.libsdl.org/SDL_SetWindowBordered"/></param>
    /// <param name="alwaysOnTop">Whether the window should remain always on top. <see cref="SDL_SetWindowAlwaysOnTop" href="https://wiki.libsdl.org/SDL_SetWindowAlwaysOnTop"/></param>
    /// <param name="isResizable">Whether the window is resizable. <see cref="SDL_SetWindowResizable" href="https://wiki.libsdl.org/SDL_SetWindowResizable"/></param>
    public void Configure(bool hasBorder, bool alwaysOnTop, bool isResizable)
    {
        ThrowIfDisposed();
        SDL_SetWindowBordered(_handle, hasBorder ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        SDL_SetWindowAlwaysOnTop(_handle, alwaysOnTop ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        SDL_SetWindowResizable(_handle, isResizable ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Updates the <see cref="Window"/>'s surface by copying the <see cref="Window"/> surface to the screen. <see cref="SDL_UpdateWindowSurface" href="https://wiki.libsdl.org/SDL_UpdateWindowSurface"/>
    /// </summary>
    /// <remarks>
    /// This is the method you use to reflect any changes to the surface on the screen.
    /// </remarks>
    public void UpdateSurface()
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_UpdateWindowSurface(_handle), 0);
    }

    /// <summary>
    /// Copy areas of the window surface to the screen. <see cref="SDL_UpdateWindowSurfaceRects" href="https://wiki.libsdl.org/SDL_UpdateWindowSurfaceRects"/>
    /// </summary>
    /// <param name="rectangles">The areas of the window's surface to copy to the screen</param>
    /// <param name="numrect">The amount of rectangles to update, or null to use <paramref name="rectangles"/>'s length</param>
    /// <remarks>
    /// This is the function you use to reflect changes to portions of the surface on the screen.
    /// </remarks>
    public void UpdateSurfaceRects(Span<Rectangle> rectangles, int? numrect)
    {
        ThrowIfDisposed();
        Span<SDL_Rect> rects = stackalloc SDL_Rect[numrect ?? rectangles.Length];
        for (int i = 0; i < rects.Length; i++)
            rectangles[i].ToSDLRect(ref rects[i]);
        SDLWindowException.ThrowIfLessThan(SDL_UpdateWindowSurfaceRects(_handle, rects, rects.Length), 0);
    }

    private UserData? hitTestCallbackData;
    private HitTestCallback? hitTestCallback;
    private readonly bool _hitTestSupported;
    /// <summary>
    /// Provide a callback that decides if a window region has special properties. <see cref="SDL_SetWindowHitTest" href="https://wiki.libsdl.org/SDL_SetWindowHitTest"/>
    /// </summary>
    /// <param name="callback">The callback to assign to this <see cref="Window"/></param>
    /// <param name="userData">User data to be held by this window, and passed to the callback when called. This has no effect on the SDL side of things (and is in fact stored in the .NET heap for the purposes of this library) and is intended to provide special, and even identification data for a given <see cref="Window"/></param>
    /// <remarks>
    /// Your callback may fire at any time, and its firing does not indicate any specific behavior (for example, on Windows, this certainly might fire when the OS is deciding whether to drag your window, but it fires for lots of other reasons, too, some unrelated to anything you probably care about and when the mouse isn't actually at the location it is testing). Since this can fire at any time, you should try to keep your callback efficient, devoid of allocations, etc.
    /// </remarks>
    /// <exception cref="PlatformNotSupportedException">Thrown if the current platform does not support assigning a HitTestCallback to a <see cref="Window"/>. See <see cref="IsHitTestSupported"/></exception>
    public void SetHitTestCallback(HitTestCallback? callback, UserData? userData)
    {
        ThrowIfDisposed();
        if (!_hitTestSupported)
            throw new PlatformNotSupportedException($"Assigning a HitTestCallback to a Window is not supported on this platform.");
        hitTestCallback = callback;
        hitTestCallbackData = userData;
    }

    /// <summary>
    /// <see cref="true"/> if setting a HitTestCallback is supported in this platform. <see cref="false"/> otherwise.
    /// </summary>
    /// <remarks>
    /// This is tested inside .NET as SDL does not provide a way to test this. However, for the purposes of this library, a <see cref="Window"/>'s constructor will always assign a callback to an <see cref="SDL"/>'s window, and manage .NET callbacks from that. If that assignment fails, it'll be assumed to be unsupported.
    /// </remarks>
    public bool IsHitTestSupported => _hitTestSupported;

    /// <summary>
    /// Request a <see cref="Window"/> to demand attention from the user. <see cref="SDL_FlashWindow" href="https://wiki.libsdl.org/SDL_FlashWindow"/>
    /// </summary>
    /// <param name="operation">The operation to request for the <see cref="Window"/></param>
    public void Flash(SDL_FlashOperation operation)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_FlashWindow(_handle, operation), 0);
    }

    /// <summary>
    /// A barrier inside the window, defining an area the grabbed mouse is bound in. This does NOT grab the mouse, and only works when the window has mouse focus.
    /// </summary>
    public Rectangle? MouseRectangle
    {
        get
        {
            ThrowIfDisposed();
            var ptr = SDL_GetWindowMouseRect(_handle);
            return ptr == IntPtr.Zero ? null : Marshal.PtrToStructure<SDL_Rect>(ptr);
        }
        set
        {
            ThrowIfDisposed();
            if (value is null)
            {
                SDLWindowException.ThrowIfLessThan(SDL_SetWindowMouseRect(_handle, IntPtr.Zero), 0);
                return;
            }

            SDL_Rect r = default;
            ((Rectangle)value).ToSDLRect(ref r);
            SDLWindowException.ThrowIfLessThan(SDL_SetWindowMouseRect(_handle, ref r), 0);
        }
    }

    /// <summary>
    /// Get the <see cref="Window"/> that currently has an input grab enabled. <see cref="SDL_GetGrabbedWindow" href="https://wiki.libsdl.org/SDL_GetGrabbedWindow"/>
    /// </summary>
    /// <returns>Returns the <see cref="Window"/> if input is grabbed or <see cref="null"/> otherwise.</returns>
    /// <exception cref="SDLWindowException"></exception>
    public Window? GetGrabbedWindow()
    {
        ThrowIfDisposed();
        var ptr = SDL_GetGrabbedWindow();
        return ptr == IntPtr.Zero
            ? null
            : _handleDict.TryGetValue(ptr, out var wr)
            ? wr.TryGetTarget(out var window) ?
            window
            : throw new SDLWindowException("This window object has already been garbage collected and disposed")
            : throw new SDLWindowException("Could not match the returned pointer to a window object. Did you instantiate this Window directly through SDL?");
    }

    /// <summary>
    /// Represents a method set as a callback for a <see cref="Window"/>'s hit test
    /// </summary>
    /// <param name="window">The <see cref="Window"/> object this callback refers to</param>
    /// <param name="area">The area the <see cref="Window"/> was hit</param>
    /// <param name="data">User defined data held by the <see cref="Window"/></param>
    /// <returns>The appropriate user selected HitTestResult</returns>
    /// <remarks>See <see cref="SetHitTestCallback(HitTestCallback?, UserData?)"/></remarks>
    public delegate HitTestResult HitTestCallback(Window window, Point area, UserData? data);

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_DestroyWindow(_handle);
            _handleDict.TryRemove(_handle, out _);
            disposedValue = true;
        }
    }

    ~Window()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Window));
    }

    #endregion
}
