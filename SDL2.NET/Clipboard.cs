using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;

/// <summary>
/// Provides an interface into the system's clipboard
/// </summary>
public static class Clipboard
{
    /// <summary>
    /// Represents an event fired by the system's clipboard
    /// </summary>
    public delegate void ClipboardEvent();

    /// <summary>
    /// Fired when the system clipboard obtains a new value;
    /// </summary>
    public static event ClipboardEvent? ClipboardChanged;

    internal static void TriggerClipboardChanged()
        => ClipboardChanged?.Invoke();
}
