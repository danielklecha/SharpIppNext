using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SharpIpp.Models.Requests;
public class PrintJobOperationAttributes : CreateJobOperationAttributes
{
    /// <summary>
    /// The <c>document-metadata</c> operation attribute.
    /// See: PWG 5100.13-2023 Section 6.1.1
    /// </summary>
    public string[]? DocumentMetadata { get; set; }

    /// <summary>
    /// The <c>document-password</c> operation attribute.
    /// See: PWG 5100.13-2023 Section 6.1.2
    /// </summary>
    public string? DocumentPassword { get; set; }

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
    public string? DocumentName { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute.  The Printer
    /// object MUST support this attribute and the "compression-
    /// supported" attribute (see section 4.4.32).  The client supplied
    /// "compression" operation attribute identifies the compression
    /// algorithm used on the document data. The following cases exist:
    /// </summary>
    /// <example>none</example>
    /// <code>compression</code>
    public Compression? Compression { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute.  The Printer
    /// object MUST support this attribute.  The value of this
    /// attribute identifies the format of the supplied document data.
    /// The following cases exist:
    /// </summary>
    /// <example>application/octet-stream</example>
    /// <code>document-format</code>
    public string? DocumentFormat { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object OPTIONALLY supports this attribute. This attribute specifies the natural language of the document for those document-formats that require a specification of the natural language in order to image the document unambiguously. There are no particular values required for the Printer object to support
    /// See: pwg5100.5-2024
    /// </summary>
    /// <code>document-natural-language</code>
    public string? DocumentNaturalLanguage { get; set; }
    /// <summary>
    /// The document-charset IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.1
    /// </summary>
    /// <code>document-charset</code>
    public string? DocumentCharset { get; set; }

    /// <summary>
    /// The document-message IPP attribute.
    /// See: PWG 5100.5-2024 Section 8.3 Table 6
    /// </summary>
    /// <code>document-message</code>
    public string? DocumentMessage { get; set; }
}
