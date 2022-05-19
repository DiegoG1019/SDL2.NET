using SDL2.NET.Exceptions;
using SDL2.NET.Image.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL_image;

namespace SDL2.NET.Image;

public static class Image
{
    /// <summary>
    /// The version of SDL_image.
    /// </summary>
    public static Version SDLImageVersion { get; } = new(SDL_IMAGE_MAJOR_VERSION, SDL_IMAGE_MINOR_VERSION, SDL_IMAGE_PATCHLEVEL);

    /// <summary>
    /// Loads the given image from disk, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="file">The file to load the image from.</param>
    /// <returns>The converted surface object</returns>
    public static Surface Load(string file)
        => new(IMG_Load(file));

    /// <summary>
    /// Loads the given image from disk, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="file">The file to load the image from.</param>
    /// <returns>The converted surface object</returns>
    public static Texture LoadTexture(Renderer renderer, string file)
        => new(IMG_LoadTexture(renderer._handle, file), renderer);

    /// <summary>
    /// Saves the given surface as a PNG file
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="file"></param>
    public static void SavePNG(this Surface surface, string file)
        => SDLImageException.ThrowIfLessThan(IMG_SavePNG(surface._handle, file), 0);

    /// <summary>
    /// Saves the given surface as a JPG file
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="file"></param>
    public static void SaveJPG(this Surface surface, string file)
        => SDLImageException.ThrowIfLessThan(IMG_SavePNG(surface._handle, file), 0);

    /// <summary>
    /// Loads the given file as an Animation
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static Animation LoadAnimation(string file)
        => new(IMG_LoadAnimation(file));
}
