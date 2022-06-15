namespace SDL2.NET;

/// <summary>
/// Represents a set of color masks for a pixel
/// </summary>
/// <param name="BytesPerPixel">The number of bytes required to hold a pixel value, may not always be set</param>
/// <param name="BitsPerPixel">The number of significant bits in a pixel value</param>
/// <param name="Red">A mask representing the location of the red component of the pixel</param>
/// <param name="Green">A mask representing the location of the green component of the pixel</param>
/// <param name="Blue">A mask representing the location of the blue component of the pixel</param>
/// <param name="Alpha">A mask representing the location of the alpha component of the pixel or 0 if the pixel format doesn't have any alpha information</param>
public readonly record struct PixelFormatRGBAMask(byte BitsPerPixel, uint Red, uint Green, uint Blue, uint Alpha, byte BytesPerPixel)
{
    /// <summary>
    /// The number of significant bits in a pixel value
    /// </summary>
    /// <remarks>i.e.: 8, 15, 16, 24, 32</remarks>
    public byte BitsPerPixel { get; init; } = BitsPerPixel;

    /// <summary>
    /// The number of bytes required to hold a pixel value, may not always be set
    /// </summary>
    /// <remarks>
    /// i.e.: 1 (<see cref="byte"/>), 2 (<see cref="ushort"/>), 3 (<see cref="(byte red, byte green, byte blue)"/> or <see cref="RGBColor"/>), 4 (<see cref="uint"/> or <see cref="RGBAColor"/>)
    /// </remarks>
    public byte BytesPerPixel { get; init; } = BytesPerPixel;
}

/// <summary>
/// Represents a set of color masks for a pixel
/// </summary>
/// <param name="BitsPerPixel">The number of significant bits in a pixel value</param>
/// <param name="Red">A mask representing the location of the red component of the pixel</param>
/// <param name="Green">A mask representing the location of the green component of the pixel</param>
/// <param name="Blue">A mask representing the location of the blue component of the pixel</param>
/// <param name="Alpha">A mask representing the location of the alpha component of the pixel or 0 if the pixel format doesn't have any alpha information</param>
public readonly record struct RGBAMask(int BitsPerPixel, uint Red, uint Green, uint Blue, uint Alpha)
{
    /// <summary>
    /// The number of significant bits in a pixel value
    /// </summary>
    /// <remarks>i.e.: 8, 15, 16, 24, 32</remarks>
    public int BitsPerPixel { get; init; } = BitsPerPixel;
}