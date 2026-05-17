using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// PWG 5100.5-2024 Section 5.2.1.2
/// </summary>
public class GetDocumentsResponse : IppResponse<OperationAttributes>
{
    public List<DocumentAttributes> Documents { get; set; } = new();
}
