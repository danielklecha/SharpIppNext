namespace SharpIpp.Protocol.Models;

/// <summary>
/// Marker interface for IPP collection types that support NoValue detection.
/// </summary>
public interface IIppCollection
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance represents a "no value" sentinel.
    /// </summary>
    bool IsNoValue { get; set; }
}
