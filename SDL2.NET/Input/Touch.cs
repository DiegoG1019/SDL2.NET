using SDL2.NET.Exceptions;
using System.Collections;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Provides a means of querying and getting information on Touch input into the application
/// </summary>
public static class Touch
{
    /// <summary>
    /// Represents a way of querying Touch devices
    /// </summary>
    /// <remarks>
    /// On some platforms SDL first sees the touch device if it was actually used. Therefore this collection may not be populated even though devices are available. After using all devices at least once the collection will be correct.
    /// </remarks>
    public static IReadOnlyList<TouchReport> Devices { get; } = new TouchCollection();

    private sealed class TouchCollection : IReadOnlyList<TouchReport>
    {
        internal TouchCollection() { }

        public TouchReport this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) 
                    throw new IndexOutOfRangeException("Index is out of range");

                var x = SDL_GetTouchDevice(index);
                SDLTouchException.ThrowIfLessThan(x, 1);
                return new TouchReport(x);
            }
        }

        public int Count => SDL_GetNumTouchDevices();

        public IEnumerator<TouchReport> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                var x = SDL_GetTouchDevice(i);
                SDLTouchException.ThrowIfLessThan(x, 1);
                yield return new TouchReport(x);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
