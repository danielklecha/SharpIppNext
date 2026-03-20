using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Job operation attributes.
/// See: PWG 5100.18-2025 Section 5.3.1
/// </summary>
public class AcknowledgeJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    public Uri? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>output-device-job-states</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.11
    /// </summary>
    public JobState[]? OutputDeviceJobStates { get; set; }
}
