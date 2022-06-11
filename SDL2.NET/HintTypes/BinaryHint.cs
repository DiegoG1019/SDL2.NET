using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

public class BinaryHint : Hint
{
    internal BinaryHint(string name, bool @default) : base(name)
    {
        def = @default;
    }

    private readonly bool def;

    public void SetPriority(bool isEnabled, HintPriority priority) 
        => Set(isEnabled ? "1" : "0", priority);

    public bool IsEnabled
    {
        get => GetBool(def);
        set => Set(value ? "1" : "0");
    }
}
