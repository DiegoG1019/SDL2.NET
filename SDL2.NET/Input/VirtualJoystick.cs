using SDL2.NET.Exceptions;
using System.Diagnostics.CodeAnalysis;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Represents a Virtual (software) Joystick
/// </summary>
public class VirtualJoystick : Joystick
{
    internal VirtualJoystick(IntPtr handle, int index) : base(handle, index)
    {
        if (SDL_JoystickIsVirtual(index) == SDL_bool.SDL_FALSE)
            throw new SDLJoystickException(SDL_GetError());
        VirtualIndex = index;
    }

    /// <summary>
    /// Attaches a Virtual Joystick and obtains a new object representing it
    /// </summary>
    /// <param name="type">The type of joystick to attach</param>
    /// <param name="axes">The number of axes the joystick will have</param>
    /// <param name="buttons">The number of buttons the joystick will have</param>
    /// <param name="hats">The number of hats the joystick will have</param>
    /// <returns>The Device Index of the newly attached Joystick</returns>
    public static int Attach(JoystickType type, int axes, int buttons, int hats)
    {
        var index = SDL_JoystickAttachVirtual((int)type, axes, buttons, hats);
        SDLJoystickException.ThrowIfLessThan(index, 0);
        return index;
    }
    
    /// <summary>
    /// Attaches a Virtual Joystick and obtains a new object representing it
    /// </summary>
    /// <param name="type">The type of joystick to attach</param>
    /// <param name="axes">The number of axes the joystick will have</param>
    /// <param name="buttons">The number of buttons the joystick will have</param>
    /// <param name="hats">The number of hats the joystick will have</param>
    /// <param name="joystick">The newly instanced joystick</param>
    /// <returns>The Device Index of the newly attached Joystick</returns>
    public static int Attach(JoystickType type, int axes, int buttons, int hats, out VirtualJoystick joystick)
    {
        var i = Attach(type, axes, buttons, hats);
        joystick = new(SDL_JoystickOpen(i), i);
        return i;
    }

    /// <summary>
    /// Attempts to open and instantiate a new <see cref="VirtualJoystick"/>, or return an old one if already instantiated
    /// </summary>
    /// <param name="index">The same joystick index passed to <see cref="Joystick.TryOpen(int, out Joystick?)"/></param>
    /// <param name="joystick">The fetched <see cref="VirtualJoystick"/></param>
    /// <returns></returns>
    public static bool TryOpen(int index, [NotNullWhen(true)] out VirtualJoystick? joystick)
    {
        {
            var id = SDL_JoystickGetDeviceInstanceID(index);
            if (JoystickDict.TryGetValue(id, out var wr))
                if (wr.TryGetTarget(out var js) && js.disposedValue is false)
                    return ((IJoystickDefinition)js).IsVirtual(out joystick);
                else
                    JoystickDict.Remove(id, out _);
        }

        var p = SDL_JoystickOpen(index);
        if (p != IntPtr.Zero)
        {
            joystick = null;
            return false;
        }
        joystick = new(p, index);
        return true;
    }

    /// <summary>
    /// The Virtual Index that was given to this Virtual Joystick when attached
    /// </summary>
    public int VirtualIndex { get; }

    #region IDisposable

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDLJoystickException.ThrowIfLessThan(SDL_JoystickDetachVirtual(VirtualIndex), 0);
            disposedValue = true;
        }
    }

    ~VirtualJoystick()
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
            throw new ObjectDisposedException(nameof(VirtualJoystick));
    }

    #endregion

}