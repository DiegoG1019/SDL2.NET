using System.Runtime.InteropServices;
using static SDL2.Bindings.SDL_image;

namespace SDL2.NET.SDLImage;

public class SDLAnimation : IDisposable, IHandle
{
    IntPtr IHandle.Handle => _handle;
    private readonly SDLAnimationFrame[] _frames;
    internal readonly IntPtr _handle;

    internal SDLAnimation(IntPtr handle, int frameCount)
    {
        var anim = Marshal.PtrToStructure<IMG_Animation>(handle);
        Size = new(anim.w, anim.h);
        Frames = frameCount;

        IntPtr[] frames = new IntPtr[frameCount];
        int[] delays = new int[frameCount];

        Marshal.Copy(anim.frames, frames, 0, frameCount);
        Marshal.Copy(anim.delays, delays, 0, frameCount);

        _frames = new SDLAnimationFrame[frameCount];
        for (int i = 0; i < frameCount; i++)
            _frames[i] = new(Surface.FetchOrNew(frames[i]), delays[i]);
    }

    public SDLAnimationFrame this[int index] 
        => _frames[index];

    public int Frames { get; }
    public Size Size { get; }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            IMG_FreeAnimation(_handle);
            disposedValue = true;
        }
    }

    ~SDLAnimation()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(SDLAnimation));
    }

    #endregion
}
