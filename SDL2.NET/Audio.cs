using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

public static class Audio
{
    /// <summary>
    /// Represents an event relating to audio devices
    /// </summary>
    /// <param name="timestamp">The amount of time that has passed since SDL's library initialized</param>
    /// <param name="deviceId">The Id of the audio device</param>
    public delegate void AudioDeviceEvent(TimeSpan timestamp, uint deviceId);

    /// <summary>
    /// A new audio playback device has been added
    /// </summary>
    public static event AudioDeviceEvent? PlaybackDeviceAdded;

    /// <summary>
    /// An audio playback device has been removed
    /// </summary>
    public static event AudioDeviceEvent? PlaybackDeviceRemoved;

    /// <summary>
    /// A new audio recording device has been added
    /// </summary>
    public static event AudioDeviceEvent? CaptureDeviceAdded;

    /// <summary>
    /// An audio recording device has been removed
    /// </summary>
    public static event AudioDeviceEvent? CaptureDeviceRemoved;

    internal static void TriggerDeviceAdded(SDL_AudioDeviceEvent e)
    {
        if (e.iscapture == 1)
        {
            CaptureDeviceAdded?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), e.which);
            return;
        }
        PlaybackDeviceAdded?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), e.which);
    }

    internal static void TriggerDeviceRemoved(SDL_AudioDeviceEvent e)
    {
        if (e.iscapture == 1)
        {
            CaptureDeviceRemoved?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), e.which);
            return;
        }
        PlaybackDeviceRemoved?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), e.which);
    }

    /// <summary>
    /// Counts the playback devices available
    /// </summary>
    /// <returns>Returns the number of available devices exposed by the current driver or -1 if an explicit list of devices can't be determined</returns>
    /// <remarks>This function is only valid after successfully initializing the audio subsystem. This function may trigger a complete redetect of available hardware.</remarks>
    public static int CountPlaybackDevices()
    {
        var d = SDL_GetNumAudioDevices(0);
        SDLAudioException.ThrowIfLessThan(d, -1);
        return d;
    }

    /// <summary>
    /// Counts the audio capture (recording) devices available
    /// </summary>
    /// <returns>Returns the number of available devices exposed by the current driver or -1 if an explicit list of devices can't be determined</returns>
    /// <remarks>This function is only valid after successfully initializing the audio subsystem. This function may trigger a complete redetect of available hardware.</remarks>
    public static int CountAudioCaptureDevices()
    {
        var d = SDL_GetNumAudioDevices(1);
        SDLAudioException.ThrowIfLessThan(d, -1);
        return d;
    }
}
