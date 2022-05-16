using System.Text;
using System.Runtime.InteropServices;
using SDL2.Bindings;

namespace SDL2.NET;

internal class Program
{
    static void Main(string[] args)
    {
        // avoid 0x406D1388 exception. See https://wiki.libsdl.org/SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING
        SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");

        SDLApplication.App
            .InitializeVideo()
            .InitializeAudio()
            .OpenAudioMixer()
            .InitializeTTF()
            .LaunchWindow("SDL2.NET", 800, 600);

        
        // spawn window
        Console.Write("Spawn window: ");
        IntPtr Window = IntPtr.Zero;
        Window = SDL.SDL_CreateWindow(
            "SDL2Example",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            800,
            600,
            SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
        );

        if (Window == IntPtr.Zero)
        {
            Console.WriteLine($"Error: {SDL.SDL_GetError()}", ConsoleColor.Red);
            return;
        }
        else
        {
            Console.WriteLine("Success", ConsoleColor.Green);
        }

        // set icon
        IntPtr ico = SDL_image.IMG_Load("Resources\\Icon.png");
        SDL.SDL_SetWindowIcon(Window, ico);
        SDL.SDL_FreeSurface(ico);

        // create renderer
        Console.Write("Creating renderer: ");
        IntPtr Renderer = IntPtr.Zero;
        bool vSync = true;
        Renderer = SDL.SDL_CreateRenderer(
            Window,
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | (vSync ? SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC : 0x0)
        );

        if (Renderer == IntPtr.Zero)
        {
            Console.WriteLine($"Error: {SDL.SDL_GetError()}", ConsoleColor.Red);
            return;
        }
        else
        {
            Console.WriteLine("Success", ConsoleColor.Green);
        }

        // log some renderer information
        SDL.SDL_RendererInfo rendererInfo;
        SDL.SDL_GetRendererInfo(Renderer, out rendererInfo);
        string? currentVideoDriver = Marshal.PtrToStringAnsi(rendererInfo.name);
        if (currentVideoDriver != null && currentVideoDriver != "")
        {
            Console.Write("Rendering using ");
            Console.Write(currentVideoDriver, ConsoleColor.Blue);
            Console.Write(" (");
            Console.Write(SDL.SDL_GetCurrentVideoDriver(), ConsoleColor.Blue);
            Console.WriteLine(")");
        }

        // load audio
        IntPtr Music = SDL_mixer.Mix_LoadMUS("Resources\\loop.wav");
        IntPtr Pew = SDL_mixer.Mix_LoadWAV("Resources\\laserpew.ogg");

        // play music
        SDL_mixer.Mix_VolumeMusic(64); // 0..128
        SDL_mixer.Mix_FadeInMusic(Music, -1, 200); // fadein period under 200ms won't work
        //SDL_mixer.Mix_PlayMusic(Music, -1); // for no fadein

        // common texture size rect(s) for easy reuse
        SDL.SDL_Rect TextureSize32 = new SDL.SDL_Rect() { w = 32, h = 32 };

        // load texture(s)
        IntPtr DeerTex = SDL_image.IMG_LoadTexture(Renderer, "Resources\\deer.png");
        SDL.SDL_Rect DeerTexDrawPosition = TextureSize32; // initialize from texture size, to keep W and H in sync
        DeerTexDrawPosition.x = 128;
        DeerTexDrawPosition.y = 128;

        // load font
        IntPtr VCRFont = SDL_ttf.TTF_OpenFont("Resources\\VCR_OSD_MONO_1.001.ttf", 32);  // filename, fontsize
        SDL.SDL_Color FontColor = new SDL.SDL_Color() { r = 255, g = 127, b = 31 }; // r g b

        // prerender font to texture
        IntPtr FontSurface = SDL_ttf.TTF_RenderText_Solid(VCRFont, "This is a deer!", FontColor);
        IntPtr FontTexture = SDL.SDL_CreateTextureFromSurface(Renderer, FontSurface);

        // get font texture dimensions
        SDL.SDL_QueryTexture(FontTexture, out _, out _, out int FontTexW, out int FontTexH);
        SDL.SDL_Rect FontRect = new SDL.SDL_Rect() { w = FontTexW, h = FontTexH };
        SDL.SDL_Rect FontTarget = FontRect; // initialize from texture size, to keep W and H in sync
        FontTarget.x = 64;
        FontTarget.y = 64;

        // prepare a color for SDL2_gfx
        uint SolidRed = 0xFF0000FF; // Warning, this is in reverse but basically HTML color codes: A B G R

        // main loop
        SDL.SDL_Event e;
        bool stop = false;
        while (!stop)
        {

            ulong start = SDL.SDL_GetPerformanceCounter();

            // start render batch
            SDL.SDL_SetRenderDrawColor(Renderer, 32, 32, 64, 255); // R G B A background color
            SDL.SDL_RenderClear(Renderer);

            // draw texture
            SDL.SDL_RenderCopy(Renderer, DeerTex, ref TextureSize32, ref DeerTexDrawPosition);

            // draw font texture
            SDL.SDL_RenderCopy(Renderer, FontTexture, ref FontRect, ref FontTarget);

            // underline the font with SDL_gfx
            SDL_gfx.lineColor(Renderer, (short)FontTarget.x, (short)(FontTarget.y + FontTarget.h + 10), (short)(FontTarget.x + FontTarget.w), (short)(FontTarget.y + FontTarget.h + 10), SolidRed);

            // end render batch
            SDL.SDL_RenderPresent(Renderer);

            // handle events
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        //quit on window closed
                        Console.WriteLine("Exiting: Caught SDL_QUIT", ConsoleColor.Blue);
                        stop = true;
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        //quit on escape
                        if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                        {
                            Console.WriteLine("Exiting: Caught SDLK_ESCAPE", ConsoleColor.Blue);
                            stop = true;
                        }
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:

                        // example to catch actual key name:
                        Console.WriteLine($"KeyUp: {Enum.GetName(typeof(SDL.SDL_Keycode), e.key.keysym.sym)}");

                        // play sound effect on RETURN (enter)
                        if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_RETURN)
                        {
                            SDL_mixer.Mix_PlayChannel(-1, Pew, 0); //play Pew effect once
                        }

                        break;
                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        // catching this separately to avoid mouse movement spam as unhandled events in console
                        break;
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        switch (e.window.windowEvent)
                        {
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:

                                //minimum size: 640x480
                                bool setRes = false;
                                int xSize = 640;
                                int ySize = 480;
                                if (e.window.data1 < 640)
                                {
                                    setRes = true;
                                }
                                else
                                {
                                    xSize = e.window.data1;
                                }
                                if (e.window.data2 < 480)
                                {
                                    setRes = true;
                                }
                                else
                                {
                                    ySize = e.window.data2;
                                }
                                if (setRes)
                                {
                                    SDL.SDL_SetWindowSize(Window, xSize, ySize);
                                }
                                break;
                            default:

                                Console.WriteLine($"Unhandled window event: {Enum.GetName(typeof(SDL.SDL_WindowEventID), e.window.windowEvent)}", ConsoleColor.Yellow);
                                break;
                        }
                        break;
                    case SDL.SDL_EventType.SDL_TEXTINPUT:
                        // example to catch TextInput event. This supports IME so will work for compound characters like ë â ç and so on.
                        // use this to handle text input (like username/password)
                        byte[] rawBytes = new byte[SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE];
                        unsafe
                        {
                            Marshal.Copy((IntPtr)e.text.text, rawBytes, 0, SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE);
                        }
                        int length = Array.IndexOf(rawBytes, (byte)0);
                        string text = Encoding.UTF8.GetString(rawBytes, 0, length);
                        Console.WriteLine($"Caught TextInput event: {text}", ConsoleColor.Yellow);
                        break;
                    default:
                        Console.WriteLine($"Unhandled event: {Enum.GetName(typeof(SDL.SDL_EventType), e.type)}", ConsoleColor.Yellow);
                        break;
                }
            }

            // limit framerate to ~120 fps when vSync is disabled
            if (!vSync)
            {
                ulong end = SDL.SDL_GetPerformanceCounter();
                float elapsed = (end - start) / (float)SDL.SDL_GetPerformanceFrequency() * 1000.0f;
                uint delay = (uint)Math.Floor(8.333f - elapsed);

                // this check avoids a huge delay when the window can't be drawn during for example moving it
                if (delay < 32)
                {
                    SDL.SDL_Delay(delay);
                }
            }
        } // end of main loop

        // handle shutdown
        Console.WriteLine("Cleaning up...", ConsoleColor.White);

        SDL.SDL_DestroyTexture(FontTexture);
        SDL.SDL_FreeSurface(FontSurface);
        SDL_ttf.TTF_CloseFont(VCRFont);
        SDL_mixer.Mix_FreeChunk(Pew);
        SDL_mixer.Mix_FreeMusic(Music);
        SDL.SDL_DestroyTexture(DeerTex);
        SDL.SDL_DestroyRenderer(Renderer);
        SDL.SDL_DestroyWindow(Window);
        SDL_mixer.Mix_CloseAudio();
        SDL_ttf.TTF_Quit();
        SDL.SDL_Quit();

        Console.WriteLine("Bye!", ConsoleColor.Green);
        Console.ResetColor();
    }
}