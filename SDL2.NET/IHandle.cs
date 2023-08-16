namespace SDL2.NET;

/// <summary>
/// This interface allows access to the internal handle of each class. Using the handle directly may change the status of the object as seen from .NET or corrupt it completely. Use with care.
/// </summary>
public interface IHandle
{
    /// <summary>
    /// The handle that internally represents this class. Using the handle directly may change the status of the object as seen from .NET or corrupt it completely. Use with care.
    /// </summary>
    public IntPtr Handle { get; }
}
