namespace SharpIpp.Protocol.Models
{
    public enum Finishings
    {
        /// <summary>
        /// Perform no finishing
        /// </summary>
        None = 3,

        /// <summary>
        /// Bind the document(s) with one or more staples. The
        /// exact number and placement of the staples is site-
        /// defined.
        /// </summary>
        Staple = 4,

        /// <summary>
        /// This value indicates that holes are required in the
        /// finished document. The exact number and placement of the
        /// holes is site-defined  The punch specification MAY be
        /// satisfied (in a site- and implementation-specific manner)
        /// either by drilling/punching, or by substituting pre-
        /// drilled media.
        /// </summary>
        Punch = 5,

        /// <summary>
        /// This value is specified when it is desired to select
        /// a non-printed (or pre-printed) cover for the document.
        /// This does not supplant the specification of a printed
        /// cover (on cover stock medium) by the document itself.
        /// </summary>
        Cover = 6,

        /// <summary>
        /// This value indicates that a binding is to be applied
        /// to the document; the type and placement of the binding is
        /// site-defined.
        /// </summary>
        Bind = 7,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples (wire stitches) along the middle fold.  The exact
        /// number and placement of the staples and the middle fold
        /// is implementation and/or site-defined.
        /// </summary>
        SaddleStitch = 8,

        /// <summary>
        /// Bind the document(s) with one or more staples
        /// (wire stitches) along one edge.  The exact number and
        /// placement of the staples is implementation and/or site-
        /// defined.
        /// </summary>
        EdgeStitch = 9,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples in the top left corner.
        /// </summary>
        StapleTopLeft = 20,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples in the bottom left corner.
        /// </summary>
        StapleBottomLeft = 21,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples in the top right corner.
        /// </summary>
        StapleTopRight = 22,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples in the bottom right corner.
        /// </summary>
        StapleBottomRight = 23,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples (wire stitches) along the left edge.  The exact
        /// number and placement of the staples is implementation
        /// and/or site-defined.
        /// </summary>
        EdgeStitchLeft = 24,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples (wire stitches) along the top edge.  The exact
        /// number and placement of the staples is implementation
        /// and/or site-defined.
        /// </summary>
        EdgeStitchTop = 25,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples (wire stitches) along the right edge.  The exact
        /// number and placement of the staples is implementation
        /// and/or site-defined.
        /// </summary>
        EdgeStitchRight = 26,

        /// <summary>
        /// Bind the document(s) with one or more
        /// staples (wire stitches) along the bottom edge.  The exact
        /// number and placement of the staples is implementation
        /// and/or site-defined.
        /// </summary>
        EdgeStitchBottom = 27,

        /// <summary>
        /// Bind the document(s) with two staples
        /// (wire stitches) along the left edge assuming a portrait
        /// document (see above).
        /// </summary>
        StapleDualLeft = 28,

        /// <summary>
        /// Bind the document(s) with two staples
        /// (wire stitches) along the top edge assuming a portrait
        /// document (see above).
        /// </summary>
        StapleDualTop = 29,

        /// <summary>
        /// Bind the document(s) with two staples
        /// (wire stitches) along the right edge assuming a portrait
        /// document (see above).
        /// </summary>
        StapleDualRight = 30,

        /// <summary>
        /// Bind the document(s) with two staples
        /// (wire stitches) along the bottom edge assuming a portrait
        /// document (see above).
        /// </summary>
        StapleDualBottom = 31,

        /// <summary>
        /// Bind the document(s) with three staples (wire stitches)
        /// along the left edge assuming a portrait document.
        /// See: PWG 5100.1-2014
        /// </summary>
        StapleTripleLeft = 32,

        /// <summary>
        /// Bind the document(s) with three staples (wire stitches)
        /// along the top edge assuming a portrait document.
        /// See: PWG 5100.1-2014
        /// </summary>
        StapleTripleTop = 33,

        /// <summary>
        /// Bind the document(s) with three staples (wire stitches)
        /// along the right edge assuming a portrait document.
        /// See: PWG 5100.1-2014
        /// </summary>
        StapleTripleRight = 34,

        /// <summary>
        /// Bind the document(s) with three staples (wire stitches)
        /// along the bottom edge assuming a portrait document.
        /// See: PWG 5100.1-2014
        /// </summary>
        StapleTripleBottom = 35,

        /// <summary>
        /// Fold the hardcopy output. The exact number and
        /// orientations of the folds is implementation and/or
        /// site defined.
        /// See: PWG 5100.1-2001
        /// </summary>
        Fold = 10,

        /// <summary>
        /// Trim the hardcopy output on one or more edges.
        /// See: PWG 5100.1-2001
        /// </summary>
        Trim = 11,

        /// <summary>
        /// Bale the document(s). The type of baling is
        /// implementation and/or site defined.
        /// See: PWG 5100.1-2001
        /// </summary>
        Bale = 12,

        /// <summary>
        /// Deliver the document(s) to the signature booklet maker.
        /// This value is a short cut for specifying a Job that is
        /// to be folded, trimmed and then saddle-stitched.
        /// See: PWG 5100.1-2001
        /// </summary>
        BookletMaker = 13,

        /// <summary>
        /// (DEPRECATED) Shift each Set from the previous one by
        /// a small amount which is device dependent.
        /// See: PWG 5100.1-2001
        /// </summary>
        JogOffset = 14,

        /// <summary>
        /// Apply a protective liquid or powdered coating to each
        /// sheet in an implementation and/or site defined manner.
        /// See: PWG 5100.1-2014
        /// </summary>
        Coat = 15,

        /// <summary>
        /// Apply a protective (solid) material to each sheet in
        /// an implementation and/or site defined manner.
        /// See: PWG 5100.1-2014
        /// </summary>
        Laminate = 16,

        /// <summary>
        /// Bind the document(s) along the left edge; the type
        /// of the binding is implementation and/or site defined.
        /// See: PWG 5100.1-2001
        /// </summary>
        BindLeft = 50,

        /// <summary>
        /// Bind the document(s) along the top edge; the type
        /// of the binding is implementation and/or site defined.
        /// See: PWG 5100.1-2001
        /// </summary>
        BindTop = 51,

        /// <summary>
        /// Bind the document(s) along the right edge; the type
        /// of binding is implementation and/or site defined.
        /// See: PWG 5100.1-2001
        /// </summary>
        BindRight = 52,

        /// <summary>
        /// Bind the document(s) along the bottom edge; the type
        /// of the binding is implementation and/or site defined.
        /// See: PWG 5100.1-2001
        /// </summary>
        BindBottom = 53,

        /// <summary>
        /// Trim output after each page.
        /// See: PWG 5100.1-2020
        /// </summary>
        TrimAfterPages = 60,

        /// <summary>
        /// Trim output after each Document.
        /// See: PWG 5100.1-2020
        /// </summary>
        TrimAfterDocuments = 61,

        /// <summary>
        /// Trim output after each Set.
        /// See: PWG 5100.1-2020
        /// </summary>
        TrimAfterCopies = 62,

        /// <summary>
        /// Trim output after Job.
        /// See: PWG 5100.1-2020
        /// </summary>
        TrimAfterJob = 63,

        /// <summary>
        /// Punch a single hole in the top left of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchTopLeft = 70,

        /// <summary>
        /// Punch a single hole in the bottom left of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchBottomLeft = 71,

        /// <summary>
        /// Punch a single hole in the top right of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchTopRight = 72,

        /// <summary>
        /// Punch a single hole in the bottom right of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchBottomRight = 73,

        /// <summary>
        /// Punch two holes on the left side of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchDualLeft = 74,

        /// <summary>
        /// Punch two holes at the top of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchDualTop = 75,

        /// <summary>
        /// Punch two holes on the right side of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchDualRight = 76,

        /// <summary>
        /// Punch two holes at the bottom of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchDualBottom = 77,

        /// <summary>
        /// Punch three holes on the left side of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchTripleLeft = 78,

        /// <summary>
        /// Punch three holes at the top of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchTripleTop = 79,

        /// <summary>
        /// Punch three holes on the right side of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchTripleRight = 80,

        /// <summary>
        /// Punch three holes at the bottom of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchTripleBottom = 81,

        /// <summary>
        /// Punch four holes on the left side of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchQuadLeft = 82,

        /// <summary>
        /// Punch four holes at the top of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchQuadTop = 83,

        /// <summary>
        /// Punch four holes on the right side of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchQuadRight = 84,

        /// <summary>
        /// Punch four holes at the bottom of the hardcopy output.
        /// See: PWG 5100.1-2014
        /// </summary>
        PunchQuadBottom = 85,

        /// <summary>
        /// Drill or punch more than four holes along the left
        /// reference edge.
        /// See: PWG 5100.1-2017
        /// </summary>
        PunchMultipleLeft = 86,

        /// <summary>
        /// Drill or punch more than four holes along the top
        /// reference edge.
        /// See: PWG 5100.1-2017
        /// </summary>
        PunchMultipleTop = 87,

        /// <summary>
        /// Drill or punch more than four holes along the right
        /// reference edge.
        /// See: PWG 5100.1-2017
        /// </summary>
        PunchMultipleRight = 88,

        /// <summary>
        /// Drill or punch more than four holes along the bottom
        /// reference edge.
        /// See: PWG 5100.1-2017
        /// </summary>
        PunchMultipleBottom = 89,

        /// <summary>
        /// Accordion-fold the hardcopy output vertically into
        /// four sections.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldAccordion = 90,

        /// <summary>
        /// Fold the top and bottom quarters of the hardcopy output
        /// towards the midline, then fold in half vertically.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldDoubleGate = 91,

        /// <summary>
        /// Fold the top and bottom quarters of the hardcopy output
        /// towards the midline.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldGate = 92,

        /// <summary>
        /// Fold the hardcopy output in half vertically.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldHalf = 93,

        /// <summary>
        /// Fold the hardcopy output in half horizontally, then
        /// Z-fold the paper vertically into three sections.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldHalfZ = 94,

        /// <summary>
        /// Fold the top quarter of the hardcopy output towards
        /// the midline.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldLeftGate = 95,

        /// <summary>
        /// Fold the hardcopy output into three sections vertically;
        /// sometimes also known as a C fold.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldLetter = 96,

        /// <summary>
        /// Fold the hardcopy output in half vertically two times,
        /// yielding four sections.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldParallel = 97,

        /// <summary>
        /// Fold the hardcopy output in half horizontally and
        /// vertically; sometimes also called a cross fold.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldPoster = 98,

        /// <summary>
        /// Fold the bottom quarter of the hardcopy output towards
        /// the midline.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldRightGate = 99,

        /// <summary>
        /// Fold the hardcopy output vertically into three sections,
        /// forming a Z.
        /// See: PWG 5100.1-2014
        /// </summary>
        FoldZ = 100,

        /// <summary>
        /// Fold the hardcopy output vertically into three sections,
        /// forming a Z but leaving room for binding, punching, or
        /// stapling along the top edge.
        /// See: PWG 5100.1-2017
        /// </summary>
        FoldEngineeringZ = 101,
    }
}
