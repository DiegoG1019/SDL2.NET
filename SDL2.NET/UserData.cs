using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;
public abstract class UserData
{
    internal UserData(bool isStruct) => IsValueType = isStruct;

    /// <summary>
    /// Gets the <see cref="Type"/> of the held <see cref="Data"/>
    /// </summary>
    public abstract Type DataType { get; }

    /// <summary>
    /// Gets an abstracted (up-casted) form of the held data. This involves a boxing operationg if the held data is a <see cref="ValueType"/> (<see cref="struct"/> or <see cref="ValueTuple"/>). See <see cref="IsValueType"/> if you want to avoid boxing
    public abstract object? Value { get; }
    
    /// <summary>
    /// Whether the held data is a struct or not
    /// </summary>
    public bool IsValueType { get; }
}

public sealed class UserData<T> : UserData
{
    private T _data;

    /// <summary>
    /// Provides a <see cref="ref"/> to the held data; for instances where you'd want to modify the fields of a <see cref="ValueType"/>
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

    public UserData(T data) : base(typeof(T).IsValueType) => _data = data;
    public override object? Value => _data;
    public override Type DataType => typeof(T);
}
