namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-amount-units</c> attribute.
/// See: PWG 5100.21-2019 Section 6.8.11
/// </summary>
public readonly record struct MaterialAmountUnits(string Value, bool IsKeyword = true, bool IsValue = true) : IKeywordSmartEnum
{
    public static readonly MaterialAmountUnits Grams = new("g");
    public static readonly MaterialAmountUnits Kilograms = new("kg");
    public static readonly MaterialAmountUnits Liters = new("l");
    public static readonly MaterialAmountUnits Meters = new("m");
    public static readonly MaterialAmountUnits Milliliters = new("ml");
    public static readonly MaterialAmountUnits Millimeters = new("mm");
    public static readonly MaterialAmountUnits Ounces = new("oz");

    public override string ToString() => Value;
    public static implicit operator string(MaterialAmountUnits value) => value.Value;
    public static explicit operator MaterialAmountUnits(string value) => new(value);
}
