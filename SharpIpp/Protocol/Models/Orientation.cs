namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// IPP orientation-requested attribute values.
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// The content will be imaged across the short edge of the medium.
        /// See: RFC 8011 Section 5.2.11
        /// </summary>
        Portrait = 3,

        /// <summary>
        /// The content will be imaged across the long edge of the medium.
        /// Landscape is defined to be a rotation of the print-stream page to be imaged by +90 degrees
        /// with respect to the medium (i.e. anti-clockwise) from the portrait orientation.
        /// See: RFC 8011 Section 5.2.11
        /// </summary>
        Landscape = 4,

        /// <summary>
        /// The content will be imaged across the long edge of the medium.
        /// Reverse-landscape is defined to be a rotation of the print-stream page to be imaged by
        /// -90 degrees with respect to the medium (i.e. clockwise) from the portrait orientation.
        /// See: RFC 8011 Section 5.2.11
        /// </summary>
        ReverseLandscape = 5,

        /// <summary>
        /// The content will be imaged across the short edge of the medium.
        /// Reverse-portrait is defined to be a rotation of the print-stream page to be imaged by
        /// 180 degrees with respect to the medium from the portrait orientation.
        /// See: RFC 8011 Section 5.2.11
        /// </summary>
        ReversePortrait = 6,
    }
}
