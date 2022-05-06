using static SDL2.SDL;

namespace SDL2.NET;

public struct FRectangle
{
    public float Width { get; }
    public float Height { get; }
    public float X { get; }
    public float Y { get; }

    public FRectangle(float width, float height, float x, float y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
    }

    public static implicit operator FRectangle(SDL_FRect rect)
        => new(rect.w, rect.h, rect.x, rect.y);

    internal void ToSDLFRect(ref SDL_FRect rect)
    {
        rect = new SDL_FRect()
        {
            h = Height,
            w = Width,
            x = X,
            y = Y
        };
    }
}