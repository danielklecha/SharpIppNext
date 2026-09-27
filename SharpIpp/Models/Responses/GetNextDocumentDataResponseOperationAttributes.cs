using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-Next-Document-Data response operation attributes.
/// See: PWG 5100.17-2014 Section 6.1.2
/// </summary>
[IppAttribute]
public class GetNextDocumentDataResponseOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>compression</c> operation attribute.
    /// </summary>
    [IppAttribute(IppAttributeNames.Compression, Tag = Tag.Keyword)]
    public IppValue<Compression>? Compression { get; set; }

    /// <summary>
    /// The <c>document-data-get-interval</c> operation attribute.
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.DocumentDataGetInterval, Tag = Tag.Integer)]
    public IppValue<int>? DocumentDataGetInterval { get; set; }

    /// <summary>
    /// The <c>last-document</c> operation attribute.
    /// </summary>
    [IppAttribute(IppAttributeNames.LastDocument, Tag = Tag.Boolean)]
    public IppValue<bool>? LastDocument { get; set; }

    /// <summary>
    /// The <c>document-number</c> operation attribute.
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.DocumentNumber, Tag = Tag.Integer)]
    public IppValue<int>? DocumentNumber { get; set; }
}
