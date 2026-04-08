namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// IPP Power state values used in PWG 5100.22-2025 (Power extensions).
    /// Vendor-specific states can be represented by constructing a new instance.
    /// See: PWG5100.22 Sections 7.3.2, 7.3.4, 7.3.5, 7.3.1
    /// </summary>
    public readonly record struct PowerState(string Value, bool IsValue = true) : ISmartEnum
    {
        public static readonly PowerState Hibernate = new("hibernate");
        public static readonly PowerState OffHard = new("off-hard");
        public static readonly PowerState OffSoft = new("off-soft");
        public static readonly PowerState On = new("on");
        public static readonly PowerState Standby = new("standby");
        public static readonly PowerState Suspend = new("suspend");

        public override string ToString() => Value;
        public static implicit operator string(PowerState value) => value.Value;
        public static explicit operator PowerState(string value) => new(value);
    }
}
