using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;
public class Application : IDisposable
{
#error Not Implemented

    private static bool _hasOne = false;
    private static readonly object _lock = new();

    private Window? _mw;
    private Renderer? _mr;
    public Window MainWindow => _mw ?? throw new InvalidOperationException("This application's window has not been launched");
    public Renderer MainRenderer => _mr ?? throw new InvalidOperationException("This application's window has not been launched");

    private Application() { }

    public Application App { get; } = new();

    public Application LaunchWindow(string title, int width, int height)
    {
        ThrowIfDisposed()
        _mw = InsantiateMainWindow(title, width, height);
        _mr = InstantiateMainRenderer();
        return this;
    }

    #region Initialization

    public Application InitializeVideo()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL.SDL_Init(SDL.SDL_INIT_VIDEO), 0);
        return this;
    }

    public Application InitializeAudio()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL.SDL_Init(SDL.SDL_INIT_AUDIO), 0);
        return this;
    }

    public Application OpenAudioMixer(int frequency = 44100, int channels = 2, int chunksize = 2048, ushort? format = null)
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfEquals(SDL_mixer.Mix_OpenAudio(frequency, format ?? SDL_mixer.MIX_DEFAULT_FORMAT, channels, chunksize), -1);
        return this;
    }

    public Application InitializeTTF()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL_ttf.TTF_Init(), 0);
        return this;
    }

    #endregion

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                MainRenderer.Dispose();
                MainWindow.Dispose();
            }

            disposedValue = true;
            SDL.SDL_Quit();
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Application()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion


    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Application));
    }

    protected virtual Renderer InstantiateMainRenderer()
        => new(MainWindow, -1);

    protected virtual Window InsantiateMainWindow(string title, int width, int height)
        => new(title, width, height);
}
