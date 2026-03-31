using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-System-Attributes response.
/// See: PWG 5100.22-2025 Section 6.3.9
/// </summary>
public class GetSystemAttributesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// System status attributes returned by the System object.
    /// See: PWG 5100.22-2025 Section 7.3
    /// </summary>
    public SystemStatusAttributes? SystemAttributes { get; set; }

    /// <summary>
    /// System description attributes returned by the System object.
    /// See: PWG 5100.22-2025 Section 7.3
    /// </summary>
    public SystemDescriptionAttributes? SystemDescriptionAttributes { get; set; }
}
