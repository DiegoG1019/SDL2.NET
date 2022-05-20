using SDL2.Bindings;
using SDL2.NET.Exceptions;
using SDL2.NET.Mixer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL_mixer;

namespace SDL2.NET.Mixer;

public static class AudioMixer
{
    private static bool isInit;
    private static readonly object _l = new();

    static AudioMixer()
    {
        var v = MIX_Linked_Version();
        SDLMixerVersion = new(v.major, v.minor, v.patch);
    }

    /// <summary>
    /// The version of SDL_mixer. <see cref="MIX_Linked_Version"/>
    /// </summary>
    public static Version SDLMixerVersion { get; }

    /// <summary>
    /// The amount of times this mixer was opened
    /// </summary>
    public static int TimesOpened { get; private set; }

    /// <summary>
    /// Whether the Mixer is Open or not
    /// </summary>
    public static bool IsOpen { get; private set; } = false;

    /// <summary>
    /// Whether the Mixer has been initialized
    /// </summary>
    /// <remarks>Ideally, Mixer should be initialized through <see cref="SDLApplication.InitializeAudio"/> and cleaned up when <see cref="SDLApplication"/> is disposed</remarks>
    public static bool IsInitialized => isInit;

    /// <summary>
    /// The currently allocated channels for the mixer
    /// </summary>
    /// <remarks>
    /// This can be set at any time, multiple times; even with sounds playing. If set to a value that is less than the current number of channels, then the higher channels will be stopped, freed, and therefore not mixed any longer.
    /// </remarks>
    public static int Channels
    {
        get => _chnls;
        set
        {
            ThrowIfNotInit();
            SDLMixerException.ThrowIfEquals(Mix_AllocateChannels(value), 0);
            UpdateProperties();
        }
    }
    private static int _chnls;

    /// <summary>
    /// The current mixer frequency
    /// </summary>
    public static int Frequency { get; private set; }

    /// <summary>
    /// The current mixer format
    /// </summary>
    public static ushort Format { get; private set; }

    /// <summary>
    /// Initializes the Audio Mixer
    /// </summary>
    /// <param name="flags"></param>
    /// <remarks>Ideally, Mixer should be initialized through <see cref="SDLApplication.InitializeAudio"/> and cleaned up when <see cref="SDLApplication"/> is disposed</remarks>
    public static void InitAudioMixer(MixerInitFlags flags)
    {
        lock (_l)
        {
            if (isInit)
                throw new InvalidOperationException("The Audio Mixer is already initialized");

            SDLMixerException.ThrowIfLessThan(Mix_Init((MIX_InitFlags)flags), 0);
            isInit = true;
        }
    }

    /// <summary>
    /// This function cleans up all dynamically loaded library handles, freeing memory. If support is required again it will be initialized again, either by calling <see cref="InitAudioMixer(MixerInitFlags)"/> or loading a sample or some music with dynamic support required. 
    /// </summary>
    /// <remarks>This is best used if the application will no longer need Audio, and the memory can safely be freed. This should not be called often, and if unsure, best let it be called automatically by <see cref="SDLApplication"/></remarks>
    public static void Quit()
    {
        lock (_l)
        {
            ThrowIfNotInit();
            Mix_Quit();
            isInit = false;
        }
    }

    /// <summary>
    /// Initialize the mixer API.
    /// This must be called before using other functions in this library.
    /// </summary>
    /// <param name="frequency">Output sampling frequency in samples per second (Hz)</param>
    /// <param name="channels">Number of sound channels in output. Set to 2 for stereo, 1 for mono. This has nothing to do with mixing channels</param>
    /// <param name="chunksize">Bytes used per output sample</param>
    /// <param name="format">Output sample format</param>
    /// <remarks>Ideally, this should be first set through <see cref="SDLApplication.InitializeAudio"/></remarks>
    public static void OpenAudioMixer(int frequency = 44100, int channels = 2, int chunksize = 2048, ushort? format = null)
    {
        lock (_l)
        {
            ThrowIfNotInit();
            if (IsOpen)
                throw new InvalidOperationException("The Audio Mixer is already open");

            SDLMixerException.ThrowIfEquals(Mix_OpenAudio(frequency, format ??= MIX_DEFAULT_FORMAT, channels, chunksize), -1);
            UpdateProperties();
        }
    }

    /// <summary>
    /// Shutdown and cleanup the mixer API.
    /// </summary>
    /// <remarks>You may use this to reset the Audio API, to open again with new settings</remarks>
    public static void CloseAudioMixer()
    {
        lock (_l)
        {
            ThrowIfNotInit();
            if (!IsOpen)
                throw new InvalidOperationException("The Audio Mixer is not open");

            Mix_CloseAudio();
            UpdateProperties();
        }
    }

    private static void ThrowIfNotInit()
    {
        if (!isInit)
            throw new InvalidOperationException("The Audio Mixer is not initialized");
    }

    private static void UpdateProperties()
    {
        TimesOpened = Mix_QuerySpec(out int freq, out ushort f, out int chn);
        IsOpen = TimesOpened > 0;
        Frequency = freq;
        _chnls = chn;
        Format = f;
    }
}
