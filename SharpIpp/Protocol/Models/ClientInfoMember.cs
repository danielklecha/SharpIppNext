namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>client-info</code>.
/// See: PWG 5100.7-2023 Section 6.1.1
/// </summary>
public readonly record struct ClientInfoMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The name of the client application or software.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    public static readonly ClientInfoMember ClientName = new("client-name");

    /// <summary>
    /// The patches applied to the client software.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    public static readonly ClientInfoMember ClientPatches = new("client-patches");

    /// <summary>
    /// The human-readable version string of the client software.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    public static readonly ClientInfoMember ClientStringVersion = new("client-string-version");

    /// <summary>
    /// The type of the client (e.g., application, driver, operating system).
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    public static readonly ClientInfoMember ClientType = new("client-type");

    /// <summary>
    /// The machine-readable version of the client software.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    public static readonly ClientInfoMember ClientVersion = new("client-version");

    public override string ToString() => Value;
    public static implicit operator string(ClientInfoMember value) => value.Value;
    public static explicit operator ClientInfoMember(string value) => new(value);
}
