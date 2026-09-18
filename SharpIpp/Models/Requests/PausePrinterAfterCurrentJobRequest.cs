using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Pause-Printer-After-Current-Job Operation.
/// See: RFC 3998 Section 3.1.2
/// </summary>
[IppRequest(IppOperation.PausePrinterAfterCurrentJob)]
public class PausePrinterAfterCurrentJobRequest : IppRequest<PausePrinterAfterCurrentJobOperationAttributes>, IIppPrinterRequest { }
