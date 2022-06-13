using SDL2.NET.Exceptions;
using System;
using System.Collections;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents a Sensor in SDL, and statically provides access to SDL Sensor related facilities
/// </summary>
public sealed class Sensor
{
    #region static

    private static readonly Dictionary<int, WeakReference<Sensor>> _dict = new();

    /// <summary>
    /// Represents the constant Standard Gravity of Earth: <c>9.80665 (m/s)^2</c>
    /// </summary>
    public const float StandardGravity = SDL_STANDARD_GRAVITY;

    /// <summary>
    /// Locks for multi-threaded access to the sensor API, use <see cref="Unlock"/> after this
    /// </summary>
    /// <remarks>
    /// If you are using the sensor API or handling events from multiple threads you should use these locking functions to protect access to the sensors. In particular, you are guaranteed that the sensor list won't change, so the API functions that take a sensor index will be valid, and sensor events will not be delivered.
    /// </remarks>
    public static void Lock()
        => SDL_LockSensors();

    /// <summary>
    /// Unlocks for multi-threaded access to the sensor API, use after calling <see cref="Lock"/>
    /// </summary>
    /// <remarks>
    /// If you are using the sensor API or handling events from multiple threads you should use these locking functions to protect access to the sensors. In particular, you are guaranteed that the sensor list won't change, so the API functions that take a sensor index will be valid, and sensor events will not be delivered.
    /// </remarks>
    public static void Unlock()
        => SDL_UnlockSensors();

    /// <summary>
    /// Fetches the Sensor object from the heap or instances a new one
    /// </summary>
    /// <param name="report">The report that contains info about the given sensor</param>
    /// <returns></returns>
    public static Sensor FetchOrOpen(in SensorReport report)
    {
        var device = report.Id;
        Sensor? sensor;

        lock (_dict)
        {
            if (_dict.TryGetValue(device, out var wr))
                if (wr.TryGetTarget(out sensor))
                    return sensor;
                else
                {
                    sensor = new(device);
                    wr.SetTarget(sensor);
                    return sensor;
                }

            _dict[device] = new(sensor = new(device));
            return sensor;
        }
    }

    /// <summary>
    /// Provides a way of querying the sensors currently available to SDL
    /// </summary>
    /// <remarks>
    /// If this collection is to be enumerated, ensure to use both <see cref="Lock"/> and <see cref="Unlock"/> if you expect the available sensors to change at any time
    /// </remarks>
    public static IReadOnlyList<SensorReport> Sensors { get; } = new SensorCollection();

    #region Collection classes

    private sealed class SensorCollection : IReadOnlyList<SensorReport>
    {
        public SensorReport this[int index] 
            => index < 0 || index >= Count
               ? throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be greater than 0 and less than the amount of available sensors")
               : (new(index));

        public int Count => SDL_NumSensors();

        public IEnumerator<SensorReport> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return new(i);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion

    #endregion

    internal readonly IntPtr _handle;

    internal Sensor(int device)
    {
        _handle = SDL_SensorOpen(device);
        if (_handle == IntPtr.Zero)
            throw new SDLSensorException(SDL_GetAndClearError());

        Id = SDL_SensorGetInstanceID(_handle);
        DeviceName = SDL_SensorGetName(_handle);
        DeviceType = (SensorType)SDL_SensorGetType(_handle);
        NonPortableType = SDL_SensorGetNonPortableType(_handle);
    }

    /// <summary>
    /// The unique device instance Id
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Get the implementation dependent name of a sensor
    /// </summary>
    public string DeviceName { get; }

    /// <summary>
    /// Get the type of a sensor
    /// </summary>
    public SensorType DeviceType { get; }

    /// <summary>
    /// Get the platform dependent type of a sensor as an int
    /// </summary>
    public int NonPortableType { get; }

    /// <summary>
    /// Get the current state of an opened sensor
    /// </summary>
    /// <remarks>
    /// The number of values and interpretation of the data is sensor dependent; this method is sensitive to <see cref="Span{T}.Length"/>
    /// </remarks>
    /// <param name="data">The span in which to write the data from the Sensor</param>
    public void Read(in Span<float> data)
    {
        SDLSensorException.ThrowIfLessThan(SDL_SensorGetData(_handle, in data, data.Length), 0);
    }
}
