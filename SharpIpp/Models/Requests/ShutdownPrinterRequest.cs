using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Shutdown-Printer Operation.
/// See: RFC 3998 Section 3.1.6
/// </summary>
public class ShutdownPrinterRequest : IppRequest<ShutdownPrinterOperationAttributes>, IIppPrinterRequest { }
