using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class GetJobsOperationAttributes : GetJobAttributesOperationAttributes
{
    /// The client OPTIONALLY supplies this attribute.  The Printer
    /// object MUST support this attribute.  It indicates which Job
    /// objects MUST be returned by the Printer object. The values for
    /// this attribute are:
    public WhichJobs? WhichJobs { get; set; }

    /// The client OPTIONALLY supplies this attribute.  The Printer
    /// object MUST support this attribute. It is an integer value that
    /// determines the maximum number of jobs that a client will
    /// receive from the Printer even if "which-jobs" or "my-jobs"
    /// constrain which jobs are returned.  The limit is a "stateless
    /// limit" in that if the value supplied by the client is 'N', then
    /// only the first 'N' jobs are returned in the Get-Jobs Response.
    /// There is no mechanism to allow for the next 'M' jobs after the
    /// first 'N' jobs.  If the client does not supply this attribute,
    /// the Printer object responds with all applicable jobs.
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

    public static new T Create<T>(Dictionary<string, IppAttribute[]> dict, IMapperApplier mapper) where T : GetJobsOperationAttributes, new()
    {
        var attributes = GetJobAttributesOperationAttributes.Create<T>(dict, mapper);
        attributes.Limit = mapper.MapFromDic<int?>(dict, JobAttribute.Limit);
        attributes.WhichJobs = mapper.MapFromDic<WhichJobs?>(dict, JobAttribute.WhichJobs);
        attributes.MyJobs = mapper.MapFromDic<bool?>(dict, JobAttribute.MyJobs);
        return attributes;
    }

    public override IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        foreach (var attribute in base.GetIppAttributes(mapper))
            yield return attribute;
        if (Limit.HasValue)
            yield return new IppAttribute(Tag.Integer, JobAttribute.Limit, Limit.Value);
        if (WhichJobs.HasValue)
            yield return new IppAttribute(Tag.Keyword, JobAttribute.WhichJobs, mapper.Map<string>(WhichJobs.Value));
        if (MyJobs.HasValue)
            yield return new IppAttribute(Tag.Boolean, JobAttribute.MyJobs, MyJobs.Value);
    }
}
