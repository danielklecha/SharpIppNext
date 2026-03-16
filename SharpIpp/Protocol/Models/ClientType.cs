namespace SharpIpp.Protocol.Models;

/// <summary>
/// Standard values for the "client-type" member attribute of the "client-info" operation attribute.
/// See: PWG 5100.7-2023 Section 6.1.1.4
/// </summary>
public enum ClientType
{
    /// <summary>
    /// An application.
    /// </summary>
    Application = 3,

    /// <summary>
    /// An operating system.
    /// </summary>
    OperatingSystem = 4,

    /// <summary>
    /// A printer or other driver.
    /// </summary>
    Driver = 5,

    /// <summary>
    /// Another kind of software on the Client.
    /// </summary>
    Other = 6,
}
