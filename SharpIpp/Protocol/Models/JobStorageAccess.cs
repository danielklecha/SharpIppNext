namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-storage-access</c> member attribute.
/// See: PWG 5100.11-2024 Section 6.1.4.1
/// </summary>
public readonly record struct JobStorageAccess(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The stored job is accessible to members of the owner's group.
    /// See: PWG 5100.11-2024 Section 6.1.4.1
    /// </summary>
    public static readonly JobStorageAccess Group = new("group");

    /// <summary>
    /// No access control is applied to the stored job.
    /// See: PWG 5100.11-2024 Section 6.1.4.1
    /// </summary>
    public static readonly JobStorageAccess None = new("none");

    /// <summary>
    /// The stored job is protected by a PIN.
    /// See: PWG 5100.11-2024 Section 6.1.4.1
    /// </summary>
    public static readonly JobStorageAccess Pin = new("pin");

    /// <summary>
    /// The stored job is private (accessible only to the owner).
    /// See: PWG 5100.11-2024 Section 6.1.4.1
    /// </summary>
    public static readonly JobStorageAccess Private = new("private");

    /// <summary>
    /// The stored job is publicly accessible.
    /// See: PWG 5100.11-2024 Section 6.1.4.1
    /// </summary>
    public static readonly JobStorageAccess Public = new("public");

    /// <summary>
    /// The stored job is protected by a username and password.
    /// See: PWG 5100.11-2024 Section 6.1.4.1
    /// </summary>
    public static readonly JobStorageAccess UsernameAndPassword = new("username-and-password");

    public override string ToString() => Value;
    public static implicit operator string(JobStorageAccess value) => value.Value;
    public static implicit operator JobStorageAccess(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
