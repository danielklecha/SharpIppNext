namespace SharpIpp.Protocol.Models;

/// <summary>
/// This attribute specifies whether the Printer collates output sheets when producing multiple copies of a document.
/// See: PWG 5100.8-2003 Section 3
/// </summary>
public readonly record struct SheetCollate(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly SheetCollate Collated = new("collated");
    public static readonly SheetCollate Uncollated = new("uncollated");

    public override string ToString() => Value;
    public static implicit operator string(SheetCollate collate) => collate.Value;
    public static implicit operator SheetCollate(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
