using SDL2.Bindings;
using SDL2.NET.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Represents a Game Controller
/// </summary>
/// <remarks>
/// Keep in mind that this class does NOT override Joystick's methods, the methods found here that share the same name as methods in Joystick are DIFFERENT methods altogether. Calling those methods while handling an object of type <see cref="GameController"/> will yield different results than calling them when handling an object of type <see cref="Joystick"/>
/// </remarks>
public class GameController : Joystick, IDisposable
{
    private static readonly Dictionary<int, GameController> playerDict = new(4);

    internal readonly IntPtr gchandle;

    internal GameController(IntPtr handle, int deviceIndex) : base(SDL_GameControllerGetJoystick(handle), deviceIndex)
    {
        gchandle = handle;
        gc = true;
        Axes = new IndexableAxisState(handle);
        Buttons = new IndexableButtonState(handle);
        TouchPad = new(handle);
    }

    /// <summary>
    /// Attempts to open and instantiate a new GameController, or return an old one if already instantiated
    /// </summary>
    /// <param name="index">The same joystick index passed to <see cref="Joystick.TryOpen(int, out Joystick?)"/></param>
    /// <param name="gameController">The fetched GameController</param>
    /// <returns></returns>
    public static bool TryOpen(int index, [NotNullWhen(true)] out GameController? gameController)
    {
        Lock();
        try
        {
            var id = SDL_JoystickGetDeviceInstanceID(index);
            if (JoystickDict.TryGetValue(id, out var wr))
                if (wr.TryGetTarget(out var joystick) && joystick.disposedValue is false)
                    return ((IJoystickDefinition)joystick).IsGameController(out gameController);
                else
                    JoystickDict.Remove(id, out _);

            var f = SDL_GameControllerFromInstanceID(id);
            var p = f != IntPtr.Zero ? f : SDL_GameControllerOpen(index);
            if (p != IntPtr.Zero)
            {
                gameController = null;
                return false;
            }
            gameController = new(p, index);
            return true;
        }
        finally
        {
            Unlock();
        }
    }

    /// <summary>
    /// Attempts to get the Joystick represented by the given instanceId
    /// </summary>
    public static bool TryGetGameController(int instanceId, [NotNullWhen(true)] out GameController? gamecontroller)
    {
        if (JoystickDict.TryGetValue(instanceId, out var wr))
            if (wr.TryGetTarget(out var j) && j.disposedValue is false)
            {
                if (j is GameController gc) 
                {
                    gamecontroller = gc;
                    return true;
                }
            }
            else
                JoystickDict.Remove(instanceId, out _);

        gamecontroller = null;
        return false;
    }

    /// <summary>
    /// Add support for controllers that SDL is unaware of or to cause an existing controller to have a different binding. <see cref="SDL_GameControllerAddMapping" href="https://wiki.libsdl.org/SDL_GameControllerAddMapping"/>
    /// </summary>
    /// <remarks>
    /// The mapping string has the format "GUID,name,mapping", where GUID is the string value from SDL_JoystickGetGUIDString(), name is the human readable string for the device and mappings are controller mappings to joystick ones. Under Windows there is a reserved GUID of "xinput" that covers all XInput devices. The mapping format for joystick is: {| |bX |a joystick button, index X |- |hX.Y |hat X with value Y |- |aX |axis X of the joystick |} Buttons can be used as a controller axes and vice versa.
    /// </remarks>
    /// <returns>true if the mapping was added, false if updated.</returns>
    /// <exception cref="SDLJoystickException"></exception>
    public static bool AddMapping(string mapping)
    {
        var r = SDL_GameControllerAddMapping(mapping);
        SDLJoystickException.ThrowIfLessThan(r, 0);
        return r == 1;
    }

    /// <summary>
    /// Gets whether or not this <see cref="GameController"/> is currently attached
    /// </summary>
    public bool IsAttached => SDL_GameControllerGetAttached(gchandle) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Provides a count of installed mappings and a means of accessing them
    /// </summary>
    public static IReadOnlyList<string> Mappings { get; } = new MappingCollection();

    /// <summary>
    /// Use this function to load a set of Game Controller mappings from a file, filtered by the current Platform. A community sourced database of controllers is available here: <see href="https://raw.githubusercontent.com/gabomdq/SDL_GameControllerDB/master/gamecontrollerdb.txt"/> (on GitHub). <see cref="SDL_GameControllerAddMappingsFromFile" href="https://wiki.libsdl.org/SDL_GameControllerAddMappingsFromFile"/>
    /// </summary>
    /// <param name="file">The file to load</param>
    /// <returns>I'm not sure, the library doesn't really say. See this issue for more https://github.com/DiegoG1019/SDL2.NET/issues/34</returns>
    public static int AddMappingsFromFile(string file)
        => SDL_GameControllerAddMappingsFromFile(file);

    /// <summary>
    /// Get the <see cref="GameController"/> mapping string for a given <see cref="Joystick"/>'s <see cref="Guid"/>
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="SDLJoystickException"></exception>
    public static string GetMapping(Guid guid)
        => SDL_GameControllerMappingForGUID(guid) ?? throw new SDLJoystickException(SDL_GetAndClearError());

    /// <summary>
    /// Get the <see cref="GameController"/> mapping string for a given <see cref="Joystick"/>'s device index
    /// </summary>
    /// <param name="deviceIndex"></param>
    /// <returns></returns>
    /// <exception cref="SDLJoystickException"></exception>
    public static string GetMapping(int deviceIndex)
        => SDL_GameControllerMappingForDeviceIndex(deviceIndex) ?? throw new SDLJoystickException(SDL_GetAndClearError());

    /// <summary>
    /// Get the implementation-dependent name for an opened <see cref="GameController"/>, or null if there is no name or the identifier passed is invalid.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string GetName(int index)
        => SDL_GameControllerNameForIndex(index);

    /// <summary>
    /// Get the <see cref="GameControllerType"/> of the Game Controller at index <paramref name="index"/>
    /// </summary>
    /// <param name="index">The device index of the controller</param>
    /// <returns></returns>
    public static GameControllerType GetType(int index)
        => (GameControllerType)SDL_GameControllerTypeForIndex(index);

    /// <summary>
    /// Whether Game Controller Events are enabled or not
    /// </summary>
    public static bool ControllerEventsEnabled
    {
        get => SDL_GameControllerEventState(SDL_QUERY) == 1;
        set => SDL_GameControllerEventState(value ? SDL_ENABLE : SDL_IGNORE);
    }

    /// <summary>
    /// Manually pump <see cref="GameController"/> updates if <see cref="ControllerEventsEnabled"/> is false
    /// </summary>
    public static void ControllerUpdate()
    {
        SDL_GameControllerUpdate();
    }

    /// <summary>
    /// Convert a string into <see cref="GameControllerAxis"/> enum
    /// </summary>
    /// <param name="str"></param>
    /// <remarks>
    /// This function is called internally to translate <see cref="GameController"/> mapping strings for the underlying joystick device into the consistent <see cref="GameController"/> mapping. You do not normally need to call this function unless you are parsing <see cref="GameController"/> mappings in your own code.
    /// </remarks>
    public static GameControllerAxis GetAxis(string str)
        => (GameControllerAxis)SDL_GameControllerGetAxisFromString(str);

    /// <summary>
    /// Convert a string into <see cref="GameControllerAxis"/> enum
    /// </summary>
    /// <param name="str"></param>
    /// <remarks>
    /// This function is called internally to translate <see cref="GameController"/> mapping strings for the underlying joystick device into the consistent <see cref="GameController"/> mapping. You do not normally need to call this function unless you are parsing <see cref="GameController"/> mappings in your own code.
    /// </remarks>
    public static GameControllerButton GetButton(string str)
        => (GameControllerButton)SDL_GameControllerGetButtonFromString(str);

    /// <summary>
    /// Return the sfSymbolsName for a given button on a game controller on Apple platforms.
    /// </summary>
    /// <param name="button"></param>
    /// <returns>Returns the sfSymbolsName or null if the name can't be found</returns>
    [SupportedOSPlatform("iOS")]
    public string? GetButtonAppleSFSymbolsName(GameControllerButton button)
        => SDL_GameControllerGetAppleSFSymbolsNameForButton(gchandle, (SDL_GameControllerButton)button);

    /// <summary>
    /// Return the sfSymbolsName for a given axis on a game controller on Apple platforms.
    /// </summary>
    /// <param name="axis"></param>
    /// <returns>Returns the sfSymbolsName or null if the name can't be found</returns>
    [SupportedOSPlatform("iOS")]
    public string? GetAxisAppleSFSymbolsName(GameControllerAxis axis)
        => SDL_GameControllerGetAppleSFSymbolsNameForAxis(gchandle, (SDL_GameControllerAxis)axis);

    /// <summary>
    /// Gets or sets the current player index for this <see cref="GameController"/>
    /// </summary>
    /// <remarks>
    /// This same <see cref="GameController"/> can later be retrieved by calling <see cref="FromPlayerIndex(int)"/>
    /// </remarks>
    public int PlayerIndex
    {
        get => SDL_GameControllerGetPlayerIndex(gchandle);
        set
        {
            SDL_GameControllerSetPlayerIndex(gchandle, value);
            playerDict[value] = this;
        }
    }

    /// <summary>
    /// This method is NOT implemented and should NOT be used yet. Will be implemented in the future
    /// </summary>
    /// <param name="_"></param>
    /// <exception cref="NotImplementedException"></exception>
    [Obsolete("This method is NOT implemented and should NOT be used yet. Will be implemented in the future")]
    public static void FromPlayerIndex(int _) => throw new NotImplementedException();

    /// <summary>
    /// Get the <see cref="GameController"/> mapping for this instance
    /// </summary>
    public string Mapping => SDL_GameControllerMapping(gchandle) ?? throw new SDLJoystickException(SDL_GetAndClearError());

    /// <summary>
    /// Get the implementation-dependent name for an opened <see cref="GameController"/>, or null if there is no name or the identifier passed is invalid.
    /// </summary>
    /// <remarks>
    /// This is the same name as returned by <see cref="GetName(int)"/>, but it uses a <see cref="GameController"/> instance instead of the (unstable) device index.
    /// </remarks>
    public string? ControllerName => SDL_GameControllerName(gchandle);

    /// <summary>
    /// Whether or not this <see cref="GameController"/> has an LED that can be accessed from SDL
    /// </summary>
    public bool HasLED => SDL_GameControllerHasLED(gchandle) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Sets the color of this <see cref="GameController"/>'s LED.
    /// </summary>
    /// <remarks>This method throws if the <see cref="GameController"/> does not have an LED, see <see cref="HasLED"/></remarks>
    /// <param name="color"></param>
    public void SetLEDColor(RGBColor color)
        => SDLJoystickException.ThrowIfLessThan(SDL_GameControllerSetLED(gchandle, color.Red, color.Green, color.Blue), 0, "This GameController does not have a valid LED");

    /// <summary>
    /// Gets the vendor code of this <see cref="GameController"/>
    /// </summary>
    public ushort Vendor => SDL_GameControllerGetVendor(gchandle);

    /// <summary>
    /// Gets the product code of this <see cref="GameController"/>
    /// </summary>
    public ushort Product => SDL_GameControllerGetProduct(gchandle);

    /// <summary>
    /// Gets the product version code of this <see cref="GameController"/>
    /// </summary>
    public ushort ProductVersion => SDL_GameControllerGetProductVersion(gchandle);

    /// <summary>
    /// Gets the serial number of this <see cref="GameController"/>
    /// </summary>
    public string Serial => _serial ??= SDL_GameControllerGetSerial(gchandle);
    private string? _serial;

    /// <summary>
    /// Gets the type that this <see cref="GameController"/> belongs to
    /// </summary>
    public GameControllerType Type => (GameControllerType)SDL_GameControllerGetType(gchandle);

    /// <summary>
    /// Represents a way of querying the current state of and availability of an axis control on a <see cref="GameController"/>. 
    /// </summary>
    /// <remarks>
    /// The values of the dictionary represent the axis' state. You may query the existence and availability of an axis by using <see cref="IReadOnlyDictionary{GameControllerAxis, short}.ContainsKey(GameControllerAxis)"/>. This merely reports whether the controller's mapping defined this axis, as that is all the information SDL has about the physical device.
    /// </remarks>
    public IReadOnlyDictionary<GameControllerAxis, short> Axes { get; }

    /// <summary>
    /// Represents a way of querying the current state of an button control on a <see cref="GameController"/>
    /// </summary>
    /// <remarks>
    /// The values of the dictionary represent the button' state. You may query the existence and availability of an button by using <see cref="IReadOnlyDictionary{GameControllerButton, short}.ContainsKey(GameControllerButton)"/>. This merely reports whether the controller's mapping defined this button, as that is all the information SDL has about the physical device.
    /// </remarks>
    public IReadOnlyDictionary<GameControllerButton, short> Buttons { get; }

    /// <summary>
    /// Represents a way of querying the current state of a TouchPad, if any on a <see cref="GameController"/>
    /// </summary>
    public IndexableTouchPadState TouchPad { get; }

    /// <summary>
    /// Start a rumble effect on a <see cref="GameController"/>
    /// </summary>
    /// <param name="lowFreqRumble">The frequency of the low frequency motor</param>
    /// <param name="highFreqRumble">The frequency of the high frequency motor</param>
    /// <param name="durationMs">The duration of the rumble in milliseconds</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling it with 0 intensity stops any rumbling.
    /// </remarks>
    public void Rumble(ushort lowFreqRumble, ushort highFreqRumble, uint durationMs)
        => SDLJoystickException.ThrowIfLessThan(SDL_GameControllerRumble(gchandle, lowFreqRumble, highFreqRumble, durationMs), 0, "Rumble isn't supported on this GameController");

    /// <summary>
    /// Start a rumble effect on a <see cref="GameController"/>
    /// </summary>
    /// <param name="lowFreqRumble">The frequency of the low frequency motor</param>
    /// <param name="highFreqRumble">The frequency of the high frequency motor</param>
    /// <param name="duration">The duration of the rumble</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling it with 0 intensity stops any rumbling.
    /// </remarks>
    public void Rumble(ushort lowFreqRumble, ushort highFreqRumble, TimeSpan duration)
        => SDLJoystickException.ThrowIfLessThan(SDL_GameControllerRumble(gchandle, lowFreqRumble, highFreqRumble, (uint)duration.TotalMilliseconds), 0, "Rumble isn't supported on this GameController");

    /// <summary>
    /// Whether or not this <see cref="GameController"/> supports rumble
    /// </summary>
    public bool IsRumbleSupported => SDL_GameControllerHasRumble(gchandle) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Start a rumble effect on a <see cref="GameController"/>'s triggers
    /// </summary>
    /// <param name="lowFreqRumble">The frequency of the low frequency motor</param>
    /// <param name="highFreqRumble">The frequency of the high frequency motor</param>
    /// <param name="durationMs">The duration of the rumble in milliseconds</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling it with 0 intensity stops any rumbling. Note that this is rumbling of the triggers and not the game controller as a whole. This is currently only supported on Xbox One controllers. If you want the (more common) whole-controller rumble, use <see cref="Rumble(ushort, ushort, uint)"/> instead.
    /// </remarks>
    public void RumbleTriggers(ushort lowFreqRumble, ushort highFreqRumble, uint durationMs)
        => SDLJoystickException.ThrowIfLessThan(SDL_GameControllerRumbleTriggers(gchandle, lowFreqRumble, highFreqRumble, durationMs), 0, "Trigger Rumble isn't supported on this GameController");

    /// <summary>
    /// Start a rumble effect on a <see cref="GameController"/>'s triggers
    /// </summary>
    /// <param name="lowFreqRumble">The frequency of the low frequency motor</param>
    /// <param name="highFreqRumble">The frequency of the high frequency motor</param>
    /// <param name="duration">The duration of the rumble</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling it with 0 intensity stops any rumbling. Note that this is rumbling of the triggers and not the game controller as a whole. This is currently only supported on Xbox One controllers. If you want the (more common) whole-controller rumble, use <see cref="Rumble(ushort, ushort, TimeSpan)"/> instead.
    /// </remarks>
    public void RumbleTriggers(ushort lowFreqRumble, ushort highFreqRumble, TimeSpan duration)
        => SDLJoystickException.ThrowIfLessThan(SDL_GameControllerRumbleTriggers(gchandle, lowFreqRumble, highFreqRumble, (uint)duration.TotalMilliseconds), 0, "Trigger Rumble isn't supported on this GameController");

    /// <summary>
    /// Whether or not this <see cref="GameController"/> supports rumble on the controller's triggers
    /// </summary>
    public bool IsTriggerRumbleSupported => SDL_GameControllerHasRumbleTriggers(gchandle) == SDL_bool.SDL_TRUE;

    #region IDisposable

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_GameControllerClose(gchandle);
            SDL_JoystickClose(_handle);
            disposedValue = true;
        }
    }

    ~GameController()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    internal void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(GameController));
    }

    #endregion

    #region collection classes

    /// <summary>
    /// Represents a Touchpad State that can be indexed
    /// </summary>
    public class IndexableTouchPadState
    {
        private readonly IntPtr handle;

        internal IndexableTouchPadState(IntPtr handle)
        {
            this.handle = handle;
        }

        /// <summary>
        /// The amount of touchpads this <see cref="GameController"/> has
        /// </summary>
        public int Count => SDL_GameControllerGetNumTouchpads(handle);

        /// <summary>
        /// Gets the amount of fingers currently in contact with the indexed touchpad
        /// </summary>
        /// <param name="touchpad"></param>
        /// <returns></returns>
        public int this[int touchpad]
        {
            get
            {
                var x = SDL_GameControllerGetNumTouchpadFingers(handle, touchpad);
                SDLJoystickException.ThrowIfLessThan(x, 0);
                return x;
            }
        }

        /// <summary>
        /// Gets information regarding the current fingerpress on the given touchpad
        /// </summary>
        /// <param name="touchpad"></param>
        /// <param name="finger"></param>
        /// <returns></returns>
        public FingerReport this[int touchpad, int finger]
        {
            get
            {
                var r = SDL_GameControllerGetTouchpadFinger(handle, touchpad, finger, out var state, out var x, out var y, out var pressure);
                SDLJoystickException.ThrowIfLessThan(r, 0);
                return new(state, x, y, pressure);
            }
        }
    }

    private class IndexableButtonState : IReadOnlyDictionary<GameControllerButton, short>
    {
        private readonly IntPtr _handle;
        private readonly HashSet<GameControllerButton> buttons;

        internal IndexableButtonState(IntPtr ptr)
        {
            _handle = ptr;

            var bl = Enum.GetValues<GameControllerButton>();
            HashSet<GameControllerButton> butts = new(bl.Length);
            foreach (var b in Enum.GetValues<GameControllerButton>())
            {
                if (SDL_GameControllerHasButton(_handle, (SDL_GameControllerButton)b) == SDL_bool.SDL_TRUE)
                    butts.Add(b);
            }

            buttons = butts;
        }

        public short this[GameControllerButton axis]
        {
            get
            {
                var x = SDL_GameControllerGetButton(_handle, (SDL_GameControllerButton)axis);
                return x == 0 && INTERNAL_SDL_GetError() != IntPtr.Zero ? throw new SDLJoystickException(SDL_GetAndClearError()) : x;
            }
        }

        public bool ContainsKey(GameControllerButton key) => buttons.Contains(key);

        public bool TryGetValue(GameControllerButton key, [MaybeNullWhen(false)] out short value)
        {
            value = SDL_GameControllerGetButton(_handle, (SDL_GameControllerButton)key);
            return value == 0 && INTERNAL_SDL_GetError() != IntPtr.Zero;
        }

        public IEnumerable<GameControllerButton> Keys => buttons;
        public IEnumerable<short> Values
        {
            get
            {
                foreach (var b in buttons) 
                    yield return this[b];
            }
        }

        public int Count => buttons.Count;

        public IEnumerator<KeyValuePair<GameControllerButton, short>> GetEnumerator()
        {
            foreach (var b in buttons)
                yield return new(b, this[b]);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class IndexableAxisState : IReadOnlyDictionary<GameControllerAxis, short>
    {
        private readonly IntPtr _handle;
        private readonly HashSet<GameControllerAxis> axe;

        internal IndexableAxisState(IntPtr ptr)
        {
            _handle = ptr;

            var bl = Enum.GetValues<GameControllerAxis>();
            HashSet<GameControllerAxis> butts = new(bl.Length);
            foreach (var b in Enum.GetValues<GameControllerAxis>())
            {
                if (SDL_GameControllerHasAxis(_handle, (SDL_GameControllerAxis)b) == SDL_bool.SDL_TRUE)
                    butts.Add(b);
            }
            axe = butts;
        }

        public short this[GameControllerAxis axis]
        {
            get
            {
                var x = SDL_GameControllerGetAxis(_handle, (SDL_GameControllerAxis)axis);
                return x == 0 && INTERNAL_SDL_GetError() != IntPtr.Zero ? throw new SDLJoystickException(SDL_GetAndClearError()) : x;
            }
        }

        public bool ContainsKey(GameControllerAxis key) => axe.Contains(key);

        public bool TryGetValue(GameControllerAxis key, [MaybeNullWhen(false)] out short value)
        {
            value = SDL_GameControllerGetAxis(_handle, (SDL_GameControllerAxis)key);
            return value == 0 && INTERNAL_SDL_GetError() != IntPtr.Zero;
        }

        public IEnumerable<GameControllerAxis> Keys => axe;
        public IEnumerable<short> Values
        {
            get
            {
                foreach (var b in axe)
                    yield return this[b];
            }
        }

        public int Count => axe.Count;

        public IEnumerator<KeyValuePair<GameControllerAxis, short>> GetEnumerator()
        {
            foreach (var b in axe)
                yield return new(b, this[b]);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class MappingCollection : IReadOnlyList<string>
    {
        public string this[int index] => index < 0 || index >= Mappings.Count
                    ? throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 and {Mappings.Count}")
                    : SDL_GameControllerMappingForIndex(index);

        public int Count
        {
            get
            {
                var r = SDL_GameControllerNumMappings();
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

    #endregion
}
