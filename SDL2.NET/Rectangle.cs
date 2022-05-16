using static SDL2.SDL;

namespace SDL2.NET;

public struct Rectangle : IEquatable<Rectangle>
{
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

    /// <summary>
    /// Gets the <see cref="Rectangle"/>'s <see cref="Size"/>: Its <see cref="Width"/> and <see cref="Height"/> only.
    /// </summary>
    public Size Size => new(Width, Height);

    /// <summary>
    /// Calculate a minimal <see cref="Rectangle"/> enclosing a set of <see cref="Point"/>s. <see cref="SDL_EnclosePoints" href="https://wiki.libsdl.org/SDL_EnclosePoints"/>
    /// </summary>
    /// <param name="points">The <see cref="Point"/>s to enclose</param>
    /// <param name="clip">A <see cref="Rectangle"/> used for clipping which <see cref="Point"/>s to enclose, or <see cref="null"/> to enclose all <see cref="Point"/>s</param>
    /// <returns></returns>
    public static bool Enclose(ReadOnlySpan<Point> points, Rectangle? clip, out Rectangle result)
    {
        Span<SDL_Point> sdl_p = stackalloc SDL_Point[points.Length];
        for (int i = 0; i < sdl_p.Length; i++)
            points[i].ToSDL(ref sdl_p[i]);

        SDL_Rect res = default;
        SDL_bool suc = SDL_bool.SDL_FALSE;
        if (clip is Rectangle c)
        {
            SDL_Rect r = default;
            c.ToSDL(ref r);
            suc = SDL_EnclosePoints(sdl_p, points.Length, ref r, out res);
            result = (Rectangle)res;
            return suc is SDL_bool.SDL_TRUE;
        }

        SDL_EnclosePoints(sdl_p, points.Length, IntPtr.Zero, out res);
        result = (Rectangle)res;
        return suc is SDL_bool.SDL_TRUE;
    }

    /// <summary>
    /// Calculate the intersection of two rectangles. <see cref="SDL_IntersectRect" href="https://wiki.libsdl.org/SDL_IntersectRect"/>
    /// </summary>
    /// <remarks>An intersection between two <see cref="Rectangle"/>s is a <see cref="Rectangle"/> enclosing the area in which both <see cref="Rectangle"/>s overlap.</remarks>
    /// <param name="other">The <see cref="Rectangle"/> to intersect the current <see cref="Rectangle"/> with</param>
    /// <param name="intersection">The resulting intersection of both <see cref="Rectangle"/>s</param>
    /// <returns>Whether there exists an intersection between the two rectangles</returns>
    public bool Intersect(Rectangle other, out Rectangle intersection)
    {
        SDL_Rect a = default;
        SDL_Rect b = default;
        ToSDL(ref a);
        ToSDL(ref b);

        if (SDL_IntersectRect(ref a, ref b, out var result) is SDL_bool.SDL_TRUE)
        {
            intersection = new(result.w, result.h, result.x, result.y);
            return true;
        }
        intersection = default;
        return false;
    }

    /// <summary>
    /// Verifies the existence of an intersection between two <see cref="Rectangle"/>s. <see cref="SDL_HasIntersection" href="https://wiki.libsdl.org/SDL_HasIntersection"/>
    /// </summary>
    /// <remarks>An intersection between two <see cref="Rectangle"/>s is a <see cref="Rectangle"/> enclosing the area in which both <see cref="Rectangle"/>s overlap.</remarks>
    /// <param name="other">The <see cref="Rectangle"/> to intersect the current <see cref="Rectangle"/> with</param>
    /// <returns>Whether there exists an intersection between the two rectangles</returns>
    public bool Intersect(Rectangle other)
    {
        SDL_Rect a = default;
        SDL_Rect b = default;
        ToSDL(ref a);
        ToSDL(ref b);

        return SDL_HasIntersection(ref a, ref b) is SDL_bool.SDL_TRUE;
    }

    /// <summary>
    /// Verifies the existence of an intersection between a <see cref="Rectangle"/> and a line. <see cref="SDL_IntersectRectAndLine" href="https://wiki.libsdl.org/SDL_IntersectRectAndLine"/>
    /// </summary>
    /// <remarks>An intersection between a <see cref="Rectangle"/> and a line the area in which a line crosses the <see cref="Rectangle"/>. If there exists an intersection, in means the line crosses the <see cref="Rectangle"/> at at least one point</remarks>
    /// <param name="ox">The X coordinate of the origin point of the line to intersect the current <see cref="Rectangle"/> with</param>
    /// <param name="oy">The Y coordinate of the origin point of the line to intersect the current <see cref="Rectangle"/> with</param>
    /// <param name="dx">The X coordinate of the destination point of the line to intersect the current <see cref="Rectangle"/> with</param>
    /// <param name="dy">The Y coordinate of the destination point of the line to intersect the current <see cref="Rectangle"/> with</param>
    /// <returns>Whether there exists an intersection between the <see cref="Rectangle"/> and the line</returns>
    public bool Intersect(int ox, int oy, int dx, int dy)
    {
        SDL_Rect a = default;
        ToSDL(ref a);
        return SDL_IntersectRectAndLine(ref a, ref ox, ref oy, ref dx, ref dy) is SDL_bool.SDL_TRUE;
    }

    /// <summary>
    /// Verifies the existence of an intersection between a <see cref="Rectangle"/> and a line. <see cref="SDL_IntersectRectAndLine" href="https://wiki.libsdl.org/SDL_IntersectRectAndLine"/>
    /// </summary>
    /// <remarks>An intersection between a <see cref="Rectangle"/> and a line the area in which a line crosses the <see cref="Rectangle"/>. If there exists an intersection, in means the line crosses the <see cref="Rectangle"/> at at least one point</remarks>
    /// <param name="origin">The origin point of the line to intersect the current <see cref="Rectangle"/> with</param>
    /// <param name="destination">The destination point of the line to intersect the current <see cref="Rectangle"/> with</param>
    /// <returns>Whether there exists an intersection between the <see cref="Rectangle"/> and the line</returns>
    public bool Intersect(Point origin, Point destination)
        => Intersect(origin.X, origin.Y, destination.X, destination.Y);

    /// <summary>
    /// Calculate the union of two rectangles. <see cref="SDL_UnionRect" href="https://wiki.libsdl.org/SDL_UnionRect"/>
    /// </summary>
    /// <remarks>An union between two <see cref="Rectangle"/>s is a <see cref="Rectangle"/> enclosing both <see cref="Rectangle"/>s at once.</remarks>
    /// <param name="rect"></param>
    public Rectangle Union(Rectangle other)
    {
        SDL_Rect a = default;
        SDL_Rect b = default;
        ToSDL(ref a);
        ToSDL(ref b);

        SDL_UnionRect(ref a, ref b, out var result);
        return (Rectangle)result;
    }

    /// <summary>
    /// Verifies if the given <see cref="Point"/> <paramref name="point"/> is contained within this <see cref="Rectangle"/>
    /// </summary>
    /// <param name="point">The <see cref="Point"/> to check if is resident of this <see cref="Rectangle"/></param>
    /// <returns>Whether <see cref="Point"/> <paramref name="point"/> resides within <see cref="this"/> <see cref="Rectangle"/></returns>
    public bool Contains(Point point)
        => point.IsContainedIn(this);

    public static implicit operator Rectangle(SDL_Rect rect)
        => new(rect.w, rect.h, rect.x, rect.y);

    internal void ToSDL(ref SDL_Rect rect)
    {
        rect = new SDL_Rect()
        {
            h = Height,
            w = Width,
            x = X,
            y = Y
        };
    }

    public bool Equals(Rectangle other) => Height == other.Height && Width == other.Width && X == other.X && Y == other.Y;
    public static bool operator ==(Rectangle a, Rectangle b) => a.Equals(b);
    public static bool operator !=(Rectangle a, Rectangle b) => !a.Equals(b);

    public override bool Equals(object? obj) 
        => obj is Rectangle rectangle && Equals(rectangle);

    public override int GetHashCode()
        => HashCode.Combine(Width, Height, X, Y);
}
