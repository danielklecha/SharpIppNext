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
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute.  It is a set of Job
    ///     attribute names and/or attribute groups names in whose values
    ///     the requester is interested.  This set of attributes is
    ///     returned for each Job object that is returned.  The allowed
    ///     attribute group names are the same as those defined in the
    ///     Get-Job-Attributes operation in section 3.3.4.  If the client
    ///     does not supply this attribute, the Printer MUST respond as if
    ///     the client had supplied this attribute with two values: 'job-
    ///     uri' and 'job-id'.
    /// </summary>
    public string[]? RequestedAttributes { get; set; }
    /// <summary>
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute.  It indicates which Job
    ///     objects MUST be returned by the Printer object. The values for
    ///     this attribute are:
    /// </summary>
    public WhichJobs? WhichJobs { get; set; }

    /// <summary>
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute. It is an integer value that
    ///     determines the maximum number of jobs that a client will
    ///     receive from the Printer even if "which-jobs" or "my-jobs"
    ///     constrain which jobs are returned.  The limit is a "stateless
    ///     limit" in that if the value supplied by the client is 'N', then
    ///     only the first 'N' jobs are returned in the Get-Jobs Response.
    ///     There is no mechanism to allow for the next 'M' jobs after the
    ///     first 'N' jobs.  If the client does not supply this attribute,
    ///     the Printer object responds with all applicable jobs.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute.  It indicates whether jobs
    ///     from all users or just the jobs submitted by the requesting
    ///     user of this request MUST be considered as candidate jobs to be
    ///     returned by the Printer object.  If the client does not supply
    ///     this attribute, the Printer object MUST respond as if the
    ///     client had supplied the attribute with a value of 'false',
    ///     i.e., jobs from all users.  The means for authenticating the
    ///     requesting user and matching the jobs is described in section
    /// </summary>
    public bool? MyJobs { get; set; }
}
