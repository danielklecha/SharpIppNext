using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Restart-Job Response
/// <br/>
/// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as the Restart-Job operation (Deprecated in RFC 8011).
/// See: RFC 2911 Section 3.3.7
/// </summary>
public class RestartJobResponse : IppResponse<OperationAttributes>
{
}
