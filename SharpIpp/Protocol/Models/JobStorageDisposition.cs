namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-storage-disposition</c> member attribute.
/// See: PWG 5100.11-2024 Section 6.1.4.2
/// </summary>
public readonly record struct JobStorageDisposition(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The job is printed and then stored.
    /// See: PWG 5100.11-2024 Section 6.1.4.2
    /// </summary>
    public static readonly JobStorageDisposition PrintAndStore = new("print-and-store");

    /// <summary>
    /// The job is stored without printing.
    /// See: PWG 5100.11-2024 Section 6.1.4.2
    /// </summary>
    public static readonly JobStorageDisposition StoreOnly = new("store-only");

    public override string ToString() => Value;
    public static implicit operator string(JobStorageDisposition value) => value.Value;
    public static explicit operator JobStorageDisposition(string value) => new(value);
}
