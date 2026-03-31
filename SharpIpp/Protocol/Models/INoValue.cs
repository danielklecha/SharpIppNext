namespace SharpIpp.Protocol.Models;

/// <summary>
/// Defines a common value/no-value state contract for models that support NoValue semantics.
/// </summary>
public interface INoValue
{
    /// <summary>
    /// Gets a value indicating whether this instance represents a real value.
    /// </summary>
    bool IsValue { get; }
}
