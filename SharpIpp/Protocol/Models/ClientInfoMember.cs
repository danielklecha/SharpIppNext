namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>client-info</code>.
/// See: PWG 5100.7-2019 Section 6.9.5
/// </summary>
public readonly record struct ClientInfoMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly ClientInfoMember ClientName = new("client-name");
    public static readonly ClientInfoMember ClientPatches = new("client-patches");
    public static readonly ClientInfoMember ClientStringVersion = new("client-string-version");
    public static readonly ClientInfoMember ClientType = new("client-type");
    public static readonly ClientInfoMember ClientVersion = new("client-version");

    public override string ToString() => Value;
    public static implicit operator string(ClientInfoMember value) => value.Value;
    public static explicit operator ClientInfoMember(string value) => new(value);
}
