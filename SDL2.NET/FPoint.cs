using static SDL2.Bindings.SDL;

namespace SDL2.NET;

public struct FPoint
{
    public float X { get; set; }
    public float Y { get; set; }

    public FPoint(float x, float y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Verifies if the given <see cref="FRectangle"/> <paramref name="rectangle"/> contains <see cref="this"/> <see cref="FPoint"/>
    /// </summary>
    /// <param name="rectangle">The <see cref="FRectangle"/> to check if is residency of <see cref="this"/> <see cref="FPoint"/></param>
    /// <returns>Whether <see cref="this"/> <see cref="FPoint"/> <paramref name="point"/> resides within <see cref="FRectangle"/> <paramref name="rectangle"/></returns>
    public bool IsContainedIn(FRectangle rectangle)
        => X >= rectangle.X && X < rectangle.X + rectangle.Width && Y >= rectangle.Y && Y < rectangle.Y + rectangle.Height;

    /// <summary>
    /// Whether this <see cref="FPoint"/> is to the left of <paramref name="b"/>
    /// </summary>
    /// <returns>true if this is to the left of <paramref name="b"/></returns>
    public bool IsToLeft(FPoint b) => -b.X + X > 0;

    public static implicit operator FPoint(SDL_Point point)
        => new(point.x, point.y);

    public static explicit operator Point(FPoint point)
        => new((int)point.X, (int)point.Y);

    public void Deconstruct(out float x, out float y)
    {
        x = X;
        y = Y;
    }

    internal void ToSDL(out SDL_FPoint point)
    {
        point = new SDL_FPoint()
        {
            x = X,
            y = Y
        };
    }
}
