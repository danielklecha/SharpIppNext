namespace SharpIpp.Protocol.Models;

/// <summary>
/// Standard values for the "client-type" member attribute of the "client-info" operation attribute.
/// See: PWG 5100.7-2023 Section 6.1.1
/// </summary>
public enum ClientType
{
    /// <summary>
    /// An application.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    Application = 3,

    /// <summary>
    /// An operating system.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    OperatingSystem = 4,

    /// <summary>
    /// A printer or other driver.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    Driver = 5,

    /// <summary>
    /// Another kind of software on the Client.
    /// See: PWG 5100.7-2023 Section 6.1.1
    /// </summary>
    Other = 6,
}
