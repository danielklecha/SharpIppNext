namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>print-objects</code>.
/// See: PWG 5100.21-2019 Section 8.3.30
/// </summary>
public readonly record struct PrintObjectsMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The print object is a document. See: PWG 5100.21-2019 Section 8.1.25</summary>
    public static readonly PrintObjectsMember Document = new("document");
    /// <summary>The print object is a job. See: PWG 5100.21-2019 Section 8.1.25</summary>
    public static readonly PrintObjectsMember Job = new("job");
    /// <summary>No print object is specified. See: PWG 5100.21-2019 Section 8.1.25</summary>
    public static readonly PrintObjectsMember None = new("none");

    public override string ToString() => Value;
    public static implicit operator string(PrintObjectsMember value) => value.Value;
    public static explicit operator PrintObjectsMember(string value) => new(value);
}
