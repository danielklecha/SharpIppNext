using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Resubmit-Job Operation Attributes.
/// See: PWG 5100.7-2023 Section 5.3.1
/// </summary>
public class ResubmitJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// This DEPRECATED operation attribute specifies details about the source of the Document data and can be included in any Job or Document Creation request.
    /// See: PWG 5100.7-2023 Section 6.1.2
    /// </summary>
    /// <code>document-format-details</code>
    public DocumentFormatDetails? DocumentFormatDetails { get; set; }

    /// <summary>
    /// This REQUIRED operation attribute identifies which Job Template attributes the Printer MUST support in order to accept the Job.
    /// See: PWG 5100.7-2023 Section 6.1.5
    /// </summary>
    /// <code>job-mandatory-attributes</code>
    public string[]? JobMandatoryAttributes { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute.
    /// See: RFC 8011 Section 5.4.18
    /// </summary>
    /// <code>ipp-attribute-fidelity</code>
    public bool? IppAttributeFidelity { get; set; }
}
