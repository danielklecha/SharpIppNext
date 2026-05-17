namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>multiple-object-handling</c> attribute.
/// See: PWG 5100.21-2019 Section 8.1.4
/// </summary>
public readonly record struct MultipleObjectHandling(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The Printer automatically selects the object handling method.
    /// See: PWG 5100.21-2019 Section 8.1.4
    /// </summary>
    public static readonly MultipleObjectHandling Auto = new("auto");

    /// <summary>
    /// Only the first object is processed.
    /// See: PWG 5100.21-2019 Section 8.1.4
    /// </summary>
    public static readonly MultipleObjectHandling FirstOnly = new("first-only");

    /// <summary>
    /// Only the last object is processed.
    /// See: PWG 5100.21-2019 Section 8.1.4
    /// </summary>
    public static readonly MultipleObjectHandling LastOnly = new("last-only");

    /// <summary>
    /// All objects are processed, each with its own set of parts.
    /// See: PWG 5100.21-2019 Section 8.1.4
    /// </summary>
    public static readonly MultipleObjectHandling AllObjectsWithParts = new("all-objects-with-parts");

    /// <summary>
    /// Only the first object is processed, with its own set of parts.
    /// See: PWG 5100.21-2019 Section 8.1.4
    /// </summary>
    public static readonly MultipleObjectHandling FirstObjectWithParts = new("first-object-with-parts");

    public override string ToString() => Value;
    public static implicit operator string(MultipleObjectHandling value) => value.Value;
    public static explicit operator MultipleObjectHandling(string value) => new(value);
}
