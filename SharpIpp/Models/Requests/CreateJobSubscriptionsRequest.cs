using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Create-Job-Subscriptions Operation.
/// See: RFC 3995 Section 5.1
/// See: RFC 3995 Section 11.1.2
/// </summary>
public class CreateJobSubscriptionsRequest : IppRequest<CreateJobSubscriptionsOperationAttributes>, IIppPrinterRequest { }
