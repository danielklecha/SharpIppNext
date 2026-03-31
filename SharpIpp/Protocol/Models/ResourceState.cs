namespace SharpIpp.Protocol.Models;

/// <summary>
/// IPP Resource state values used in PWG 5100.22-2025 Section 7.9.11
/// </summary>
public enum ResourceState
{
    Pending = 3,
    Available = 4,
    Installed = 5,
    Canceled = 6,
    Aborted = 7,
}
