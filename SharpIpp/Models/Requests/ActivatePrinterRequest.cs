using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Activate-Printer Operation.
/// See: RFC 3998 Section 3.1.3
/// </summary>
public class ActivatePrinterRequest : IppRequest<ActivatePrinterOperationAttributes>, IIppPrinterRequest { }
