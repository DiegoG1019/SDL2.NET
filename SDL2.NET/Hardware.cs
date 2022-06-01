using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Provides methods and properties to query the underlying system's hardware specs. 
/// </summary>
/// <remarks>
/// For .NET Native ways of getting (and using) this information (excluding <see cref="SystemRAM"/>) see: <see cref="Environment"/> and explore Hardware Intrinsics <see cref="System.Runtime.Intrinsics" href="https://devblogs.microsoft.com/dotnet/hardware-intrinsics-in-net-core/"/>
/// </remarks>
public static class Hardware
{
    /// <summary>
    /// Returns the total number of logical CPU cores. 
    /// </summary>
    /// <remarks>
    /// On CPUs that include technologies such as hyperthreading, the number of logical cores may be more than the number of physical cores. Also see <see cref="Environment.ProcessorCount"/>
    /// </remarks>
    public static int CPUCount => SDL_GetCPUCount();

    /// <summary>
    /// Determine the L1 cache line size of the CPU in bytes
    /// </summary>
    /// <remarks>
    /// This is useful for determining multi-threaded structure padding or SIMD prefetch sizes.
    /// </remarks>
    public static int CPUCacheLineSize => SDL_GetCPUCacheLineSize();

    /// <summary>
    /// Determine whether the CPU has the RDTSC instruction
    /// </summary>
    /// <remarks>This always returns false on CPUs that aren't using Intel instruction sets</remarks>
    public static bool HasRDTSC => SDL_HasRDTSC() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has AltiVec features
    /// </summary>
    /// <remarks>This always returns false on CPUs that aren't using PowerPC instruction sets</remarks>
    public static bool HasAltiVec  => SDL_HasAltiVec() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has MMX features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </remarks>
    public static bool HasMMX => SDL_HasMMX() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has 3DNow! features.
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using AMD instruction sets.
    /// </remarks>
    public static bool Has3DNow => SDL_Has3DNow() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has SSE features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasSSE => SDL_HasSSE() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has SSE2 features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasSSE2 => SDL_HasSSE2() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has SSE3 features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasSSE3 => SDL_HasSSE3() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has SSE4.1 features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasSSE41 => SDL_HasSSE41() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has SSE4.2 features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasSSE42 => SDL_HasSSE42() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has AVX features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasAVX => SDL_HasAVX() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has AVX2 features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasAVX2 => SDL_HasAVX2() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has AVX-512F (foundation) features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using Intel instruction sets
    /// </remarks>
    public static bool HasAVX512F => SDL_HasAVX512F() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Determine whether the CPU has NEON (ARM SIMD) features
    /// </summary>
    /// <remarks>
    /// This always returns false on CPUs that aren't using ARM instruction sets.
    /// </remarks>
    public static bool HasNEON => SDL_HasNEON() == SDL_bool.SDL_TRUE;

    /// <summary>
    /// Get the amount of RAM configured in the system in MB
    /// </summary>
    public static int SystemRAM => SDL_GetSystemRAM();

    /// <summary>
    /// Returns the alignment in bytes needed for available, known SIMD instructions
    /// </summary>
    /// <remarks>
    /// This will return the minimum number of bytes to which a pointer must be aligned to be compatible with SIMD instructions on the current machine. For example, if the machine supports SSE only, it will return 16, but if it supports AVX-512F (<see cref="HasAVX512F"/>), it'll return 64 (etc). This only reports values for instruction sets SDL knows about, so if your SDL build doesn't have <see cref="SDL_HasAVX512F"/>, then it might return 16 for the SSE support it sees and not 64 for the AVX-512 instructions that exist but SDL doesn't know about. Plan accordingly.
    /// </remarks>
    public static uint SIMDGetAlignment => SDL_SIMDGetAlignment();

    /// <summary>
    /// Determine whether the CPU has ARM SIMD (ARMv6) features
    /// </summary>
    /// <remarks>
    /// This is different from ARM NEON, which is a different instruction set. This always returns false on CPUs that aren't using ARM instruction sets
    /// </remarks>
    public static bool HasARMSIMD => SDL_HasARMSIMD() == SDL_bool.SDL_TRUE;
}
