using System.Runtime.Versioning;

namespace SDL2.NET.HintTypes;

/// <summary>
/// Represents two hints that can be used to customize and set WinRT's mandatory privacy policy configurations
/// </summary>
[SupportedOSPlatform("Windows")]
public sealed class WinRTPrivacyPolicyHint
{
    /// <summary>
    /// A hint that specifies a URL to a WinRT app's privacy policy.
    /// </summary>
    /// <remarks>
    /// All network-enabled WinRT apps must make a privacy policy available to its users. On Windows 8, 8.1, and RT, Microsoft mandates that this policy be available in the Windows Settings charm, as accessed from within the app. SDL provides code to add a URL-based link there, which can point to the app's privacy policy. To setup a URL to an app's privacy policy, set SDL_HINT_WINRT_PRIVACY_POLICY_URL before calling any <see cref="SDLApplication.Initialize"/> functions. The contents of the hint should be a valid URL. For example, "http://www.example.com". The default value is "", which will prevent SDL from adding a privacy policy link to the Settings charm. This hint should only be set during app init.
    /// </remarks>
    public WinRTPrivacyPolicyURLHint URL { get; } = new();

    /// <summary>
    /// A hint that specifies a label text for a WinRT app's privacy policy link.
    /// </summary>
    /// <remarks>
    /// Network-enabled WinRT apps must include a privacy policy. On Windows 8, 8.1, and RT, Microsoft mandates that this policy be available via the Windows Settings charm. SDL provides code to add a link there, with its label text being set via this optional hint. The default value is "Privacy Policy". This hint should only be set during app initialization, preferably before any calls to <see cref="SDLApplication.Initialize"/>
    /// </remarks>
    public WinRTPrivacyPolicyLabelHint Label { get; } = new();
}
