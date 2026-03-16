namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the multiple-document-handling attribute.
    /// </summary>
    public readonly record struct MultipleDocumentHandling(string Value)
    {
        public static readonly MultipleDocumentHandling SingleDocument = new("single-document");
        public static readonly MultipleDocumentHandling SeparateDocumentsUncollatedCopies = new("separate-documents-uncollated-copies");
        public static readonly MultipleDocumentHandling SeparateDocumentsCollatedCopies = new("separate-documents-collated-copies");
        public static readonly MultipleDocumentHandling SingleDocumentNewSheet = new("single-document-new-sheet");

        public override string ToString() => Value;
        public static implicit operator string(MultipleDocumentHandling bin) => bin.Value;
        public static explicit operator MultipleDocumentHandling(string value) => new(value);
    }
}
