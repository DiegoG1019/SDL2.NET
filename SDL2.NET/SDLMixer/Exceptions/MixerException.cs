using SDL2.NET.Exceptions;

namespace SDL2.NET.SDLMixer.Exceptions;

[Serializable]
public class MixerException : SDLException 
{
    public MixerException() { }
    public MixerException(string message) : base(message) { }
    public MixerException(string message, Exception inner) : base(message, inner) { }
    protected MixerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
