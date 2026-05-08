namespace SharpIpp.Protocol.Models;

/// <summary>
/// Description attributes for a Resource object.
/// See: PWG 5100.22-2025 Section 7.9
/// </summary>
public class ResourceDescriptionAttributes
{
    /// <summary>
    /// The unique identifier for this Resource object.
    /// See: PWG 5100.22-2025 Section 7.9.6
    /// </summary>
    /// <code>resource-id</code>
    public int? ResourceId { get; set; }

    /// <summary>
    /// The resource format (media type).
    /// See: PWG 5100.22-2025 Section 7.9.1
    /// </summary>
    /// <code>resource-format</code>
    public string? ResourceFormat { get; set; }

    /// <summary>
    /// The list of supported data formats.
    /// See: PWG 5100.22-2025 Section 7.9.2
    /// </summary>
    /// <code>resource-formats</code>
    public string[]? ResourceFormats { get; set; }

    /// <summary>
    /// The resource name.
    /// See: PWG 5100.22-2025 Section 7.9.5
    /// </summary>
    /// <code>resource-name</code>
    public string? ResourceName { get; set; }

    /// <summary>
    /// Additional resource info.
    /// See: PWG 5100.22-2025 Section 7.9.4
    /// </summary>
    /// <code>resource-info</code>
    public string? ResourceInfo { get; set; }

    /// <summary>
    /// Resource type.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    /// <code>resource-type</code>
    public ResourceType? ResourceType { get; set; }

    /// <summary>
    /// The set of resource states this resource supports.
    /// See: PWG 5100.22-2025 Section 7.1.20
    /// </summary>
    /// <code>resource-states</code>
    public ResourceState[]? ResourceStates { get; set; }

    /// <summary>
    /// Resource version.
    /// See: PWG 5100.22-2025 Section 7.9.10
    /// </summary>
    /// <code>resource-version</code>
    public string? ResourceVersion { get; set; }
}
