using SharpIpp.Mapping;
using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Description attributes for a Resource object.
/// See: PWG 5100.22-2025 Section 7.9
/// </summary>
[IppAttribute]
public class ResourceDescriptionAttributes
{
    /// <summary>
    /// The unique identifier for this Resource object.
    /// See: PWG 5100.22-2025 Section 6.2.1
    /// </summary>
    /// <code>resource-id</code>
    [IppAttribute(IppAttributeNames.ResourceId, Tag = Tag.Integer)]
    public IppValue<int>? ResourceId { get; set; }

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
    [IppAttribute(IppAttributeNames.ResourceFormats, Tag = Tag.MimeMediaType)]
    public IppValue<ResourceFormat[]>? ResourceFormats { get; set; }

    /// <summary>
    /// The resource name.
    /// See: PWG 5100.22-2025 Section 6.2.4
    /// </summary>
    /// <code>resource-name</code>
    [IppAttribute(IppAttributeNames.ResourceName, Tag = Tag.NameWithoutLanguage)]
    public IppValue<string>? ResourceName { get; set; }

    /// <summary>
    /// Additional resource info.
    /// See: PWG 5100.22-2025 Section 6.2.5
    /// </summary>
    /// <code>resource-info</code>
    [IppAttribute(IppAttributeNames.ResourceInfo, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? ResourceInfo { get; set; }

    /// <summary>
    /// Resource type.
    /// See: PWG 5100.22-2025 Section 6.2.6
    /// </summary>
    /// <code>resource-type</code>
    [IppAttribute(IppAttributeNames.ResourceType, Tag = Tag.Keyword)]
    public IppValue<ResourceType>? ResourceType { get; set; }

    /// <summary>
    /// Resource version.
    /// See: PWG 5100.22-2025 Section 6.2.7
    /// </summary>
    /// <code>resource-version</code>
    [IppAttribute(IppAttributeNames.ResourceVersion, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? ResourceVersion { get; set; }

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
    [IppAttribute(IppAttributeNames.ResourceStateReasons)]
    public IppValue<ResourceStateReason[]>? ResourceStateReasons { get; set; }

    /// <summary>
    /// Human-readable resource state message.
    /// See: PWG 5100.22-2025 Section 6.2.10
    /// </summary>
    /// <code>resource-state-message</code>
    [IppAttribute(IppAttributeNames.ResourceStateMessage, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? ResourceStateMessage { get; set; }

    /// <summary>
    /// Resource size in kilobytes.
    /// See: PWG 5100.22-2025 Section 6.2.11
    /// </summary>
    /// <code>resource-k-octets</code>
    [IppAttribute(IppAttributeNames.ResourceKOctets, Tag = Tag.Integer)]
    public IppValue<int>? ResourceKOctets { get; set; }

    /// <summary>
    /// The resource data URI.
    /// See: PWG 5100.22-2025 Section 6.2.12
    /// </summary>
    /// <code>resource-data-uri</code>
    [IppAttribute(IppAttributeNames.ResourceDataUri, Tag = Tag.Uri)]
    public IppValue<Uri>? ResourceDataUri { get; set; }

    /// <summary>
    /// The number of allocations of this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.13
    /// </summary>
    /// <code>resource-use-count</code>
    [IppAttribute(IppAttributeNames.ResourceUseCount, Tag = Tag.Integer)]
    public IppValue<int>? ResourceUseCount { get; set; }

    /// <summary>
    /// Unique identifier (UUID) of this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.14
    /// </summary>
    /// <code>resource-uuid</code>
    [IppAttribute(IppAttributeNames.ResourceUuid, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public IppValue<OctetString>? ResourceUuid { get; set; }

    /// <summary>
    /// The date and time of creation.
    /// See: PWG 5100.22-2025 Section 6.2.15
    /// </summary>
    /// <code>date-time-at-creation</code>
    [IppAttribute(IppAttributeNames.ResourceDateTimeAtCreation, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtCreation { get; set; }

    /// <summary>
    /// The date and time of installation.
    /// See: PWG 5100.22-2025 Section 6.2.16
    /// </summary>
    /// <code>date-time-at-installed</code>
    [IppAttribute(IppAttributeNames.ResourceDateTimeAtInstalled, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtInstalled { get; set; }

    /// <summary>
    /// The date and time of cancellation.
    /// See: PWG 5100.22-2025 Section 6.2.17
    /// </summary>
    /// <code>date-time-at-canceled</code>
    [IppAttribute(IppAttributeNames.ResourceDateTimeAtCanceled, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtCanceled { get; set; }

    /// <summary>
    /// The time of creation in printer uptime seconds.
    /// See: PWG 5100.22-2025 Section 6.2.18
    /// </summary>
    /// <code>time-at-creation</code>
    [IppAttribute(IppAttributeNames.ResourceTimeAtCreation, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtCreation { get; set; }

    /// <summary>
    /// The time of installation in printer uptime seconds.
    /// See: PWG 5100.22-2025 Section 6.2.19
    /// </summary>
    /// <code>time-at-installed</code>
    [IppAttribute(IppAttributeNames.ResourceTimeAtInstalled, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtInstalled { get; set; }

    /// <summary>
    /// The time of cancellation in printer uptime seconds.
    /// See: PWG 5100.22-2025 Section 6.2.20
    /// </summary>
    /// <code>time-at-canceled</code>
    [IppAttribute(IppAttributeNames.ResourceTimeAtCanceled, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtCanceled { get; set; }

    /// <summary>
    /// The natural language of this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.21
    /// </summary>
    /// <code>resource-natural-language</code>
    [IppAttribute(IppAttributeNames.ResourceNaturalLanguage, Tag = Tag.NaturalLanguage)]
    public IppValue<string>? ResourceNaturalLanguage { get; set; }

    /// <summary>
    /// Patches applied to this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.22
    /// </summary>
    /// <code>resource-patches</code>
    [IppAttribute(IppAttributeNames.ResourcePatches, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? ResourcePatches { get; set; }

    /// <summary>
    /// Digital signatures for this Resource (1setOf octetString).
    /// See: PWG 5100.22-2025 Section 6.2.23
    /// </summary>
    /// <code>resource-signature</code>
    [IppAttribute(IppAttributeNames.ResourceSignature, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public IppValue<OctetString[]>? ResourceSignature { get; set; }

    /// <summary>
    /// Human-readable version string for this Resource.
    /// See: PWG 5100.22-2025 Section 6.2.24
    /// </summary>
    /// <code>resource-string-version</code>
    [IppAttribute(IppAttributeNames.ResourceStringVersion, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? ResourceStringVersion { get; set; }

    /// <summary>
    /// The set of resource states this resource supports.
    /// See: PWG 5100.22-2025 Section 7.1.20
    /// </summary>
    /// <code>resource-states</code>
    [IppAttribute(IppAttributeNames.ResourceStates)]
    public IppValue<ResourceState[]>? ResourceStates { get; set; }
}
