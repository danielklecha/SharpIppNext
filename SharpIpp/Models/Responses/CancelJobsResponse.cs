using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Cancel-Jobs Response.
/// See: PWG 5100.7-2023 Section 5.1.2
/// </summary>
[IppResponse(IppOperation.CancelJobs)]
public class CancelJobsResponse : IppResponse<OperationAttributes>
{
}
