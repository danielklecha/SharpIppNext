using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Specialized operation attributes for job and document validation responses.
/// </summary>
public class ValidateOperationAttributes : OperationAttributes
{
    /// <summary>
    /// Recommended Printer preferred Job Template attributes returned when conflicts are detected.
    /// See: PWG 5100.13-2023 Section 6.1.5
    /// </summary>
    public JobTemplateAttributes? PreferredAttributes { get; set; }
}
