using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace SDL2.NET;
public struct Size
{
    public int Width { get; }
    public int Height { get; }

    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static implicit operator Size(SDL_Point point)
        => new(point.x, point.y);

    internal void ToSDLPoint(ref SDL_Point point)
    {
        point = new SDL_Point()
        {
            x = Width,
            y = Height
        };
    }
}
