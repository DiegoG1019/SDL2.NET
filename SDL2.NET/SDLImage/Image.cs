using SDL2.NET.SDLImage.Exceptions;
using static SDL2.Bindings.SDL_image;

namespace SDL2.NET.SDLImage;

public static class Image
{
    static Image()
    {
        var v = IMG_Linked_Version();
        SDLImageVersion = new(v.major, v.minor, v.patch);
    }

    /// <summary>
    /// The version of SDL_image. <see cref="IMG_Linked_Version"/>
    /// </summary>
    public static Version SDLImageVersion { get; }

    /// <summary>
    /// Loads the given image from disk, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="file">The file to load the image from.</param>
    /// <returns>The converted surface object</returns>
    public static Surface Load(string file)
        => new(IMG_Load(file));

    /// <summary>
    /// Loads the given image from <paramref name="rwops"/>, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load_RW" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="rwops">The <see cref="RWops"/> object to load the image from.</param>
    /// <returns>The converted surface object</returns>
    public static Surface Load(RWops rwops)
        => new(IMG_Load_RW(rwops.handle, 0));

    /// <summary>
    /// Loads the given image from a <see cref="Stream"/> presented as a <see cref="RWops"/> object to <see cref="Load(RWops)"/>, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load_RW" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="stream">The <see cref="Stream"/> object to load the image from.</param>
    /// <returns>The converted surface object</returns>
    public static Surface Load(Stream stream)
    {
        using var rwop = RWops.CreateFromStream(stream);
        return Load(rwop);
    }

    /// <summary>
    /// Loads the given image from <paramref name="rwops"/>, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="rwops">The <see cref="RWops"/> object to load the image from.</param>
    /// <param name="renderer">The <see cref="Renderer"/> to bind the image to.</param>
    /// <returns>The converted surface object</returns>
    public static Texture LoadTexture(Renderer renderer, RWops rwops)
        => new(IMG_LoadTexture_RW(renderer._handle, rwops.handle, 0), renderer);

    /// <summary>
    /// Loads the given image from <see cref="Stream"/>, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="stream">The <see cref="Stream"/> object to load the image from.</param>
    /// <param name="renderer">The <see cref="Renderer"/> to bind the image to.</param>
    /// <returns>The converted surface object</returns>
    public static Texture LoadTexture(Renderer renderer, Stream stream)
    {
        using var rwop = RWops.CreateFromStream(stream);
        return LoadTexture(renderer, rwop);
    }

    /// <summary>
    /// Loads the given image from disk, and converts it into a <see cref="Surface"/> object. <see cref="IMG_Load" href="https://www.libsdl.org/projects/SDL_image/docs/SDL_image_11.html#SEC11"/>
    /// </summary>
    /// <remarks>The type of the image is taken from the file extension, and works for any supported type (jpg, png, tga, bmp)</remarks>
    /// <param name="file">The file to load the image from.</param>
    /// <param name="renderer">The <see cref="Renderer"/> to bind the image to.</param>
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
    /// <param name="quality"></param>
    public static void SaveJPG(this Surface surface, string file, int quality)
        => SDLImageException.ThrowIfLessThan(IMG_SaveJPG(surface._handle, file, quality), 0);

    /// <summary>
    /// Saves the given surface as a PNG file
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="rwops"></param>
    /// <param name="freeStream"></param>
    public static void SavePNG(this Surface surface, RWops rwops, bool freeStream = false)
    {
        SDLImageException.ThrowIfLessThan(IMG_SavePNG_RW(surface._handle, rwops.handle, freeStream ? 1 : 0), 0);
    }

    /// <summary>
    /// Saves the given surface as a JPG file
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="rwops"></param>
    /// <param name="quality"></param>
    /// <param name="freeStream"></param>
    public static void SaveJPG(this Surface surface, RWops rwops, int quality, bool freeStream = false)
    {
        SDLImageException.ThrowIfLessThan(IMG_SaveJPG_RW(surface._handle, rwops.handle, freeStream ? 1 : 0, quality), 0);
    }

    /// <summary>
    /// Saves the given surface as a PNG file
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="stream"></param>
    /// <param name="freeStream"></param>
    public static void SavePNG(this Surface surface, Stream stream, bool freeStream = false)
    {
        var rwops = RWops.CreateFromStream(stream, freeStream);
        SDLImageException.ThrowIfLessThan(IMG_SavePNG_RW(surface._handle, rwops.handle, freeStream ? 1 : 0), 0);
    }

    /// <summary>
    /// Saves the given surface as a JPG file
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="stream"></param>
    /// <param name="quality"></param>
    /// <param name="freeStream"></param>
    public static void SaveJPG(this Surface surface, Stream stream, int quality, bool freeStream = false)
    {
        var rwops = RWops.CreateFromStream(stream, freeStream);
        SDLImageException.ThrowIfLessThan(IMG_SaveJPG_RW(surface._handle, rwops.handle, freeStream ? 1 : 0, quality), 0);
    }

    /// <summary>
    /// Loads the given file as an Animation.
    /// </summary>
    /// <remarks>I'm not sure length should be used that way, but I couldn't really find much documentation on it</remarks>
    /// <param name="file"></param>
    /// <returns></returns>
    public static SDLAnimation LoadAnimation(string file, int length)
        => new(IMG_LoadAnimation(file), length);
}
