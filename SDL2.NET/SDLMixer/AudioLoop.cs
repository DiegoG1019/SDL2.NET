using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET.SDLMixer;

/// <summary>
/// Represents an amount of loops for an AudioChunk to play
/// </summary>
public static class AudioLoop
{
    /// <summary>
    /// Gets a loop value that plays the AudioChunk the set amount of times
    /// </summary>
    /// <param name="loops">The amount of times to play the AudioChunk. Set to 1 for <see cref="Once"/></param>
    /// <returns>A loop instance representing the loop value</returns>
    /// <remarks>Same as new Loop(loops - 1)</remarks>
    public static int FromTimes(int loops) 
        => loops <= 0
            ? throw new ArgumentOutOfRangeException(nameof(loops), loops, $"loops must be between 1 and {int.MaxValue}")
            : loops - 1;

    /// <summary>
    /// Plays the AudioChunk once
    /// </summary>
    /// <remarks>Same as passing a 0</remarks>
    public static int Once => 0;

    /// <summary>
    /// Plays the AudioChunk twice
    /// </summary>
    /// <remarks>Same as passing a 1</remarks>
    public static int Twice => 1;

    /// <summary>
    /// Plays the AudioChunk three times
    /// </summary>
    /// <remarks>Same as passing a 2</remarks>
    public static int Thrice => 2;

    /// <summary>
    /// Plays the AudioChunk indefinitely
    /// </summary>
    /// <remarks>Same as passing a -1</remarks>
    public static int Infinite => -1;
}
