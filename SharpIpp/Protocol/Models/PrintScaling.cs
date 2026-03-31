namespace SharpIpp.Protocol.Models
{
    public readonly record struct PrintScaling(string Value, bool IsValue = true) : ISmartEnum
    {
        public static readonly PrintScaling Auto = new("auto");
        public static readonly PrintScaling AutoFit = new("auto-fit");
        public static readonly PrintScaling Fill = new("fill");
        public static readonly PrintScaling Fit = new("fit");
        public static readonly PrintScaling None = new("none");

        public override string ToString() => Value;
        public static implicit operator string(PrintScaling bin) => bin.Value;
        public static explicit operator PrintScaling(string value) => new(value);
    }
}
