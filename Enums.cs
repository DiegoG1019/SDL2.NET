using SDL2.NET.HintTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace SDL2.NET;

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

/// <summary>
/// An enumeration of texture access patterns. <see cref="SDL_TextureAccess" href="https://wiki.libsdl.org/SDL_TextureAccess"/>
/// </summary>
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

public static class EnumExtensions
{
	public static SDL_HitTestResult ToSDL(this HitTestResult hit)
		=> hit switch
        {
			HitTestResult.Normal => SDL_HitTestResult.SDL_HITTEST_NORMAL,
			HitTestResult.Draggable => SDL_HitTestResult.SDL_HITTEST_DRAGGABLE,
			HitTestResult.ResizeTopLeft => SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPLEFT,
			HitTestResult.ResizeTop => SDL_HitTestResult.SDL_HITTEST_RESIZE_TOP,
			HitTestResult.ResizeTopRight => SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPRIGHT,
			HitTestResult.ResizeRight => SDL_HitTestResult.SDL_HITTEST_RESIZE_RIGHT,
			HitTestResult.ResizeBottomRight => SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMRIGHT,
			HitTestResult.ResizeBottom => SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOM,
			HitTestResult.ResizeBottomLeft => SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMLEFT,
			HitTestResult.ResizeLeft => SDL_HitTestResult.SDL_HITTEST_RESIZE_LEFT,
			_ => throw new ArgumentException($"Unknown hit test result {hit}", nameof(hit))
        };

	public static SDL_HintPriority ToSDL(this HintPriority priority)
		=> priority switch
		{
			HintPriority.Default => SDL_HintPriority.SDL_HINT_DEFAULT,			
			HintPriority.Normal => SDL_HintPriority.SDL_HINT_NORMAL,
			HintPriority.Override => SDL_HintPriority.SDL_HINT_OVERRIDE,
			_ => throw new ArgumentException($"Unknown hint priority {priority}", nameof(priority))
		};
}
