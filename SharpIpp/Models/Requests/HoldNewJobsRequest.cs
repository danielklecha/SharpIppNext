using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Hold-New-Jobs Operation.
/// See: RFC 3998 Section 3.1.3
/// </summary>
[IppRequest(IppOperation.HoldNewJobs)]
public class HoldNewJobsRequest : IppRequest<HoldNewJobsOperationAttributes>, IIppPrinterRequest { }
