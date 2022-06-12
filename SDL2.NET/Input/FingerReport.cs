namespace SDL2.NET.Input;

/// <summary>
/// Represents touch information
/// </summary>
public struct FingerReport
{
    public FingerReport(FingerId id, float x, float y, float pressure)
    {
        Id = id;
        X = x;
        Y = y;
        Pressure = pressure;
    }

    /// <summary>
    /// The finger id
    /// </summary>
    public FingerId Id { get; set; }

    /// <summary>
    /// The x-axis location of the touch event, normalized (0...1)
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// The y-axis location of the touch event, normalized (0...1)
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// The quantity of pressure applied, normalized (0...1)
    /// </summary>
    public float Pressure { get; set; }
}
