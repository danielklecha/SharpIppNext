using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Deactivate-Printer Operation.
/// See: RFC 3998 Section 3.1.2
/// </summary>
public class DeactivatePrinterRequest : IppRequest<DeactivatePrinterOperationAttributes>, IIppPrinterRequest { }
