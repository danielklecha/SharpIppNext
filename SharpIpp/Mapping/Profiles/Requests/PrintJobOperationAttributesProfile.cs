using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class PrintJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PrintJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>(src, dst);
            dst.DocumentMetadata = map.MapFromDicSetNullable<OctetString[]?>(src, JobAttribute.DocumentMetadata);
            dst.DocumentPassword = map.MapFromDicNullable<OctetString?>(src, JobAttribute.DocumentPassword);
            dst.DocumentName = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, JobAttribute.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentNaturalLanguage);
            dst.DocumentCharset = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentCharset);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentMessage);
            return dst;
        });

        mapper.CreateMap<PrintJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CreateJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentMetadata != null)
                dst.AddRange(src.DocumentMetadata.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentMetadata, x)));
            if (src.DocumentPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, src.DocumentPassword.Value));
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.DocumentName, src.DocumentName));
            if (src.Compression != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Compression, map.Map<string>(src.Compression.Value)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormat, src.DocumentFormat));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage));
            if (src.DocumentCharset != null)
                dst.Add(new IppAttribute(Tag.Charset, JobAttribute.DocumentCharset, src.DocumentCharset));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentMessage, src.DocumentMessage));
            return dst;
        });
    }
}
