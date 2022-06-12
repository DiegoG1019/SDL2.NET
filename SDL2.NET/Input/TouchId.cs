using System.Runtime.CompilerServices;

namespace SDL2.NET.Input;

/// <summary>
/// Represents the identification of a touch device
/// </summary>
/// <remarks>
/// For all intents and purposes, this is a long value.
/// </remarks>
public readonly record struct TouchId(long Id)
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => Id.GetHashCode();

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string? ToString() => Id.ToString();

    /// <summary>
    /// Converts a TouchId into its equivalent long value
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator long(TouchId touch) => touch.Id;

    /// <summary>
    /// Converts a long value into its equivalent TouchId
    /// </summary>
    /// <param name="touch"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TouchId(long touch) => new(touch);
}
