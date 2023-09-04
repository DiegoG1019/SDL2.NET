using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents an SDL Texture: a structure that contains an efficient, driver-specific representation of pixel data. <see cref="SDL_Texture" href="https://wiki.libsdl.org/SDL_Texture"/>
/// </summary>
public class Texture : IDisposable, IHandle
{
    IntPtr IHandle.Handle => _handle;
    internal readonly IntPtr _handle;
    private static readonly ConcurrentDictionary<IntPtr, WeakReference<Texture>> _textDict = new(2, 20);

    internal static Texture? FetchTexture(IntPtr handle)
    {
        if (_textDict.TryGetValue(handle, out var reference))
            if (reference.TryGetTarget(out var target))
                return target;
            else
                _textDict.TryRemove(handle, out _);
        return null;
    }

    /// <summary>
    /// The renderer that was hooked to this Texture when it was created
    /// </summary>
    public Renderer Renderer { get; }

    internal Texture(IntPtr handle, Renderer renderer)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLTextureCreationException(SDL_GetAndClearError());

        if (_textDict.TryAdd(_handle, new(this)) is false)
            throw new SDLTextureCreationException("Could not register this Texture's address in .NET (Since SDL sometimes returns pointers, and these objects are only in .NET, they need to be indexed). This is likely a problem with the library, please report this and your code on GitHub. If this texture was created succesfully, it has NOT been destroyed.");
        Renderer = renderer;
    }

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
        : this(SDL_CreateTexture(renderer._handle, (uint)format, (int)access, width, height), renderer) { }

    /// <summary>
    /// Create a <see cref="Texture"/> from an existing <see cref="Surface"/>. <see cref="SDL_CreateTextureFromSurface" href="https://wiki.libsdl.org/SDL_CreateTextureFromSurface"/>
    /// </summary>
    /// <remarks>The <see cref="Surface"/> is not modified or freed by this method. The <see cref="TextureAccess"/> for the created texture is <see cref="TextureAccess.Static"/>.</remarks>
    /// <param name="renderer">The rendering context</param>
    /// <param name="surface">The <see cref="Surface"/> containing pixel data used to fill the <see cref="Texture"/></param>
    /// <exception cref="SDLTextureCreationException"></exception>
    public Texture(Renderer renderer, Surface surface)
        : this(SDL_CreateTextureFromSurface(renderer._handle, surface._handle), renderer) { }

    /// <summary>
    /// Render a list of triangles, using <see cref="this"/> <see cref="Texture"/> and optionally <paramref name="indices"/> into the vertex array. Color and alpha modulation is done per vertex (<see cref="Color"/> and <see cref="Alpha"/> are ignored). <see cref="SDL_RenderGeometry"/>
    /// </summary>
    /// <remarks>See <see cref="Renderer.RenderGeometry(ReadOnlySpan{Vertex}, ReadOnlySpan{int})"/> to discard the use of a <see cref="Texture"/></remarks>
    /// <param name="vertices">The list of vertices to use when drawing the triangles</param>
    /// <param name="indices">The indices into the vertex array</param>
    public void RenderGeometry(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices = default)
        => Renderer.RenderGeometry(Renderer, this, vertices, indices);

    /// <summary>
    /// The blend mode used for texture copy operations. get: <see cref="SDL_GetTextureBlendMode" href="https://wiki.libsdl.org/SDL_GetTextureBlendMode"/>; set: <see cref="SDL_SetTextureBlendMode" href="https://wiki.libsdl.org/SDL_SetTextureBlendMode"/>
    /// </summary>
    public BlendMode BlendMode
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
        get => new(Color, Alpha);

        set
        {
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
    /// Get the scale mode used for texture scale operations. <see cref="SDL_GetTextureScaleMode" href="https://wiki.libsdl.org/SDL_GetTextureScaleMode"/> <see cref="SDL_SetTextureScaleMode" href="https://wiki.libsdl.org/SDL_SetTextureScaleMode"/>
    /// </summary>
    public ScaleMode ScaleMode
    {
        get
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_GetTextureScaleMode(_handle, out var v), 0);
            return (ScaleMode)v;
        }
        set
        {
            ThrowIfDisposed();
            SDLTextureException.ThrowIfLessThan(SDL_SetTextureScaleMode(_handle, (SDL_ScaleMode)value), 0);
        }
    }

    /// <summary>
    /// User defined Data attached to this Texture
    /// </summary>
    /// <remarks>
    /// This is not the same as the user data that could be set using <see cref="SDL_SetTextureUserData(IntPtr, IntPtr)"/>
    /// </remarks>
    public UserData? UserData { get; set; }

    /// <summary>
    /// Gets a <see cref="Rectangle"/> that is positioned at <paramref name="x"/> and <paramref name="y"/> and has the dimensions of the texture
    /// </summary>
    /// <param name="x">The x coordinate of the <see cref="Rectangle"/> to create</param>
    /// <param name="y">The y coordinate of the <see cref="Rectangle"/> to create</param>
    /// <returns>The resulting <see cref="Rectangle"/></returns>
    public Rectangle GetRectangle(int x, int y)
        => new(Size, x, y);

    /// <summary>
    /// Lock a portion of the texture for write-only pixel access. The texture must have been created with <see cref="TextureAccess.Streaming"/>. <see cref="SDL_LockTexture" href="https://wiki.libsdl.org/SDL_LockTexture"/>
    /// </summary>
    /// <remarks>As an optimization, the pixels made available for editing don't necessarily contain the old texture data. This is a write-only operation, and if you need to keep a copy of the texture data you should do that at the application level. You must use SDL_UnlockTexture() to unlock the pixels and apply any changes.</remarks>
    public unsafe void Lock(Rectangle? area, out Span<byte> pixels, out int pitch)
    {
        ThrowIfDisposed();
        if (Access is not TextureAccess.Streaming)
            throw new InvalidOperationException("Cannot lock a texture that was not created with TextureAccess.Streaming");

        IntPtr rect;
        int h, w;
        if (area is Rectangle r)
        {
            rect = (nint)Unsafe.AsPointer(ref r.ToSDLRef());
            h = r.Height;
            w = r.Width;
        }
        else
        {
            rect = IntPtr.Zero;
            h = Size.Height;
            w = Size.Width;
        }

        SDLTextureException.ThrowIfLessThan(SDL_LockTexture(_handle, rect, out var px, out pitch), 0);

        var bytes = new PixelFormatData(Format).BytesPerPixel;

        pixels = new Span<byte>((void*)px, bytes * h * w);
    }

    /// <summary>
    /// Unlock a texture, uploading the changes to video memory, if needed. <see cref="SDL_UnlockTexture" href="https://wiki.libsdl.org/SDL_UnlockTexture"/>
    /// </summary>
    /// <remarks>Warning: Please note that <see cref="Lock"/> is intended to be write-only; it will not guarantee the previous contents of the texture will be provided. You must fully initialize any area of a texture that you lock before unlocking it, as the pixels might otherwise be uninitialized memory. Which is to say: locking and immediately unlocking a texture can result in corrupted textures, depending on the renderer in use.</remarks>
    public void Unlock()
    {
        ThrowIfDisposed();
        SDL_UnlockTexture(_handle);
    }

    /// <summary>
    /// Update the given texture rectangle with new pixel data. <see cref="SDL_UpdateTexture" href="https://wiki.libsdl.org/SDL_UpdateTexture"/>
    /// </summary>
    /// <remarks>The pixel data must be in the <see cref="PixelFormat"/> of the <see cref="Texture"/>. Use <see cref="Format"/> to query the <see cref="PixelFormat"/> of the <see cref="Texture"/>. This is a fairly slow function, intended for use with static <see cref="Texture"/>s that do not change often. If the <see cref="Texture"/> is intended to be updated often, it is preferred to create the <see cref="Texture"/> as streaming and use the locking functions referenced below. While this function will work with streaming <see cref="Texture"/>s, for optimization reasons you may not get the pixels back if you lock the <see cref="Texture"/> afterward.</remarks>
    public unsafe void Update(Rectangle? rectangle, ReadOnlySpan<byte> pixels, int pitch)
    {
        ThrowIfDisposed();

        fixed (void* ptr = pixels)
        {
            if (rectangle is Rectangle rect)
                SDLTextureException.ThrowIfLessThan(SDL_UpdateTexture(_handle, ref rect.ToSDLRef(), (IntPtr)ptr, pitch), 0);
            else
                SDLTextureException.ThrowIfLessThan(SDL_UpdateTexture(_handle, IntPtr.Zero, (IntPtr)ptr, pitch), 0);
        }
    }

    /// <summary>
    /// Copy a portion of the texture to the current rendering target. <see cref="SDL_RenderCopy" href="https://wiki.libsdl.org/SDL_RenderCopy"/>
    /// </summary>
    /// <remarks>The texture is blended with the destination based on its blend mode, color modulation and alpha modulation set with <see cref="BlendMode"/>, <see cref="Color"/>, and <see cref="Alpha"/> respectively.</remarks>
    public void Render()
    {
        ThrowIfDisposed();
        SDLTextureException.ThrowIfLessThan(SDL_RenderCopy(Renderer._handle, _handle, IntPtr.Zero, IntPtr.Zero), 0);
    }

    /// <summary>
    /// Copy a portion of the texture to the current rendering target. <see cref="SDL_RenderCopy" href="https://wiki.libsdl.org/SDL_RenderCopy"/>
    /// </summary>
    /// <remarks>The texture is blended with the destination based on its blend mode, color modulation and alpha modulation set with <see cref="BlendMode"/>, <see cref="Color"/>, and <see cref="Alpha"/> respectively.</remarks>
    /// <param name="source">The source rectangle for this operation. The rectangle will be used to capture a portion of the texture. Set to null to use the entire texture.</param>
    /// <param name="destination">The destination rectangle for this operation. The texture will be stretched to fill the given rectangle. Set to null to fill the entire rendering target</param>
    public unsafe void Render(Rectangle? source, Rectangle? destination = null)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        if (source is Rectangle src)
            srect = (nint)Unsafe.AsPointer(ref src.ToSDLRef());

        if (destination is Rectangle dst)
            drect = (nint)Unsafe.AsPointer(ref dst.ToSDLRef());

        SDLTextureException.ThrowIfLessThan(SDL_RenderCopy(Renderer._handle, _handle, srect, drect), 0);
    }

    /// <summary>
    /// Copy a portion of the texture to the current rendering target. <see cref="SDL_RenderCopyEx" href="https://wiki.libsdl.org/SDL_RenderCopyEx"/>
    /// </summary>
    /// <remarks>The texture is blended with the destination based on its blend mode, color modulation and alpha modulation set with <see cref="BlendMode"/>, <see cref="Color"/>, and <see cref="Alpha"/> respectively.</remarks>
    /// <param name="source">The source rectangle for this operation. The rectangle will be used to capture a portion of the texture. Set to null to use the entire texture.</param>
    /// <param name="destination">The destination rectangle for this operation. The texture will be stretched to fill the given rectangle. Set to null to fill the entire rendering target</param>
    public unsafe void Render(Rectangle? source = null, Rectangle? destination = null, double angle = 0, Point center = default, Flip flip = Flip.None)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        if (source is Rectangle src)
            srect = (nint)Unsafe.AsPointer(ref src.ToSDLRef());

        if (destination is Rectangle dst)
            drect = (nint)Unsafe.AsPointer(ref dst.ToSDLRef());

        center.ToSDL(out var sdl_p);

        SDLTextureException.ThrowIfLessThan(SDL_RenderCopyEx(Renderer._handle, _handle, srect, drect, angle, ref sdl_p, (SDL_RendererFlip)flip), 0);
    }

    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel precision. <see cref="SDL_RenderCopyF" href="https://wiki.libsdl.org/SDL_RenderCopyF"/>
    /// </summary>
    /// <remarks>The texture is blended with the destination based on its blend mode, color modulation and alpha modulation set with <see cref="BlendMode"/>, <see cref="Color"/>, and <see cref="Alpha"/> respectively.</remarks>
    /// <param name="source">The source rectangle for this operation. The rectangle will be used to capture a portion of the texture. Set to null to use the entire texture.</param>
    /// <param name="destination">The destination rectangle for this operation. The texture will be stretched to fill the given rectangle. Set to null to fill the entire rendering target</param>
    public unsafe void Render(Rectangle? source, FRectangle? destination = null)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        if (source is Rectangle src)
            srect = (nint)Unsafe.AsPointer(ref src.ToSDLRef());

        if (destination is FRectangle dst)
            drect = (nint)Unsafe.AsPointer(ref dst.ToSDLRef());

        SDLTextureException.ThrowIfLessThan(SDL_RenderCopyF(Renderer._handle, _handle, srect, drect), 0);
    }

    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel precision. <see cref="SDL_RenderCopyExF" href="https://wiki.libsdl.org/SDL_RenderCopyExF"/>
    /// </summary>
    /// <remarks>The texture is blended with the destination based on its blend mode, color modulation and alpha modulation set with <see cref="BlendMode"/>, <see cref="Color"/>, and <see cref="Alpha"/> respectively.</remarks>
    /// <param name="source">The source rectangle for this operation. The rectangle will be used to capture a portion of the texture. Set to null to use the entire texture.</param>
    /// <param name="destination">The destination rectangle for this operation. The texture will be stretched to fill the given rectangle. Set to null to fill the entire rendering target</param>
    public unsafe void Render(Rectangle? source = null, FRectangle? destination = null, double angle = 0, FPoint center = default, Flip flip = Flip.None)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        if (source is Rectangle src)
            srect = (nint)Unsafe.AsPointer(ref src.ToSDLRef());

        if (destination is FRectangle dst)
            drect = (nint)Unsafe.AsPointer(ref dst.ToSDLRef());

        center.ToSDL(out var sdl_p);

        SDLTextureException.ThrowIfLessThan(SDL_RenderCopyExF(Renderer._handle, _handle, srect, drect, angle, ref sdl_p, (SDL_RendererFlip)flip), 0);
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_DestroyTexture(_handle);
            _textDict.Remove(_handle, out _);
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
