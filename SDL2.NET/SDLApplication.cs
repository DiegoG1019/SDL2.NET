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
using System.Drawing;
using static SDL2.Bindings.SDL;
using System.Reflection.PortableExecutable;

namespace SDL2.NET;

/// <summary>
/// Represents an SDL Application's basic functionality, such as events and timing related functions. For all intents and purposes, see <see cref="SDLApplication{TApp}"/> instead
/// </summary>
/// <remarks>This class' constructor is <c>internal</c> and thus this class cannot be inherited directly</remarks>
public abstract class SDLApplication
{
    internal SDLApplication() { }

    #region Events

    /// <summary>
    /// Represents a standard event fired by the application
    /// </summary>
    public delegate void SDLApplicationEvent();

    /// <summary>
    /// Fired when the render targets have been reset and their contents need to be updated
    /// </summary>
    public static event SDLApplicationEvent? RenderTargetsReset;

    /// <summary>
    /// Fired when the render device has been reset and all textures need to be recreated 
    /// </summary>
    public static event SDLApplicationEvent? RenderDeviceReset;

    /// <summary>
    /// Fired when the SDL application is terminating
    /// </summary>
    /// <remarks>May run on a separate thread. The OS will terminate the app shortly after this event is called, finish your work quickly. Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public static event SDLApplicationEvent? Terminating;

    /// <summary>
    /// Fired when SDL detects that the process is running out of memory
    /// </summary>
    /// <remarks>May run on a separate thread. The Program may run out of memory shortly after this event is called, finish your work quickly and don't make allocations. Only supported on: Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public static event SDLApplicationEvent? LowMemory;

    /// <summary>
    /// Fired when the application is about to go into the background
    /// </summary>
    /// <remarks>May run on a separate thread. The OS will effectuate the action shortly after this event is called, finish your work quickly. Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public static event SDLApplicationEvent? WillEnterBackground;

    /// <summary>
    /// Fired when the application is about to enter the foreground
    /// </summary>
    /// <remarks>May run on a separate thread. The OS will effectuate the action shortly after this event is called, finish your work quickly. Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public static event SDLApplicationEvent? WillEnterForeground;

    /// <summary>
    /// Fired when the application entered the foreground
    /// </summary>
    /// <remarks>May run on a separate thread. Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public static event SDLApplicationEvent? DidEnterForeground;

    /// <summary>
    /// Fired when the application entered into the background
    /// </summary>
    /// <remarks>May run on a separate thread. Only supported on: Windows, Android and iOS</remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    public static event SDLApplicationEvent? DidEnterBackground;

    /// <summary>
    /// Fired when the locale of the app changed
    /// </summary>
    public static event SDLApplicationEvent? LocaleChanged;

    /// <summary>
    /// Fired when the SDLApplication is about to quit
    /// </summary>
    public static event SDLApplicationEvent? Quitting;

    /// <summary>
    /// Fired when the key mapping changed, such as when a new keyboard was plugged in, or the language of the keyboard changed
    /// </summary>
    public static event SDLApplicationEvent? KeyMapChanged;

    internal static void TriggerLocaleChanged() => LocaleChanged?.Invoke();

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal static void TriggerTerminating() => Terminating?.Invoke();

    [SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal static void TriggerLowMemory() => LowMemory?.Invoke();

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal static void TriggerWillEnterBackground() => WillEnterBackground?.Invoke();

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal static void TriggerWillEnterForeground() => WillEnterForeground?.Invoke();

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal static void TriggerDidEnterForeground() => DidEnterForeground?.Invoke();

    [SupportedOSPlatform("Windows"), SupportedOSPlatform("Android"), SupportedOSPlatform("iOS")]
    internal static void TriggerDidEnterBackground() => DidEnterBackground?.Invoke();
    internal static void TriggerSDLQuitting() => Quitting?.Invoke();
    internal static void TriggerKeyMapChanged() => KeyMapChanged?.Invoke();
    internal static void TriggerRenderTargetsReset() => RenderTargetsReset?.Invoke();
    internal static void TriggerRenderDeviceReset() => RenderDeviceReset?.Invoke();

    #endregion

    /// <summary>
    /// The total amount of time that has elapsed since SDL was initialized
    /// </summary>
    public static TimeSpan TotalTime => TimeSpan.FromMilliseconds(SDL_GetTicks64());

    /// <summary>
    /// Blocks the current thread for <paramref name="time"/> amount of time
    /// </summary>
    /// <param name="time">The amount of time to block the current thread</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void Delay(TimeSpan time)
    {
        var x = time.TotalMilliseconds;
        if (x <= 0)
            throw new ArgumentOutOfRangeException(nameof(time), time, "time must be larger than 0");
        SDL_Delay(x >= uint.MaxValue ? uint.MaxValue : (uint)x);
    }

    /// <summary>
    /// Fetches and reacts to SDL's events
    /// </summary>
    public static void UpdateEvents()
    {
        Events.Update();
    }

    /// <summary>
    /// Fetches and reacts to a single one of SDL's events, if available
    /// </summary>
    /// <returns>The remaining events in SDL's queue</returns>
    public static int UpdateEventOnce()
    {
        var i = Events.UpdateOnce();
        return i;
    }

    /// <summary>
    /// The default Message box scheme for this Application
    /// </summary>
    /// <remarks>If null, falls back to system default</remarks>
    public static MessageBoxColorScheme? MessageBoxColorScheme { get; set; }

    /// <summary>
    /// Display a simple modal message box. Not to be confused with <see cref="Window.ShowMessageBox(string, string, MessageBoxFlags, MessageBoxButton[], MessageBoxColorScheme?)"/>
    /// </summary>
    /// <param name="title">The title of the message box</param>
    /// <param name="message">The message to be shown in the messagebox</param>
    /// <param name="flags">The flags of the message box</param>
    /// <param name="buttons">An array of buttons to display in the Message Box</param>
    /// <param name="scheme">The color scheme of the Message Box. Leave null to use <see cref="MessageBoxColorScheme"/></param>
    /// <returns>The button that was pressed, or null if none was pressed</returns>
    public static MessageBoxButton? ShowMessageBox(string title, string message, MessageBoxFlags flags, MessageBoxButton[] buttons, MessageBoxColorScheme? scheme = null)
    {
        var butts = new SDL_MessageBoxButtonData[buttons.Length];
        for (int i = 0; i < butts.Length; i++)
            butts[i] = buttons[i].ToSDL(i);

        SDL_MessageBoxData dat = new()
        {
            flags = (SDL_MessageBoxFlags)flags,
            numbuttons = buttons.Length,
            message = message,
            title = title,
            buttons = butts,
            colorScheme = scheme?.ToSDL() ?? MessageBoxColorScheme?.ToSDL()
        };

        SDLWindowException.ThrowIfLessThan(SDL_ShowMessageBox(ref dat, out int buttonPressed), 0);
        return buttonPressed is -1 ? null : buttons[buttonPressed];
    }

    /// <summary>
    /// Display a simple modal message box. Not to be confused with <see cref="Window.ShowMessageBox(string, string, MessageBoxFlags)"/>
    /// </summary>
    /// <param name="title">The title of the message box</param>
    /// <param name="message">The message to be shown in the messagebox</param>
    /// <param name="flags">The flags of the message box</param>
    /// <remarks>This method may be called at any time, even before Initialization. This makes it useful for reporting errors like a failure to create a renderer or OpenGL context.</remarks>
    public static void ShowMessageBox(string title, string message, MessageBoxFlags flags)
    {
        SDLApplicationException.ThrowIfLessThan(SDL_ShowSimpleMessageBox((SDL_MessageBoxFlags)flags, title, message, IntPtr.Zero), 0);
    }

    /// <summary>
    /// Open a URL/URI in the browser or other appropriate external application. A successful result does not mean the URL loaded, just that SDL launched /something/ to handle it, or that, at least, SDL believes it did
    /// </summary>
    /// <param name="url">A valid URL/URI to open. You may use an URI such as <c>file:///full/path/to/file</c> to open a local file, if supported</param>
    /// <remarks>
    /// Open a URL in a separate, system-provided application. How this works will vary wildly depending on the platform. This will likely launch what makes sense to handle a specific URL's protocol, but it might also be able to launch file managers for directories and other things. What happens when you open a URL varies wildly as well: your game window may lose focus (and may or may not lose focus if your game was fullscreen or grabbing input at the time). On mobile devices, your app will likely move to the background or your process might be paused. Any given platform may or may not handle a given URL. Test this method on each of your target platforms to identify expected results
    /// </remarks>
    public static void OpenURL(string url)
    {
        SDLApplicationException.ThrowIfLessThan(SDL_OpenURL(url), 0);
    }
}

/// <summary>
/// Represents an SDL Application, and is the root of this library
/// </summary>
/// <remarks>Initialize the app by calling the Initialization methods (all starting with initialize, code-completion should help you) you want, and finally <see cref="LaunchWindow"/>. Then, in your main loop, you should also regularly call <see cref="UpdateEvents"/>. The static constructor of this class takes care of basic setup, like disabling thread naming</remarks>
public abstract class SDLApplication<TApp> : SDLApplication, IDisposable where TApp : SDLApplication<TApp>
{
    private Window? _mw;
    private Renderer? _mr;

    /// <summary>
    /// The current Main Window of the application.
    /// </summary>
    public Window MainWindow => _mw ?? throw new InvalidOperationException("This application's window has not been launched");

    /// <summary>
    /// The current Main Renderer of the application.
    /// </summary>
    public Renderer MainRenderer => _mr ?? throw new InvalidOperationException("This application's window has not been launched");

    private static readonly object sync = new();

    /// <summary>
    /// Instances the SDLApplication
    /// </summary>
    /// <remarks>
    /// This class can only be instanced once, and will throw if multiple instances are attempted, even if attempted outside <see cref="Instance"/> or <see cref="Instance{TApp}"/> which should be avoided. If you want to override the behaviours of this class, feel free to inherit it and use <see cref="Instance{TApp}"/>. Otherwise, simply use <see cref="Instance"/>
    /// </remarks>
    protected SDLApplication() 
    {
        lock (sync)
        {
            if (SDLAppBuilder.AppInstance is null)
            {
                SDLAppBuilder.AppInstance = this;
                return;
            }
            throw new InvalidOperationException("SDLApplication, or any of its sub-types can only be instanced once.");
        }
    }

    static SDLApplication()
    {
        if (OperatingSystem.IsWindows())
            Hints.DisableThreadNaming.IsEnabled = true;
    }

    private static TApp? inst;
    /// <summary>
    /// Fetches the instance of <see cref="SDLApplication{TApp}"/>, or instances a new one if not available
    /// </summary>
    /// <returns>The singleton instance of <see cref="SDLApplication{TApp}"/></returns>
    public static TApp Instance 
        => inst ??= ((SDLAppBuilder.AppInstance ?? throw new InvalidOperationException("SDLApplication has not been instanced, see SDLAppBuilder")) as TApp ?? throw new InvalidOperationException($"The instanced SDLApplication is not of type {typeof(TApp).FullName}"));

    #region Initialization

    private TApp Initialize(SDLSubSystem sys)
    {
        ThrowIfDisposed();
        if ((InitializedSystems & sys) > 0)
            return Instance;
        SDLInitializationException.ThrowIfLessThan(SDL_Init((uint)sys), 0);
        return Instance;
    }

    /// <summary>
    /// Initializes SDL's Video Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeVideo()
        => Initialize(SDLSubSystem.Video);

    /// <summary>
    /// Initializes SDL's Audio Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeAudio()
        => Initialize(SDLSubSystem.Audio);

    /// <summary>
    /// Initializes SDL's Timer Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeTimer()
        => Initialize(SDLSubSystem.Timer);

    /// <summary>
    /// Initializes SDL's Joystick Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeJoystick()
        => Initialize(SDLSubSystem.Joystick);

    /// <summary>
    /// Initializes SDL's Haptic Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeHaptic()
        => Initialize(SDLSubSystem.Haptic);

    /// <summary>
    /// Initializes SDL's GameController Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeGameController()
        => Initialize(SDLSubSystem.GameController);

    /// <summary>
    /// Initializes SDL's Events Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeEvents()
        => Initialize(SDLSubSystem.Events);

    /// <summary>
    /// Initializes SDL's Sensor Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeSensor()
        => Initialize(SDLSubSystem.Sensor);

    /// <summary>
    /// Quits the passed SDL Libraries
    /// </summary>
    /// <param name="subSystem">An enum mask describing the libraries to quit</param>
    public void Quit(SDLSubSystem subSystem)
        => SDL_QuitSubSystem((uint)subSystem);

    /// <summary>
    /// Get a mask of the specified subsystems which are currently initialized.
    /// </summary>
    public SDLSubSystem InitializedSystems
        => (SDLSubSystem)SDL_WasInit(0);

    /// <summary>
    /// Initializes SDL's Audio Mixing Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeAndOpenAudioMixer(MixerInitFlags flags, int frequency = 44100, int channels = 2, int chunksize = 2048, ushort? format = null)
    {
        ThrowIfDisposed();
        AudioMixer.InitAudioMixer(flags);
        AudioMixer.OpenAudioMixer(frequency, channels, chunksize, format);
        return Instance;
    }

    /// <summary>
    /// Initializes SDL's TrueTypeFont Library
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp InitializeTTF()
    {
        ThrowIfDisposed();
        SDLInitializationException.ThrowIfLessThan(SDL_ttf.TTF_Init(), 0);
        return Instance;
    }

    /// <summary>
    /// Instantiates and Launches both <see cref="MainRenderer"/> and <see cref="MainWindow"/>, should be called last
    /// </summary>
    /// <returns>This instance of the SDLApplication for chaining calls</returns>
    public TApp LaunchWindow(string title, int width, int height, WindowConfig? windowConfig = null, RendererFlags rendererFlags = RendererFlags.Accelerated | RendererFlags.PresentVSync)
    {
        ThrowIfDisposed();
        if (_mw is not null)
            throw new InvalidOperationException("A Main Window for this app has already been launched. Try SetMainWindow instead");
        _mw = InstantiateMainWindow(title, width, height, windowConfig);
        _mr = InstantiateMainRenderer(_mw, rendererFlags);

        _mw.Closed += _mw_Closed;

        return Instance;
    }

    private void _mw_Closed(Window sender, TimeSpan timestamp)
    {
        sender.Closed -= _mw_Closed;
        _mw = null;
        _mr = null;
    }

    /// <summary>
    /// Replaces this app's <see cref="MainWindow"/> and <see cref="MainRenderer"/>
    /// </summary>
    /// <param name="renderer">The <see cref="Renderer"/> to replace <see cref="MainRenderer"/> with, which also contains the <see cref="Window"/> to replace <see cref="MainWindow"/> with</param>
    /// <param name="disposePrevious">Whether or not to dispose of the replaced <see cref="Window"/> or <see cref="Renderer"/></param>
    /// <remarks>Since a <see cref="WindowRenderer"/> always belongs to one specific <see cref="Window"/>, there's no parameter to replace the <see cref="Window"/>. This method checks if <see cref="MainWindow"/> needs to be replaced (for example, it may be the same <see cref="Window"/>), and if it doesn't, it won't try to dispose of it or update events</remarks>
    public void SetMainWindow(WindowRenderer renderer, bool disposePrevious)
    {
        if (_mr is null || _mw is null)
            throw new InvalidOperationException("A Main Window for this app has not been launched. Try LaunchMainWindow instead. Consider overriding InstantiateMainRenderer and InstantiateMainWindow");

        var prevr = _mr;
        _mr = renderer;

        if (ReferenceEquals(_mw, renderer.AttachedWindow))
        {
            if (disposePrevious)
                prevr?.Dispose();
            return;
        }

        var prevw = _mw;
        _mw = renderer.AttachedWindow;
        if (disposePrevious)
        {
            prevr?.Dispose();
            prevw?.Dispose();
        }
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
                _mr?.Dispose();
                _mw?.Dispose();
                if (AudioMixer.IsInitialized)
                    AudioMixer.Quit();
            }

            disposedValue = true;
            SDL_Quit();
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
            throw new ObjectDisposedException(nameof(SDLApplication<TApp>));
    }

    #endregion

    /// <summary>
    /// The procedure that instantiates <see cref="MainRenderer"/>
    /// </summary>
    /// <param name="mainWindow">The Main <see cref="Window"/></param>
    /// <param name="flags">The preferred flags of the <see cref="Renderer"/></param>
    /// <returns>The instantiated <see cref="Renderer"/></returns>
    protected virtual Renderer InstantiateMainRenderer(Window mainWindow, RendererFlags flags)
        => new WindowRenderer(mainWindow, flags, - 1);

    /// <summary>
    /// The procedure that instantiates <see cref="MainWindow"/>
    /// </summary>
    /// <param name="title">The preferred title of the <see cref="Window"/></param>
    /// <param name="width">The preferred width of the <see cref="Window"/></param>
    /// <param name="height">The preferred height of the <see cref="Window"/></param>
    /// <param name="configuration">The preferred configuration of the <see cref="Window"/></param>
    /// <returns>The instantiated <see cref="Window"/></returns>
    protected virtual Window InstantiateMainWindow(string title, int width, int height, WindowConfig? configuration)
        => new(title, width, height, configuration);
}
