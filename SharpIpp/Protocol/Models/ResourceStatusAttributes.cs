using System;

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

    /// <summary>
    /// The current state of this Resource object.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    /// <code>resource-state</code>
    public ResourceState? ResourceState { get; set; }

    /// <summary>
    /// The date and time of cancelation or abortion.
    /// See: PWG 5100.22-2025 Section 7.9.1
    /// </summary>
    /// <code>date-time-at-canceled</code>
    public DateTimeOffset? DateTimeAtCanceled { get; set; }

    /// <summary>
    /// The date and time of creation.
    /// See: PWG 5100.22-2025 Section 7.9.2
    /// </summary>
    /// <code>date-time-at-creation</code>
    public DateTimeOffset? DateTimeAtCreation { get; set; }

    /// <summary>
    /// The date and time of installation.
    /// See: PWG 5100.22-2025 Section 7.9.3
    /// </summary>
    /// <code>date-time-at-installed</code>
    public DateTimeOffset? DateTimeAtInstalled { get; set; }

    /// <summary>
    /// Human-readable resource state message.
    /// See: PWG 5100.22-2025 Section 7.9.12
    /// </summary>
    /// <code>resource-state-message</code>
    public string? ResourceStateMessage { get; set; }

    /// <summary>
    /// The set of reasons for this resource state.
    /// See: PWG 5100.22-2025 Section 7.9.13
    /// </summary>
    /// <code>resource-state-reasons</code>
    public ResourceStateReason[]? ResourceStateReasons { get; set; }

    /// <summary>
    /// Resource size in kilobytes.
    /// See: PWG 5100.22-2025 Section 7.9.7
    /// </summary>
    /// <code>resource-k-octets</code>
    public int? ResourceKOctets { get; set; }

    /// <summary>
    /// The resource data URI.
    /// See: PWG 5100.22-2025 Section 7.9.4
    /// </summary>
    /// <code>resource-data-uri</code>
    public Uri? ResourceDataUri { get; set; }

    /// <summary>
    /// The number of allocations of this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.16
    /// </summary>
    /// <code>resource-use-count</code>
    public int? ResourceUseCount { get; set; }

    /// <summary>
    /// Unique identifier (UUID) of this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.13
    /// </summary>
    /// <code>resource-uuid</code>
    public Uri? ResourceUuid { get; set; }

    /// <summary>
    /// The total time from creation to cancellation/installation in seconds for this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.1 for time-at-canceled
    /// </summary>
    /// <code>time-at-canceled</code>
    public int? TimeAtCanceled { get; set; }

    /// <summary>
    /// The total time from creation to current state in seconds.
    /// See: PWG 5100.22-2025 Section 7.9.2 for time-at-creation
    /// </summary>
    /// <code>time-at-creation</code>
    public int? TimeAtCreation { get; set; }

    /// <summary>
    /// The total time from creation to installation in seconds.
    /// See: PWG 5100.22-2025 Section 7.9.3 for time-at-installed
    /// </summary>
    /// <code>time-at-installed</code>
    public int? TimeAtInstalled { get; set; }

    /// <summary>
    /// Version identifier of this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.10
    /// </summary>
    /// <code>resource-version</code>
    public string? ResourceVersion { get; set; }

    /// <summary>
    /// Human-readable resource information.
    /// See: PWG 5100.22-2025 Section 7.9.4
    /// </summary>
    /// <code>resource-info</code>
    public string? ResourceInfo { get; set; }

    /// <summary>
    /// Human-readable resource name.
    /// See: PWG 5100.22-2025 Section 7.9.5
    /// </summary>
    /// <code>resource-name</code>
    public string? ResourceName { get; set; }

    /// <summary>
    /// MIME type format of this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.1
    /// </summary>
    /// <code>resource-format</code>
    public string? ResourceFormat { get; set; }

    /// <summary>
    /// Supported formats for this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.2
    /// </summary>
    /// <code>resource-formats</code>
    public string[]? ResourceFormats { get; set; }

    /// <summary>
    /// Natural language of this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.8
    /// </summary>
    /// <code>resource-natural-language</code>
    public string? ResourceNaturalLanguage { get; set; }

    /// <summary>
    /// Patches applied to this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.9
    /// </summary>
    /// <code>resource-patches</code>
    public string? ResourcePatches { get; set; }

    /// <summary>
    /// Digital signatures for this Resource.
    /// See: PWG 5100.22-2025 Section 7.9.10
    /// </summary>
    /// <code>resource-signature</code>
    public byte[][]? ResourceSignature { get; set; }

    /// <summary>
    /// Resource string version.
    /// See: PWG 5100.22-2025 Section 7.9.14
    /// </summary>
    /// <code>resource-string-version</code>
    public string? ResourceStringVersion { get; set; }

    /// <summary>
    /// Resource type keyword.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    /// <code>resource-type</code>
    public string? ResourceType { get; set; }
}
