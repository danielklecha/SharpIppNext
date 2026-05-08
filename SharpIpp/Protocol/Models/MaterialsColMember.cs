namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>materials-col</code>.
/// See: PWG 5100.21-2019 Section 8.1.18
/// </summary>
public readonly record struct MaterialsColMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly MaterialsColMember MaterialAmount = new("material-amount");
    public static readonly MaterialsColMember MaterialColor = new("material-color");
    public static readonly MaterialsColMember MaterialKey = new("material-key");
    public static readonly MaterialsColMember MaterialName = new("material-name");
    public static readonly MaterialsColMember MaterialPurpose = new("material-purpose");
    public static readonly MaterialsColMember MaterialRate = new("material-rate");
    public static readonly MaterialsColMember MaterialRateUnits = new("material-rate-units");
    public static readonly MaterialsColMember MaterialType = new("material-type");

    public override string ToString() => Value;
    public static implicit operator string(MaterialsColMember value) => value.Value;
    public static explicit operator MaterialsColMember(string value) => new(value);
}
