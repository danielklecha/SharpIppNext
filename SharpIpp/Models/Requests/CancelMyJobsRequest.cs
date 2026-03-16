using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-My-Jobs Operation.
/// See: PWG 5100.7-2023 Section 5.2
/// </summary>
public class CancelMyJobsRequest : IppRequest<CancelMyJobsOperationAttributes>, IIppPrinterRequest
{
}
