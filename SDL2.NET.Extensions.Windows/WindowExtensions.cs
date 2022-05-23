using SDL2.NET.Extensions.Windows.Bindings;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static SDL2.NET.Extensions.Windows.Bindings.WindowBindings;

namespace SDL2.NET.Extensions.Windows;

/// <summary>
/// A set of Platform specific extensions for Microsoft Windows
/// </summary>
[SupportedOSPlatform("Windows")]
public static class WindowExtensions
{
    private static bool IsLayered(IntPtr win)
        => (GetWindowLong(win, GWL_EXSTYLE) & WS_EX_LAYERED) != 0;

    /// <summary>
    /// Whether <paramref name="window"/> is marked as a Layered Window
    /// </summary>
    /// <param name="window"></param>
    /// <returns></returns>
    public static bool IsLayered(this Window window)
        => IsLayered(window.SystemInfo.info.win.window);

    private static void SetLayered(IntPtr win)
        => SetWindowLong(win, GWL_EXSTYLE, GetWindowLong(win, GWL_EXSTYLE) | WS_EX_LAYERED);

    /// <summary>
    /// Marks <paramref name="window"/> as a Layered <see cref="Window"/>
    /// </summary>
    /// <param name="window"></param>
    public static void SetLayered(this Window window)
        => SetLayered(window.SystemInfo.info.win.window);

    private static void SetColorKey(IntPtr win, uint color, byte alpha)
        => SetLayeredWindowAttributes(win, color, alpha, LWA_COLORKEY);

    /// <summary>
    /// Sets the specified <paramref name="color"/> as a ColorKey to the specified <paramref name="alpha"/> value
    /// </summary>
    /// <param name="window">The <see cref="Window"/> to perform the operation on</param>
    /// <param name="color">The colorkey to set</param>
    /// <param name="alpha">The alpha value represented by the colorkey</param>
    public static void SetColorKey(this Window window, RGBColor color, byte alpha)
        => SetColorKey(window.SystemInfo.info.win.window, color.ToUInt32(), alpha);

    /// <summary>
    /// Marks the specified <paramref name="color"/> as a transparency key for the <see cref="Window"/>. This means that, whenever that color is drawn to the screen, Windows will interpret it as transparent
    /// </summary>
    /// <param name="window">The <see cref="Window"/> to perform the operation on</param>
    /// <param name="color">The color to set as transparent</param>
    /// <remarks>This method calls <see cref="SetLayered(Window)"/> on the <see cref="Window"/></remarks>
    public static void MarkColorAsTransparent(this Window window, RGBColor color)
    {
        var win = window.SystemInfo.info.win.window;
        SetLayered(win);
        SetColorKey(win, color.ToUInt32(), 0);
    }

    /// <summary>
    /// Marks the specified <paramref name="color"/> as a transparency key for the <see cref="Window"/>, sets it as opaque. This means that, whenever that color is drawn to the screen, Windows will interpret it as an opaque color.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> to perform the operation on</param>
    /// <param name="color">The color to set as transparent</param>
    /// <remarks>This is useful for when you no longer want a color to be a transparency key. This method calls <see cref="SetLayered(Window)"/> on the <see cref="Window"/></remarks>
    public static void MarkColorAsOpaque(this Window window, RGBColor color)
    {
        var win = window.SystemInfo.info.win.window;
        SetLayered(win);
        SetColorKey(win, color.ToUInt32(), 255);
    }
}
