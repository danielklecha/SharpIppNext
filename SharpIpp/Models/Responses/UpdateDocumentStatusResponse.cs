using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Update-Document-Status response.
/// See: PWG 5100.18-2025 Section 5.8.2
/// </summary>
[IppResponse]
public class UpdateDocumentStatusResponse : IppResponse<OperationAttributes>
{
}
