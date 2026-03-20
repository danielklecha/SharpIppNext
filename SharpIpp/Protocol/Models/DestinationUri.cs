namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>destination-uris</c> member collection.
/// See: PWG 5100.15-2013 Section 7.4.10
/// </summary>
public class DestinationUri : IIppCollection
{
    public bool IsNoValue { get; set; }
    public string? DestinationUriValue { get; set; }
    public string? PostDialString { get; set; }
    public string? PreDialString { get; set; }
    public string? T33Subaddress { get; set; }
}
