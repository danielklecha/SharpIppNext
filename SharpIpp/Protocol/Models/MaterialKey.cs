namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-key</c> member attribute (vendor-extensible keyword).
/// See: PWG 5100.21-2019 Section 6.8.11
/// </summary>
public readonly record struct MaterialKey(string Value, bool IsKeyword = true, bool IsValue = true) : IKeywordSmartEnum
{
    public override string ToString() => Value;
    public static implicit operator string(MaterialKey value) => value.Value;
    public static explicit operator MaterialKey(string value) => new(value);
}
