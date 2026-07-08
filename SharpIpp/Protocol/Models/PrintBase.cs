namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the <c>print-base</c> attribute for 3D printing.
    /// See: PWG 5100.21-2019 Section 8.1.7
    /// </summary>
    public readonly record struct PrintBase(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>No base is printed. See: PWG 5100.21-2019 Section 8.1.7</summary>
        public static readonly PrintBase None = new("none");
        /// <summary>A brim (wide flat base) is printed around the object. See: PWG 5100.21-2019 Section 8.1.7</summary>
        public static readonly PrintBase Brim = new("brim");
        /// <summary>A raft (thick base layer) is printed under the object. See: PWG 5100.21-2019 Section 8.1.7</summary>
        public static readonly PrintBase Raft = new("raft");
        /// <summary>A skirt (outline around the object) is printed. See: PWG 5100.21-2019 Section 8.1.7</summary>
        public static readonly PrintBase Skirt = new("skirt");
        /// <summary>The standard base type for the printer. See: PWG 5100.21-2019 Section 8.1.7</summary>
        public static readonly PrintBase Standard = new("standard");

        public override string ToString() => Value;
        public static implicit operator string(PrintBase bin) => bin.Value;
        public static implicit operator PrintBase(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
    }
}
