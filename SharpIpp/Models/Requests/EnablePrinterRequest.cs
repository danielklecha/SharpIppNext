using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Enable-Printer Operation.
/// See: RFC 3998 Section 3.1.1
/// </summary>
public class EnablePrinterRequest : IppRequest<EnablePrinterOperationAttributes>, IIppPrinterRequest { }
