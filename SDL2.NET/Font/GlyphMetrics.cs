namespace SDL2.NET.Font;

public struct GlyphMetrics
{
    public int MinimumX { get; }
    public int MinimumY { get; }
    public int MaximumX { get; }
    public int MaximumY { get; }
    public int Advance { get; }

    public GlyphMetrics(int minx, int miny, int maxx, int maxy, int advance)
    {
        MinimumX = minx;
        MinimumY = miny;
        MaximumX = maxx;
        MaximumY = maxy;
        Advance = advance;
    }
}