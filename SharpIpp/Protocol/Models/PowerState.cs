namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// IPP Power state values used in PWG 5100.22-2025 (Power extensions).
    /// See: PWG5100.22 Sections 7.3.2, 7.3.4, 7.3.5, 7.3.1
    /// </summary>
    public enum PowerState
    {
        Hibernate,
        OffHard,
        OffSoft,
        On,
        Standby,
        Suspend,

        // The spec allows vendor-specific states (e.g. 'low-on', ephemeral transitions);
        // unknown values can be represented through this sentinel if needed.
        Unknown
    }
}
