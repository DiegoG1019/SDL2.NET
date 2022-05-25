namespace SDL2.NET;

public struct FSize
{
    public float Width { get; set; }
    public float Height { get; set; }

    public FSize(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public void Deconstruct(out float width, out float height)
    {
        width = Width;
        height = Height;
    }
}