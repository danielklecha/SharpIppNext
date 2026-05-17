namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// IPP Power state values used in PWG 5100.22-2025 (Power extensions).
    /// Vendor-specific states can be represented by constructing a new instance.
    /// See: PWG5100.22 Sections 7.3.2, 7.3.4, 7.3.5, 7.3.1
    /// </summary>
    public readonly record struct PowerState(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// The system is in hibernate (deep sleep) state with minimal power consumption.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public static readonly PowerState Hibernate = new("hibernate");

        /// <summary>
        /// The system is powered off (hard power-off).
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public static readonly PowerState OffHard = new("off-hard");

        /// <summary>
        /// The system is powered off (soft power-off, e.g., via OS shutdown).
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public static readonly PowerState OffSoft = new("off-soft");

        /// <summary>
        /// The system is fully powered on and operational.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public static readonly PowerState On = new("on");

        /// <summary>
        /// The system is in standby (low-power) state.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public static readonly PowerState Standby = new("standby");

        /// <summary>
        /// The system is in suspend (sleep) state.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public static readonly PowerState Suspend = new("suspend");

        public override string ToString() => Value;
        public static implicit operator string(PowerState value) => value.Value;
        public static explicit operator PowerState(string value) => new(value);
    }
}
