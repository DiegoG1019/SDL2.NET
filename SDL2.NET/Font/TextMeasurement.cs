namespace SDL2.NET.Font;

public struct TextMeasurement
{
    public int Extent { get; }
    public int Count { get; }

    public TextMeasurement(int extent, int count)
    {
        Extent = extent;
        Count = count;
    }
}