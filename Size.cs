using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
