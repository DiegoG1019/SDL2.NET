using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents a <see cref="Vertex"/> that holds a <see cref="FPoint"/> <see cref="Position"/>, <see cref="RGBAColor"/> <see cref="Color"/> and <see cref="FPoint"/> <see cref="TextureOffset"/>
/// </summary>
public struct Vertex
{
    public Vertex(FPoint position, RGBAColor color, FPoint textureOffset)
    {
        Position = position;
        Color = color;
        TextureOffset = textureOffset;
    }

    /// <summary>
    /// The <see cref="Vertex"/> position, in <see cref="Renderer"/> coordinates
    /// </summary>
    public FPoint Position { get; }

    /// <summary>
    /// The <see cref="Vertex"/> color
    /// </summary>
    public RGBAColor Color { get; }

    /// <summary>
    /// Normalized <see cref="Texture"/> coordinates, if needed.
    /// </summary>
    /// <remarks>Range from 0 to 1; i.e. for a 64x64px <see cref="Texture"/>, beginning at 32x32px will be entered as 0.5,0,5</remarks>
    public FPoint TextureOffset { get; }

    internal void ToSDL(ref SDL_Vertex vertex)
    {
        Position.ToSDL(out var pos);
        Color.ToSDL(out var color);
        TextureOffset.ToSDL(out var tex_coord);

        vertex = new SDL_Vertex()
        {
            color = color,
            position = pos,
            tex_coord = tex_coord
        };
    }
}
