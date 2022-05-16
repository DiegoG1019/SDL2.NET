using static SDL2.Bindings.SDL;

namespace SDL2.NET.HintTypes;

/// <summary>
/// A hint that specifies a value to override the binding element for keyboard inputs for Emscripten builds.
/// </summary>
/// <remarks>
/// This hint only applies to the Emscripten platform.
/// </remarks>
public sealed class EmscriptenKeyboardElement : Hint
{
    internal EmscriptenKeyboardElement() : base(SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT) { }

    public bool SetPriority(EmscriptenBindingElement bindingElement, HintPriority priority)
        => Set(BindingElementToString(bindingElement), priority);

    public EmscriptenBindingElement BindingElement
    {
        get => StringToBindingElement(Get());
        set => Set(BindingElementToString(value));
    }

    private static string BindingElementToString(EmscriptenBindingElement el)
        => el switch
        {
            EmscriptenBindingElement.Window => "#window",
            EmscriptenBindingElement.Document => "#document",
            EmscriptenBindingElement.Screen => "#screen",
            EmscriptenBindingElement.Canvas => "#canvas",
            _ => throw new NotSupportedException()
        };

    private static EmscriptenBindingElement StringToBindingElement(string? el)
        => el switch
        {
            null or "#window" => EmscriptenBindingElement.Window,
            "#document" => EmscriptenBindingElement.Document,
            "#screen" => EmscriptenBindingElement.Screen,
            "#canvas" => EmscriptenBindingElement.Canvas,
            _ => throw new InvalidOperationException($"Unrecognized string '{el}' for binding element")
        };
}

public enum EmscriptenBindingElement
{
    /// <summary>
    /// The JavaScript window object (this is the default)
    /// </summary>
    Window,

    /// <summary>
    /// the JavaScript document object
    /// </summary>
    Document,

    /// <summary>	
    /// The JavaScript window.screen object
    /// </summary>
    Screen,

    /// <summary>
    /// The default WebGL canvas element
    /// </summary>
    Canvas
}