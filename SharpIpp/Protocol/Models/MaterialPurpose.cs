namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-purpose</c> member attribute.
/// See: PWG 5100.21-2019 Section 6.8.11
/// </summary>
public readonly record struct MaterialPurpose(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly MaterialPurpose All = new("all");
    public static readonly MaterialPurpose Base = new("base");
    public static readonly MaterialPurpose Fill = new("fill");
    public static readonly MaterialPurpose Raft = new("raft");
    public static readonly MaterialPurpose Shell = new("shell");
    public static readonly MaterialPurpose SolubleSupport = new("soluble-support");
    public static readonly MaterialPurpose Support = new("support");

    public override string ToString() => Value;
    public static implicit operator string(MaterialPurpose value) => value.Value;
    public static explicit operator MaterialPurpose(string value) => new(value);
}
