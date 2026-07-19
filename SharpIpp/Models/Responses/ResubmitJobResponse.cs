using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Resubmit-Job Response.
/// See: PWG 5100.7-2023 Section 5.4.2
/// </summary>
public class ResubmitJobResponse : IppResponse<OperationAttributes>, IIppJobResponse
{
    /// <summary>
    /// The job-attributes IPP attribute.
    /// See: pwg5100.7
    /// </summary>
    /// <code>job-attributes</code>
    public JobAttributes? JobAttributes { get; set; }
}

