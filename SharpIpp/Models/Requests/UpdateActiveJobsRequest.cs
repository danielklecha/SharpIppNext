using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Active-Jobs operation.
/// See: PWG 5100.18-2025 Section 5.7
/// </summary>
[IppRequest(IppOperation.UpdateActiveJobs)]
public class UpdateActiveJobsRequest : IppRequest<UpdateActiveJobsOperationAttributes>, IIppPrinterRequest
{
}
