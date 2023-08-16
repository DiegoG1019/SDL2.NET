using System.Collections;
using SDL2.NET.Exceptions;
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

    /// <summary>
    /// Fired when a Touch Device is touched
    /// </summary>
    public static event TouchFingerEvent? FingerPressed;
    internal static void TriggerFingerPressed(SDL_TouchFingerEvent e)
    {
        TouchFingerEvent? del = FingerPressed;
        if (del != null)
        {
            var tr = new TouchReport(e.touchId);
            var fr = new FingerReport(e.fingerId, e.x, e.y, e.pressure);

            Window? win = null;
            if (e.windowID > 0)
                Window.TryGetWindow(e.windowID, out win);

            del?.Invoke(tr, fr, TimeSpan.FromMilliseconds(e.timestamp), e.dx, e.dy, win);
        }
    }

    /// <summary>
    /// Fired when a Touch Device is no longer being touched
    /// </summary>
    public static event TouchFingerEvent? FingerReleased;
    internal static void TriggerFingerReleased(SDL_TouchFingerEvent e)
    {
        TouchFingerEvent? del = FingerReleased;
        if (del != null)
        {
            var tr = new TouchReport(e.touchId);
            var fr = new FingerReport(e.fingerId, e.x, e.y, e.pressure);

            Window? win = null;
            if (e.windowID > 0)
                Window.TryGetWindow(e.windowID, out win);

            del?.Invoke(tr, fr, TimeSpan.FromMilliseconds(e.timestamp), e.dx, e.dy, win);
        }
    }

    /// <summary>
    /// Fired when a finger is dragging along a Touch Device
    /// </summary>
    public static event TouchFingerEvent? FingerMotion;
    internal static void TriggerFingerMotion(SDL_TouchFingerEvent e)
    {
        TouchFingerEvent? del = FingerMotion;
        if (del != null)
        {
            var tr = new TouchReport(e.touchId);
            var fr = new FingerReport(e.fingerId, e.x, e.y, e.pressure);

            Window? win = null;
            if (e.windowID > 0)
                Window.TryGetWindow(e.windowID, out win);

            del?.Invoke(tr, fr, TimeSpan.FromMilliseconds(e.timestamp), e.dx, e.dy, win);
        }
    }

    /// <summary>
    /// Fired when a Dollar Gesture is performed
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://github.com/libsdl-org/SDL/blob/main/docs/README-gesture.md"/>
    /// </remarks>
    public static event DollarGestureEvent? DollarGesture;
    internal static void TriggerDollarGesture(SDL_DollarGestureEvent e)
    {
        DollarGesture?.Invoke(new TouchReport(e.touchId), TimeSpan.FromMilliseconds(e.timestamp), e.gestureId, e.numFingers, e.error, new(e.x, e.y));
    }

    /// <summary>
    /// Fired when a Dollar Record is performed
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://github.com/libsdl-org/SDL/blob/main/docs/README-gesture.md"/>
    /// </remarks>
    public static event DollarGestureEvent? DollarRecord;
    internal static void TriggerDollarRecord(SDL_DollarGestureEvent e)
    {
        DollarRecord?.Invoke(new TouchReport(e.touchId), TimeSpan.FromMilliseconds(e.timestamp), e.gestureId, e.numFingers, e.error, new(e.x, e.y));
    }

    /// <summary>
    /// Fired when a multi finger gesture is performed
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://github.com/libsdl-org/SDL/blob/main/docs/README-gesture.md"/>
    /// </remarks>
    public static event MultiGestureEvent? MultiGesture;
    internal static void TriggerMultiGesture(SDL_MultiGestureEvent e)
    {
        MultiGesture?.Invoke(new TouchReport(e.touchId), TimeSpan.FromMilliseconds(e.timestamp), e.dTheta, e.dDist, e.numFingers, new(e.x, e.y));
    }

    /// <summary>
    /// Represents an event where a touch pad was interacted with
    /// </summary>
    /// <remarks>
    /// The finger report is generated from the index that SDL actually passes, and is generated in the interval between the actual SDL event is recorded and this event is fired
    /// </remarks>
    /// <param name="touch">Information about the actual Touch</param>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="window">The <see cref="Window"/> under the touch, if any</param>
    /// <param name="device">Information about the device that was touched</param>
    /// <param name="dx">The distance moved in the x-axis, normalized -1 - 1</param>
    /// <param name="dy">The distance moved in the y-axis, normalized -1 - 1</param>
    public delegate void TouchFingerEvent(TouchReport device, FingerReport touch, TimeSpan timestamp, float dx, float dy, Window? window);

    /// <summary>
    /// Represents an event where a complex gesture was performed
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://github.com/libsdl-org/SDL/blob/main/docs/README-gesture.md"/>
    /// </remarks>
    /// <param name="device">Information about the device where this gesture took place</param>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="gesture">The Id of the closest matching gesture</param>
    /// <param name="fingers">The amount of fingers used to perform this gesture</param>
    /// <param name="error">The difference between the gesture template and the actual performed gesture. The closer this value is to 0, the closer the match</param>
    /// <param name="center">The normalized center of the gesture, 0f - 1f</param>
    public delegate void DollarGestureEvent(TouchReport device, TimeSpan timestamp, GestureId gesture, uint fingers, float error, FPoint center);

    /// <summary>
    /// Represents an event where a multi-finger gesture was performed
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://github.com/libsdl-org/SDL/blob/main/docs/README-gesture.md"/>
    /// </remarks>
    /// <param name="device">Information about the device where this gesture took place</param>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="theta">The measure in radians that the fingers rotated during the motion</param>
    /// <param name="fingers">The amount of fingers used to perform this gesture</param>
    /// <param name="dist">The measure that the fingers pinched during this motion</param>
    /// <param name="center">The normalized center of the gesture, 0f - 1f</param>
    public delegate void MultiGestureEvent(TouchReport device, TimeSpan timestamp, float theta, float dist, uint fingers, FPoint center);

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
