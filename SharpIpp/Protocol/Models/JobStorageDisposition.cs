namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-storage-disposition</c> member attribute.
/// See: PWG 5100.11-2024 Section 6.1.4.2
/// </summary>
public readonly record struct JobStorageDisposition(string Value, bool IsKeyword = true, bool IsValue = true) : IKeywordSmartEnum
{
    public static readonly JobStorageDisposition PrintAndStore = new("print-and-store");
    public static readonly JobStorageDisposition StoreOnly = new("store-only");

    public override string ToString() => Value;
    public static implicit operator string(JobStorageDisposition value) => value.Value;
    public static explicit operator JobStorageDisposition(string value) => new(value);
}
