using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// Represents an SDL Hint
/// </summary>
public class Hint
{
    //private UserData? callbackUserData;
    //private HintCallback? callback;

    protected readonly string HintName;

    internal Hint(string name)
    {
        HintName = name;
        //SDL.SDL_AddHintCallback()
    }

    protected bool Set(string value)
        => SDL_SetHint(HintName, value) is SDL_bool.SDL_TRUE;

    protected bool Set(string value, HintPriority priority)
        => SDL_SetHintWithPriority(HintName, value, (SDL_HintPriority)priority) is SDL_bool.SDL_TRUE;

    protected string? Get()
        => SDL_GetHint(HintName);

    /// <summary>
    /// Get the boolean value of a hint variable. <see cref="SDL_GetHintBoolean" href="https://wiki.libsdl.org/SDL_GetHintBoolean"/>
    /// </summary>
    /// <param name="def">The value to return if the hint does not exist</param>
    /// <returns>The boolean value of a hint</returns>
    public bool GetBool(bool def = true)
        => SDL_GetHintBoolean(HintName, def ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE) is SDL_bool.SDL_TRUE;
}
