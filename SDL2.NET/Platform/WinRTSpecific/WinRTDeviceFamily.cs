using static SDL2.Bindings.SDL;

namespace SDL2.NET.Platform.WinRTSpecific;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public enum WinRTDeviceFamily
{
    Unknown = SDL_WinRT_DeviceFamily.SDL_WINRT_DEVICEFAMILY_UNKNOWN,
    Desktop = SDL_WinRT_DeviceFamily.SDL_WINRT_DEVICEFAMILY_DESKTOP,
    Mobile = SDL_WinRT_DeviceFamily.SDL_WINRT_DEVICEFAMILY_MOBILE,
    XBox = SDL_WinRT_DeviceFamily.SDL_WINRT_DEVICEFAMILY_XBOX
}