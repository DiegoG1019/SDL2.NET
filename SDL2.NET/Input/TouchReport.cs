using SDL2.NET.Exceptions;
using System.Runtime.InteropServices;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Represents a Report on a touch input device
/// </summary>
public struct TouchReport
{
    internal TouchReport(TouchId id)
    {
        Id = id;
        DeviceType = (TouchDeviceType)SDL_GetTouchDeviceType(id);

        var x = SDL_GetNumTouchFingers(id);
        SDLTouchException.ThrowIfLessThan(x, 1);
        FingerCount = x;
    }

    /// <summary>
    /// The identification of the Touch Device
    /// </summary>
    public TouchId Id { get; }

    /// <summary>
    /// The type of the device
    /// </summary>
    public TouchDeviceType DeviceType { get; }

    /// <summary>
    /// The currently active fingers on this device
    /// </summary>
    public int FingerCount { get; }

    /// <summary>
    /// Indexes the device to query for specific fingers
    /// </summary>
    /// <param name="index">The index of the finger to query</param>
    /// <returns>A report on the given finger</returns>
    public FingerReport this[int index]
    {
        get
        {
            IntPtr ptr;
            if (index < 0 || index >= FingerCount || (ptr = SDL_GetTouchFinger(Id, index)) == IntPtr.Zero)
                throw new IndexOutOfRangeException("The index is out of range");

            var f = Marshal.PtrToStructure<SDL_Finger>(ptr);

            return new FingerReport(
                f.id,
                f.x,
                f.y,
                f.pressure
            );
        }
    }
}