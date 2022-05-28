using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Provides methods and properties to interact with the currently plugged mice
/// </summary>
public static class Keyboard
{
    /// <summary>
    /// A collection of indexable key states of the keyboard
    /// </summary>
    /// <remarks>
    /// Not an actual list in the regular sense; simply conforming to the interface and backed by interoping with SDL's functions
    /// </remarks>
    public static IReadOnlyList<KeyState> KeyStates { get; } = new KeyboardKeyStates();

    /// <summary>
    /// The current modifier state of the keyboard
    /// </summary>
    /// <remarks>
    /// Setting this does not change the keyboard state, only the key modifier flags that SDL reports.
    /// </remarks>
    public static KeyModifier ActiveModifiers
    {
        get => (KeyModifier)SDL_GetModState();
        set => SDL_SetModState((SDL_Keymod)value);
    }

    /// <summary>
    /// The focused <see cref="Window"/> will start accepting Unicode text input events and start firing <see cref="Window.TextInput"/> and <see cref="Window.TextEditing"/>. Please use this method in pair with <see cref="StopTextInput"/>.
    /// </summary>
    /// <remarks>While the focused <see cref="Window"/> will fire the events, this method is entirely independent of the current <see cref="Window"/>; and focusing another one will change which <see cref="Window"/> is firing the events. On some platforms, this method activates the screen keyboard</remarks>
    public static void StartTextInput() => SDL_StartTextInput();

    /// <summary>
    /// Stops receiving any text input events.
    /// </summary>
    /// <remarks>As this method, as well as <see cref="StartTextInput"/> is entirely independent of the focused <see cref="Window"/>, this will cease TextInput events from all <see cref="Window"/>s. On some platforms, this method deactivates the screen keyboard</remarks>
    public static void StopTextInput() => SDL_StopTextInput();

    /// <summary>
    /// Whether or not TextInput is currently active for this application
    /// </summary>
    public static bool IsTextInputActive => SDL_IsTextInputActive() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Set the rectangle used to type Unicode text inputs.
    /// </summary>
    /// <remarks>If you want use system native IME window, try to enable <see cref="Hints.IMEShowUI"/>, otherwise this method won't give you any feedback.</remarks>
    public static void SetTextInputArea(Rectangle? area)
    {
        if (area is Rectangle r)
        {
            r.ToSDL(out var rect);
            SDL_SetTextInputRect(ref rect);
        }
        else
            SDL_SetTextInputRect(IntPtr.Zero);
    }

    /// <summary>
    /// Whether or not the current platform has Screen keyboard support, such as Android
    /// </summary>
    public static bool IsScreenKeyboardSupported => SDL_HasScreenKeyboardSupport() == SDL_bool.SDL_TRUE;

    #region Scancode

    /// <summary>
    /// Obtains the matching <see cref="Scancode"/> from the specified <paramref name="code"/>
    /// </summary>
    /// <param name="code">The <see cref="Keycode"/> to match</param>
    /// <returns>The matched <see cref="Scancode"/></returns>
    public static Scancode GetScancodeFrom(Keycode code)
        => code.ToScancode();

    /// <summary>
    /// Gets a scancode from the specified name
    /// </summary>
    public static Scancode GetScancodeFrom(string name)
        => (Scancode)SDL_GetScancodeFromName(name);

    /// <summary>
    /// Gets a Keycode from the specified name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Keycode GetKeycodeFrom(string name)
        => (Keycode)SDL_GetKeyFromName(name);

    /// <summary>
    /// Gets a Keycode from the specified scancode
    /// </summary>
    public static Keycode GetKeycodeFrom(Scancode scancode)
        => scancode.ToKeycode();

    #endregion

    private sealed class KeyboardKeyStates : IReadOnlyList<KeyState>
    {
        internal KeyboardKeyStates() { }

        public KeyState this[int index]
        {
            get
            {
                var ptr = SDL_GetKeyboardState(out int numkeys);
                return index is < 0 || index >= numkeys
                    ? throw new IndexOutOfRangeException($"index must be between 0 and {numkeys}")
                    : new KeyState((Scancode)index, Marshal.ReadByte(ptr, index) == 1);
            }
        }

        public int Count
        {
            get
            {
                _ = SDL_GetKeyboardState(out int numkeys);
                return numkeys;
            }
        }

        public IEnumerator<KeyState> GetEnumerator()
        {
            var ptr = SDL_GetKeyboardState(out int numkeys);
            for (int i = 0; i < numkeys; i++)
                yield return new KeyState((Scancode)i, Marshal.ReadByte(ptr, i) == 1);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
