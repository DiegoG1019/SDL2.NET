using static SDL2.Bindings.SDL;

namespace SDL2.NET;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

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
    // It's better to just calc it here instead of bothering with marshalling and converting the types to their SDL counterparts. Not like native code has any advantage over compiled arithmetic expressions anyway

    /// <summary>
    /// Whether this <see cref="Point"/> is to the left of <paramref name="b"/>
    /// </summary>
    /// <returns>true if this is to the left of <paramref name="b"/></returns>
    public bool IsToLeft(Point b) => -b.X + X > 0;

    public static implicit operator Point(SDL_Point point)
        => new(point.x, point.y);

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    internal void ToSDL(ref SDL_Point point)
    {
        point = new SDL_Point()
        {
            x = X,
            y = Y
        };
    }
}