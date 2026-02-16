using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharpIpp.Models;
public class OperationAttributes
{
    public string? AttributesCharset { get; set; } = "utf-8";

    public string? AttributesNaturalLanguage { get; set; } = "en";

    public Uri PrinterUri { get; set; } = null!;

    public string? RequestingUserName { get; set; }


    public static T Create<T>(Dictionary<string, IppAttribute[]> dict, IMapperApplier mapper) where T : OperationAttributes, new()
    {
        var attributes = new T
        {
            AttributesCharset = mapper.MapFromDic<string?>(dict, JobAttribute.AttributesCharset),
            AttributesNaturalLanguage = mapper.MapFromDic<string?>(dict, JobAttribute.AttributesNaturalLanguage),
            RequestingUserName = mapper.MapFromDic<string?>(dict, JobAttribute.RequestingUserName)
        };
        var printerUri = mapper.MapFromDic<string?>(dict, JobAttribute.PrinterUri);
        if(printerUri != null && Uri.TryCreate(printerUri, UriKind.RelativeOrAbsolute, out Uri uri))
            attributes.PrinterUri = uri;
        return attributes;
    }

    public virtual IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        if (AttributesCharset == null)
            throw new ArgumentNullException(nameof(AttributesCharset));
        yield return new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, AttributesCharset);
        if (AttributesNaturalLanguage == null)
            throw new ArgumentNullException(nameof(AttributesNaturalLanguage));
        yield return new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, AttributesNaturalLanguage);
        if (PrinterUri != null)
            yield return new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, PrinterUri.ToString());
        if (RequestingUserName != null)
            yield return new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, RequestingUserName);
    }
}
