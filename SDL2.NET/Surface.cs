using SDL2.Bindings;
using SDL2.NET.Exceptions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// An object that contains a collection of pixels used in software blitting. <see href="https://wiki.libsdl.org/SDL_Surface"/>
/// </summary>
public class Surface : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, WeakReference<Surface>> _handleDict = new(2, 10);

    protected internal readonly IntPtr _handle;

    internal static Surface FetchOrNew(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : new(handle);

    private SDL_Surface ____back;
    private bool isBacked = false;
    protected internal SDL_Surface BackingStruct
    {
        get
        {
            if (isBacked)
                return ____back;
            ____back = Marshal.PtrToStructure<SDL_Surface>(_handle);
            isBacked = true;
            return ____back;
        }
    }

    protected internal void InvalidateBackingStruct()
        => isBacked = false;

    internal Surface(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLSurfaceCreationException(SDL_GetAndClearError());

        var str = BackingStruct;
        Format = PixelFormatData.FetchOrNew(str.format);
        Pitch = str.pitch;
        Size = new(str.w, str.h);
    }

    /// <summary>
    /// Creates a new RGB surface
    /// </summary>
    /// <param name="width">The width of the surface</param>
    /// <param name="height">The Height of the surface</param>
    /// <param name="depth">The Depth of the surface in bits</param>
    /// <param name="rmask">The red mask for the pixels</param>
    /// <param name="gmask">The green mask for the pixels</param>
    /// <param name="bmask">The blue mask for the pixels</param>
    /// <param name="amask">The alpha mask for the pixels</param>
    public Surface(int width, int height, int depth, uint rmask, uint gmask, uint bmask, uint amask, PixelFormat? format = null)
        : this(SDL_CreateRGBSurface(0, width, height, depth, rmask, gmask, bmask, amask)) { }

    /// <summary>
    /// Creates a new RGB surface. <see cref="SDL_CreateRGBSurfaceWithFormat" href="https://wiki.libsdl.org/SDL_CreateRGBSurfaceWithFormat"/>
    /// </summary>
    /// <param name="width">The width of the surface</param>
    /// <param name="height">The Height of the surface</param>
    /// <param name="depth">The Depth of the surface in bits</param>
    /// <param name="format">The pixel format to use</param>
    public Surface(int width, int height, int depth, PixelFormat format)
        : this(SDL_CreateRGBSurfaceWithFormat(0, width, height, depth, (uint)format)) { }

#warning missing https://wiki.libsdl.org/SDL_CreateRGBSurfaceFrom

    /// <summary>
    /// The format of the pixels stored in the surface
    /// </summary>
    public PixelFormatData Format { get; }

    /// <summary>
    /// The length of a row of pixels in <see cref="byte"/>s
    /// </summary>
    public int Pitch { get; }

    /// <summary>
    /// The width and height in pixels
    /// </summary>
    public Size Size { get; }

    /// <summary>
    /// Represents user-defined Data that can be tied to this surface
    /// </summary>
    public UserData? UserData { get; set; }

    /// <summary>
    /// Copy an existing <see cref="Surface"/> to a new <see cref="Surface"/> of the specified format. <see cref="SDL_ConvertSurfaceFormat" href="https://wiki.libsdl.org/SDL_ConvertSurfaceFormat"/>
    /// </summary>
    /// <param name="format">the <see cref="PixelFormat"/> that the new surface is optimized for</param>
    /// <returns>Returns the new <see cref="Surface"/> structure that is created</returns>
    public Surface Convert(PixelFormat format) 
        => new(SDL_ConvertSurfaceFormat(_handle, (uint)format, 0));

    /// <summary>
    /// Copy an existing <see cref="Surface"/> to a new <see cref="Surface"/> of the specified format. <see cref="SDL_ConvertSurfaceFormat" href="https://wiki.libsdl.org/SDL_ConvertSurfaceFormat"/>
    /// </summary>
    /// <param name="format">the <see cref="PixelFormat"/> that the new surface is optimized for</param>
    /// <returns>Returns the new <see cref="Surface"/> structure that is created</returns>
    public Surface Convert(PixelFormatData format)
        => new(SDL_ConvertSurface(_handle, format._handle, 0));

    /// <summary>
    /// Perform a fast fill of a <see cref="Rectangle"/> with a specific <see cref="RGBAColor"/>.
    /// </summary>
    /// /// <remarks>If there is a <see cref="Clip"/> is set, then this method will fill based on the intersection of the <see cref="Clip"/> <see cref="Rectangle"/> and <paramref name="rect"/>.</remarks>
    /// <param name="rect">The <see cref="Rectangle"/> representing the area to fill, or <see cref="null"/> to fill the entire surface</param>
    /// <param name="color">The color to fill with</param>
    public void FillRectangle(RGBAColor color, Rectangle? rect = null)
    {
        ThrowIfDisposed();
        if (rect is Rectangle re)
        {
            re.ToSDL(out var r);
            SDLSurfaceException.ThrowIfLessThan(SDL_FillRect(_handle, ref r, color.ToUInt32(Format)), 0);
        }
        SDLSurfaceException.ThrowIfLessThan(SDL_FillRect(_handle, IntPtr.Zero, color.ToUInt32(Format)), 0);
    }

    /// <summary>
    /// Performs a fast surface copy to a destination surface. <see cref="SDL_Blit" href="https://wiki.libsdl.org/SDL_Blit"/>
    /// </summary>
    /// <remarks>You should call <see cref="Blit"/> unless you know exactly how SDL blitting works internally and how to use the other blit functions. The blit function should not be called on a locked <see cref="Surface"/>.</remarks>
    /// <param name="destination">The blit target</param>
    /// <param name="sourceRect">The <see cref="Rectangle"/> representing the area to be copied, or <see cref="null"/> to copy the entire surface</param>
    /// <param name="destinationRect">The <see cref="Rectangle"/> representing the area to be copied into, or <see cref="null"/> to copy the entire surface. <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> are ignored, and only <see cref="Rectangle.X"/> and <see cref="Rectangle.Y"/> are taken into account</param>
    public void BlitTo(Surface destination, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        try
        {
            if (sourceRect is Rectangle src)
            {
                srect = Marshal.AllocHGlobal(Marshal.SizeOf(src));
                Marshal.StructureToPtr(src, srect, false);
            }

            if (destinationRect is Rectangle dst)
            {
                drect = Marshal.AllocHGlobal(Marshal.SizeOf(dst));
                Marshal.StructureToPtr(dst, drect, false);
            }

            SDLTextureException.ThrowIfLessThan(SDL_BlitSurface(_handle, srect, destination._handle, drect), 0);
        }
        finally
        {
            if (srect != IntPtr.Zero)
                Marshal.FreeHGlobal(srect);
            if (drect != IntPtr.Zero)
                Marshal.FreeHGlobal(drect);
        }
    }

    /// <summary>
    /// Performs a scaled surface copy to a destination surface. <see cref="SDL_BlitScaled" href="https://wiki.libsdl.org/SDL_BlitScaled"/>
    /// </summary>
    /// <param name="destination">The blit target</param>
    /// <param name="sourceRect">The <see cref="Rectangle"/> representing the area to be copied, or <see cref="null"/> to copy the entire surface</param>
    /// <param name="destinationRect">The <see cref="Rectangle"/> representing the area to be copied into, or <see cref="null"/> to copy the entire surface.</param>
    public void BlitScaledTo(Surface destination, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
    {
        ThrowIfDisposed();
        IntPtr srect = IntPtr.Zero;
        IntPtr drect = IntPtr.Zero;

        try
        {
            if (sourceRect is Rectangle src)
            {
                srect = Marshal.AllocHGlobal(Marshal.SizeOf(src));
                Marshal.StructureToPtr(src, srect, false);
            }

            if (destinationRect is Rectangle dst)
            {
                drect = Marshal.AllocHGlobal(Marshal.SizeOf(dst));
                Marshal.StructureToPtr(dst, drect, false);
            }

            SDLTextureException.ThrowIfLessThan(SDL_BlitScaled(_handle, srect, destination._handle, drect), 0);
        }
        finally
        {
            if (srect != IntPtr.Zero)
                Marshal.FreeHGlobal(srect);
            if (drect != IntPtr.Zero)
                Marshal.FreeHGlobal(drect);
        }
    }

    /// <summary>
    /// Performs a fast surface copy from a source surface. <see cref="SDL_Blit" href="https://wiki.libsdl.org/SDL_Blit"/>
    /// </summary>
    /// <remarks>You should call <see cref="Blit"/> unless you know exactly how SDL blitting works internally and how to use the other blit functions. The blit function should not be called on a locked <see cref="Surface"/>.</remarks>
    /// <param name="destination">The blit source</param>
    /// <param name="sourceRect">The <see cref="Rectangle"/> representing the area to be copied, or <see cref="null"/> to copy the entire surface</param>
    /// <param name="destinationRect">The <see cref="Rectangle"/> representing the area to be copied into, or <see cref="null"/> to copy the entire surface. <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> are ignored, and only <see cref="Rectangle.X"/> and <see cref="Rectangle.Y"/> are taken into account</param>
    public void BlitFrom(Surface source, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
        => source.BlitTo(this, sourceRect, destinationRect);

    /// <summary>
    /// Performs a scaled surface copy from a source surface. <see cref="SDL_BlitScaled" href="https://wiki.libsdl.org/SDL_BlitScaled"/>
    /// </summary>
    /// <param name="destination">The blit source</param>
    /// <param name="sourceRect">The <see cref="Rectangle"/> representing the area to be copied, or <see cref="null"/> to copy the entire surface</param>
    /// <param name="destinationRect">The <see cref="Rectangle"/> representing the area to be copied into, or <see cref="null"/> to copy the entire surface.</param>
    public void BlitScaledFrom(Surface source, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
        => source.BlitScaledTo(this, sourceRect, destinationRect);

    /// <summary>
    /// Perform a fast fill of a set of <see cref="Rectangle"/>s with a specific <see cref="RGBAColor"/>. <see cref="SDL_FillRects" href="https://wiki.libsdl.org/SDL_FillRects"/>
    /// </summary>
    /// <remarks>If there is a <see cref="Clip"/> is set, then this method will fill based on the intersection of the <see cref="Clip"/> <see cref="Rectangle"/> and <paramref name="rectangles"/>.</remarks>
    /// <param name="rectangles">The <see cref="Rectangle"/>s to fill in this <see cref="Surface"/></param>
    /// <param name="color">The <see cref="RGBAColor"/> to fill with</param>
    public void FillRectangles(ReadOnlySpan<Rectangle> rectangles, RGBAColor color)
    {
        ThrowIfDisposed();
        Span<SDL_Rect> rects = stackalloc SDL_Rect[rectangles.Length];
        for (int i = 0; i < rectangles.Length; i++)
            rectangles[i].ToSDL(out rects[i]);
        
        SDLSurfaceException.ThrowIfLessThan(SDL_FillRects(_handle, rects, rects.Length, color.ToUInt32(Format)), 0);
    }

    /// <summary>
    /// Get the color key (transparent pixel) for a surface. get: <see cref="SDL_GetColorKey" href="https://wiki.libsdl.org/SDL_GetColorKey"/>; set: <see cref="SDL_SetColorKey" href="https://wiki.libsdl.org/SDL_SetColorKey"/>
    /// </summary>
    /// <remarks>The color key defines a pixel value that will be treated as transparent in a blit. For example, one can use this to specify that cyan pixels should be considered transparent, and therefore not rendered. RLE acceleration can substantially speed up blitting of images with large horizontal runs of transparent pixels. See <see cref="SetRLE"/></remarks>
    public RGBColor? ColorKey
    {
        get
        {
            ThrowIfDisposed();
            int r = SDL_GetColorKey(_handle, out uint key);
            if (r is -1)
                return null;
            SDLSurfaceException.ThrowIfLessThan(r, 0);
            return RGBColor.FromUInt32(key, Format);
        }
        set
        {
            ThrowIfDisposed();
            SDLSurfaceException.ThrowIfLessThan(
                value is RGBColor color ? 
                    SDL_SetColorKey(_handle, (int)SDL_bool.SDL_TRUE, color.ToUInt32(Format)) : 
                    SDL_SetColorKey(_handle, (int)SDL_bool.SDL_FALSE, 0), 
                0
            );
        }
    }

    /// <summary>
    /// The additional alpha value used in blit operations. get: <see cref="SDL_GetSurfaceAlphaMod" href="https://wiki.libsdl.org/SDL_GetSurfaceAlphaMod"/>; set: <see cref="SDL_SetSurfaceAlphaMod" href="https://wiki.libsdl.org/SDL_SetSurfaceAlphaMod"/>
    /// </summary>
    /// <remarks>When this surface is blitted, during the blit operation the source alpha value is modulated by this alpha value according to the following formula: source.Alpha *= (Alpha / 255)</remarks>
    public byte Alpha
    {
        get
        {
            SDLSurfaceException.ThrowIfLessThan(SDL_GetSurfaceAlphaMod(_handle, out var alpha), 0);
            return alpha;
        }
        set
        {
            SDLSurfaceException.ThrowIfLessThan(SDL_SetSurfaceAlphaMod(_handle, value), 0);
        }
    }

    /// <summary>
    /// The blend mode used for blit operations. get: <see cref="SDL_GetSurfaceBlendMode" href="https://wiki.libsdl.org/SDL_GetSurfaceBlendMode"/>; set: <see cref="SDL_SetSurfaceBlendMode" href="https://wiki.libsdl.org/SDL_SetSurfaceBlendMode"/>
    /// </summary>
    /// <remarks>To copy a <see cref="Surface"/> to another <see cref="Surface"/> (or <see cref="Texture"/>) without blending with the existing data, the blendmode of the SOURCE surface should be set to <see cref="BlendMode.None"/>.</remarks>
    public BlendMode BlendMode
    {
        get
        {
            SDLSurfaceException.ThrowIfLessThan(SDL_GetSurfaceBlendMode(_handle, out var blendMode), 0);
            return (BlendMode)blendMode;
        }
        set
        {
            SDLSurfaceException.ThrowIfLessThan(SDL_SetSurfaceBlendMode(_handle, (SDL_BlendMode)value), 0);
        }
    }

    /// <summary>
    /// The additional color value multiplied into blit operations. get: <see cref="SDL_GetSurfaceColorMod" href="https://wiki.libsdl.org/SDL_GetSurfaceColorMod"/>; set: <see cref="SDL_SetSurfaceColorMod" href="https://wiki.libsdl.org/SDL_SetSurfaceColorMod"/>
    /// </summary>
    /// <remarks>
    /// When this surface is blitted, during the blit operation each source color channel is modulated by the appropriate color value according to the following formula: source.Color = source.Color * (Color / 255)
    /// </remarks>
    public RGBColor Color
    {
        get
        {
            SDLSurfaceException.ThrowIfLessThan(SDL_GetSurfaceColorMod(_handle, out var r, out var g, out var b), 0);
            return new(r, g, b);
        }
        set
        {
            SDLSurfaceException.ThrowIfLessThan(SDL_SetSurfaceColorMod(_handle, value.Red, value.Green, value.Blue), 0);
        }
    }

    /// <summary>
    /// <see cref="SDL_LockSurface" href="https://wiki.libsdl.org/SDL_LockSurface"/>
    /// </summary>
    public void Lock()
    {
        throw new NotImplementedException();
#warning Not Implemented
    }

    /// <summary>
    /// Release a surface after directly accessing the pixels. <see cref="SDL_UnlockSurface" href="https://wiki.libsdl.org/SDL_UnlockSurface"/>
    /// </summary>
    public void Unlock()
        => SDL_UnlockSurface(_handle);

    /// <summary>
    /// Perform low-level surface blitting only. <see cref="SDL_LowerBlit" href="https://wiki.libsdl.org/SDL_LowerBlit"/>
    /// </summary>
    /// <remarks>This is a semi-private blit function and it performs low-level surface blitting, assuming the input rectangles have already been clipped. Unless you know what you're doing, you should be using <see cref="BlitTo"/> instead.</remarks>
    public void LowerBlitTo(Surface destination, Rectangle destinationRect, Rectangle? sourceRect = null)
    {
        ThrowIfDisposed();

        destinationRect.ToSDL(out var d);

        if(sourceRect is Rectangle src)
        {
            src.ToSDL(out var r);
            SDLTextureException.ThrowIfLessThan(SDL_LowerBlit(_handle, ref r, destination._handle, ref d), 0);
        }
        else
            SDLTextureException.ThrowIfLessThan(SDL_LowerBlit(_handle, IntPtr.Zero, destination._handle, ref d), 0);
    }

    /// <summary>
    /// Perform low-level surface blitting only. <see cref="SDL_LowerBlit" href="https://wiki.libsdl.org/SDL_LowerBlit"/>
    /// </summary>
    /// <remarks>This is a semi-private blit function and it performs low-level surface blitting, assuming the input rectangles have already been clipped. Unless you know what you're doing, you should be using <see cref="BlitFrom"/> instead.</remarks>
    public void LowerBlitFrom(Surface source, Rectangle destinationRect, Rectangle? sourceRect = null)
        => source.LowerBlitTo(this, destinationRect, sourceRect);

    /// <summary>
    /// Load a BMP image from a file path. <see cref="SDL_LoadBMP" href="https://wiki.libsdl.org/SDL_LoadBMP"/>
    /// </summary>
    public static Surface LoadBMP(string file)
        => new(SDL_LoadBMP(file));

    /// <summary>
    /// Save a surface to a BMP file. <see cref="SDL_SaveBMP" href="https://wiki.libsdl.org/SDL_SaveBMP"/>
    /// </summary>
    /// <remarks>Surfaces with a 24-bit, 32-bit and paletted 8-bit format get saved in the BMP directly. Other RGB formats with 8-bit or higher get converted to a 24-bit surface or, if they have an alpha mask or a colorkey, to a 32-bit surface before they are saved. YUV and paletted 1-bit and 4-bit formats are not supported.</remarks>
    public void SaveBMP(string file)
    {
        ThrowIfDisposed();
        SDLSurfaceException.ThrowIfLessThan(SDL_SaveBMP(_handle, file), 0);
    }

    /// <summary>
    /// Set the RLE acceleration hint for a surface. <see cref="SDL_SetSurfaceRLE" href="https://wiki.libsdl.org/SDL_SetSurfaceRLE"/>
    /// </summary>
    /// <remarks>If RLE is enabled, color key and alpha blending blits are much faster, but the surface must be locked before directly accessing the pixels. Since the wiki page is unclear, I'll go for an int for now just to conform to SDL. If a bool would suffice, let me know or PR</remarks>
    public void SetRLE(int flag)
    {
        ThrowIfDisposed();
        InvalidateBackingStruct();
        SDLSurfaceException.ThrowIfLessThan(SDL_SetSurfaceRLE(_handle, flag), 0);
    }

    /// <summary>
    /// Whether a surface must be locked for access. <see cref="SDL_MUSTLOCK" href="https://wiki.libsdl.org/SDL_MUSTLOCK"/>
    /// </summary>
    public bool MustLock => (BackingStruct.flags & SDL_RLEACCEL) != 0;

    /// <summary>
    /// Perform a fast, low quality, stretch blit between two surfaces of the same format. <see cref="SDL_SoftStretch" href="https://wiki.libsdl.org/SDL_SoftStretch"/>
    /// </summary>
    /// <remarks>Unless you know exactly what this does and why you're using it, use <see cref="BlitScaledTo"/> instead</remarks>
    public void SoftStretchTo(Surface destination, Rectangle sourceRect, Rectangle destinationRect)
    {
        sourceRect.ToSDL(out var src);
        destinationRect.ToSDL(out var dst);

        SDLSurfaceException.ThrowIfLessThan(SDL_SoftStretch(_handle, ref src, destination._handle, ref dst), 0);
    }

    /// <summary>
    /// Perform a fast, low quality, stretch blit between two surfaces of the same format. <see cref="SDL_SoftStretch" href="https://wiki.libsdl.org/SDL_SoftStretch"/>
    /// </summary>
    /// <remarks>Unless you know exactly what this does and why you're using it, use <see cref="BlitScaledFrom"/> instead</remarks>
    public void SoftStretchFrom(Surface source, Rectangle sourceRect, Rectangle destinationRect)
        => source.SoftStretchTo(this, sourceRect, destinationRect);

    /// <summary>
    /// The clipping rectangle for a surface. get: <see cref="SDL_GetClipRect" href="https://wiki.libsdl.org/SDL_GetClipRect"/>; set: <see cref="SDL_SetClipRect" href="https://wiki.libsdl.org/SDL_SetClipRect"/>
    /// </summary>
    /// <remarks>When <see cref="this"/> <see cref="Surface"/> is the destination of a blit, only the area within the clip rectangle is drawn into.</remarks>
    public Rectangle? Clip
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetClipRect(_handle, out var rect);
            Rectangle r = (Rectangle)rect;
            return r.Size != default ? r : null;
        }
        set
        {
            ThrowIfDisposed();
            if (value is Rectangle r)
            {
                r.ToSDL(out var clipRect);
                ClipStatus = SDL_SetClipRect(_handle, ref clipRect) is SDL_bool.SDL_TRUE ? ClipRectangleStatus.Enabled : ClipRectangleStatus.Invalid;
                return;
            }
            SDL_SetClipRect(_handle, IntPtr.Zero);
            ClipStatus = ClipRectangleStatus.Disabled;
        }
    }
    private SDL_Rect clipRect;

    /// <summary>
    /// Represents the current status of <see cref="Clip"/>
    /// </summary>
    /// <remarks>Not an SDL property, represents information that could have been missed from <see cref="set_Clip"/>. May not represent the actual status of the backing <see cref="SDL_Surface"/> until the first time <see cref="Clip"/> is set</remarks>
    public ClipRectangleStatus ClipStatus { get; private set; }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_FreeSurface(_handle);
            _handleDict.TryRemove(_handle, out _);
            disposedValue = true;
        }
    }

    ~Surface()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Surface));
    }

    #endregion
}
