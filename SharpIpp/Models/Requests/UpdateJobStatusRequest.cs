using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Job-Status operation.
/// See: PWG 5100.18-2025 Section 5.9
/// </summary>
public class UpdateJobStatusRequest : IppRequest<UpdateJobStatusOperationAttributes>, IIppJobRequest
{
    /// <summary>
    /// Job status attributes supplied by the Proxy.
    /// See: PWG 5100.18-2025 Section 5.9.1
    /// </summary>
    public JobDescriptionAttributes? JobAttributes { get; set; }
}
