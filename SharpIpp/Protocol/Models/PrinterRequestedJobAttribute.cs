namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>printer-requested-job-attributes</code>.
/// See: RFC 8011 Section 5.4.43
/// </summary>
public readonly record struct PrinterRequestedJobAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The job-id attribute. See: RFC 8011 Section 5.4.43</summary>
    public static readonly PrinterRequestedJobAttribute JobId = new("job-id");
    /// <summary>The job-name attribute. See: RFC 8011 Section 5.4.43</summary>
    public static readonly PrinterRequestedJobAttribute JobName = new("job-name");
    /// <summary>The job-state attribute. See: RFC 8011 Section 5.4.43</summary>
    public static readonly PrinterRequestedJobAttribute JobState = new("job-state");
    /// <summary>The job-uri attribute. See: RFC 8011 Section 5.4.43</summary>
    public static readonly PrinterRequestedJobAttribute JobUri = new("job-uri");

    public override string ToString() => Value;
    public static implicit operator string(PrinterRequestedJobAttribute value) => value.Value;
    public static explicit operator PrinterRequestedJobAttribute(string value) => new(value);
}
