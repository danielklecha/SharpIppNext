using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SendDocumentOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SendDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SendDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentMetadata = map.MapFromDicSetNullable<OctetString[]?>(src, JobAttribute.DocumentMetadata);
            dst.DocumentPassword = map.MapFromDicNullable<OctetString?>(src, JobAttribute.DocumentPassword);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            if (src.TryGetValue(JobAttribute.DocumentFormatDetails, out var documentFormatDetails))
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.DocumentName = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, JobAttribute.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentNaturalLanguage);
            dst.DocumentCharset = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentCharset);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentMessage);
            var lastDocument = map.MapFromDicNullable<bool?>(src, JobAttribute.LastDocument);
            if (lastDocument.HasValue)
                dst.LastDocument = lastDocument.Value;
            return dst;
        });

        mapper.CreateMap<SendDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentMetadata != null)
                dst.AddRange(src.DocumentMetadata.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentMetadata, x)));
            if (src.DocumentPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, src.DocumentPassword.Value));
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(JobAttribute.DocumentFormatDetails));
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
            dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, src.LastDocument));
            return dst;
        });
    }
}
