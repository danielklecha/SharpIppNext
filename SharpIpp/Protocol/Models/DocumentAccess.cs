namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>document-access</c> collection.
/// See: PWG 5100.18-2025 Section 7.1.5
/// </summary>
public class DocumentAccess : IIppCollection
{
    public bool IsValue { get; set; } = true;
    public string? AccessOAuthToken { get; set; }
    public string? AccessOAuthUri { get; set; }
    public string? AccessPassword { get; set; }
    public string? AccessPin { get; set; }
    public string? AccessUserName { get; set; }
    public string? AccessX509Certificate { get; set; }
}
