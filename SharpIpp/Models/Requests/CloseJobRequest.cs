using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Close-Job Operation.
/// See: PWG 5100.7-2023 Section 5.4
/// </summary>
public class CloseJobRequest : IppRequest<CloseJobOperationAttributes>, IIppPrinterRequest
{
}
