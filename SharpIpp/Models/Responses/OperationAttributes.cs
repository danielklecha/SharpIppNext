using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

[IppAttribute]
public class OperationAttributes
{
    /// <summary>
    /// The Printer object OPTIONALLY returns this attribute. It contains a brief message describing the status of the operation
    /// See: RFC 8011 Section 4.1.6.2
    /// See: PWG 5100.18-2025 Section 5.7.2
    /// </summary>
    /// <code>status-message</code>
    [IppAttribute(IppAttributeNames.StatusMessage, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? StatusMessage { get; set; }

    /// <summary>
    /// The Printer object OPTIONALLY returns this attribute. It contains additional detailed and technical information about the operation
    /// See: RFC 8011 Section 4.1.6.3
    /// See: PWG 5100.18-2025 Section 5.7.2
    /// </summary>
    /// <code>detailed-status-message</code>
    [IppAttribute(IppAttributeNames.DetailedStatusMessage, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? DetailedStatusMessage { get; set; }

    /// <summary>
    /// The Printer object OPTIONALLY returns this attribute. It provides additional information about each document access error encountered by the Printer in a Print-URI or Send-URI operation
    /// See: RFC 8011 Section 4.1.6.4
    /// See: PWG 5100.18-2025 Section 4.2.5
    /// See: PWG 5100.18-2025 Section 5.8
    /// </summary>
    /// <code>document-access-error</code>
    [IppAttribute(IppAttributeNames.DocumentAccessError, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? DocumentAccessError { get; set; }

    /// <summary>
    /// The Printer object MUST return this attribute. It identifies the charset used by any 'name' and 'text' attributes that the Printer object is returning in this response. Defaults to "utf-8"
    /// See: RFC 8011 Section 5.3.19
    /// </summary>
    /// <code>attributes-charset</code>
    [IppAttribute(IppAttributeNames.AttributesCharset, Tag = Tag.Charset, Order = 0, DefaultValue = "utf-8")]
    public SharpIpp.Protocol.Models.Charset AttributesCharset { get; set; } = SharpIpp.Protocol.Models.Charset.Utf8;

    /// <summary>
    /// The Printer object MUST return this attribute. It identifies the natural language used by any 'name' and 'text' attributes that the Printer object is returning in this response. Defaults to "en"
    /// See: RFC 8011 Section 5.3.20
    /// </summary>
    /// <code>attributes-natural-language</code>
    [IppAttribute(IppAttributeNames.AttributesNaturalLanguage, Tag = Tag.NaturalLanguage, Order = 1, DefaultValue = "en")]
    public SharpIpp.Protocol.Models.NaturalLanguage? AttributesNaturalLanguage { get; set; } = SharpIpp.Protocol.Models.NaturalLanguage.En;
}
