using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies whether the Android / iOS built-in accelerometer should be listed as a joystick device, rather than listing actual joysticks only. <see cref="SDL_HINT_ACCELEROMETER_AS_JOYSTICK" href="https://wiki.libsdl.org/SDL_HINT_ACCELEROMETER_AS_JOYSTICK"/>
/// </summary>
/// <remarks>
/// By default SDL will list real joysticks along with the accelerometer as if it were a 3 axis joystick.
/// </remarks>
public sealed class AccelerometerAsJoystick : BinaryHint
{
    internal AccelerometerAsJoystick() : base(SDL_HINT_ACCELEROMETER_AS_JOYSTICK) { }
}

//public delegate void HintCallback();
