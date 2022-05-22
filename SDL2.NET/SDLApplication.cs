using SDL2.Bindings;
using SDL2.NET.Exceptions;
using SDL2.NET.SDLMixer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
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

    #region Events

    /// <summary>
    /// Represents a standard event fired by the application
    /// </summary>
    /// <param name="application">The application object</param>
    public delegate void SDLApplicationEvent(SDLApplication application);

    /// <summary>
    /// Fired when the render targets have been reset and their contents need to be updated
    /// </summary>
    public event SDLApplicationEvent? RenderTargetsReset;

    /// <summary>
    /// Fired when the render device has been reset and all textures need to be recreated 
    /// </summary>
    public event SDLApplicationEvent? RenderDeviceReset;

    /// <summary>
    /// Fired when the SDL application is terminating
    /// </summary>
    /// <remarks>Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public event SDLApplicationEvent? Terminating;

    /// <summary>
    /// Fired when SDL detects that the process is running out of memory
    /// </summary>
    /// <remarks>Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public event SDLApplicationEvent? LowMemory;

    /// <summary>
    /// Fired when the application is about to go into the background
    /// </summary>
    /// <remarks>Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public event SDLApplicationEvent? WillEnterBackground;

    /// <summary>
    /// Fired when the application is about to enter the foreground
    /// </summary>
    /// <remarks>Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public event SDLApplicationEvent? WillEnterForeground;

    /// <summary>
    /// Fired when the application entered the foreground
    /// </summary>
    /// <remarks>Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public event SDLApplicationEvent? DidEnterForeground;

    /// <summary>
    /// Fired when the application entered into the background
    /// </summary>
    /// <remarks>Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public event SDLApplicationEvent? DidEnterBackground;

    /// <summary>
    /// Fired when the locale of the app changed
    /// </summary>
    public event SDLApplicationEvent? LocaleChanged;

    /// <summary>
    /// Fired when the SDLApplication is about to quit
    /// </summary>
    public event SDLApplicationEvent? Quitting;

    /// <summary>
    /// Fired when the key mapping changed, such as when a new keyboard was plugged in, or the language of the keyboard changed
    /// </summary>
    public event SDLApplicationEvent? KeyMapChanged;

    internal void TriggerLocaleChanged() => LocaleChanged?.Invoke(this);

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal void TriggerTerminating() => Terminating?.Invoke(this);

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal void TriggerLowMemory() => LowMemory?.Invoke(this);

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal void TriggerWillEnterBackground() => WillEnterBackground?.Invoke(this);

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal void TriggerWillEnterForeground() => WillEnterForeground?.Invoke(this);

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal void TriggerDidEnterForeground() => DidEnterForeground?.Invoke(this);

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal void TriggerDidEnterBackground() => DidEnterBackground?.Invoke(this);
    internal void TriggerSDLQuitting() => Quitting?.Invoke(this);
    internal void TriggerKeyMapChanged() => KeyMapChanged?.Invoke(this);
    internal void TriggerRenderTargetsReset() => RenderTargetsReset?.Invoke(this);
    internal void TriggerRenderDeviceReset() => RenderDeviceReset?.Invoke(this);

    #endregion

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

    #region Input
    
    public InputEvents InputEvent { get; } = new();

    public class InputEvents
    {

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

    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(SDLApplication));
    }

    #endregion

    protected virtual Renderer InstantiateMainRenderer(RendererFlags flags)
        => new WindowRenderer(MainWindow, flags, - 1);

    protected virtual Window InsantiateMainWindow(string title, int width, int height)
        => new(title, width, height);
}
