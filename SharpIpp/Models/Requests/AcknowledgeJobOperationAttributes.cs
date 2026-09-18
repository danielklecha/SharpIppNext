using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Job operation attributes.
/// See: PWG 5100.18-2025 Section 5.3
/// See: PWG 5100.18-2025 Section 5.3.1
/// See: PWG 5100.18-2025 Section 14.4
/// </summary>
[IppAttribute]
public class AcknowledgeJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.1.1
    /// See: PWG 5100.18-2025 Section 5.3.1
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// See: PWG 5100.18-2025 Section 14.1
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuid, Tag = Tag.Uri)]
    public Uri? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>output-device-job-states</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.3.1
    /// See: PWG 5100.18-2025 Section 7.1.7
    /// See: PWG 5100.18-2025 Section 14.3
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceJobStates, Tag = Tag.Enum)]
    public JobState[]? OutputDeviceJobStates { get; set; }

    /// <summary>
    /// The <c>fetch-status-code</c> operation attribute.
    /// Allowed values include all registered IPP status codes (except 'successful-ok' (0x0000)).
    /// See: PWG 5100.18-2025 Section 5.1.1
    /// See: PWG 5100.18-2025 Section 5.3.1
    /// See: PWG 5100.18-2025 Section 7.1.5
    /// See: PWG 5100.18-2025 Section 14.3
    /// </summary>
    [IppAttribute(IppAttributeNames.FetchStatusCode, Tag = Tag.Enum)]
    public IppStatusCode? FetchStatusCode { get; set; }

    /// <summary>
    /// The <c>fetch-status-message</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.1.1
    /// See: PWG 5100.18-2025 Section 5.3.1
    /// See: PWG 5100.18-2025 Section 7.1.6
    /// See: PWG 5100.18-2025 Section 14.1
    /// </summary>
    [IppAttribute(IppAttributeNames.FetchStatusMessage, Tag = Tag.TextWithoutLanguage)]
    public string? FetchStatusMessage { get; set; }
}
