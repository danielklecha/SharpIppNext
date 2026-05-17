using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Startup-Printer Operation.
/// See: RFC 3998 Section 3.1.6
/// </summary>
public class StartupPrinterRequest : IppRequest<StartupPrinterOperationAttributes>, IIppPrinterRequest { }
