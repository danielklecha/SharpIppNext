using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Identify-Printer Operation.
/// See: PWG 5100.13-2023 Section 5.1
/// </summary>
public class IdentifyPrinterRequest : IppRequest<IdentifyPrinterOperationAttributes>, IIppPrinterRequest
{
}
