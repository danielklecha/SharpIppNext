namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies when Media Sheets are baled.
/// See: PWG 5100.1-2022 Section 5.2.1.2
/// </summary>
public readonly record struct BalingWhen(string Value)
{
    public static readonly BalingWhen AfterJob = new("after-job");
    public static readonly BalingWhen AfterSets = new("after-sets");

    public override string ToString() => Value;
    public static implicit operator string(BalingWhen bin) => bin.Value;
    public static explicit operator BalingWhen(string value) => new(value);
}
