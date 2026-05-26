namespace SharpIpp.Protocol.Models;

/// <summary>
/// Defines a mutable state contract for reference-type models that support setting NoValue state.
/// </summary>
public interface INoValueWritable : INoValue
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance represents a real value.
    /// </summary>
    new bool IsValue { get; set; }
}
