using System.Runtime.CompilerServices;
using System.Threading.Channels;
using static SDL2.Bindings.SDL_mixer;
using static SDL2.NET.SDLMixer.AudioMixer;

namespace SDL2.NET.SDLMixer;

// I really tried to encapsulate these methods into an object, but I couldn't really figure out how.
// The entire API is very dependant on a state that is not only hidden away from me, but would require a lot of work (and not insignificant CPU) to
// manage and keep track of, and it seems wasteful considering SDL already does it.
// Any object or state I could create here would be simply unable to make any sort of guarantees to the users, channels could stop, start, resume, fadeOut, change groups
// and do any number of things at any given time at the user's bequest; and it would probably be bloat to keep track of all that in .NET, not to mention memory bloat for
// all the objects and lists that would be created for every group and channel. So, user, I leave this to you.

/// <summary>
/// Represents SDL channels.
/// </summary>
public static class AudioChannels
{
    /// <summary>
    /// The currently allocated channels for the mixer. This is the same as <see cref="AudioMixer.ChannelCount"/>
    /// </summary>
    /// <remarks>
    /// This can be set at any time, multiple times; even with sounds playing. If set to a value that is less than the current number of channels, then the higher channels will be stopped, freed, and therefore not mixed any longer.
    /// </remarks>
    public static int ChannelCount
    {
        get => AudioMixer.ChannelCount;
        set => AudioMixer.ChannelCount = value;
    }

    /// <summary>
    /// Gets the average volume of all channels, or sets the volume of all channels
    /// </summary>
    /// <remarks>This is the equivalent of calling <see cref="Mix_Volume(int, int)"/> with an index of -1. Use this one for global operations, as the per-channel methods will throw if below 0</remarks>
    public static int GlobalVolume
    {
        get
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            return Mix_Volume(-1, -1);
        }

        set
        {
            AudioMixer.ThrowIfNotInitAndOpen();
            Mix_Volume(-1, value);
        }
    }

    /// <summary>
    /// The volume of all channels. Ranges from 0 (0%) to 1 (100%), and WILL be clamped
    /// </summary>
    /// <remarks>
    /// This is not an SDL function, just math-magic
    /// </remarks>
    public static float GlobalVolumePercentage
    {
        get => GlobalVolume / 128f;
        set => GlobalVolume = (int)(128 * Math.Clamp(value, 0, 1));
    }

    /// <summary>
    /// Gets the volume of a specific channel
    /// </summary>
    /// <param name="channel">The channel to get the volume of</param>
    /// <returns>The volume of the channel</returns>
    public static int GetChannelVolume(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_Volume(check(channel), -1);
    }

    /// <summary>
    /// Gets the volume of a specific channel
    /// </summary>
    /// <param name="channel">The channel to get the volume of</param>
    /// <returns>The volume of the channel</returns>
    /// <remarks>
    /// This is not an SDL function, and the volume will not be reset when the song finishes
    /// </remarks>
    public static float GetChannelVolumePercentage(int channel) => GetChannelVolume(channel) / 128f;

    /// <summary>
    /// Sets the volume of a specific channel
    /// </summary>
    /// <param name="channel">The channel to get the volume of</param>
    /// <param name="volume">The volume of the channel to set</param>
    /// <returns>The final volume of the channel after it has been set</returns>
    public static int SetChannelVolume(int channel, int volume)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_Volume(check(channel), volume);
    }

    /// <summary>
    /// Sets the volume of a specific channel. Ranges from 0 (0%) to 1 (100%), and WILL be clamped
    /// </summary>
    /// <param name="channel">The channel to get the volume of</param>
    /// <param name="volume">The volume of the channel to set</param>
    /// <returns>The final volume of the channel after it has been set</returns>
    /// <remarks>
    /// This is not an SDL function, and the volume will not be reset when the song finishes
    /// </remarks>
    public static float SetChannelVolumePercentage(int channel, float volume) => SetChannelVolume(channel, (int)(128 * Math.Clamp(volume, 0, 1))) / 128f;

    /// <summary>
    /// Pauses a specific channel
    /// </summary>
    /// <param name="channel">The channel to pause</param>
    public static void Pause(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_Pause(check(channel));
    }

    /// <summary>
    /// Pauses all channels
    /// </summary>
    public static void PauseAll()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_Pause(-1);
    }

    /// <summary>
    /// Resumes a specific channel
    /// </summary>
    /// <param name="channel">The channel to resume</param>
    public static void Resume(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_Resume(check(channel));
    }

    /// <summary>
    /// Resumes all channels
    /// </summary>
    public static void ResumeAll()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_Resume(-1);
    }

    /// <summary>
    /// Halts all channels
    /// </summary>
    /// <remarks>Unlike <see cref="PauseAll"/>, halted channels cannot be resumed, and are finished instead</remarks>
    public static void HaltAll()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_HaltChannel(-1);
    }

    /// <summary>
    /// Halts all channels in group
    /// </summary>
    /// <remarks>Unlike <see cref="PauseAll"/>, halted channels cannot be resumed, and are finished instead</remarks>
    public static void HaltGroup(int group)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_HaltGroup(check(group, 0));
    }

    /// <summary>
    /// Halts a specific channel
    /// </summary>
    /// <param name="channel">The channel to halt</param>
    public static void Halt(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_HaltChannel(check(channel));
    }

    /// <summary>
    /// Halts a specific channel after a set amount of time
    /// </summary>
    /// <param name="channel">The channel to expire</param>
    /// <param name="expiration">The amount of time the channel will have remaining</param>
    public static void Expire(int channel, TimeSpan expiration)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_ExpireChannel(check(channel), (int)expiration.TotalMilliseconds);
    }

    /// <summary>
    /// Halts all channels after a set amount of time
    /// </summary>
    /// <param name="expiration">The amount of time the channels will have remaining</param>
    /// <returns>The amount of channels to expire, whether or not they are active</returns>
    public static int ExpireAll(TimeSpan expiration)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_ExpireChannel(-1, (int)expiration.TotalMilliseconds);
    }

    /// <summary>
    /// Sets a specific channel to fade out, reducing its volume slowly throughout the set amount of time, starting at the time of calling
    /// </summary>
    /// <param name="channel">The channel to fade out</param>
    /// <param name="fadeOut">The amount of time it takes for the channel to fade out</param>
    public static void FadeOut(int channel, TimeSpan fadeOut)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_FadeOutChannel(check(channel), (int)fadeOut.TotalMilliseconds);
    }

    /// <summary>
    /// Sets a specific channel group to fade out, reducing their volume slowly throughout the set amount of time, starting at the time of calling
    /// </summary>
    /// <param name="group">The group of channels to fade out</param>
    /// <param name="fadeOut">The amount of time it takes for the channels to fade out</param>
    public static void FadeOutGroup(int group, TimeSpan fadeOut)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_FadeOutGroup(check(group, 0), (int)fadeOut.TotalMilliseconds);
    }

    /// <summary>
    /// Sets all channels to fade out, reducing their volume slowly throughout the set amount of time, starting at the time of calling
    /// </summary>
    /// <param name="fadeOut">The amount of time it takes for the channels to fade out</param>
    /// <returns>The amount of channels to fade out</returns>
    public static int FadeOutAll(TimeSpan fadeOut)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_FadeOutChannel(-1, (int)fadeOut.TotalMilliseconds);
    }

    /// <summary>
    /// Checks whether a specific channel is playing
    /// </summary>
    /// <param name="channel">The channel to verify</param>
    /// <returns>Whether the channel is playing or not</returns>
    public static bool IsPlaying(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_Playing(check(channel)) is > 0;
    }

    /// <summary>
    /// Counts the amount of channels that are currently playing audio
    /// </summary>
    /// <returns>The amount of channels that are currently playing audio</returns>
    public static int CountPlaying()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_Playing(-1);
    }

    /// <summary>
    /// Checks whether a specific channel is paused
    /// </summary>
    /// <param name="channel">The channel to verify</param>
    /// <returns>Whether the channel is paused or not</returns>
    public static bool IsPaused(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_Paused(check(channel)) is > 0;
    }

    /// <summary>
    /// Counts the amount of channels that are currently paused
    /// </summary>
    /// <returns>The amount of channels that are currently paused</returns>
    public static int CountPaused()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_Paused(-1);
    }

    /// <summary>
    /// Checks whether a specific channel is fading, and if it is, whether it's fading in or out
    /// </summary>
    /// <param name="channel">The channel to check</param>
    /// <returns>Whether the channel is fading in, out, or at all</returns>
    public static AudioFadeStatus IsFading(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return (AudioFadeStatus)Mix_FadingChannel(check(channel));
    }

    /// <summary>
    /// Reserve the set amount of channels from being used when playing samples when passing in -1 as a channel number to playback functions. The channels are reserved starting from channel 0 to num-1.
    /// </summary>
    /// <param name="channels">The amount of channels to reserve, starting from channel 0 to channel <paramref name="channels"/> - 1</param>
    /// <remarks>Attemps to reserve more channels than available are fine, the return value will contain the amount of channels that were actually reserved. Passing 0 will reserve one channel, see <see cref="UnreserveAllChannels"/></remarks>
    /// <returns>The amount of channels that were reserved</returns>
    public static int ReserveChannels(int channels)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_ReserveChannels(check(channels, 0));
    }

    /// <summary>
    /// Unreserves all channels that were reserved with <see cref="ReserveChannels(int)"/>
    /// </summary>
    public static void UnreserveAllChannels()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        Mix_ReserveChannels(-1);
    }

    /// <summary>
    /// Adds the specific channel to the set group
    /// </summary>
    /// <param name="channel">The channel to group</param>
    /// <param name="group">The group to add the channel to</param>
    /// <returns>Whether the channel was succesfully added to the group. If it fails, the channel was probably invalid</returns>
    public static bool AddToGroup(int channel, int group)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupChannel(check(channel), check(group, 0)) == 1;
    }

    /// <summary>
    /// Removes the specific channel from any group.
    /// </summary>
    /// <remarks>This method adds the channel to the default group, -1</remarks>
    /// <param name="channel">The channel to remove from groups</param>
    /// <returns>Whether the channel was succesfully removed from a group</returns>
    public static bool RemoveFromGroup(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupChannel(check(channel), -1) == 1;
    }

    /// <summary>
    /// Adds the channels to the set group
    /// </summary>
    /// <param name="from">The first channel to add, along with every channel until <paramref name="to"/></param>
    /// <param name="group">The group to add the channels to</param>
    /// <param name="to">The last channel to add, along with every channel beginning at <paramref name="from"/></param>
    /// <returns>The amount of channels that were added to the group</returns>
    public static int AddToGroup(int from, int group, int to)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        if (check(from) >= check(to))
            throw new ArgumentException($"{nameof(from)} must be less than {nameof(to)}");

        return Mix_GroupChannels(from, to, group);
    }

    /// <summary>
    /// Removes the channels from any group.
    /// </summary>
    /// <remarks>This method removes the channels to the default group, -1</remarks>
    /// <param name="from">The first channel to remove, along with every channel until <paramref name="to"/></param>
    /// <param name="to">The last channel to remove, along with every channel beginning at <paramref name="from"/></param>
    /// <returns>The amount of channels that were succesfully removed from a group</returns>
    public static int RemoveFromGroup(int from, int to)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        if (check(from) >= check(to))
            throw new ArgumentException($"{nameof(from)} must be less than {nameof(to)}");

        return Mix_GroupChannels(from, to, -1);
    }

    /// <summary>
    /// Finds the first available (not playing) channel in the given group
    /// </summary>
    /// <param name="group">The group to search</param>
    /// <returns>The found channel, or -1, if none was found</returns>
    public static int FindAvailable(int group)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupAvailable(check(group, 0));
    }

    /// <summary>
    /// Finds the first available (not playing) channel
    /// </summary>
    /// <returns>The found channel, or -1, if none was found</returns>
    public static int FindAvailable()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupAvailable(-1);
    }

    /// <summary>
    /// Finds the oldest actively playing channel
    /// </summary>
    /// <returns>The found channel, or -1, if none was found</returns>
    public static int FindOldest()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupOldest(-1);
    }

    /// <summary>
    /// Finds the oldest actively playing channel in the given group
    /// </summary>
    /// <param name="group">The group to query</param>
    /// <returns>The found channel, or -1, if none was found</returns>
    public static int FindOldest(int group)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupOldest(check(group));
    }

    /// <summary>
    /// Finds the newest actively playing channel
    /// </summary>
    /// <returns>The found channel, or -1, if none was found</returns>
    public static int FindNewer()
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupNewer(-1);
    }

    /// <summary>
    /// Finds the newest actively playing channel in the given group
    /// </summary>
    /// <param name="group">The group to query</param>
    /// <returns>The found channel, or -1, if none was found</returns>
    public static int FindNewer(int group)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupNewer(check(group));
    }

    /// <summary>
    /// Counts the number of channels in group <paramref name="group"/>
    /// </summary>
    /// <param name="group">The group to check</param>
    /// <remarks>This can't count channels in the default group, so negative numbers throw</remarks>
    /// <returns>The amount of channels in the group</returns>
    public static int CountChannelsInGroup(int group)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        return Mix_GroupCount(check(group, 0));
    }

    /// <summary>
    /// Gets the chunk that is currently being played by this channel, if one is playing
    /// </summary>
    /// <param name="channel">The channel to check</param>
    /// <returns>An <see cref="AudioChunk"/> object, if the channel is currently playing one</returns>
    public static AudioChunk? GetChunk(int channel)
    {
        AudioMixer.ThrowIfNotInitAndOpen();
        var ptr = Mix_GetChunk(check(channel));
        return ptr == IntPtr.Zero ? null : AudioChunk.Fetch(ptr);
    }

    #region helpers

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int check(int x, [CallerArgumentExpression("x")] string arg = default)
        => x < 0 || x >= ChannelCount ? throw new ArgumentOutOfRangeException(arg) : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int check(int x, int min, [CallerArgumentExpression("x")] string arg = default)
        => x < min ? throw new ArgumentOutOfRangeException(arg) : x;

    #endregion
}