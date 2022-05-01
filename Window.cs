using SDL2.NET.Exceptions;
using System.Runtime.CompilerServices;

namespace SDL2.NET;

public class Window : IDisposable
{
    internal readonly IntPtr _handle = IntPtr.Zero;

    public string Title
    {
        get => SDL.SDL_GetWindowTitle(_handle);
        set => SDL.SDL_SetWindowTitle(_handle, value);
    }

    public float Opacity
    {
        get
        {
            SDLWindowException.ThrowIfLessThan(SDL.SDL_GetWindowOpacity(_handle, out var opacity), 0);
            return opacity;
        }
        set => SDLWindowException.ThrowIfLessThan(SDL.SDL_SetWindowOpacity(_handle, value), 0);
    }

    public void GetWindowSize(out int width, out int height)
        => SDL.SDL_GetWindowSize(_handle, out width, out height);

    public void SetWindowSize(int width, int height)
        => SDL.SDL_SetWindowSize(_handle, width, height);

    public float GetBrightness()
        => SDL.SDL_GetWindowBrightness(_handle);

    public void SetAsModalFor(Window parent)
        => SDLWindowException.ThrowIfLessThan(SDL.SDL_SetWindowModalFor(_handle, parent._handle), 0);

#error Not Implemented

    public Window(string title, int width, int height, SDL.SDL_WindowFlags flags = SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE, int? centerPointX = null, int? centerPointY = null)
    {
        _handle = SDL.SDL_CreateWindow(
            "SDL2Example",
            centerPointX ?? SDL.SDL_WINDOWPOS_CENTERED,
            centerPointY ?? SDL.SDL_WINDOWPOS_CENTERED,
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
            SDL.SDL_DestroyWindow(_handle);
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

    #endregion
}