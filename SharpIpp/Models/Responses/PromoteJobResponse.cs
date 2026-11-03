using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Promote-Job Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse(IppOperation.PromoteJob)]
public class PromoteJobResponse : IppResponse<OperationAttributes> { }
