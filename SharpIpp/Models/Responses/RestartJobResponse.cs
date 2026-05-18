using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Restart-Job Response
/// See: RFC 2911 Section 3.3.7
/// </summary>
[Obsolete("See RFC 8011 Section 4.3.7.")]
public class RestartJobResponse : IppResponse<OperationAttributes>
{
}
