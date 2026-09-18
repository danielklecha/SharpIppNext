using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Promote-Job Operation.
/// See: RFC 3998 Section 3.2.5
/// </summary>
[IppRequest(IppOperation.PromoteJob)]
public class PromoteJobRequest : IppRequest<PromoteJobOperationAttributes>, IIppPrinterRequest { }
