namespace SharpIpp.Protocol.Models
{
    public enum Finishings
    {
        /// <summary>
        ///     Perform no finishing
        /// </summary>
        None = 3,

        /// <summary>
        ///     Bind the document(s) with one or more staples. The
        ///     exact number and placement of the staples is site-
        ///     defined.
        /// </summary>
        Staple = 4,

        /// <summary>
        ///     This value indicates that holes are required in the
        ///     finished document. The exact number and placement of the
        ///     holes is site-defined  The punch specification MAY be
        ///     satisfied (in a site- and implementation-specific manner)
        ///     either by drilling/punching, or by substituting pre-
        ///     drilled media.
        /// </summary>
        Punch = 5,

        /// <summary>
        ///     This value is specified when it is desired to select
        ///     a non-printed (or pre-printed) cover for the document.
        ///     This does not supplant the specification of a printed
        ///     cover (on cover stock medium) by the document itself.
        /// </summary>
        Cover = 6,

        /// <summary>
        ///     This value indicates that a binding is to be applied
        ///     to the document; the type and placement of the binding is
        ///     site-defined.
        /// </summary>
        Bind = 7,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples (wire stitches) along the middle fold.  The exact
        ///     number and placement of the staples and the middle fold
        ///     is implementation and/or site-defined.
        /// </summary>
        SaddleStitch = 8,

        /// <summary>
        ///     Bind the document(s) with one or more staples
        ///     (wire stitches) along one edge.  The exact number and
        ///     placement of the staples is implementation and/or site-
        ///     defined.
        /// </summary>
        EdgeStitch = 9,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples in the top left corner.
        /// </summary>
        StapleTopLeft = 20,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples in the bottom left corner.
        /// </summary>
        StapleBottomLeft = 21,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples in the top right corner.
        /// </summary>
        StapleTopRight = 22,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples in the bottom right corner.
        /// </summary>
        StapleBottomRight = 23,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples (wire stitches) along the left edge.  The exact
        ///     number and placement of the staples is implementation
        ///     and/or site-defined.
        /// </summary>
        EdgeStitchLeft = 24,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples (wire stitches) along the top edge.  The exact
        ///     number and placement of the staples is implementation
        ///     and/or site-defined.
        /// </summary>
        EdgeStitchTop = 25,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples (wire stitches) along the right edge.  The exact
        ///     number and placement of the staples is implementation
        ///     and/or site-defined.
        /// </summary>
        EdgeStitchRight = 26,

        /// <summary>
        ///     Bind the document(s) with one or more
        ///     staples (wire stitches) along the bottom edge.  The exact
        ///     number and placement of the staples is implementation
        ///     and/or site-defined.
        /// </summary>
        EdgeStitchBottom = 27,

        /// <summary>
        ///     Bind the document(s) with two staples
        ///     (wire stitches) along the left edge assuming a portrait
        ///     document (see above).
        /// </summary>
        StapleDualLeft = 28,

        /// <summary>
        ///     Bind the document(s) with two staples
        ///     (wire stitches) along the top edge assuming a portrait
        ///     document (see above).
        /// </summary>
        StapleDualTop = 29,

        /// <summary>
        ///     Bind the document(s) with two staples
        ///     (wire stitches) along the right edge assuming a portrait
        ///     document (see above).
        /// </summary>
        StapleDualRight = 30,

        /// <summary>
        ///     Bind the document(s) with two staples
        ///     (wire stitches) along the bottom edge assuming a portrait
        ///     document (see above).
        /// </summary>
        StapleDualBottom = 31,
    }
}
