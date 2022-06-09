using SDL2.Bindings;
using SDL2.NET.HintTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class EnumExtensions
{
    public static Keycode ToKeycode(this Scancode code)
        => (Keycode)((int)code | SDLK_SCANCODE_MASK);
}

public enum JoystickHAT : byte
{
    Centered = 0x00,
    Up = 0x01,
    Right = 0x02,
    Down = 0x04,
    Left = 0x08,
    RightUp = Right | Up,
    RightDown = Right | Down,
    LeftUp = Left | Up,
    LeftDown = Left | Down
}

public enum JoystickPowerLevel
{
    Unknown = -1,
    Empty,
    Low,
    Medium,
    Full,
    Wired,
    Max
}

public enum JoystickType
{
    Unknown,
    GameController,
    Wheel,
    ArcadeStick,
    FlightStick,
    DancePad,
    Guitar,
    DrumKit,
    ArcadePad
}

public enum GameControllerBindType
{
    None = SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE,
    Button = SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_BUTTON,
    Axis = SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_AXIS,
    Hat = SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_HAT,
}

public enum GameControllerAxis
{
    Invalid = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_INVALID,
    LeftX = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX,
    LeftY = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY,
    RightX = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX,
    RightY = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY,
    LeftTrigger = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT,
    RightTrigger = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT,
    Max = SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_MAX,
}

public enum GameControllerButton
{
    Invalid = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_INVALID,
    A = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A,
    B = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B,
    X = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X,
    Y = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y,
    Back = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK,
    Guide = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_GUIDE,
    Start = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START,
    LeftStick = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK,
    RightStick = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK,
    LeftShoulder = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER,
    RightShoulder = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
    DpadUp = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP,
    DpadDown = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN,
    DpadLeft = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT,
    DpadRight = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
    Misc1 = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MISC1,
    Paddle1 = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE1,
    Paddle2 = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE2,
    Paddle3 = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE3,
    Paddle4 = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE4,
    Touchpad = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_TOUCHPAD,
    Max = SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MAX,
}

public enum GameControllerType
{
    Unknown                     = SDL_GameControllerType.SDL_CONTROLLER_TYPE_UNKNOWN,
    Xbox360                     = SDL_GameControllerType.SDL_CONTROLLER_TYPE_XBOX360,
    XboxOne                     = SDL_GameControllerType.SDL_CONTROLLER_TYPE_XBOXONE,
    PS3                         = SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS3,
    PS4                         = SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS4,
    NintendoSwitchProController = SDL_GameControllerType.SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_PRO,
    Virtual                     = SDL_GameControllerType.SDL_CONTROLLER_TYPE_VIRTUAL,
    PS5                         = SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS5,
    AmazonLuna                  = SDL_GameControllerType.SDL_CONTROLLER_TYPE_AMAZON_LUNA,
    GoogleStadia                = SDL_GameControllerType.SDL_CONTROLLER_TYPE_GOOGLE_STADIA,
}

public enum DisplayOrientation
{
    Unknown = SDL_DisplayOrientation.SDL_ORIENTATION_UNKNOWN,
    Landscape = SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE,
    LandscapeFlipped = SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE_FLIPPED,
    Portrait = SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT,
    PortraitFlipped = SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT_FLIPPED
}

public enum ScalingQuality
{
    /// <summary>
    /// Nearest pixel sampling
    /// </summary>
    Nearest,

    /// <summary>
    /// Linear filtering (supported by OpenGL and Direct3D)
    /// </summary>
    Linear,

    /// <summary>
    /// Anisotropic filtering (supported by Direct3D)
    /// </summary>
    Best
}

public enum SDLSubSystem : uint
{
    Timer = SDL_INIT_TIMER,
    Audio = SDL_INIT_AUDIO,
    Video = SDL_INIT_VIDEO,
    Joystick = SDL_INIT_JOYSTICK,
    Haptic = SDL_INIT_HAPTIC,
    GameController = SDL_INIT_GAMECONTROLLER,
    Events = SDL_INIT_EVENTS,
    Sensor = SDL_INIT_SENSOR
}

public enum PowerState
{
    Unknown = 0,
    OnBattery,
    NoBattery,
    Charging,
    Charged
}

public enum SystemCursor
{
    /// <summary>
    /// Arrow (standard) cursor
    /// </summary>
    Arrow,   

    /// <summary>
    /// I-Beam cursor
    /// </summary>
    IBeam,   

    /// <summary>
    /// Wait cursor
    /// </summary>
    Wait,    

    /// <summary>
    /// Crosshair cursor
    /// </summary>
    Crosshair,   

    /// <summary>
    /// Small wait cursor, or <see cref="Wait"/> if unavailable
    /// </summary>
    WaitArrow,   

    /// <summary>
    /// Double arrow pointing up and to the left, and down and to the right ( \ )
    /// </summary>
    SizeUpLeft,

    /// <summary>
    /// Double arrow pointing up and to the right, and down and to the left ( / )
    /// </summary>
    SizeUpRight,

    /// <summary>
    /// Double arrow pointing left and right ( - )
    /// </summary>
    SizeLeftRight,  

    /// <summary>
    /// Double arrow pointing up and down ( | )
    /// </summary>
    SizeUpDown,  
    
    /// <summary>
    /// Four pointed arrow pointing up, down, left and right ( + )
    /// </summary>
    SizeAll, 

    /// <summary>
    /// Slashed circle or crossbones
    /// </summary>
    No,      

    /// <summary>
    /// Hand cursor
    /// </summary>
    Hand
}

[Flags]
public enum MessageBoxButtonFlags : uint
{
    ReturnKeyDefault = SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT,
    EscapeKeyDefault = SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT
}

[Flags]
public enum MessageBoxFlags : uint
{
    Error = SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR,
    Warning = SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING,
    Information = SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION,
}

public enum MaximizedType : uint
{
    Default = 0,
    Maximized = SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,
    Minimized = SDL_WindowFlags.SDL_WINDOW_MINIMIZED
}

[Flags]
public enum WindowFlags : uint
{
    Fullscreen = 0x00000001,
    OpenGL = 0x00000002,
    Shown = 0x00000004,
    Hidden = 0x00000008,
    Borderless = 0x00000010,
    Resizable = 0x00000020,
    Minimized = 0x00000040,
    Maximized = 0x00000080,
    MouseGrabbed = 0x00000100,
    InputFocus = 0x00000200,
    MouseFocus = 0x00000400,
    FullscreenDesktop = Fullscreen | 0x00001000,
    Foreign = 0x00000800,
    AllowHighDPI = 0x00002000, 
    MouseCapture = 0x00004000, 
    AlwaysOnTop = 0x00008000, 
    SkipTaskbar = 0x00010000,  
    Utility = 0x00020000,   
    Tooltip = 0x00040000,   
    PopupMenu = 0x00080000,
    KeyboardGrabbed = 0x00100000,  
    Vulkan = 0x10000000,
    Metal = 0x2000000,  
    InputGrabbed = MouseGrabbed,
}

[Flags]
public enum MouseButton : byte
{
    Left = 1,
    Middle = 1 << 1,
    Right = 1 << 2,
    X1 = 1 << 3,
    X2 = 1 << 4
}

public enum ClipRectangleStatus
{
	/// <summary>
	/// The <see cref="Surface"/> has clipping enabled
	/// </summary>
	Enabled,

	/// <summary>
	/// The <see cref="Surface"/> does not have clipping enabled
	/// </summary>
	Disabled,

	/// <summary>
	/// The <see cref="Surface"/> has clipping enabled, but the rectangles do not intersect and blits will be completely clipped
	/// </summary>
	Invalid
}

public enum FullscreenMode : uint
{
    Windowed = 0,
    Fullscreen = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
    DesktopFullscreen = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP,
}

public enum HitTestResult
{
	/// <summary>
	/// Represents a normal region, with no special properties.
	/// </summary>
	Normal = SDL_HitTestResult.SDL_HITTEST_NORMAL,

	/// <summary>
	/// Represents an area of the window that is draggable, much like the title bar. This will tell the OS to take all mouse input recorded here to drag the window.
	/// </summary>
	Draggable = SDL_HitTestResult.SDL_HITTEST_DRAGGABLE,

	/// <summary>
	/// Represents an area of the window that resizes the top left corner, much like the top left corner itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the top and left borders.
	/// </summary>
	ResizeTopLeft = SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPLEFT,

	/// <summary>
	/// Represents an area of the window that resizes the top border, much like the top border itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the top border.
	/// </summary>
	ResizeTop = SDL_HitTestResult.SDL_HITTEST_RESIZE_TOP,

	/// <summary>
	/// Represents an area of the window that resizes the top right corner, much like the top right corner itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the top and right borders.
	/// </summary>
	ResizeTopRight = SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPRIGHT,

	/// <summary>
	/// Represents an area of the window that resizes the right border, much like the right border itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the right border.
	/// </summary>
	ResizeRight = SDL_HitTestResult.SDL_HITTEST_RESIZE_RIGHT,

	/// <summary>
	/// Represents an area of the window that resizes the bottom right corner, much like the bottom right corner itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the bottom and right borders.
	/// </summary>
	ResizeBottomRight = SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMRIGHT,

	/// <summary>
	/// Represents an area of the window that resizes the bottom border, much like the bottom border itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the bottom border.
	/// </summary>
	ResizeBottom = SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOM,

	/// <summary>
	/// Represents an area of the window that resizes the bottom left corner, much like the bottom left corner itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the bottom and left borders.
	/// </summary>
	ResizeBottomLeft = SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMLEFT,

	/// <summary>
	/// Represents an area of the window that resizes the left border, much like the left border itself in a bordered window. This will tell the OS to take all mouse input recorded here to resize the left border.
	/// </summary>
	ResizeLeft = SDL_HitTestResult.SDL_HITTEST_RESIZE_LEFT
}

public enum HintPriority
{
	/// <summary>
	/// Low priority, used for default values
	/// </summary>
	Default = SDL_HintPriority.SDL_HINT_DEFAULT,

	/// <summary>
	/// Medium priority
	/// </summary>
	Normal = SDL_HintPriority.SDL_HINT_NORMAL,

	/// <summary>
	/// High priority
	/// </summary>
	Override = SDL_HintPriority.SDL_HINT_OVERRIDE
}

public enum PixelFormat : uint
{
	Unknown = 0u,
	Index1LSB = 286261504u,
	Index1MSB = 287310080u,
	Index4LSB = 303039488u,
	Index4MSB = 304088064u,
	Index8 = 318769153u,
	RGB332 = 336660481u,
	XRGB444 = 353504258u,
	RGB444 = 353504258u,
	XBGR444 = 357698562u,
	BGR444 = 357698562u,
	XRGB1555 = 353570562u,
	RGB555 = 353570562u,
	XBGR1555 = 286461698u,
	BGR555 = 286461698u,
	ARGB4444 = 355602434u,
	RGBA4444 = 356651010u,
	ABGR4444 = 359796738u,
	BGRA4444 = 360845314u,
	ARGB1555 = 355667970u,
	RGBA5551 = 356782082u,
	ABGR1555 = 359862274u,
	BGRA5551 = 360976386u,
	RGB565 = 353701890u,
	BGR565 = 357896194u,
	RGB24 = 386930691u,
	BGR24 = 390076419u,
	XRGB888 = 370546692u,
	RGB888 = 370546692u,
	RGBX8888 = 371595268u,
	XBGR888 = 374740996u,
	BGR888 = 374740996u,
	BGRX8888 = 375789572u,
	ARGB8888 = 372645892u,
	RGBA8888 = 373694468u,
	ABGR8888 = 376840196u,
	BGRA8888 = 377888772u,
	ARGB2101010 = 372711428u,
	YV12 = 842094169u,
	IYUV = 1448433993u,
	YUY2 = 844715353u,
	UYVY = 1498831189u,
	YVYU = 1431918169u,
}

public enum TextureAccess
{
	/// <summary>
	/// Changes rarely, not lockable
	/// </summary>
	Static = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC,

	/// <summary>
	/// Changes frequently, lockable
	/// </summary>
	Streaming = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,

	/// <summary>
	/// Can be used as a render target
	/// </summary>
	Target = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET
}

[Flags]
public enum BlendMode
{
	None = SDL_BlendMode.SDL_BLENDMODE_NONE,
	Blend = SDL_BlendMode.SDL_BLENDMODE_BLEND,
	Add = SDL_BlendMode.SDL_BLENDMODE_ADD,
	Mod = SDL_BlendMode.SDL_BLENDMODE_MOD,
	Mul = SDL_BlendMode.SDL_BLENDMODE_MUL,
	Invalid = SDL_BlendMode.SDL_BLENDMODE_INVALID
}

public enum BlendOperation
{
	Add = SDL_BlendOperation.SDL_BLENDOPERATION_ADD,
	Subtract = SDL_BlendOperation.SDL_BLENDOPERATION_SUBTRACT,
	Rev_subtract = SDL_BlendOperation.SDL_BLENDOPERATION_REV_SUBTRACT,
	Minimum = SDL_BlendOperation.SDL_BLENDOPERATION_MINIMUM,
	Maximum = SDL_BlendOperation.SDL_BLENDOPERATION_MAXIMUM
}

public enum BlendFactor
{
	Zero = SDL_BlendFactor.SDL_BLENDFACTOR_ZERO,
	One = SDL_BlendFactor.SDL_BLENDFACTOR_ONE,
	SourceColor = SDL_BlendFactor.SDL_BLENDFACTOR_SRC_COLOR,
	OneMinusSourceColor = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
	SourceAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_SRC_ALPHA,
	OneMinusSourceAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
	DestinationColor = SDL_BlendFactor.SDL_BLENDFACTOR_DST_COLOR,
	OneMinusDestinationColor = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR,
	DestinationAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_DST_ALPHA,
	OneMinusDestinationAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA
}

[Flags]
public enum RendererFlags : uint
{
	Software = SDL_RendererFlags.SDL_RENDERER_SOFTWARE,
	Accelerated = SDL_RendererFlags.SDL_RENDERER_ACCELERATED,
	PresentVSync = SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC,
	TargetTexture = SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
}

[Flags]
public enum Flip
{
	None = SDL_RendererFlip.SDL_FLIP_NONE,
	Horizontal = SDL_RendererFlip.SDL_FLIP_HORIZONTAL,
	Vertical = SDL_RendererFlip.SDL_FLIP_VERTICAL
}

public enum Scancode
{
    Unknown = SDL_Scancode.SDL_SCANCODE_UNKNOWN,

    A = SDL_Scancode.SDL_SCANCODE_A,
    B = SDL_Scancode.SDL_SCANCODE_B,
    C = SDL_Scancode.SDL_SCANCODE_C,
    D = SDL_Scancode.SDL_SCANCODE_D,
    E = SDL_Scancode.SDL_SCANCODE_E,
    F = SDL_Scancode.SDL_SCANCODE_F,
    G = SDL_Scancode.SDL_SCANCODE_G,
    H = SDL_Scancode.SDL_SCANCODE_H,
    I = SDL_Scancode.SDL_SCANCODE_I,
    J = SDL_Scancode.SDL_SCANCODE_J,
    K = SDL_Scancode.SDL_SCANCODE_K,
    L = SDL_Scancode.SDL_SCANCODE_L,
    M = SDL_Scancode.SDL_SCANCODE_M,
    N = SDL_Scancode.SDL_SCANCODE_N,
    O = SDL_Scancode.SDL_SCANCODE_O,
    P = SDL_Scancode.SDL_SCANCODE_P,
    Q = SDL_Scancode.SDL_SCANCODE_Q,
    R = SDL_Scancode.SDL_SCANCODE_R,
    S = SDL_Scancode.SDL_SCANCODE_S,
    T = SDL_Scancode.SDL_SCANCODE_T,
    U = SDL_Scancode.SDL_SCANCODE_U,
    V = SDL_Scancode.SDL_SCANCODE_V,
    W = SDL_Scancode.SDL_SCANCODE_W,
    X = SDL_Scancode.SDL_SCANCODE_X,
    Y = SDL_Scancode.SDL_SCANCODE_Y,
    Z = SDL_Scancode.SDL_SCANCODE_Z,

    Num1 = SDL_Scancode.SDL_SCANCODE_1,
    Num2 = SDL_Scancode.SDL_SCANCODE_2,
    Num3 = SDL_Scancode.SDL_SCANCODE_3,
    Num4 = SDL_Scancode.SDL_SCANCODE_4,
    Num5 = SDL_Scancode.SDL_SCANCODE_5,
    Num6 = SDL_Scancode.SDL_SCANCODE_6,
    Num7 = SDL_Scancode.SDL_SCANCODE_7,
    Num8 = SDL_Scancode.SDL_SCANCODE_8,
    Num9 = SDL_Scancode.SDL_SCANCODE_9,
    Num0 = SDL_Scancode.SDL_SCANCODE_0,

    Return = SDL_Scancode.SDL_SCANCODE_RETURN,
    Escape = SDL_Scancode.SDL_SCANCODE_ESCAPE,
    Backspace = SDL_Scancode.SDL_SCANCODE_BACKSPACE,
    Tab = SDL_Scancode.SDL_SCANCODE_TAB,
    Space = SDL_Scancode.SDL_SCANCODE_SPACE,

    Minus = SDL_Scancode.SDL_SCANCODE_MINUS,
    Equals = SDL_Scancode.SDL_SCANCODE_EQUALS,
    LeftBracket = SDL_Scancode.SDL_SCANCODE_LEFTBRACKET,
    RightBracket = SDL_Scancode.SDL_SCANCODE_RIGHTBRACKET,
    Backslash = SDL_Scancode.SDL_SCANCODE_BACKSLASH,
    Nonushash = SDL_Scancode.SDL_SCANCODE_NONUSHASH,
    Semicolon = SDL_Scancode.SDL_SCANCODE_SEMICOLON,
    Apostrophe = SDL_Scancode.SDL_SCANCODE_APOSTROPHE,
    Grave = SDL_Scancode.SDL_SCANCODE_GRAVE,
    Comma = SDL_Scancode.SDL_SCANCODE_COMMA,
    Period = SDL_Scancode.SDL_SCANCODE_PERIOD,
    Slash = SDL_Scancode.SDL_SCANCODE_SLASH,

    Capslock = SDL_Scancode.SDL_SCANCODE_CAPSLOCK,

    F1 = SDL_Scancode.SDL_SCANCODE_F1,
    F2 = SDL_Scancode.SDL_SCANCODE_F2,
    F3 = SDL_Scancode.SDL_SCANCODE_F3,
    F4 = SDL_Scancode.SDL_SCANCODE_F4,
    F5 = SDL_Scancode.SDL_SCANCODE_F5,
    F6 = SDL_Scancode.SDL_SCANCODE_F6,
    F7 = SDL_Scancode.SDL_SCANCODE_F7,
    F8 = SDL_Scancode.SDL_SCANCODE_F8,
    F9 = SDL_Scancode.SDL_SCANCODE_F9,
    F10 = SDL_Scancode.SDL_SCANCODE_F10,
    F11 = SDL_Scancode.SDL_SCANCODE_F11,
    F12 = SDL_Scancode.SDL_SCANCODE_F12,

    Printscreen = SDL_Scancode.SDL_SCANCODE_PRINTSCREEN,
    ScrollLock = SDL_Scancode.SDL_SCANCODE_SCROLLLOCK,
    Pause = SDL_Scancode.SDL_SCANCODE_PAUSE,
    Insert = SDL_Scancode.SDL_SCANCODE_INSERT,
    Home = SDL_Scancode.SDL_SCANCODE_HOME,
    Pageup = SDL_Scancode.SDL_SCANCODE_PAGEUP,
    Delete = SDL_Scancode.SDL_SCANCODE_DELETE,
    End = SDL_Scancode.SDL_SCANCODE_END,
    Pagedown = SDL_Scancode.SDL_SCANCODE_PAGEDOWN,
    Right = SDL_Scancode.SDL_SCANCODE_RIGHT,
    Left = SDL_Scancode.SDL_SCANCODE_LEFT,
    Down = SDL_Scancode.SDL_SCANCODE_DOWN,
    Up = SDL_Scancode.SDL_SCANCODE_UP,

    NumlockClear = SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR,
    KeyPadDivide = SDL_Scancode.SDL_SCANCODE_KP_DIVIDE,
    KeyPadMultiply = SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY,
    KeyPadMinus = SDL_Scancode.SDL_SCANCODE_KP_MINUS,
    KeyPadPlus = SDL_Scancode.SDL_SCANCODE_KP_PLUS,
    KeyPadEnter = SDL_Scancode.SDL_SCANCODE_KP_ENTER,
    KeyPad1 = SDL_Scancode.SDL_SCANCODE_KP_1,
    KeyPad2 = SDL_Scancode.SDL_SCANCODE_KP_2,
    KeyPad3 = SDL_Scancode.SDL_SCANCODE_KP_3,
    KeyPad4 = SDL_Scancode.SDL_SCANCODE_KP_4,
    KeyPad5 = SDL_Scancode.SDL_SCANCODE_KP_5,
    KeyPad6 = SDL_Scancode.SDL_SCANCODE_KP_6,
    KeyPad7 = SDL_Scancode.SDL_SCANCODE_KP_7,
    KeyPad8 = SDL_Scancode.SDL_SCANCODE_KP_8,
    KeyPad9 = SDL_Scancode.SDL_SCANCODE_KP_9,
    KeyPad0 = SDL_Scancode.SDL_SCANCODE_KP_0,
    KeyPadPeriod = SDL_Scancode.SDL_SCANCODE_KP_PERIOD,

    Nonusbackslash = SDL_Scancode.SDL_SCANCODE_NONUSBACKSLASH,
    Application = SDL_Scancode.SDL_SCANCODE_APPLICATION,
    Power = SDL_Scancode.SDL_SCANCODE_POWER,
    KeyPadEquals = SDL_Scancode.SDL_SCANCODE_KP_EQUALS,
    F13 = SDL_Scancode.SDL_SCANCODE_F13,
    F14 = SDL_Scancode.SDL_SCANCODE_F14,
    F15 = SDL_Scancode.SDL_SCANCODE_F15,
    F16 = SDL_Scancode.SDL_SCANCODE_F16,
    F17 = SDL_Scancode.SDL_SCANCODE_F17,
    F18 = SDL_Scancode.SDL_SCANCODE_F18,
    F19 = SDL_Scancode.SDL_SCANCODE_F19,
    F20 = SDL_Scancode.SDL_SCANCODE_F20,
    F21 = SDL_Scancode.SDL_SCANCODE_F21,
    F22 = SDL_Scancode.SDL_SCANCODE_F22,
    F23 = SDL_Scancode.SDL_SCANCODE_F23,
    F24 = SDL_Scancode.SDL_SCANCODE_F24,
    Execute = SDL_Scancode.SDL_SCANCODE_EXECUTE,
    Help = SDL_Scancode.SDL_SCANCODE_HELP,
    Menu = SDL_Scancode.SDL_SCANCODE_MENU,
    Select = SDL_Scancode.SDL_SCANCODE_SELECT,
    Stop = SDL_Scancode.SDL_SCANCODE_STOP,
    Again = SDL_Scancode.SDL_SCANCODE_AGAIN,
    Undo = SDL_Scancode.SDL_SCANCODE_UNDO,
    Cut = SDL_Scancode.SDL_SCANCODE_CUT,
    Copy = SDL_Scancode.SDL_SCANCODE_COPY,
    Paste = SDL_Scancode.SDL_SCANCODE_PASTE,
    Find = SDL_Scancode.SDL_SCANCODE_FIND,
    Mute = SDL_Scancode.SDL_SCANCODE_MUTE,
    Volumeup = SDL_Scancode.SDL_SCANCODE_VOLUMEUP,
    Volumedown = SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN,
    KeyPadComma = SDL_Scancode.SDL_SCANCODE_KP_COMMA,
    KeyPadEqualSAS400 = SDL_Scancode.SDL_SCANCODE_KP_EQUALSAS400,

    International1 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL1,
    International2 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL2,
    International3 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL3,
    International4 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL4,
    International5 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL5,
    International6 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL6,
    International7 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL7,
    International8 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL8,
    International9 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL9,
    Lang1 = SDL_Scancode.SDL_SCANCODE_LANG1,
    Lang2 = SDL_Scancode.SDL_SCANCODE_LANG2,
    Lang3 = SDL_Scancode.SDL_SCANCODE_LANG3,
    Lang4 = SDL_Scancode.SDL_SCANCODE_LANG4,
    Lang5 = SDL_Scancode.SDL_SCANCODE_LANG5,
    Lang6 = SDL_Scancode.SDL_SCANCODE_LANG6,
    Lang7 = SDL_Scancode.SDL_SCANCODE_LANG7,
    Lang8 = SDL_Scancode.SDL_SCANCODE_LANG8,
    Lang9 = SDL_Scancode.SDL_SCANCODE_LANG9,

    Alterase = SDL_Scancode.SDL_SCANCODE_ALTERASE,
    Sysreq = SDL_Scancode.SDL_SCANCODE_SYSREQ,
    Cancel = SDL_Scancode.SDL_SCANCODE_CANCEL,
    Clear = SDL_Scancode.SDL_SCANCODE_CLEAR,
    Prior = SDL_Scancode.SDL_SCANCODE_PRIOR,
    Return2 = SDL_Scancode.SDL_SCANCODE_RETURN2,
    Separator = SDL_Scancode.SDL_SCANCODE_SEPARATOR,
    Out = SDL_Scancode.SDL_SCANCODE_OUT,
    Oper = SDL_Scancode.SDL_SCANCODE_OPER,
    ClearAgain = SDL_Scancode.SDL_SCANCODE_CLEARAGAIN,
    Crsel = SDL_Scancode.SDL_SCANCODE_CRSEL,
    Exsel = SDL_Scancode.SDL_SCANCODE_EXSEL,

    KeyPad00 = SDL_Scancode.SDL_SCANCODE_KP_00,
    KeyPad000 = SDL_Scancode.SDL_SCANCODE_KP_000,
    ThousandsSeparator = SDL_Scancode.SDL_SCANCODE_THOUSANDSSEPARATOR,
    DecimalSeparator = SDL_Scancode.SDL_SCANCODE_DECIMALSEPARATOR,
    CurrencyUnit = SDL_Scancode.SDL_SCANCODE_CURRENCYUNIT,
    CurrencySubUnit = SDL_Scancode.SDL_SCANCODE_CURRENCYSUBUNIT,
    KeyPadLeftParentheses = SDL_Scancode.SDL_SCANCODE_KP_LEFTPAREN,
    KeyPadRightParentheses = SDL_Scancode.SDL_SCANCODE_KP_RIGHTPAREN,
    KeyPadLeftBrace = SDL_Scancode.SDL_SCANCODE_KP_LEFTBRACE,
    KeyPadRightBrace = SDL_Scancode.SDL_SCANCODE_KP_RIGHTBRACE,
    KeyPadTab = SDL_Scancode.SDL_SCANCODE_KP_TAB,
    KeyPadBackspace = SDL_Scancode.SDL_SCANCODE_KP_BACKSPACE,
    KeyPadA = SDL_Scancode.SDL_SCANCODE_KP_A,
    KeyPadB = SDL_Scancode.SDL_SCANCODE_KP_B,
    KeyPadC = SDL_Scancode.SDL_SCANCODE_KP_C,
    KeyPadD = SDL_Scancode.SDL_SCANCODE_KP_D,
    KeyPadE = SDL_Scancode.SDL_SCANCODE_KP_E,
    KeyPadF = SDL_Scancode.SDL_SCANCODE_KP_F,
    KeyPadXor = SDL_Scancode.SDL_SCANCODE_KP_XOR,
    KeyPadPower = SDL_Scancode.SDL_SCANCODE_KP_POWER,
    KeyPadPercent = SDL_Scancode.SDL_SCANCODE_KP_PERCENT,
    KeyPadLessThan = SDL_Scancode.SDL_SCANCODE_KP_LESS,
    KeyPadGreaterThan = SDL_Scancode.SDL_SCANCODE_KP_GREATER,
    KeyPadAmpersand = SDL_Scancode.SDL_SCANCODE_KP_AMPERSAND,
    KeyPadDoubleAmpersand = SDL_Scancode.SDL_SCANCODE_KP_DBLAMPERSAND,
    KeyPadVerticalBar = SDL_Scancode.SDL_SCANCODE_KP_VERTICALBAR,
    KeyPadDoubleVerticalBar = SDL_Scancode.SDL_SCANCODE_KP_DBLVERTICALBAR,
    KeyPadColon = SDL_Scancode.SDL_SCANCODE_KP_COLON,
    KeyPadHash = SDL_Scancode.SDL_SCANCODE_KP_HASH,
    KeyPadSpace = SDL_Scancode.SDL_SCANCODE_KP_SPACE,
    KeyPadAt = SDL_Scancode.SDL_SCANCODE_KP_AT,
    KeyPadExclam = SDL_Scancode.SDL_SCANCODE_KP_EXCLAM,
    KeyPadMemoryStore = SDL_Scancode.SDL_SCANCODE_KP_MEMSTORE,
    KeyPadMemoryRecall = SDL_Scancode.SDL_SCANCODE_KP_MEMRECALL,
    KeyPadMemoryClear = SDL_Scancode.SDL_SCANCODE_KP_MEMCLEAR,
    KeyPadMemoryAdd = SDL_Scancode.SDL_SCANCODE_KP_MEMADD,
    KeyPadMemorySubtract = SDL_Scancode.SDL_SCANCODE_KP_MEMSUBTRACT,
    KeyPadMemoryMultiply = SDL_Scancode.SDL_SCANCODE_KP_MEMMULTIPLY,
    KeyPadMemoryDivide = SDL_Scancode.SDL_SCANCODE_KP_MEMDIVIDE,
    KeyPadPlusMinus = SDL_Scancode.SDL_SCANCODE_KP_PLUSMINUS,
    KeyPadClear = SDL_Scancode.SDL_SCANCODE_KP_CLEAR,
    KeyPadClearEntry = SDL_Scancode.SDL_SCANCODE_KP_CLEARENTRY,
    KeyPadBinary = SDL_Scancode.SDL_SCANCODE_KP_BINARY,
    KeyPadOctal = SDL_Scancode.SDL_SCANCODE_KP_OCTAL,
    KeyPadDecimal = SDL_Scancode.SDL_SCANCODE_KP_DECIMAL,
    KeyPadHexadecimal = SDL_Scancode.SDL_SCANCODE_KP_HEXADECIMAL,

    LeftControl = SDL_Scancode.SDL_SCANCODE_LCTRL,
    LeftShift = SDL_Scancode.SDL_SCANCODE_LSHIFT,
    LeftAlt = SDL_Scancode.SDL_SCANCODE_LALT,

    /// <summary>
    /// Windows key, Command in Mac, etc.
    /// </summary>
    LeftGUI = SDL_Scancode.SDL_SCANCODE_LGUI,
    RightCtrl = SDL_Scancode.SDL_SCANCODE_RCTRL,
    RightShift = SDL_Scancode.SDL_SCANCODE_RSHIFT,
    RightAlt = SDL_Scancode.SDL_SCANCODE_RALT,

    /// <summary>
    /// Windows key, Command in Mac, etc.
    /// </summary>
    RightGUI = SDL_Scancode.SDL_SCANCODE_RGUI,

    Mode = SDL_Scancode.SDL_SCANCODE_MODE,

    /* These come from the USB consumer page (0x0C) */
    AudioNext = SDL_Scancode.SDL_SCANCODE_AUDIONEXT,
    AudioPrev = SDL_Scancode.SDL_SCANCODE_AUDIOPREV,
    AudioStop = SDL_Scancode.SDL_SCANCODE_AUDIOSTOP,
    AudioPlay = SDL_Scancode.SDL_SCANCODE_AUDIOPLAY,
    AudioMute = SDL_Scancode.SDL_SCANCODE_AUDIOMUTE,
    MediaSelect = SDL_Scancode.SDL_SCANCODE_MEDIASELECT,
    WWW = SDL_Scancode.SDL_SCANCODE_WWW,
    Mail = SDL_Scancode.SDL_SCANCODE_MAIL,
    Calculator = SDL_Scancode.SDL_SCANCODE_CALCULATOR,
    Computer = SDL_Scancode.SDL_SCANCODE_COMPUTER,
    AppControlSearch = SDL_Scancode.SDL_SCANCODE_AC_SEARCH,
    AppControlHome = SDL_Scancode.SDL_SCANCODE_AC_HOME,
    AppControlBack = SDL_Scancode.SDL_SCANCODE_AC_BACK,
    AppControlForward = SDL_Scancode.SDL_SCANCODE_AC_FORWARD,
    AppControlStop = SDL_Scancode.SDL_SCANCODE_AC_STOP,
    AppControlRefresh = SDL_Scancode.SDL_SCANCODE_AC_REFRESH,
    AppControlBookmarks = SDL_Scancode.SDL_SCANCODE_AC_BOOKMARKS,

    /* These come from other sources, and are mostly mac related */
    BrightnessDown = SDL_Scancode.SDL_SCANCODE_BRIGHTNESSDOWN,
    BrightnessUp = SDL_Scancode.SDL_SCANCODE_BRIGHTNESSUP,
    DisplaySwitch = SDL_Scancode.SDL_SCANCODE_DISPLAYSWITCH,
    KeyboardIlluminationToggle = SDL_Scancode.SDL_SCANCODE_KBDILLUMTOGGLE,
    KeyboardIlluminationDown = SDL_Scancode.SDL_SCANCODE_KBDILLUMDOWN,
    KeyboardIlluminationUp = SDL_Scancode.SDL_SCANCODE_KBDILLUMUP,
    Eject = SDL_Scancode.SDL_SCANCODE_EJECT,
    Sleep = SDL_Scancode.SDL_SCANCODE_SLEEP,

    App1 = SDL_Scancode.SDL_SCANCODE_APP1,
    App2 = SDL_Scancode.SDL_SCANCODE_APP2,

    /* These come from the USB consumer page (0x0C) */
    AudioRewind = SDL_Scancode.SDL_SCANCODE_AUDIOREWIND,
    AudioFastForward = SDL_Scancode.SDL_SCANCODE_AUDIOFASTFORWARD,

    /// <summary>
    /// Represents the amount of possible scan codes. Not a key.
    /// </summary>
    ScancodeCount = 512
}

public enum Keycode
{
    Unknown = 0,

    Return = '\r',
    Escape = 27, // '\033'
    Backspace = '\b',
    Tab = '\t',
    Space = ' ',
    Exclaim = '!',
    Quotedbl = '"',
    Hash = '#',
    Percent = '%',
    Dollar = '$',
    Ampersand = '&',
    Quote = '\'',
    Leftparen = '(',
    Rightparen = ')',
    Asterisk = '*',
    Plus = '+',
    Comma = ',',
    Minus = '-',
    Period = '.',
    Slash = '/',
    Num0 = '0',
    Num1 = '1',
    Num2 = '2',
    Num3 = '3',
    Num4 = '4',
    Num5 = '5',
    Num6 = '6',
    Num7 = '7',
    Num8 = '8',
    Num9 = '9',
    Colon = ':',
    Semicolon = ';',
    Less = '<',
    Equals = '=',
    Greater = '>',
    Question = '?',
    At = '@',

    A = 'A',
    B = 'B',
    C = 'C',
    D = 'D',
    E = 'E',
    F = 'F',
    G = 'G',
    H = 'H',
    I = 'I',
    J = 'J',
    K = 'K',
    L = 'L',
    M = 'M',
    N = 'N',
    O = 'O',
    P = 'P',
    Q = 'Q',
    R = 'R',
    S = 'S',
    T = 'T',
    U = 'U',
    V = 'V',
    W = 'W',
    X = 'X',
    Y = 'Y',
    Z = 'Z',

    Leftbracket = '[',
    Backslash = '\\',
    Rightbracket = ']',
    Caret = '^',
    Underscore = '_',
    Backquote = '`',
    a = 'a',
    b = 'b',
    c = 'c',
    d = 'd',
    e = 'e',
    f = 'f',
    g = 'g',
    h = 'h',
    i = 'i',
    j = 'j',
    k = 'k',
    l = 'l',
    m = 'm',
    n = 'n',
    o = 'o',
    p = 'p',
    q = 'q',
    r = 'r',
    s = 's',
    t = 't',
    u = 'u',
    v = 'v',
    w = 'w',
    x = 'x',
    y = 'y',
    z = 'z',

    Capslock = SDL_Scancode.SDL_SCANCODE_CAPSLOCK | SDLK_SCANCODE_MASK,

    F1 = SDL_Scancode.SDL_SCANCODE_F1 | SDLK_SCANCODE_MASK,
    F2 = SDL_Scancode.SDL_SCANCODE_F2 | SDLK_SCANCODE_MASK,
    F3 = SDL_Scancode.SDL_SCANCODE_F3 | SDLK_SCANCODE_MASK,
    F4 = SDL_Scancode.SDL_SCANCODE_F4 | SDLK_SCANCODE_MASK,
    F5 = SDL_Scancode.SDL_SCANCODE_F5 | SDLK_SCANCODE_MASK,
    F6 = SDL_Scancode.SDL_SCANCODE_F6 | SDLK_SCANCODE_MASK,
    F7 = SDL_Scancode.SDL_SCANCODE_F7 | SDLK_SCANCODE_MASK,
    F8 = SDL_Scancode.SDL_SCANCODE_F8 | SDLK_SCANCODE_MASK,
    F9 = SDL_Scancode.SDL_SCANCODE_F9 | SDLK_SCANCODE_MASK,
    F10 = SDL_Scancode.SDL_SCANCODE_F10 | SDLK_SCANCODE_MASK,
    F11 = SDL_Scancode.SDL_SCANCODE_F11 | SDLK_SCANCODE_MASK,
    F12 = SDL_Scancode.SDL_SCANCODE_F12 | SDLK_SCANCODE_MASK,

    PrintScreen = SDL_Scancode.SDL_SCANCODE_PRINTSCREEN | SDLK_SCANCODE_MASK,
    ScrollLock = SDL_Scancode.SDL_SCANCODE_SCROLLLOCK | SDLK_SCANCODE_MASK,
    Pause = SDL_Scancode.SDL_SCANCODE_PAUSE | SDLK_SCANCODE_MASK,
    Insert = SDL_Scancode.SDL_SCANCODE_INSERT | SDLK_SCANCODE_MASK,
    Home = SDL_Scancode.SDL_SCANCODE_HOME | SDLK_SCANCODE_MASK,
    PageUp = SDL_Scancode.SDL_SCANCODE_PAGEUP | SDLK_SCANCODE_MASK,
    Delete = 127,
    End = SDL_Scancode.SDL_SCANCODE_END | SDLK_SCANCODE_MASK,
    PageDown = SDL_Scancode.SDL_SCANCODE_PAGEDOWN | SDLK_SCANCODE_MASK,
    Right = SDL_Scancode.SDL_SCANCODE_RIGHT | SDLK_SCANCODE_MASK,
    Left = SDL_Scancode.SDL_SCANCODE_LEFT | SDLK_SCANCODE_MASK,
    Down = SDL_Scancode.SDL_SCANCODE_DOWN | SDLK_SCANCODE_MASK,
    Up = SDL_Scancode.SDL_SCANCODE_UP | SDLK_SCANCODE_MASK,

    NumlockClear = SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR | SDLK_SCANCODE_MASK,
    KeyPadDivide = SDL_Scancode.SDL_SCANCODE_KP_DIVIDE | SDLK_SCANCODE_MASK,
    KeyPadMultiply = SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY | SDLK_SCANCODE_MASK,
    KeyPadMinus = SDL_Scancode.SDL_SCANCODE_KP_MINUS | SDLK_SCANCODE_MASK,
    KeyPadPlus = SDL_Scancode.SDL_SCANCODE_KP_PLUS | SDLK_SCANCODE_MASK,
    KeyPadEnter = SDL_Scancode.SDL_SCANCODE_KP_ENTER | SDLK_SCANCODE_MASK,
    KeyPad1 = SDL_Scancode.SDL_SCANCODE_KP_1 | SDLK_SCANCODE_MASK,
    KeyPad2 = SDL_Scancode.SDL_SCANCODE_KP_2 | SDLK_SCANCODE_MASK,
    KeyPad3 = SDL_Scancode.SDL_SCANCODE_KP_3 | SDLK_SCANCODE_MASK,
    KeyPad4 = SDL_Scancode.SDL_SCANCODE_KP_4 | SDLK_SCANCODE_MASK,
    KeyPad5 = SDL_Scancode.SDL_SCANCODE_KP_5 | SDLK_SCANCODE_MASK,
    KeyPad6 = SDL_Scancode.SDL_SCANCODE_KP_6 | SDLK_SCANCODE_MASK,
    KeyPad7 = SDL_Scancode.SDL_SCANCODE_KP_7 | SDLK_SCANCODE_MASK,
    KeyPad8 = SDL_Scancode.SDL_SCANCODE_KP_8 | SDLK_SCANCODE_MASK,
    KeyPad9 = SDL_Scancode.SDL_SCANCODE_KP_9 | SDLK_SCANCODE_MASK,
    KeyPad0 = SDL_Scancode.SDL_SCANCODE_KP_0 | SDLK_SCANCODE_MASK,
    KeyPadPeriod = SDL_Scancode.SDL_SCANCODE_KP_PERIOD | SDLK_SCANCODE_MASK,

    Application = SDL_Scancode.SDL_SCANCODE_APPLICATION | SDLK_SCANCODE_MASK,
    Power = SDL_Scancode.SDL_SCANCODE_POWER | SDLK_SCANCODE_MASK,
    KeyPadEquals = SDL_Scancode.SDL_SCANCODE_KP_EQUALS | SDLK_SCANCODE_MASK,
    F13 = SDL_Scancode.SDL_SCANCODE_F13 | SDLK_SCANCODE_MASK,
    F14 = SDL_Scancode.SDL_SCANCODE_F14 | SDLK_SCANCODE_MASK,
    F15 = SDL_Scancode.SDL_SCANCODE_F15 | SDLK_SCANCODE_MASK,
    F16 = SDL_Scancode.SDL_SCANCODE_F16 | SDLK_SCANCODE_MASK,
    F17 = SDL_Scancode.SDL_SCANCODE_F17 | SDLK_SCANCODE_MASK,
    F18 = SDL_Scancode.SDL_SCANCODE_F18 | SDLK_SCANCODE_MASK,
    F19 = SDL_Scancode.SDL_SCANCODE_F19 | SDLK_SCANCODE_MASK,
    F20 = SDL_Scancode.SDL_SCANCODE_F20 | SDLK_SCANCODE_MASK,
    F21 = SDL_Scancode.SDL_SCANCODE_F21 | SDLK_SCANCODE_MASK,
    F22 = SDL_Scancode.SDL_SCANCODE_F22 | SDLK_SCANCODE_MASK,
    F23 = SDL_Scancode.SDL_SCANCODE_F23 | SDLK_SCANCODE_MASK,
    F24 = SDL_Scancode.SDL_SCANCODE_F24 | SDLK_SCANCODE_MASK,
    Execute = SDL_Scancode.SDL_SCANCODE_EXECUTE | SDLK_SCANCODE_MASK,
    Help = SDL_Scancode.SDL_SCANCODE_HELP | SDLK_SCANCODE_MASK,
    Menu = SDL_Scancode.SDL_SCANCODE_MENU | SDLK_SCANCODE_MASK,
    Select = SDL_Scancode.SDL_SCANCODE_SELECT | SDLK_SCANCODE_MASK,
    Stop = SDL_Scancode.SDL_SCANCODE_STOP | SDLK_SCANCODE_MASK,
    Again = SDL_Scancode.SDL_SCANCODE_AGAIN | SDLK_SCANCODE_MASK,
    Undo = SDL_Scancode.SDL_SCANCODE_UNDO | SDLK_SCANCODE_MASK,
    Cut = SDL_Scancode.SDL_SCANCODE_CUT | SDLK_SCANCODE_MASK,
    Copy = SDL_Scancode.SDL_SCANCODE_COPY | SDLK_SCANCODE_MASK,
    Paste = SDL_Scancode.SDL_SCANCODE_PASTE | SDLK_SCANCODE_MASK,
    Find = SDL_Scancode.SDL_SCANCODE_FIND | SDLK_SCANCODE_MASK,
    Mute = SDL_Scancode.SDL_SCANCODE_MUTE | SDLK_SCANCODE_MASK,
    VolumeUp = SDL_Scancode.SDL_SCANCODE_VOLUMEUP | SDLK_SCANCODE_MASK,
    VolumeDown = SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN | SDLK_SCANCODE_MASK,
    KeyPadComma = SDL_Scancode.SDL_SCANCODE_KP_COMMA | SDLK_SCANCODE_MASK,
    KeyPadEqualSAS400 = SDL_Scancode.SDL_SCANCODE_KP_EQUALSAS400 | SDLK_SCANCODE_MASK,

    Alterase = SDL_Scancode.SDL_SCANCODE_ALTERASE | SDLK_SCANCODE_MASK,
    Sysreq = SDL_Scancode.SDL_SCANCODE_SYSREQ | SDLK_SCANCODE_MASK,
    Cancel = SDL_Scancode.SDL_SCANCODE_CANCEL | SDLK_SCANCODE_MASK,
    Clear = SDL_Scancode.SDL_SCANCODE_CLEAR | SDLK_SCANCODE_MASK,
    Prior = SDL_Scancode.SDL_SCANCODE_PRIOR | SDLK_SCANCODE_MASK,
    Return2 = SDL_Scancode.SDL_SCANCODE_RETURN2 | SDLK_SCANCODE_MASK,
    Separator = SDL_Scancode.SDL_SCANCODE_SEPARATOR | SDLK_SCANCODE_MASK,
    Out = SDL_Scancode.SDL_SCANCODE_OUT | SDLK_SCANCODE_MASK,
    Oper = SDL_Scancode.SDL_SCANCODE_OPER | SDLK_SCANCODE_MASK,
    ClearAgain = SDL_Scancode.SDL_SCANCODE_CLEARAGAIN | SDLK_SCANCODE_MASK,
    Crsel = SDL_Scancode.SDL_SCANCODE_CRSEL | SDLK_SCANCODE_MASK,
    Exsel = SDL_Scancode.SDL_SCANCODE_EXSEL | SDLK_SCANCODE_MASK,

    KeyPad00 = SDL_Scancode.SDL_SCANCODE_KP_00 | SDLK_SCANCODE_MASK,
    KeyPad000 = SDL_Scancode.SDL_SCANCODE_KP_000 | SDLK_SCANCODE_MASK,
    ThousandsSeparator =
    SDL_Scancode.SDL_SCANCODE_THOUSANDSSEPARATOR | SDLK_SCANCODE_MASK,
    DecimalSeparator =
    SDL_Scancode.SDL_SCANCODE_DECIMALSEPARATOR | SDLK_SCANCODE_MASK,
    CurrencyUnit = SDL_Scancode.SDL_SCANCODE_CURRENCYUNIT | SDLK_SCANCODE_MASK,
    CurrencySubUnit =
    SDL_Scancode.SDL_SCANCODE_CURRENCYSUBUNIT | SDLK_SCANCODE_MASK,
    KeyPadLeftParentheses = SDL_Scancode.SDL_SCANCODE_KP_LEFTPAREN | SDLK_SCANCODE_MASK,
    KeyPadRightParentheses = SDL_Scancode.SDL_SCANCODE_KP_RIGHTPAREN | SDLK_SCANCODE_MASK,
    KeyPadLeftBrace = SDL_Scancode.SDL_SCANCODE_KP_LEFTBRACE | SDLK_SCANCODE_MASK,
    KeyPadRightBrace = SDL_Scancode.SDL_SCANCODE_KP_RIGHTBRACE | SDLK_SCANCODE_MASK,
    KeyPadTab = SDL_Scancode.SDL_SCANCODE_KP_TAB | SDLK_SCANCODE_MASK,
    KeyPadBackspace = SDL_Scancode.SDL_SCANCODE_KP_BACKSPACE | SDLK_SCANCODE_MASK,
    KeyPadA = SDL_Scancode.SDL_SCANCODE_KP_A | SDLK_SCANCODE_MASK,
    KeyPadB = SDL_Scancode.SDL_SCANCODE_KP_B | SDLK_SCANCODE_MASK,
    KeyPadC = SDL_Scancode.SDL_SCANCODE_KP_C | SDLK_SCANCODE_MASK,
    KeyPadD = SDL_Scancode.SDL_SCANCODE_KP_D | SDLK_SCANCODE_MASK,
    KeyPadE = SDL_Scancode.SDL_SCANCODE_KP_E | SDLK_SCANCODE_MASK,
    KeyPadF = SDL_Scancode.SDL_SCANCODE_KP_F | SDLK_SCANCODE_MASK,
    KeyPadXor = SDL_Scancode.SDL_SCANCODE_KP_XOR | SDLK_SCANCODE_MASK,
    KeyPadPower = SDL_Scancode.SDL_SCANCODE_KP_POWER | SDLK_SCANCODE_MASK,
    KeyPadPercent = SDL_Scancode.SDL_SCANCODE_KP_PERCENT | SDLK_SCANCODE_MASK,
    KeyPadLess = SDL_Scancode.SDL_SCANCODE_KP_LESS | SDLK_SCANCODE_MASK,
    KeyPadGreater = SDL_Scancode.SDL_SCANCODE_KP_GREATER | SDLK_SCANCODE_MASK,
    KeyPadAmpersand = SDL_Scancode.SDL_SCANCODE_KP_AMPERSAND | SDLK_SCANCODE_MASK,
    KeyPadDoubleAmpersand =
    SDL_Scancode.SDL_SCANCODE_KP_DBLAMPERSAND | SDLK_SCANCODE_MASK,
    KeyPadVerticalbar =
    SDL_Scancode.SDL_SCANCODE_KP_VERTICALBAR | SDLK_SCANCODE_MASK,
    KeyPadDoubleVerticalBar =
    SDL_Scancode.SDL_SCANCODE_KP_DBLVERTICALBAR | SDLK_SCANCODE_MASK,
    KeyPadColon = SDL_Scancode.SDL_SCANCODE_KP_COLON | SDLK_SCANCODE_MASK,
    KeyPadHash = SDL_Scancode.SDL_SCANCODE_KP_HASH | SDLK_SCANCODE_MASK,
    KeyPadSpace = SDL_Scancode.SDL_SCANCODE_KP_SPACE | SDLK_SCANCODE_MASK,
    KeyPadAt = SDL_Scancode.SDL_SCANCODE_KP_AT | SDLK_SCANCODE_MASK,
    KeyPadExclam = SDL_Scancode.SDL_SCANCODE_KP_EXCLAM | SDLK_SCANCODE_MASK,
    KeyPadMemoryStore = SDL_Scancode.SDL_SCANCODE_KP_MEMSTORE | SDLK_SCANCODE_MASK,
    KeyPadMemoryRecall = SDL_Scancode.SDL_SCANCODE_KP_MEMRECALL | SDLK_SCANCODE_MASK,
    KeyPadMemoryClear = SDL_Scancode.SDL_SCANCODE_KP_MEMCLEAR | SDLK_SCANCODE_MASK,
    KeyPadMemoryAdd = SDL_Scancode.SDL_SCANCODE_KP_MEMADD | SDLK_SCANCODE_MASK,
    KeyPadMemorySubtract =
    SDL_Scancode.SDL_SCANCODE_KP_MEMSUBTRACT | SDLK_SCANCODE_MASK,
    KeyPadMemoryMultiply =
    SDL_Scancode.SDL_SCANCODE_KP_MEMMULTIPLY | SDLK_SCANCODE_MASK,
    KeyPadMemoryDivide = SDL_Scancode.SDL_SCANCODE_KP_MEMDIVIDE | SDLK_SCANCODE_MASK,
    KeyPadPlusMinus = SDL_Scancode.SDL_SCANCODE_KP_PLUSMINUS | SDLK_SCANCODE_MASK,
    KeyPadClear = SDL_Scancode.SDL_SCANCODE_KP_CLEAR | SDLK_SCANCODE_MASK,
    KeyPadClearEntry = SDL_Scancode.SDL_SCANCODE_KP_CLEARENTRY | SDLK_SCANCODE_MASK,
    KeyPadBinary = SDL_Scancode.SDL_SCANCODE_KP_BINARY | SDLK_SCANCODE_MASK,
    KeyPadOctal = SDL_Scancode.SDL_SCANCODE_KP_OCTAL | SDLK_SCANCODE_MASK,
    KeyPadDecimal = SDL_Scancode.SDL_SCANCODE_KP_DECIMAL | SDLK_SCANCODE_MASK,
    KeyPadHexadecimal =
    SDL_Scancode.SDL_SCANCODE_KP_HEXADECIMAL | SDLK_SCANCODE_MASK,

    LeftCtrl = SDL_Scancode.SDL_SCANCODE_LCTRL | SDLK_SCANCODE_MASK,
    LeftShift = SDL_Scancode.SDL_SCANCODE_LSHIFT | SDLK_SCANCODE_MASK,
    LeftAlt = SDL_Scancode.SDL_SCANCODE_LALT | SDLK_SCANCODE_MASK,

    /// <summary>
    /// Windows key, Command in Mac, etc.
    /// </summary>
    LeftGUI = SDL_Scancode.SDL_SCANCODE_LGUI | SDLK_SCANCODE_MASK,
    RightCtrl = SDL_Scancode.SDL_SCANCODE_RCTRL | SDLK_SCANCODE_MASK,
    RightShift = SDL_Scancode.SDL_SCANCODE_RSHIFT | SDLK_SCANCODE_MASK,
    RightAlt = SDL_Scancode.SDL_SCANCODE_RALT | SDLK_SCANCODE_MASK,

    /// <summary>
    /// Windows key, Command in Mac, etc.
    /// </summary>
    RightGUI = SDL_Scancode.SDL_SCANCODE_RGUI | SDLK_SCANCODE_MASK,

    Mode = SDL_Scancode.SDL_SCANCODE_MODE | SDLK_SCANCODE_MASK,

    AudioNext = SDL_Scancode.SDL_SCANCODE_AUDIONEXT | SDLK_SCANCODE_MASK,
    AudioPrev = SDL_Scancode.SDL_SCANCODE_AUDIOPREV | SDLK_SCANCODE_MASK,
    AudioStop = SDL_Scancode.SDL_SCANCODE_AUDIOSTOP | SDLK_SCANCODE_MASK,
    AudioPlay = SDL_Scancode.SDL_SCANCODE_AUDIOPLAY | SDLK_SCANCODE_MASK,
    AudioMute = SDL_Scancode.SDL_SCANCODE_AUDIOMUTE | SDLK_SCANCODE_MASK,
    MediaSelect = SDL_Scancode.SDL_SCANCODE_MEDIASELECT | SDLK_SCANCODE_MASK,
    WWW = SDL_Scancode.SDL_SCANCODE_WWW | SDLK_SCANCODE_MASK,
    Mail = SDL_Scancode.SDL_SCANCODE_MAIL | SDLK_SCANCODE_MASK,
    Calculator = SDL_Scancode.SDL_SCANCODE_CALCULATOR | SDLK_SCANCODE_MASK,
    Computer = SDL_Scancode.SDL_SCANCODE_COMPUTER | SDLK_SCANCODE_MASK,
    AppControlSearch = SDL_Scancode.SDL_SCANCODE_AC_SEARCH | SDLK_SCANCODE_MASK,
    AppControlHome = SDL_Scancode.SDL_SCANCODE_AC_HOME | SDLK_SCANCODE_MASK,
    AppControlBack = SDL_Scancode.SDL_SCANCODE_AC_BACK | SDLK_SCANCODE_MASK,
    AppControlForward = SDL_Scancode.SDL_SCANCODE_AC_FORWARD | SDLK_SCANCODE_MASK,
    AppControlStop = SDL_Scancode.SDL_SCANCODE_AC_STOP | SDLK_SCANCODE_MASK,
    AppControlRefresh = SDL_Scancode.SDL_SCANCODE_AC_REFRESH | SDLK_SCANCODE_MASK,
    AppControlBookmarks = SDL_Scancode.SDL_SCANCODE_AC_BOOKMARKS | SDLK_SCANCODE_MASK,

    BrightnessDown =
    SDL_Scancode.SDL_SCANCODE_BRIGHTNESSDOWN | SDLK_SCANCODE_MASK,
    BrightnessUp = SDL_Scancode.SDL_SCANCODE_BRIGHTNESSUP | SDLK_SCANCODE_MASK,
    DisplaySwitch = SDL_Scancode.SDL_SCANCODE_DISPLAYSWITCH | SDLK_SCANCODE_MASK,
    KeyboardIlluminationToggle =
    SDL_Scancode.SDL_SCANCODE_KBDILLUMTOGGLE | SDLK_SCANCODE_MASK,
    KeyboardIlluminationDown = SDL_Scancode.SDL_SCANCODE_KBDILLUMDOWN | SDLK_SCANCODE_MASK,
    KeyboardIlluminationUp = SDL_Scancode.SDL_SCANCODE_KBDILLUMUP | SDLK_SCANCODE_MASK,
    Eject = SDL_Scancode.SDL_SCANCODE_EJECT | SDLK_SCANCODE_MASK,
    Sleep = SDL_Scancode.SDL_SCANCODE_SLEEP | SDLK_SCANCODE_MASK,
    App1 = SDL_Scancode.SDL_SCANCODE_APP1 | SDLK_SCANCODE_MASK,
    App2 = SDL_Scancode.SDL_SCANCODE_APP2 | SDLK_SCANCODE_MASK,

    AudioRewind = SDL_Scancode.SDL_SCANCODE_AUDIOREWIND | SDLK_SCANCODE_MASK,
    AudioFastForward = SDL_Scancode.SDL_SCANCODE_AUDIOFASTFORWARD | SDLK_SCANCODE_MASK
}

[Flags]
public enum KeyModifier
{
    None = 0x0000,
    LeftShift = 0x0001,
    RightShift = 0x0002,
    LeftCtrl = 0x0040,
    RightCtrl = 0x0080,
    LeftAlt = 0x0100,
    RightAlt = 0x0200,

    /// <summary>
    /// Windows key, Command in Mac, etc.
    /// </summary>
    LeftGUI = 0x0400,

    /// <summary>
    /// Windows key, Command in Mac, etc.
    /// </summary>
    RightGUI = 0x0800,
    Num = 0x1000,
    Caps = 0x2000,
    Mode = 0x4000,
    Scroll = 0x8000,

    /* These are defines in the SDL headers */
    Ctrl = LeftCtrl | RightCtrl,
    Shift = LeftShift | RightShift,
    Alt = LeftAlt | RightAlt,
    Gui = LeftGUI | RightGUI,

    Reserved = Scroll
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member