using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SharpIpp.Models.Requests;
[IppAttribute]
public class PrintJobOperationAttributes : CreateJobOperationAttributes
{
    /// <summary>
    /// The <c>document-metadata</c> operation attribute.
    /// See: PWG 5100.13-2023 Section 6.1.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentMetadata, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    [Metadata]
    public IppValue<DocumentMetadata>? DocumentMetadata { get; set; }

    /// <summary>
    /// The <c>document-password</c> operation attribute.
    /// A password required to access the document (maximum 1023 octets).
    /// See: PWG 5100.13-2023 Section 6.1.2
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentPassword, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    [ByteRange(1, 1023)]
    public OctetString? DocumentPassword { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute.  The Printer
    /// object MUST support this attribute.   It contains the client
    /// supplied document name.  The document name MAY be different
    /// than the Job name.  Typically, the client software
    /// automatically supplies the document name on behalf of the end
    /// user by using a file name or an application generated name.  If
    /// this attribute is supplied, its value can be used in a manner
    /// defined by each implementation.  Examples include: printed
    /// along with the Job (job start sheet, page adornments, etc.),
    /// used by accounting or resource tracking management tools, or
    /// even stored along with the document as a document level
    /// attribute.  IPP/1.1 does not support the concept of document
    /// level attributes.
    /// </summary>
    /// <example>job63</example>
    /// <code>document-name</code>
    [IppAttribute(IppAttributeNames.DocumentName, Tag = Tag.NameWithoutLanguage)]
    public StringWithLanguage? DocumentName { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute.  The Printer
    /// object MUST support this attribute and the "compression-
    /// supported" attribute (see section 4.4.32).  The client supplied
    /// "compression" operation attribute identifies the compression
    /// algorithm used on the document data. The following cases exist:
    /// </summary>
    /// <example>none</example>
    /// <code>compression</code>
    [IppAttribute(IppAttributeNames.Compression, Tag = Tag.Keyword)]
    public IppValue<Compression>? Compression { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute.  The Printer
    /// object MUST support this attribute.  The value of this
    /// attribute identifies the format of the supplied document data.
    /// The following cases exist:
    /// </summary>
    /// <example>application/octet-stream</example>
    /// <code>document-format</code>
    [IppAttribute(IppAttributeNames.DocumentFormat, Tag = Tag.MimeMediaType)]
    public IppValue<DocumentFormat>? DocumentFormat { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object OPTIONALLY supports this attribute. This attribute specifies the natural language of the document for those document-formats that require a specification of the natural language in order to image the document unambiguously. There are no particular values required for the Printer object to support
    /// See: RFC 8011 Section 4.2.1.1
    /// </summary>
    /// <code>document-natural-language</code>
    [IppAttribute(IppAttributeNames.DocumentNaturalLanguage, Tag = Tag.NaturalLanguage)]
    public IppValue<NaturalLanguage>? DocumentNaturalLanguage { get; set; }
    /// <summary>
    /// The document-charset IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2.1
    /// </summary>
    /// <code>document-charset</code>
    [IppAttribute(IppAttributeNames.DocumentCharset, Tag = Tag.Charset)]
    public IppValue<Charset>? DocumentCharset { get; set; }

    /// <summary>
    /// The document-message IPP attribute.
    /// See: PWG 5100.5-2024 Section 8.4.1
    /// See: PWG 5100.5-2024 Section 6.2.3
    /// </summary>
    /// <code>document-message</code>
    [IppAttribute(IppAttributeNames.DocumentMessage, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? DocumentMessage { get; set; }
}
