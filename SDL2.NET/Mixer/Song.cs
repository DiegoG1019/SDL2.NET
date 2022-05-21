using SDL2.NET.Mixer.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL_mixer;

namespace SDL2.NET.Mixer;

public class Song : IDisposable, IAsyncDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, WeakReference<Song>> _handleDict = new(4, 20);

    internal static Song? Fetch(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : null;

    /// <remarks>
    /// The only reason this is not readonly is because, since this is an async disposable, I need to ensure safety by setting it to null in case it gets freed more than once
    /// </remarks>
    internal IntPtr _handle;

    internal Song(IntPtr handle)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new MixerAudioChunkCreationException(Mix_GetError());
    }

    /// <summary>
    /// Load a music file to use. This can load WAVE, MOD, MIDI, OGG, MP3, FLAC, and any file that you use a command to play with.
    /// </summary>
    /// <remarks>
    /// If you are using an external command to play the music, you must call <see cref="Music.SetPlayCommand(string?)"/> before this, otherwise the internal players will be used. Alternatively, if you have set an external command up and don't want to use it, you must call <see cref="Music.SetCommand"/> passing <see cref="null"/> to use the built-in players again
    /// </remarks>
    /// <param name="file">Name of music file to use</param>
    public Song(string file) : this(Mix_LoadMUS(file)) { }

    /// <summary>
    /// This volume has no effect on the song directly, but will be set as the global music volume when playing
    /// </summary>
    /// <remarks>
    /// This is not an SDL function, and the volume will not be reset when the song finishes
    /// </remarks>
    public int? VolumeOverride
    {
        get => _vol;
        set => _vol = value is >= 0 and <= 128 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, "Volume value must be between 0 and 128");
    }
    private int? _vol;

    /// <summary>
    /// This volume has no effect on the song directly, but will be set as the global music volume when playing. Ranges from 0 (0%) to 1 (100%), and WILL be clamped
    /// </summary>
    /// <remarks>
    /// This is not an SDL function, and the volume will not be reset when the song finishes
    /// </remarks>
    public float? VolumePercentageOverride
    {
        get => VolumeOverride is int v ? v / 128f : null;
        set => _vol = value is float v ? (int)(128 * Math.Clamp(v, 0, 1)) : null;
    }

    /// <summary>
    /// User defined custom Song data
    /// </summary>
    public UserData? UserData { get; set; }

    /// <summary>
    /// The file format encoding of the <see cref="Song"/>.
    /// </summary>
    public MusicType MusicType
    {
        get
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            return (MusicType)Mix_GetMusicType(_handle);
        }
    }

    /// <summary>
    /// Plays this song, halting (or waiting for, if fading out) any other previous song that may have been playing
    /// </summary>
    /// <param name="loops">The amount of times to play the <see cref="Song"/></param>
    public void Play(int loops = 1)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        ThrowIfDisposed();
        MixerSongException.ThrowIfLessThan(Mix_PlayMusic(_handle, loops), 0);
        Music.CurrentlyPlaying = this;
    }

    /// <summary>
    /// Plays this song, halting (or waiting for asynchronously, if fading out) any other previous song that may have been playing
    /// </summary>
    /// <param name="loops">The amount of times to play the <see cref="Song"/></param>
    public async ValueTask PlayAsync(int loops = 1)
    {
        if (Music.IsEffectivelyPlaying && Music.FadeStatus == AudioFadeStatus.FadingOut)
            await Task.Run(() => Play(loops));
        else
            Play(loops);
    }

    /// <summary>
    /// Fades this song in, starting from volume 0 to max volume over <paramref name="fadeInTime"/>, halting (or waiting for, if fading out) any other previous song that may have been playing
    /// </summary>
    /// <param name="fadeInTime">The amount of time to fade the song in over</param>
    /// <param name="loops">The amount of times to play the <see cref="Song"/></param>
    public void FadeIn(TimeSpan fadeInTime, int loops = 1)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        ThrowIfDisposed();
        MixerSongException.ThrowIfLessThan(Mix_FadeInMusic(_handle, loops, (int)fadeInTime.TotalMilliseconds), 0);
        Music.CurrentlyPlaying = this;
    }

    /// <summary>
    /// Fades this song in, starting from volume 0 to max volume over <paramref name="fadeInTime"/>, halting (or waiting for asynchronously, if fading out) any other previous song that may have been playing
    /// </summary>
    /// <param name="fadeInTime">The amount of time to fade the song in over</param>
    /// <param name="loops">The amount of times to play the <see cref="Song"/></param>
    public async ValueTask FadeInAsync(TimeSpan fadeInTime, int loops = 1)
    {
        if (Music.IsEffectivelyPlaying && Music.FadeStatus == AudioFadeStatus.FadingOut)
            await Task.Run(() => FadeIn(fadeInTime, loops));
        else
            FadeIn(fadeInTime, loops);
    }

    /// <summary>
    /// Fades this song in from <paramref name="position"/>, starting from volume 0 to max volume over <paramref name="fadeInTime"/>, halting (or waiting for, if fading out) any other previous song that may have been playing
    /// </summary>
    /// <param name="fadeInTime">The amount of time to fade the song in over</param>
    /// <param name="loops">The amount of times to play the <see cref="Song"/></param>
    /// <param name="position">The position of the song to start from. <see cref="Music.SetPosition(double)"/> for details</param>
    public void FadeIn(TimeSpan fadeInTime, double position, int loops = 1)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        ThrowIfDisposed();
        MixerSongException.ThrowIfLessThan(Mix_FadeInMusicPos(_handle, loops, (int)fadeInTime.TotalMilliseconds, position), 0);
        Music.CurrentlyPlaying = this;
    }

    /// <summary>
    /// Fades this song in from <paramref name="position"/>, starting from volume 0 to max volume over <paramref name="fadeInTime"/>, halting (or waiting for asynchronously, if fading out) any other previous song that may have been playing
    /// </summary>
    /// <param name="fadeInTime">The amount of time to fade the song in over</param>
    /// <param name="loops">The amount of times to play the <see cref="Song"/></param>
    /// <param name="position">The position of the song to start from. <see cref="Music.SetPosition(double)"/> for details</param>
    public async Task FadeInAsync(TimeSpan fadeInTime, double position, int loops = 1)
    {
        if (Music.IsEffectivelyPlaying && Music.FadeStatus == AudioFadeStatus.FadingOut)
            await Task.Run(() => FadeIn(fadeInTime, position, loops));
        else
            FadeIn(fadeInTime, position, loops);
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            var h = _handle;
            _handle = IntPtr.Zero;

            Mix_FreeMusic(h);
            _handleDict.TryRemove(h, out _);
            disposedValue = true;
        }
    }

    ~Song()
    {
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes of this song, and frees unmanaged resources
    /// </summary>
    /// <remarks>
    /// If music is playing it will be halted. If music is fading out, then this function will wait (blocking) until the fade out is complete.
    /// </remarks>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Song));
    }

    /// <summary>
    /// Disposes of this song, and frees unmanaged resources asynchronously
    /// </summary>
    /// <remarks>
    /// If music is playing it will be halted. If music is fading out, then this function will wait (asynchronously) until the fade out is complete.
    /// </remarks>
    public async ValueTask DisposeAsync()
    {
        if (!disposedValue)
            await Task.Run(Dispose); //Dispose already calls GC.SupressFinalize(this)
    }

    #endregion
}
