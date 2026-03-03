using SharpIpp.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class DocumentAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentAttributes>((
            src,
            dst,
            map) =>
        {
            dst ??= new DocumentAttributes();
            dst.DocumentNumber = map.MapFromDic<int>(src, DocumentAttribute.DocumentNumber);
            dst.DocumentState = map.MapFromDic<DocumentState>(src, DocumentAttribute.DocumentState);
            dst.DocumentStateReasons = map.MapFromDicSetNullable<DocumentStateReason[]?>(src, DocumentAttribute.DocumentStateReasons);
            dst.DocumentStateMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentStateMessage);
            return dst;
        });

        mapper.CreateMap<DocumentAttributes, IDictionary<string, IppAttribute[]>>((
            src,
            dst,
            map) =>
        {
            var dic = new Dictionary<string, IppAttribute[]>
            {
                { DocumentAttribute.DocumentNumber, [new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber)] },
                { DocumentAttribute.DocumentState, [new IppAttribute(Tag.Enum, DocumentAttribute.DocumentState, (int)src.DocumentState)] }
            };
            if (src.DocumentStateReasons != null)
                dic.Add(DocumentAttribute.DocumentStateReasons, src.DocumentStateReasons.Select(x => new IppAttribute(Tag.Keyword, DocumentAttribute.DocumentStateReasons, map.Map<string>(x))).ToArray());
            if (src.DocumentStateMessage != null)
                dic.Add(DocumentAttribute.DocumentStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentStateMessage, src.DocumentStateMessage)]);
            return dic;
        });
    }
}
