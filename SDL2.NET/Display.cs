using SDL2.NET.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Provides methods and properties to interact with the currently plugged displays
/// </summary>
public static class Display
{
    /// <summary>
    /// Gets or Sets whether the screensaver is currently enabled
    /// </summary>
    public static bool IsScreenSaverEnabled
    {
        get => SDL_IsScreenSaverEnabled() is SDL_bool.SDL_TRUE;
        set
        {
            if (value)
            {
                SDL_EnableScreenSaver();
                return;
            }
            SDL_DisableScreenSaver();
        }
    }

    /// <summary>
    /// Get the name of the currently initialized video driver or null if no driver has been initialized
    /// </summary>
    public static string? CurrentVideoDriver => SDL_GetCurrentVideoDriver();

    /// <summary>
    /// Information about all the video drivers compiled into SDL
    /// </summary>
    /// <returns></returns>
    public static IReadOnlyList<string> VideoDrivers { get; } = new DriverInfoCollection();

    /// <summary>
    /// Represents a list of the currently plugged displays
    /// </summary>
    public static IReadOnlyList<DisplayInfo> Displays { get; } = new DisplayInfoCollection();

    #region Events

    /// <summary>
    /// Represents an SDL Display Event
    /// </summary>
    /// <param name="timestamp">The amount of time that has passed since SDL's initialization</param>
    /// <param name="display">Information about the display the event ocurred in</param>
    public delegate void DisplayEvent(TimeSpan timestamp, DisplayInfo display);

    /// <summary>
    /// The orientation of a display changed
    /// </summary>
    public static event DisplayEvent? OrientationChanged;

    /// <summary>
    /// A new display has been connected
    /// </summary>
    public static event DisplayEvent? DisplayConnected;
    
    /// <summary>
    /// A display has been disconnected
    /// </summary>
    public static event DisplayEvent? DisplayDisconnected;

    internal static void TriggerEvent(SDL_DisplayEvent e)
    {
        switch (e.displayEvent)
        {
            case SDL_DisplayEventID.SDL_DISPLAYEVENT_ORIENTATION:
                OrientationChanged?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), Displays[(int)e.display]);
                return;
            case SDL_DisplayEventID.SDL_DISPLAYEVENT_CONNECTED:
                DisplayConnected?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), Displays[(int)e.display]);
                return;
            case SDL_DisplayEventID.SDL_DISPLAYEVENT_DISCONNECTED:
                DisplayDisconnected?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), Displays[(int)e.display]);
                return;
        }
        return;
    }

    #endregion

    #region display collection

    private class DriverInfoCollection : IReadOnlyList<string>
    {
        public string this[int index] => SDL_GetVideoDriver(index);

        public int Count
        {
            get
            {
                var r = SDL_GetNumVideoDrivers();
                SDLDisplayException.ThrowIfLessThan(r, 0);
                return r;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class DisplayInfoCollection : IReadOnlyList<DisplayInfo>
    {
        public DisplayInfo this[int index] => new(index);

        public int Count
        {
            get
            {
                var r = SDL_GetNumVideoDisplays();
                SDLDisplayException.ThrowIfLessThan(r, 0);
                return r;
            }
        }

        public IEnumerator<DisplayInfo> GetEnumerator()
        {
            for (int i = 0; i < Count; i++) 
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion
}
