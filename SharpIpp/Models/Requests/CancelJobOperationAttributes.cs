using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class CancelJobOperationAttributes : JobOperationAttributes
{
    public string? Message { get; set; }

    public static new T Create<T>(Dictionary<string, IppAttribute[]> dict, IMapperApplier mapper) where T : CancelJobOperationAttributes, new()
    {
        var attributes = JobOperationAttributes.Create<T>(dict, mapper);
        attributes.Message = mapper.MapFromDic<string?>(dict, JobAttribute.Message);
        return attributes;
    }

    public override IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        foreach (var attribute in base.GetIppAttributes(mapper))
            yield return attribute;
        if (Message != null)
            yield return new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.Message, Message);
    }
}
