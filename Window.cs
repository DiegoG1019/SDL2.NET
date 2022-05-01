using SDL2.NET.Exceptions;
using System.Runtime.CompilerServices;
using static SDL2.SDL;

namespace SDL2.NET;

public class Window : IDisposable
{
    internal readonly IntPtr _handle = IntPtr.Zero;

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

    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowSize(_handle, out var width, out var height);
            return new(width, height);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowSize(_handle, value.Width, value.Height);
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

    ///Max Size

#error Not Implemented

    public Window(string title, int width, int height, SDL_WindowFlags flags = SDL_WindowFlags.SDL_WINDOW_RESIZABLE, int? centerPointX = null, int? centerPointY = null)
    {
        _handle = SDL_CreateWindow(
            "SDL2Example",
            centerPointX ?? SDL_WINDOWPOS_CENTERED,
            centerPointY ?? SDL_WINDOWPOS_CENTERED,
            width,
            height,
            flags
        );
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_DestroyWindow(_handle);
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
            throw new ObjectDisposedException(nameof(Application));
    }

    #endregion
}