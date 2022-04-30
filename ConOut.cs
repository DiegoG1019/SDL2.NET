namespace SDL2Example;

// This is just a helper class for easy pretty console colors
static public class ConOut
{
    public static string ConLog = "";
    public static void Write(string Text = "", Nullable<ConsoleColor> TextColor = null)
    {
        ConLog += Text;
        if (TextColor == null)
        {
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = (ConsoleColor)TextColor;
        }
        Console.Write(Text);
    }

    public static void WriteLine(string Text = "", Nullable<ConsoleColor> TextColor = null)
    {
        ConLog += Text + Environment.NewLine;
        if (TextColor == null)
        {
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = (ConsoleColor)TextColor;
        }
        Console.WriteLine(Text);
    }
}