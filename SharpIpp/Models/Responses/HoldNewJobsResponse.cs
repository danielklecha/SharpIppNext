using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Hold-New-Jobs Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse(IppOperation.HoldNewJobs)]
public class HoldNewJobsResponse : IppResponse<OperationAttributes> { }
