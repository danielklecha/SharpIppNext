using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class CollectionProfiles : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        // Cover
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, Cover>((src, map) =>
        {
            var dst = new Cover
            {
                CoverType = map.MapFromDicNullable<CoverType?>(src, nameof(Cover.CoverType).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string?>(src, nameof(Cover.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<Cover, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.CoverType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(Cover.CoverType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.CoverType.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(Cover.Media).ConvertCamelCaseToKebabCase(), src.Media));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // InsertSheet
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, InsertSheet>((src, map) =>
        {
            var dst = new InsertSheet
            {
                InsertAfterPageNumber = map.MapFromDicNullable<int?>(src, nameof(InsertSheet.InsertAfterPageNumber).ConvertCamelCaseToKebabCase()),
                InsertCount = map.MapFromDicNullable<int?>(src, nameof(InsertSheet.InsertCount).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string?>(src, nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<InsertSheet, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.InsertAfterPageNumber.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(InsertSheet.InsertAfterPageNumber).ConvertCamelCaseToKebabCase(), src.InsertAfterPageNumber.Value));
            if (src.InsertCount.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(InsertSheet.InsertCount).ConvertCamelCaseToKebabCase(), src.InsertCount.Value));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase(), src.Media));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // JobAccountingSheets
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobAccountingSheets>((src, map) =>
        {
            var dst = new JobAccountingSheets
            {
                JobAccountingOutputBin = map.MapFromDicNullable<string?>(src, nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase()),
                JobAccountingSheetsType = map.MapFromDicNullable<JobSheetsType?>(src, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string?>(src, nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<JobAccountingSheets, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.JobAccountingOutputBin != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase(), src.JobAccountingOutputBin));
            if (src.JobAccountingSheetsType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobAccountingSheetsType.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase(), src.Media));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // JobErrorSheet
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobErrorSheet>((src, map) =>
        {
            var dst = new JobErrorSheet
            {
                JobErrorSheetType = map.MapFromDicNullable<JobSheetsType?>(src, nameof(JobErrorSheet.JobErrorSheetType).ConvertCamelCaseToKebabCase()),
                JobErrorSheetWhen = map.MapFromDicNullable<JobErrorSheetWhen?>(src, nameof(JobErrorSheet.JobErrorSheetWhen).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string?>(src, nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<JobErrorSheet, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.JobErrorSheetType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.JobErrorSheetType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobErrorSheetType.Value)));
            if (src.JobErrorSheetWhen.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.JobErrorSheetWhen).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobErrorSheetWhen.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase(), src.Media));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // SeparatorSheets
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SeparatorSheets>((src, map) =>
        {
            var dst = new SeparatorSheets
            {
                Media = map.MapFromDicNullable<string?>(src, nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase()),
                SeparatorSheetsType = map.MapFromDicSetNullable<SeparatorSheetsType[]?>(src, nameof(SeparatorSheets.SeparatorSheetsType).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<SeparatorSheets, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase(), src.Media));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()));
            if (src.SeparatorSheetsType != null)
                attributes.AddRange(src.SeparatorSheetsType.Select(x => new IppAttribute(Tag.Keyword, nameof(SeparatorSheets.SeparatorSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(x))));
            return attributes;
        });
    }
}
