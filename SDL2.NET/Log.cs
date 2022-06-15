using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Provides access to SDL's logging facilities
/// </summary>
/// <remarks>
/// Due to the way that .NET's interoperability works with strings, most of the functions in SDL are not implemented here. Only The Log output callback for safety purposes. Use that only if you intend to use more than one language and intend to connect them /through/ SDL rather than directly with .NET. If you wish to use SDL's logging functions from .NET, feel free to call yourself. Find them in <see cref="Bindings.SDL"/> under the same name as defined in <c>SDL_log.h</c>
/// </remarks>
public static class SDLLog
{
    /// <summary>
    /// Represents a method that can be used by SDL to sink log messages
    /// </summary>
    /// <param name="userdata">The User Data attached to this delegate</param>
    /// <param name="category">The category of the log event</param>
    /// <param name="priority">The priority of the log event</param>
    /// <param name="message">The message describing the event</param>
    public delegate void LogSinkMethodDelegate(
            UserData? userdata,
            LogCategory category,
            LogPriority priority,
            string message
        );

    private static LogSinkMethodDelegate? outmethod;
    private readonly static SDL_LogOutputFunction outfunc = (u, c, p, m) =>
    {
        var meth = outmethod;
        if (meth is null)
            return;
        meth.Invoke(UserData, (LogCategory)c, (LogPriority)p, UTF8_ToManaged(m));
    };

    /// <summary>
    /// The <see cref="UserData"/> attached to the log sink method
    /// </summary>
    public static UserData? UserData { get; set; }

    /// <summary>
    /// Represents the method that is currently being used to output log events
    /// </summary>
    /// <remarks>As the actual method is static and internally held by this class, <see cref="SDL_LogGetOutputFunction(out SDL_LogOutputFunction, out IntPtr)"/> is invalid here, and the delegate is instead stored in .NET</remarks>
    public static LogSinkMethodDelegate? OutputMethod
    {
        get => outmethod;
        set
        {
            if (value == null && outmethod is not null)
            {
                SDL_LogSetOutputFunction(null, IntPtr.Zero);
                outmethod = null;
                return;
            }

            outmethod = value;
            SDL_LogSetOutputFunction(outfunc, IntPtr.Zero);
        }
    }
}
