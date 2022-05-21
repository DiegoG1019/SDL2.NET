using SDL2.Bindings;
using SDL2.NET.Exceptions;
using SDL2.NET.SDLMixer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;

public class SDLApplication : IDisposable
{
    private Window? _mw;
    private Renderer? _mr;
    public Window MainWindow => _mw ?? throw new InvalidOperationException("This application's window has not been launched");
    public Renderer MainRenderer => _mr ?? throw new InvalidOperationException("This application's window has not been launched");

    private SDLApplication() { }

    public static SDLApplication App { get; } = new();

    #region Initialization

    public SDLApplication InitializeVideo()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL.SDL_Init(SDL.SDL_INIT_VIDEO), 0);
        return this;
    }

    public SDLApplication InitializeAudio()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL.SDL_Init(SDL.SDL_INIT_AUDIO), 0);
        return this;
    }

    public SDLApplication OpenAudioMixer(MixerInitFlags flags, int frequency = 44100, int channels = 2, int chunksize = 2048, ushort? format = null)
    {
        ThrowIfDisposed();
        AudioMixer.InitAudioMixer(flags);
        AudioMixer.OpenAudioMixer(frequency, channels, chunksize, format);
        return this;
    }

    public SDLApplication InitializeTTF()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL_ttf.TTF_Init(), 0);
        return this;
    }

    public SDLApplication LaunchWindow(string title, int width, int height, RendererFlags rendererFlags = RendererFlags.Accelerated | RendererFlags.PresentVSync)
    {
        ThrowIfDisposed();
        if (_mw is not null)
            throw new InvalidOperationException("A Main Window for this app has already been launched. Try SetMainWindow instead");
        _mw = InsantiateMainWindow(title, width, height);
        _mr = InstantiateMainRenderer(rendererFlags);
        return this;
    }

    public void SetMainWindow(WindowRenderer renderer)
    {
        _mw = renderer.AttachedWindow;
        _mr = renderer;
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
                if (AudioMixer.IsInitialized)
                    AudioMixer.Quit();
            }

            disposedValue = true;
            SDL.SDL_Quit();
        }
    }

    ~SDLApplication()
    {
        Dispose(disposing: false);
    }

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
            throw new ObjectDisposedException(nameof(SDLApplication));
    }

    protected virtual Renderer InstantiateMainRenderer(RendererFlags flags)
        => new WindowRenderer(MainWindow, flags, - 1);

    protected virtual Window InsantiateMainWindow(string title, int width, int height)
        => new(title, width, height);
}
