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
            dst.DocumentMetadata = map.MapFromDicSetNullable<DocumentMetadata?>(src, IppAttributeNames.DocumentMetadata);
            dst.DocumentPassword = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.DocumentPassword);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ResourceIds);
            if (src.TryGetValue(IppAttributeNames.DocumentFormatDetails, out var documentFormatDetails))
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.DocumentName = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, IppAttributeNames.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<NaturalLanguage?>(src, IppAttributeNames.DocumentNaturalLanguage);
            dst.DocumentCharset = map.MapFromDicNullable<Charset?>(src, IppAttributeNames.DocumentCharset);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentMessage);
            var lastDocument = map.MapFromDicNullable<bool?>(src, IppAttributeNames.LastDocument);
            if (lastDocument.HasValue)
                dst.LastDocument = lastDocument.Value;
            return dst;
        });

        mapper.CreateMap<SendDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentMetadata != null)
                dst.AddRange(src.DocumentMetadata.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString(x))));
            if (src.DocumentPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentPassword, src.DocumentPassword.Value));
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, x)));
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(IppAttributeNames.DocumentFormatDetails));
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.DocumentName, src.DocumentName));
            if (src.Compression != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Compression, map.Map<string>(src.Compression.Value)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, src.DocumentFormat.Value.Value));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.DocumentNaturalLanguage, src.DocumentNaturalLanguage.Value));
            if (src.DocumentCharset != null)
                dst.Add(new IppAttribute(Tag.Charset, IppAttributeNames.DocumentCharset, src.DocumentCharset.Value));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentMessage, src.DocumentMessage));
            dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, src.LastDocument));
            return dst;
        });
    }
}
