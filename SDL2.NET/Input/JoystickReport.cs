namespace SDL2.NET.Input;

/// <summary>
/// Represents a report on a Joystick
/// </summary>
public struct JoystickReport
{
    /// <summary>
    /// The Guid of the <see cref="Joystick"/>
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// The vendor code of the <see cref="Joystick"/>
    /// </summary>
    public ushort Vendor { get; set; }

    /// <summary>
    /// The product code of the <see cref="Joystick"/>
    /// </summary>
    public ushort Product { get; set; }

    /// <summary>
    /// The product version code of the <see cref="Joystick"/>
    /// </summary>
    public ushort ProductVersion { get; set; }

    /// <summary>
    /// The type of the <see cref="Joystick"/>
    /// </summary>
    public JoystickType Type { get; set; }

    /// <summary>
    /// The instance Id of the <see cref="Joystick"/>
    /// </summary>
    public int InstanceId { get; set; }

    /// <summary>
    /// The name of the Joystick
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Generates a report on a joystick
    /// </summary>
    public JoystickReport(Guid guid, ushort vendor, ushort product, ushort productVersion, JoystickType type, int instanceId, string name)
    {
        Guid = guid;
        Vendor = vendor;
        Product = product;
        ProductVersion = productVersion;
        Type = type;
        InstanceId = instanceId;
        Name = name;
    }
}
