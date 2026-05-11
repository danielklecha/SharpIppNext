namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-accounting-sheets-type</c> member attribute values.
/// See: PWG 5100.3-2023 Section 5.2.6.1
/// </summary>
public readonly record struct JobAccountingSheetsType(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>'none': Suppress printing of accounting sheets.</summary>
    public static readonly JobAccountingSheetsType None = new("none");
    /// <summary>'standard': Use the standard site accounting sheets.</summary>
    public static readonly JobAccountingSheetsType Standard = new("standard");

    public override string ToString() => Value;
    public static implicit operator string(JobAccountingSheetsType v) => v.Value;
    public static explicit operator JobAccountingSheetsType(string value) => new(value);
}
