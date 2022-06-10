using SDL2.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Tests;

[SDLTestRepository("Clipboard Tests")]
internal class ClipboardTests
{
    [SDLTestMethod("Display clipboard text")]
    public static void DisplayText(RunControl _)
    {
        var app = SDLApplication.Instance().InitializeVideo();

        var buttons = new MessageBoxButton[2]
        {
            new MessageBoxButton("Update")
            {
                Flags = MessageBoxButtonFlags.ReturnKeyDefault
            },
            new MessageBoxButton("Exit")
            {
                Flags = MessageBoxButtonFlags.EscapeKeyDefault
            }
        };

        while (_.IsRunning)
        {
            var text = Clipboard.ClipboardText;
            var pressed = app.ShowMessageBox("ClipboardText", text ?? "!No clipboard text", MessageBoxFlags.Information, buttons);
            if (pressed?.Text is "Exit" or null)
            {
                _.IsRunning = false;
                break;
            }
        }
    }
}
