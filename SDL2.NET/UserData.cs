namespace SDL2.NET;

/// <summary>
/// Represents User defined Data that can be attached to certain types or callbacks
/// </summary>
/// <remarks>This class is abstract and cannot be instanced. See <see cref="UserData{T}"/></remarks>
public abstract class UserData
{
    internal UserData(bool isStruct) => IsValueType = isStruct;

    /// <summary>
    /// Gets the <see cref="Type"/> of the held Data
    /// </summary>
    public abstract Type DataType { get; }

    /// <summary>
    /// Gets an abstracted (up-casted) form of the held data. This involves a boxing operation if the held data is a <see cref="ValueType"/> (struct or <see cref="ValueTuple"/>). See <see cref="IsValueType"/> if you want to avoid boxing
    /// </summary>
    public abstract object? Value { get; }

    /// <summary>
    /// Whether the held data is a struct or not
    /// </summary>
    public bool IsValueType { get; }
}

/// <summary>
/// Represents User defined Data of type <typeparamref name="T"/> that can be attached to certain types or callbacks
/// </summary>
/// <typeparam name="T">The type this UserData represents</typeparam>
public sealed class UserData<T> : UserData
{
    private T _data;

    /// <summary>
    /// Provides a <c>ref</c> to the held data; for instances where you'd want to modify the fields of a <see cref="ValueType"/>
    /// </summary>
    public ref T RefData => ref _data;

    /// <summary>
    /// The data held by this instance
    /// </summary>
    public T Data
    {
        get => _data;
        set => _data = value;
    }

    /// <summary>
    /// Creates a new UserData object with the given data in it
    /// </summary>
    /// <param name="data"></param>
    public UserData(T data) : base(typeof(T).IsValueType) => _data = data;

    /// <inheritdoc/>
    public override object? Value => _data;

    /// <inheritdoc/>
    public override Type DataType => typeof(T);
}
