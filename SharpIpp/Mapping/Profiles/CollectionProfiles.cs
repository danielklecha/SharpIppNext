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
                Media = map.MapFromDicNullable<Media?>(src, nameof(Cover.Media).ConvertCamelCaseToKebabCase())
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
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(Cover.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
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
                Media = map.MapFromDicNullable<Media?>(src, nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase())
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
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // JobAccountingSheets
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobAccountingSheets>((src, map) =>
        {
            var dst = new JobAccountingSheets
            {
                JobAccountingOutputBin = map.MapFromDicNullable<OutputBin?>(src, nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase()),
                JobAccountingSheetsType = map.MapFromDicNullable<JobSheetsType?>(src, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<JobAccountingSheets, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.JobAccountingOutputBin != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobAccountingOutputBin)));
            if (src.JobAccountingSheetsType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobAccountingSheetsType.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
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
                Media = map.MapFromDicNullable<Media?>(src, nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase())
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
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // SeparatorSheets
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SeparatorSheets>((src, map) =>
        {
            var dst = new SeparatorSheets
            {
                Media = map.MapFromDicNullable<Media?>(src, nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase()),
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
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()));
            if (src.SeparatorSheetsType != null)
                attributes.AddRange(src.SeparatorSheetsType.Select(x => new IppAttribute(Tag.Keyword, nameof(SeparatorSheets.SeparatorSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(x))));
            return attributes;
        });

        // ClientInfo
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ClientInfo>((src, map) =>
        {
            var dst = new ClientInfo
            {
                ClientName = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientName).ConvertCamelCaseToKebabCase()),
                ClientPatches = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientPatches).ConvertCamelCaseToKebabCase()),
                ClientStringVersion = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientStringVersion).ConvertCamelCaseToKebabCase()),
                ClientVersion = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientVersion).ConvertCamelCaseToKebabCase()),
            };

            var clientType = map.MapFromDicNullable<int?>(src, nameof(ClientInfo.ClientType).ConvertCamelCaseToKebabCase());
            if (clientType.HasValue)
                dst.ClientType = (ClientType)clientType.Value;

            return dst;
        });
        mapper.CreateMap<ClientInfo, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.ClientName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(ClientInfo.ClientName).ConvertCamelCaseToKebabCase(), src.ClientName));
            if (src.ClientPatches != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(ClientInfo.ClientPatches).ConvertCamelCaseToKebabCase(), src.ClientPatches));
            if (src.ClientStringVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(ClientInfo.ClientStringVersion).ConvertCamelCaseToKebabCase(), src.ClientStringVersion));
            if (src.ClientType.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, nameof(ClientInfo.ClientType).ConvertCamelCaseToKebabCase(), (int)src.ClientType.Value));
            if (src.ClientVersion != null)
                attributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, nameof(ClientInfo.ClientVersion).ConvertCamelCaseToKebabCase(), src.ClientVersion));
            return attributes;
        });

        // DocumentFormatDetails
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentFormatDetails>((src, map) =>
        {
            var dst = new DocumentFormatDetails
            {
                DocumentSourceApplicationName = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceApplicationName).ConvertCamelCaseToKebabCase()),
                DocumentSourceApplicationVersion = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceApplicationVersion).ConvertCamelCaseToKebabCase()),
                DocumentSourceOsName = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceOsName).ConvertCamelCaseToKebabCase()),
                DocumentSourceOsVersion = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceOsVersion).ConvertCamelCaseToKebabCase()),
            };
            return dst;
        });
        mapper.CreateMap<DocumentFormatDetails, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.DocumentSourceApplicationName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceApplicationName).ConvertCamelCaseToKebabCase(), src.DocumentSourceApplicationName));
            if (src.DocumentSourceApplicationVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceApplicationVersion).ConvertCamelCaseToKebabCase(), src.DocumentSourceApplicationVersion));
            if (src.DocumentSourceOsName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceOsName).ConvertCamelCaseToKebabCase(), src.DocumentSourceOsName));
            if (src.DocumentSourceOsVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceOsVersion).ConvertCamelCaseToKebabCase(), src.DocumentSourceOsVersion));
            return attributes;
        });

        // JobCounter
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobCounter>((src, map) =>
        {
            return new JobCounter
            {
                Blank = map.MapFromDicNullable<int?>(src, nameof(JobCounter.Blank).ConvertCamelCaseToKebabCase()),
                BlankTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.BlankTwoSided).ConvertCamelCaseToKebabCase()),
                FullColor = map.MapFromDicNullable<int?>(src, nameof(JobCounter.FullColor).ConvertCamelCaseToKebabCase()),
                FullColorTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.FullColorTwoSided).ConvertCamelCaseToKebabCase()),
                HighlightColor = map.MapFromDicNullable<int?>(src, nameof(JobCounter.HighlightColor).ConvertCamelCaseToKebabCase()),
                HighlightColorTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.HighlightColorTwoSided).ConvertCamelCaseToKebabCase()),
                Monochrome = map.MapFromDicNullable<int?>(src, nameof(JobCounter.Monochrome).ConvertCamelCaseToKebabCase()),
                MonochromeTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.MonochromeTwoSided).ConvertCamelCaseToKebabCase()),
            };
        });
        mapper.CreateMap<JobCounter, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.Blank.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.Blank).ConvertCamelCaseToKebabCase(), src.Blank.Value));
            if (src.BlankTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.BlankTwoSided).ConvertCamelCaseToKebabCase(), src.BlankTwoSided.Value));
            if (src.FullColor.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.FullColor).ConvertCamelCaseToKebabCase(), src.FullColor.Value));
            if (src.FullColorTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.FullColorTwoSided).ConvertCamelCaseToKebabCase(), src.FullColorTwoSided.Value));
            if (src.HighlightColor.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.HighlightColor).ConvertCamelCaseToKebabCase(), src.HighlightColor.Value));
            if (src.HighlightColorTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.HighlightColorTwoSided).ConvertCamelCaseToKebabCase(), src.HighlightColorTwoSided.Value));
            if (src.Monochrome.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.Monochrome).ConvertCamelCaseToKebabCase(), src.Monochrome.Value));
            if (src.MonochromeTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.MonochromeTwoSided).ConvertCamelCaseToKebabCase(), src.MonochromeTwoSided.Value));
            return attributes;
        });

        // JobSheetsCol
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobSheetsCol>((src, map) =>
        {
            var dst = new JobSheetsCol
            {
                JobSheets = map.MapFromDicNullable<JobSheets?>(src, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase()),
            };

            if (src.ContainsKey(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());

            return dst;
        });
        mapper.CreateMap<JobSheetsCol, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.JobSheets.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobSheets.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });
    }
}
