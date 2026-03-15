using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class PrintUriResponse : IppResponse<OperationAttributes>, IIppJobResponse
{
    /// <summary>
    /// The job-attributes IPP attribute.
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0
    /// </summary>
    /// <code>job-attributes</code>
    public JobAttributes? JobAttributes { get; set; }
    /// <summary>
    /// The document-attributes IPP attribute.
    /// See: pwg5100.18 - IPP Shared Infrastructure Extensions v1.1
    /// </summary>
    /// <code>document-attributes</code>
    public DocumentAttributes? DocumentAttributes { get; set; }
}
