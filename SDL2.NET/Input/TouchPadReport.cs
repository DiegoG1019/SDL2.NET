namespace SDL2.NET.Input;

public struct TouchPadReport
{
    public byte State { get; set; }
    public FPoint Position { get; set; }
    public float Pressure { get; set; }

    public TouchPadReport(byte state, FPoint position, float pressure)
    {
        State = state;
        Position = position;
        Pressure = pressure;
    }
}
