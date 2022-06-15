﻿using Microsoft.VisualBasic;
using SDL2.NET.HintTypes;
using SDL2.NET.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// SDL Library hints
/// </summary>
public static class Hints
{
    /*
    public const string SDL_HINT_RENDER_DRIVER = "SDL_RENDER_DRIVER";
    public const string SDL_HINT_ALLOW_TOPMOST = "SDL_ALLOW_TOPMOST";
    
    public const string SDL_HINT_CTRL_CLICK_EMULATE_RIGHT_CLICK = "SDL_CTRL_CLICK_EMULATE_RIGHT_CLICK";
    public const string SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION = "SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION";
    public const string SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION = "SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION";

    public const string SDL_HINT_AUDIO_RESAMPLING_MODE = "SDL_AUDIO_RESAMPLING_MODE";
    public const string SDL_HINT_RENDER_LOGICAL_SIZE_MODE = "SDL_RENDER_LOGICAL_SIZE_MODE";
    public const string SDL_HINT_MOUSE_NORMAL_SPEED_SCALE = "SDL_MOUSE_NORMAL_SPEED_SCALE";
    public const string SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
    public const string SDL_HINT_TOUCH_MOUSE_EVENTS = "SDL_TOUCH_MOUSE_EVENTS";
    public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON = "SDL_WINDOWS_INTRESOURCE_ICON";
    public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";

    public const string SDL_HINT_IOS_HIDE_HOME_INDICATOR = "SDL_IOS_HIDE_HOME_INDICATOR";
    public const string SDL_HINT_TV_REMOTE_AS_JOYSTICK = "SDL_TV_REMOTE_AS_JOYSTICK";
    public const string SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";

    public const string SDL_HINT_MOUSE_DOUBLE_CLICK_TIME = "SDL_MOUSE_DOUBLE_CLICK_TIME";
    public const string SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
    public const string SDL_HINT_JOYSTICK_HIDAPI = "SDL_JOYSTICK_HIDAPI";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS4 = "SDL_JOYSTICK_HIDAPI_PS4";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";
    public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM = "SDL_JOYSTICK_HIDAPI_STEAM";
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH = "SDL_JOYSTICK_HIDAPI_SWITCH";
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX = "SDL_JOYSTICK_HIDAPI_XBOX";
    public const string SDL_HINT_ENABLE_STEAM_CONTROLLERS = "SDL_ENABLE_STEAM_CONTROLLERS";
    public const string SDL_HINT_ANDROID_TRAP_BACK_BUTTON = "SDL_ANDROID_TRAP_BACK_BUTTON";

    public const string SDL_HINT_MOUSE_TOUCH_EVENTS = "SDL_MOUSE_TOUCH_EVENTS";
    public const string SDL_HINT_GAMECONTROLLERCONFIG_FILE = "SDL_GAMECONTROLLERCONFIG_FILE";
    public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE = "SDL_ANDROID_BLOCK_ON_PAUSE";
    public const string SDL_HINT_RENDER_BATCHING = "SDL_RENDER_BATCHING";
    public const string SDL_HINT_EVENT_LOGGING = "SDL_EVENT_LOGGING";
    public const string SDL_HINT_WAVE_RIFF_CHUNK_SIZE = "SDL_WAVE_RIFF_CHUNK_SIZE";
    public const string SDL_HINT_WAVE_TRUNCATION = "SDL_WAVE_TRUNCATION";
    public const string SDL_HINT_WAVE_FACT_CHUNK = "SDL_WAVE_FACT_CHUNK";

    public const string SDL_HINT_VIDO_X11_WINDOW_VISUALID = "SDL_VIDEO_X11_WINDOW_VISUALID";
    public const string SDL_HINT_GAMECONTROLLER_USE_BUTTON_LABELS = "SDL_GAMECONTROLLER_USE_BUTTON_LABELS";
    public const string SDL_HINT_VIDEO_EXTERNAL_CONTEXT = "SDL_VIDEO_EXTERNAL_CONTEXT";
    public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
    public const string SDL_HINT_DISPLAY_USABLE_BOUNDS = "SDL_DISPLAY_USABLE_BOUNDS";
    public const string SDL_HINT_VIDEO_X11_FORCE_EGL = "SDL_VIDEO_X11_FORCE_EGL";
    public const string SDL_HINT_GAMECONTROLLERTYPE = "SDL_GAMECONTROLLERTYPE";

    public const string SDL_HINT_JOYSTICK_HIDAPI_CORRELATE_XINPUT = "SDL_JOYSTICK_HIDAPI_CORRELATE_XINPUT"; 
    public const string SDL_HINT_JOYSTICK_RAWINPUT = "SDL_JOYSTICK_RAWINPUT";
    public const string SDL_HINT_AUDIO_DEVICE_APP_NAME = "SDL_AUDIO_DEVICE_APP_NAME";
    public const string SDL_HINT_AUDIO_DEVICE_STREAM_NAME = "SDL_AUDIO_DEVICE_STREAM_NAME";
    public const string SDL_HINT_PREFERRED_LOCALES = "SDL_PREFERRED_LOCALES";
    public const string SDL_HINT_THREAD_PRIORITY_POLICY = "SDL_THREAD_PRIORITY_POLICY";
    public const string SDL_HINT_LINUX_JOYSTICK_DEADZONES = "SDL_LINUX_JOYSTICK_DEADZONES";
    public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO = "SDL_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5 = "SDL_JOYSTICK_HIDAPI_PS5";
    public const string SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";
    public const string SDL_HINT_JOYSTICK_THREAD = "SDL_JOYSTICK_THREAD";
    public const string SDL_HINT_AUTO_UPDATE_JOYSTICKS = "SDL_AUTO_UPDATE_JOYSTICKS";
    public const string SDL_HINT_AUTO_UPDATE_SENSORS = "SDL_AUTO_UPDATE_SENSORS";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS5_RUMBLE";

    public const string SDL_HINT_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS = "SDL_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS";
    public const string SDL_HINT_WINDOWS_FORCE_SEMAPHORE_KERNEL = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
    public const string SDL_HINT_WINDOWS_USE_D3D9EX = "SDL_WINDOWS_USE_D3D9EX";
    public const string SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
    public const string SDL_HINT_JOYSTICK_HIDAPI_STADIA = "SDL_JOYSTICK_HIDAPI_STADIA";
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
    public const string SDL_HINT_ALLOW_ALT_TAB_WHILE_GRABBED = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";
    public const string SDL_HINT_KMSDRM_REQUIRE_DRM_MASTER = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
    public const string SDL_HINT_AUDIO_DEVICE_STREAM_ROLE = "SDL_AUDIO_DEVICE_STREAM_ROLE";
    public const string SDL_HINT_X11_FORCE_OVERRIDE_REDIRECT = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
    public const string SDL_HINT_JOYSTICK_HIDAPI_LUNA = "SDL_JOYSTICK_HIDAPI_LUNA";
    public const string SDL_HINT_JOYSTICK_RAWINPUT_CORRELATE_XINPUT = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
    public const string SDL_HINT_AUDIO_INCLUDE_MONITORS = "SDL_AUDIO_INCLUDE_MONITORS";
    public const string SDL_HINT_VIDEO_WAYLAND_ALLOW_LIBDECOR = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";

    public const string SDL_HINT_VIDEO_EGL_ALLOW_TRANSPARENCY = "SDL_VIDEO_EGL_ALLOW_TRANSPARENCY";
    public const string SDL_HINT_APP_NAME = "SDL_APP_NAME";
    public const string SDL_HINT_SCREENSAVER_INHIBIT_ACTIVITY_NAME = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";
    public const string SDL_HINT_WINDOW_NO_ACTIVATION_WHEN_SHOWN = "SDL_WINDOW_NO_ACTIVATION_WHEN_SHOWN";
    public const string SDL_HINT_POLL_SENTINEL = "SDL_POLL_SENTINEL";
    public const string SDL_HINT_JOYSTICK_DEVICE = "SDL_JOYSTICK_DEVICE";
    public const string SDL_HINT_LINUX_JOYSTICK_CLASSIC = "SDL_LINUX_JOYSTICK_CLASSIC";

    public const string SDL_HINT_RENDER_LINE_METHOD = "SDL_RENDER_LINE_METHOD";

    */ // Missing Hints

    /// <summary>
    /// A hint that specifies whether SDL should not name threads on Microsoft Windows.
    /// </summary>
    /// <remarks>
    /// By default SDL will name threads on Microsoft Windows. If the hint is not set then SDL will raise the 0x406D1388 Exception to name threads. This is the default behavior of SDL. If the hint is set then SDL will not raise this exception, and threads will be unnamed. For .NET languages this is required when running under a debugger.
    /// </remarks>
    [SupportedOSPlatform("Windows")]
    public static BinaryHint DisableThreadNaming { get; } = new(SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, false);

    /// <summary>
    /// A hint that specifies whether the Android / iOS built-in accelerometer should be listed as a joystick device, rather than listing actual joysticks only. <see cref="SDL_HINT_ACCELEROMETER_AS_JOYSTICK" href="https://wiki.libsdl.org/SDL_HINT_ACCELEROMETER_AS_JOYSTICK"/>
    /// </summary>
    /// <remarks>
    /// By default SDL will list real joysticks along with the accelerometer as if it were a 3 axis joystick.
    /// </remarks>
    public static BinaryHint AccelerometerAsJoystick { get; } = new(SDL_HINT_ACCELEROMETER_AS_JOYSTICK, true);

#warning come back for SDL_RWFromFile
    /// <summary>
    /// A hint that specifies the Android APK expansion file version. <see cref="SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION" href="https://wiki.libsdl.org/SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION"/>; <see cref="SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION" href="https://wiki.libsdl.org/SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION"/>
    /// </summary>
    /// <remarks>
    /// If both hints were set then SDL_RWFromFile() will look into expansion files after a given relative path was not found in the internal storage and assets. By default this hint is not set and the APK expansion files are not searched.
    /// </remarks>
    public static AndroidAPKExpansionFileVersion AndroidAPKExpansionFileVersion { get; } = new();

    /// <summary>
    /// A hint that specifies a variable to control whether mouse and touch events are to be treated together or separately. <see cref="SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH" href="https://wiki.libsdl.org/SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH"/>
    /// </summary>
    /// <remarks>
    /// The value of this hint is used at runtime, so it can be changed at any time. By default mouse events will be handled as touch events and touch will raise fake mouse events.
    /// </remarks>
    public static BinaryHint AndroidSeparateMouseAndTouch { get; } = new(SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH, true);

    /// <summary>
    /// A hint that specifies whether controllers used with the Apple TV generate UI events. <see cref="SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS" href="https://wiki.libsdl.org/SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS"/>
    /// </summary>
    /// <remarks>
    /// When UI events are generated by controller input, the app will be backgrounded when the Apple TV remote's menu button is pressed, and when the pause or B buttons on gamepads are pressed.
    /// </remarks>
    public static BinaryHint AppleTVControllerUIEvents { get; } = new(SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS, false);

    /// <summary>
    /// A hint that specifies whether the Apple TV remote's joystick axes will automatically match the rotation of the remote. <see cref="SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION" href="https://wiki.libsdl.org/SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION"/>
    /// </summary>
    /// <remarks>
    /// When UI events are generated by controller input, the app will be backgrounded when the Apple TV remote's menu button is pressed, and when the pause or B buttons on gamepads are pressed.
    /// </remarks>
    public static BinaryHint AppleTVRemoteAllowRotation { get; } = new(SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION, true);

    /// <summary>
    /// A hint that specifies whether SDL should not use version 4 of the bitmap header when saving BMPs .<see cref="SDL_HINT_BMP_SAVE_LEGACY_FORMAT" href="https://wiki.libsdl.org/SDL_HINT_BMP_SAVE_LEGACY_FORMAT"/>
    /// </summary>
    /// <remarks>
    /// The bitmap header version 4 is required for proper alpha channel support and SDL will use it when required. Should this not be desired, this hint can force the use of the 40 byte header version which is supported everywhere.
    /// </remarks>
    public static BinaryHint BMPSaveLegacyFormat { get; } = new(SDL_HINT_BMP_SAVE_LEGACY_FORMAT, false);

    /// <summary>
    /// A hint that specifies if SDL should give back control to the browser automatically when running with asyncify. <see cref="SDL_HINT_EMSCRIPTEN_ASYNCIFY" href="https://wiki.libsdl.org/SDL_HINT_EMSCRIPTEN_ASYNCIFY"/>
    /// </summary>
    /// <remarks>
    /// This hint only applies to the Emscripten platform.
    /// </remarks>
    public static BinaryHint EmscriptenAsyncify { get; } = new(SDL_HINT_EMSCRIPTEN_ASYNCIFY, false);

    /// <summary>
    /// A hint that specifies a value to override the binding element for keyboard inputs for Emscripten builds.
    /// </summary>
    /// <remarks>
    /// This hint only applies to the Emscripten platform.
    /// </remarks>
    public static EmscriptenKeyboardElement EmscriptenKeyboardElement { get; } = new();

    /// <summary>
    /// A hint that specifies how 3D acceleration is used with SDL_GetWindowSurface(). <see cref="SDL_HINT_FRAMEBUFFER_ACCELERATION" href="https://wiki.libsdl.org/SDL_HINT_FRAMEBUFFER_ACCELERATION"/>
    /// </summary>
    /// <remarks>SDL can try to accelerate the screen surface returned by SDL_GetWindowSurface() by using streaming textures with a 3D rendering engine. This variable controls whether and how this is done.</remarks>
    public static FramebufferAcceleration FramebufferAcceleration { get; } = new();

    /// <summary>
    /// A hint that specifies scaling quality. <see cref="SDL_HINT_RENDER_SCALE_QUALITY" href="https://wiki.libsdl.org/SDL_HINT_RENDER_SCALE_QUALITY"/>
    /// </summary>
    /// <remarks>This hint is checked when a texture is created and it affects scaling when copying that texture.</remarks>
    public static RenderScaleQuality RenderScaleQuality { get; } = new();

    /// <summary>
    /// A hint that specifies whether relative motion is affected by renderer scaling. <see cref="SDL_HINT_MOUSE_RELATIVE_SCALING" href="https://wiki.libsdl.org/SDL_HINT_MOUSE_RELATIVE_SCALING"/>
    /// </summary>
    /// <remarks>
    /// By default relative mouse deltas are affected by DPI and renderer scaling
    /// </remarks>
    public static BinaryHint MouseRelativeScaling { get; } = new(SDL_HINT_MOUSE_RELATIVE_SCALING, false);

    public static BinaryHint IMEShowUI { get; } = new(SDL_HINT_IME_SHOW_UI, true);

    /// <summary>
    /// A hint that specifies whether the screensaver is enabled by default. <see cref="SDL_HINT_VIDEO_ALLOW_SCREENSAVER"/>
    /// </summary>
    public static BinaryHint VideoAllowScreenSaver { get; } = new(SDL_HINT_VIDEO_ALLOW_SCREENSAVER, true);

    /// <summary>
    /// A hint that specifies extra gamecontroller db entries
    /// </summary>
    /// <remarks>
    /// By default no extra gamecontroller db entries are specified. This hint must be set before calling <see cref="SDLApplication.InitializeGameController"/>. You can update mappings after the system is initialized with <see cref="GameController.AddMapping(string)"/>
    /// </remarks>
    public static GameControllerConfigHint GameControllerConfig { get; } = new();

    /// <summary>
    /// A hint that specifies whether grabbing input grabs the keyboard.
    /// </summary>
    /// <remarks>
    /// By default SDL will not grab the keyboard so system shortcuts still work.
    /// </remarks>
    public static BinaryHint GrabKeyboard { get; } = new(SDL_HINT_GRAB_KEYBOARD, false);

    /// <summary>
    /// A hint that specifies a variable controlling whether the idle timer is disabled on iOS.
    /// </summary>
    /// <remarks>When an iOS application does not receive touches for some time, the screen is dimmed automatically. For games where the accelerometer is the only input this is problematic. This functionality can be disabled by setting this hint.</remarks>
    [SupportedOSPlatform("iOS")]
    public static BinaryHint IdleTimerDisabled { get; } = new(SDL_HINT_IDLE_TIMER_DISABLED, false);

    /// <summary>
    /// A hint that specifies whether certain IMEs should handle text editing internally instead of firing <see cref="Window.TextEditing"/> events.
    /// </summary>
    /// <remarks>
    /// By default <see cref="Window.TextEditing"/> events are sent, and it is the application's responsibility to render the text from these events and differentiate it somehow from committed text.
    /// </remarks>
    public static BinaryHint IMEInternalEditing { get; } = new(SDL_HINT_IME_INTERNAL_EDITING, false);

    /// <summary>
    /// A hint that specifies if <see cref="Joystick"/> (and <see cref="GameController"/>) events are enabled even when the application is in the background.
    /// </summary>
    /// <remarks>
    /// By default <see cref="Joystick"/> (and <see cref="GameController"/>) events are not enabled when the application is in the background. This hint can be set at any time.
    /// </remarks>
    public static BinaryHint JoystickAllowBackgroundEvents { get; } = new(SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS, false);

    /// <summary>
    /// A hint that specifies if the SDL app should not be forced to become a foreground process on Mac OS X.
    /// </summary>
    /// <remarks>
    /// By default the SDL app will be forced to become a foreground process on Mac OS X.
    /// </remarks>
    [SupportedOSPlatform("OSX")]
    public static BinaryHint MacBackgroundApp { get; } = new(SDL_HINT_MAC_BACKGROUND_APP, false);

    /// <summary>
    /// A hint that specifies whether ctrl+click should generate a right-click event on Mac.
    /// </summary>
    /// <remarks>
    /// By default holding ctrl while left clicking will not generate a right click event when on Mac.
    /// </remarks>
    [SupportedOSPlatform("OSX")]
    public static BinaryHint MacCtrlClickEmulateRightClick { get; } = new(SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK, false);

    /// <summary>
    /// A hint that specifies if mouse click events are sent when clicking to focus an SDL window.
    /// </summary>
    /// <remarks>
    /// By default no mouse click events are sent when clicking to focus.
    /// </remarks>
    public static BinaryHint MouseFocusClickthrough { get; } = new(SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH, true);

    /// <summary>
    /// A hint that specifies whether relative mouse mode is implemented using mouse warping.
    /// </summary>
    /// <remarks>
    /// By default SDL will use raw input for relative mouse mode
    /// </remarks>
    public static BinaryHint MouseRelativeModeWarp { get; } = new(SDL_HINT_MOUSE_RELATIVE_MODE_WARP, false);

    /// <summary>
    /// A hint that specifies not to catch the SIGINT or SIGTERM signals.
    /// </summary>
    /// <remarks>
    /// By default install a SIGINT and SIGTERM handler, and when it catches a signal, convert it into an <see cref="SDLApplication.Quitting"/> event.
    /// </remarks>
    [SupportedOSPlatform("Linux"), SupportedOSPlatform("FreeBSD")]
    public static BinaryHint NoSignalHandlers { get; } = new(SDL_HINT_NO_SIGNAL_HANDLERS, false);

    /// <summary>
    /// A hint that specifies a variable controlling which orientations are allowed on iOS.
    /// </summary>
    /// <remarks>By default all orientations are allowed.</remarks>
    public static HintOrientations Orientations { get; } = new();

    /// <summary>
    /// A hint that specifies a variable controlling whether to enable Direct3D 11+'s Debug Layer.
    /// </summary>
    /// <remarks>
    /// By default SDL does not use Direct3D Debug Layer. This variable does not have any effect on the Direct3D 9 based renderer.
    /// </remarks>
    public static BinaryHint RenderDirect3D11Debug { get; } = new(SDL_HINT_RENDER_DIRECT3D11_DEBUG, false);

    /// <summary>
    /// A hint that specifies whether the Direct3D device is initialized for thread-safe operations. 
    /// </summary>
    /// <remarks>
    /// By default the Direct3D device is created with thread-safety disabled. With thread-safety disabled, the function calls are faster
    /// </remarks>
    public static BinaryHint RenderDirect3DThreadsafe { get; } = new(SDL_HINT_RENDER_DIRECT3D_THREADSAFE, false);

    /// <summary>
    /// A hint that specifies which render driver to use.
    /// </summary>
    /// <remarks>
    /// By default the first one in the list that is available on the current platform is chosen. If the application doesn't pick a specific renderer to use, this variable specifies the name of the preferred renderer. If the preferred renderer can't be initialized, the normal default renderer is used.
    /// </remarks>
    public static RenderDriverHint RenderDriver { get; } = new();

    /// <summary>
    /// A hint that specifies whether the OpenGL render driver uses shaders.
    /// </summary>
    /// <remarks>By default shaders are used if OpenGL supports them.</remarks>
    public static BinaryHint RenderOpenGLShaders { get; } = new(SDL_HINT_RENDER_OPENGL_SHADERS, true);

    /// <summary>
    /// A hint that specifies whether sync to vertical refresh is enabled or disabled in <see cref="Renderer.Renderer"/> to avoid tearing.
    /// </summary>
    public static BinaryHint RenderVsync { get; } = new(SDL_HINT_RENDER_VSYNC, false);

    /// <summary>
    /// A hint that specifies which Dispmanx layer SDL should use on a Raspberry PI.
    /// </summary>
    /// <remarks>
    /// By default the Dispmanx layer is "10000". This is also known as Z-order. The variable can take a negative or positive value.
    /// </remarks>
    public static RPIVideoLayerHint RPIVideoLayer { get; } = new();

    /// <summary>
    /// A hint that specifies a variable specifying SDL's threads stack size in bytes or "0" for the backend's default size. This hint has NO EFFECT on .NET
    /// </summary>
    /// <remarks>
    /// By default the backend's default threads stack size is used. Support for this Hint is currently available in the pthread; Windows, and PSP backend. Use this hint in case you need to set SDL's threads stack size to other than the default. This is specially useful if you build SDL against a non glibc libc library (such as musl) which provides a relatively small default thread stack size (a few kilobytes versus the default 8 MB glibc uses).
    /// </remarks>
    [SupportedOSPlatform("Windows"), SupportedOSPlatform("PSP")]
    public static ThreadStackSizeHint ThreadStackSize { get; } = new();

    /// <summary>
    /// A hint that specifies the timer resolution in milliseconds.
    /// </summary>
    /// <remarks>
    /// The higher resolution the timer, the more frequently the CPU services timer interrupts, and the more precise delays are, but this takes up power and CPU time. This hint is only used on Windows, but not supported on WinRT. If this variable is set to "0", the system timer resolution is not set. This hint may be set at any time. See this blog post for more information: <see href="http://randomascii.wordpress.com/2013/07/08/windows-timer-resolution-megawatts-wasted/"/>
    /// </remarks>
    [SupportedOSPlatform("Windows")]
    public static TimerResolutionHint TimerResolution { get; } = new();

    /// <summary>
    /// A hint that specifies if high-DPI windows ("Retina" on Mac and iOS) are not allowed.
    /// </summary>
    /// <remarks>
    /// By default high-DPI windows ("Retina" on Mac and iOS) are allowed. On Apple's OS X you must set the NSHighResolutionCapable Info.plist property to YES, otherwise you will not receive a High DPI OpenGL display.
    /// </remarks>
    public static BinaryHint VideoHighDPIDisabled { get; } = new(SDL_HINT_VIDEO_HIGHDPI_DISABLED, true);

    /// <summary>
    /// A hint that dictates policy for fullscreen Spaces on Mac OS X. If enabled, <see cref="WindowFlags.FullscreenDesktop"/> will use them and <see cref="WindowFlags.Resizable"/> windows will offer the "FullScreen" button on their titlebars
    /// </summary>
    /// <remarks>By default Spaces support is enabled. Spaces are disabled regardless of this hint if the OS isn't at least Mac OS X Lion (10.7). This hint must be set before any windows are created.</remarks>
    [SupportedOSPlatform("OSX")]
    public static BinaryHint VideoMacFullScreenSpaces { get; } = new(SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES, true);

    /// <summary>
    /// A hint that specifies if a <see cref="Window"/> is minimized if it loses key focus when in fullscreen mode.
    /// </summary>
    /// <remarks>
    /// By default a <see cref="Window"/> is minimized if it loses key focus when in fullscreen mode.
    /// </remarks>
    public static BinaryHint VideoMinimizeOnFocusLoss { get; } = new(SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS, true);

    /// <summary>
    /// A hint that specifies which shader compiler to preload when using the Chrome ANGLE binaries.
    /// </summary>
    /// <remarks>
    /// By default <see cref="D3DCompiler.D3DCompiler46"/> will be used. SDL has EGL and OpenGL ES2 support on Windows via the ANGLE project. It can use two different sets of binaries, those compiled by the user from source or those provided by the Chrome browser. In the later case, these binaries require that SDL loads a DLL providing the shader compiler.
    /// </remarks>
    public static D3DCompilerHint VideoWinD3DCompiler { get; } = new();

    /// <summary>
    /// A hint that specifies the address of another <see cref="Window"/> that new <see cref="Window"/>s should share pixel format data with, when created with <see cref="SDL2.Bindings.SDL.SDL_CreateWindowFrom"/>
    /// </summary>
    /// <remarks>
    /// By default this hint is not set. For more info, see <see href="https://wiki.libsdl.org/SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT"/>
    /// </remarks>
    public static VideoWindowSharePixelFormatHint VideoWindowSharePixelFormat { get; } = new();

    /// <summary>
    /// A hint that specifies whether the X11 _NET_WM_PING protocol should be supported.
    /// </summary>
    /// <remarks>
    /// By default SDL will use _NET_WM_PING, but for applications that know they will not always be able to respond to ping requests in a timely manner they can turn it off to avoid the window manager thinking the app is hung.
    /// </remarks>
    public static BinaryHint VideoX11NetWmPing { get; } = new(SDL_HINT_VIDEO_X11_NET_WM_PING, true);

    /// <summary>
    /// A hint that specifies whether the X11 Xinerama extension should be used.
    /// </summary>
    /// <remarks>
    /// By default SDL will use Xinerama if it is available.
    /// </remarks>
    public static BinaryHint VideoX11Xinerama { get; } = new(SDL_HINT_VIDEO_X11_XINERAMA, true);

    /// <summary>
    /// A hint that specifies whether the X11 XRandR extension should be used.
    /// </summary>
    /// <remarks>
    /// By default SDL will not use XRandR because of window manager issues. For details about the referenced window manager issues see the following message (and its thread) on the SDL mailing list: <see href="http://lists.libsdl.org/pipermail/sdl-libsdl.org/2012-October/651919.html"/>
    /// </remarks>
    public static BinaryHint VideoX11XRandR { get; } = new(SDL_HINT_VIDEO_X11_XRANDR, false);

    /// <summary>
    /// A hint that specifies whether the X11 !VidMode extension should be used.
    /// </summary>
    /// <remarks>
    /// By default SDL will use XVidMode if it is available.
    /// </remarks>
    public static BinaryHint VideoX11Xvidmode { get; } = new(SDL_HINT_VIDEO_X11_XVIDMODE, true);

    /// <summary>
    /// A hint that specifies whether the window frame and title bar are interactive when the cursor is hidden.
    /// </summary>
    /// <remarks>
    /// By default SDL will allow interaction with the window frame when the cursor is hidden.
    /// </remarks>
    public static BinaryHint WindowFrameUsableWhileCursorHidden { get; } = new(SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN, true);

    /// <summary>
    /// A hint that specifies whether the windows message loop is processed by SDL.
    /// </summary>
    /// <remarks>
    /// By default SDL will process the windows message loop.
    /// </remarks>
    [SupportedOSPlatform("Windows")]
    public static BinaryHint WindowsEnableMessageloop { get; } = new(SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP, true);

    /// <summary>
    /// A hint that specifies that SDL should not to fire <see cref="Window.Closed"/> events for Alt+F4 on Microsoft Windows.
    /// </summary>
    /// <remarks>
    /// By default SDL will generate an <see cref="Window.Closed"/> event for Alt+F4.
    /// </remarks>
    [SupportedOSPlatform("Windows")]
    public static BinaryHint WindowsNoCloseOnAltF4 { get; } = new(SDL_HINT_WINDOWS_NO_CLOSE_ON_ALT_F4, false);

    /// <summary>
    /// A hint that specifies a variable to allow back-button-press events on Windows Phone to be marked as handled.
    /// </summary>
    /// <remarks>
    /// By default this hint is not set and the application will be terminated. See <see href="https://wiki.libsdl.org/SDL_HINT_WINRT_HANDLE_BACK_BUTTON"/> for more info
    /// </remarks>
    [SupportedOSPlatform("Windows")]
    public static BinaryHint WinRTHandleBackButton { get; } = new(SDL_HINT_WINRT_HANDLE_BACK_BUTTON, false);

    /// <summary>
    /// Represents two hints that can be used to customize and set WinRT's mandatory privacy policy configurations
    /// </summary>
    [SupportedOSPlatform("Windows")]
    public static WinRTPrivacyPolicyHint WinRTPrivacyPolicy { get; } = new();

    /// <summary>
    /// A hint that specifies if Xinput gamepad devices are detected. Xinput is limited to 4 gamepads, if you want more than four you need to set it to false.
    /// </summary>
    /// <remarks>
    /// By default Xinput gamepad devices are detected.
    /// </remarks>
    [SupportedOSPlatform("Windows")]
    public static BinaryHint XinputEnabled { get; } = new(SDL_HINT_XINPUT_ENABLED, true);

    /// <summary>
    /// A hint that specifies that SDL should use the old axis and button mapping for XInput devices.
    /// </summary>
    /// <remarks>
    /// By default SDL does not use the old axis and button mapping for XInput devices. This hint is for backwards compatibility only and will be removed in SDL 2.1. This hint must be set before <see cref="SDLApplication.Initialize"/>
    /// </remarks>
    [SupportedOSPlatform("Windows")]
    public static BinaryHint XinputUseOldJoystickMapping { get; } = new(SDL_HINT_XINPUT_USE_OLD_JOYSTICK_MAPPING, false);
}
