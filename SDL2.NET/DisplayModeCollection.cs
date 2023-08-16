using System.Collections;
using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents the available <see cref="DisplayMode"/>s for a given Display
/// </summary>
public struct DisplayModeCollection : IReadOnlyList<DisplayMode>
{
    private readonly int dindex;

    public DisplayModeCollection()
    {
        dindex = -1;
    }

    internal DisplayModeCollection(int index)
    {
        dindex = index;
    }

    /// <summary>
    /// Obtain the <see cref="DisplayMode"/> at <paramref name="index"/> for the Display represented by this struct
    /// </summary>
    public DisplayMode this[int index]
    {
        get
        {
            SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayMode(dindex, index, out var mode), 0);
            return (DisplayMode)mode;
        }
    }

    /// <summary>
    /// Obtain the amount of <see cref="DisplayMode"/>s the Display represented by this struct has
    /// </summary>
    public int Count => SDL_GetNumDisplayModes(dindex);

    public IEnumerator<DisplayMode> GetEnumerator()
    {
        for (int i = 0; i < dindex; i++)
            yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}