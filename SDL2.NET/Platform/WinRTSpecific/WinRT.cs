using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Platform.WinRTSpecific;

/// <summary>
/// Provides access to SDL's Microsoft Windows Runtime specific functions
/// </summary>
[SupportedOSPlatform("Windows"), SupportedOSPlatform("WinRT")]
public static class WinRT
{
    /// <summary>
    /// Detects the device family of WinRT platform at runtime
    /// </summary>
    public static WinRTDeviceFamily CurrentDeviceFamily => (WinRTDeviceFamily)SDL_WinRTGetDeviceFamily();

    /// <summary>
    /// Query if the current device is a tablet
    /// </summary>
    /// <remarks>
    /// This property also evaluates to false if SDL cannot determine this
    /// </remarks>
    public static bool IsTablet => SDL_IsTablet() == SDL_bool.SDL_TRUE;
}
