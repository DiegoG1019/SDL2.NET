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