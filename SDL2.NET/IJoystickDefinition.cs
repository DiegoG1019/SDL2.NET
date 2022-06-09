namespace SDL2.NET;

/// <summary>
/// Represents special data about a Joystick's form and function
/// </summary>
public interface IJoystickDefinition
{
    /// <summary>
    /// Whether this Joystick is a Virtual one
    /// </summary>
    /// <param name="virtualJoystick"></param>
    public bool IsVirtual(out VirtualJoystick? virtualJoystick);

    /// <summary>
    /// Whether this Joystick is actually a GameController
    /// </summary>
    /// <param name="gameController"></param>
    public bool IsGameController(out GameController? gameController);

    /// <summary>
    /// Whether this Joystick is a Virtual one
    /// </summary>
    public bool IsVirtual();

    /// <summary>
    /// Whether this Joystick is actually a GameController
    /// </summary>
    public bool IsGameController();
}
