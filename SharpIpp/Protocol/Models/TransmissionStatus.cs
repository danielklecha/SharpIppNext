namespace SharpIpp.Protocol.Models;

/// <summary>
/// Values for the <c>transmission-status</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.3.1.3 and Table 6.
/// </summary>
public enum TransmissionStatus
{
    /// <summary>
    /// The transmission is pending and has not yet started.
    /// See: PWG 5100.15-2013 Section 7.3.1.3
    /// </summary>
    Pending = 3,

    /// <summary>
    /// The transmission is pending a retry after a previous failure.
    /// See: PWG 5100.15-2013 Section 7.3.1.3
    /// </summary>
    PendingRetry = 4,

    /// <summary>
    /// The transmission is currently in progress.
    /// See: PWG 5100.15-2013 Section 7.3.1.3
    /// </summary>
    Processing = 5,

    /// <summary>
    /// The transmission was canceled before completion.
    /// See: PWG 5100.15-2013 Section 7.3.1.3
    /// </summary>
    Canceled = 7,

    /// <summary>
    /// The transmission was aborted by the system.
    /// See: PWG 5100.15-2013 Section 7.3.1.3
    /// </summary>
    Aborted = 8,

    /// <summary>
    /// The transmission completed successfully.
    /// See: PWG 5100.15-2013 Section 7.3.1.3
    /// </summary>
    Completed = 9,
}
