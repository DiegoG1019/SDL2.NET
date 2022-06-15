using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Tests;

[SDLTestRepository("Message Box tests")]
internal class MessageBoxTests
{
    [SDLTestMethod("Test Window Box")]
    public static void TestWindowBox(RunControl _)
    {
        var app = SDLDefaultApp.Instance.LaunchWindow("Test Window Box", 100, 100);
        app.MainWindow.ShowMessageBox("This is a test!", "Don't mind me!", MessageBoxFlags.Information);
        app.MainWindow.ShowMessageBox("This is a test!", "Don't mind me!", MessageBoxFlags.Warning);
        app.MainWindow.ShowMessageBox("This is a test!", "Don't mind me!", MessageBoxFlags.Error);
        app.MainWindow.Dispose();
    }

    [SDLTestMethod("Test Window Box Style")]
    public static void TestWindowBoxStyle(RunControl _)
    {
        var app = SDLDefaultApp.Instance;
        var buttons = new MessageBoxButton[3]
        {
            new("Button 1"),
            new("Button 2"),
            new("Button 3")
        };
        var style = new MessageBoxColorScheme()
        {
            Background = Colors.AliceBlue,
            ButtonBackground = Colors.AntiqueWhite,
            ButtonBorder = Colors.Gray,
            ButtonSelected = Colors.IndianRed,
            Text = Colors.LavenderBlush
        };

        SDLApplication.ShowMessageBox("This is a test!", "Don't mind me!", MessageBoxFlags.Information, buttons, style);
        SDLApplication.ShowMessageBox("This is a test!", "Don't mind me!", MessageBoxFlags.Warning, buttons, style);
        SDLApplication.ShowMessageBox("This is a test!", "Don't mind me!", MessageBoxFlags.Error, buttons, style);
    }
}
