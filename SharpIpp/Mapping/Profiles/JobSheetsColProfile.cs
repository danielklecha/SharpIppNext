using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class JobSheetsColProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobSheetsCol>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobSheetsCol>();

            var dst = new JobSheetsCol
            {
                JobSheets = map.MapFromDicNullable<JobSheets?>(src, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
            };

            if (src.ContainsKey(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());

            return dst;
        });
        mapper.CreateMap<JobSheetsCol, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobSheetsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobSheets.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobSheets.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ProofPrint>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<ProofPrint>();

            var dst = new ProofPrint
            {
                ProofPrintCopies = map.MapFromDicNullable<int?>(src, nameof(ProofPrint.ProofPrintCopies).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(ProofPrint.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword))
            };
            if (src.ContainsKey(nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<ProofPrint, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.ProofPrint, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ProofPrintCopies.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(ProofPrint.ProofPrintCopies).ConvertCamelCaseToKebabCase(), src.ProofPrintCopies.Value));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(ProofPrint.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobStorage>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobStorage>();

            return new JobStorage
            {
                JobStorageAccess = map.MapFromDicNullable<JobStorageAccess?>(src, nameof(JobStorage.JobStorageAccess).ConvertCamelCaseToKebabCase()),
                JobStorageDisposition = map.MapFromDicNullable<JobStorageDisposition?>(src, nameof(JobStorage.JobStorageDisposition).ConvertCamelCaseToKebabCase()),
                JobStorageGroup = map.MapFromDicNullable<string?>(src, nameof(JobStorage.JobStorageGroup).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<JobStorage, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobStorage, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobStorageAccess != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobStorage.JobStorageAccess).ConvertCamelCaseToKebabCase(), src.JobStorageAccess.Value.Value));
            if (src.JobStorageDisposition != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobStorage.JobStorageDisposition).ConvertCamelCaseToKebabCase(), src.JobStorageDisposition.Value.Value));
            if (src.JobStorageGroup != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(JobStorage.JobStorageGroup).ConvertCamelCaseToKebabCase(), src.JobStorageGroup));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentAccess>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DocumentAccess>();

            return new DocumentAccess
            {
                AccessOAuthToken = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessOAuthToken).ConvertCamelCaseToKebabCase()),
                AccessOAuthUri = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessOAuthUri).ConvertCamelCaseToKebabCase()),
                AccessPassword = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessPassword).ConvertCamelCaseToKebabCase()),
                AccessPin = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessPin).ConvertCamelCaseToKebabCase()),
                AccessUserName = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessUserName).ConvertCamelCaseToKebabCase()),
                AccessX509Certificate = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessX509Certificate).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<DocumentAccess, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.DocumentAccess, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.AccessOAuthToken != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessOAuthToken).ConvertCamelCaseToKebabCase(), src.AccessOAuthToken));
            if (src.AccessOAuthUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, nameof(DocumentAccess.AccessOAuthUri).ConvertCamelCaseToKebabCase(), src.AccessOAuthUri));
            if (src.AccessPassword != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessPassword).ConvertCamelCaseToKebabCase(), src.AccessPassword));
            if (src.AccessPin != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessPin).ConvertCamelCaseToKebabCase(), src.AccessPin));
            if (src.AccessUserName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentAccess.AccessUserName).ConvertCamelCaseToKebabCase(), src.AccessUserName));
            if (src.AccessX509Certificate != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessX509Certificate).ConvertCamelCaseToKebabCase(), src.AccessX509Certificate));
            return attributes;
        });

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
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.CoverSheetInfo, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.FromName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(CoverSheetInfo.FromName).ConvertCamelCaseToKebabCase(), src.FromName));
            if (src.Logo != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Logo).ConvertCamelCaseToKebabCase(), src.Logo));
            if (src.Message != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Message).ConvertCamelCaseToKebabCase(), src.Message));
            if (src.OrganizationName != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.OrganizationName).ConvertCamelCaseToKebabCase(), src.OrganizationName));
            if (src.Subject != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Subject).ConvertCamelCaseToKebabCase(), src.Subject));
            if (src.ToName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(CoverSheetInfo.ToName).ConvertCamelCaseToKebabCase(), src.ToName));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DestinationUri>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DestinationUri>();

            return new DestinationUri
            {
                DestinationUriValue = map.MapFromDicNullable<string?>(src, "destination-uri"),
                PostDialString = map.MapFromDicNullable<string?>(src, nameof(DestinationUri.PostDialString).ConvertCamelCaseToKebabCase()),
                PreDialString = map.MapFromDicNullable<string?>(src, nameof(DestinationUri.PreDialString).ConvertCamelCaseToKebabCase()),
                T33Subaddress = map.MapFromDicNullable<int?>(src, nameof(DestinationUri.T33Subaddress).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<DestinationUri, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.DestinationUris, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DestinationUriValue != null) attributes.Add(new IppAttribute(Tag.Uri, "destination-uri", src.DestinationUriValue));
            if (src.PostDialString != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DestinationUri.PostDialString).ConvertCamelCaseToKebabCase(), src.PostDialString));
            if (src.PreDialString != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DestinationUri.PreDialString).ConvertCamelCaseToKebabCase(), src.PreDialString));
            if (src.T33Subaddress.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(DestinationUri.T33Subaddress).ConvertCamelCaseToKebabCase(), src.T33Subaddress.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DestinationStatus>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DestinationStatus>();

            return new DestinationStatus
            {
                DestinationUri = map.MapFromDicNullable<string?>(src, "destination-uri"),
                ImagesCompleted = map.MapFromDicNullable<int?>(src, "images-completed"),
                TransmissionStatus = map.MapFromDicNullable<TransmissionStatus?>(src, "transmission-status")
            };
        });

        mapper.CreateMap<DestinationStatus, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.DestinationStatuses, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DestinationUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "destination-uri", src.DestinationUri));
            if (src.ImagesCompleted.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "images-completed", src.ImagesCompleted.Value));
            if (src.TransmissionStatus.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, "transmission-status", (int)src.TransmissionStatus.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DestinationUriReady>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DestinationUriReady>();

            IDictionary<string, IppAttribute[]>[]? destinationAttributes = null;
            if (src.TryGetValue("destination-attributes", out var nestedDestinationAttributes) && nestedDestinationAttributes.GroupBegCollection().Any())
            {
                destinationAttributes = nestedDestinationAttributes
                    .GroupBegCollection()
                    .Select(x => x.FromBegCollection().ToIppDictionary())
                    .ToArray();
            }

            return new DestinationUriReady
            {
                DestinationAttributes = destinationAttributes,
                DestinationAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, "destination-attributes-supported"),
                DestinationInfo = map.MapFromDicNullable<string?>(src, "destination-info"),
                DestinationIsDirectory = map.MapFromDicNullable<bool?>(src, "destination-is-directory"),
                DestinationMandatoryAccessAttributes = map.MapFromDicSetNullable<string[]?>(src, "destination-mandatory-access-attributes"),
                DestinationName = map.MapFromDicNullable<string?>(src, "destination-name"),
                DestinationOAuthScope = map.MapFromDicSetNullable<string[]?>(src, "destination-oauth-scope"),
                DestinationOAuthToken = map.MapFromDicSetNullable<string[]?>(src, "destination-oauth-token"),
                DestinationOAuthUri = map.MapFromDicNullable<Uri?>(src, "destination-oauth-uri"),
                DestinationUri = map.MapFromDicNullable<string?>(src, "destination-uri")
            };
        });

        mapper.CreateMap<DestinationUriReady, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.DestinationUriReady, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DestinationAttributes != null)
            {
                attributes.AddRange(src.DestinationAttributes.SelectMany(x => x.Values.SelectMany(y => y).ToBegCollection("destination-attributes")));
            }

            if (src.DestinationAttributesSupported != null)
                attributes.AddRange(src.DestinationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, "destination-attributes-supported", x)));
            if (src.DestinationInfo != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "destination-info", src.DestinationInfo));
            if (src.DestinationIsDirectory.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "destination-is-directory", src.DestinationIsDirectory.Value));
            if (src.DestinationMandatoryAccessAttributes != null)
                attributes.AddRange(src.DestinationMandatoryAccessAttributes.Select(x => new IppAttribute(Tag.Keyword, "destination-mandatory-access-attributes", x)));
            if (src.DestinationName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "destination-name", src.DestinationName));
            if (src.DestinationOAuthScope != null)
                attributes.AddRange(src.DestinationOAuthScope.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "destination-oauth-scope", x)));
            if (src.DestinationOAuthToken != null)
                attributes.AddRange(src.DestinationOAuthToken.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "destination-oauth-token", x)));
            if (src.DestinationOAuthUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "destination-oauth-uri", src.DestinationOAuthUri.ToString()));
            if (src.DestinationUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "destination-uri", src.DestinationUri));

            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OutputAttributes>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<OutputAttributes>();

            return new OutputAttributes
            {
                NoiseRemoval = map.MapFromDicNullable<int?>(src, nameof(OutputAttributes.NoiseRemoval).ConvertCamelCaseToKebabCase()),
                OutputCompressionQualityFactor = map.MapFromDicNullable<int?>(src, nameof(OutputAttributes.OutputCompressionQualityFactor).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<OutputAttributes, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.OutputAttributes, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.NoiseRemoval.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(OutputAttributes.NoiseRemoval).ConvertCamelCaseToKebabCase(), src.NoiseRemoval.Value));
            if (src.OutputCompressionQualityFactor.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(OutputAttributes.OutputCompressionQualityFactor).ConvertCamelCaseToKebabCase(), src.OutputCompressionQualityFactor.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, Material>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Material>();

            return new Material
            {
                MaterialAmount = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialAmount).ConvertCamelCaseToKebabCase()),
                MaterialColor = map.MapFromDicNullable<MaterialColor?>(src, nameof(Material.MaterialColor).ConvertCamelCaseToKebabCase()),
                MaterialDiameter = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialDiameter).ConvertCamelCaseToKebabCase()),
            MaterialFillDensity = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialFillDensity).ConvertCamelCaseToKebabCase()),
            MaterialKey = map.MapFromDicNullable<MaterialKey?>(src, nameof(Material.MaterialKey).ConvertCamelCaseToKebabCase()),
            MaterialName = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialName).ConvertCamelCaseToKebabCase()),
            MaterialPurpose = map.MapFromDicSetNullable<MaterialPurpose[]?>(src, nameof(Material.MaterialPurpose).ConvertCamelCaseToKebabCase()),
            MaterialRate = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialRate).ConvertCamelCaseToKebabCase()),
            MaterialRateUnits = map.MapFromDicNullable<MaterialRateUnits?>(src, nameof(Material.MaterialRateUnits).ConvertCamelCaseToKebabCase()),
            MaterialShellThickness = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialShellThickness).ConvertCamelCaseToKebabCase()),
            MaterialTemperature = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialTemperature).ConvertCamelCaseToKebabCase()),
            MaterialType = map.MapFromDicNullable<MaterialType?>(src, nameof(Material.MaterialType).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<Material, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.MaterialsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.MaterialAmount.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialAmount).ConvertCamelCaseToKebabCase(), src.MaterialAmount.Value));
            if (src.MaterialColor != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialColor).ConvertCamelCaseToKebabCase(), src.MaterialColor.Value.Value));
            if (src.MaterialDiameter.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialDiameter).ConvertCamelCaseToKebabCase(), src.MaterialDiameter.Value));
            if (src.MaterialFillDensity.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialFillDensity).ConvertCamelCaseToKebabCase(), src.MaterialFillDensity.Value));
            if (src.MaterialKey != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialKey).ConvertCamelCaseToKebabCase(), src.MaterialKey.Value.Value));
            if (src.MaterialName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(Material.MaterialName).ConvertCamelCaseToKebabCase(), src.MaterialName));
            if (src.MaterialPurpose != null) attributes.AddRange(src.MaterialPurpose.Select(x => new IppAttribute(Tag.Keyword, nameof(Material.MaterialPurpose).ConvertCamelCaseToKebabCase(), x.Value)));
            if (src.MaterialRate.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialRate).ConvertCamelCaseToKebabCase(), src.MaterialRate.Value));
            if (src.MaterialRateUnits != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialRateUnits).ConvertCamelCaseToKebabCase(), src.MaterialRateUnits.Value.Value));
            if (src.MaterialShellThickness.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialShellThickness).ConvertCamelCaseToKebabCase(), src.MaterialShellThickness.Value));
            if (src.MaterialTemperature.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialTemperature).ConvertCamelCaseToKebabCase(), src.MaterialTemperature.Value));
            if (src.MaterialType != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialType).ConvertCamelCaseToKebabCase(), src.MaterialType.Value.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintAccuracy>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrintAccuracy>();

            return new PrintAccuracy
            {
                AccuracyUnits = map.MapFromDicNullable<AccuracyUnits?>(src, nameof(PrintAccuracy.AccuracyUnits).ConvertCamelCaseToKebabCase()),
                XAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.XAccuracy).ConvertCamelCaseToKebabCase()),
            YAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.YAccuracy).ConvertCamelCaseToKebabCase()),
            ZAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.ZAccuracy).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<PrintAccuracy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.PrintAccuracy, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.AccuracyUnits != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(PrintAccuracy.AccuracyUnits).ConvertCamelCaseToKebabCase(), src.AccuracyUnits.Value.Value));
            if (src.XAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.XAccuracy).ConvertCamelCaseToKebabCase(), src.XAccuracy.Value));
            if (src.YAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.YAccuracy).ConvertCamelCaseToKebabCase(), src.YAccuracy.Value));
            if (src.ZAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.ZAccuracy).ConvertCamelCaseToKebabCase(), src.ZAccuracy.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintObject>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrintObject>();

            return new PrintObject
            {
                DocumentNumber = map.MapFromDicNullable<int?>(src, nameof(PrintObject.DocumentNumber).ConvertCamelCaseToKebabCase()),
                PrintObjectsSource = map.MapFromDicNullable<string?>(src, "print-objects-source"),
            TransformationMatrix = map.MapFromDicSetNullable<int[]?>(src, nameof(PrintObject.TransformationMatrix).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<PrintObject, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.PrintObjects, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DocumentNumber.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintObject.DocumentNumber).ConvertCamelCaseToKebabCase(), src.DocumentNumber.Value));
            if (src.PrintObjectsSource != null) attributes.Add(new IppAttribute(Tag.Uri, "print-objects-source", src.PrintObjectsSource));
            if (src.TransformationMatrix != null)
                attributes.AddRange(src.TransformationMatrix.Select(x => new IppAttribute(Tag.Integer, nameof(PrintObject.TransformationMatrix).ConvertCamelCaseToKebabCase(), x)));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterVolumeSupported>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrinterVolumeSupported>();

            return new PrinterVolumeSupported
            {
                XDimension = map.MapFromDicNullable<int?>(src, nameof(PrinterVolumeSupported.XDimension).ConvertCamelCaseToKebabCase()),
                YDimension = map.MapFromDicNullable<int?>(src, nameof(PrinterVolumeSupported.YDimension).ConvertCamelCaseToKebabCase()),
                ZDimension = map.MapFromDicNullable<int?>(src, nameof(PrinterVolumeSupported.ZDimension).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<PrinterVolumeSupported, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterVolumeSupported, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.XDimension.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrinterVolumeSupported.XDimension).ConvertCamelCaseToKebabCase(), src.XDimension.Value));
            if (src.YDimension.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrinterVolumeSupported.YDimension).ConvertCamelCaseToKebabCase(), src.YDimension.Value));
            if (src.ZDimension.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrinterVolumeSupported.ZDimension).ConvertCamelCaseToKebabCase(), src.ZDimension.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OverrideInstruction>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<OverrideInstruction>();

            var pageRanges = map.MapFromDicSetNullable<SharpIpp.Protocol.Models.Range[]?>(src, "pages");
            var documentNumberRanges = map.MapFromDicSetNullable<SharpIpp.Protocol.Models.Range[]?>(src, "document-numbers");
            var documentCopyRanges = map.MapFromDicSetNullable<SharpIpp.Protocol.Models.Range[]?>(src, "document-copies");

            var overrideTemplateMembers = src
                .Where(x => x.Key != "pages" && x.Key != "document-numbers" && x.Key != "document-copies")
                .ToDictionary(x => x.Key, x => x.Value);

            JobTemplateAttributes? overrideTemplateAttributes = null;
            if (overrideTemplateMembers.Count > 0)
            {
                var templateRequest = new IppRequestMessage();
                templateRequest.JobAttributes.AddRange(overrideTemplateMembers.Values.SelectMany(x => x));
                overrideTemplateAttributes = map.Map<IIppRequestMessage, JobTemplateAttributes>(templateRequest);
            }

            return new OverrideInstruction
            {
                PageRanges = pageRanges,
                DocumentNumberRanges = documentNumberRanges,
                DocumentCopyRanges = documentCopyRanges,
                JobTemplateAttributes = overrideTemplateAttributes
            };
        });
        mapper.CreateMap<OverrideInstruction, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.Overrides, NoValue.Instance) };

            var pageRanges = src.PageRanges;

            var documentNumberRanges = src.DocumentNumberRanges;

            var documentCopyRanges = src.DocumentCopyRanges;

            var attributes = new List<IppAttribute>();
            if (pageRanges != null)
                attributes.AddRange(pageRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, "pages", x)));
            if (documentNumberRanges != null)
                attributes.AddRange(documentNumberRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, "document-numbers", x)));
            if (documentCopyRanges != null)
                attributes.AddRange(documentCopyRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, "document-copies", x)));

            if (src.JobTemplateAttributes != null)
            {
                var templateRequest = map.Map<IppRequestMessage>(src.JobTemplateAttributes);
                var overrideTemplateMembers = templateRequest.JobAttributes
                    .ToIppDictionary()
                    .Where(x => x.Key != JobAttribute.Overrides && x.Key != JobAttribute.OverridesActual)
                    .SelectMany(x => x.Value);
                attributes.AddRange(overrideTemplateMembers);
            }

            return attributes;
        });
    }
}
