namespace SharpIpp.Protocol.Models
{
    public readonly record struct PrintBase(string Value, bool IsValue = true) : ISmartEnum
    {
        public static readonly PrintBase None = new("none");
        public static readonly PrintBase Brim = new("brim");
        public static readonly PrintBase Raft = new("raft");
        public static readonly PrintBase Skirt = new("skirt");
        public static readonly PrintBase Standard = new("standard");

        public override string ToString() => Value;
        public static implicit operator string(PrintBase bin) => bin.Value;
        public static explicit operator PrintBase(string value) => new(value);
    }
}
