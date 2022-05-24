using SDL2.NET.Extensions.Windows.Bindings;
using SDL2.NET.Extensions.Windows.Exceptions;
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
    private const int Win32RedShift = 0;
    private const int Win32GreenShift = 8;
    private const int Win32BlueShift = 16;

    public static int ToWindowsColor(this RGBColor color)
        => color.Red << Win32RedShift | color.Green << Win32GreenShift | color.Blue << Win32BlueShift;

    public static void SetTransparencyKey(this Window window, RGBColor transKey)
    {
        var hWnd = window.SystemInfo.info.win.window;
        SetWindowLong(hWnd, GWL_EXSTYLE, GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED);

        bool result;
        if (transKey.IsBlack)
            result = SetLayeredWindowAttributes(hWnd, 0, window.OpacityByte, LWA_ALPHA);
        else if (window.OpacityByte == 255)
            result = SetLayeredWindowAttributes(hWnd, transKey.ToWindowsColor(), 0, LWA_COLORKEY);
        else
            result = SetLayeredWindowAttributes(hWnd, transKey.ToWindowsColor(), window.OpacityByte, LWA_ALPHA | LWA_COLORKEY);

        if (result is false)
            throw new SDLExtensionWin32Exception("Unable to set the Transparency key of the Window");
    }
}
