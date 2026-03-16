using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Set-Job-Attributes operation request.
/// </summary>
public class SetJobAttributesRequest : IppRequest<SetJobAttributesOperationAttributes>, IIppJobRequest
{
    /// <summary>
    /// Job Template attributes to set for the target Job.
    /// </summary>
    public JobTemplateAttributes? JobTemplateAttributes { get; set; }
}
