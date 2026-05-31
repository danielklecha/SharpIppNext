using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class CoverSheetInfoProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CoverSheetInfo>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<CoverSheetInfo>();

            return new CoverSheetInfo
            {
                FromName = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.FromName).ConvertCamelCaseToKebabCase()),
                Logo = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.Logo).ConvertCamelCaseToKebabCase()),
                Message = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.Message).ConvertCamelCaseToKebabCase()),
                OrganizationName = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.OrganizationName).ConvertCamelCaseToKebabCase()),
                Subject = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.Subject).ConvertCamelCaseToKebabCase()),
                ToName = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.ToName).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<CoverSheetInfo, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.CoverSheetInfo, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.FromName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(CoverSheetInfo.FromName).ConvertCamelCaseToKebabCase(), src.FromName));
            if (src.Logo != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Logo).ConvertCamelCaseToKebabCase(), src.Logo));
            if (src.Message != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Message).ConvertCamelCaseToKebabCase(), src.Message));
            if (src.OrganizationName != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.OrganizationName).ConvertCamelCaseToKebabCase(), src.OrganizationName));
            if (src.Subject != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Subject).ConvertCamelCaseToKebabCase(), src.Subject));
            if (src.ToName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(CoverSheetInfo.ToName).ConvertCamelCaseToKebabCase(), src.ToName));
            return attributes;
        });
    }
}
