namespace SharpIpp.Protocol.Models
{
    public enum MultipleDocumentHandling
    {

        /// 'single-document': If a Job object has multiple documents, say,
        /// the document data is called a and b, then the result of
        /// processing all the document data (a and then b) MUST be treated
        /// as a single sequence of media sheets for finishing operations;
        /// that is, finishing would be performed on the concatenation of
        /// the sequences a(*),b(*).  The Printer object MUST NOT force the
        /// data in each document instance to be formatted onto a new
        /// print-stream page, nor to start a new impression on a new media
        /// sheet. If more than one copy is made, the ordering of the sets
        /// of media sheets resulting from processing the document data
        /// MUST be a(*), b(*), a(*), b(*), start on a new media sheet.
        SingleDocument,

        /// 'separate-documents-uncollated-copies': If a Job object has
        /// multiple documents, say, the document data is called a and b,
        /// then the result of processing the data in each document
        /// instance MUST be treated as a single sequence of media sheets
        /// for finishing operations; that is, the sets a(*) and b(*) would
        /// each be finished separately. The Printer object MUST force each
        /// copy of the result of processing the data in a single document
        /// to start on a new media sheet. If more than one copy is made,
        /// the ordering of the sets of media sheets resulting from
        /// processing the document data MUST be a(*), a(*), ..., b(*),
        /// b(*) ... .
        SeparateDocumentsUncollatedCopies,

        /// 'separate-documents-collated-copies': If a Job object has multiple
        /// documents, say, the document data is called a and b, then the
        /// result of processing the data in each document instance MUST be
        /// treated as a single sequence of media sheets for finishing
        /// operations; that is, the sets a(*) and b(*) would each be
        /// finished separately. The Printer object MUST force each copy of
        /// the result of processing the data in a single document to start
        /// on a new media sheet.  If more than one copy is made, the
        /// ordering of the sets of media sheets resulting from processing
        /// the document data MUST be a(*), b(*), a(*), b(*), ... .
        SeparateDocumentsCollatedCopies,

        /// 'single-document-new-sheet':  Same as 'single-document', except
        /// that the Printer object MUST ensure that the first impression
        /// of each document instance in the job is placed on a new media
        /// sheet.  This value allows multiple documents to be stapled
        /// together with a single staple where each document starts on a
        /// new sheet.
        SingleDocumentNewSheet,
    }
}
