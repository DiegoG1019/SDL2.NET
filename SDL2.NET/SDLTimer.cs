using SDL2.NET.Exceptions;
using SDL2.NET.Platform.WindowsSpecific;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Schedules a callback method to be executed in a separate thread after a given amount of time
/// </summary>
/// <remarks>
/// Timing may be inexact due to OS scheduling. Be sure to note the current time with <see cref="SDLApplication.TotalTime"/> or <see cref="Performance.PerformanceCounter"/> in case your callback needs to adjust for variances. Timers take into account the amount of time it took to execute the callback. For example, if the callback took 250 ms to execute and the Interval now is 1000 ms, the timer would only wait another 750 ms before its next iteration.
/// </remarks>
public class SDLTimer : IDisposable
{
    private readonly Action<SDLTimer, UserData?> Action;
    private readonly UserData? udat;
    private readonly SDL_TimerCallback sdl_cb_del;
    private readonly int Id;

    /// <summary>
    /// Starts a timer that calls a delegate after a given time has passed
    /// </summary>
    /// <param name="interval">The amount of time in milliseconds to wait before executing the callback</param>
    /// <param name="action">The Action to execute</param>
    /// <param name="userData">The <see cref="UserData"/> to pass to the callback</param>
    public SDLTimer(uint interval, Action<SDLTimer, UserData?> action, UserData? userData = null)
    {
        sdl_cb_del = sdl_cb;
        Action = action;
        udat = userData;
        SDLTimerException.ThrowIfEquals(Id = SDL_AddTimer(interval, sdl_cb_del, IntPtr.Zero), 0);
    }

    /// <summary>
    /// Starts a timer that calls a delegate after a given time has passed
    /// </summary>
    /// <param name="interval">The amount of time to wait before executing the callback</param>
    /// <param name="action">The Action to execute</param>
    /// <param name="userData">The <see cref="UserData"/> to pass to the callback</param>
    public SDLTimer(TimeSpan interval, Action<SDLTimer, UserData?> action, UserData? userData = null)
        : this((uint)interval.TotalMilliseconds, action, userData) { }

    /// <summary>
    /// The amount of time in milliseconds to wait before the action is executed
    /// </summary>
    /// <remarks>
    /// Changes to this property only take effect after the current (now previous, if changed) interval has elapsed. That is to say, if this property is changed, the new value will only take effect after the action fires. Not before.
    /// </remarks>
    public uint Interval
    {
        get
        {
            ThrowIfDisposed();
            return interval;
        }

        set
        {
            ThrowIfDisposed();
            interval = value;
        }
    }
    private uint interval;

    /// <summary>
    /// The amount of time to wait before the action is executed
    /// </summary>
    /// <remarks>
    /// Changes to this property only take effect after the current (now previous, if changed) interval has elapsed. That is to say, if this property is changed, the new value will only take effect after the action fires. Not before.
    /// </remarks>
    public TimeSpan IntervalSpan
    {
        get => TimeSpan.FromMilliseconds(Interval);
        set => Interval = (uint)value.TotalMilliseconds;
    }

    private uint sdl_cb(uint inter, IntPtr ptr)
    {
        Action(this, udat);
        return Interval;
    }

    #region IDisposable

    private bool disposedValue;

    /// <summary>
    /// Disposes the object, freeing unmanaged SDL resources
    /// </summary>
    /// <remarks>Try not to call this directly</remarks>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_RemoveTimer(Id);
            disposedValue = true;
        }
    }

    /// <summary>
    /// Finalizes the object, in case it wasn't disposed
    /// </summary>
    ~SDLTimer()
    {
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes the object, freeing unmanaged SDL resources
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(SDLTimer));
    }

    #endregion
}
