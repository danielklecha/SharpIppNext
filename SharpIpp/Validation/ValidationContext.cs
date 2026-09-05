using System.Text;

namespace SharpIpp.Validation;

/// <summary>
/// Describes the context in which a validation check is performed.
/// </summary>
public readonly struct ValidationContext
{
    private readonly Encoding? _encoding;
    private readonly string? _memberName;

    /// <summary>
    /// Gets the character encoding associated with the request or response.
    /// </summary>
    public Encoding Encoding => _encoding ?? Encoding.UTF8;

    /// <summary>
    /// Gets the name of the member being validated.
    /// </summary>
    public string MemberName => _memberName ?? string.Empty;

    public ValidationContext(Encoding? encoding = null, string? memberName = null)
    {
        _encoding = encoding;
        _memberName = memberName;
    }
}
