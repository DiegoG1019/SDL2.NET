using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;
public static class Extensions
{
    public static unsafe ref SDL_Rect ToSDLRef(this ref Rectangle rect)
        => ref Unsafe.AsRef<SDL_Rect>(Unsafe.AsPointer(ref rect));

    public static unsafe ref SDL_FRect ToSDLRef(this ref FRectangle rect)
        => ref Unsafe.AsRef<SDL_FRect>(Unsafe.AsPointer(ref rect));

    public static unsafe ref SDL_Point ToSDLRef(this ref Point point)
        => ref Unsafe.AsRef<SDL_Point>(Unsafe.AsPointer(ref point));

    public static unsafe ref SDL_FPoint ToSDLRef(this ref FPoint point)
        => ref Unsafe.AsRef<SDL_FPoint>(Unsafe.AsPointer(ref point));

    public static unsafe ref SDL_Vertex ToSDLRef(this ref Vertex point)
        => ref Unsafe.AsRef<SDL_Vertex>(Unsafe.AsPointer(ref point));

    public static unsafe Span<SDL_Rect> ToSDL(this Span<Rectangle> rects)
        => MemoryMarshal.Cast<Rectangle, SDL_Rect>(rects);

    public static unsafe Span<SDL_FRect> ToSDL(this Span<FRectangle> rects)
        => MemoryMarshal.Cast<FRectangle, SDL_FRect>(rects);

    public static unsafe Span<SDL_Point> ToSDL(this Span<Point> points)
        => MemoryMarshal.Cast<Point, SDL_Point>(points);

    public static unsafe Span<SDL_FPoint> ToSDL(this Span<FPoint> points)
        => MemoryMarshal.Cast<FPoint, SDL_FPoint>(points);

    public static unsafe Span<SDL_Vertex> ToSDL(this Span<Vertex> points)
        => MemoryMarshal.Cast<Vertex, SDL_Vertex>(points);

    public static unsafe ReadOnlySpan<SDL_Rect> ToSDL(this ReadOnlySpan<Rectangle> rects)
        => MemoryMarshal.Cast<Rectangle, SDL_Rect>(rects);

    public static unsafe ReadOnlySpan<SDL_FRect> ToSDL(this ReadOnlySpan<FRectangle> rects)
        => MemoryMarshal.Cast<FRectangle, SDL_FRect>(rects);

    public static unsafe ReadOnlySpan<SDL_Point> ToSDL(this ReadOnlySpan<Point> points)
        => MemoryMarshal.Cast<Point, SDL_Point>(points);

    public static unsafe ReadOnlySpan<SDL_FPoint> ToSDL(this ReadOnlySpan<FPoint> points)
        => MemoryMarshal.Cast<FPoint, SDL_FPoint>(points);

    public static unsafe ReadOnlySpan<SDL_Vertex> ToSDL(this ReadOnlySpan<Vertex> points)
        => MemoryMarshal.Cast<Vertex, SDL_Vertex>(points);
}
