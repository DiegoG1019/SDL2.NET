using SDL2.NET.Input;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies extra gamecontroller db entries
/// </summary>
/// <remarks>
/// By default no extra gamecontroller db entries are specified. This hint must be set before calling <see cref="SDLApplication.InitializeGameController"/>. You can update mappings after the system is initialized with <see cref="GameController.AddMapping(string)"/>
/// </remarks>
public sealed class GameControllerConfigHint : Hint
{
    internal GameControllerConfigHint() : base(SDL_HINT_GAMECONTROLLERCONFIG) { }

    /// <summary>
    /// A newline delimited rows of gamecontroller config data
    /// </summary>
    public string? Value
    {
        get => Get();
        set => Set(value!);
    }

    public void SetWithPriority(string value, HintPriority priority)
        => Set(value, priority);
}