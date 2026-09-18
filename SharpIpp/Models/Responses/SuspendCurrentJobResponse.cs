using SharpIpp.Mapping;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Suspend-Current-Job Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse]
public class SuspendCurrentJobResponse : IppResponse<OperationAttributes> { }
