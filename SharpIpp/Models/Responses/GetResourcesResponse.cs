using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-Resources response.
/// See: PWG 5100.22-2025 Section 6.3.7
/// </summary>
public class GetResourcesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The list of resources returned by this response.
    /// </summary>
    public ResourceDescriptionAttributes[]? ResourcesAttributes { get; set; }
}
