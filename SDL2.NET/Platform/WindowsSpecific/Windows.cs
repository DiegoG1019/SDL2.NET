using System.Runtime.Versioning;
using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Platform.WindowsSpecific;

/// <summary>
/// Provides access to SDL's Microsoft Windows specific functions
/// </summary>
[SupportedOSPlatform("Windows")]
public static class Windows
{
    /// <summary>
    /// Get the D3D9 device associated with a renderer.
    /// </summary>
    /// <param name="renderer">The renderer from which to get the associated D3D device</param>
    /// <remarks>
    /// This method returns an unmanaged object, which needs to be destroyed after finishing its use. SDL doesn't directly provide a way to manage these types of objects. It's up to you to understand how to use it. <see href="https://docs.microsoft.com/en-us/windows/win32/api/d3d9helper/nn-d3d9helper-idirect3ddevice9"/>
    /// </remarks>
    /// <returns>
    /// Returns the D3D9 device associated with given renderer. Throws if it is not a D3D9 renderer
    /// </returns>
    public static IntPtr GetD3D9Device(this Renderer renderer)
    {
        var ptr = SDL_RenderGetD3D9Device(renderer._handle);
        return ptr == IntPtr.Zero ? throw new SDLRendererException(SDL_GetAndClearError()) : ptr;
    }

    /// <summary>
    /// Get the D3D11 device associated with a renderer.
    /// </summary>
    /// <param name="renderer">The renderer from which to get the associated D3D device</param>
    /// <remarks>
    /// This method returns an unmanaged object, which needs to be destroyed after finishing its use. SDL doesn't directly provide a way to manage these types of objects. It's up to you to understand how to use it. <see href="https://docs.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11device"/>
    /// </remarks>
    /// <returns>
    /// Returns the D3D11 device associated with given renderer. Throws if it is not a D3D11 renderer
    /// </returns>
    public static IntPtr GetD3D11Device(this Renderer renderer)
    {
        var ptr = SDL_RenderGetD3D11Device(renderer._handle);
        return ptr == IntPtr.Zero ? throw new SDLRendererException(SDL_GetAndClearError()) : ptr;
    }
}
