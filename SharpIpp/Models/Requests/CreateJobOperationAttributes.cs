using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class CreateJobOperationAttributes : OperationAttributes
{
    /// <summary>
    /// This REQUIRED attribute is the name of the job. It is a name that is more user friendly than the "job-uri" attribute value. It does not need to be unique between Jobs. The Job's "job-name" attribute is set to the value supplied by the client in the "job-name" operation attribute in the create request. If, however, the "job-name" operation attribute is not supplied by the client in the create request, the Printer object, on creation of the Job, MUST generate a name.
    /// See: RFC 8011 Section 5.3.5
    /// </summary>
    /// <code>job-name</code>
    /// <example>job63</example>
    public string? JobName { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. The value 'true' indicates that total fidelity to client supplied Job Template attributes and values is required, else the Printer object MUST reject the Print-Job request. The value 'false' indicates that a reasonable attempt to print the Job object is acceptable and the Printer object MUST accept the Print-Job request. If not supplied, the Printer object assumes the value is 'false'. All Printer objects MUST support both types of job processing. See section 15 for a full description of "ipp-attribute-fidelity" and its relationship to other attributes, especially the Printer object's "pdl-override-supported" attribute
    /// See: RFC 8011 Section 5.4.18
    /// </summary>
    /// <code>ipp-attribute-fidelity</code>
    /// <example>False</example>
    public bool? IppAttributeFidelity { get; set; }
    /// <summary>
    /// This attribute specifies the total size in number of impressions of the document(s) being submitted. As with "job-k-octets", this value MUST NOT include the multiplicative factors contributed by the number of copies specified by the "copies" attribute, independent of whether the device can process multiple copies without making multiple passes over the job or document data and independent of whether the output is collated or not. Thus the value is independent of the implementation and reflects the size of the document(s) measured in impressions independent of the number of copies. As with "job-k-octets", this value MUST also not include the multiplicative factor due to a copies instruction embedded in the document data. If the document data actually includes replications of the document data, this value will include such replication. In other words, this value is always the number of impressions in the source document data, rather than a measure of the number of impressions to be produced by the job
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>job-impressions</code>
    /// <example>no value</example>
    public int? JobImpressions { get; set; }
    /// <summary>
    /// This attribute specifies the total number of media sheets to be produced for this job. Unlike the "job-k-octets" and the "job-impressions" attributes, this value MUST include the multiplicative factors contributed by the number of copies specified by the "copies" attribute and a 'number of copies' instruction embedded in the document data, if any. This difference allows the system administrator to control the lower and upper bounds of both (1) the size of the document(s) with "job-k- octets-supported" and "job-impressions-supported" and (2) the size of the job with "job-media-sheets-supported"
    /// See: pwg5100.1-2022 Section 6.9.4
    /// </summary>
    /// <code>job-media-sheets</code>
    /// <example>no value</example>
    public int? JobMediaSheets { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. The value of this attribute identifies the total size of the document(s) in K octets, i.e., in units of 1024 octets. The value MUST be rounded up, so that a job between 1 and 1024 octets inclusive MUST be indicated as being 1, 1025 to 2048 inclusive MUST be 2, etc
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0 Section 6.2.2
    /// </summary>
    /// <code>job-k-octets</code>
    /// <example>26</example>
    public int? JobKOctets { get; set; }

}
