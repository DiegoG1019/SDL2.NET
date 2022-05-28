using SDL2.NET.Exceptions;
using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// An object that contains palette information.
/// </summary>
public class Palette : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, WeakReference<Palette>> _handleDict = new(2, 10);

    private readonly IntPtr _handle;

    internal static Palette FetchOrNew(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : new(handle);

    internal Palette(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLPaletteCreationException(SDL_GetError());

        var p = Marshal.PtrToStructure<SDL_Palette>(handle);

        _colors = new RGBAColor[ColorCount = p.ncolors];

        IntPtr c = p.colors;
        int s = Marshal.SizeOf<SDL_Color>();

        for (int i = 0; i < ColorCount; i++)
        {
            _colors[i] = Marshal.PtrToStructure<SDL_Color>(c + i * s);
        }

        //IntPtr[] pointers = new IntPtr[ColorCount];
        //Marshal.Copy(p.colors, pointers, 0, ColorCount);
        //for (int i = 0; i < ColorCount; i++)
        //    _colors[i] = Marshal.PtrToStructure<SDL_Color>(pointers[i]);

        _handleDict[_handle] = new(this);
    }

    /// <summary>
    /// Create a palette structure with the specified number of color entries.
    /// </summary>
    /// <param name="colors"></param>
    public Palette(int colors)
        : this(SDL_AllocPalette(colors)) { }

    /// <summary>
    /// Set a range of colors in a palette.
    /// </summary>
    /// <param name="colors">The colors to copy into the palette</param>
    /// <param name="firstColor">The index of the first palette entry to modify</param>
    public void SetColors(ReadOnlySpan<RGBAColor> colors, int firstColor = 0)
    {
        ThrowIfDisposed();

        if (colors.Length + firstColor >= ColorCount)
            throw new IndexOutOfRangeException($"parameter {nameof(firstColor)} is out of range for this palette's colors and the provided color set. ColorCount: {ColorCount}; firstColor: {firstColor}; colors length: {colors.Length}");

        Span<SDL_Color> sdl_c = stackalloc SDL_Color[colors.Length];
        for (int i = 0; i < colors.Length; i++)
            (_colors[i + firstColor] = colors[i]).ToSDL(out sdl_c[i]);
        SDLPaletteException.ThrowIfLessThan(SDL_SetPaletteColors(_handle, sdl_c, firstColor, colors.Length), 0);
    }

    /// <summary>
    /// The number of colors in the palette
    /// </summary>
    public int ColorCount { get; }

    private readonly RGBAColor[] _colors;

    /// <summary>
    /// Index one of the <see cref="RGBAColor"/>s representing this <see cref="Palette"/>.
    /// </summary>
    /// <remarks>This may not be representative of the underlying SDL object. This keeps a cache on the .NET side of the colors used.</remarks>
    /// <param name="index"></param>
    /// <returns></returns>
    public RGBAColor this[int index]
    {
        get
        {
            throw new NotImplementedException();
            ThrowIfDisposed();
            return _colors[index];
        }
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_FreePalette(_handle);
            _handleDict.TryRemove(_handle, out _);
            disposedValue = true;
        }
    }

    ~Palette()
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
            throw new ObjectDisposedException(nameof(Palette));
    }

    #endregion
}
