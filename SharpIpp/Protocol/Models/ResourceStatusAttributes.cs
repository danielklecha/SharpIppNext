namespace SharpIpp.Protocol.Models;

/// <summary>
/// Attributes describing the status of a Resource object.
/// See: PWG 5100.22-2025 Section 7.5
/// </summary>
public class ResourceStatusAttributes
{
    /// <summary>
    /// The unique identifier for this Resource object, assigned by the System.
    /// See: PWG 5100.22-2025 Section 7.5.4
    /// </summary>
    /// <code>resource-id</code>
    public int? ResourceId { get; set; }
}
