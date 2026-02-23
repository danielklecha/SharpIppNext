using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class JobOperationAttributes : OperationAttributes
{
    public Uri? JobUri { get; set; }

    public int? JobId { get; set; }

    public static new T Create<T>(Dictionary<string, IppAttribute[]> dict, IMapperApplier mapper) where T : JobOperationAttributes, new()
    {
        var attributes = OperationAttributes.Create<T>(dict, mapper);
        attributes.JobId = mapper.MapFromDic<int?>(dict, JobAttribute.JobId);
        var jobUri = mapper.MapFromDic<string?>(dict, JobAttribute.JobUri);
        if (jobUri != null && Uri.TryCreate(jobUri, UriKind.RelativeOrAbsolute, out var uri))
            attributes.JobUri = uri;
        return attributes;
    }

    public override IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        foreach (var attribute in base.GetIppAttributes(mapper))
            yield return attribute;
        if (JobId != null)
            yield return new IppAttribute(Tag.Integer, JobAttribute.JobId, JobId);
        if (JobUri != null)
            yield return new IppAttribute(Tag.Uri, JobAttribute.JobUri, JobUri.ToString());
    }
}
