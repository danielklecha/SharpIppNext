namespace SharpIpp.Protocol.Models;

/// <summary>
/// Non-generic interface for <see cref="IppValue{T}"/> enabling untyped value unwrapping.
/// </summary>
public interface IIppValue : INoValue
{
    /// <summary>
    /// Gets the boxed underlying value, or null if in the NoValue state or null.
    /// </summary>
    object? ValueAsObject { get; }
}

