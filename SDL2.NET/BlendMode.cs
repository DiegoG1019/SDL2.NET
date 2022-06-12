using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;
using static System.Net.WebRequestMethods;

namespace SDL2.NET;

/// <summary>
/// Represents a way of manipulating BlendModes 
/// </summary>
public static class BlendModes
{
    /// <summary>
    /// Compose a custom blend mode for renderers.
    /// </summary>
    /// <remarks>
    /// The properties <see cref="Renderer.BlendMode"/> and <see cref="Texture.BlendMode"/> accept the <see cref="BlendMode"/> returned by this method if the renderer supports it. A blend mode controls how the pixels from a drawing operation (source) get combined with the pixels from the render target (destination). First, the components of the source and destination pixels get multiplied with their blend factors. Then, the blend operation takes the two products and calculates the result that will get stored in the render target. For more info, see <see href="https://wiki.libsdl.org/SDL_ComposeCustomBlendMode"/>
    /// </remarks>
    /// <param name="sourceColorFactor">the <see cref="BlendFactor"/> applied to the red, green, and blue components of the source pixels</param>
    /// <param name="destinationColorFactor">the <see cref="BlendFactor"/> applied to the red, green, and blue components of the destination pixels</param>
    /// <param name="colorOperation">the <see cref="BlendOperation"/> used to combine the red, green, and blue components of the source and destination pixels</param>
    /// <param name="sourceAlphaFactor">the <see cref="BlendFactor"/> applied to the alpha component of the source pixels</param>
    /// <param name="destinationAlphaFactor">the <see cref="BlendFactor"/> applied to the alpha component of the destination pixels</param>
    /// <param name="alphaOperation">the <see cref="BlendOperation"/> used to combine the alpha component of the source and destination pixels</param>
    /// <returns>Returns an <see cref="BlendMode"/> that represents the chosen factors and operations.</returns>
    public static BlendMode ComposeCustomBlendMode(
            BlendFactor sourceColorFactor,
            BlendFactor destinationColorFactor,
            BlendOperation colorOperation,
            BlendFactor sourceAlphaFactor,
            BlendFactor destinationAlphaFactor,
            BlendOperation alphaOperation
        )
        => (BlendMode)SDL_ComposeCustomBlendMode(
            (SDL_BlendFactor)sourceColorFactor,
            (SDL_BlendFactor)destinationColorFactor,
            (SDL_BlendOperation)colorOperation,
            (SDL_BlendFactor)sourceAlphaFactor,
            (SDL_BlendFactor)destinationAlphaFactor,
            (SDL_BlendOperation)alphaOperation
        );
}
