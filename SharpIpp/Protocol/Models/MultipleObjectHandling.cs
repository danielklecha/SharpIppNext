namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>multiple-object-handling</c> attribute.
/// See: PWG 5100.21-2019 Section 6.8.12
/// </summary>
public readonly record struct MultipleObjectHandling(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly MultipleObjectHandling Auto = new("auto");
    public static readonly MultipleObjectHandling FirstOnly = new("first-only");
    public static readonly MultipleObjectHandling LastOnly = new("last-only");
    public static readonly MultipleObjectHandling AllObjectsWithParts = new("all-objects-with-parts");
    public static readonly MultipleObjectHandling FirstObjectWithParts = new("first-object-with-parts");

    public override string ToString() => Value;
    public static implicit operator string(MultipleObjectHandling value) => value.Value;
    public static explicit operator MultipleObjectHandling(string value) => new(value);
}
