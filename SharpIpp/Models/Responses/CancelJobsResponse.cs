using SharpIpp.Mapping;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Cancel-Jobs Response.
/// See: PWG 5100.7-2023 Section 5.1.2
/// </summary>
[IppResponse]
public class CancelJobsResponse : IppResponse<OperationAttributes>
{
}
