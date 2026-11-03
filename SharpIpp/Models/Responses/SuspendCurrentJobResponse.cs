using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Suspend-Current-Job Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse(IppOperation.SuspendCurrentJob)]
public class SuspendCurrentJobResponse : IppResponse<OperationAttributes> { }
