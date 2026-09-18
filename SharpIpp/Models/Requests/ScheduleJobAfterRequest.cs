using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Schedule-Job-After Operation.
/// See: RFC 3998 Section 3.2.6
/// </summary>
[IppRequest(IppOperation.ScheduleJobAfter)]
public class ScheduleJobAfterRequest : IppRequest<ScheduleJobAfterOperationAttributes>, IIppPrinterRequest { }
