using static SDL2.SDL;

namespace SDL2.NET;

public struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator Point(SDL_Point point)
        => new(point.x, point.y);

    internal void ToSDLPoint(ref SDL_Point point)
    {
        point = new SDL_Point()
        {
            x = X,
            y = Y
        };
    }
}