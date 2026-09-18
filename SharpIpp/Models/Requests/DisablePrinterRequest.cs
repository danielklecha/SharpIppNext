using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Disable-Printer Operation.
/// See: RFC 3998 Section 3.1.1
/// </summary>
[IppRequest(IppOperation.DisablePrinter)]
public class DisablePrinterRequest : IppRequest<DisablePrinterOperationAttributes>, IIppPrinterRequest { }
