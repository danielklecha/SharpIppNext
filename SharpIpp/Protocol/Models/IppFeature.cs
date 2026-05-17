namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known keyword values for <code>ipp-features-supported</code>.
/// See: RFC 8011 Section 5.4.39, PWG 5100.13-2023 Section 6.5.4,
/// PWG 5100.11-2024 Section 8.1, and PWG 5100.22-2025 Section 9.1.
/// </summary>
public readonly record struct IppFeature(string Value, bool IsValue = true) : ISmartEnum 
{
    // Core/driver-replacement feature keywords.

    /// <summary>
    /// No specific IPP features are required.
    /// See: RFC 8011 Section 5.4.39
    /// </summary>
    public static readonly IppFeature None = new("none");

    /// <summary>
    /// The Printer supports the Document Object extension.
    /// See: PWG 5100.5-2024
    /// </summary>
    public static readonly IppFeature DocumentObject = new("document-object");

    /// <summary>
    /// The Printer supports page overrides.
    /// See: PWG 5100.6-2003
    /// </summary>
    public static readonly IppFeature PageOverrides = new("page-overrides");

    /// <summary>
    /// The Printer supports production printing features.
    /// See: PWG 5100.3-2023
    /// </summary>
    public static readonly IppFeature Production = new("production");

    /// <summary>
    /// The Printer supports the Subscription Object extension.
    /// See: RFC 3995
    /// </summary>
    public static readonly IppFeature SubscriptionObject = new("subscription-object");

    // Enterprise Printing Extensions feature keywords.

    /// <summary>
    /// The Printer supports fax output (FaxOut) operations.
    /// See: PWG 5100.8-2003
    /// </summary>
    public static readonly IppFeature FaxOut = new("faxout");

    /// <summary>
    /// The Printer supports job release (hold-and-release) operations.
    /// See: PWG 5100.11-2024
    /// </summary>
    public static readonly IppFeature JobRelease = new("job-release");

    /// <summary>
    /// The Printer supports job storage operations.
    /// See: PWG 5100.11-2024
    /// </summary>
    public static readonly IppFeature JobStorage = new("job-storage");

    /// <summary>
    /// The Printer supports print policy enforcement.
    /// See: PWG 5100.11-2024
    /// </summary>
    public static readonly IppFeature PrintPolicy = new("print-policy");

    /// <summary>
    /// The Printer supports proof-and-suspend operations.
    /// See: PWG 5100.11-2024
    /// </summary>
    public static readonly IppFeature ProofAndSuspend = new("proof-and-suspend");

    /// <summary>
    /// The Printer supports proof-print operations.
    /// See: PWG 5100.11-2024
    /// </summary>
    public static readonly IppFeature ProofPrint = new("proof-print");

    /// <summary>
    /// The Printer supports shared infrastructure (Infrastructure Printer) operations.
    /// See: PWG 5100.18-2025
    /// </summary>
    public static readonly IppFeature SharedInfrastructure = new("shared-infrastructure");

    // System service feature keywords.

    /// <summary>
    /// The Printer supports the Resource Object extension.
    /// See: PWG 5100.22-2025
    /// </summary>
    public static readonly IppFeature ResourceObject = new("resource-object");

    /// <summary>
    /// The Printer supports the System Object extension.
    /// See: PWG 5100.22-2025
    /// </summary>
    public static readonly IppFeature SystemObject = new("system-object");

    public override string ToString() => Value;
    public static implicit operator string(IppFeature value) => value.Value;
    public static explicit operator IppFeature(string value) => new(value);
}
