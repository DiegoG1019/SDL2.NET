using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies scaling quality. <see cref="SDL_HINT_RENDER_SCALE_QUALITY" href="https://wiki.libsdl.org/SDL_HINT_RENDER_SCALE_QUALITY"/>
/// </summary>
/// <remarks>This hint is checked when a texture is created and it affects scaling when copying that texture.</remarks>
public sealed class RenderScaleQuality : Hint
{
    internal RenderScaleQuality() : base(SDL_HINT_RENDER_SCALE_QUALITY) { }

    public void SetPriority(ScalingQuality quality, HintPriority priority)
        => Set(((int)quality).ToString(), priority);

    public ScalingQuality Quality
    {
        get
        {
            var q = Get();
            return q is null ? ScalingQuality.Nearest : Enum.Parse<ScalingQuality>(q, true);
        }
        set => Set(((int)value).ToString());
    }
}
