using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Pause-Printer-After-Current-Job Operation.
/// See: RFC 3998 Section 3.1.2
/// </summary>
public class PausePrinterAfterCurrentJobRequest : IppRequest<PausePrinterAfterCurrentJobOperationAttributes>, IIppPrinterRequest { }
