using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Active-Jobs operation attributes.
/// See: PWG 5100.18-2025 Section 5.7.1
/// </summary>
[IppAttribute]
public class UpdateActiveJobsOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuid, Tag = Tag.Uri)]
    public IppValue<Uri>? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>output-device-job-states</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.11
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceJobStates, Tag = Tag.Enum)]
    public IppValue<JobState[]>? OutputDeviceJobStates { get; set; }

    /// <summary>
    /// The <c>job-ids</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.7.1
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.JobIds, Tag = Tag.Integer)]
    public IppValue<int[]>? JobIds { get; set; }
}
