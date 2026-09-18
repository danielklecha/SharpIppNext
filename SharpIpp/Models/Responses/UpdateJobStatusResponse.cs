using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Update-Job-Status response.
/// See: PWG 5100.18-2025 Section 5.9.2
/// </summary>
[IppResponse]
public class UpdateJobStatusResponse : IppResponse<OperationAttributes>
{
}
