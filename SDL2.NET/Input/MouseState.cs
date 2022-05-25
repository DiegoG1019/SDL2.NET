namespace SDL2.NET.Input;

/// <summary>
/// A structure containing data about the mouse's state
/// </summary>
public struct MouseState
{
    /// <summary>
    /// The location that the mouse pointer was at the time of this state being contsructed
    /// </summary>
    public Point Location { get; }

    /// <summary>
    /// A set of flags denoting the buttons that were pressed at the time of this state being constructed
    /// </summary>
    public MouseButton PressedButtons { get; }

    /// <summary>
    /// Constructs a <see cref="MouseState"/> object to hold information about the mouse's state at a given time
    /// </summary>
    public MouseState(MouseButton pressedButtons, Point location)
    {
        Location = location;
        PressedButtons = pressedButtons;
    }
}