using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Release-Held-New-Jobs Operation.
/// See: RFC 3998 Section 3.2.1
/// </summary>
public class ReleaseHeldNewJobsRequest : IppRequest<ReleaseHeldNewJobsOperationAttributes>, IIppPrinterRequest { }
