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

    /// <summary>
    /// Verifies if the given <see cref="Rectangle"/> <paramref name="rectangle"/> contains <see cref="this"/> <see cref="Point"/>
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to check if is residency of <see cref="this"/> <see cref="Point"/></param>
    /// <returns>Whether <see cref="this"/> <see cref="Point"/> <paramref name="point"/> resides within <see cref="Rectangle"/> <paramref name="rectangle"/></returns>
    public bool IsContainedIn(Rectangle rectangle)
        => X >= rectangle.X && X < rectangle.X + rectangle.Width && Y >= rectangle.Y && Y < rectangle.Y + rectangle.Height;

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