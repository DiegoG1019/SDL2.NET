using static SDL2.SDL;

namespace SDL2.NET;

public struct Rectangle
{
#error Not Implemented
    public int Width { get; }
    public int Height { get; }
    public int X { get; }
    public int Y { get; }

    public Rectangle(int width, int height, int x, int y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
    }

    public bool Intersect(Rectangle other, out Rectangle intersection)
    {
        SDL_Rect a = default;
        SDL_Rect b = default;
        ToSDLRect(ref a);
        ToSDLRect(ref b);

        if (SDL_IntersectRect(ref a, ref b, out var result) is SDL_bool.SDL_TRUE)
        {
            intersection = new(result.w, result.h, result.x, result.y);
            return true;
        }
        intersection = default;
        return false;
    }

    public bool Intersect(Rectangle other)
    {
        SDL_Rect a = default;
        SDL_Rect b = default;
        ToSDLRect(ref a);
        ToSDLRect(ref b);

        return SDL_HasIntersection(ref a, ref b) is SDL_bool.SDL_TRUE;
    }

    public static implicit operator Rectangle(SDL_Rect rect)
        => new(rect.w, rect.h, rect.x, rect.y);

    internal void ToSDLRect(ref SDL_Rect rect)
    {
        rect = new SDL_Rect()
        {
            h = Height,
            w = Width,
            x = X,
            y = Y
        };
    }
}