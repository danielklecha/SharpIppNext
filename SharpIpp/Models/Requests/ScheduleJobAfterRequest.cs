using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Schedule-Job-After Operation.
/// See: RFC 3998 Section 3.2.6
/// </summary>
public class ScheduleJobAfterRequest : IppRequest<ScheduleJobAfterOperationAttributes>, IIppPrinterRequest { }
