using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-Resource-Attributes response.
/// See: PWG 5100.22-2025 Section 6.2.3
/// </summary>
public class GetResourceAttributesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The status attributes of the requested Resource.
    /// </summary>
    public ResourceStatusAttributes? ResourceAttributes { get; set; }
}
