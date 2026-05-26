using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ValidateJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ValidateJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ValidateJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>(src, dst);
            dst.DocumentMetadata = map.MapFromDicSetNullable<DocumentMetadata?>(src, IppAttributeNames.DocumentMetadata);
            dst.DocumentPassword = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.DocumentPassword);
            dst.DocumentName = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, IppAttributeNames.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentNaturalLanguage);
            dst.DocumentCharset = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentCharset);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentMessage);
            return dst;
        });

        mapper.CreateMap<ValidateJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CreateJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentMetadata != null)
                dst.AddRange(src.DocumentMetadata.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString(x))));
            if (src.DocumentPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentPassword, src.DocumentPassword.Value));
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.DocumentName, src.DocumentName));
            if (src.Compression != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Compression, map.Map<string>(src.Compression.Value)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, src.DocumentFormat));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.DocumentNaturalLanguage, src.DocumentNaturalLanguage));
            if (src.DocumentCharset != null)
                dst.Add(new IppAttribute(Tag.Charset, IppAttributeNames.DocumentCharset, src.DocumentCharset));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentMessage, src.DocumentMessage));
            return dst;
        });
    }
}
