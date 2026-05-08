namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>printer-requested-job-attributes</code>.
/// See: RFC 8011 Section 5.4.43
/// </summary>
public readonly record struct PrinterRequestedJobAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly PrinterRequestedJobAttribute JobId = new("job-id");
    public static readonly PrinterRequestedJobAttribute JobName = new("job-name");
    public static readonly PrinterRequestedJobAttribute JobState = new("job-state");
    public static readonly PrinterRequestedJobAttribute JobUri = new("job-uri");

    public override string ToString() => Value;
    public static implicit operator string(PrinterRequestedJobAttribute value) => value.Value;
    public static explicit operator PrinterRequestedJobAttribute(string value) => new(value);
}
