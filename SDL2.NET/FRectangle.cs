using static SDL2.Bindings.SDL;

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

    /// <summary>
    /// Verifies if the given <see cref="FPoint"/> <paramref name="point"/> is contained within this <see cref="FRectangle"/>
    /// </summary>
    /// <param name="point">The <see cref="FPoint"/> to check if is resident of this <see cref="FRectangle"/></param>
    /// <returns>Whether <see cref="FPoint"/> <paramref name="point"/> resides within <see cref="this"/> <see cref="FRectangle"/></returns>
    public bool Contains(FPoint point)
        => point.IsContainedIn(this);

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