namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>materials-col</code>.
/// See: PWG 5100.21-2019 Section 8.1.18
/// </summary>
public readonly record struct MaterialsColMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The material-amount member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialAmount = new("material-amount");
    /// <summary>The material-color member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialColor = new("material-color");
    /// <summary>The material-key member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialKey = new("material-key");
    /// <summary>The material-name member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialName = new("material-name");
    /// <summary>The material-purpose member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialPurpose = new("material-purpose");
    /// <summary>The material-rate member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialRate = new("material-rate");
    /// <summary>The material-rate-units member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialRateUnits = new("material-rate-units");
    /// <summary>The material-type member attribute. See: PWG 5100.21-2019 Section 8.1.18</summary>
    public static readonly MaterialsColMember MaterialType = new("material-type");

    public override string ToString() => Value;
    public static implicit operator string(MaterialsColMember value) => value.Value;
    public static explicit operator MaterialsColMember(string value) => new(value);
}
