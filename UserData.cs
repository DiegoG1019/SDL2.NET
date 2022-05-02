using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;
public abstract class UserData
{
    internal UserData() { }
    public abstract Type GetDataType();
    public abstract bool Match(Type type);
    public abstract bool Match<TMatch>();
    public abstract bool Match(Type type, [MaybeNullWhen(false)] out object? data);
    public abstract bool Match<TMatch>([MaybeNullWhen(false)] out TMatch data);
}

public sealed class UserData<T> : UserData
{
    private T _data;
    public ref T RefData => ref _data;
    public T Data
    {
        get => _data;
        set => _data = value;
    }

    public UserData(T data) => _data = data;

    public override Type GetDataType() => typeof(T);
    public override bool Match(Type type, [MaybeNullWhen(false)] out object? data)
    {
        if (typeof(T) == type)
        {
            data = _data;
            return true;
        }
        data = null;
        return false;
    }

    public override bool Match(Type type)
        => typeof(T) == type;

    public override bool Match<TMatch>()
        => _data is TMatch;

    public override bool Match<TMatch>([MaybeNullWhen(false)] out TMatch data)
    {
        if (_data is TMatch d)
        {
            data = d;
            return true;
        }
        data = default;
        return false;
    }
}
