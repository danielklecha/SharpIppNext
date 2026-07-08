namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-error-sheet-when</c> member values.
/// See: PWG 5100.3-2023 Section 5.2.9.2
/// </summary>
public readonly record struct JobErrorSheetWhen(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// An error sheet is always printed at the end of the job.
    /// See: PWG 5100.3-2023 Section 5.2.9.2
    /// </summary>
    public static readonly JobErrorSheetWhen Always = new("always");

    /// <summary>
    /// An error sheet is printed only when an error occurs.
    /// See: PWG 5100.3-2023 Section 5.2.9.2
    /// </summary>
    public static readonly JobErrorSheetWhen OnError = new("on-error");

    public override string ToString() => Value;
    public static implicit operator string(JobErrorSheetWhen bin) => bin.Value;
    public static implicit operator JobErrorSheetWhen(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
