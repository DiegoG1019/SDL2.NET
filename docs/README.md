*Documentation site on the way

# Getting started with SDL2.NET

For an example on how to use this library, please refer to [SDL2.NET Tests](https://github.com/DiegoG1019/SDL2.NET/tree/main/SDL2.NET%20Tests)

The very first thing you need to do when using this library, is **initialize** the SDL libraries you wish to use, and, preferably, Launch your main Window and listen to OS events as follows:

```C#
using SDL2.NET;
using SDL2.NET.SDLMixer;

namespace MyApp;

internal class Program
{
    static void Main(string[] args)
    {
        var app = SDLApplication.Instance()
                                .InitializeVideo()
                                .InitializeTTF()
                                .InitializeAndOpenAudioMixer(MixerInitFlags.MP3)
                                .InitializeAudio()
                                .LaunchWindow("My SDL2.NET App", 800, 600);

        while (true)
        {
            // React to Input
            app.UpdateEvents();
        } 
    }
}
```

Afterwards, you may consider setting up a drawing and object update loop or whatever you may please. You can load Textures from images from the `Image` static class in the`SDL2.NET.SDLImage` namespace.

You can Render onto the window via the methods found in `SDLApplication.Instance().MainRenderer`, and much more! Feel free to explore the library, it's all documented!

## Overriding SDLApplication methods
You may have noticed that SDLApplication is an `unsealed` `singleton` class, which is highly unusual. The reason for this is that `SDL`'s library is largely of a single instance, and while a `static class` would have served well, there are methods that may be overriden, and other advantages that come from using an instanceable class.

In order to make use of `Object Oriented Programming`'s arguably most useful feature, `method overriding`, you can simply inherit from `SDLApplication` and override the methods you wish to override. Then, call `SDLApplication.Instance<MyApp>()` at least once, and you're all set! The rest of the app will use your modified class, even those that don't (need to) know it's a sub-type!

<sub>*Warning: SDLApplication can only be instanced **once**, and while it can technically be done outside of `SDLApplication.Instance`, use of that method down the line will result in an Exception being thrown, and is best avoided. Simply define your type and call `SDLApplication.Instance<MyApp>()` at least once for best results.</sub>

<sub>Calling `SDLApplication.Instance()` before calling it with your type will result in an `InvalidCastException` being thrown. The same will happen if a different type is attempted later.</sub>