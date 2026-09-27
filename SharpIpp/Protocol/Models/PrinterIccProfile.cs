using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the printer-icc-profiles collection attribute.
/// Each element describes an ICC profile available on the printer.
/// See: PWG 5100.13-2023 Section 6.5.34
/// </summary>
[IppAttribute(IppAttributeNames.PrinterIccProfiles)]
public class PrinterIccProfile : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// profile-name — human-readable name of the ICC profile (name).
    /// See: PWG 5100.13-2023 Section 6.5.34
    /// </summary>
    public IppValue<string>? ProfileName { get; set; }

    /// <summary>
    /// profile-uri — reference to the ICC color profile (uri).
    /// See: PWG 5100.13-2023 Section 6.5.34
    /// </summary>
    public IppValue<Uri>? ProfileUri { get; set; }
}
