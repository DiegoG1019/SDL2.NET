namespace SDL2.NET;

/// <summary>
/// Represents Power Status of the system
/// </summary>
public struct PowerStatus
{
    internal PowerStatus(PowerState powerState, TimeSpan remaining, int charge)
    {
        PowerState = powerState;
        Remaining = remaining;
        Charge = charge;
    }


    /// <summary>
    /// The current Power State
    /// </summary>
    public PowerState PowerState { get; }

    /// <summary>
    /// The remaining time of battery life
    /// </summary>
    public TimeSpan Remaining { get; }

    /// <summary>
    /// The charge left in the battery,
    /// </summary>
    public int Charge { get; }

    /// <summary>
    /// The percentage of charge left, from 0.0 to 1.0
    /// </summary>
    public float ChargePercent => Charge / 100;
}