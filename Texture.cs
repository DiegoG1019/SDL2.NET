using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents an SDL Texture: a structure that contains an efficient, driver-specific representation of pixel data. <see cref="SDL_Texture" href="https://wiki.libsdl.org/SDL_Texture"/>
/// </summary>
public class Texture : IDisposable
{
    protected internal readonly IntPtr _handle;

    /// <summary>
    /// The renderer that was hooked to this Texture when it was created
    /// </summary>
    public Renderer Renderer { get; }

    /// <summary>
    /// Create a <see cref="Texture"/> for a rendering context. <see cref="SDL_CreateTexture" href="https://wiki.libsdl.org/SDL_CreateTexture"/>
    /// </summary>
    /// <remarks>You can set the <see cref="Texture"/> scaling method by setting <see cref="HintTypes.RenderScaleQuality"/> before creating the <see cref="Texture"/>.</remarks>
    /// <param name="renderer">The <see cref="Renderer"/> context to be used for this <see cref="Texture"/></param>
    /// <param name="format">The <see cref="PixelFormat"/> for this <see cref="Texture"/></param>
    /// <param name="access">The <see cref="TextureAccess"/> for this <see cref="Texture"/></param>
    /// <param name="width">The width of the <see cref="Texture"/> in pixels</param>
    /// <param name="height">The height of the <see cref="Texture"/> in pixels</param>
    /// <exception cref="SDLTextureCreationException"></exception>
    public Texture(Renderer renderer, PixelFormat format, TextureAccess access, int width, int height)
    {
        _handle = SDL_CreateTexture(renderer._handle, (uint)format, (int)access, width, height);
        if (_handle == IntPtr.Zero)
            throw new SDLTextureCreationException(SDL_GetError());
        Renderer = renderer;
    }

    /// <summary>
    /// Create a <see cref="Texture"/> from an existing <see cref="Surface"/>. <see cref="SDL_CreateTextureFromSurface" href="https://wiki.libsdl.org/SDL_CreateTextureFromSurface"/>
    /// </summary>
    /// <remarks>The <see cref="Surface"/> is not modified or freed by this method. The <see cref="TextureAccess"/> for the created texture is <see cref="TextureAccess.Static"/>.</remarks>
    /// <param name="renderer">The rendering context</param>
    /// <param name="surface">The <see cref="Surface"/> containing pixel data used to fill the <see cref="Texture"/></param>
    /// <exception cref="SDLTextureCreationException"></exception>
    public Texture(Renderer renderer, Surface surface)
    {
        _handle = SDL_CreateTextureFromSurface(renderer._handle, surface._handle);
        if (_handle == IntPtr.Zero)
            throw new SDLTextureCreationException(SDL_GetError());
        Renderer = renderer;
    }

    /// <summary>
    /// The blend mode used for texture copy operations. get: <see cref="SDL_GetTextureBlendMode" href="https://wiki.libsdl.org/SDL_GetTextureBlendMode"/>; set: <see cref="SDL_SetTextureBlendMode" href="https://wiki.libsdl.org/SDL_SetTextureBlendMode"/>
    /// </summary>
    public BlendMode Blend
    {
        get
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_GetTextureBlendMode(_handle, out SDL_BlendMode blendMode), 0);
            return (BlendMode)blendMode;
        }
        set
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_SetTextureBlendMode(_handle, (SDL_BlendMode)value), 0);
        }
    }

    /// <summary>
    /// The additional alpha value multiplied into render copy operations. get: <see cref="SDL_GetTextureAlphaMod" href="https://wiki.libsdl.org/SDL_GetTextureAlphaMod"/>; set: <see cref="SDL_SetTextureAlphaMod" href="https://wiki.libsdl.org/SDL_SetTextureAlphaMod"/>
    /// </summary>
    public byte Alpha
    {
        get
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_GetTextureAlphaMod(_handle, out byte mod), 0);
            return mod;
        }
        set
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_SetTextureAlphaMod(_handle, value), 0);
        }
    }

    /// <summary>
    /// The additional color value multiplied into render copy operations. get: <see cref="SDL_GetTextureColorMod" href="https://wiki.libsdl.org/SDL_GetTextureColorMod"/>; set: <see cref="SDL_SetTextureColorMod" href="https://wiki.libsdl.org/SDL_SetTextureColorMod"/>
    /// </summary>
    public RGBColor Color
    {
        get
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_GetTextureColorMod(_handle, out byte r, out byte g, out byte b), 0);
            return new(r, g, b);
        }
        set
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_SetTextureColorMod(_handle, value.Red, value.Green, value.Blue), 0);
        }
    }

    /// <summary>
    /// Manages this texture's color and alpha channel simultaneously. <see cref="SDL_SetTextureColorMod" href="https://wiki.libsdl.org/SDL_SetTextureColorMod"/> <see cref="SDL_SetTextureAlphaMod" href="https://wiki.libsdl.org/SDL_SetTextureAlphaMod"/>
    /// </summary>
    public RGBAColor ColorAlpha
    {
        get
        {
            ThrowIfDisposed();
            return new(Color, Alpha);
        }

        set
        {
            ThrowIfDisposed();
            Alpha = value.Alpha;
            Color = (RGBColor)value;
        }
    }

    /// <summary>
    /// The actual access to the texture. <see cref="SDL_QueryTexture" href="https://wiki.libsdl.org/SDL_QueryTexture"/>; 
    /// </summary>
    public TextureAccess Access
    {
        get
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_QueryTexture(_handle, out _, out int access, out _, out _), 0);
            return (TextureAccess)access;
        }
    }

    /// <summary>
    /// The raw format of the texture; the actual format may differ, but pixel transfers will use this format. <see cref="SDL_QueryTexture" href="https://wiki.libsdl.org/SDL_QueryTexture"/>; 
    /// </summary>
    public PixelFormat Format
    {
        get
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_QueryTexture(_handle, out uint format, out _, out _, out _), 0);
            return (PixelFormat)format;
        }
    }

    /// <summary>
    /// The Width and Height of the texture in pixels. <see cref="SDL_QueryTexture" href="https://wiki.libsdl.org/SDL_QueryTexture"/>; 
    /// </summary>
    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_QueryTexture(_handle, out _, out _, out int w, out int h), 0);
            return new(w, h);
        }
    }

    /// <summary>
    /// Lock a portion of the texture for write-only pixel access. The texture must have been created with <see cref="TextureAccess.Streaming"/>. <see cref="SDL_LockTexture" href="https://wiki.libsdl.org/SDL_LockTexture"/>
    /// </summary>
    /// <remarks>As an optimization, the pixels made available for editing don't necessarily contain the old texture data. This is a write-only operation, and if you need to keep a copy of the texture data you should do that at the application level. You must use SDL_UnlockTexture() to unlock the pixels and apply any changes.</remarks>
    public void Lock(Rectangle? area)
    {
        ThrowIfDisposed();
        throw new NotImplementedException();
        if (Access is not TextureAccess.Streaming)
            throw new InvalidOperationException("Cannot lock a texture that was not created with TextureAccess.Streaming");

        var pixels = IntPtr.Zero;
#warning I have NO idea what to do about pixels**. https://gamedev.stackexchange.com/questions/175341/what-do-i-provide-to-sdl-locktexture-pixels
#warning more docs https://slouken.blogspot.com/2011/02/streaming-textures-with-sdl-13.html

        if (area is null)
        {
            SDLTextureException.ThrowIfLessThan(SDL_LockTexture(_handle, IntPtr.Zero, out pixels, out _), 0);
            return;
        }

        SDL_Rect rect = default;
        ((Rectangle)area).ToSDLRect(ref rect);
        SDLTextureException.ThrowIfLessThan(SDL_LockTexture(_handle, ref rect, out pixels, out _), 0);
    }

    /// <summary>
    /// Unlock a texture, uploading the changes to video memory, if needed. <see cref="SDL_UnlockTexture" href="https://wiki.libsdl.org/SDL_UnlockTexture"/>
    /// </summary>
    /// <remarks>Warning: Please note that <see cref="Lock"/> is intended to be write-only; it will not guarantee the previous contents of the texture will be provided. You must fully initialize any area of a texture that you lock before unlocking it, as the pixels might otherwise be uninitialized memory. Which is to say: locking and immediately unlocking a texture can result in corrupted textures, depending on the renderer in use.</remarks>
    public void Unlock()
    {
        ThrowIfDisposed();
        throw new NotImplementedException();
        SDL_UnlockTexture(_handle);
    }

    /// <summary>
    /// This feature is NOT IMPLEMENTED. DO NOT USE IT.
    /// </summary>
    /// <remarks>This will attempt to copy the current texture into a new one set as a RenderTarget. If that is not supported, this method will try the MUCH slower approach of drawing to a renderer and read the pixels back into a new texture.</remarks>
    /// <returns>A copy of the current texture</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Texture Duplicate()
    {
        ThrowIfDisposed();
        throw new NotImplementedException();
    }

    /// <summary>
    /// Update the given texture rectangle with new pixel data. <see cref="SDL_UpdateTexture" href="https://wiki.libsdl.org/SDL_UpdateTexture"/>
    /// </summary>
    /// <remarks>The pixel data must be in the <see cref="PixelFormat"/> of the <see cref="Texture"/>. Use <see cref="Format"/> to query the <see cref="PixelFormat"/> of the <see cref="Texture"/>. This is a fairly slow function, intended for use with static <see cref="Texture"/>s that do not change often. If the <see cref="Texture"/> is intended to be updated often, it is preferred to create the <see cref="Texture"/> as streaming and use the locking functions referenced below. While this function will work with streaming <see cref="Texture"/>s, for optimization reasons you may not get the pixels back if you lock the <see cref="Texture"/> afterward.</remarks>
    public void Update(Rectangle? rectangle, object pixels, int pitch)
    {
        ThrowIfDisposed();
        throw new NotImplementedException();
    }

    /// <summary>
    /// Copy a portion of the texture to the current rendering target. <see cref="SDL_RenderCopy" href="https://wiki.libsdl.org/SDL_RenderCopy"/>
    /// </summary>
    /// <remarks>The texture is blended with the destination based on its blend mode, color modulation and alpha modulation set with <see cref="Blend"/>, <see cref="Color"/>, and <see cref="Alpha"/> respectively.</remarks>
    /// <param name="source">The source rectangle for this operation. The rectangle will be used to capture a portion of the texture. Set to <see cref="null"/> to use the entire texture.</param>
    /// <param name="destination">The destination rectangle for this operation. The texture will be stretched to fill the given rectangle. Set to <see cref="null"/> to fill the entire rendering target</param>
    public void Render(Rectangle? source = null, Rectangle? destination = null)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        try
        {
            if (source is Rectangle src)
            {
                srect = Marshal.AllocHGlobal(Marshal.SizeOf(src));
                Marshal.StructureToPtr(src, srect, false);
            }

            if (destination is Rectangle dst)
            {
                drect = Marshal.AllocHGlobal(Marshal.SizeOf(dst));
                Marshal.StructureToPtr(dst, drect, false);
            }

            SDLTextureException.ThrowIfLessThan(SDL_RenderCopy(Renderer._handle, _handle, srect, drect), 0);
        }
        finally
        {
            if (srect != IntPtr.Zero) 
                Marshal.FreeHGlobal(srect);
            if (drect != IntPtr.Zero)
                Marshal.FreeHGlobal(drect);
        }
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_DestroyTexture(_handle);
            disposedValue = true;
        }
    }

    ~Texture()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Texture));
    }

    #endregion

}
