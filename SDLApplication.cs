using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;
public class SDLApplication : IDisposable
{
    private static bool _hasOne = false;
    private static readonly object _lock = new();

    protected ISDLLogger Logger = DefaultSDLLogger.Default;
    protected ISDLLogContext LogContext = SDLAppLogContext.Instance;

    private Window? _mw;
    private Renderer? _mr;
    public Window MainWindow => _mw ?? throw new InvalidOperationException("This application's window has not been launched");
    public Renderer MainRenderer => _mr ?? throw new InvalidOperationException("This application's window has not been launched");

    private SDLApplication() { }

    public static SDLApplication App { get; } = new();

    #region Initialization

    public SDLApplication SetLogger(ISDLLogger logger)
    {
        ThrowIfDisposed();
        Logger = logger;
        return this;
    }

    public SDLApplication InitializeVideo()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL.SDL_Init(SDL.SDL_INIT_VIDEO), 0);
        Logger.Debug(LogContext, "Initialized SDL2 Video");
        return this;
    }

    public SDLApplication InitializeAudio()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL.SDL_Init(SDL.SDL_INIT_AUDIO), 0);
        Logger.Debug(LogContext, "Initialized SDL2 Audio");
        return this;
    }

    public SDLApplication OpenAudioMixer(int frequency = 44100, int channels = 2, int chunksize = 2048, ushort? format = null)
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfEquals(SDL_mixer.Mix_OpenAudio(frequency, format ?? SDL_mixer.MIX_DEFAULT_FORMAT, channels, chunksize), -1);
        Logger.Debug(LogContext, "Initialized SDL2 Audio Mixer: Frequency: {0}; Format: {1}, Channels: {2}; Chunk Size: {3}", frequency, format, channels, chunksize);
        return this;
    }

    public SDLApplication InitializeTTF()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL_ttf.TTF_Init(), 0);
        Logger.Debug(LogContext, "Initialized SDL2 TTF");
        return this;
    }
    public SDLApplication LaunchWindow(string title, int width, int height)
    {
        ThrowIfDisposed();
        _mw = InsantiateMainWindow(title, width, height);
        Logger.Debug(LogContext, "Instantiated main SDL2 Window");
        _mr = InstantiateMainRenderer();
        Logger.Debug(LogContext, "Instantiated main SDL2 Renderer");

        Logger.Information(LogContext, "Launched Main SDL2 Window");
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
            throw new ObjectDisposedException(nameof(SDLApplication));
    }

    protected virtual Renderer InstantiateMainRenderer()
        => new WindowRenderer(MainWindow, -1);

    protected virtual Window InsantiateMainWindow(string title, int width, int height)
        => new(title, width, height);

    private sealed class SDLAppLogContext : ISDLLogContext
    {
        private SDLAppLogContext() { }
        public static SDLAppLogContext Instance { get; } = new();
        public string FormatContext() => nameof(SDLApplication);
    }
}
