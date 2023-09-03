using System.Text.Json.Serialization;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

public struct Rectangle : IEquatable<Rectangle>
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public readonly Point TopLeft => new(X, Y);

    public readonly Point TopRight => new(X + Width, Y);

    public readonly Point BottomRight => new(X + Width, Y + Height);

    public readonly Point BottomLeft => new(X, Y + Height);

    public readonly Point Center => new(X + Width / 2, Y + Height / 2);

    public Rectangle(int width, int height, int x, int y)
    {
        Width = width;
        Height = height;
        X = x;
        Y = y;
    }

    public Rectangle(Size size, int x, int y) : this(size.Width, size.Height, x, y) { }
    public Rectangle(int width, int height, Point location) : this(width, height, location.X, location.Y) { }
    public Rectangle(Size size, Point location) : this(size.Width, size.Height, location.X, location.Y) { }

    /// <summary>
    /// Gets the <see cref="Rectangle"/>'s <see cref="Size"/>: Its <see cref="Width"/> and <see cref="Height"/> only.
    /// </summary>
    [JsonIgnore]
    public Size Size => new(Width, Height);

    public void Deconstruct(out int width, out int height, out int x, out int y)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public void Deconstruct(out int width, out int height, out Point position)
    {
        width = Width;
        height = Height;
        position = new(X, Y);
    }

    public void Deconstruct(out Size size, out int x, out int y)
    {
        size = Size;
        x = X;
        y = Y;
    }

    public void Deconstruct(out Size size, out Point position)
    {
        size = Size;
        position = new(X, Y);
    }

    /// <summary>
    /// Calculate a minimal <see cref="Rectangle"/> enclosing a set of <see cref="Point"/>s. <see cref="SDL_EnclosePoints" href="https://wiki.libsdl.org/SDL_EnclosePoints"/>
    /// </summary>
    /// <param name="points">The <see cref="Point"/>s to enclose</param>
    /// <param name="clip">A <see cref="Rectangle"/> used for clipping which <see cref="Point"/>s to enclose, or <see cref="null"/> to enclose all <see cref="Point"/>s</param>
    /// <returns></returns>
    public static bool Enclose(ReadOnlySpan<Point> points, Rectangle? clip, out Rectangle result)
    {
        SDL_bool suc = SDL_bool.SDL_FALSE;
        SDL_Rect res;
        if (clip is Rectangle c)
        {
            suc = SDL_EnclosePoints(points.ToSDL(), points.Length, ref c.ToSDLRef(), out res);
            result = res.ToDotNETRef();
            return suc is SDL_bool.SDL_TRUE;
        }

        SDL_EnclosePoints(points.ToSDL(), points.Length, IntPtr.Zero, out res);
        result = res.ToDotNETRef();
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
        if (SDL_IntersectRect(ref this.ToSDLRef(), ref other.ToSDLRef(), out var result) is SDL_bool.SDL_TRUE)
        {
            intersection = result.ToDotNETRef();
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
        return SDL_HasIntersection(ref this.ToSDLRef(), ref other.ToSDLRef()) is SDL_bool.SDL_TRUE;
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
        return SDL_IntersectRectAndLine(ref this.ToSDLRef(), ref ox, ref oy, ref dx, ref dy) is SDL_bool.SDL_TRUE;
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
        SDL_UnionRect(ref this.ToSDLRef(), ref other.ToSDLRef(), out var result);
        return (Rectangle)result;
    }

    /// <summary>
    /// Verifies if the given <see cref="Point"/> <paramref name="point"/> is contained within this <see cref="Rectangle"/>
    /// </summary>
    /// <param name="point">The <see cref="Point"/> to check if is resident of this <see cref="Rectangle"/></param>
    /// <returns>Whether <see cref="Point"/> <paramref name="point"/> resides within <see cref="this"/> <see cref="Rectangle"/></returns>
    public readonly bool Contains(Point point)
        => point.IsContainedIn(this);

    public static implicit operator Rectangle(SDL_Rect rect)
        => new(rect.w, rect.h, rect.x, rect.y);

    public bool Equals(Rectangle other) => Height == other.Height && Width == other.Width && X == other.X && Y == other.Y;
    public static bool operator ==(Rectangle a, Rectangle b) => a.Equals(b);
    public static bool operator !=(Rectangle a, Rectangle b) => !a.Equals(b);

    public override bool Equals(object? obj)
        => obj is Rectangle rectangle && Equals(rectangle);

    public override int GetHashCode()
        => HashCode.Combine(Width, Height, X, Y);
}
