using System.Runtime.Versioning;
using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Platform.AndroidSpecific;

/// <summary>
/// Provides access to SDL's Android specific facilities
/// </summary>
[SupportedOSPlatform("Android")]
public static class Android
{
    static Android()
    {
        IsAndroidTV = SDL_IsAndroidTV() == SDL_bool.SDL_TRUE;
        IsChromebook = SDL_IsChromebook() == SDL_bool.SDL_TRUE;
    }

    /// <summary>
    /// Whether or not the current platform is an Android TV
    /// </summary>
    public static bool IsAndroidTV { get; }

    /// <summary>
    /// Whether or not the current platform is a Chromebook
    /// </summary>
    public static bool IsChromebook { get; }

    /// <summary>
    /// Whether or not the current platform is a Samsung DeX docking station
    /// </summary>
    public static bool IsDeXMode { get; }

    /// <summary>
    /// Get the path used for internal storage for this application
    /// </summary>
    public static string InternalStoragePath => _isp ??= (SDL_AndroidGetInternalStoragePath() ?? throw new SDLException(SDL_GetAndClearError()));
    private static string? _isp;

    /// <summary>
    /// Get the path used for external storage for this application
    /// </summary>
    public static string ExternalStoragePath => _esp ??= (SDL_AndroidGetExternalStoragePath() ?? throw new SDLException(SDL_GetAndClearError()));
    private static string? _esp;

    /// <summary>
    /// Gets the current Android API Level
    /// </summary>
    /// <remarks>
    /// This value can later be passed to <see cref="GetVersion(int)"/> in order to get the corresponding Android Version
    /// </remarks>
    public static int APILevel => SDL_GetAndroidSDKVersion();

    /// <summary>
    /// Matches the passed API Level (preferrably taken from <see cref="APILevel"/> to the corresponding Android version string
    /// </summary>
    /// <param name="apiLevel">The Android API Level to match</param>
    /// <returns>The corresponding version string, or null if not recognized</returns>
    public static string? GetVersion(int apiLevel)
        => apiLevel switch
        {
            31 => "Android 12",
            30 => "Android 11",
            29 => "Android 10",
            28 => "Android 9",
            27 => "Android 8.1",
            26 => "Android 8.0",
            25 => "Android 7.1",
            24 => "Android 7.0",
            23 => "Android 6.0",
            22 => "Android 5.1",
            21 => "Android 5.0",
            20 => "Android 4.4W",
            19 => "Android 4.4",
            18 => "Android 4.3",
            17 => "Android 4.2",
            16 => "Android 4.1",
            15 => "Android 4.0.3",
            14 => "Android 4.0",
            13 => "Android 3.2",
            12 => "Android 3.1",
            11 => "Android 3.0",
            10 => "Android 2.3.3",
            _ => null
        };

    /// <summary>
    /// Get the current state of external storage.
    /// </summary>
    public static AndroidExternalStorageState ExternalStorageState => (AndroidExternalStorageState)SDL_AndroidGetExternalStorageState();

    /// <summary>
    /// Request permissions at runtime
    /// </summary>
    /// <remarks>
    /// This blocks the calling thread until the permission is granted or denied
    /// </remarks>
    /// <returns>True if the permission was granted, false otherwise</returns>
    public static bool RequestPermission(string permission) => SDL_AndroidRequestPermission(permission) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Shows an Android toast notification
    /// </summary>
    /// <remarks>
    /// Toasts are a sort of lightweight notification that are unique to Android and are shown in the UI thread. For more info, see <see href="https://developer.android.com/guide/topics/ui/notifiers/toasts"/>
    /// </remarks>
    /// <param name="message">The text message to be shown</param>
    /// <param name="duration">0=short, 1=long</param>
    /// <param name="gravity">Where the notification should appear on the screen. Set to -1 if you don't have a preference, or if you do, see: <see href="https://developer.android.com/reference/android/view/Gravity"/></param>
    /// <param name="offset">Set this parameter only when gravity >= 0</param>
    public static void ShowToast(string message, int duration, int gravity, Point offset)
        => ShowToast(message, duration, gravity, offset.X, offset.Y);

    /// <summary>
    /// Shows an Android toast notification
    /// </summary>
    /// <remarks>
    /// Toasts are a sort of lightweight notification that are unique to Android and are shown in the UI thread. For more info, see <see href="https://developer.android.com/guide/topics/ui/notifiers/toasts"/>
    /// </remarks>
    /// <param name="message">The text message to be shown</param>
    /// <param name="duration">0=short, 1=long</param>
    /// <param name="gravity">Where the notification should appear on the screen. Set to -1 if you don't have a preference, or if you do, see: <see href="https://developer.android.com/reference/android/view/Gravity"/></param>
    /// <param name="XOffset">Set this parameter only when gravity >= 0</param>
    /// <param name="YOffset">Set this parameter only when gravity >= 0</param>
    public static void ShowToast(string message, int duration, int gravity, int XOffset, int YOffset)
    {
        if (SDL_AndroidShowToast(message, duration, gravity, XOffset, YOffset) < 0)
            throw new SDLException(SDL_GetAndClearError());
    }

    /// <summary>
    /// Triggers Android's Back Button
    /// </summary>
    public static void BackButton() => SDL_AndroidBackButton();

    /// <summary>
    /// Get the Android Java Native Interface Environment of the current thread
    /// </summary>
    /// <remarks>
    /// This method returns an unmanaged object, which needs to be destroyed after finishing its use. SDL doesn't directly provide a way to manage these types of objects. It's up to you to understand how to use it
    /// </remarks>
    /// <returns>
    /// Returns the addresses expressed by a JNIEnv*
    /// </returns>
    public static IntPtr GetJNIEnv()
    {
        var ptr = SDL_AndroidGetJNIEnv();
        return ptr == IntPtr.Zero ? throw new SDLException(SDL_GetAndClearError()) : ptr;
    }

    /// <summary>
    /// Retrieve the Java instance of the Android activity class.
    /// </summary>
    /// <remarks>
    /// This method returns an unmanaged object, which needs to be destroyed after finishing its use. SDL doesn't directly provide a way to manage these types of objects. It's up to you to understand how to use it
    /// </remarks>
    /// <returns>
    /// Returns the addresses expressed by the jobject* that represents the instance of Activity class of the Android application
    /// </returns>
    public static IntPtr GetActivity()
    {
        var ptr = SDL_AndroidGetActivity();
        return ptr == IntPtr.Zero ? throw new SDLException(SDL_GetAndClearError()) : ptr;
    }
}
