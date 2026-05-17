namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-amount-units</c> attribute.
/// See: PWG 5100.21-2019 Section 8.1.3.2
/// </summary>
public readonly record struct MaterialAmountUnits(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>Amount measured in grams (g). See: PWG 5100.21-2019 Section 8.1.3.2</summary>
    public static readonly MaterialAmountUnits Grams = new("g");
    /// <summary>Amount measured in kilograms (kg). See: PWG 5100.21-2019 Section 8.1.3.2</summary>
    public static readonly MaterialAmountUnits Kilograms = new("kg");
    /// <summary>Amount measured in liters (l). See: PWG 5100.21-2019 Section 8.1.3.2</summary>
    public static readonly MaterialAmountUnits Liters = new("l");
    /// <summary>Amount measured in meters (m). See: PWG 5100.21-2019 Section 8.1.3.2</summary>
    public static readonly MaterialAmountUnits Meters = new("m");
    /// <summary>Amount measured in milliliters (ml). See: PWG 5100.21-2019 Section 8.1.3.2</summary>
    public static readonly MaterialAmountUnits Milliliters = new("ml");
    /// <summary>Amount measured in millimeters (mm). See: PWG 5100.21-2019 Section 8.1.3.2</summary>
    public static readonly MaterialAmountUnits Millimeters = new("mm");
    /// <summary>Amount measured in ounces (oz). See: PWG 5100.21-2019 Section 8.1.3.2</summary>
    public static readonly MaterialAmountUnits Ounces = new("oz");

    public override string ToString() => Value;
    public static implicit operator string(MaterialAmountUnits value) => value.Value;
    public static explicit operator MaterialAmountUnits(string value) => new(value);
}
