using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Promote-Job Operation.
/// See: RFC 3998 Section 3.2.5
/// </summary>
public class PromoteJobRequest : IppRequest<PromoteJobOperationAttributes>, IIppPrinterRequest { }
