namespace SharpIpp.Protocol.Models;

/// <summary>
/// Values for the <c>transmission-status</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.3.1.3 and Table 6.
/// </summary>
public enum TransmissionStatus
{
    Pending = 3,
    PendingRetry = 4,
    Processing = 5,
    Canceled = 7,
    Aborted = 8,
    Completed = 9,
}
