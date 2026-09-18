using SharpIpp.Mapping;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Hold-New-Jobs Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse]
public class HoldNewJobsResponse : IppResponse<OperationAttributes> { }
