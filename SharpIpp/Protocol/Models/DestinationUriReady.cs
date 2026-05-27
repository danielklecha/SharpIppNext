using System;
using System.Collections.Generic;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>destination-uri-ready</c> member collection.
/// See: PWG 5100.17-2014 Section 8.3.3
/// </summary>
public class DestinationUriReady : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

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
    public OctetString[]? DestinationOAuthScope { get; set; }
    public OctetString[]? DestinationOAuthToken { get; set; }
    public Uri? DestinationOAuthUri { get; set; }
    public Uri? DestinationUri { get; set; }
}
