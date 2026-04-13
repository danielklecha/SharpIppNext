using System;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Job-Status operation attributes.
/// See: PWG 5100.18-2025 Section 5.9.1
/// </summary>
public class UpdateJobStatusOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    public Uri? OutputDeviceUuid { get; set; }
}
