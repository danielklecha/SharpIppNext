using System;
using System.Collections.Generic;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>destination-uri-ready</c> member collection.
/// See: PWG 5100.17-2014 Section 8.3.3
/// </summary>
public class DestinationUriReady : IIppCollection
{
    public bool IsValue { get; set; } = true;

    /// <summary>
    /// The <c>destination-attributes</c> member attribute (1setOf collection).
    /// Stored as raw IPP dictionaries to preserve destination-specific attributes.
    /// </summary>
    public IDictionary<string, IppAttribute[]>[]? DestinationAttributes { get; set; }

    public string[]? DestinationAttributesSupported { get; set; }
    public string? DestinationInfo { get; set; }
    public bool? DestinationIsDirectory { get; set; }
    public string[]? DestinationMandatoryAccessAttributes { get; set; }
    public string? DestinationName { get; set; }
    public string[]? DestinationOAuthScope { get; set; }
    public string[]? DestinationOAuthToken { get; set; }
    public Uri? DestinationOAuthUri { get; set; }
    public string? DestinationUri { get; set; }
}
