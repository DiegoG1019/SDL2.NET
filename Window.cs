using SDL2.NET.Exceptions;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SDL2.SDL;

namespace SDL2.NET;

public class Window : IDisposable
{
    internal static readonly ConcurrentDictionary<IntPtr, WeakReference<Window>> _handleDict = new();
    protected internal readonly IntPtr _handle = IntPtr.Zero;

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
            SDLWindowException.ThrowIfLessThan(SDL_SetWindowOpacity(_handle, value), 0);
        }
    }

    public float GetBrightness()
    {
        ThrowIfDisposed();
        return SDL_GetWindowBrightness(_handle);
    }

    public void SetAsModalFor(Window parent)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowModalFor(_handle, parent._handle), 0);
    }

    public void SetInputFocus()
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowInputFocus(_handle), 0);
    }

    public int DisplayIndex
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowDisplayIndex(_handle);
        }
    }

    public void GetDisplayMode(out SDL_DisplayMode mode)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_GetWindowDisplayMode(_handle, out mode), 0);
    }

    public void SetDisplayMode(ref SDL_DisplayMode mode)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowDisplayMode(_handle, ref mode), 0);
    }

    public void SetFullscreen(uint flags)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowFullscreen(_handle, flags), 0);
    }

    public void SetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowGammaRamp(_handle, red, green, blue), 0);
    }

    public bool Grab
    {
        get => SDL_GetWindowGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    public bool KeyboardGrab
    {
        get => SDL_GetWindowKeyboardGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowKeyboardGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    public bool MouseGrab
    {
        get => SDL_GetWindowMouseGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowMouseGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    private Image? _icon;
    public Image? Icon
    {
        get
        {
            ThrowIfDisposed();
            return _icon;
        }

        set
        {
            ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(value); // Rather than throwing if null in a property with a nullable type; is it possible to remove a Window's Icon by setting it to IntPtr.Zero?
            SDL_SetWindowIcon(_handle, value._handle);
            _icon = value;
        }
    }

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

    public void GetBorderSize(out int top, out int left, out int bottom, out int right)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_GetWindowBordersSize(_handle, out top, out left, out bottom, out right), 0);
    }

    public void Show()
    {
        ThrowIfDisposed();
        SDL_ShowWindow(_handle);
    }

    public void Configure(bool hasBorder, bool alwaysOnTop, bool isResizable)
    {
        ThrowIfDisposed();
        SDL_SetWindowBordered(_handle, hasBorder ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        SDL_SetWindowAlwaysOnTop(_handle, alwaysOnTop ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        SDL_SetWindowResizable(_handle, isResizable ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    public void UpdateSurface()
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_UpdateWindowSurface(_handle), 0);
    }

    public void UpdateWindowSurfaceRects(Span<Rectangle> rectangles, int? numrect)
    {
        ThrowIfDisposed();
        Span<SDL_Rect> rects = stackalloc SDL_Rect[numrect ?? rectangles.Length];
        for (int i = 0; i < rects.Length; i++)
            rectangles[i].ToSDLRect(ref rects[i]);
        SDLWindowException.ThrowIfLessThan(SDL_UpdateWindowSurfaceRects(_handle, rects, rects.Length), 0);
    }

#warning I'm not sure what to do about callbackData
    public void SetHitTestCallback(SDL_HitTest callback, IntPtr callbackData)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowHitTest(_handle, callback, callbackData), 0);
    }

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

    public Window GetGrabbedWindow()
    {
        ThrowIfDisposed();
        var ptr = SDL_GetGrabbedWindow();
        return ptr == IntPtr.Zero
            ? throw new SDLWindowException(SDL_GetError())
            : _handleDict.TryGetValue(ptr, out var wr)
            ? wr.TryGetTarget(out var window) ?
            window
            : throw new SDLWindowException("This window object has already been garbage collected and disposed")
            : throw new SDLWindowException("Could not match the returned pointer to a window object. Did you instantiate this Window outside of this class?");
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
    }

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
            throw new ObjectDisposedException(nameof(SDLApplication));
    }

    #endregion
}