using System;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Output-Device-Attributes operation attributes.
/// See: PWG 5100.18-2025
/// </summary>
public class GetOutputDeviceAttributesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>requested-attributes</c> operation attribute.
    /// See: RFC 8011 Section 5.4.1
    /// </summary>
    public string[]? RequestedAttributes { get; set; }

    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    public Uri? OutputDeviceUuid { get; set; }
}
