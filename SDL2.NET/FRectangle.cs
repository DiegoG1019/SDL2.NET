using static SDL2.Bindings.SDL;

namespace SDL2.NET;

public struct FRectangle
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public FRectangle(float width, float height, float x, float y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
    }

    public void Deconstruct(out float width, out float height, out float x, out float y)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public void Deconstruct(out float width, out float height, out FPoint position)
    {
        width = Width;
        height = Height;
        position = new(X, Y);
    }

    public void Deconstruct(out FSize size, out float x, out float y)
    {
        size = new(Width, Height);
        x = X;
        y = Y;
    }

    public void Deconstruct(out FSize size, out FPoint position)
    {
        size = new(Width, Height);
        position = new(X, Y);
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

    internal SDL_FRect ToSDL()
    {
        return new SDL_FRect()
        {
            h = Height,
            w = Width,
            x = X,
            y = Y
        };
    }

    internal void ToSDL(out SDL_FRect rect)
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