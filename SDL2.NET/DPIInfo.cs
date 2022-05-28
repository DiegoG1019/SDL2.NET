namespace SDL2.NET;

/// <summary>
/// Represents the DPI info of a Display
/// </summary>
/// <remarks>Dots per inch, in the context of a Video Display; is the number of individual dots that can be placed in a line within the span of 1 inch (2.54 cm). <see href="https://en.wikipedia.org/wiki/Dots_per_inch"/></remarks>
public struct DPIInfo
{
    /// <summary>
    /// The DPI of the Display
    /// </summary>
    public float DPI { get; }

    /// <summary>
    /// The horizontal DPI of the Display
    /// </summary>
    public float HDPI { get; }

    /// <summary>
    /// The Vertical DPI of the Display
    /// </summary>
    public float VDPI { get; }

    public DPIInfo(float dPI, float hDPI, float vDPI)
    {
        DPI = dPI;
        HDPI = hDPI;
        VDPI = vDPI;
    }
}