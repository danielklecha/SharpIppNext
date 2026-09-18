using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-System-Supported-Values operation.
/// See: PWG 5100.22-2025 Section 6.3.9
/// </summary>
[IppRequest(IppOperation.GetSystemSupportedValues)]
public class GetSystemSupportedValuesRequest : IppRequest<GetSystemSupportedValuesOperationAttributes>, IIppSystemRequest
{
}
