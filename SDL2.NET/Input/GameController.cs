using SDL2.Bindings;
using SDL2.NET.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Represents a Game Controller
/// </summary>
public class GameController : Joystick, IDisposable
{
    internal readonly IntPtr gc_handle;

    internal GameController(IntPtr handle, int deviceIndex) : base(SDL_GameControllerGetJoystick(handle), deviceIndex)
    {
        gc_handle = handle;
        gc = true;
    }

    /// <summary>
    /// Attempts to open and instantiate a new GameController, or return an old one if already instantiated
    /// </summary>
    /// <param name="index">The same joystick index passed to <see cref="Joystick.TryOpen(int, out Joystick?)"/></param>
    /// <param name="gameController">The fetched GameController</param>
    /// <returns></returns>
    public static bool TryOpen(int index, [NotNullWhen(true)] out GameController? gameController)
    {
        {
            var id = SDL_JoystickGetDeviceInstanceID(index);
            if (JoystickDict.TryGetValue(id, out var wr))
                if (wr.TryGetTarget(out var joystick) && joystick.disposedValue is false)
                    return ((IJoystickDefinition)joystick).IsGameController(out gameController);
                else
                    JoystickDict.Remove(id, out _);
        }

        var p = SDL_GameControllerOpen(index);
        if (p != IntPtr.Zero)
        {
            gameController = null;
            return false;
        }
        gameController = new(p, index);
        return true;
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
    /// Get the game controller mapping string for a given <see cref="Joystick"/>'s <see cref="Guid"/>
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    /// <exception cref="SDLJoystickException"></exception>
    public static string GetMapping(Guid guid)
        => SDL_GameControllerMappingForGUID(guid) ?? throw new SDLJoystickException(SDL_GetAndClearError());

    /// <summary>
    /// Get the game controller mapping string for a given <see cref="Joystick"/>'s device index
    /// </summary>
    /// <param name="deviceIndex"></param>
    /// <returns></returns>
    /// <exception cref="SDLJoystickException"></exception>
    public static string GetMapping(int deviceIndex)
        => SDL_GameControllerMappingForDeviceIndex(deviceIndex) ?? throw new SDLJoystickException(SDL_GetAndClearError());

    /// <summary>
    /// Get the implementation-dependent name for an opened game controller, or null if there is no name or the identifier passed is invalid.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string GetName(int index)
        => SDL_GameControllerNameForIndex(index);

    /// <summary>
    /// Whether Game Controller Events are enabled or not
    /// </summary>
    public static bool ControllerEventsEnabled
    {
        get => SDL_GameControllerEventState(SDL_QUERY) == 1;
        set => SDL_GameControllerEventState(value ? SDL_ENABLE : SDL_IGNORE);
    }

    /// <summary>
    /// Manually pump game controller updates if <see cref="ControllerEventsEnabled"/> is false
    /// </summary>
    public static void Update()
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
    /// <returns></returns>
    public static GameControllerAxis GetAxis(string str)
        => (GameControllerAxis)SDL_GameControllerGetAxisFromString(str);

    /// <summary>
    /// Get the game controller mapping for this instance
    /// </summary>
    public string Mapping => SDL_GameControllerMapping(gc_handle) ?? throw new SDLJoystickException(SDL_GetAndClearError());

    /// <summary>
    /// Get the implementation-dependent name for an opened game controller, or null if there is no name or the identifier passed is invalid.
    /// </summary>
    /// <remarks>
    /// This is the same name as returned by <see cref="GetName(int)"/>, but it uses a <see cref="GameController"/> instance instead of the (unstable) device index.
    /// </remarks>
    public string? ControllerName => SDL_GameControllerName(gc_handle);

    /// <summary>
    /// Gets the vendor code of this <see cref="GameController"/>
    /// </summary>
    public ushort Vendor => SDL_GameControllerGetVendor(gc_handle);

    /// <summary>
    /// Gets the product code of this <see cref="GameController"/>
    /// </summary>
    public ushort Product => SDL_GameControllerGetProduct(gc_handle);

    /// <summary>
    /// Gets the product version code of this <see cref="GameController"/>
    /// </summary>
    public ushort ProductVersion => SDL_GameControllerGetProductVersion(gc_handle);

    /// <summary>
    /// Gets the serial number of this <see cref="GameController"/>
    /// </summary>
    public string Serial => _serial ??= SDL_GameControllerGetSerial(gc_handle);
    private string? _serial;

    #region IDisposable

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_GameControllerClose(gc_handle);
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

    private class IndexableAxisState
    {
        private readonly IntPtr _handle;
        internal IndexableAxisState(IntPtr ptr) => _handle = ptr;

        public ushort this[GameControllerAxis axis]
        {
            get
            {
                var x = SDL_GameControllerGetAxis(_handle, (SDL_GameControllerAxis)axis);
                if (x == 0 && INTERNAL_SDL_GetError() != IntPtr.Zero)
                    throw new SDLJoystickException(SDL_GetAndClearError());
            }
        }
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
