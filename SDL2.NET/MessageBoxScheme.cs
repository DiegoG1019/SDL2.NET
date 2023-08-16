using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents a color scheme for a Message Box
/// </summary>
public sealed class MessageBoxColorScheme
{
    /// <summary>
    /// The background color of the Message box
    /// </summary>
    public RGBColor Background { get; set; }

    /// <summary>
    /// The text color of the Message box
    /// </summary>
    public RGBColor Text { get; set; }

    /// <summary>
    /// The color of a button's borders in the Message box
    /// </summary>
    public RGBColor ButtonBorder { get; set; }

    /// <summary>
    /// The color of a button's background in the Message box
    /// </summary>
    public RGBColor ButtonBackground { get; set; }

    /// <summary>
    /// The color of a selected button in the Message box
    /// </summary>
    public RGBColor ButtonSelected { get; set; }

    internal SDL_MessageBoxColorScheme ToSDL()
    {
        return new()
        {
            colors = new SDL_MessageBoxColor[]
            {
                new(){ r = Background.Red, b = Background.Blue, g = Background.Green  },
                new(){ r = Text.Red, b = Text.Blue, g = Text.Green  },
                new(){ r = ButtonBorder.Red, b = ButtonBorder.Blue, g = ButtonBorder.Green  },
                new(){ r = ButtonBackground.Red, b = ButtonBackground.Blue, g = ButtonBackground.Green  },
                new(){ r = ButtonSelected.Red, b = ButtonSelected.Blue, g = ButtonSelected.Green  },
            }
        };
    }
}
