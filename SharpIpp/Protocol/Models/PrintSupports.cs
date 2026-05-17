namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the <c>print-supports</c> attribute for 3D printing.
    /// See: PWG 5100.21-2019 Section 8.1.9
    /// </summary>
    public readonly record struct PrintSupports(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// No support structures are printed.
        /// See: PWG 5100.21-2019 Section 8.1.9
        /// </summary>
        public static readonly PrintSupports None = new("none");
        /// <summary>
        /// Standard support structures are printed.
        /// See: PWG 5100.21-2019 Section 8.1.9
        /// </summary>
        public static readonly PrintSupports Standard = new("standard");
        /// <summary>
        /// Support structures are printed using a separate support material.
        /// See: PWG 5100.21-2019 Section 8.1.9
        /// </summary>
        public static readonly PrintSupports Material = new("material");

        public override string ToString() => Value;
        public static implicit operator string(PrintSupports bin) => bin.Value;
        public static explicit operator PrintSupports(string value) => new(value);
    }
}
