using SDL2.NET.Font.Exceptions;
using static SDL2.Bindings.SDL_ttf;

namespace SDL2.NET.Font;

/// <summary>
/// Represents a True Type Font (TTF)
/// </summary>
public class TTFont : IDisposable
{
    private readonly IntPtr _handle;
    internal TTFont(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLFontException(TTF_GetError());
    }   

    public TTFont(string file, int size) : this(TTF_OpenFont(file, size))
    {
        _size = size;
    }

    public TTFont(string file, int size, long index) : this(TTF_OpenFontIndex(file, size, index))
    {
        _size = size;
    }

    private int _size;

    /// <summary>
    /// The current size of this font. get: Cached in .NET; set: <see cref="TTF_SetFontSize"/>
    /// </summary>
    public int Size
    {
        get => _size;
        set
        {
            ThrowIfDisposed();
            SDLFontException.ThrowIfLessThan(TTF_SetFontSize(_handle, value), 0);
            _size = value;
        }
    }

    public TTFStyle Style
    {
        get
        {
            ThrowIfDisposed();
            return (TTFStyle)TTF_GetFontStyle(_handle);
        }
        set
        {
            ThrowIfDisposed();
            TTF_SetFontStyle(_handle, (int)value);
        }
    }

    public int Outline
    {
        get
        {
            ThrowIfDisposed();
            return TTF_GetFontOutline(_handle);
        }
        set
        {
            ThrowIfDisposed();
            TTF_SetFontOutline(_handle, value);
        }
    }

    public TTFHinting Hinting
    {
        get
        {
            ThrowIfDisposed();
            return (TTFHinting)TTF_GetFontHinting(_handle);
        }
        set
        {
            ThrowIfDisposed();
            TTF_SetFontHinting(_handle, (int)value);
        }
    }

#warning This sounds like it should be a bool, but I'm not sure.
    public int Kerning
    {
        get
        {
            ThrowIfDisposed();
            return TTF_GetFontKerning(_handle);
        }
        set
        {
            ThrowIfDisposed();
            TTF_SetFontKerning(_handle, value);
        }
    }

    public int IsGlyphProvided(char character)
    {
        ThrowIfDisposed();
        return TTF_GlyphIsProvided(_handle, character);
    }

    /// <param name="character">The character to test. <see cref="char"/> can be implicitly casted (widened) to <see cref="uint"/></param>
    public int IsGlyphProvided32(uint character)
    {
        ThrowIfDisposed();
        return TTF_GlyphIsProvided32(_handle, character);
    }

    public GlyphMetrics GetGlyphMetrics(char character)
    {
        ThrowIfDisposed();
        SDLFontException.ThrowIfLessThan(TTF_GlyphMetrics(_handle, character, out int minx, out int maxx, out int miny, out int maxy, out int advance), 0);
        return new GlyphMetrics(minx, miny, maxx, maxy, advance);
    }

    /// <param name="character">The character to test. <see cref="char"/> can be implicitly casted (widened) to <see cref="uint"/></param>
    public GlyphMetrics GetGlyphMetrics32(uint character)
    {
        ThrowIfDisposed();
        SDLFontException.ThrowIfLessThan(TTF_GlyphMetrics32(_handle, character, out int minx, out int maxx, out int miny, out int maxy, out int advance), 0);
        return new GlyphMetrics(minx, miny, maxx, maxy, advance);
    }


    public string FaceStyleName
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontFaceStyleName(_handle);
        }
    }

    public string FamilyName
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontFaceFamilyName(_handle);
        }
    }

#warning Possibly also a bool
    public int IsFixedWidth
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontFaceIsFixedWidth(_handle);
        }
    }

    public int Height
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontHeight(_handle);
        }
    }

    public int Ascent
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontAscent(_handle);
        }
    }

    public int Descent
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontDescent(_handle);
        }
    }

    public int LineSkip
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontLineSkip(_handle);
        }
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            TTF_CloseFont(_handle);
            disposedValue = true;
        }
    }

    ~TTFont()
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
            throw new ObjectDisposedException(nameof(TTFont));
    }

    #endregion
}

public struct GlyphMetrics
{
    public int MinimumX { get; }
    public int MinimumY { get; }
    public int MaximumX { get; }
    public int MaximumY { get; }
    public int Advance { get; }

    public GlyphMetrics(int minx, int miny, int maxx, int maxy, int advance)
    {
        MinimumX = minx;
        MinimumY = miny;
        MaximumX = maxx;
        MaximumY = maxy;
        Advance = advance;
    }
}