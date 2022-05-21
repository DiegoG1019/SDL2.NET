using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using SDL2.NET.Mixer.Exceptions;
using static SDL2.Bindings.SDL_mixer;

namespace SDL2.NET.Mixer;

// I may need to use the hook bindings to build up a system that automatically buffers IDisposable calls and keeps a reference to the chunk if it's still being played

/// <summary>
/// The format of an Audio Chunk. This stores the sample data, and the volume when mixing the sample
/// </summary>
/// <remarks>
/// Wraps around Mix_Chunk: The internal format for an audio chunk. This stores the sample data, the length in bytes of that data, and the volume to use when mixing the sample.
/// </remarks>
public partial class AudioChunk : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, WeakReference<AudioChunk>> _handleDict = new(4, 20);

    private readonly object sync = new();
    internal readonly IntPtr _handle;

    internal static AudioChunk FetchOrNew(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : new(handle);

    internal static AudioChunk? Fetch(IntPtr handle)
        => (_handleDict.TryGetValue(handle, out var wp) && wp.TryGetTarget(out var p)) ? p : null;

    internal AudioChunk(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new MixerAudioChunkCreationException(Mix_GetError());
        _handleDict[_handle] = new(this);
    }

    /// <summary>
    /// Load file for use as a sample. This can load WAVE, AIFF, RIFF, OGG, and VOC files.
    /// </summary>
    /// <remarks><see cref="AudioMixer.IsOpen"/> must be true before calling this constructor</remarks>
    /// <param name="file"></param>
    public AudioChunk(string file) : this(Mix_LoadWAV(file)) { }

    #region Play

    private int _play(int channel, int loops)
    {
        ThrowIfDisposed();
        var i = Mix_PlayChannel(-1, _handle, loops);
        MixerAudioChunkException.ThrowIfLessThan(i, 0);
        return i;
    }

    private int _play(int channel, int loops, TimeSpan time)
    {
        ThrowIfDisposed();
        var i = Mix_PlayChannelTimed(-1, _handle, loops, (int)time.TotalMilliseconds);
        MixerAudioChunkException.ThrowIfLessThan(i, 0);
        return i;
    }

    /// <summary>
    /// Plays the AudioChunk on the first unreserved channel for the set amount of loops
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <returns>The channel the audio is playing on</returns>
    public int Play(int loops = -1) => _play(-1, loops);


    /// <summary>
    /// Plays the AudioChunk on the first unreserved channel for the set amount of loops
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <param name="channel">The channel to play the Audio on</param>
    public void Play(int channel, int loops = -1) => _play(checkCh(channel), loops);

    /// <summary>
    /// Plays the AudioChunk on the first unreserved channel for the set amount of loops, and for at most the set amount of time
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <param name="time">The amount of time to play the audio. Will only take effect if it's long enough, or it loops enough times</param>
    /// <returns>The channel the audio is playing on</returns>
    public int Play(TimeSpan time, int loops = -1) => _play(-1, loops, time);


    /// <summary>
    /// Plays the AudioChunk on the first unreserved channel for the set amount of loops, and for at most the set amount of time
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <param name="channel">The channel to play the Audio on</param>
    /// <param name="time">The amount of time to play the audio. Will only take effect if it's long enough, or it loops enough times</param>
    public void Play(int channel, TimeSpan time, int loops = -1) => _play(checkCh(channel), loops, time);

    #endregion

    #region FadeIn

    private int _fadein(int channel, int loops, TimeSpan fadein)
    {
        ThrowIfDisposed();
        var i = Mix_FadeInChannel(-1, _handle, loops, (int)fadein.TotalMilliseconds);
        MixerAudioChunkException.ThrowIfLessThan(i, 0);
        return i;
    }

    private int _fadein(int channel, int loops, TimeSpan fadein, TimeSpan time)
    {
        ThrowIfDisposed();
        var i = Mix_FadeInChannelTimed(-1, _handle, loops, (int)fadein.TotalMilliseconds, (int)time.TotalMilliseconds);
        MixerAudioChunkException.ThrowIfLessThan(i, 0);
        return i;
    }

    /// <summary>
    /// Plays the AudioChunk on the first unreserved channel for the set amount of loops
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <param name="fadeInTime">The amount of time it takes for the chunk to transition from Volume 0 to full volume</param>
    /// <returns>The channel the audio is playing on</returns>
    public int FadeIn(TimeSpan fadeInTime, int loops = -1) => _fadein(-1, loops, fadeInTime);


    /// <summary>
    /// Fades in the AudioChunk on the first unreserved channel for the set amount of loops
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <param name="fadeInTime">The amount of time it takes for the chunk to transition from Volume 0 to full volume</param>
    /// <param name="channel">The channel to play the Audio on</param>
    public void FadeIn(int channel, TimeSpan fadeInTime, int loops = -1) => _fadein(checkCh(channel), loops, fadeInTime);

    /// <summary>
    /// Fades in the AudioChunk on the first unreserved channel for the set amount of loops, and for at most the set amount of time
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <param name="time">The amount of time to play the audio. Will only take effect if it's long enough, or it loops enough times</param>
    /// <param name="fadeInTime">The amount of time it takes for the chunk to transition from Volume 0 to full volume</param>
    /// <returns>The channel the audio is playing on</returns>
    public int FadeIn(TimeSpan fadeInTime, TimeSpan time, int loops = -1) => _fadein(-1, loops, fadeInTime, time);


    /// <summary>
    /// Fades in the AudioChunk on the first unreserved channel for the set amount of loops, and for at most the set amount of time
    /// </summary>
    /// <param name="loops">The amount of times to loop the Audio. See <see cref="LoopAudio"/></param>
    /// <param name="channel">The channel to play the Audio on</param>
    /// <param name="time">The amount of time to play the audio. Will only take effect if it's long enough, or it loops enough times</param>
    /// <param name="fadeInTime">The amount of time it takes for the chunk to transition from Volume 0 to full volume</param>
    public void FadeIn(int channel, TimeSpan fadeInTime, TimeSpan time, int loops = -1) => _fadein(checkCh(channel), loops, fadeInTime, time);

    #endregion

    /// <summary>
    /// Obtains a stream that allows the modification of the underlying byte data of the AudioChunk
    /// </summary>
    /// <returns></returns>
    public AudioChunkSampleBuffer GetBuffer() => new(_handle, sync);

    /// <summary>
    /// The volume to be used when mixing the chunk
    /// </summary>
    public int Volume
    {
        get => Mix_VolumeChunk(_handle, -1);
        set => Mix_VolumeChunk(_handle, value is >= 0 and <= 128 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, "Volume value must be between 0 and 128"));
    }

    /// <summary>
    /// The volume to be used when mixing the chunk. Ranges from 0 (0%) to 1 (100%), and WILL be clamped
    /// </summary>
    /// <remarks>
    /// This is not an SDL function, just math-magic
    /// </remarks>
    public float VolumePercentage
    {
        get => Volume / 128f;
        set => Volume = (int)(128 * Math.Clamp(value, 0, 1));
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            Mix_FreeChunk(_handle);
            _handleDict.TryRemove(_handle, out _);
            disposedValue = true;
        }
    }

    ~AudioChunk()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(AudioChunk));
    }

    #endregion


    #region helpers

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int checkCh(int x, [CallerArgumentExpression("x")] string arg = default)
        => x < 0 || x >= AudioMixer.ChannelCount ? throw new ArgumentOutOfRangeException(arg) : x;

    #endregion
}