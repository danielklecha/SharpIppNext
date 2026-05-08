namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-storage-access</c> member attribute.
/// See: PWG 5100.11-2024 Section 6.1.4.1
/// </summary>
public readonly record struct JobStorageAccess(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly JobStorageAccess Group = new("group");
    public static readonly JobStorageAccess None = new("none");
    public static readonly JobStorageAccess Pin = new("pin");
    public static readonly JobStorageAccess Private = new("private");
    public static readonly JobStorageAccess Public = new("public");
    public static readonly JobStorageAccess UsernameAndPassword = new("username-and-password");

    public override string ToString() => Value;
    public static implicit operator string(JobStorageAccess value) => value.Value;
    public static explicit operator JobStorageAccess(string value) => new(value);
}
