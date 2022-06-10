using SDL2.NET.Exceptions;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Provides an interface into the system's clipboard
/// </summary>
/// <remarks>
/// SDL's Video Subsystem must be initialized before this class can be used. This class only supports raw text, image data or file data are not supported, due to SDL's limitations
/// </remarks>
public static class Clipboard
{
    /// <summary>
    /// Represents an event fired by the system's clipboard
    /// </summary>
    public delegate void ClipboardEvent(string? newText);

    /// <summary>
    /// Fired when the system clipboard obtains a new value;
    /// </summary>
    public static event ClipboardEvent? ClipboardChanged;

    /// <summary>
    /// Access the current text the system clipboard has
    /// </summary>
    public static string? ClipboardText
    {
        get => SDLClipboardException.ThrowIfEmpty(SDL_HasClipboardText() == SDL_bool.SDL_TRUE ? SDL_GetClipboardText() : null);
        set => SDLClipboardException.ThrowIfLessThan(SDL_SetClipboardText(value ?? ""), 0);
    }

    internal static void TriggerClipboardChanged()
    {
        ClipboardChanged?.Invoke(ClipboardText);
    }
}
