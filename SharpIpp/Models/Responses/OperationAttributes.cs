namespace SharpIpp.Models.Responses;

public class OperationAttributes
{
    /// <summary>
    /// The Printer object OPTIONALLY returns this attribute. It contains a brief message describing the status of the operation
    /// See: RFC 8011 Section 4.1.6.2
    /// See: PWG 5100.18-2025 Section 5.7.2
    /// </summary>
    /// <code>status-message</code>
    public string? StatusMessage { get; set; }
    /// <summary>
    /// The Printer object OPTIONALLY returns this attribute. It contains additional detailed and technical information about the operation
    /// See: RFC 8011 Section 4.1.6.3
    /// See: PWG 5100.18-2025 Section 5.7.2
    /// </summary>
    /// <code>detailed-status-message</code>
    public string[]? DetailedStatusMessage { get; set; }
    /// <summary>
    /// The Printer object OPTIONALLY returns this attribute. It provides additional information about each document access error encountered by the Printer in a Print-URI or Send-URI operation
    /// See: RFC 8011 Section 4.1.6.4
    /// See: PWG 5100.18-2025 Section 4.2.5
    /// See: PWG 5100.18-2025 Section 5.8
    /// </summary>
    /// <code>document-access-error</code>
    public string? DocumentAccessError { get; set; }
    /// <summary>
    /// The Printer object MUST return this attribute. It identifies the charset used by any 'name' and 'text' attributes that the Printer object is returning in this response. Defaults to "utf-8"
    /// See: RFC 8011 Section 5.3.19
    /// </summary>
    /// <code>attributes-charset</code>
    public string AttributesCharset { get; set; } = "utf-8";
    /// <summary>
    /// The Printer object MUST return this attribute. It identifies the natural language used by any 'name' and 'text' attributes that the Printer object is returning in this response. Defaults to "en"
    /// See: RFC 8011 Section 5.3.20
    /// </summary>
    /// <code>attributes-natural-language</code>
    public string? AttributesNaturalLanguage { get; set; } = "en";
}
