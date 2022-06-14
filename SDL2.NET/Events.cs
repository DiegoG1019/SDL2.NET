using SDL2.NET.Input;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Provides a way of configuring certain types of event handling for SDL's Event Queue
/// </summary>
/// <remarks>
/// The actual processing of the events is done under this class, however, they're for internal use only. See <see cref="SDLApplication.UpdateEvents"/> and <see cref="SDLApplication.UpdateEventOnce"/>
/// </remarks>
public static class Events
{
    static Events()
    {
        SDL_SetEventFilter(_filter, IntPtr.Zero);
    }

    /// <summary>
    /// Presents the states (true for enabled or false for disabled) of SDL's events in an indexable form
    /// </summary>
    public static EventStateCollection EventStates { get; } = new();

    /// <summary>
    /// Presents the states of SDL's events in an indexable form
    /// </summary>
    public sealed class EventStateCollection
    {
        /// <summary>
        /// Sets or queries the state of the indexed SDL event
        /// </summary>
        /// <remarks>
        /// If set to false (disabled) the event will automatically be dropped from the event queue and will not be filtered; if set to true (enabled) the event will be processed normally
        /// </remarks>
        public bool this[SDL_EventType @event]
        {
            get => SDL_EventState(@event, SDL_QUERY) == SDL_ENABLE;
            set => SDL_EventState(@event, value ? SDL_ENABLE : SDL_DISABLE);
        }

        internal EventStateCollection() { }
    }

    #region Event Filtering

    private static readonly SDL_EventFilter _filter = Filter_method;
    private static EventFilter? _userfilter;
    private static UserData? _userdat;
    private static int Filter_method(IntPtr udat, SDL_Event e)
    {
        if ((OperatingSystem.IsAndroid() || OperatingSystem.IsIOS()) && e.type == SDL_EventType.SDL_APP_LOWMEMORY)
                SDLApplication.TriggerLowMemory();

        if (OperatingSystem.IsWindows() || OperatingSystem.IsAndroid() || OperatingSystem.IsIOS())
            switch (e.type)
            {
                case SDL_EventType.SDL_APP_TERMINATING:
                    SDLApplication.TriggerTerminating();
                    return 0;
                case SDL_EventType.SDL_APP_LOWMEMORY:
                    return 0;
                case SDL_EventType.SDL_APP_WILLENTERBACKGROUND:
                    SDLApplication.TriggerWillEnterBackground();
                    return 0;
                case SDL_EventType.SDL_APP_DIDENTERBACKGROUND:
                    SDLApplication.TriggerDidEnterBackground();
                    return 0;
                case SDL_EventType.SDL_APP_WILLENTERFOREGROUND:
                    SDLApplication.TriggerWillEnterForeground();
                    return 0;
                case SDL_EventType.SDL_APP_DIDENTERFOREGROUND:
                    SDLApplication.TriggerDidEnterForeground();
                    return 0;
            }

        return _userfilter?.Invoke(_userdat, e) ?? 1;
    }

    private static readonly SDL_EventFilter temp_filter = TempFilter_method;
    private static EventFilter? _tempfilter;
    private static UserData? _tempUdat;
    private static int TempFilter_method(IntPtr udat, SDL_Event e)
    {
        return _tempfilter?.Invoke(_tempUdat, e) ?? 1;
    }

    /// <summary>
    /// An event filter that can be used to filter in or out SDL events
    /// </summary>
    /// <remarks>
    /// This library makes no attempts at presenting SDL's event information in a .NET friendly manner -- You are entirely responsible for handling these events properly. Please make sure there is a good reason for your usage of an event filter
    /// </remarks>
    /// <param name="userData">The user data associated with the filter</param>
    /// <param name="e">An union representing the event that SDL just received</param>
    /// <returns></returns>
    public delegate int EventFilter(UserData? userData, SDL_Event e);

    /// <summary>
    /// Adds a custom event filter for SDL's event queue
    /// </summary>
    /// <remarks>
    /// This library makes no attempts at presenting SDL's event information in a .NET friendly manner -- You are entirely responsible for handling these events properly. Please make sure there is a good reason for your usage of an event filter
    /// </remarks>
    /// <param name="filter">The filter method to call</param>
    /// <param name="userData">The data to attach to the filter. This will be passed to the filter when called</param>
    public static void SetEventFilter(EventFilter filter, UserData? userData = null)
    {
        _userfilter = filter;
        _userdat = userData;
    }

    /// <summary>
    /// Removes the currently set <see cref="EventFilter"/> from use
    /// </summary>
    public static void DiscardEventFilter()
    {
        _userfilter = null;
        _userdat = null;
    }

    /// <summary>
    /// Run a specific filter function on the current event queue, removing any events for which the filter returns 0
    /// </summary>
    public static void FilterEvents(EventFilter filter, UserData? userData = null)
    {
        _tempfilter = filter;
        _tempUdat = userData;
        SDL_FilterEvents(temp_filter, IntPtr.Zero);
        _tempfilter = null;
        _tempUdat = null;
    }

    #endregion

    #region Event Processing

    ///// <summary>
    ///// Updates the Event Queue asynchronously, using <paramref name="parallelization"/> for an amount of simultaneous tasks to use
    ///// </summary>
    ///// <param name="parallelization"></param>
    ///// <returns></returns>
    //public static async Task UpdateAsync(byte parallelization = 5)
    //{

    //}
    // planned for the future, but I need to cherry pick what events can be parallelized and which ones can only run on the main thread,
    // and then benchmark it to make sure it's not a waste of CPU
    // Work, work, work

    /// <summary>
    /// Updates the Event Queue until no more events are available
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Update()
    {
        while (UpdateOnce() != 0) ;
    }

    /// <summary>
    /// Fetches one event from SDL and processes it
    /// </summary>
    /// <returns>The remaining events in the queue</returns>
    //[MethodImpl(MethodImplOptions.AggressiveOptimization)] // Profile before enabling
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int UpdateOnce()
    {
        var i = SDL_PollEvent(out var e);
        if (i == 0)
            return 0;

        switch (e.type)
        {
            case SDL_EventType.SDL_QUIT:
                SDLApplication.TriggerSDLQuitting();
                return i;
            case SDL_EventType.SDL_LOCALECHANGED:
                SDLApplication.TriggerLocaleChanged();
                return i;

            case SDL_EventType.SDL_DISPLAYEVENT:
                Display.TriggerEvent(e.display);
                return i;

            case SDL_EventType.SDL_WINDOWEVENT:
                if (Window.TryGetWindow(e.window.windowID, out var window))
                    window.TriggerEvent(e.window);
                return i;

            case SDL_EventType.SDL_KEYDOWN:
                if (Window.TryGetWindow(e.key.windowID, out window))
                    window.TriggerEventKDown(e.key);
                return i;
            case SDL_EventType.SDL_KEYUP:
                if (Window.TryGetWindow(e.key.windowID, out window))
                    window.TriggerEventKUp(e.key);
                return i;

            case SDL_EventType.SDL_TEXTEDITING:
                if (Window.TryGetWindow(e.edit.windowID, out window))
                    window.TriggerEvent(e.edit);
                return i;
            case SDL_EventType.SDL_TEXTINPUT:
                if (Window.TryGetWindow(e.text.windowID, out window))
                    window.TriggerEvent(e.text);
                return i;
            case SDL_EventType.SDL_KEYMAPCHANGED:
                SDLApplication.TriggerKeyMapChanged();
                return i;

            case SDL_EventType.SDL_MOUSEMOTION:
                if (Window.TryGetWindow(e.motion.windowID, out window))
                    window.TriggerEvent(e.motion);
                return i;
            case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                if (Window.TryGetWindow(e.button.windowID, out window))
                    window.TriggerEventMBDOWN(e.button);
                return i;
            case SDL_EventType.SDL_MOUSEBUTTONUP:
                if (Window.TryGetWindow(e.button.windowID, out window))
                    window.TriggerEventMBUP(e.button);
                return i;
            case SDL_EventType.SDL_MOUSEWHEEL:
                if (Window.TryGetWindow(e.wheel.windowID, out window))
                    window.TriggerEvent(e.wheel);
                return i;

            case SDL_EventType.SDL_JOYAXISMOTION:
                if (Joystick.TryGetJoystick(e.jaxis.which, out var joy))
                    joy.TriggerJoyAxisMotion(e.jaxis);
                return i;
            case SDL_EventType.SDL_JOYBALLMOTION:
                if (Joystick.TryGetJoystick(e.jball.which, out joy))
                    joy.TriggerJoyBallMotion(e.jball);
                return i;
            case SDL_EventType.SDL_JOYHATMOTION: 
                if (Joystick.TryGetJoystick(e.jhat.which, out joy))
                    joy.TriggerJoyHatMotion(e.jhat);
                return i;
            case SDL_EventType.SDL_JOYBUTTONDOWN: 
                if (Joystick.TryGetJoystick(e.jbutton.which, out joy))
                    joy.TriggerJoyButtonDown(e.jbutton);
                return i;
            case SDL_EventType.SDL_JOYBUTTONUP: 
                if (Joystick.TryGetJoystick(e.jbutton.which, out joy))
                    joy.TriggerJoyButtonUp(e.jbutton);
                return i;
            case SDL_EventType.SDL_JOYDEVICEADDED:
                Joystick.TriggerJoyDeviceAdded(e.jdevice);
                return i;
            case SDL_EventType.SDL_JOYDEVICEREMOVED:
                if (Joystick.TryGetJoystick(e.jhat.which, out joy))
                    joy.TriggerRemoved(e.jdevice);
                Joystick.TriggerJoyDeviceRemoved(e.jdevice);
                return i;

            case SDL_EventType.SDL_CONTROLLERAXISMOTION:
                if (GameController.TryGetGameController(e.caxis.which, out var gc)) 
                    gc.TriggerControllerAxisMotion(e.caxis);
                return i;
            case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                if (GameController.TryGetGameController(e.cbutton.which, out gc))
                    gc.TriggerControllerButtonDown(e.cbutton);
                return i;
            case SDL_EventType.SDL_CONTROLLERBUTTONUP: 
                if (GameController.TryGetGameController(e.cbutton.which, out gc))
                    gc.TriggerControllerButtonUp(e.cbutton);
                return i;
            case SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                GameController.TriggerControllerDeviceAdded(e.cdevice);
                return i;
            case SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                if (GameController.TryGetGameController(e.cdevice.which, out gc))
                    gc.TriggerRemoved(e.cdevice);
                GameController.TriggerControllerDeviceRemoved(e.cdevice);
                return i;
            case SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED:
                if (GameController.TryGetGameController(e.cdevice.which, out gc))
                    gc.TriggerRemapped(e.cdevice);
                return i;
            case SDL_EventType.SDL_CONTROLLERTOUCHPADDOWN: 
                if (GameController.TryGetGameController(e.ctouchpad.which, out gc))
                    gc.TriggerControllerPadDown(e.ctouchpad);
                return i;
            case SDL_EventType.SDL_CONTROLLERTOUCHPADMOTION: 
                if (GameController.TryGetGameController(e.ctouchpad.which, out gc))
                    gc.TriggerControllerPadMotion(e.ctouchpad);
                return i;
            case SDL_EventType.SDL_CONTROLLERTOUCHPADUP: 
                if (GameController.TryGetGameController(e.ctouchpad.which, out gc))
                    gc.TriggerControllerPadUp(e.ctouchpad);
                return i;
            case SDL_EventType.SDL_CONTROLLERSENSORUPDATE:
#warning Not Implemented
                return i;

            case SDL_EventType.SDL_FINGERDOWN:
                Touch.TriggerFingerPressed(e.tfinger);
                return i;
            case SDL_EventType.SDL_FINGERUP: 
                Touch.TriggerFingerReleased(e.tfinger);
                return i;
            case SDL_EventType.SDL_FINGERMOTION: 
                Touch.TriggerFingerMotion(e.tfinger);
                return i;
            case SDL_EventType.SDL_DOLLARGESTURE: 
                Touch.TriggerDollarGesture(e.dgesture);
                return i;
            case SDL_EventType.SDL_DOLLARRECORD: 
                Touch.TriggerDollarRecord(e.dgesture);
                return i;
            case SDL_EventType.SDL_MULTIGESTURE: 
                Touch.TriggerMultiGesture(e.mgesture);
                return i;

            case SDL_EventType.SDL_CLIPBOARDUPDATE:
                Clipboard.TriggerClipboardChanged();
                return i;

            case SDL_EventType.SDL_DROPFILE:
                if (Window.TryGetWindow(e.drop.windowID, out window))
                    window.TriggerDropFile(e.drop);
                return i;

            case SDL_EventType.SDL_DROPTEXT:
                if (Window.TryGetWindow(e.drop.windowID, out window))
                    window.TriggerDropText(e.drop);
                return i;

            case SDL_EventType.SDL_DROPBEGIN:
                if (Window.TryGetWindow(e.drop.windowID, out window))
                    window.TriggerDropBegan(e.drop);
                return i;

            case SDL_EventType.SDL_DROPCOMPLETE:
                if (Window.TryGetWindow(e.drop.windowID, out window))
                    window.TriggerDropCompleted(e.drop);
                return i;

            case SDL_EventType.SDL_AUDIODEVICEADDED:
                Audio.TriggerDeviceAdded(e.adevice);
                return i;

            case SDL_EventType.SDL_AUDIODEVICEREMOVED:
                Audio.TriggerDeviceRemoved(e.adevice);
                return i;

            case SDL_EventType.SDL_SENSORUPDATE:
                #warning Not Implemented, also not in the web docs
                return i;

            case SDL_EventType.SDL_RENDER_TARGETS_RESET:
                SDLApplication.TriggerRenderTargetsReset();
                return i;

            case SDL_EventType.SDL_RENDER_DEVICE_RESET:
                SDLApplication.TriggerRenderDeviceReset();
                return i;

            case SDL_EventType.SDL_POLLSENTINEL:
                #warning Not Implemented, also not in the web docs
                return i;
        }

        return i;
    }

    #endregion
}
