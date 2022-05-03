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
}
