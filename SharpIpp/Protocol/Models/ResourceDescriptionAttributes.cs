using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Description attributes for a Resource object.
/// See: PWG 5100.22-2025 Section 7.9
/// </summary>
public class ResourceDescriptionAttributes
{
    /// <summary>
    /// The unique identifier for this Resource object.
    /// See: PWG 5100.22-2025 Section 6.2.1
    /// </summary>
    /// <code>resource-id</code>
    public int? ResourceId { get; set; }

    /// <summary>
    /// The resource format (media type).
    /// See: PWG 5100.22-2025 Section 6.2.2
    /// </summary>
    /// <code>resource-format</code>
    public ResourceFormat? ResourceFormat { get; set; }

    /// <summary>
    /// The list of supported data formats.
    /// See: PWG 5100.22-2025 Section 6.2.3
    /// </summary>
    /// <code>resource-formats</code>
    public ResourceFormat[]? ResourceFormats { get; set; }

    /// <summary>
    /// The resource name.
    /// See: PWG 5100.22-2025 Section 6.2.4
    /// </summary>
    /// <code>resource-name</code>
    public string? ResourceName { get; set; }

    /// <summary>
    /// Additional resource info.
    /// See: PWG 5100.22-2025 Section 6.2.5
    /// </summary>
    /// <code>resource-info</code>
    public string? ResourceInfo { get; set; }

    /// <summary>
    /// Resource type.
    /// See: PWG 5100.22-2025 Section 6.2.6
    /// </summary>
    /// <code>resource-type</code>
    public ResourceType? ResourceType { get; set; }

    /// <summary>
    /// Resource version.
    /// See: PWG 5100.22-2025 Section 6.2.7
    /// </summary>
    /// <code>resource-version</code>
    public string? ResourceVersion { get; set; }

    /// <summary>
    /// The current state of this Resource object.
    /// See: PWG 5100.22-2025 Section 6.2.8
    /// </summary>
    /// <code>resource-state</code>
    public ResourceState? ResourceState { get; set; }

    /// <summary>
    /// The set of reasons for this resource state.
    /// See: PWG 5100.22-2025 Section 6.2.9
    /// </summary>
    /// <code>resource-state-reasons</code>
    public ResourceStateReason[]? ResourceStateReasons { get; set; }

    /// <summary>
    /// Human-readable resource state message.
    /// See: PWG 5100.22-2025 Section 6.2.10
    /// </summary>
    /// <code>resource-state-message</code>
    public string? ResourceStateMessage { get; set; }

    /// <summary>
    /// Resource size in kilobytes.
    /// See: PWG 5100.22-2025 Section 6.2.11
    /// </summary>
    /// <code>resource-k-octets</code>
    public int? ResourceKOctets { get; set; }

    /// <summary>
    /// The resource data URI.
    /// See: PWG 5100.22-2025 Section 6.2.12
    /// </summary>
    /// <code>resource-data-uri</code>
    public Uri? ResourceDataUri { get; set; }

    /// <summary>
    /// The number of allocations of this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.13
    /// </summary>
    /// <code>resource-use-count</code>
    public int? ResourceUseCount { get; set; }

    /// <summary>
    /// Unique identifier (UUID) of this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.14
    /// </summary>
    /// <code>resource-uuid</code>
    public OctetString? ResourceUuid { get; set; }

    /// <summary>
    /// The date and time of creation.
    /// See: PWG 5100.22-2025 Section 6.2.15
    /// </summary>
    /// <code>date-time-at-creation</code>
    public DateTimeOffset? DateTimeAtCreation { get; set; }

    /// <summary>
    /// The date and time of installation.
    /// See: PWG 5100.22-2025 Section 6.2.16
    /// </summary>
    /// <code>date-time-at-installed</code>
    public DateTimeOffset? DateTimeAtInstalled { get; set; }

    /// <summary>
    /// The date and time of cancellation.
    /// See: PWG 5100.22-2025 Section 6.2.17
    /// </summary>
    /// <code>date-time-at-canceled</code>
    public DateTimeOffset? DateTimeAtCanceled { get; set; }

    /// <summary>
    /// The time of creation in printer uptime seconds.
    /// See: PWG 5100.22-2025 Section 6.2.18
    /// </summary>
    /// <code>time-at-creation</code>
    public int? TimeAtCreation { get; set; }

    /// <summary>
    /// The time of installation in printer uptime seconds.
    /// See: PWG 5100.22-2025 Section 6.2.19
    /// </summary>
    /// <code>time-at-installed</code>
    public int? TimeAtInstalled { get; set; }

    /// <summary>
    /// The time of cancellation in printer uptime seconds.
    /// See: PWG 5100.22-2025 Section 6.2.20
    /// </summary>
    /// <code>time-at-canceled</code>
    public int? TimeAtCanceled { get; set; }

    /// <summary>
    /// The natural language of this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.21
    /// </summary>
    /// <code>resource-natural-language</code>
    public string? ResourceNaturalLanguage { get; set; }

    /// <summary>
    /// Patches applied to this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.22
    /// </summary>
    /// <code>resource-patches</code>
    public string? ResourcePatches { get; set; }

    /// <summary>
    /// Digital signatures for this Resource (1setOf octetString).
    /// See: PWG 5100.22-2025 Section 6.2.23
    /// </summary>
    /// <code>resource-signature</code>
    public OctetString[]? ResourceSignature { get; set; }

    /// <summary>
    /// Human-readable version string for this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.24
    /// </summary>
    /// <code>resource-string-version</code>
    public string? ResourceStringVersion { get; set; }

    /// <summary>
    /// The set of resource states this resource supports.
    /// See: PWG 5100.22-2025 Section 7.1.20
    /// </summary>
    /// <code>resource-states</code>
    public ResourceState[]? ResourceStates { get; set; }
}
