namespace SharpIpp.Protocol.Models;

/// <summary>
/// IPP Resource state values used in PWG 5100.22-2025 Section 7.9.11
/// </summary>
public enum ResourceState
{
    /// <summary>
    /// The resource has been created but is not yet available for use.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    Pending = 3,
    /// <summary>
    /// The resource is available for allocation to printers.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    Available = 4,
    /// <summary>
    /// The resource has been installed and is in use.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    Installed = 5,
    /// <summary>
    /// The resource has been canceled and is no longer available.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    Canceled = 6,
    /// <summary>
    /// The resource has been aborted by the system.
    /// See: PWG 5100.22-2025 Section 7.9.11
    /// </summary>
    Aborted = 7,
}
