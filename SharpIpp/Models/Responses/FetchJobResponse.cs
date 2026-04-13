using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Fetch-Job response.
/// See: PWG 5100.18-2025 Section 5.6.2
/// </summary>
public class FetchJobResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// Job attributes returned by the Infrastructure Printer.
    /// </summary>
    public JobDescriptionAttributes? JobAttributes { get; set; }
}
