using System;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Register-Output-Device operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.12
/// </summary>
public class RegisterOutputDeviceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The identity URI of the Output Device being registered.
    /// See: PWG 5100.22-2025 Section 6.3.12.1
    /// </summary>
    public Uri? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>output-device-x509-certificate</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.3
    /// </summary>
    public string[]? OutputDeviceX509Certificate { get; set; }

    /// <summary>
    /// The <c>output-device-x509-request</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.4
    /// </summary>
    public string[]? OutputDeviceX509Request { get; set; }

    /// <summary>
    /// The <c>printer-service-type</c> operation attribute specifying the type(s) of print service.
    /// See: PWG 5100.22-2025 Section 7.1.9
    /// </summary>
    public string[]? PrinterServiceType { get; set; }

    /// <summary>
    /// The <c>printer-xri-requested</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.10
    /// </summary>
    public string[]? PrinterXriRequested { get; set; }
}
