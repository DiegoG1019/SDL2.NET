using SDL2.NET.SDLFont.Exceptions;
using static SDL2.Bindings.SDL_ttf;

namespace SDL2.NET.SDLFont;

/// <summary>
/// Represents a True Type Font (TTF)
/// </summary>
public class TTFont : IDisposable, IHandle
{
    IntPtr IHandle.Handle => _handle;
    private readonly IntPtr _handle;
    internal TTFont(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLFontException(TTF_GetError());
    }


    /// <summary>
    /// Loads a file for use as a font, at <see cref="size"/> . This can load TTF and FON files.
    /// </summary>
    /// <param name="file">The name of the file in the disk</param>
    /// <param name="size">The size of the font</param>
    public TTFont(string file, int size) : this(TTF_OpenFont(file, size))
    {
        _size = size;
    }

    /// <summary>
    /// Load file, face index, for use as a font, at <see cref="size"/> . This can load TTF and FON files.
    /// </summary>
    /// <param name="file">The name of the file in the disk</param>
    /// <param name="size">The size of the font</param>
    /// <param name="index">The index of the fontface</param>
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

    /// <summary>
    /// The rendering <see cref="TTFStyle"/> of the loaded font. get: <see cref="TTF_GetFontStyle" href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_21.html#SEC21"/>; set: <see cref="TTF_SetFontStyle(IntPtr, int)" href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_22.html#SEC22"/>
    /// </summary>
    /// <remarks>Defaults to <see cref="TTFStyle.Normal"/></remarks>
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

    /// <summary>
    /// Get the current font face style name from the loaded font, or <see cref="null"/> if it's not available.
    /// </summary>
    public string? FaceStyleName
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontFaceStyleName(_handle);
        }
    }

    /// <summary>
    /// Get the current font family name from the loaded font, or <see cref="null"/> if it's not available.
    /// </summary>
    public string? FamilyName
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontFaceFamilyName(_handle);
        }
    }

    /// <summary>
    /// Test if the current font face of the loaded font is a fixed width font.
    /// </summary>
    /// <remarks>Fixed width fonts are monospace, meaning every character that exists in the font is the same width, thus you can assume that a rendered string's width is going to be the result of a simple calculation: glyph_width* string_length</remarks>
    public int IsFixedWidth
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontFaceIsFixedWidth(_handle);
        }
    }

    /// <summary>
    /// Gets the maximum pixel height of all glyphs of the loaded font.
    /// </summary>
    /// <remarks>While you may use tis value to render the drawn text on-screen as closely as possible, you may prefer use of <see cref="LineSkip"/></remarks>
    public int Height
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontHeight(_handle);
        }
    }

    /// <summary>
    /// Get the maximum pixel ascent of all glyphs of the loaded font. This can also be interpreted as the distance from the top of the font to the baseline. <see cref="TTF_FontAscent(IntPtr)" href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_24.html#SEC24"/>
    /// </summary>
    /// <remarks>It could be used when drawing an individual glyph relative to a top point, by combining it with the glyph's <see cref="GlyphMetrics.MaximumY"/> metric to resolve the top of the <see cref="Rectangle"/> used when blitting the glyph on the screen.</remarks>
    public int Ascent
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontAscent(_handle);
        }
    }

    /// <summary>
    /// Get the maximum pixel descent of all glyphs of the loaded font. This can also be interpreted as the distance from the baseline to the bottom of the font. <see cref="TTF_FontDescent" href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_25.html#SEC25"/>
    /// </summary>
    /// <remarks>It could be used when drawing an individual glyph relative to a bottom point, by combining it with the glyph's <see cref="GlyphMetrics.MaximumY"/> metric to resolve the bottom of the <see cref="Rectangle"/> used when blitting the glyph on the screen.</remarks>
    public int Descent
    {
        get
        {
            ThrowIfDisposed();
            return TTF_FontDescent(_handle);
        }
    }

    /// <summary>
    /// Get the reccomended pixel height of a rendered line of text of the loaded font. This is usually larger than the <see cref="Height"/> of the font. <see cref="TTF_FontLineSkip" href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_26.html#SEC26"/>
    /// </summary>
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

    /// <summary>
    /// Get glyph metrics of the given <see cref="char"/> from the loaded font
    /// </summary>
    /// <param name="character">The character to get the metrics of</param>
    /// <remarks>For a picture detailing what each value means, see: <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_31.html#SEC31"/></remarks>
    /// <returns></returns>
    public GlyphMetrics GetGlyphMetrics(char character)
    {
        ThrowIfDisposed();
        SDLFontException.ThrowIfLessThan(TTF_GlyphMetrics(_handle, character, out int minx, out int maxx, out int miny, out int maxy, out int advance), 0);
        return new GlyphMetrics(minx, miny, maxx, maxy, advance);
    }

    /// <summary>
    /// Get glyph metrics of the given <see cref="char"/> from the loaded font
    /// </summary>
    /// <param name="character">The character to get the metrics of</param>
    /// <remarks>For a picture detailing what each value means, see: <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_31.html#SEC31"/></remarks>
    /// <returns></returns>
    /// <param name="character">The character to test. <see cref="char"/> can be implicitly casted (widened) to <see cref="uint"/></param>
    public GlyphMetrics GetGlyphMetrics32(uint character)
    {
        ThrowIfDisposed();
        SDLFontException.ThrowIfLessThan(TTF_GlyphMetrics32(_handle, character, out int minx, out int maxx, out int miny, out int maxy, out int advance), 0);
        return new GlyphMetrics(minx, miny, maxx, maxy, advance);
    }

    /// <summary>
    /// Calculate the resulting surface size of the encoded text rendered using this font. No actual rendering is done, but correct kerning is applied to get the actual width.
    /// </summary>
    /// <remarks>The height returned in <see cref="Size.Height"/> is the same as you get by using <see cref="Height"/></remarks>
    /// <param name="text"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Size GetTextSize(string text, EncodingType type)
    {
        ThrowIfDisposed();
        int w, h;

        SDLFontException.ThrowIfLessThan(type switch
        {
            EncodingType.Latin1 => TTF_SizeText(_handle, text, out w, out h),
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
            EncodingType.Latin1 => TTF_MeasureText(_handle, text, measureWidth, out e, out c),
            EncodingType.Unicode => TTF_MeasureUNICODE(_handle, text, measureWidth, out e, out c),
            EncodingType.UTF8 => TTF_MeasureUTF8(_handle, text, measureWidth, out e, out c),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        }, 0);

        return new TextMeasurement(e, c);
    }

    /// <summary>
    /// Render the encoded text using this font with the given color onto a new surface, using the Solid mode
    /// </summary>
    /// <param name="text">The text to render</param>
    /// <param name="color">The color to render the text with</param>
    /// <param name="type">The encoding type</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderTextSolid(string text, RGBAColor color, EncodingType type)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Latin1 => TTF_RenderText_Solid(_handle, text, color.ToSDL()),
            EncodingType.Unicode => TTF_RenderUNICODE_Solid(_handle, text, color.ToSDL()),
            EncodingType.UTF8 => TTF_RenderUTF8_Solid(_handle, text, color.ToSDL()),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        });
    }

    /// <summary>
    /// Render the encoded text using this font with the given color onto a new surface, using the Solid mode and text wrapping
    /// </summary>
    /// <param name="text">The text to render</param>
    /// <param name="color">The color to render the text with</param>
    /// <param name="type">The encoding type</param>
    /// <param name="wrapLength">The length of text to render before wrapping</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderTextSolid(string text, RGBAColor color, EncodingType type, uint wrapLength)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Latin1 => TTF_RenderText_Solid_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.Unicode => TTF_RenderUNICODE_Solid_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.UTF8 => TTF_RenderUTF8_Solid_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        });
    }

    /// <summary>
    /// Render the encoded text using this font with the given color onto a new surface, using the Shaded mode
    /// </summary>
    /// <param name="text">The text to render</param>
    /// <param name="foreground">The foreground color to use</param>
    /// <param name="background">The background color to use</param>
    /// <param name="type">The encoding type</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderTextShaded(string text, RGBAColor foreground, RGBAColor background, EncodingType type)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Latin1 => TTF_RenderText_Shaded(_handle, text, foreground.ToSDL(), background.ToSDL()),
            EncodingType.Unicode => TTF_RenderUNICODE_Shaded(_handle, text, foreground.ToSDL(), background.ToSDL()),
            EncodingType.UTF8 => TTF_RenderUTF8_Shaded(_handle, text, foreground.ToSDL(), background.ToSDL()),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        });
    }

    /// <summary>
    /// Render the encoded text using this font with the given color onto a new surface, using the Shaded mode and text wrapping
    /// </summary>
    /// <param name="text">The text to render</param>
    /// <param name="foreground">The foreground color to use</param>
    /// <param name="background">The background color to use</param>
    /// <param name="type">The encoding type</param>
    /// <param name="wrapLength">The length of text to render before wrapping</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderTextShaded(string text, RGBAColor foreground, RGBAColor background, EncodingType type, uint wrapLength)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Latin1 => TTF_RenderText_Shaded_Wrapped(_handle, text, foreground.ToSDL(), background.ToSDL(), wrapLength),
            EncodingType.Unicode => TTF_RenderUNICODE_Shaded_Wrapped(_handle, text, foreground.ToSDL(), background.ToSDL(), wrapLength),
            EncodingType.UTF8 => TTF_RenderUTF8_Shaded_Wrapped(_handle, text, foreground.ToSDL(), background.ToSDL(), wrapLength),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        });
    }

    /// <summary>
    /// Render the encoded text using this font with the given color onto a new surface, using the Blended mode
    /// </summary>
    /// <param name="text">The text to render</param>
    /// <param name="color">The color to render the text with</param>
    /// <param name="type">The encoding type</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderTextBlended(string text, RGBAColor color, EncodingType type)
    {
        ThrowIfDisposed();
        return new Surface(type switch
        {
            EncodingType.Latin1 => TTF_RenderText_Blended(_handle, text, color.ToSDL()),
            EncodingType.Unicode => TTF_RenderUNICODE_Blended(_handle, text, color.ToSDL()),
            EncodingType.UTF8 => TTF_RenderUTF8_Blended(_handle, text, color.ToSDL()),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        });
    }

    /// <summary>
    /// Render the encoded text using this font with the given color onto a new surface, using the Shaded mode and text wrapping
    /// </summary>
    /// <param name="text">The text to render</param>
    /// <param name="color">The color to render the text with</param>
    /// <param name="type">The encoding type</param>
    /// <param name="wrapLength">The length of text to render before wrapping</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderTextBlended(string text, RGBAColor color, EncodingType type, uint wrapLength)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(type switch
        {
            EncodingType.Latin1 => TTF_RenderText_Blended_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.Unicode => TTF_RenderUNICODE_Blended_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            EncodingType.UTF8 => TTF_RenderUTF8_Blended_Wrapped(_handle, text, color.ToSDL(), wrapLength),
            _ => throw new InvalidOperationException($"Unknown EncodingType {type}")
        }));
    }

    /// <summary>
    /// Render the glyph represented by the given character onto a new surface, using the Solid mode
    /// </summary>
    /// <param name="character">The character representing the glyph to render</param>
    /// <param name="color">The color to render the character with</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered character</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderGlyphSolid(char character, RGBAColor color)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph_Solid(_handle, character, color.ToSDL())));
    }

    /// <summary>
    /// Render the glyph represented by the given character onto a new surface, using the Shaded mode
    /// </summary>
    /// <param name="character">The character representing the glyph to render</param>
    /// <param name="foreground">The foreground color to use</param>
    /// <param name="background">The background color to use</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderGlyphShaded(char character, RGBAColor foreground, RGBAColor background)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph_Shaded(_handle, character, foreground.ToSDL(), background.ToSDL())));
    }

    /// <summary>
    /// Render the glyph represented by the given character onto a new surface, using the Blended mode
    /// </summary>
    /// <param name="character">The character representing the glyph to render</param>
    /// <param name="color">The color to render the character with</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderGlyphBlended(char character, RGBAColor color)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph_Blended(_handle, character, color.ToSDL())));
    }

    /// <summary>
    /// Render the glyph represented by the given character onto a new surface, using the Solid mode
    /// </summary>
    /// <param name="character">The character representing the glyph to render</param>
    /// <param name="color">The color to render the character with</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered character</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderGlyphSolid32(uint character, RGBAColor color)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph32_Solid(_handle, character, color.ToSDL())));
    }

    /// <summary>
    /// Render the glyph represented by the given character onto a new surface, using the Shaded mode
    /// </summary>
    /// <param name="character">The character representing the glyph to render</param>
    /// <param name="foreground">The foreground color to use</param>
    /// <param name="background">The background color to use</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
    public Surface RenderGlyphShaded32(uint character, RGBAColor foreground, RGBAColor background)
    {
        ThrowIfDisposed();
        return new Surface(SDLFontException.ThrowIfPointerNull(TTF_RenderGlyph32_Shaded(_handle, character, foreground.ToSDL(), background.ToSDL())));
    }

    /// <summary>
    /// Render the glyph represented by the given character onto a new surface, using the Blended mode
    /// </summary>
    /// <param name="character">The character representing the glyph to render</param>
    /// <param name="color">The color to render the character with</param>
    /// <returns>The generated <see cref="Surface"/> with the rendered text</returns>
    /// <remarks>See <see href="https://www.libsdl.org/projects/docs/SDL_ttf/SDL_ttf_35.html#SEC35"/ for more info on render modes></remarks>
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
