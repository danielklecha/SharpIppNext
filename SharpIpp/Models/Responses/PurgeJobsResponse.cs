using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Purge-Jobs Response
/// <br/>
/// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as the Purge-Jobs operation (Deprecated in RFC 8011).
/// See: RFC 2911 Section 3.2.9
/// </summary>
public class PurgeJobsResponse : IppResponse<OperationAttributes>
{
}
