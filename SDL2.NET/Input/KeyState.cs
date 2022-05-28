namespace SDL2.NET.Input;

/// <summary>
/// Represents the status of a given key
/// </summary>
public struct KeyState
{
    /// <summary>
    /// The scancode of they key
    /// </summary>
    public Scancode Scancode { get; }

    /// <summary>
    /// Whether the key was pressed at the time of this snapshot or not
    /// </summary>
    public bool IsPressed { get; }

    internal KeyState(Scancode scancode, bool isPressed)
    {
        Scancode = scancode;
        IsPressed = isPressed;
    }
}