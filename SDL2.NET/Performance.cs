using static SDL2.Bindings.SDL;

namespace SDL2.NET;
public static class Performance
{
    public static ulong PerformanceCounter => SDL_GetPerformanceCounter();
    public static ulong PerformanceFrequency => SDL_GetPerformanceFrequency();
}
