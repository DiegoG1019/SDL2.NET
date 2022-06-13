using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

internal static class Events
{
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
    public static void Update()
    {
        while (UpdateOnce() != 0) ;
    }

    /// <summary>
    /// Fetches one event from SDL and processes it
    /// </summary>
    /// <returns>The remaining events in the queue</returns>
    //[MethodImpl(MethodImplOptions.AggressiveOptimization)] // Profile before enabling
    public static int UpdateOnce()
    {
        var i = SDL_PollEvent(out var e);
        if (i == 0)
            return 0;

        if (OperatingSystem.IsWindows() || OperatingSystem.IsAndroid() || OperatingSystem.IsIOS())
            switch (e.type)
            {
                case SDL_EventType.SDL_APP_TERMINATING:
                    SDLApplication.TriggerTerminating();
                    return i;
                case SDL_EventType.SDL_APP_LOWMEMORY:
                    SDLApplication.TriggerLowMemory();
                    return i;
                case SDL_EventType.SDL_APP_WILLENTERBACKGROUND:
                    SDLApplication.TriggerWillEnterBackground();
                    return i;
                case SDL_EventType.SDL_APP_DIDENTERBACKGROUND:
                    SDLApplication.TriggerDidEnterBackground();
                    return i;
                case SDL_EventType.SDL_APP_WILLENTERFOREGROUND:
                    SDLApplication.TriggerWillEnterForeground();
                    return i;
                case SDL_EventType.SDL_APP_DIDENTERFOREGROUND:
                    SDLApplication.TriggerDidEnterForeground();
                    return i;
            }

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
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_JOYBALLMOTION: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_JOYHATMOTION: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_JOYBUTTONDOWN: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_JOYBUTTONUP: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_JOYDEVICEADDED: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_JOYDEVICEREMOVED: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERAXISMOTION: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERBUTTONDOWN: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERBUTTONUP: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERDEVICEADDED: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERDEVICEREMOVED: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERTOUCHPADDOWN: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERTOUCHPADMOTION: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERTOUCHPADUP: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_CONTROLLERSENSORUPDATE: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_FINGERDOWN: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_FINGERUP: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_FINGERMOTION: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_DOLLARGESTURE: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_DOLLARRECORD: 
                #warning Not Implemented
                return i;

            case SDL_EventType.SDL_MULTIGESTURE: 
                #warning Not Implemented
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
}
