namespace SDL2.NET.Input;

/// <summary>
/// Represents touch information on a controller
/// </summary>
public readonly struct ControllerFingerReport
{
    internal ControllerFingerReport(byte state, float x, float y, float pressure)
    {
        State = state;
        X = x;
        Y = y;
        Pressure = pressure;
    }

    /// <summary>
    /// The finger id
    /// </summary>
    public byte State { get; }

    /// <summary>
    /// The x-axis location of the touch event
    /// </summary>
    public float X { get; }

    /// <summary>
    /// The y-axis location of the touch event
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// The quantity of pressure applied
    /// </summary>
    public float Pressure { get; }
}
