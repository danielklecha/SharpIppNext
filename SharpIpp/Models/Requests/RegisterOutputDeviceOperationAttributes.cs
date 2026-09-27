using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Register-Output-Device operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.12
/// </summary>
[IppAttribute]
public class RegisterOutputDeviceOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The identity URI of the Output Device being registered.
    /// See: PWG 5100.22-2025 Section 6.3.12.1
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuid, Tag = Tag.Uri)]
    public IppValue<Uri>? OutputDeviceUuid { get; set; }

    /// <summary>
    /// The <c>output-device-x509-certificate</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.3
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceX509Certificate, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string[]>? OutputDeviceX509Certificate { get; set; }

    /// <summary>
    /// The <c>output-device-x509-request</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.4
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceX509Request, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string[]>? OutputDeviceX509Request { get; set; }

    /// <summary>
    /// The <c>printer-service-type</c> operation attribute specifying the type(s) of print service.
    /// See: PWG 5100.22-2025 Section 7.1.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterServiceType, Tag = Tag.Keyword)]
    public IppValue<PrinterServiceType[]>? PrinterServiceType { get; set; }

    /// <summary>
    /// The <c>printer-xri-requested</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.10
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterXriRequested)]
    public IppValue<SystemXri[]>? PrinterXriRequested { get; set; }
}
