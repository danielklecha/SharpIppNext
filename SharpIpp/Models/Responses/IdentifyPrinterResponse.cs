using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Identify-Printer Response.
/// See: PWG 5100.13-2023 Section 5.1.2
/// </summary>
public class IdentifyPrinterResponse : IppResponse<OperationAttributes>
{
}
