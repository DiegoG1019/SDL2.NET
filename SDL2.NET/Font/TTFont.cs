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

    public int GetKerningSize(int index, int previousIndex)
    {
        ThrowIfDisposed();
        return SDL_GetFontKerningSize(_handle, previousIndex, index);
    }

    public int GetGlyphKerningSize(char character, char previousCharacter)
    {
        ThrowIfDisposed();
        return TTF_GetFontKerningSizeGlyphs(_handle, previousCharacter, character);
    }

    public int GetGlyphKerningSize32(char character, char previousCharacter)
    {
        ThrowIfDisposed();
        return TTF_GetFontKerningSizeGlyphs32(_handle, previousCharacter, character);
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

    public Size GetTextSize(string text, EncodingType type)
    {
        ThrowIfDisposed();
        int w, h;

        SDLFontException.ThrowIfLessThan(type switch
        {
            EncodingType.Text => TTF_SizeText(_handle, text, out w, out h),
            EncodingType.Unicode => TTF_SizeUNICODE(_handle, text, out w, out h),
            EncodingType.UTF8 => TTF_SizeUTF8(_handle, text, out w, out h),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        }, 0);

        return new Size(w, h);
    }


    public TextMeasurement MeasureTextSize(string text, int measureWidth, EncodingType type)
    {
        ThrowIfDisposed();
        int e, c;

        SDLFontException.ThrowIfLessThan(type switch
        {
            EncodingType.Text => TTF_MeasureText(_handle, text, measureWidth, out e, out c),
            EncodingType.Unicode => TTF_MeasureUNICODE(_handle, text, measureWidth, out e, out c),
            EncodingType.UTF8 => TTF_MeasureUTF8(_handle, text, measureWidth, out e, out c),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        }, 0);

        return new TextMeasurement(e, c);
    }

    public Surface RenderText(string text, RGBAColor color, EncodingType type)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Text => TTF_RenderText_Solid(_handle, text, color.ToSDL()),
            EncodingType.Unicode => TTF_RenderUNICODE_Solid(_handle, text, color.ToSDL()),
            EncodingType.UTF8 => TTF_RenderUTF8_Solid(_handle, text, color.ToSDL()),
            _ => throw new NotImplementedException(),
        });
    }

    public Surface RenderText(string text, RGBAColor color, EncodingType type, uint wrapLength)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Text => TTF_RenderText_Solid_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.Unicode => TTF_RenderUNICODE_Solid_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.UTF8 => TTF_RenderUTF8_Solid_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            _ => throw new NotImplementedException(),
        });
    }

    public Surface RenderTextShaded(string text, RGBAColor foreground, RGBAColor background, EncodingType type)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Text => TTF_RenderText_Shaded(_handle, text, foreground.ToSDL(), background.ToSDL()),
            EncodingType.Unicode => TTF_RenderUNICODE_Shaded(_handle, text, foreground.ToSDL(), background.ToSDL()),
            EncodingType.UTF8 => TTF_RenderUTF8_Shaded(_handle, text, foreground.ToSDL(), background.ToSDL()),
            _ => throw new NotImplementedException(),
        });
    }

    public Surface RenderTextShaded(string text, RGBAColor foreground, RGBAColor background, EncodingType type, uint wrapLength)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Text => TTF_RenderText_Shaded_Wrapped(_handle, text, foreground.ToSDL(), background.ToSDL(), wrapLength),
            EncodingType.Unicode => TTF_RenderUNICODE_Shaded_Wrapped(_handle, text, foreground.ToSDL(), background.ToSDL(), wrapLength),
            EncodingType.UTF8 => TTF_RenderUTF8_Shaded_Wrapped(_handle, text, foreground.ToSDL(), background.ToSDL(), wrapLength),
            _ => throw new NotImplementedException(),
        });
    }

    public Surface RenderTextBlended(string text, RGBAColor color, EncodingType type)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Text => TTF_RenderText_Blended(_handle, text, color.ToSDL()),
            EncodingType.Unicode => TTF_RenderUNICODE_Blended(_handle, text, color.ToSDL()),
            EncodingType.UTF8 => TTF_RenderUTF8_Blended(_handle, text, color.ToSDL()),
            _ => throw new NotImplementedException(),
        });
    }

    public Surface RenderTextBlended(string text, RGBAColor color, EncodingType type, uint wrapLength)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(type switch
        {
            EncodingType.Text => TTF_RenderText_Blended_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.Unicode => TTF_RenderUNICODE_Blended_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.UTF8 => TTF_RenderUTF8_Blended_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            _ => throw new NotImplementedException(),
        }));
    }

    public Surface RenderGlyph(char character, RGBAColor color)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph_Solid(_handle, character, color.ToSDL())));
    }

    public Surface RenderGlyphShaded(char character, RGBAColor foreground, RGBAColor background)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph_Shaded(_handle, character, foreground.ToSDL(), background.ToSDL())));
    }

    public Surface RenderGlyphBlended(char character, RGBAColor color)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph_Blended(_handle, character, color.ToSDL())));
    }

    public Surface RenderGlyph32(uint character, RGBAColor color)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph32_Solid(_handle, character, color.ToSDL())));
    }

    public Surface RenderGlyphShaded32(uint character, RGBAColor foreground, RGBAColor background)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph32_Shaded(_handle, character, foreground.ToSDL(), background.ToSDL())));
    }

    public Surface RenderGlyphBlended32(uint character, RGBAColor color)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph32_Blended(_handle, character, color.ToSDL())));
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
