using System.Numerics;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;
public struct Size : IEquatable<Size>
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static implicit operator Size(SDL_Point point)
        => new(point.x, point.y);

    internal void ToSDLPoint(ref SDL_Point point)
    {
        point = new SDL_Point()
        {
            x = Width,
            y = Height
        };
    }

    public void Deconstruct(out int width, out int height)
    {
        width = Width;
        height = Height;
    }

    public Vector2 ToVector2()
        => new(Width, Height);

    public bool Equals(Size other) => Height == other.Height && Width == other.Width;
    public static bool operator ==(Size a, Size b) => a.Equals(b);
    public static bool operator !=(Size a, Size b) => !a.Equals(b);

    public override bool Equals(object? obj)
        => obj is Size size && Equals(size);

    public override int GetHashCode()
        => HashCode.Combine(Width, Height);
}
