using SharpIpp.Mapping;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

[IppResponse(IppOperation.ValidateJob)]
public class ValidateJobResponse : IppResponse<ValidateOperationAttributes>
{
}
