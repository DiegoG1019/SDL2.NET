using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDL2.NET.Extensions.Windows.Exceptions;

[Serializable]
public class SDLExtensionWin32Exception : Exception
{
    public SDLExtensionWin32Exception() { }
    public SDLExtensionWin32Exception(string message) : base(message) { }
    public SDLExtensionWin32Exception(string message, Exception inner) : base(message, inner) { }
    protected SDLExtensionWin32Exception(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
