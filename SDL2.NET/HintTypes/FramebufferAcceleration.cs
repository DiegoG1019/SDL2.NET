using static SDL2.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies how 3D acceleration is used with SDL_GetWindowSurface(). <see cref="SDL_HINT_FRAMEBUFFER_ACCELERATION" href="https://wiki.libsdl.org/SDL_HINT_FRAMEBUFFER_ACCELERATION"/>
/// </summary>
/// <remarks>SDL can try to accelerate the screen surface returned by SDL_GetWindowSurface() by using streaming textures with a 3D rendering engine. This variable controls whether and how this is done.</remarks>
public sealed class FramebufferAcceleration : Hint
{
    internal FramebufferAcceleration() : base(SDL_HINT_FRAMEBUFFER_ACCELERATION) { }
    public bool SetPriority(FramebufferAccelerationRenderer renderer, HintPriority priority)
        => Set(RendererToString(renderer), priority);

    public FramebufferAccelerationRenderer Renderer
    {
        get => StringToRenderer(Get());
        set => Set(RendererToString(value));
    }

    private static string RendererToString(FramebufferAccelerationRenderer renderer)
        => renderer switch
        {
            FramebufferAccelerationRenderer.Disabled => "0",
            FramebufferAccelerationRenderer.Default => "1",
            FramebufferAccelerationRenderer.Direct3D => "direct3d",
            FramebufferAccelerationRenderer.OpenGL => "opengl",
            FramebufferAccelerationRenderer.OpenGLES => "opengles",
            FramebufferAccelerationRenderer.Metal => "metal",
            FramebufferAccelerationRenderer.Vulkan => "vulkan",
            _ => throw new NotSupportedException()
        };

    private static FramebufferAccelerationRenderer StringToRenderer(string? renderer)
        => renderer switch
        {
            "0" => FramebufferAccelerationRenderer.Disabled,
            "1" => FramebufferAccelerationRenderer.Default,
            "direct3d" => FramebufferAccelerationRenderer.Direct3D,
            "opengl" => FramebufferAccelerationRenderer.OpenGL,
            "opengles" => FramebufferAccelerationRenderer.OpenGLES,
            "metal" => FramebufferAccelerationRenderer.Metal,
            "vulkan" => FramebufferAccelerationRenderer.Vulkan,
            _ => throw new InvalidOperationException($"Unrecognized string '{renderer}' for renderer")
        };
}

public enum FramebufferAccelerationRenderer
{
    Disabled,
    Default,
    Direct3D,
    OpenGL,
    OpenGLES,
    Metal,
    Vulkan
}
