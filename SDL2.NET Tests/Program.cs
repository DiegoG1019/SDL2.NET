using System.Text;
using System.Runtime.InteropServices;
using SDL2.Bindings;
using SDL2.NET.SDLMixer;
using SDL2.NET.SDLImage;
using Serilog;
using Serilog.Events;
using SDL2.NET.SDLFont;
using System.Reflection;
using System.Linq;

namespace SDL2.NET.Tests;

// HOW TO USE THIS PROJECT
// This is a quick and dirty way of having multiple tests at once: I did not put much though into this, and it's not intended to be the best way of doing things
// You may create a class, decorate it with `SDLTestRepositoryAttribute` and fill it with STATIC methods (whose ONLY parameter is an instance of `RunControl`)
// and fill it with methods that are, too, decorated with `SDLTestMethodAttribute` and the """"GUI"""" I set up here will do the rest, have fun
// Each test is completely and entirely responsible for which SDL SubSystems they use, but they should NOT dispose of the app object
// It's probably better if you just call QuitSubSystem

internal class Program
{
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(LogEventLevel.Verbose)
            .CreateLogger();

        var app = SDLAppBuilder.CreateDefaultInstance();

        var runControl = new RunControl();
        var runControlParam = new object[1] { runControl };

        MessageBoxButton[] tests;
        {
            var repos = new List<TestRepoDesc>();

            foreach (var tp in from type in Assembly.GetCallingAssembly().GetTypes()
                               let attr = type.GetCustomAttribute<SDLTestRepositoryAttribute>()
                               where attr is not null
                               select (type, attr))
            {
                var methods = new List<TestMethodDesc>();
                foreach (var m in from meth in tp.type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                  let attr = meth.GetCustomAttribute<SDLTestMethodAttribute>()
                                  where attr is not null
                                  let par = meth.GetParameters()
                                  where par.Length == 1 && par[0].ParameterType == typeof(RunControl)
                                  select (meth, attr))
                    methods.Add(new(m.attr.Name, m.meth));

                repos.Add(new TestRepoDesc(tp.attr.Name, methods.ToArray()));
            }
            tests = repos.ToArray();
        }

        while (true)
        {
            var pressed = SDLApplication.ShowMessageBox("SDL.NET Tests", "Pick a test group", MessageBoxFlags.Information, tests);
            if (pressed is QuitMessageBoxButton or null)
                break;

            if (pressed is TestRepoDesc tr)
            {
                var test = SDLApplication.ShowMessageBox(tr.Name, "Pick a test", MessageBoxFlags.Information, tr.Methods);
                if (test is not null)
                {
                    runControl.IsRunning = true;
                    ((TestMethodDesc)test).Method.Invoke(null, runControlParam);
                    runControl.IsRunning = false;
                }
                continue;
            }

            throw new InvalidOperationException($"Can not handle a button of type {pressed?.GetType()} being pressed");
        }
        app.Dispose();

        Console.WriteLine("SDL2.NET Tests is now finished, press any key to continue");
        Console.ReadKey();
    }
}

public class QuitMessageBoxButton : MessageBoxButton
{
    public QuitMessageBoxButton() : base("Quit") { }
}

public class TestRepoDesc : MessageBoxButton
{
    public string Name { get; }
    public TestMethodDesc[] Methods { get; }

    public TestRepoDesc(string name, TestMethodDesc[] methods) : base(name)
    {
        Name = name;
        Methods = methods;
    }
}

public class TestMethodDesc : MessageBoxButton
{
    public string Name { get; }
    public MethodInfo Method { get; }

    public TestMethodDesc(string name, MethodInfo method) : base(name)
    {
        Name = name;
        Method = method;
    }
}

public class RunControl
{
    public bool IsRunning { get; set; }
}