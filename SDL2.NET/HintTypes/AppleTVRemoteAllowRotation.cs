﻿using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies whether the Apple TV remote's joystick axes will automatically match the rotation of the remote. <see cref="SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION" href="https://wiki.libsdl.org/SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION"/>
/// </summary>
/// <remarks>
/// When UI events are generated by controller input, the app will be backgrounded when the Apple TV remote's menu button is pressed, and when the pause or B buttons on gamepads are pressed.
/// </remarks>
public sealed class AppleTVRemoteAllowRotation : BinaryHint
{
    internal AppleTVRemoteAllowRotation() : base(SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION) { }
}