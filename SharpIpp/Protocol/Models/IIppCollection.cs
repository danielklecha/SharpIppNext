namespace SharpIpp.Protocol.Models;

/// <summary>
/// Marker interface for IPP collection types that support NoValue detection.
/// </summary>
public interface IIppCollection : INoValue
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance represents a real value.
    /// </summary>
    new bool IsValue { get; set; }
}
