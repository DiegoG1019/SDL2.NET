namespace SDL2.NET.HintTypes;

public class BinaryHint : Hint
{
    internal BinaryHint(string name) : base(name) { }

    public void SetPriority(bool isEnabled, HintPriority priority) 
        => Set(isEnabled ? "1" : "0", priority);

    public bool IsEnabled
    {
        get => Get() == "1";
        set => Set(value ? "1" : "0");
    }
}

//public delegate void HintCallback();
