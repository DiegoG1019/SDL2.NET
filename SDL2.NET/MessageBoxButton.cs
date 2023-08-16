using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents a button in a message box
/// </summary>
/// <remarks>Feel free to inherit this class to add custom tagging data and even custom methods if you wish. The button id is discarded from this class, and is instead set by its position in the array internally</remarks>
public class MessageBoxButton
{
    /// <summary>
    /// Creates a new instance of <see cref="MessageBoxButton"/>
    /// </summary>
    /// <param name="text"></param>
    public MessageBoxButton(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        this.text = text;
    }

    private string text;

    /// <summary>
    /// The text describing this button
    /// </summary>
    public string Text
    {
        get => text;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            text = value;
        }
    }

    /// <summary>
    /// The flags to be applied to this button
    /// </summary>
    public MessageBoxButtonFlags Flags { get; set; }

    internal SDL_MessageBoxButtonData ToSDL(int id)
    {
        return new()
        {
            text = Text,
            buttonid = id,
            flags = (SDL_MessageBoxButtonFlags)Flags
        };
    }
}
