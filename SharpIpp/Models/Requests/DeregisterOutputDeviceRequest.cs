namespace SharpIpp.Models.Requests;

/// <summary>
/// Deregister-Output-Device operation.
/// See: PWG 5100.18-2025 Section 5.4
/// </summary>
public class DeregisterOutputDeviceRequest : IppRequest<DeregisterOutputDeviceOperationAttributes>, IIppPrinterRequest
{
}
