using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;
public class Surface : IDisposable
{
    protected internal readonly IntPtr _handle = IntPtr.Zero;

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL.SDL_FreeSurface(_handle);
            disposedValue = true;
        }
    }

    ~Surface()
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
            throw new ObjectDisposedException(nameof(Surface));
    }

    #endregion
}

#error Not Implemented