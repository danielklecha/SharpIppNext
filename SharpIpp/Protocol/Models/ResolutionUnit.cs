namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Units for printer resolution values.
    /// See: RFC 8011 Section 5.1.16
    /// </summary>
    public enum ResolutionUnit
    {
        /// <summary>
        /// Dots per inch.
        /// See: RFC 8011 Section 5.1.16
        /// </summary>
        DotsPerInch = 3,

        /// <summary>
        /// Dots per centimeter.
        /// See: RFC 8011 Section 5.1.16
        /// </summary>
        DotsPerCm = 4,
    }
}
