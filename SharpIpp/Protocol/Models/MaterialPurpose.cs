namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-purpose</c> member attribute.
/// See: PWG 5100.21-2019 Section 8.1.3.10
/// </summary>
public readonly record struct MaterialPurpose(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The material is used for all purposes. See: PWG 5100.21-2019 Section 8.1.3.10</summary>
    public static readonly MaterialPurpose All = new("all");
    /// <summary>The material is used as the base layer. See: PWG 5100.21-2019 Section 8.1.3.10</summary>
    public static readonly MaterialPurpose Base = new("base");
    /// <summary>The material is used as fill material. See: PWG 5100.21-2019 Section 8.1.3.10</summary>
    public static readonly MaterialPurpose Fill = new("fill");
    /// <summary>The material is used for the raft (build platform adhesion). See: PWG 5100.21-2019 Section 8.1.3.10</summary>
    public static readonly MaterialPurpose Raft = new("raft");
    /// <summary>The material is used for the outer shell. See: PWG 5100.21-2019 Section 8.1.3.10</summary>
    public static readonly MaterialPurpose Shell = new("shell");
    /// <summary>The material is used as soluble support material. See: PWG 5100.21-2019 Section 8.1.3.10</summary>
    public static readonly MaterialPurpose SolubleSupport = new("soluble-support");
    /// <summary>The material is used as support material. See: PWG 5100.21-2019 Section 8.1.3.10</summary>
    public static readonly MaterialPurpose Support = new("support");

    public override string ToString() => Value;
    public static implicit operator string(MaterialPurpose value) => value.Value;
    public static implicit operator MaterialPurpose(string value) => new(value);
}
