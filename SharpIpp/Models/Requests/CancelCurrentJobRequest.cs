using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-Current-Job Operation.
/// See: RFC 3998 Section 3.2.2
/// </summary>
public class CancelCurrentJobRequest : IppRequest<CancelCurrentJobOperationAttributes>, IIppPrinterRequest { }
