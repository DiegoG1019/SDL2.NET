using static SDL2.Bindings.SDL;

namespace SDL2.NET.Platform.AndroidSpecific;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public enum AndroidExternalStorageState
{
    Unavailable = 0,
    Read = SDL_ANDROID_EXTERNAL_STORAGE_READ,
    Write = SDL_ANDROID_EXTERNAL_STORAGE_WRITE
}