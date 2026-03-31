namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-error-sheet-when</c> member values.
/// See: PWG 5100.3-2023 Section 5.2.9.2
/// </summary>
public readonly record struct JobErrorSheetWhen(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly JobErrorSheetWhen Always = new("always");
    public static readonly JobErrorSheetWhen OnError = new("on-error");

    public override string ToString() => Value;
    public static implicit operator string(JobErrorSheetWhen bin) => bin.Value;
    public static explicit operator JobErrorSheetWhen(string value) => new(value);
}
