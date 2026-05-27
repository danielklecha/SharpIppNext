using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>document-access</c> collection.
/// DEPRECATED.
/// See: PWG 5100.18-2025 Section 7.1.5
/// </summary>
[Obsolete("The 'document-access' attribute is deprecated in favor of URI authentication. See PWG 5100.18-2025 Section 7.1.5.")]
public class DocumentAccess : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public string? AccessOAuthToken { get; set; }
    public Uri? AccessOAuthUri { get; set; }
    public string? AccessPassword { get; set; }
    public string? AccessPin { get; set; }
    public string? AccessUserName { get; set; }
    public string? AccessX509Certificate { get; set; }
}
