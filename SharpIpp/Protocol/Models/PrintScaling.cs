namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies how the Printer scales the document to fit the selected media.
    /// See: PWG 5100.13-2023 Section 6.2.28
    /// </summary>
    public readonly record struct PrintScaling(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// The Printer automatically selects the scaling method based on the document content and media.
        /// See: PWG 5100.13-2023 Section 6.2.28
        /// </summary>
        public static readonly PrintScaling Auto = new("auto");

        /// <summary>
        /// The Printer automatically scales the document to fit the media without cropping.
        /// See: PWG 5100.13-2023 Section 6.2.28
        /// </summary>
        public static readonly PrintScaling AutoFit = new("auto-fit");

        /// <summary>
        /// The Printer scales the document to fill the media, possibly cropping the edges.
        /// See: PWG 5100.13-2023 Section 6.2.28
        /// </summary>
        public static readonly PrintScaling Fill = new("fill");

        /// <summary>
        /// The Printer scales the document to fit within the media without cropping.
        /// See: PWG 5100.13-2023 Section 6.2.28
        /// </summary>
        public static readonly PrintScaling Fit = new("fit");

        /// <summary>
        /// The Printer does not scale the document; it is printed at its natural size.
        /// See: PWG 5100.13-2023 Section 6.2.28
        /// </summary>
        public static readonly PrintScaling None = new("none");

        public override string ToString() => Value;
        public static implicit operator string(PrintScaling bin) => bin.Value;
        public static implicit operator PrintScaling(string value) => new(value);
    }
}
