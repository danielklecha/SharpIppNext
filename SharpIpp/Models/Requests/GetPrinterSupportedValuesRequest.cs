using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Printer-Supported-Values Operation.
/// See: RFC 3380 Section 4.3
/// </summary>
public class GetPrinterSupportedValuesRequest : IppRequest<GetPrinterSupportedValuesOperationAttributes>, IIppPrinterRequest { }
