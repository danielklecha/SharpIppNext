using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class JobOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The client MUST supply either (1) the "job-uri" attribute or (2) the "printer-uri" and "job-id" attributes. The Printer object MUST support both of these forms of target identification. It contains the URI of the Job object which is the target of this operation.
    /// See: RFC 8011 Section 5.3.2
    /// See: PWG 5100.5-2024 Section 5.1.1.1 (Cancel-Document)
    /// See: PWG 5100.5-2024 Section 5.1.2.1 (Get-Document-Attributes)
    /// See: PWG 5100.5-2024 Section 5.1.3.1 (Set-Document-Attributes)
    /// See: PWG 5100.5-2024 Section 5.2.1.1 (Get-Documents)
    /// </summary>
    /// <code>job-uri</code>
    [Obsolete("The 'job-uri' attribute is deprecated in favor of 'job-id'. See RFC 8011 Section 5.3.2.")]
    public Uri? JobUri { get; set; }

    /// <summary>
    /// The client MUST supply either (1) the "job-uri" attribute or (2) the "printer-uri" and "job-id" attributes. It contains the ID of the Job object which is the target of this operation.
    /// See: RFC 8011 Section 5.3.1
    /// See: PWG 5100.5-2024 Section 5.1.1.1 (Cancel-Document)
    /// See: PWG 5100.5-2024 Section 5.1.2.1 (Get-Document-Attributes)
    /// See: PWG 5100.5-2024 Section 5.1.3.1 (Set-Document-Attributes)
    /// See: PWG 5100.5-2024 Section 5.2.1.1 (Get-Documents)
    /// </summary>
    /// <code>job-id</code>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
    public int? JobId { get; set; }

}
