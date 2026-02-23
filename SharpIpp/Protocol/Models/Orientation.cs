namespace SharpIpp.Protocol.Models
{
    public enum Orientation
    {
        Unsupported,

        /// The content will be imaged across the short edge
        /// of the medium.
        Portrait = 3,

        /// The content will be imaged across the long edge
        /// of the medium.  Landscape is defined to be a rotation of
        /// the print-stream page to be imaged by +90 degrees with
        /// respect to the medium (i.e. anti-clockwise) from the
        /// portrait orientation.  Note:  The +90 direction was
        /// chosen because simple finishing on the long edge is the
        /// same edge whether portrait or landscape
        Landscape = 4,

        /// The content will be imaged across the
        /// long edge of the medium.  Reverse-landscape is defined to
        /// be a rotation of the print-stream page to be imaged by -
        /// 90 degrees with respect to the medium (i.e. clockwise)
        /// from the portrait orientation.  Note: The 'reverse-
        /// landscape' value was added because some applications
        /// rotate landscape -90 degrees from portrait, rather than
        /// +90 degrees.
        ReverseLandscape = 5,

        /// The content will be imaged across the
        /// short edge of the medium.  Reverse-portrait is defined to
        /// be a rotatio of the print-stream page to be imaged by
        /// 180 degrees with respect to the medium from the portrait
        /// orientation.  Note: The 'reverse-portrait' value was
        /// added for use with the "finishings" attribute in cases
        /// where the opposite edge is desired for finishing a
        /// portrait document on simple finishing devices that have
        /// only one finishing position.  Thus a 'text'/plain'
        /// portrait document can be stapled "on the right" by a
        /// simple finishing device as is common use with some middle
        /// eastern languages such as Hebrew.
        ReversePortrait = 6,
    }
}
