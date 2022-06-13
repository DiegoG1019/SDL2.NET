using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents information about a given sensor
/// </summary>
/// <param name="Index">The index of the sensor</param>
public readonly record struct SensorReport(int Index)
{
    /// <summary>
    /// The unique device instance Id
    /// </summary>
    public int Id => SDLSensorException.ThrowIfNull(SDL_SensorGetDeviceInstanceID(Index));

    /// <summary>
    /// Get the implementation dependent name of a sensor
    /// </summary>
    public string DeviceName => SDLSensorException.ThrowIfNull(SDL_SensorGetDeviceName(Index));

    /// <summary>
    /// Get the type of a sensor
    /// </summary>
    public SensorType DeviceType => (SensorType)SDLSensorException.ThrowIfEquals((int)SDL_SensorGetDeviceType(Index), -1);

    /// <summary>
    /// Get the platform dependent type of a sensor as an int
    /// </summary>
    public int NonPortableType => SDLSensorException.ThrowIfEquals(SDL_SensorGetDeviceNonPortableType(Index), -1);

    /// <summary>
    /// Fetches the Sensor object represented by this <see cref="SensorReport"/> from the heap or instances a new one
    /// </summary>
    /// <remarks>
    /// This method is shorthand for <see cref="Sensor.FetchOrOpen(in SensorReport)"/>
    /// </remarks>
    /// <returns>The now open sensor</returns>
    public Sensor FetchOrOpen() => Sensor.FetchOrOpen(in this);
}