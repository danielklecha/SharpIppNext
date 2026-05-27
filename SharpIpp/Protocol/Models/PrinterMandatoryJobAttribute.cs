namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>printer-mandatory-job-attributes</code>.
/// See: RFC 8011 Section 5.4.42
/// </summary>
public readonly record struct PrinterMandatoryJobAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The attributes-natural-language attribute. See: RFC 8011 Section 5.4.42</summary>
    public static readonly PrinterMandatoryJobAttribute AttributesNaturalLanguage = new("attributes-natural-language");
    /// <summary>The document-format attribute. See: RFC 8011 Section 5.4.42</summary>
    public static readonly PrinterMandatoryJobAttribute DocumentFormat = new("document-format");
    /// <summary>The job-name attribute. See: RFC 8011 Section 5.4.42</summary>
    public static readonly PrinterMandatoryJobAttribute JobName = new("job-name");
    /// <summary>The printer-uri attribute. See: RFC 8011 Section 5.4.42</summary>
    public static readonly PrinterMandatoryJobAttribute PrinterUri = new("printer-uri");

    public override string ToString() => Value;
    public static implicit operator string(PrinterMandatoryJobAttribute value) => value.Value;
    public static implicit operator PrinterMandatoryJobAttribute(string value) => new(value);
}
