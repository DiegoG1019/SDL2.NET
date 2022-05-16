﻿using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;
public static class Display
{
    public static void EnableScreenSaver()
        => SDL_EnableScreenSaver();
    public static void DisableScreenSaver()
        => SDL_DisableScreenSaver();

    public static SDL_DisplayMode GetCurrentDisplayMode(int displayIndex)
    {
        SDLDisplayException.ThrowIfLessThan(SDL_GetCurrentDisplayMode(displayIndex, out var mode), 0);
        return mode;
    }

    public static SDL_DisplayMode GetClosestDisplayMode(int displayIndex, SDL_DisplayMode mode)
    {
        SDL_GetClosestDisplayMode(displayIndex, ref mode, out var closest);
        return closest;
    }

    public static string GetCurrentVideoDriver()
        => SDL_GetCurrentVideoDriver();

    public static SDL_DisplayMode GetDesktopDisplayMode(int displayIndex)
    {
        SDLDisplayException.ThrowIfLessThan(SDL_GetDesktopDisplayMode(displayIndex, out var mode), 0);
        return mode;
    }

    public static string GetDisplayName(int displayIndex)
        => SDL_GetDisplayName(displayIndex);

    public static Rectangle GetDisplayBounds(int displayIndex)
    {
        SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayBounds(displayIndex, out var rect), 0);
        return rect;
    }

    public static void GetDisplayDPI(int displayIndex, out float dpi, out float hdpi, out float vdpi)
    {
        SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayDPI(displayIndex, out dpi, out hdpi, out vdpi), 0);
    }

    public static SDL_DisplayOrientation GetDisplayOrientation(int displayIndex)
        => SDL_GetDisplayOrientation(displayIndex);

    public static SDL_DisplayMode GetDisplayMode(int displayIndex, int modeIndex)
    {
        SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayMode(displayIndex, modeIndex, out var mode), 0);
        return mode;
    }

    public static Rectangle GetDisplayUsableBounds(int displayIndex)
    {
        SDLDisplayException.ThrowIfLessThan(SDL_GetDisplayUsableBounds(displayIndex, out var rect), 0);
        return rect;
    }

    public static int GetDisplayModeCount(int displayIndex)
        => SDL_GetNumDisplayModes(displayIndex);

    public static int GetVideoDisplayCount()
        => SDL_GetNumVideoDrivers();

    public static string GetVideoDriver(int displayIndex)
        => SDL_GetVideoDriver(displayIndex);
}
