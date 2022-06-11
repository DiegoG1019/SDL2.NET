using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies a URL to a WinRT app's privacy policy.
/// </summary>
/// <remarks>
/// All network-enabled WinRT apps must make a privacy policy available to its users. On Windows 8, 8.1, and RT, Microsoft mandates that this policy be available in the Windows Settings charm, as accessed from within the app. SDL provides code to add a URL-based link there, which can point to the app's privacy policy. To setup a URL to an app's privacy policy, set SDL_HINT_WINRT_PRIVACY_POLICY_URL before calling any <see cref="SDLApplication.Initialize"/> functions. The contents of the hint should be a valid URL. For example, "http://www.example.com". The default value is "", which will prevent SDL from adding a privacy policy link to the Settings charm. This hint should only be set during app init.
/// </remarks>
[SupportedOSPlatform("Windows")]
public sealed class WinRTPrivacyPolicyURLHint : Hint
{
    internal WinRTPrivacyPolicyURLHint() : base(SDL_HINT_WINRT_PRIVACY_POLICY_URL)
    {

    }

    public string? URL
    {
        get => Get() ?? "";
        set => Set(value ?? "");
    }

    public void SetWithPriority(string? url, HintPriority priority)
    {
        SetWithPriority(url ?? "", priority);
    }
}
