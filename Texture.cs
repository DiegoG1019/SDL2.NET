using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace SDL2.NET;
/// <summary>
/// Represents an SDL Texture: a structure that contains an efficient, driver-specific representation of pixel data. <see cref="SDL_Texture" href="https://wiki.libsdl.org/SDL_Texture"/>
/// </summary>
public class Texture : IDisposable
{
    protected internal readonly IntPtr _handle;

    public Texture(Renderer renderer, PixelFormat format, TextureAccess access, int width, int height)
    {
        _handle = SDL_CreateTexture(renderer._handle, (uint)format, (int)access, width, height);
    }

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_DestroyTexture(_handle);
            disposedValue = true;
        }
    }

    ~Texture()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Texture));
    }

    #endregion

}

#error Not Implemented
