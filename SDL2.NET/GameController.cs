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

namespace SDL2.NET;

/// <summary>
/// Represents a Game Controller
/// </summary>
public class GameController : Joystick, IDisposable
{
    internal GameController(IntPtr handle, int deviceIndex) : base(handle, deviceIndex)
    {
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
                if (wr.TryGetTarget(out var joystick))
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
    /// <exception cref="SDLGameControllerException"></exception>
    public static bool AddMapping(string mapping)
    {
        var r = SDL_GameControllerAddMapping(mapping);
        SDLGameControllerException.ThrowIfLessThan(r, 0);
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
    /// <exception cref="SDLGameControllerException"></exception>
    public static string GetMapping(Guid guid)
        => SDL_GameControllerMappingForGUID(guid) ?? throw new SDLGameControllerException(SDL_GetError());

    /// <summary>
    /// Get the implementation-dependent name for an opened game controller, or null if there is no name or the identifier passed is invalid.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string GetName(int index)
        => SDL_GameControllerNameForIndex(index);

    /// <summary>
    /// Get the game controller mapping for this instance
    /// </summary>
    public string Mapping => SDL_GameControllerMapping(_handle) ?? throw new SDLGameControllerException(SDL_GetError());

    /// <summary>
    /// Get the implementation-dependent name for an opened game controller, or null if there is no name or the identifier passed is invalid.
    /// </summary>
    /// <remarks>
    /// This is the same name as returned by <see cref="GetName(int)"/>, but it uses a <see cref="GameController"/> instance instead of the (unstable) device index.
    /// </remarks>
    public string? ControllerName => SDL_GameControllerName(_handle);

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_GameControllerClose(_handle);
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

    protected void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Renderer));
    }

    #endregion

    #region collection classes

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
