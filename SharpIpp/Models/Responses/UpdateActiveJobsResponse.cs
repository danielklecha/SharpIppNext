using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Update-Active-Jobs response.
/// See: PWG 5100.18-2025 Section 5.7.2
/// </summary>
public class UpdateActiveJobsResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The <c>job-ids</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.7.2
    /// </summary>
    public int[]? JobIds { get; set; }

    /// <summary>
    /// The <c>output-device-job-states</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 5.7.2
    /// </summary>
    public JobState[]? OutputDeviceJobStates { get; set; }
}
