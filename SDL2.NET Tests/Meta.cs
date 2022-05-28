using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.Tests;

[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class SDLTestRepositoryAttribute : Attribute
{
    public string Name { get; }

    public SDLTestRepositoryAttribute(string name)
    {
        Name = name;
    }
}

[System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class SDLTestMethodAttribute : Attribute
{
    public string Name { get; }

    public SDLTestMethodAttribute(string name)
    {
        Name = name;
    }
}
