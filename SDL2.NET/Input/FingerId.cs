using System.Runtime.CompilerServices;

namespace SDL2.NET.Input;

/// <summary>
/// Represents the identification of a finger on a touch device
/// </summary>
/// <remarks>
/// For all intents and purposes, this is a long value.
/// </remarks>
public readonly record struct FingerId(long Id)
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => Id.GetHashCode();

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string? ToString() => Id.ToString();

    /// <summary>
    /// Converts a FingerId into its equivalent long value
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator long(FingerId touch) => touch.Id;

    /// <summary>
    /// Converts a long value into its equivalent FingerId
    /// </summary>
    /// <param name="touch"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator FingerId(long touch) => new(touch);
}