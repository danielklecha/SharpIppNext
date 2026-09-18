using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Printers operation.
/// See: PWG 5100.22-2025 Section 6.1.4
/// </summary>
[IppRequest(IppOperation.GetPrinters)]
public class GetPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
