using static SDL2.SDL;

namespace SDL2.NET;

public struct FPoint
{
    public float X { get; }
    public float Y { get; }

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
