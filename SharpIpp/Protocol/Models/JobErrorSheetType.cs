namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-error-sheet-type</c> member attribute values.
/// See: PWG 5100.3-2023 Section 5.2.9.1
/// </summary>
public readonly record struct JobErrorSheetType(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>'none': Do not print error information.</summary>
    public static readonly JobErrorSheetType None = new("none");
    /// <summary>'standard': Use the standard site or vendor defined error template.</summary>
    public static readonly JobErrorSheetType Standard = new("standard");

    public override string ToString() => Value;
    public static implicit operator string(JobErrorSheetType v) => v.Value;
    public static implicit operator JobErrorSheetType(string value) => new(value);
}
