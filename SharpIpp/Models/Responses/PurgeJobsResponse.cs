using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Purge-Jobs Response
/// See: RFC 2911 Section 3.2.9
/// </summary>
[Obsolete("See RFC 8011 Section 4.2.9.")]
public class PurgeJobsResponse : IppResponse<OperationAttributes>
{
}
