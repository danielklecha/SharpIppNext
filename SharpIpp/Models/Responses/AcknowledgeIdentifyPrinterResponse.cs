using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Acknowledge-Identify-Printer response.
/// See: PWG 5100.18-2025 Section 5.2.2
/// </summary>
public class AcknowledgeIdentifyPrinterResponse : IppResponse<OperationAttributes>
{
}
