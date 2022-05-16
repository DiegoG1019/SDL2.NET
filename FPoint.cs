using static SDL2.SDL;

namespace SDL2.NET;

public struct FPoint
{
#error Not Implemented
    public float X { get; }
    public float Y { get; }

    public FPoint(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator FPoint(SDL_Point point)
        => new(point.x, point.y);

    internal void ToSDL(ref SDL_FPoint point)
    {
        point = new SDL_FPoint()
        {
            x = X,
            y = Y
        };
    }
}
