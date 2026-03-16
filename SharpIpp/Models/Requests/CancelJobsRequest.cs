using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-Jobs Operation.
/// See: PWG 5100.7-2023 Section 5.1
/// </summary>
public class CancelJobsRequest : IppRequest<CancelJobsOperationAttributes>, IIppPrinterRequest
{
}
