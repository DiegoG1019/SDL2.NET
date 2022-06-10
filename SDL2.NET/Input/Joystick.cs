using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Represents a Joystick
/// </summary>
public class Joystick : IJoystickDefinition, IDisposable
{
    internal readonly IntPtr _handle;
    internal static readonly ConcurrentDictionary<int, WeakReference<Joystick>> JoystickDict = new(4, 10);

    /// <summary>
    /// The unique identification for this <see cref="Joystick"/>. Not the same as deviceIndex, as this one is more stable
    /// </summary>
    public int Id { get; }

    internal Joystick(IntPtr handle, int deviceIndex)
    {
        Id = SDL_JoystickGetDeviceInstanceID(deviceIndex);
        _handle = handle;
        JoystickDict[Id] = new(this);
    }

    /// <summary>
    /// Attempts to open and instantiate a new GameController, or return an old one if already instantiated
    /// </summary>
    /// <remarks>This method automatically checks for whether the Joystick is Virtual or a GameController and uses the appropriate method.</remarks>
    /// <param name="index">The device index of the Joystick</param>
    /// <param name="joystick">The resulting Joystick</param>
    /// <returns></returns>
    public static bool TryOpen(int index, [NotNullWhen(true)] out Joystick? joystick)
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
            gameController = (GameController)this;
        gameController = null;
        return false;
    }

    bool IJoystickDefinition.IsVirtual() => virt;

    bool IJoystickDefinition.IsGameController() => gc;

    #endregion
}
