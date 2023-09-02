using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Utilities;

/// <summary>
/// Represents an Array that has been pinned in memory from the GC, making its pointer always valid
/// </summary>
public sealed class PinnedArray<T> : IDisposable
{
    private readonly GCHandle GCHandle;

    /// <summary>
    /// Pins <paramref name="array"/> in memory and maintains the respective <see cref="GCHandle"/> in this object until it's disposed or finalized
    /// </summary>
    /// <param name="array"></param>
    public PinnedArray(T[] array)
    {
        Array = array;
        GCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
    }

    /// <summary>
    /// The Array that has been pinned by this <see cref="PinnedArray{T}"/>
    /// </summary>
    public T[] Array { get; }

    /// <summary>
    /// Gets a <see cref="nint"/> pointer for the Array
    /// </summary>
    /// <returns></returns>
    public nint GetPointer() 
        => IsFreed ? throw new ObjectDisposedException(nameof(PinnedArray<T>)) : GCHandle.AddrOfPinnedObject();

    /// <summary>
    /// Whether this <see cref="PinnedArray{T}"/> has already been freed
    /// </summary>
    /// <remarks>
    /// If freed, this <see cref="PinnedArray{T}"/> is no longer valid and should be dismissed from any variables or properties
    /// </remarks>
    public bool IsFreed { get; private set; }

    private void Dispose(bool disposing)
    {
        if (IsFreed is false)
            lock (Array)
                if (IsFreed is false)
                {
                    GCHandle.Free();
                    IsFreed = true;
                }
    }

    /// <inheritdoc/>
    ~PinnedArray()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
