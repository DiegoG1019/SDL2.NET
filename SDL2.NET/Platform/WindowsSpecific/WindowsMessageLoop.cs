using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Platform.WindowsSpecific;

/// <summary>
/// Provides access to SDL's functions to access Window's message loop. This class must be activated before use with <see cref="Activate"/>
/// </summary>
[SupportedOSPlatform("Windows")]
public static class WindowsMessageLoop
{
    private static readonly Dictionary<IntPtr, WeakReference<Window>> WindowsHookDat = new();
    private static bool isActive;

    /// <summary>
    /// Activates this class's functionality.
    /// </summary>
    /// <remarks>
    /// This includes initializing relevant data and hooking to Microsoft Window's Message Loop
    /// </remarks>
    public static void Activate()
    {
        if (isActive)
            throw new InvalidOperationException("WindowsMessageLoop is already active");
        isActive = true;

        lock (WindowsHookDat)
        {
            SDL_SetWindowsMessageHook(MsgHook, IntPtr.Zero);
        }
    }

    /// <summary>
    /// Deactivates this class's functionality, clearing now unused objects.
    /// </summary>
    /// <remarks>
    /// This method will remove the message hook previously set by <see cref="Activate"/> and clear the list of hooked windows
    /// </remarks>
    /// <exception cref="InvalidOperationException"></exception>
    public static void Deactivate()
    {
        if (!isActive)
            throw new InvalidOperationException("WindowsMessageLoop is not active");
        isActive = false;

        lock (WindowsHookDat)
        {
            SDL_SetWindowsMessageHook(null, IntPtr.Zero);
            WindowsHookDat.Clear();
        }
    }

    /// <summary>
    /// Register the Window into the Window's Message Hook, along with the relevant user data
    /// </summary>
    public static void SetMessageHook(this Window window, WindowsMessageHook hook, UserData? userData = null)
    {
        if (!isActive)
            throw new InvalidOperationException("WindowsMessageLoop is not active");
        ArgumentNullException.ThrowIfNull(hook);
        ArgumentNullException.ThrowIfNull(window);
        lock (WindowsHookDat)
            WindowsHookDat[window.SystemInfo.info.win.window] = new(window);
        window.PlatformDat = window.PlatformDat with
        {
            Windows = new(userData, hook)
        };
    }

    private static readonly SDL_WindowsMessageHook MsgHook = (dat, hWnd, msg, wparam, lparam) =>
    {
        Window? win = default;
        bool call = false;

        lock (WindowsHookDat)
        {
            if (WindowsHookDat.TryGetValue(hWnd, out var wr))
                if (wr.TryGetTarget(out win))
                    call = true;
                else
                    WindowsHookDat.Remove(hWnd);
        }

        if (call)
        {
            var hook = win!.PlatformDat.Windows;
            hook!.Delegate(hook.UserData, win, msg, wparam, lparam);
        }

        return hWnd;
    };

}

internal sealed record WindowsHookInfo(UserData? UserData, WindowsMessageHook Delegate);

/// <summary>
/// A delegate that serves as a hook for Microsoft Windows messages
/// </summary>
public delegate void WindowsMessageHook(UserData? userdata, Window window, uint message, ulong wParam, long lParam);
