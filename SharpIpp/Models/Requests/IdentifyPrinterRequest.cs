using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Identify-Printer Operation.
/// See: PWG 5100.13-2023 Section 6.8.4
/// </summary>
public class IdentifyPrinterRequest : IppRequest<IdentifyPrinterOperationAttributes>, IIppPrinterRequest
{
}
