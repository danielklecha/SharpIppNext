namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known keyword values for <code>ipp-features-supported</code>.
/// See: RFC 8011 Section 5.4.39, PWG 5100.13-2023 Section 6.5.4,
/// PWG 5100.11-2024 Section 8.1, and PWG 5100.22-2025 Section 9.1.
/// </summary>
public readonly record struct IppFeature(string Value, bool IsValue = true) : ISmartEnum 
{
    // Core/driver-replacement feature keywords.
    public static readonly IppFeature None = new("none");
    public static readonly IppFeature DocumentObject = new("document-object");
    public static readonly IppFeature PageOverrides = new("page-overrides");
    public static readonly IppFeature Production = new("production");
    public static readonly IppFeature SubscriptionObject = new("subscription-object");

    // Enterprise Printing Extensions feature keywords.
    public static readonly IppFeature FaxOut = new("faxout");
    public static readonly IppFeature JobRelease = new("job-release");
    public static readonly IppFeature JobStorage = new("job-storage");
    public static readonly IppFeature PrintPolicy = new("print-policy");
    public static readonly IppFeature ProofAndSuspend = new("proof-and-suspend");
    public static readonly IppFeature ProofPrint = new("proof-print");
    public static readonly IppFeature SharedInfrastructure = new("shared-infrastructure");

    // System service feature keywords.
    public static readonly IppFeature ResourceObject = new("resource-object");
    public static readonly IppFeature SystemObject = new("system-object");

    public override string ToString() => Value;
    public static implicit operator string(IppFeature value) => value.Value;
    public static explicit operator IppFeature(string value) => new(value);
}