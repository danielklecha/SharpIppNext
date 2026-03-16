using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class GetJobsOperationAttributes : OperationAttributes
{
    /// <summary>
    /// This REQUIRED attribute specifies a list of "job-id" values for the Cancel-Jobs, Cancel-My-Jobs, and Get-Jobs operations.
    /// See: PWG 5100.7-2023 Section 6.1.4
    /// </summary>
    /// <code>job-ids</code>
    public int[]? JobIds { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is a set of Job attribute names and/or attribute groups names in whose values the requester is interested. This set of attributes is returned for each Job object that is returned. The allowed attribute group names are the same as those defined in the Get-Job-Attributes operation in section 3.3.4. If the client does not supply this attribute, the Printer MUST respond as if the client had supplied this attribute with two values: 'job- uri' and 'job-id'
    /// See: pwg5100.1-2022 Section 6.9.4
    /// </summary>
    /// <code>requested-attributes</code>
    public string[]? RequestedAttributes { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It indicates which Job objects MUST be returned by the Printer object. The values for this attribute are:
    /// See: pwg5100.11 - IPP Enterprise Printing Extensions v2.0 Section 8.4
    /// </summary>
    /// <code>which-jobs</code>
    public WhichJobs? WhichJobs { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is an integer value that determines the maximum number of jobs that a client will receive from the Printer even if "which-jobs" or "my-jobs" constrain which jobs are returned. The limit is a "stateless limit" in that if the value supplied by the client is 'N', then only the first 'N' jobs are returned in the Get-Jobs Response. There is no mechanism to allow for the next 'M' jobs after the first 'N' jobs. If the client does not supply this attribute, the Printer object responds with all applicable jobs
    /// See: pwg5100.1-2022 Section 2.2
    /// </summary>
    /// <code>limit</code>
    public int? Limit { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It indicates whether jobs from all users or just the jobs submitted by the requesting user of this request MUST be considered as candidate jobs to be returned by the Printer object. If the client does not supply this attribute, the Printer object MUST respond as if the client had supplied the attribute with a value of 'false', i.e., jobs from all users. The means for authenticating the requesting user and matching the jobs is described in section
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.3.1
    /// </summary>
    /// <code>my-jobs</code>
    public bool? MyJobs { get; set; }
}
