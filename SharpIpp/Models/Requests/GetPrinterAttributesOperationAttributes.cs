using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class GetPrinterAttributesOperationAttributes : OperationAttributes
{
    public string[]? RequestedAttributes { get; set; }

    /// <summary>
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute.  The value of this
    ///     attribute identifies the format of the supplied document data.
    /// </summary>
    /// <example>application/octet-stream</example>
    public string? DocumentFormat { get; set; }

    public static new T Create<T>(Dictionary<string, IppAttribute[]> dict, IMapperApplier mapper) where T : GetPrinterAttributesOperationAttributes, new()
    {
        var attributes = OperationAttributes.Create<T>(dict, mapper);
        attributes.RequestedAttributes = mapper.MapFromDicSetNullable<string[]?>(dict, JobAttribute.RequestedAttributes);
        attributes.DocumentFormat = mapper.MapFromDicNullable<string?>(dict, JobAttribute.DocumentFormat);
        return attributes;
    }

    public override IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        foreach (var attribute in base.GetIppAttributes(mapper))
            yield return attribute;
        if(RequestedAttributes != null)
            foreach (var attribute in RequestedAttributes)
                yield return new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, attribute);
        if (DocumentFormat != null)
            yield return new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormat, DocumentFormat);
    }
}
