using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Restart-Printer Operation.
/// See: RFC 3998 Section 3.1.5
/// </summary>
[IppRequest(IppOperation.RestartPrinter)]
public class RestartPrinterRequest : IppRequest<RestartPrinterOperationAttributes>, IIppPrinterRequest { }
