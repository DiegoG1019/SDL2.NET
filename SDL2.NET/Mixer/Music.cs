using SDL2.NET.SDLMixer.Exceptions;
using static SDL2.Bindings.SDL_mixer;

namespace SDL2.NET.SDLMixer;

public static class Music
{
    // Author's note: I'm refraining from adding Mix_HookMusic support here because, well, while I do believe that C# is more than good enough to
    // muscle through fast-paced music data fast enough, specially in JIT supporting platforms, all the marshalling and copying required to make it
    // work would most likely cripple performance

    static Music()
    {
        SongFinished += () => CurrentlyPlaying = null;
    }

    internal static void TriggerSongFinished() => SongFinished();

    /// <summary>
    /// Fired every time a song finishes, either naturally or by halting (including fade-outs), and just before a new one begins if queued.
    /// </summary>
    /// <remarks>
    /// NEVER call <see cref="AudioMixer"/>, <see cref="Music"/>, <see cref="Song"/> or <see cref="AudioChunk"/> methods from one of this event's callbacks.
    /// </remarks>
    public static event Action SongFinished;

    /// <summary>
    /// Fired whenever a new song starts
    /// </summary>
    /// <remarks>
    /// This is entirely a .NET event, so it's safe to call virtually any method from this event's callbacks.
    /// </remarks>
    public static event Action<Song>? SongStarted;

    /// <summary>
    /// Whether or not music is currently playing
    /// </summary>
    /// <remarks>Does not check if the music is paused</remarks>
    public static bool IsPlaying
    {
        get
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            return Mix_PlayingMusic() == 1;
        }
    }

    /// <summary>
    /// The current fading status of the currently playing song, if any.
    /// </summary>
    /// <remarks>Defaults to <see cref="AudioFadeStatus.NotFading"/> if no song is playing</remarks>
    public static AudioFadeStatus FadeStatus
    {
        get
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            return (AudioFadeStatus)Mix_FadingMusic();
        }
    }

    /// <summary>
    /// Whether or not music is currently paused.
    /// </summary>
    /// <remarks>Does not check if the music was halted first, so it may return <see cref="true"/> even if there is no music to pause</remarks>
    public static bool IsPaused
    {
        get
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            return Mix_PausedMusic() == 1;
        }
    }

    /// <summary>
    /// Whether there is music actively being played.
    /// </summary>
    /// <remarks>Checks both whether it's halted and whether it's paused.</remarks>
    public static bool IsEffectivelyPlaying
    {
        get
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            return IsPlaying && !IsPaused;
        }
    }

    /// <summary>
    /// The volume of the music
    /// </summary>
    public static int Volume
    {
        get
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            return Mix_VolumeMusic(-1);
        }

        set
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            Mix_VolumeMusic(value is >= 0 and <= 128 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, "Volume value must be between 0 and 128"));
        }
    }

    /// <summary>
    /// The volume of the music. Ranges from 0 (0%) to 1 (100%), and WILL be clamped
    /// </summary>
    /// <remarks>
    /// This is not an SDL function, just math-magic
    /// </remarks>
    public static float VolumePercentage
    {
        get => Volume / 128f;
        set => Volume = (int)(128 * Math.Clamp(value, 0, 1));
    }

    /// <summary>
    /// The currently playing song
    /// </summary>
    public static Song? CurrentlyPlaying
    {
        get => _currentlyPlaying;
        set
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            if (ReferenceEquals(_currentlyPlaying, value))
                return;
            _currentlyPlaying = value;
            if (value is not null)
            {

                SongStarted?.Invoke(value);
            }
        }
    }
    private static Song? _currentlyPlaying;

    /// <summary>
    /// Setup a command line music player to use to play music. Any music playing will be halted. For more info, see <see href="https://www.libsdl.org/projects/SDL_mixer/docs/SDL_mixer_66.html#SEC66"/>
    /// </summary>
    /// <param name="cmd">System command to play the music. Should be a complete command, as if typed in to the command line, but it should expect the filename to be added as the last argument; or <see cref="null"/> to return to internal functionality</param>
    public static void SetPlayCommand(string? cmd)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        MixerSongException.ThrowIfLessThan(Mix_SetMusicCMD(cmd!), 0);
    }

    /// <summary>
    /// Set the position of the currently playing song. The position takes different meanings for different music sources. It only works on: <see cref="MusicType.MOD"/>, <see cref="MusicType.OGG"/>, and <see cref="MusicType.MP3"/>
    /// </summary>
    /// <remarks>This function does the following: for <see cref="MusicType.MOD"/>: The double is cast to Uint16 and used for a pattern number in the module. Passing zero is similar to rewinding the song; for <see cref="MusicType.OGG"/>: Jumps to position seconds from the beginning of the song; for <see cref="MusicType.MP3"/>: Jumps to position seconds from the current position in the stream, you may want to call <see cref="Music.Rewind"/> before this. Does not go in reverse and negative values are ignored.</remarks>
    /// <param name="position">The position to play from</param>
    public static void SetPosition(double position)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        if (CurrentlyPlaying is null) return;
        MixerSongException.ThrowIfLessThan(Mix_SetMusicPosition(position), 0);
    }

    /// <summary>
    /// Halts any music playing, interrupting fade effects.
    /// </summary>
    public static void Halt()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_HaltMusic();
    }

    /// <summary>
    /// Pause the music playback. You may halt paused music.
    /// </summary>
    /// <remarks>
    /// Music can only be paused if it is actively playing.
    /// </remarks>
    public static void Pause()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_PauseMusic();
    }

    /// <summary>
    /// Unpause the music
    /// </summary>
    /// <remarks>This is safe to use on halted, paused, and already playing music.</remarks>
    public static void Resume()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_ResumeMusic();
    }

    /// <summary>
    /// Gradually fade out the music over <paramref name="fadeOutTime"/> starting now.
    /// </summary>
    /// <remarks>
    /// The music will be halted after the fade out is completed. This only takes effect if music is playing and is not fading out already. <see cref="SongFinished"/> will fire after the fade out is completed
    /// </remarks>
    /// <param name="fadeOutTime">The amount of time that the fade-out effect should take to go to silence, starting now.</param>
    public static void FadeOut(TimeSpan fadeOutTime)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        MixerSongException.ThrowIfLessThan(Mix_FadeOutMusic((int)fadeOutTime.TotalMilliseconds), 1);
    }

    /// <summary>
    /// Rewind the music to the start. This is safe to use on halted, paused, and already playing music. 
    /// </summary>
    /// <remarks>
    /// It is not useful to rewind the music immediately after starting playback, because it starts at the beginning by default. This method only works for: <see cref="MusicType.MOD"/>, <see cref="MusicType.OGG"/>, <see cref="MusicType.MP3"/>, and native <see cref="MusicType.MIDI"/>
    /// </remarks>
    public static void Rewind()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_RewindMusic();
    }
}
