# SDL2.NET
Bringing SDL2 into .NET with its code styles and best practices. Based on https://github.com/flibitijibibo/SDL2-CS (and/or https://github.com/ppy/SDL2-CS) and forked from https://github.com/DoogeJ/SDL2Example
There are probably a lot of beginner mistakes in this; feel free to point them out to me.

Library project [MIT License](LICENSE.md). All used external libraries, binaries, resources under their respective licenses as listed here or stored in the [lib](lib) folder.

### Requirements:
* This is a .NET 6 application: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

### Hints and tips:
* Check the comments in [SDL2Example.csproj](SDL2Example.csproj)
* If you use VS Code, set the program path in launch.json to the executable (not dll), for example `${workspaceFolder}/bin/Debug/net6.0/SDL2Example.exe` or external resources will not load properly when debugging

### Learning resources:
* Official SDL docs: https://wiki.libsdl.org/
* SDL_ttf docs: https://www.libsdl.org/projects/docs/SDL_ttf/
* SDL_image docs: https://libsdl.org/projects/docs/SDL_image/
* SDL_mixer docs: https://libsdl.org/projects/docs/SDL_mixer/
* SDL_gfx docs: https://www.ferzkopp.net/Software/SDL2_gfx/Docs/html/index.html
* Getting started with SDL tutorials: https://lazyfoo.net/tutorials/SDL/index.php
* Jemery Sayers' SDL2-CS basics: https://github.com/JeremySayers/SDL2-CS-Tutorial

### Binary sources:
* SDL2: https://www.libsdl.org/download-2.0.php
* SDL2_gfx: https://github.com/py-sdl/sdl2gfx-builds/releases/tag/1.0.4
* SDL2_image: https://www.libsdl.org/projects/SDL_image/
* SDL2_mixer: https://www.libsdl.org/projects/SDL_mixer/
* SDL2_ttf: https://github.com/libsdl-org/SDL_ttf/releases/tag/release-2.0.18

### Library sources:
* SDL2-CS, Ethan Lee (flibitijibibo), zlib/libpng license: https://github.com/flibitijibibo/SDL2-CS (and/or https://github.com/ppy/SDL2-CS)
