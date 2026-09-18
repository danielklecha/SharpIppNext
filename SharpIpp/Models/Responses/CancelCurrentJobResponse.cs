using SharpIpp.Mapping;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Cancel-Current-Job Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse]
public class CancelCurrentJobResponse : IppResponse<OperationAttributes> { }
