using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Resume-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.14
/// </summary>
[IppRequest(IppOperation.ResumeAllPrinters)]
public class ResumeAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
