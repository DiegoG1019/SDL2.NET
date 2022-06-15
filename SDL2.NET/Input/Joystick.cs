using SDL2.NET.Exceptions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Represents a Joystick
/// </summary>
public class Joystick : IJoystickDefinition, IDisposable, IHandle
{
    private static readonly Dictionary<int, Joystick> playerDict = new(4);

    internal readonly IntPtr _handle;
    internal static readonly ConcurrentDictionary<int, WeakReference<Joystick>> JoystickDict = new(4, 10);

    IntPtr IHandle.Handle => _handle;

    /// <summary>
    /// The unique identification for this <see cref="Joystick"/>. Not the same as deviceIndex, as this one is more stable
    /// </summary>
    public int Id { get; }

    internal Joystick(IntPtr handle, int deviceIndex)
    {
        Id = SDL_JoystickGetDeviceInstanceID(deviceIndex);
        _handle = handle;
        JoystickDict[Id] = new(this);
        Axe = new(handle);
        Buttons = new ButtonStateCollection(handle);
        Hats = new HatStateCollection(handle);
        Balls = new BallStateCollection(handle);
    }

    /// <summary>
    /// Gets the amount of joysticks that are currently connected
    /// </summary>
    public static int JoystickCount => SDL_NumJoysticks();

    /// <summary>
    /// Locking for multi-threaded access to the joystick API
    /// </summary>
    /// <remarks>
    /// If you are using the joystick API or handling events from multiple threads you should use these locking functions to protect access to the joysticks. In particular, you are guaranteed that the joystick list won't change, so the API functions that take a joystick index will be valid, and joystick and game controller events will not be delivered.
    /// </remarks>
    public static void Lock() => SDL_LockJoysticks();

    /// <summary>
    /// Unlocking for multi-threaded access to the joystick API
    /// </summary>
    /// <remarks>
    /// If you are using the joystick API or handling events from multiple threads you should use these locking functions to protect access to the joysticks. In particular, you are guaranteed that the joystick list won't change, so the API functions that take a joystick index will be valid, and joystick and game controller events will not be delivered.
    /// </remarks>
    public static void Unlock() => SDL_UnlockJoysticks();

    /// <summary>
    /// Obtains a report on a given Joystick by its deviceIndex
    /// </summary>
    /// <param name="deviceIndex"></param>
    /// <returns></returns>
    public static JoystickReport GetJoystickInfo(int deviceIndex)
        => new
        (
            SDL_JoystickGetDeviceGUID(deviceIndex),
            SDL_JoystickGetDeviceVendor(deviceIndex),
            SDL_JoystickGetDeviceProduct(deviceIndex),
            SDL_JoystickGetDeviceProductVersion(deviceIndex),
            (JoystickType)SDL_JoystickGetDeviceType(deviceIndex),
            SDL_JoystickGetDeviceInstanceID(deviceIndex),
            SDL_JoystickNameForIndex(deviceIndex)
        );

    /// <summary>
    /// Gets whether or not this <see cref="Joystick"/> is currently attached
    /// </summary>
    public bool IsAttached => SDL_JoystickGetAttached(_handle) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Provides a method of querying a <see cref="Joystick"/>'s various axe
    /// </summary>
    /// <remarks>'Axe' is plural for 'Axis'</remarks>
    public AxisStateCollection Axe { get; }

    /// <summary>
    /// Provides a method of querying a <see cref="Joystick"/>'s various buttons
    /// </summary>
    public IReadOnlyCollection<bool> Buttons { get; }

    /// <summary>
    /// Provides a method of querying a <see cref="Joystick"/>'s various balls
    /// </summary>
    public IReadOnlyCollection<Point> Balls { get; }

    /// <summary>
    /// Provides a method of querying a <see cref="Joystick"/>'s various hats
    /// </summary>
    public IReadOnlyCollection<JoystickHAT> Hats { get; }

    /// <summary>
    /// Gets the current status of power of this <see cref="Joystick"/>
    /// </summary>
    public JoystickPowerLevel CurrentPowerLevel => (JoystickPowerLevel)SDL_JoystickCurrentPowerLevel(_handle);

    /// <summary>
    /// Attempts to get the Joystick represented by the given instanceId
    /// </summary>
    public static bool TryGetJoystick(int instanceId, [NotNullWhen(true)] out Joystick? joystick)
    {
        if (JoystickDict.TryGetValue(instanceId, out var wr))
            if (wr.TryGetTarget(out joystick) && joystick.disposedValue is false)
                return true;
            else
                JoystickDict.Remove(instanceId, out _);

        joystick = null;
        return false;
    } 

    /// <summary>
    /// Attempts to open and instantiate a new Joystick, or return an old one if already instantiated
    /// </summary>
    /// <remarks>This method automatically checks for whether the Joystick is Virtual or a Joystick and uses the appropriate method.</remarks>
    /// <param name="index">The device index of the Joystick</param>
    /// <param name="joystick">The resulting Joystick</param>
    /// <returns></returns>
    public static bool TryOpen(int index, [NotNullWhen(true)] out Joystick? joystick)
    {
        Lock();
        try
        {
            var id = SDL_JoystickGetDeviceInstanceID(index);
            if (JoystickDict.TryGetValue(id, out var wr))
                if (wr.TryGetTarget(out joystick) && joystick.disposedValue is false) 
                    return true;
                else
                    JoystickDict.Remove(id, out _);

            if (SDL_JoystickIsVirtual(index) is SDL_bool.SDL_TRUE)
            {
                var r = new VirtualJoystick(SDL_JoystickOpen(index), index);
                joystick = r;
                return true;
            }

            if (SDL_IsGameController(index) is SDL_bool.SDL_TRUE)
            {
                var r = GameController.TryOpen(index, out var gc);
                joystick = gc;
                return r;
            }

            var f = SDL_JoystickFromInstanceID(id);
            var p = f != IntPtr.Zero ? f : SDL_JoystickOpen(index);
            if (p != IntPtr.Zero)
            {
                joystick = null;
                return false;
            }

            joystick = new(p, index);
            return true;
        }
        finally
        {
            Unlock();
        }
    }

    /// <summary>
    /// Checks whether the indexed <see cref="Joystick"/> is a <see cref="VirtualJoystick"/>
    /// </summary>
    /// <param name="index">The device index of the joystick</param>
    public static bool IsVirtual(int index) => SDL_JoystickIsVirtual(index) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Checks whether the indexed <see cref="Joystick"/> is a <see cref="GameController"/>
    /// </summary>
    /// <param name="index">The device index of the joystick</param>
    public static bool IsGameController(int index) => SDL_IsGameController(index) == SDL_bool.SDL_TRUE;

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
        => SDLJoystickException.ThrowIfLessThan(SDL_JoystickRumble(_handle, lowFreqRumble, highFreqRumble, durationMs), 0, "Rumble isn't supported on this Joystick");

    /// <summary>
    /// Start a rumble effect on a <see cref="Joystick"/>
    /// </summary>
    /// <param name="lowFreqRumble">The frequency of the low frequency motor</param>
    /// <param name="highFreqRumble">The frequency of the high frequency motor</param>
    /// <param name="duration">The duration of the rumble</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling it with 0 intensity stops any rumbling.
    /// </remarks>
    public void Rumble(ushort lowFreqRumble, ushort highFreqRumble, TimeSpan duration)
        => SDLJoystickException.ThrowIfLessThan(SDL_JoystickRumble(_handle, lowFreqRumble, highFreqRumble, (uint)duration.TotalMilliseconds), 0, "Rumble isn't supported on this Joystick");

    /// <summary>
    /// Whether or not this <see cref="Joystick"/> supports rumble
    /// </summary>
    public bool IsRumbleSupported => SDL_JoystickHasRumble(_handle) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Start a rumble effect on a <see cref="Joystick"/>'s triggers
    /// </summary>
    /// <param name="lowFreqRumble">The frequency of the low frequency motor</param>
    /// <param name="highFreqRumble">The frequency of the high frequency motor</param>
    /// <param name="durationMs">The duration of the rumble in milliseconds</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling it with 0 intensity stops any rumbling. Note that this is rumbling of the triggers and not the game controller as a whole. This is currently only supported on Xbox One controllers. If you want the (more common) whole-controller rumble, use <see cref="Rumble(ushort, ushort, uint)"/> instead.
    /// </remarks>
    public void RumbleTriggers(ushort lowFreqRumble, ushort highFreqRumble, uint durationMs)
        => SDLJoystickException.ThrowIfLessThan(SDL_JoystickRumbleTriggers(_handle, lowFreqRumble, highFreqRumble, durationMs), 0, "Trigger Rumble isn't supported on this Joystick");

    /// <summary>
    /// Start a rumble effect on a <see cref="Joystick"/>'s triggers
    /// </summary>
    /// <param name="lowFreqRumble">The frequency of the low frequency motor</param>
    /// <param name="highFreqRumble">The frequency of the high frequency motor</param>
    /// <param name="duration">The duration of the rumble</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling it with 0 intensity stops any rumbling. Note that this is rumbling of the triggers and not the game controller as a whole. This is currently only supported on Xbox One controllers. If you want the (more common) whole-controller rumble, use <see cref="Rumble(ushort, ushort, TimeSpan)"/> instead.
    /// </remarks>
    public void RumbleTriggers(ushort lowFreqRumble, ushort highFreqRumble, TimeSpan duration)
        => SDLJoystickException.ThrowIfLessThan(SDL_JoystickRumbleTriggers(_handle, lowFreqRumble, highFreqRumble, (uint)duration.TotalMilliseconds), 0, "Trigger Rumble isn't supported on this Joystick");

    /// <summary>
    /// Gets or sets the current player index for this <see cref="Joystick"/>
    /// </summary>
    /// <remarks>
    /// This same <see cref="Joystick"/> can later be retrieved by calling <see cref="FromPlayerIndex(int)"/>
    /// </remarks>
    public int PlayerIndex
    {
        get => SDL_JoystickGetPlayerIndex(_handle);
        set
        {
            SDL_JoystickSetPlayerIndex(_handle, value);
            playerDict[value] = this;
        }
    }

    /// <summary>
    /// Whether Joystick Events are enabled or not
    /// </summary>
    public static bool EventsEnabled
    {
        get => SDL_JoystickEventState(SDL_QUERY) == 1;
        set => SDL_JoystickEventState(value ? SDL_ENABLE : SDL_IGNORE);
    }

    /// <summary>
    /// Manually pump <see cref="Joystick"/> updates if <see cref="EventsEnabled"/> is false
    /// </summary>
    public static void JoystickUpdate()
    {
        SDL_JoystickUpdate();
    }

    /// <summary>
    /// This method is NOT implemented and should NOT be used yet. Will be implemented in the future
    /// </summary>
    /// <param name="_"></param>
    /// <exception cref="NotImplementedException"></exception>
    [Obsolete("This method is NOT implemented and should NOT be used yet. Will be implemented in the future")]
    public static void FromPlayerIndex(int _) => throw new NotImplementedException();

    /// <summary>
    /// Whether or not this <see cref="Joystick"/> supports rumble on the controller's triggers
    /// </summary>
    public bool IsTriggerRumbleSupported => SDL_JoystickHasRumbleTriggers(_handle) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Gets the type that this <see cref="Joystick"/> belongs to
    /// </summary>
    public JoystickType Type => (JoystickType)SDL_JoystickGetType(_handle);

    /// <summary>
    /// The unique <see cref="Guid"/> of this <see cref="Joystick"/>
    /// </summary>
    public Guid Guid => SDL_JoystickGetGUID(_handle);

    /// <summary>
    /// Get the implementation-dependent name for an opened <see cref="Joystick"/>, or null if there is no name or the identifier passed is invalid.
    /// </summary>
    /// <remarks>
    /// This is the same name as obtained in <see cref="JoystickReport"/>, but it uses a <see cref="Joystick"/> instance instead of the (unstable) device index.
    /// </remarks>
    public string? Name => SDL_JoystickName(_handle);

    /// <summary>
    /// Whether or not this <see cref="Joystick"/> has an LED that can be accessed from SDL
    /// </summary>
    public bool HasLED => SDL_JoystickHasLED(_handle) == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Sets the color of this <see cref="Joystick"/>'s LED.
    /// </summary>
    /// <remarks>This method throws if the <see cref="Joystick"/> does not have an LED, see <see cref="HasLED"/></remarks>
    /// <param name="color"></param>
    public void SetLEDColor(RGBColor color)
        => SDLJoystickException.ThrowIfLessThan(SDL_JoystickSetLED(_handle, color.Red, color.Green, color.Blue), 0, "This Joystick does not have a valid LED");

    /// <summary>
    /// Gets the vendor code of this <see cref="Joystick"/>
    /// </summary>
    public ushort Vendor => SDL_JoystickGetVendor(_handle);

    /// <summary>
    /// Gets the product code of this <see cref="Joystick"/>
    /// </summary>
    public ushort Product => SDL_JoystickGetProduct(_handle);

    /// <summary>
    /// Gets the product version code of this <see cref="Joystick"/>
    /// </summary>
    public ushort ProductVersion => SDL_JoystickGetProductVersion(_handle);

    /// <summary>
    /// Gets the serial number of this <see cref="Joystick"/>
    /// </summary>
    public string Serial => _serial ??= SDL_JoystickGetSerial(_handle);
    private string? _serial;

    #region Events

    /// <summary>
    /// Fired when a Joystick Axis moves
    /// </summary>
    public event JoystickAxisEvent? AxisMotion;
    internal void TriggerJoyAxisMotion(SDL_JoyAxisEvent e)
    {
        AxisMotion?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), e.axis, e.axisValue);
    }

    /// <summary>
    /// Fired when a Joystick Ball moves
    /// </summary>
    public event JoystickBallEvent? BallMotion;
    internal void TriggerJoyBallMotion(SDL_JoyBallEvent e)
    {
        BallMotion?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), e.ball, e.xrel, e.yrel);
    }

    /// <summary>
    /// Fired when a Joystick Hat moves
    /// </summary>
    public event JoystickHatEvent? HatMotion;
    internal void TriggerJoyHatMotion(SDL_JoyHatEvent e)
    {
        HatMotion?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), e.hat, e.hatValue);
    }

    /// <summary>
    /// Fired when a Joystick button is pressed
    /// </summary>
    public event JoystickButtonEvent? ButtonPressed;
    internal void TriggerJoyButtonDown(SDL_JoyButtonEvent e)
    {
        ButtonPressed?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), e.button, e.state == SDL_PRESSED);
    }

    /// <summary>
    /// Fired when a Joystick button is released
    /// </summary>
    public event JoystickButtonEvent? ButtonReleased;
    internal void TriggerJoyButtonUp(SDL_JoyButtonEvent e)
    {
        ButtonReleased?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), e.button, e.state == SDL_PRESSED);
    }

    /// <summary>
    /// Fired when this Joystick is removed and is no longer valid. This event is fired before <see cref="DeviceRemoved"/>
    /// </summary>
    public event JoystickDeviceRemovedEvent? Removed;
    internal void TriggerRemoved(SDL_JoyDeviceEvent e)
    {
        Removed?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp));
    }

    /// <summary>
    /// Fired when a Joystick is added
    /// </summary>
    public static event JoystickDeviceEvent? DeviceAdded;
    internal static void TriggerJoyDeviceAdded(SDL_JoyDeviceEvent e)
    {
        DeviceAdded?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), e.which);
    }

    /// <summary>
    /// Fired when a Joystick is removed
    /// </summary>
    public static event JoystickDeviceEvent? DeviceRemoved;
    internal static void TriggerJoyDeviceRemoved(SDL_JoyDeviceEvent e)
    {
        DeviceRemoved?.Invoke(TimeSpan.FromMilliseconds(e.timestamp), e.which);
    }

    /// <summary>
    /// Represents an event relating to a Joystick being added or removed
    /// </summary>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="deviceId">The id of the device that was added or removed</param>
    public delegate void JoystickDeviceEvent(TimeSpan timestamp, int deviceId);

    /// <summary>
    /// Represents an event relating to a Joystick being added or removed
    /// </summary>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="joystick">The joystick that is the object of the event</param>
    public delegate void JoystickDeviceRemovedEvent(Joystick joystick, TimeSpan timestamp);

    /// <summary>
    /// Represents an event relating to a given Joystick's axis
    /// </summary>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="axis">The axis that is the object of the event</param>
    /// <param name="value">The value that changed in the axis</param>
    /// <param name="joystick">The joystick that is the object of the event</param>
    public delegate void JoystickAxisEvent(Joystick joystick, TimeSpan timestamp, byte axis, short value);

    /// <summary>
    /// Represents an event relating to a given Joystick's ball
    /// </summary>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="ball">The ball that is the object of the event</param>
    /// <param name="relativeX">The relative x motion of the ball</param>
    /// <param name="relativeY">The relative y motion of the ball</param>
    /// <param name="joystick">The joystick that is the object of the event</param>
    public delegate void JoystickBallEvent(Joystick joystick, TimeSpan timestamp, byte ball, short relativeX, short relativeY);

    /// <summary>
    /// Represents an event relating to a given Joystick's hat
    /// </summary>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="axis">The hat that is the object of the event</param>
    /// <param name="value">The new position of the hat</param>
    /// <param name="joystick">The joystick that is the object of the event</param>
    public delegate void JoystickHatEvent(Joystick joystick, TimeSpan timestamp, byte axis, byte value);

    /// <summary>
    /// Represents an event relating to a given Joystick's axis
    /// </summary>
    /// <param name="timestamp">The amount of time elapsed from the initialization of SDL's library up until the firing of this event</param>
    /// <param name="button">The button that is the object of the event</param>
    /// <param name="state">The current state of the button</param>
    /// <param name="joystick">The joystick that is the object of the event</param>
    public delegate void JoystickButtonEvent(Joystick joystick, TimeSpan timestamp, byte button, bool state);

    #endregion

    #region IDisposable

    internal bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_JoystickClose(_handle);
            disposedValue = true;
        }
    }

    ~Joystick()
    {
        Dispose(disposing: false);
    }

    public virtual void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Joystick));
    }

    #endregion

    #region IJoystickDefinition

    internal bool virt;
    internal bool gc;

    bool IJoystickDefinition.IsVirtual(out VirtualJoystick? virtualJoystick)
    {
        if (virt)
        {
            virtualJoystick = (VirtualJoystick)this;
            return true;
        }
        virtualJoystick = null;
        return false;
    }

    bool IJoystickDefinition.IsGameController(out GameController? gameController)
    {
        if (gc)
        {
            gameController = (GameController)this;
            return true;
        }
        gameController = null;
        return false;
    }

    bool IJoystickDefinition.IsVirtual() => virt;

    bool IJoystickDefinition.IsGameController() => gc;

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
        public ControllerFingerReport this[int touchpad, int finger]
        {
            get
            {
                var r = SDL_GameControllerGetTouchpadFinger(handle, touchpad, finger, out var state, out var x, out var y, out var pressure);
                SDLJoystickException.ThrowIfLessThan(r, 0);
                return new ControllerFingerReport(state, x, y, pressure);
            }
        }
    }

    public class AxisStateCollection : IReadOnlyList<short>
    {
        private readonly IntPtr _handle;

        /// <summary>
        /// Queries a <see cref="Joystick"/>'s Axe for the current state of the axis at index <paramref name="index"/>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public short this[int index] => index < 0 || index >= (_c ??= Count)
                    ? throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 and {_c ??= Count}")
                    : SDL_JoystickGetAxis(_handle, index);

        /// <summary>
        /// Queries whether the given axis has an initial state
        /// </summary>
        /// <param name="index">The index of the axis</param>
        /// <param name="initial">The initial state of the axis, if it has one</param>
        /// <returns>Whether the axis hsa an initial state or not</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public bool HasInitialState(int index, [NotNullWhen(true)] out ushort initial) => index < 0 || index >= (_c ??= Count)
                    ? throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 and {_c ??= Count}")
                    : SDL_JoystickGetAxisInitialState(_handle, index, out initial) == SDL_bool.SDL_TRUE;

        private int? _c;

        /// <summary>
        /// The amount of axe the current <see cref="Joystick"/> has
        /// </summary>
        public int Count
        {
            get
            {
                var r = SDL_JoystickNumAxes(_handle);
                SDLDisplayException.ThrowIfLessThan(r, 0);
                return r;
            }
        }

        public IEnumerator<short> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal AxisStateCollection(IntPtr handle)
        {
            _handle = handle;
        }
    }

    private class ButtonStateCollection : IReadOnlyList<bool>
    {
        private readonly IntPtr _handle;

        public bool this[int index] => index < 0 || index >= (_c ??= Count)
                    ? throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 and {_c ??= Count}")
                    : SDL_JoystickGetButton(_handle, index) == 1;

        private int? _c;
        public int Count
        {
            get
            {
                var r = SDL_JoystickNumButtons(_handle);
                SDLDisplayException.ThrowIfLessThan(r, 0);
                return r;
            }
        }

        public IEnumerator<bool> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal ButtonStateCollection(IntPtr handle)
        {
            _handle = handle;
        }
    }

    private class HatStateCollection : IReadOnlyList<JoystickHAT>
    {
        private readonly IntPtr _handle;

        public JoystickHAT this[int index] => index < 0 || index >= (_c ??= Count)
                    ? throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 and {_c ??= Count}")
                    : (JoystickHAT)SDL_JoystickGetHat(_handle, index);

        private int? _c;
        public int Count
        {
            get
            {
                var r = SDL_JoystickNumHats(_handle);
                SDLDisplayException.ThrowIfLessThan(r, 0);
                return r;
            }
        }

        public IEnumerator<JoystickHAT> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal HatStateCollection(IntPtr handle)
        {
            _handle = handle;
        }
    }

    private class BallStateCollection : IReadOnlyList<Point>
    {
        private readonly IntPtr _handle;

        public Point this[int index]
        {
            get
            {
                if (index < 0 || index >= (_c ??= Count))
                    throw new IndexOutOfRangeException($"{nameof(index)} must be between 0 and {_c ??= Count}");

                SDLJoystickException.ThrowIfLessThan(SDL_JoystickGetBall(_handle, index, out var dx, out var dy), 0);
                return new(dx, dy);
            }
        }

        private int? _c;
        public int Count
        {
            get
            {
                var r = SDL_JoystickNumBalls(_handle);
                SDLDisplayException.ThrowIfLessThan(r, 0);
                return r;
            }
        }

        public IEnumerator<Point> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal BallStateCollection(IntPtr handle)
        {
            _handle = handle;
        }
    }

    #endregion
}
