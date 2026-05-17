using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Suspend-Current-Job Operation.
/// See: RFC 3998 Section 3.2.3
/// </summary>
public class SuspendCurrentJobRequest : IppRequest<SuspendCurrentJobOperationAttributes>, IIppPrinterRequest { }
