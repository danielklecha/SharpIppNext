namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>print-objects</code>.
/// See: PWG 5100.21-2019 Section 8.1.25
/// </summary>
public readonly record struct PrintObjectsMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly PrintObjectsMember Document = new("document");
    public static readonly PrintObjectsMember Job = new("job");
    public static readonly PrintObjectsMember None = new("none");

    public override string ToString() => Value;
    public static implicit operator string(PrintObjectsMember value) => value.Value;
    public static explicit operator PrintObjectsMember(string value) => new(value);
}
