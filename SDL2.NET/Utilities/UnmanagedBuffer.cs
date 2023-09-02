using System.Runtime.InteropServices;

namespace SDL2.NET.Utilities;

/// <summary>
/// Represents an unmanaged block of memory
/// </summary>
/// <remarks>
/// This object locks on itself when disposing, avoid using this object as a thread synchronization lock
/// </remarks>
public sealed class UnmanagedBuffer : IDisposable
{
    private nint handle;

    /// <summary>
    /// Allocates <paramref name="bytes"/> amount of bytes in unmanaged memory.
    /// </summary>
    /// <remarks>
    /// This class will free the memory appropriately after it's disposed
    /// </remarks>
    /// <param name="coTaskMem">if <see langword="true"/> <see cref="Marshal.AllocCoTaskMem(int)"/> will be used instead of <see cref="Marshal.AllocHGlobal(int)"/></param>
    /// <param name="bytes">The amount of bytes to allocate</param>
    public UnmanagedBuffer(int bytes, bool coTaskMem = false)
    {
        handle = coTaskMem ? Marshal.AllocCoTaskMem(bytes) : Marshal.AllocHGlobal(bytes);
        CoTaskMem = coTaskMem;
        Length = bytes;
    }

    /// <summary>
    /// Whether or not this <see cref="UnmanagedBuffer"/> was created using <see cref="Marshal.AllocCoTaskMem(int)"/>
    /// </summary>
    public bool CoTaskMem { get; }

    /// <summary>
    /// The amount of bytes allocated for this buffer
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Resizes the block of memory allocated for this <see cref="UnmanagedBuffer"/>
    /// </summary>
    /// <remarks>
    /// Automatically updates its handle, and is still owned (and solely managed by) this object
    /// </remarks>
    /// <param name="cb">The new size of the allocated buffer</param>
    /// <exception cref="ObjectDisposedException"/>
    public void Realloc(int cb)
    {
        if (IsFreed) throw new ObjectDisposedException(nameof(UnmanagedBuffer));
        handle = CoTaskMem ? Marshal.ReAllocCoTaskMem(handle, cb) : Marshal.ReAllocHGlobal(handle, (IntPtr)cb);
        Length = cb;
    }

    /// <summary>
    /// Gets a <see cref="nint"/> pointer for the Array
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"/>
    public nint GetPointer()
        => IsFreed ? throw new ObjectDisposedException(nameof(UnmanagedBuffer)) : handle;

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
            lock (this)
                if (IsFreed is false)
                {
                    if (CoTaskMem)
                        Marshal.FreeCoTaskMem(handle);
                    else
                        Marshal.FreeHGlobal(handle);
                    IsFreed = true;
                }
    }

    /// <inheritdoc/>
    ~UnmanagedBuffer()
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
