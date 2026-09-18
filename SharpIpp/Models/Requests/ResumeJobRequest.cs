using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Resume-Job Operation.
/// See: RFC 3998 Section 3.2.4
/// </summary>
[IppRequest(IppOperation.ResumeJob)]
public class ResumeJobRequest : IppRequest<ResumeJobOperationAttributes>, IIppPrinterRequest { }
