using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Create-Printer-Subscriptions Operation.
/// See: RFC 3995 Section 5.1
/// See: RFC 3995 Section 11.1.1
/// </summary>
public class CreatePrinterSubscriptionsRequest : IppRequest<CreatePrinterSubscriptionsOperationAttributes>, IIppPrinterRequest { }
