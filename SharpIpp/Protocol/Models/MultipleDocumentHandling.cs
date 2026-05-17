namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the multiple-document-handling attribute, which controls how multiple documents in a job are handled.
    /// See: RFC 8011 Section 5.2.4
    /// </summary>
    public readonly record struct MultipleDocumentHandling(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// All documents in the job are treated as a single document for finishing and output.
        /// See: RFC 8011 Section 5.2.4
        /// </summary>
        public static readonly MultipleDocumentHandling SingleDocument = new("single-document");

        /// <summary>
        /// Each document in the job is treated as a separate document; copies are not collated across documents.
        /// See: RFC 8011 Section 5.2.4
        /// </summary>
        public static readonly MultipleDocumentHandling SeparateDocumentsUncollatedCopies = new("separate-documents-uncollated-copies");

        /// <summary>
        /// Each document in the job is treated as a separate document; copies are collated within each document.
        /// See: RFC 8011 Section 5.2.4
        /// </summary>
        public static readonly MultipleDocumentHandling SeparateDocumentsCollatedCopies = new("separate-documents-collated-copies");

        /// <summary>
        /// All documents in the job are treated as a single document for finishing, but each document starts on a new sheet.
        /// See: RFC 8011 Section 5.2.4
        /// </summary>
        public static readonly MultipleDocumentHandling SingleDocumentNewSheet = new("single-document-new-sheet");

        public override string ToString() => Value;
        public static implicit operator string(MultipleDocumentHandling bin) => bin.Value;
        public static explicit operator MultipleDocumentHandling(string value) => new(value);
    }
}
