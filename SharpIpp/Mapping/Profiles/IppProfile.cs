using SharpIpp.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol.Extensions;
using System;
using System.Linq;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class IppProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<IIppRequest, IppRequestMessage>((src, dst, map) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, IIppRequest>( ( src, dst, map ) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, IIppResponseMessage>((src, dst, map) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                dst.StatusCode = src.StatusCode;
                dst.Sections.AddRange(src.Sections);
                return dst;
            });

            mapper.CreateMap<IIppResponseMessage, IppResponseMessage>( ( src, dst, map ) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                dst.StatusCode = src.StatusCode;
                if ( !src.Sections.Any( x => x.Tag == SectionTag.OperationAttributesTag ) )
                {
                    var section = new IppSection { Tag = SectionTag.OperationAttributesTag };
                    section.Attributes.Add( new IppAttribute( Tag.Charset, JobAttribute.AttributesCharset, "utf-8" ) );
                    section.Attributes.Add( new IppAttribute( Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en" ) );
                    dst.Sections.Add( section );
                }
                
                var operationSection = dst.Sections.FirstOrDefault(x => x.Tag == SectionTag.OperationAttributesTag);
                if (operationSection != null)
                {
                    if (src.StatusMessage != null)
                        operationSection.Attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.StatusMessage, src.StatusMessage));
                    if (src.DetailedStatusMessage?.Any() ?? false)
                        operationSection.Attributes.AddRange(src.DetailedStatusMessage.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DetailedStatusMessage, x)));
                    if (src.DocumentAccessError != null)
                        operationSection.Attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DocumentAccessError, src.DocumentAccessError));
                }

                dst.Sections.AddRange( src.Sections );
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, IIppResponse>((src, dst, map) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                dst.StatusCode = src.StatusCode;
                var operationAttributes = src.Sections.FirstOrDefault(x => x.Tag == SectionTag.OperationAttributesTag)?.Attributes.ToIppDictionary();
                if (operationAttributes != null)
                {
                    dst.StatusMessage = map.MapFromDic<string?>(operationAttributes, JobAttribute.StatusMessage);
                    dst.DetailedStatusMessage = map.MapFromDicSetNull<string[]?>(operationAttributes, JobAttribute.DetailedStatusMessage);
                    dst.DocumentAccessError = map.MapFromDic<string?>(operationAttributes, JobAttribute.DocumentAccessError);
                }
                return dst;
            });

            mapper.CreateMap<IIppResponse, IppResponseMessage>((src, dst, map) =>
            {
                dst.Version = src.Version;
                dst.RequestId = src.RequestId;
                dst.StatusCode = src.StatusCode;
                var section = new IppSection { Tag = SectionTag.OperationAttributesTag };
                section.Attributes.Add(new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"));
                section.Attributes.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"));
                if (src.StatusMessage != null)
                    section.Attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.StatusMessage, src.StatusMessage));
                if (src.DetailedStatusMessage?.Any() ?? false)
                    section.Attributes.AddRange(src.DetailedStatusMessage.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DetailedStatusMessage, x)));
                if (src.DocumentAccessError != null)
                    section.Attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DocumentAccessError, src.DocumentAccessError));
                dst.Sections.Add(section);
                return dst;
            });

            mapper.CreateMap<IIppPrinterRequest, IppRequestMessage>((src, dst, map) =>
            {
                map.Map<IIppRequest, IppRequestMessage>(src, dst);
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, IIppPrinterRequest>( ( src, dst, map ) =>
            {
                map.Map<IIppRequestMessage, IIppRequest>( src, dst );
                return dst;
            } );
        }
    }
}
