using SDL2.NET.Exceptions;
using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Platform.iOSSpecific;

/// <summary>
/// Provides a way of accessing SDL iOS specific facilities
/// </summary>
[SupportedOSPlatform("iOS")]
public static class iOS
{
    /// <summary>
    /// Sets the animation callback on Apple iOS.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> for which the animation <paramref name="callback"/> should be set</param>
    /// <param name="callback">The method to call</param>
    /// <param name="userData">The data to pass to the <paramref name="callback"/> when called</param>
    /// <param name="interval">The number of frames after which <paramref name="callback"/> will be called</param>
    public static void SetAnimationCallback(this Window window, int interval, iPhoneAnimationCallback callback, UserData? userData)
    {
        var dat = window.PlatformDat.iOS = new(callback, userData);
        SDLWindowException.ThrowIfLessThan(SDL_iPhoneSetAnimationCallback(window._handle, interval, dat.SDL_Callback, IntPtr.Zero), 0);
    }

    /// <summary>
    /// Enables or disables the SDL event pump on Apple iOS.
    /// </summary>
    /// <param name="isEnabled">true to enable, false to disable</param>
    public static void SetEventPump(bool isEnabled)
    {
        SDL_iPhoneSetEventPump(isEnabled ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }
}

/// <summary>
/// Represents a callback for a <see cref="Window"/>'s animation callback on iOS
/// </summary>
/// <param name="userData"></param>
public delegate void iPhoneAnimationCallback(UserData? userData);

internal sealed class iOS_Dat
{
    public readonly SDL_iPhoneAnimationCallback SDL_Callback;
    public readonly UserData? Data;
    public readonly iPhoneAnimationCallback Callback;

    public iOS_Dat(iPhoneAnimationCallback callback, UserData? data)
    {
        SDL_Callback = sdl_cb;
        Data = data;
        Callback = callback;
    }

    private void sdl_cb(IntPtr _)
        => Callback(Data);
}