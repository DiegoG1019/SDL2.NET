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

    public static unsafe ref Rectangle ToDotNETRef(this ref SDL_Rect rect)
        => ref Unsafe.AsRef<Rectangle>(Unsafe.AsPointer(ref rect));

    public static unsafe ref FRectangle ToDotNETRef(this ref SDL_FRect rect)
        => ref Unsafe.AsRef<FRectangle>(Unsafe.AsPointer(ref rect));

    public static unsafe ref Point ToDotNETRef(this ref SDL_Point point)
        => ref Unsafe.AsRef<Point>(Unsafe.AsPointer(ref point));

    public static unsafe ref FPoint ToDotNETRef(this ref SDL_FPoint point)
        => ref Unsafe.AsRef<FPoint>(Unsafe.AsPointer(ref point));

    public static unsafe ref Vertex ToDotNETRef(this ref SDL_Vertex point)
        => ref Unsafe.AsRef<Vertex>(Unsafe.AsPointer(ref point));

    public static unsafe Span<Rectangle> ToDotNET(this Span<SDL_Rect> rects)
        => MemoryMarshal.Cast<SDL_Rect, Rectangle>(rects);

    public static unsafe Span<FRectangle> ToDotNET(this Span<SDL_FRect> rects)
        => MemoryMarshal.Cast<SDL_FRect, FRectangle>(rects);

    public static unsafe Span<Point> ToDotNET(this Span<SDL_Point> points)
        => MemoryMarshal.Cast<SDL_Point, Point>(points);

    public static unsafe Span<FPoint> ToDotNET(this Span<SDL_FPoint> points)
        => MemoryMarshal.Cast<SDL_FPoint, FPoint>(points);

    public static unsafe Span<Vertex> ToDotNET(this Span<SDL_Vertex> points)
        => MemoryMarshal.Cast<SDL_Vertex, Vertex>(points);

    public static unsafe ReadOnlySpan<Rectangle> ToDotNET(this ReadOnlySpan<SDL_Rect> rects)
        => MemoryMarshal.Cast<SDL_Rect, Rectangle>(rects);

    public static unsafe ReadOnlySpan<FRectangle> ToDotNET(this ReadOnlySpan<SDL_FRect> rects)
        => MemoryMarshal.Cast<SDL_FRect, FRectangle>(rects);

    public static unsafe ReadOnlySpan<Point> ToDotNET(this ReadOnlySpan<SDL_Point> points)
        => MemoryMarshal.Cast<SDL_Point, Point>(points);

    public static unsafe ReadOnlySpan<FPoint> ToDotNET(this ReadOnlySpan<SDL_FPoint> points)
        => MemoryMarshal.Cast<SDL_FPoint, FPoint>(points);

    public static unsafe ReadOnlySpan<Vertex> ToDotNET(this ReadOnlySpan<SDL_Vertex> points)
        => MemoryMarshal.Cast<SDL_Vertex, Vertex>(points);
}
