namespace SharpIpp.Protocol.Models
{
    public enum Sides
    {

        /// 'one-sided': imposes each consecutive print-stream page upon the
        /// same side of consecutive media sheets.
        OneSided,

        /// 'two-sided-long-edge': imposes each consecutive pair of print-
        /// stream pages upon front and back sides of consecutive media
        /// sheets, such that the orientation of each pair of print-stream
        /// pages on the medium would be correct for the reader as if for
        /// binding on the long edge.  This imposition is sometimes called
        /// 'duplex' or 'head-to-head'.
        TwoSidedLongEdge,

        /// 'two-sided-short-edge': imposes each consecutive pair of print-
        /// stream pages upon front and back sides of consecutive media
        /// sheets, such that the orientation of each pair of print-stream
        /// pages on the medium would be correct for the reader as if for
        /// binding on the short edge.
        TwoSidedShortEdge,
    }
}
