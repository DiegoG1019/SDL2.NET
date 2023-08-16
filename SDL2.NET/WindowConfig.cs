using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Provides a means to easily configure a Window.
/// </summary>
/// <remarks>
/// This class is useful only for instancing a new Window. For post-init configuration, see the <see cref="Window"/>'s properties
/// </remarks>
public sealed class WindowConfig
{
    /// <summary>
    /// The default Window configuration. Feel free to modify this.
    /// </summary>
    /// <remarks>Changes will take effect only on newly created <see cref="Window"/>s</remarks>
    public static WindowConfig Default
    {
        get => _def;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _def = value;
        }
    }
    private static WindowConfig _def = new WindowConfig().Resizable(true);

    #region Properties

    /// <summary>
    /// The full screen mode the <see cref="Window"/> should start with
    /// </summary>
    public FullscreenMode FullscreenType { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should be usable with an OpenGL context
    /// </summary>
    public bool AllowOpenGL { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should be usable with a Vulkan instance
    /// </summary>
    public bool AllowVulkan { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should be usable with a Metal instance
    /// </summary>
    public bool AllowMetal { get; set; }

    /// <summary>
    /// Whether to start the <see cref="Window"/> shown or hidden
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    /// Whether or not the <see cref="Window"/> should be launched borderless
    /// </summary>
    public bool IsBorderless { get; set; }

    /// <summary>
    /// Whether or not the <see cref="Window"/> is resizable
    /// </summary>
    public bool IsResizable { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> is Maximized, Minimized or neither
    /// </summary>
    public MaximizedType MaximizedOrMinimized { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should have the mouse grabbed upon spawn
    /// </summary>
    public bool HasMouseGrabbed { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should have input focus
    /// </summary>
    public bool HasInputFocus { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should have mouse focus
    /// </summary>
    public bool HasMouseFocus { get; set; }

    /// <summary>
    /// Whether to enable High DPI if supported
    /// </summary>
    public bool AllowHighDPI { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> has the mouse captured
    /// </summary>
    public bool HasMouseCaptured { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should always be on top
    /// </summary>
    public bool IsAlwaysOnTop { get; set; }

    /// <summary>
    /// Whether the <see cref="Window"/> should not be added to the taskbar if possible
    /// </summary>
    public bool SkipTaskbar { get; set; }

    /// <summary>
    /// Whether this should be treated as an utility Window
    /// </summary>
    public bool IsUtility { get; set; }

    /// <summary>
    /// Whether this should be treated as a tooltip
    /// </summary>
    public bool IsTooltip { get; set; }

    /// <summary>
    /// Whether this should be treated as a Popup Menu
    /// </summary>
    public bool IsPopupMenu { get; set; }

    /// <summary>
    /// Whether the Keyboard should be grabbed
    /// </summary>
    public bool HasKeyboardGrabbed { get; set; }

    /// <summary>
    /// Whether the Input should be grabbed
    /// </summary>
    public bool HasInputGrabbed { get; set; }

    #endregion

    #region Setter Methods

    /// <summary>
    /// Whether the <see cref="Window"/> should be usable with an OpenGL context
    /// </summary>
    /// <param name="allowOpenGL"></param>
    /// <returns></returns>
    public WindowConfig OpenGL(bool allowOpenGL)
    {
        AllowOpenGL = allowOpenGL;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should be usable with a Vulkan instance
    /// </summary>
    /// <param name="allowVulkan"></param>
    /// <returns></returns>
    public WindowConfig Vulkan(bool allowVulkan)
    {
        AllowVulkan = allowVulkan;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should be usable with a Metal instance
    /// </summary>
    /// <param name="allowMetal"></param>
    /// <returns></returns>
    public WindowConfig Metal(bool allowMetal)
    {
        AllowMetal = allowMetal;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should be allowed to have High DPI enabled
    /// </summary>
    /// <param name="allowHighDPI"></param>
    /// <returns></returns>
    public WindowConfig HighDPI(bool allowHighDPI)
    {
        AllowHighDPI = allowHighDPI;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should have the mouse captured
    /// </summary>
    /// <param name="hasMouseCaptured"></param>
    /// <returns></returns>
    public WindowConfig MouseCaptured(bool hasMouseCaptured)
    {
        HasMouseCaptured = hasMouseCaptured;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should always be on top
    /// </summary>
    /// <param name="isAlwaysOnTop"></param>
    /// <returns></returns>
    public WindowConfig AlwaysOnTop(bool isAlwaysOnTop)
    {
        IsAlwaysOnTop = isAlwaysOnTop;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should skip being added to the Taskbar
    /// </summary>
    /// <param name="showInTaskbar"></param>
    /// <returns></returns>
    public WindowConfig Taskbar(bool showInTaskbar)
    {
        SkipTaskbar = !showInTaskbar;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should be treated as an utility Window
    /// </summary>
    /// <param name="isUtility"></param>
    /// <returns></returns>
    public WindowConfig Utility(bool isUtility)
    {
        IsUtility = isUtility;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should be treated as a Tooltip
    /// </summary>
    /// <param name="isTooltip"></param>
    /// <returns></returns>
    public WindowConfig Tooltip(bool isTooltip)
    {
        IsTooltip = isTooltip;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should be treated as a PopupMenu
    /// </summary>
    /// <param name="isPopupMenu"></param>
    /// <returns></returns>
    public WindowConfig PopupMenu(bool isPopupMenu)
    {
        IsPopupMenu = isPopupMenu;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should have keyboard grab on launch
    /// </summary>
    /// <param name="hasKeyboardGrabbed"></param>
    /// <returns></returns>
    public WindowConfig KeyboardGrabbed(bool hasKeyboardGrabbed)
    {
        HasKeyboardGrabbed = hasKeyboardGrabbed;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should have input grabbed on launch
    /// </summary>
    /// <param name="hasInputGrabbed"></param>
    /// <returns></returns>
    public WindowConfig InputGrab(bool hasInputGrabbed)
    {
        HasInputGrabbed = hasInputGrabbed;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should have mouse focus on launch
    /// </summary>
    /// <param name="hasMouseFocus"></param>
    /// <returns></returns>
    public WindowConfig MouseFocus(bool hasMouseFocus)
    {
        HasMouseFocus = hasMouseFocus;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should have Input Focus on launch
    /// </summary>
    /// <param name="hasInputFocus"></param>
    /// <returns></returns>
    public WindowConfig InputFocus(bool hasInputFocus)
    {
        HasInputFocus = hasInputFocus;
        return this;
    }

    /// <summary>
    /// Whether the <see cref="Window"/> should have the mouse grabbed
    /// </summary>
    /// <param name="hasMouseGrabbed"></param>
    /// <returns></returns>
    public WindowConfig MouseGrabbed(bool hasMouseGrabbed)
    {
        HasMouseGrabbed = hasMouseGrabbed;
        return this;
    }

    /// <summary>
    /// The mode in which the <see cref="Window"/> should be spawned
    /// </summary>
    /// <param name="type">The Maximization type to use</param>
    /// <returns></returns>
    public WindowConfig Maximization(MaximizedType type)
    {
        MaximizedOrMinimized = type;
        return this;
    }

    /// <summary>
    /// Whether to allow resizing of the <see cref="Window"/>
    /// </summary>
    /// <param name="resizable"></param>
    /// <returns></returns>
    public WindowConfig Resizable(bool resizable)
    {
        IsResizable = resizable;
        return this;
    }

    /// <summary>
    /// Whether or not the <see cref="Window"/> should be hidden on launch
    /// </summary>
    /// <param name="hideOnStartup"></param>
    /// <returns></returns>
    public WindowConfig Hide(bool hideOnStartup)
    {
        Hidden = hideOnStartup;
        return this;
    }

    /// <summary>
    /// The fullscreen mode of the <see cref="Window"/>
    /// </summary>
    /// <param name="fs"></param>
    /// <returns></returns>
    public WindowConfig Fullscreen(FullscreenMode fs)
    {
        FullscreenType = fs;
        return this;
    }

    /// <summary>
    /// Whether or not the <see cref="Window"/> is borderless
    /// </summary>
    /// <param name="isBorderless"></param>
    /// <returns></returns>
    public WindowConfig Borderless(bool isBorderless)
    {
        IsBorderless = isBorderless;
        return this;
    }

    #endregion

    internal SDL_WindowFlags GenerateFlags()
    {
        SDL_WindowFlags flags = 0;

        flags |= FullscreenType switch
        {
            FullscreenMode.Windowed => 0,
            FullscreenMode.Fullscreen => SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
            FullscreenMode.DesktopFullscreen => SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP,
            _ => throw new InvalidOperationException($"Unknown FullscreenMode {FullscreenType}")
        };

        if (AllowOpenGL)
            flags |= SDL_WindowFlags.SDL_WINDOW_OPENGL;

        if (AllowVulkan)
            flags |= SDL_WindowFlags.SDL_WINDOW_VULKAN;

        if (AllowMetal)
            flags |= SDL_WindowFlags.SDL_WINDOW_METAL;

        flags |= Hidden ? SDL_WindowFlags.SDL_WINDOW_HIDDEN : SDL_WindowFlags.SDL_WINDOW_SHOWN;

        if (IsBorderless)
            flags |= SDL_WindowFlags.SDL_WINDOW_BORDERLESS;

        if (IsResizable)
            flags |= SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

        flags |= MaximizedOrMinimized switch
        {
            MaximizedType.Default => 0,
            MaximizedType.Maximized => SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,
            MaximizedType.Minimized => SDL_WindowFlags.SDL_WINDOW_MINIMIZED,
            _ => throw new InvalidOperationException($"Unknown MaximizedType {MaximizedOrMinimized}")
        };

        if (HasMouseGrabbed)
            flags |= SDL_WindowFlags.SDL_WINDOW_MOUSE_GRABBED;

        if (HasInputFocus)
            flags |= SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS;

        if (HasMouseFocus)
            flags |= SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS;

        if (AllowHighDPI)
            flags |= SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;

        if (HasMouseCaptured)
            flags |= SDL_WindowFlags.SDL_WINDOW_MOUSE_CAPTURE;

        if (IsAlwaysOnTop)
            flags |= SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP;

        if (SkipTaskbar)
            flags |= SDL_WindowFlags.SDL_WINDOW_SKIP_TASKBAR;

        if (IsUtility)
            flags |= SDL_WindowFlags.SDL_WINDOW_UTILITY;

        if (IsTooltip)
            flags |= SDL_WindowFlags.SDL_WINDOW_TOOLTIP;

        if (IsPopupMenu)
            flags |= SDL_WindowFlags.SDL_WINDOW_POPUP_MENU;

        if (HasKeyboardGrabbed)
            flags |= SDL_WindowFlags.SDL_WINDOW_KEYBOARD_GRABBED;

        if (HasInputGrabbed)
            flags |= SDL_WindowFlags.SDL_WINDOW_INPUT_GRABBED;

        return flags;
    }
}
