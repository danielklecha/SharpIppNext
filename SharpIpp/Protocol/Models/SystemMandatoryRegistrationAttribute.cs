namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>system-mandatory-registration-attributes</code>.
/// See: PWG 5100.22-2025 Section 7.2.37
/// </summary>
public readonly record struct SystemMandatoryRegistrationAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The output-device-uuid attribute. See: PWG 5100.22-2025 Section 7.2.37</summary>
    public static readonly SystemMandatoryRegistrationAttribute OutputDeviceUuid = new("output-device-uuid");
    /// <summary>The printer-service-type attribute. See: PWG 5100.22-2025 Section 7.2.37</summary>
    public static readonly SystemMandatoryRegistrationAttribute PrinterServiceType = new("printer-service-type");

    public override string ToString() => Value;
    public static implicit operator string(SystemMandatoryRegistrationAttribute value) => value.Value;
    public static implicit operator SystemMandatoryRegistrationAttribute(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
