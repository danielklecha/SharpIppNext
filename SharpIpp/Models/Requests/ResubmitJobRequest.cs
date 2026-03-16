using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Resubmit-Job Operation.
/// See: PWG 5100.7-2023 Section 5.3
/// </summary>
public class ResubmitJobRequest : IppRequest<ResubmitJobOperationAttributes>, IIppPrinterRequest
{
    /// <summary>
    /// Job Template Attributes for the resubmitted Job.
    /// See: PWG 5100.7-2023 Section 5.3.1
    /// </summary>
    /// <code>job-template-attributes</code>
    public JobTemplateAttributes? JobTemplateAttributes { get; set; }
}
