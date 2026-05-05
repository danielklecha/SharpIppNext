namespace SharpIpp.Protocol.Models
{
    public readonly record struct PrintSupports(string Value, bool IsValue = true) : ISmartEnum
    {
        public static readonly PrintSupports None = new("none");
        public static readonly PrintSupports Standard = new("standard");
        public static readonly PrintSupports Material = new("material");

        public override string ToString() => Value;
        public static implicit operator string(PrintSupports bin) => bin.Value;
        public static explicit operator PrintSupports(string value) => new(value);
    }
}
