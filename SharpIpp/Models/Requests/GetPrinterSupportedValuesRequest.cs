using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Printer-Supported-Values Operation.
/// See: RFC 3380 Section 4.3
/// </summary>
[IppRequest(IppOperation.GetPrinterSupportedValues)]
public class GetPrinterSupportedValuesRequest : IppRequest<GetPrinterSupportedValuesOperationAttributes>, IIppPrinterRequest { }
