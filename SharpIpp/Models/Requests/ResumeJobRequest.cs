using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Resume-Job Operation.
/// See: RFC 3998 Section 3.2.4
/// </summary>
public class ResumeJobRequest : IppRequest<ResumeJobOperationAttributes>, IIppPrinterRequest { }
