namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the job-resolvers-supported collection attribute.
/// Each element describes a resolver that can resolve job constraint conflicts.
/// See: PWG 5100.13-2023 Section 6.5.9
/// </summary>
public class JobResolversSupported : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// resolver-name — name of the resolver (name).
    /// See: PWG 5100.13-2023 Section 6.5.9
    /// </summary>
    public string? ResolverName { get; set; }
}
