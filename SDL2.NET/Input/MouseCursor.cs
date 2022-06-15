using SDL2.NET.Exceptions;
using System;
using System.Collections.Concurrent;
using static SDL2.Bindings.SDL;

namespace SDL2.NET.Input;

/// <summary>
/// Represents a mouse cursor
/// </summary>
public sealed class MouseCursor : IDisposable, IHandle
{
    internal readonly IntPtr _handle;

    IntPtr IHandle.Handle => _handle;

    internal MouseCursor(IntPtr handle)
    {
        _handle = handle;
        if (_handle == IntPtr.Zero)
            throw new SDLMouseCursorCreationException(SDL_GetAndClearError());
    }

#warning SDL_CreateCursor is missing

    /// <summary>
    /// Creates new cursor based off of a Surface
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="hotspot"></param>
    public MouseCursor(Surface surface, Point hotspot)
        : this(SDL_CreateColorCursor(surface._handle, hotspot.X, hotspot.Y)) { }

    #region System Cursor

    static MouseCursor? arrow;
    static MouseCursor? ibeam;
    static MouseCursor? wait;
    static MouseCursor? crosshair;
    static MouseCursor? waitarrow;
    static MouseCursor? sizeupleft;
    static MouseCursor? sizeupright;
    static MouseCursor? sizeleftright;
    static MouseCursor? sizeupdown;
    static MouseCursor? sizeall;
    static MouseCursor? no;
    static MouseCursor? hand;

    /// <summary>
    /// Obtains a system cursor
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static MouseCursor GetSystemCursor(SystemCursor cursor)
        => cursor switch
        {
            SystemCursor.Arrow => arrow is null or { disposedValue: true } ? arrow = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.Arrow)) : arrow,
            SystemCursor.IBeam => ibeam is null or { disposedValue: true } ? ibeam = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.IBeam)) : ibeam,
            SystemCursor.Wait => wait is null or { disposedValue: true } ? wait = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.Wait)) : wait,
            SystemCursor.Crosshair => crosshair is null or { disposedValue: true } ? crosshair = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.Crosshair)) : crosshair,
            SystemCursor.WaitArrow => waitarrow is null or { disposedValue: true } ? waitarrow = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.WaitArrow)) : waitarrow,
            SystemCursor.SizeUpLeft => sizeupleft is null or { disposedValue: true } ? sizeupleft = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.SizeUpLeft)) : sizeupleft,
            SystemCursor.SizeUpRight => sizeupright is null or { disposedValue: true } ? sizeupright = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.SizeUpRight)) : sizeupright,
            SystemCursor.SizeLeftRight => sizeleftright is null or { disposedValue: true } ? sizeleftright = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.SizeLeftRight)) : sizeleftright,
            SystemCursor.SizeUpDown => sizeupdown is null or { disposedValue: true } ? sizeupdown = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.SizeUpDown)) : sizeupdown,
            SystemCursor.SizeAll => sizeall is null or { disposedValue: true } ? sizeall = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.SizeAll)) : sizeall,
            SystemCursor.No => no is null or { disposedValue: true } ? no = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.No)) : no,
            SystemCursor.Hand => hand is null or { disposedValue: true } ? hand = new(SDL_CreateSystemCursor((SDL_SystemCursor)SystemCursor.Hand)) : hand,
            _ => throw new ArgumentException("Unknown cursor type", nameof(cursor)),
        };

    #endregion

    #region IDisposable

    private bool disposedValue;

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_FreeCursor(_handle);
            disposedValue = true;
        }
    }

    /// <summary>
    /// Finalizes the object, in case it wasn't disposed
    /// </summary>
    ~MouseCursor()
    {
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes the object, freeing unmanaged SDL resources
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    internal void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(MouseCursor));
    }

    #endregion
}
