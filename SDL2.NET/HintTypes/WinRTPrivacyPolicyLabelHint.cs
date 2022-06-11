using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies a label text for a WinRT app's privacy policy link.
/// </summary>
/// <remarks>
/// Network-enabled WinRT apps must include a privacy policy. On Windows 8, 8.1, and RT, Microsoft mandates that this policy be available via the Windows Settings charm. SDL provides code to add a link there, with its label text being set via this optional hint. The default value is "Privacy Policy". This hint should only be set during app initialization, preferably before any calls to <see cref="SDLApplication.Initialize"/>
/// </remarks>
[SupportedOSPlatform("Windows")]
public sealed class WinRTPrivacyPolicyLabelHint : Hint
{
    internal WinRTPrivacyPolicyLabelHint() : base(SDL_HINT_WINRT_PRIVACY_POLICY_LABEL)
    {

    }

    public string? Label
    {
        get => Get() ?? "Privacy Policy";
        set => Set(value ?? "Privacy Policy");
    }

    public void SetWithPriority(string? label, HintPriority priority)
    {
        SetWithPriority(label ?? "Privacy Policy", priority);
    }
}