using SharpIpp.Mapping;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Cancel-My-Jobs Response.
/// See: PWG 5100.7-2023 Section 5.2.2
/// </summary>
[IppResponse]
public class CancelMyJobsResponse : IppResponse<OperationAttributes>
{
}
