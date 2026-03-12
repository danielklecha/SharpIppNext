using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class GetCUPSPrintersResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, CUPSGetPrintersResponse>((src, map) =>
        {
            var dst = new CUPSGetPrintersResponse { PrintersAttributes = map.Map<List<List<IppAttribute>>, PrinterDescriptionAttributes[]>(src.PrinterAttributes) };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<CUPSGetPrintersResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            if (src.PrintersAttributes != null)
                dst.PrinterAttributes.AddRange(map.Map<PrinterDescriptionAttributes[], List<List<IppAttribute>>>(src.PrintersAttributes));
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });

        mapper.CreateMap<List<List<IppAttribute>>, PrinterDescriptionAttributes[]>((src, map) =>
            src.Select(x => map.Map<PrinterDescriptionAttributes>(x.ToIppDictionary()))
                .ToArray());

        mapper.CreateMap<PrinterDescriptionAttributes[], List<List<IppAttribute>>>((src, map) =>
        {
            return src.Select(x =>
            {
                var attrs = new List<IppAttribute>();
                attrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(x).Values.SelectMany(v => v));
                return attrs;
            }).ToList();
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>((src, map) =>
        {
            return new PrinterDescriptionAttributes
            {
                CharsetConfigured = map.MapFromDicNullable<string?>(src, PrinterAttribute.CharsetConfigured),
                CharsetSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.CharsetSupported),
                ColorSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.ColorSupported),
                CompressionSupported = map.MapFromDicSetNullable<Compression[]?>(src, PrinterAttribute.CompressionSupported),
                DocumentFormatDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.DocumentFormatDefault),
                DocumentFormatSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentFormatSupported),
                GeneratedNaturalLanguageSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.GeneratedNaturalLanguageSupported),
                IppVersionsSupported = map.MapFromDicSetNullable<IppVersion[]?>(src, PrinterAttribute.IppVersionsSupported),
                JobImpressionsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobImpressionsSupported),
                JobKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobKOctetsSupported),
                JpegKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JpegKOctetsSupported),
                PdfKOctetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.PdfKOctetsSupported),
                JobMediaSheetsSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobMediaSheetsSupported),
                MultipleDocumentJobsSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.MultipleDocumentJobsSupported),
                MultipleOperationTimeOut = map.MapFromDicNullable<int?>(src, PrinterAttribute.MultipleOperationTimeOut),
                NaturalLanguageConfigured = map.MapFromDicNullable<string?>(src, PrinterAttribute.NaturalLanguageConfigured),
                OperationsSupported = map.MapFromDicSetNullable<IppOperation[]?>(src, PrinterAttribute.OperationsSupported),
                PagesPerMinute = map.MapFromDicNullable<int?>(src, PrinterAttribute.PagesPerMinute),
                PdlOverrideSupported = map.MapFromDicNullable<PdlOverride?>(src, PrinterAttribute.PdlOverrideSupported),
                PagesPerMinuteColor = map.MapFromDicNullable<int?>(src, PrinterAttribute.PagesPerMinuteColor),
                PrinterCurrentTime = map.MapFromDicNullable<DateTimeOffset?>(src, PrinterAttribute.PrinterCurrentTime),
                PrinterDriverInstaller = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterDriverInstaller),
                PrinterInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterInfo),
                PrinterIsAcceptingJobs = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PrinterIsAcceptingJobs),
                PrinterLocation = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterLocation),
                PrinterMakeAndModel = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMakeAndModel),
                PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMessageFromOperator),
                PrinterMoreInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMoreInfo),
                PrinterMoreInfoManufacturer = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMoreInfoManufacturer),
                PrinterName = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterName),
                PrinterState = map.MapFromDicNullable<PrinterState?>(src, PrinterAttribute.PrinterState),
                PrinterStateMessage = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterStateMessage),
                PrinterStateReasons = map.MapFromDicSetNullable<PrinterStateReason[]?>(src, PrinterAttribute.PrinterStateReasons),
                PrinterUpTime = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterUpTime),
                PrinterUriSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterUriSupported),
                PrintScalingDefault = map.MapFromDicNullable<PrintScaling?>(src, PrinterAttribute.PrintScalingDefault),
                PrintScalingSupported = map.MapFromDicSetNullable<PrintScaling[]?>(src, PrinterAttribute.PrintScalingSupported),
                QueuedJobCount = map.MapFromDicNullable<int?>(src, PrinterAttribute.QueuedJobCount),
                ReferenceUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, PrinterAttribute.ReferenceUriSchemesSupported),
                UriAuthenticationSupported = map.MapFromDicSetNullable<UriAuthentication[]?>(src, PrinterAttribute.UriAuthenticationSupported),
                UriSecuritySupported = map.MapFromDicSetNullable<UriSecurity[]?>(src, PrinterAttribute.UriSecuritySupported),
                MediaDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.MediaDefault),
                MediaSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.MediaSupported),
                SidesDefault = map.MapFromDicNullable<Sides?>(src, PrinterAttribute.SidesDefault),
                SidesSupported = map.MapFromDicSetNullable<Sides[]?>(src, PrinterAttribute.SidesSupported),
                FinishingsDefault = map.MapFromDicNullable<Finishings?>(src, PrinterAttribute.FinishingsDefault),
                FinishingsSupported = map.MapFromDicSetNullable<Finishings[]?>(src, PrinterAttribute.FinishingsSupported),
                PrinterResolutionDefault = map.MapFromDicNullable<Resolution?>(src, PrinterAttribute.PrinterResolutionDefault),
                PrinterResolutionSupported = map.MapFromDicSetNullable<Resolution[]?>(src, PrinterAttribute.PrinterResolutionSupported),
                PrintQualityDefault = map.MapFromDicNullable<PrintQuality?>(src, PrinterAttribute.PrintQualityDefault),
                PrintQualitySupported = map.MapFromDicSetNullable<PrintQuality[]?>(src, PrinterAttribute.PrintQualitySupported),
                JobPriorityDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobPriorityDefault),
                JobPrioritySupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobPrioritySupported),
                CopiesDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.CopiesDefault),
                CopiesSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.CopiesSupported),
                OrientationRequestedDefault = map.MapFromDicNullable<Orientation?>(src, PrinterAttribute.OrientationRequestedDefault),
                OrientationRequestedSupported = map.MapFromDicSetNullable<Orientation[]?>(src, PrinterAttribute.OrientationRequestedSupported),
                PageRangesSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PageRangesSupported),
                JobHoldUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, PrinterAttribute.JobHoldUntilDefault),
                JobHoldUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, PrinterAttribute.JobHoldUntilSupported),
                OutputBinDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.OutputBinDefault),
                OutputBinSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.OutputBinSupported),
                MediaColDefault = src.ContainsKey(PrinterAttribute.MediaColDefault) ? map.Map<MediaCol>(src[PrinterAttribute.MediaColDefault].FromBegCollection().ToIppDictionary()) : null,
                PrintColorModeDefault = map.MapFromDicNullable<PrintColorMode?>(src, PrinterAttribute.PrintColorModeDefault),
                PrintColorModeSupported = map.MapFromDicSetNullable<PrintColorMode[]?>(src, PrinterAttribute.PrintColorModeSupported),
                WhichJobsSupported = map.MapFromDicSetNullable<WhichJobs[]?>(src, PrinterAttribute.WhichJobsSupported),
                PrinterUUID = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterUUID),
                DocumentCreationAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentCreationAttributesSupported),
                JobAccountIdDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.JobAccountIdDefault),
                JobAccountIdSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobAccountIdSupported),
                JobAccountingUserIdDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.JobAccountingUserIdDefault),
                JobAccountingUserIdSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobAccountingUserIdSupported),
                JobCancelAfterDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobCancelAfterDefault),
                JobCancelAfterSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.JobCancelAfterSupported),
                JobSpoolingSupported = map.MapFromDicNullable<JobSpooling?>(src, PrinterAttribute.JobSpoolingSupported),
                MaxPageRangesSupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.MaxPageRangesSupported),
                PrintContentOptimizeDefault = map.MapFromDicNullable<PrintContentOptimize?>(src, PrinterAttribute.PrintContentOptimizeDefault),
                PrintContentOptimizeSupported = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, PrinterAttribute.PrintContentOptimizeSupported),
                OutputDeviceSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.OutputDeviceSupported),
                JobCreationAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.JobCreationAttributesSupported),
                FinishingTemplateSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.FinishingTemplateSupported),
                FinishingsColSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.FinishingsColSupported),
                JobPagesPerSetSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobPagesPerSetSupported),
                PunchingHoleDiameterConfigured = map.MapFromDicNullable<int?>(src, PrinterAttribute.PunchingHoleDiameterConfigured),
                PrinterFinisher = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFinisher),
                PrinterFinisherDescription = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFinisherDescription),
                PrinterFinisherSupplies = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFinisherSupplies),
                PrinterFinisherSuppliesDescription = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterFinisherSuppliesDescription),
                FinishingsColDefault = src.ContainsKey(PrinterAttribute.FinishingsColDefault) ? map.Map<FinishingsCol>(src[PrinterAttribute.FinishingsColDefault].FromBegCollection().ToIppDictionary()) : null,
                FinishingsColReady = src.ContainsKey(PrinterAttribute.FinishingsColReady) ? src[PrinterAttribute.FinishingsColReady].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                BalingTypeSupported = map.MapFromDicSetNullable<BalingType[]?>(src, PrinterAttribute.BalingTypeSupported),
                BalingWhenSupported = map.MapFromDicSetNullable<BalingWhen[]?>(src, PrinterAttribute.BalingWhenSupported),
                BindingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.BindingReferenceEdgeSupported),
                BindingTypeSupported = map.MapFromDicSetNullable<BindingType[]?>(src, PrinterAttribute.BindingTypeSupported),
                CoatingSidesSupported = map.MapFromDicSetNullable<CoatingSides[]?>(src, PrinterAttribute.CoatingSidesSupported),
                CoatingTypeSupported = map.MapFromDicSetNullable<CoatingType[]?>(src, PrinterAttribute.CoatingTypeSupported),
                CoveringNameSupported = map.MapFromDicSetNullable<CoveringName[]?>(src, PrinterAttribute.CoveringNameSupported),
                FinishingsColDatabase = src.ContainsKey(PrinterAttribute.FinishingsColDatabase) ? src[PrinterAttribute.FinishingsColDatabase].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                FoldingDirectionSupported = map.MapFromDicSetNullable<FoldingDirection[]?>(src, PrinterAttribute.FoldingDirectionSupported),
                FoldingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.FoldingOffsetSupported),
                FoldingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.FoldingReferenceEdgeSupported),
                LaminatingSidesSupported = map.MapFromDicSetNullable<CoatingSides[]?>(src, PrinterAttribute.LaminatingSidesSupported),
                LaminatingTypeSupported = map.MapFromDicSetNullable<LaminatingType[]?>(src, PrinterAttribute.LaminatingTypeSupported),
                PunchingLocationsSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.PunchingLocationsSupported),
                PunchingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.PunchingOffsetSupported),
                PunchingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.PunchingReferenceEdgeSupported),
                StitchingAngleSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.StitchingAngleSupported),
                StitchingLocationsSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.StitchingLocationsSupported),
                StitchingMethodSupported = map.MapFromDicSetNullable<StitchingMethod[]?>(src, PrinterAttribute.StitchingMethodSupported),
                StitchingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.StitchingOffsetSupported),
                StitchingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.StitchingReferenceEdgeSupported),
                TrimmingOffsetSupported = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(src, PrinterAttribute.TrimmingOffsetSupported),
                TrimmingReferenceEdgeSupported = map.MapFromDicSetNullable<FinishingReferenceEdge[]?>(src, PrinterAttribute.TrimmingReferenceEdgeSupported),
                TrimmingTypeSupported = map.MapFromDicSetNullable<TrimmingType[]?>(src, PrinterAttribute.TrimmingTypeSupported),
                TrimmingWhenSupported = map.MapFromDicSetNullable<TrimmingWhen[]?>(src, PrinterAttribute.TrimmingWhenSupported),
                CoverBackDefault = src.ContainsKey(PrinterAttribute.CoverBackDefault) ? map.Map<Cover>(src[PrinterAttribute.CoverBackDefault].FromBegCollection().ToIppDictionary()) : null,
                CoverBackSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.CoverBackSupported),
                CoverFrontDefault = src.ContainsKey(PrinterAttribute.CoverFrontDefault) ? map.Map<Cover>(src[PrinterAttribute.CoverFrontDefault].FromBegCollection().ToIppDictionary()) : null,
                CoverFrontSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.CoverFrontSupported),
                CoverTypeSupported = map.MapFromDicSetNullable<CoverType[]?>(src, PrinterAttribute.CoverTypeSupported),
                ForceFrontSideSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.ForceFrontSideSupported),
                ImageOrientationDefault = map.MapFromDicNullable<Orientation?>(src, PrinterAttribute.ImageOrientationDefault),
                ImageOrientationSupported = map.MapFromDicSetNullable<Orientation[]?>(src, PrinterAttribute.ImageOrientationSupported),
                ImpositionTemplateDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.ImpositionTemplateDefault),
                ImpositionTemplateSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.ImpositionTemplateSupported),
                InsertCountSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.InsertCountSupported),
                InsertSheetDefault = src.TryGetValue(PrinterAttribute.InsertSheetDefault, out var insertsheetdefault) && insertsheetdefault.GroupBegCollection().Any() ? insertsheetdefault.GroupBegCollection().Select(x => map.Map<InsertSheet>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                InsertSheetSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.InsertSheetSupported),
                JobAccountingOutputBinSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.JobAccountingOutputBinSupported),
                JobAccountingSheetsDefault = src.ContainsKey(PrinterAttribute.JobAccountingSheetsDefault) ? map.Map<JobAccountingSheets>(src[PrinterAttribute.JobAccountingSheetsDefault].FromBegCollection().ToIppDictionary()) : null,
                JobAccountingSheetsSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.JobAccountingSheetsSupported),
                JobAccountingSheetsTypeSupported = map.MapFromDicSetNullable<JobSheetsType[]?>(src, PrinterAttribute.JobAccountingSheetsTypeSupported),
                JobCompleteBeforeSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, PrinterAttribute.JobCompleteBeforeSupported),
                JobCompleteBeforeTimeSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobCompleteBeforeTimeSupported),
                JobErrorSheetDefault = src.ContainsKey(PrinterAttribute.JobErrorSheetDefault) ? map.Map<JobErrorSheet>(src[PrinterAttribute.JobErrorSheetDefault].FromBegCollection().ToIppDictionary()) : null,
                JobErrorSheetSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.JobErrorSheetSupported),
                JobErrorSheetTypeSupported = map.MapFromDicSetNullable<JobSheetsType[]?>(src, PrinterAttribute.JobErrorSheetTypeSupported),
                JobErrorSheetWhenSupported = map.MapFromDicSetNullable<JobErrorSheetWhen[]?>(src, PrinterAttribute.JobErrorSheetWhenSupported),
                JobMessageToOperatorSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobMessageToOperatorSupported),
                JobPhoneNumberDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.JobPhoneNumberDefault),
                JobPhoneNumberSchemeSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.JobPhoneNumberSchemeSupported),
                JobPhoneNumberSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobPhoneNumberSupported),
                JobRecipientNameSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobRecipientNameSupported),
                JobSheetMessageSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.JobSheetMessageSupported),
                PresentationDirectionNumberUpDefault = map.MapFromDicNullable<PresentationDirectionNumberUp?>(src, PrinterAttribute.PresentationDirectionNumberUpDefault),
                PresentationDirectionNumberUpSupported = map.MapFromDicSetNullable<PresentationDirectionNumberUp[]?>(src, PrinterAttribute.PresentationDirectionNumberUpSupported),
                SeparatorSheetsDefault = src.ContainsKey(PrinterAttribute.SeparatorSheetsDefault) ? map.Map<SeparatorSheets>(src[PrinterAttribute.SeparatorSheetsDefault].FromBegCollection().ToIppDictionary()) : null,
                SeparatorSheetsSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.SeparatorSheetsSupported),
                SeparatorSheetsTypeSupported = map.MapFromDicSetNullable<SeparatorSheetsType[]?>(src, PrinterAttribute.SeparatorSheetsTypeSupported),
                XImagePositionDefault = map.MapFromDicNullable<XImagePosition?>(src, PrinterAttribute.XImagePositionDefault),
                XImagePositionSupported = map.MapFromDicSetNullable<XImagePosition[]?>(src, PrinterAttribute.XImagePositionSupported),
                XImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.XImageShiftDefault),
                XImageShiftSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.XImageShiftSupported),
                XSide1ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.XSide1ImageShiftDefault),
                XSide2ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.XSide2ImageShiftDefault),
                YImagePositionDefault = map.MapFromDicNullable<YImagePosition?>(src, PrinterAttribute.YImagePositionDefault),
                YImagePositionSupported = map.MapFromDicSetNullable<YImagePosition[]?>(src, PrinterAttribute.YImagePositionSupported),
                YImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.YImageShiftDefault),
                YImageShiftSupported = map.MapFromDicNullable<Protocol.Models.Range?>(src, PrinterAttribute.YImageShiftSupported),
                YSide1ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.YSide1ImageShiftDefault),
                YSide2ImageShiftDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.YSide2ImageShiftDefault),
            };
        });

        mapper.CreateMap<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>((src, map) =>
            {
                var dic = new Dictionary<string, IppAttribute[]>();
                if (src.CharsetConfigured != null)
                    dic.Add(PrinterAttribute.CharsetConfigured, new IppAttribute[] { new IppAttribute(Tag.Charset, PrinterAttribute.CharsetConfigured, src.CharsetConfigured) });
                if (src.CharsetSupported != null)
                    dic.Add(PrinterAttribute.CharsetSupported, src.CharsetSupported.Select(x => new IppAttribute(Tag.Charset, PrinterAttribute.CharsetSupported, x)).ToArray());
                if (src.ColorSupported != null)
                    dic.Add(PrinterAttribute.ColorSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.ColorSupported, src.ColorSupported.Value) });
                if (src.CompressionSupported != null)
                    dic.Add(PrinterAttribute.CompressionSupported, src.CompressionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CompressionSupported, map.Map<string>(x))).ToArray());
                if (src.DocumentFormatDefault != null)
                    dic.Add(PrinterAttribute.DocumentFormatDefault, new IppAttribute[] { new IppAttribute(Tag.MimeMediaType, PrinterAttribute.DocumentFormatDefault, src.DocumentFormatDefault) });
                if (src.DocumentFormatSupported != null)
                    dic.Add(PrinterAttribute.DocumentFormatSupported, src.DocumentFormatSupported.Select(x => new IppAttribute(Tag.MimeMediaType, PrinterAttribute.DocumentFormatSupported, x)).ToArray());
                if (src.GeneratedNaturalLanguageSupported != null)
                    dic.Add(PrinterAttribute.GeneratedNaturalLanguageSupported, src.GeneratedNaturalLanguageSupported.Select(x => new IppAttribute(Tag.NaturalLanguage, PrinterAttribute.GeneratedNaturalLanguageSupported, x)).ToArray());
                if (src.IppVersionsSupported != null)
                    dic.Add(PrinterAttribute.IppVersionsSupported, src.IppVersionsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.IppVersionsSupported, x.ToString())).ToArray());
                if (src.JobImpressionsSupported != null)
                    dic.Add(PrinterAttribute.JobImpressionsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobImpressionsSupported, src.JobImpressionsSupported.Value) });
                if (src.JobKOctetsSupported != null)
                    dic.Add(PrinterAttribute.JobKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobKOctetsSupported, src.JobKOctetsSupported.Value) });
                if (src.JpegKOctetsSupported != null)
                    dic.Add(PrinterAttribute.JpegKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JpegKOctetsSupported, src.JpegKOctetsSupported.Value) });
                if (src.PdfKOctetsSupported != null)
                    dic.Add(PrinterAttribute.PdfKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PdfKOctetsSupported, src.PdfKOctetsSupported.Value) });
                if (src.JobMediaSheetsSupported != null)
                    dic.Add(PrinterAttribute.JobMediaSheetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobMediaSheetsSupported, src.JobMediaSheetsSupported.Value) });
                if (src.MultipleDocumentJobsSupported != null)
                    dic.Add(PrinterAttribute.MultipleDocumentJobsSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.MultipleDocumentJobsSupported, src.MultipleDocumentJobsSupported.Value) });
                if (src.MultipleOperationTimeOut != null)
                    dic.Add(PrinterAttribute.MultipleOperationTimeOut, new IppAttribute[] { new IppAttribute(Tag.Integer, PrinterAttribute.MultipleOperationTimeOut, src.MultipleOperationTimeOut.Value) });
                if (src.NaturalLanguageConfigured != null)
                    dic.Add(PrinterAttribute.NaturalLanguageConfigured, new IppAttribute[] { new IppAttribute(Tag.NaturalLanguage, PrinterAttribute.NaturalLanguageConfigured, src.NaturalLanguageConfigured) });
                if (src.OperationsSupported != null)
                    dic.Add(PrinterAttribute.OperationsSupported, src.OperationsSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.OperationsSupported, (int)x)).ToArray());
                if (src.PagesPerMinute != null)
                    dic.Add(PrinterAttribute.PagesPerMinute, new IppAttribute[] { new IppAttribute(Tag.Integer, PrinterAttribute.PagesPerMinute, src.PagesPerMinute.Value) });
                if (src.PdlOverrideSupported != null)
                    dic.Add(PrinterAttribute.PdlOverrideSupported, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PdlOverrideSupported, src.PdlOverrideSupported) });
                if (src.PagesPerMinuteColor != null)
                    dic.Add(PrinterAttribute.PagesPerMinuteColor, [new IppAttribute(Tag.Integer, PrinterAttribute.PagesPerMinuteColor, src.PagesPerMinuteColor.Value)]);
                if (src.PrinterCurrentTime != null)
                    dic.Add(PrinterAttribute.PrinterCurrentTime, new IppAttribute[] { new IppAttribute(Tag.DateTime, PrinterAttribute.PrinterCurrentTime, src.PrinterCurrentTime.Value) });
                if (src.PrinterDriverInstaller != null)
                    dic.Add(PrinterAttribute.PrinterDriverInstaller, new IppAttribute[] { new IppAttribute(Tag.Uri, PrinterAttribute.PrinterDriverInstaller, src.PrinterDriverInstaller) });
                if (src.PrinterInfo != null)
                    dic.Add(PrinterAttribute.PrinterInfo, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterInfo, src.PrinterInfo) });
                if (src.PrinterIsAcceptingJobs != null)
                    dic.Add(PrinterAttribute.PrinterIsAcceptingJobs, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.PrinterIsAcceptingJobs, src.PrinterIsAcceptingJobs.Value) });
                if (src.PrinterLocation != null)
                    dic.Add(PrinterAttribute.PrinterLocation, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterLocation, src.PrinterLocation) });
                if (src.PrinterMakeAndModel != null)
                    dic.Add(PrinterAttribute.PrinterMakeAndModel, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterMakeAndModel, src.PrinterMakeAndModel) });
                if (src.PrinterMessageFromOperator != null)
                    dic.Add(PrinterAttribute.PrinterMessageFromOperator, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterMessageFromOperator, src.PrinterMessageFromOperator) });
                if (src.PrinterMoreInfo != null)
                    dic.Add(PrinterAttribute.PrinterMoreInfo, new IppAttribute[] { new IppAttribute(Tag.Uri, PrinterAttribute.PrinterMoreInfo, src.PrinterMoreInfo) });
                if (src.PrinterMoreInfoManufacturer != null)
                    dic.Add(PrinterAttribute.PrinterMoreInfoManufacturer, new IppAttribute[] { new IppAttribute(Tag.Uri, PrinterAttribute.PrinterMoreInfoManufacturer, src.PrinterMoreInfoManufacturer) });
                if (src.PrinterName != null)
                    dic.Add(PrinterAttribute.PrinterName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.PrinterName, src.PrinterName) });
                if (src.PrinterState != null)
                    dic.Add(PrinterAttribute.PrinterState, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.PrinterState, (int)src.PrinterState.Value) });
                if (src.PrinterStateMessage != null)
                    dic.Add(PrinterAttribute.PrinterStateMessage, new IppAttribute[] { new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterStateMessage, src.PrinterStateMessage) });
                if (src.PrinterStateReasons != null)
                    dic.Add(PrinterAttribute.PrinterStateReasons, src.PrinterStateReasons.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterStateReasons, x)).ToArray());
                if (src.PrinterUpTime != null)
                    dic.Add(PrinterAttribute.PrinterUpTime, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterUpTime, src.PrinterUpTime.Value)]);
                if (src.PrinterUriSupported != null)
                    dic.Add(PrinterAttribute.PrinterUriSupported, src.PrinterUriSupported.Select(x => new IppAttribute(Tag.Uri, PrinterAttribute.PrinterUriSupported, x)).ToArray());
                if (src.PrintScalingDefault != null)
                    dic.Add(PrinterAttribute.PrintScalingDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrintScalingDefault, map.Map<string>(src.PrintScalingDefault)) });
                if (src.PrintScalingSupported != null)
                    dic.Add(PrinterAttribute.PrintScalingSupported, src.PrintScalingSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintScalingSupported, map.Map<string>(x))).ToArray());
                if (src.QueuedJobCount != null)
                    dic.Add(PrinterAttribute.QueuedJobCount, [new IppAttribute(Tag.Integer, PrinterAttribute.QueuedJobCount, src.QueuedJobCount.Value)]);
                if (src.ReferenceUriSchemesSupported != null)
                    dic.Add(PrinterAttribute.ReferenceUriSchemesSupported, src.ReferenceUriSchemesSupported.Select(x => new IppAttribute(Tag.UriScheme, PrinterAttribute.ReferenceUriSchemesSupported, map.Map<string>(x))).ToArray());
                if (src.UriAuthenticationSupported != null)
                    dic.Add(PrinterAttribute.UriAuthenticationSupported, src.UriAuthenticationSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.UriAuthenticationSupported, map.Map<string>(x))).ToArray());
                if (src.UriSecuritySupported != null)
                    dic.Add(PrinterAttribute.UriSecuritySupported, src.UriSecuritySupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.UriSecuritySupported, map.Map<string>(x))).ToArray());
                if (src.MediaDefault != null)
                    dic.Add(PrinterAttribute.MediaDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.MediaDefault, src.MediaDefault) });
                if (src.MediaSupported != null)
                    dic.Add(PrinterAttribute.MediaSupported, src.MediaSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.MediaSupported, x)).ToArray());
                if (src.SidesDefault != null)
                    dic.Add(PrinterAttribute.SidesDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.SidesDefault, map.Map<string>(src.SidesDefault)) });
                if (src.SidesSupported != null)
                    dic.Add(PrinterAttribute.SidesSupported, src.SidesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.SidesSupported, map.Map<string>(x))).ToArray());
                if (src.FinishingsDefault != null)
                    dic.Add(PrinterAttribute.FinishingsDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.FinishingsDefault, (int)src.FinishingsDefault.Value) });
                if (src.FinishingsSupported != null)
                    dic.Add(PrinterAttribute.FinishingsSupported, src.FinishingsSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.FinishingsSupported, (int)x)).ToArray());
                if (src.PrinterResolutionDefault != null)
                    dic.Add(PrinterAttribute.PrinterResolutionDefault, new IppAttribute[] { new IppAttribute(Tag.Resolution, PrinterAttribute.PrinterResolutionDefault, src.PrinterResolutionDefault.Value) });
                if (src.PrinterResolutionSupported != null)
                    dic.Add(PrinterAttribute.PrinterResolutionSupported, src.PrinterResolutionSupported.Select(x => new IppAttribute(Tag.Resolution, PrinterAttribute.PrinterResolutionSupported, x)).ToArray());
                if (src.PrintQualityDefault != null)
                    dic.Add(PrinterAttribute.PrintQualityDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.PrintQualityDefault, (int)src.PrintQualityDefault.Value) });
                if (src.PrintQualitySupported != null)
                    dic.Add(PrinterAttribute.PrintQualitySupported, src.PrintQualitySupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.PrintQualitySupported, (int)x)).ToArray());
                if (src.JobPriorityDefault != null)
                    dic.Add(PrinterAttribute.JobPriorityDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.JobPriorityDefault, src.JobPriorityDefault.Value)]);
                if (src.JobPrioritySupported != null)
                    dic.Add(PrinterAttribute.JobPrioritySupported, [new IppAttribute(Tag.Integer, PrinterAttribute.JobPrioritySupported, src.JobPrioritySupported.Value)]);
                if (src.CopiesDefault != null)
                    dic.Add(PrinterAttribute.CopiesDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.CopiesDefault, src.CopiesDefault.Value)]);
                if (src.CopiesSupported != null)
                    dic.Add(PrinterAttribute.CopiesSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.CopiesSupported, src.CopiesSupported.Value) });
                if (src.OrientationRequestedDefault != null)
                    dic.Add(PrinterAttribute.OrientationRequestedDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.OrientationRequestedDefault, (int)src.OrientationRequestedDefault.Value) });
                if (src.OrientationRequestedSupported != null)
                    dic.Add(PrinterAttribute.OrientationRequestedSupported, src.OrientationRequestedSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.OrientationRequestedSupported, (int)x)).ToArray());
                if (src.PageRangesSupported != null)
                    dic.Add(PrinterAttribute.PageRangesSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.PageRangesSupported, src.PageRangesSupported.Value) });
                if (src.JobHoldUntilDefault != null)
                    dic.Add(PrinterAttribute.JobHoldUntilDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.JobHoldUntilDefault, map.Map<string>(src.JobHoldUntilDefault.Value)) });
                if (src.JobHoldUntilSupported != null)
                    dic.Add(PrinterAttribute.JobHoldUntilSupported, src.JobHoldUntilSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobHoldUntilSupported, map.Map<string>(x))).ToArray());
                if (src.OutputBinDefault != null)
                    dic.Add(PrinterAttribute.OutputBinDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.OutputBinDefault, src.OutputBinDefault) });
                if (src.OutputBinSupported != null)
                    dic.Add(PrinterAttribute.OutputBinSupported, src.OutputBinSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.OutputBinSupported, x)).ToArray());
                if (src.MediaColDefault != null)
                    dic.Add(PrinterAttribute.MediaColDefault, map.Map<IEnumerable<IppAttribute>>(src.MediaColDefault).ToBegCollection(PrinterAttribute.MediaColDefault).ToArray());
                if (src.PrintColorModeDefault != null)
                    dic.Add(PrinterAttribute.PrintColorModeDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrintColorModeDefault, map.Map<string>(src.PrintColorModeDefault.Value)) });
                if (src.PrintColorModeSupported != null)
                    dic.Add(PrinterAttribute.PrintColorModeSupported, src.PrintColorModeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintColorModeSupported, map.Map<string>(x))).ToArray());
                if (src.WhichJobsSupported != null)
                    dic.Add(PrinterAttribute.WhichJobsSupported, src.WhichJobsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.WhichJobsSupported, map.Map<string>(x))).ToArray());
                if (src.PrinterUUID != null)
                    dic.Add(PrinterAttribute.PrinterUUID, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterUUID, src.PrinterUUID) });
                if (src.DocumentCreationAttributesSupported != null)
                    dic.Add(PrinterAttribute.DocumentCreationAttributesSupported, src.DocumentCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.DocumentCreationAttributesSupported, x)).ToArray());
                if (src.JobAccountIdDefault != null)
                    dic.Add(PrinterAttribute.JobAccountIdDefault, [new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.JobAccountIdDefault, src.JobAccountIdDefault)]);
                if (src.JobAccountIdSupported != null)
                    dic.Add(PrinterAttribute.JobAccountIdSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobAccountIdSupported, src.JobAccountIdSupported.Value)]);
                if (src.JobAccountingUserIdDefault != null)
                    dic.Add(PrinterAttribute.JobAccountingUserIdDefault, [new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.JobAccountingUserIdDefault, src.JobAccountingUserIdDefault)]);
                if (src.JobAccountingUserIdSupported != null)
                    dic.Add(PrinterAttribute.JobAccountingUserIdSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobAccountingUserIdSupported, src.JobAccountingUserIdSupported.Value)]);
                if (src.JobCancelAfterDefault != null)
                    dic.Add(PrinterAttribute.JobCancelAfterDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.JobCancelAfterDefault, src.JobCancelAfterDefault.Value)]);
                if (src.JobCancelAfterSupported != null)
                    dic.Add(PrinterAttribute.JobCancelAfterSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JobCancelAfterSupported, src.JobCancelAfterSupported.Value)]);
                if (src.JobSpoolingSupported != null)
                    dic.Add(PrinterAttribute.JobSpoolingSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobSpoolingSupported, src.JobSpoolingSupported)]);
                if (src.MaxPageRangesSupported != null)
                    dic.Add(PrinterAttribute.MaxPageRangesSupported, [new IppAttribute(Tag.Integer, PrinterAttribute.MaxPageRangesSupported, src.MaxPageRangesSupported.Value)]);
                if (src.PrintContentOptimizeDefault != null)
                    dic.Add(PrinterAttribute.PrintContentOptimizeDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrintContentOptimizeDefault, src.PrintContentOptimizeDefault)]);
                if (src.PrintContentOptimizeSupported != null)
                    dic.Add(PrinterAttribute.PrintContentOptimizeSupported, src.PrintContentOptimizeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintContentOptimizeSupported, x)).ToArray());
                if (src.OutputDeviceSupported != null)
                    dic.Add(PrinterAttribute.OutputDeviceSupported, src.OutputDeviceSupported.Select(x => new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.OutputDeviceSupported, x)).ToArray());
                if (src.JobCreationAttributesSupported != null)
                    dic.Add(PrinterAttribute.JobCreationAttributesSupported, src.JobCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobCreationAttributesSupported, x)).ToArray());
                if (src.FinishingTemplateSupported != null)
                    dic.Add(PrinterAttribute.FinishingTemplateSupported, src.FinishingTemplateSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FinishingTemplateSupported, x)).ToArray());
                if (src.FinishingsColSupported != null)
                    dic.Add(PrinterAttribute.FinishingsColSupported, src.FinishingsColSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FinishingsColSupported, x)).ToArray());
                if (src.JobPagesPerSetSupported != null)
                    dic.Add(PrinterAttribute.JobPagesPerSetSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobPagesPerSetSupported, src.JobPagesPerSetSupported.Value)]);
                if (src.PunchingHoleDiameterConfigured != null)
                    dic.Add(PrinterAttribute.PunchingHoleDiameterConfigured, [new IppAttribute(Tag.Integer, PrinterAttribute.PunchingHoleDiameterConfigured, src.PunchingHoleDiameterConfigured.Value)]);
                if (src.PrinterFinisher != null)
                    dic.Add(PrinterAttribute.PrinterFinisher, src.PrinterFinisher.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterFinisher, x)).ToArray());
                if (src.PrinterFinisherDescription != null)
                    dic.Add(PrinterAttribute.PrinterFinisherDescription, src.PrinterFinisherDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterFinisherDescription, x)).ToArray());
                if (src.PrinterFinisherSupplies != null)
                    dic.Add(PrinterAttribute.PrinterFinisherSupplies, src.PrinterFinisherSupplies.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterFinisherSupplies, x)).ToArray());
                if (src.PrinterFinisherSuppliesDescription != null)
                    dic.Add(PrinterAttribute.PrinterFinisherSuppliesDescription, src.PrinterFinisherSuppliesDescription.Select(x => new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterFinisherSuppliesDescription, x)).ToArray());
                if (src.FinishingsColDefault != null)
                    dic.Add(PrinterAttribute.FinishingsColDefault, map.Map<IEnumerable<IppAttribute>>(src.FinishingsColDefault).ToBegCollection(PrinterAttribute.FinishingsColDefault).ToArray());
                if (src.FinishingsColReady != null)
                    dic.Add(PrinterAttribute.FinishingsColReady, src.FinishingsColReady.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.FinishingsColReady)).ToArray());
                if (src.BalingTypeSupported != null)
                    dic.Add(PrinterAttribute.BalingTypeSupported, src.BalingTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.BalingTypeSupported, x)).ToArray());
                if (src.BalingWhenSupported != null)
                    dic.Add(PrinterAttribute.BalingWhenSupported, src.BalingWhenSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.BalingWhenSupported, map.Map<string>(x))).ToArray());
                if (src.BindingReferenceEdgeSupported != null)
                    dic.Add(PrinterAttribute.BindingReferenceEdgeSupported, src.BindingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.BindingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
                if (src.BindingTypeSupported != null)
                    dic.Add(PrinterAttribute.BindingTypeSupported, src.BindingTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.BindingTypeSupported, x)).ToArray());
                if (src.CoatingSidesSupported != null)
                    dic.Add(PrinterAttribute.CoatingSidesSupported, src.CoatingSidesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoatingSidesSupported, map.Map<string>(x))).ToArray());
                if (src.CoatingTypeSupported != null)
                    dic.Add(PrinterAttribute.CoatingTypeSupported, src.CoatingTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoatingTypeSupported, x)).ToArray());
                if (src.CoveringNameSupported != null)
                    dic.Add(PrinterAttribute.CoveringNameSupported, src.CoveringNameSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoveringNameSupported, x)).ToArray());
                if (src.FinishingsColDatabase != null)
                    dic.Add(PrinterAttribute.FinishingsColDatabase, src.FinishingsColDatabase.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.FinishingsColDatabase)).ToArray());
                if (src.FoldingDirectionSupported != null)
                    dic.Add(PrinterAttribute.FoldingDirectionSupported, src.FoldingDirectionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FoldingDirectionSupported, map.Map<string>(x))).ToArray());
                if (src.FoldingOffsetSupported != null)
                    dic.Add(PrinterAttribute.FoldingOffsetSupported, src.FoldingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.FoldingOffsetSupported, x)).ToArray());
                if (src.FoldingReferenceEdgeSupported != null)
                    dic.Add(PrinterAttribute.FoldingReferenceEdgeSupported, src.FoldingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.FoldingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
                if (src.LaminatingSidesSupported != null)
                    dic.Add(PrinterAttribute.LaminatingSidesSupported, src.LaminatingSidesSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.LaminatingSidesSupported, map.Map<string>(x))).ToArray());
                if (src.LaminatingTypeSupported != null)
                    dic.Add(PrinterAttribute.LaminatingTypeSupported, src.LaminatingTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.LaminatingTypeSupported, x)).ToArray());
                if (src.PunchingLocationsSupported != null)
                    dic.Add(PrinterAttribute.PunchingLocationsSupported, src.PunchingLocationsSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PunchingLocationsSupported, x)).ToArray());
                if (src.PunchingOffsetSupported != null)
                    dic.Add(PrinterAttribute.PunchingOffsetSupported, src.PunchingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PunchingOffsetSupported, x)).ToArray());
                if (src.PunchingReferenceEdgeSupported != null)
                    dic.Add(PrinterAttribute.PunchingReferenceEdgeSupported, src.PunchingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PunchingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
                if (src.StitchingAngleSupported != null)
                    dic.Add(PrinterAttribute.StitchingAngleSupported, src.StitchingAngleSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.StitchingAngleSupported, x)).ToArray());
                if (src.StitchingLocationsSupported != null)
                    dic.Add(PrinterAttribute.StitchingLocationsSupported, src.StitchingLocationsSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.StitchingLocationsSupported, x)).ToArray());
                if (src.StitchingMethodSupported != null)
                    dic.Add(PrinterAttribute.StitchingMethodSupported, src.StitchingMethodSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.StitchingMethodSupported, map.Map<string>(x))).ToArray());
                if (src.StitchingOffsetSupported != null)
                    dic.Add(PrinterAttribute.StitchingOffsetSupported, src.StitchingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.StitchingOffsetSupported, x)).ToArray());
                if (src.StitchingReferenceEdgeSupported != null)
                    dic.Add(PrinterAttribute.StitchingReferenceEdgeSupported, src.StitchingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.StitchingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
                if (src.TrimmingOffsetSupported != null)
                    dic.Add(PrinterAttribute.TrimmingOffsetSupported, src.TrimmingOffsetSupported.Select(x => new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.TrimmingOffsetSupported, x)).ToArray());
                if (src.TrimmingReferenceEdgeSupported != null)
                    dic.Add(PrinterAttribute.TrimmingReferenceEdgeSupported, src.TrimmingReferenceEdgeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.TrimmingReferenceEdgeSupported, map.Map<string>(x))).ToArray());
                if (src.TrimmingTypeSupported != null)
                    dic.Add(PrinterAttribute.TrimmingTypeSupported, src.TrimmingTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.TrimmingTypeSupported, map.Map<string>(x))).ToArray());
                if (src.TrimmingWhenSupported != null)
                    dic.Add(PrinterAttribute.TrimmingWhenSupported, src.TrimmingWhenSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.TrimmingWhenSupported, map.Map<string>(x))).ToArray());
                if (src.CoverBackDefault != null)
                    dic.Add(PrinterAttribute.CoverBackDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverBackDefault).ToBegCollection(PrinterAttribute.CoverBackDefault).ToArray());
                if (src.CoverBackSupported != null)
                    dic.Add(PrinterAttribute.CoverBackSupported, src.CoverBackSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoverBackSupported, x)).ToArray());
                if (src.CoverFrontDefault != null)
                    dic.Add(PrinterAttribute.CoverFrontDefault, map.Map<IEnumerable<IppAttribute>>(src.CoverFrontDefault).ToBegCollection(PrinterAttribute.CoverFrontDefault).ToArray());
                if (src.CoverFrontSupported != null)
                    dic.Add(PrinterAttribute.CoverFrontSupported, src.CoverFrontSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoverFrontSupported, x)).ToArray());
                if (src.CoverTypeSupported != null)
                    dic.Add(PrinterAttribute.CoverTypeSupported, src.CoverTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.CoverTypeSupported, map.Map<string>(x))).ToArray());
                if (src.ForceFrontSideSupported != null)
                    dic.Add(PrinterAttribute.ForceFrontSideSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.ForceFrontSideSupported, src.ForceFrontSideSupported.Value)]);
                if (src.ImageOrientationDefault != null)
                    dic.Add(PrinterAttribute.ImageOrientationDefault, [new IppAttribute(Tag.Enum, PrinterAttribute.ImageOrientationDefault, (int)src.ImageOrientationDefault.Value)]);
                if (src.ImageOrientationSupported != null)
                    dic.Add(PrinterAttribute.ImageOrientationSupported, src.ImageOrientationSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.ImageOrientationSupported, (int)x)).ToArray());
                if (src.ImpositionTemplateDefault != null)
                    dic.Add(PrinterAttribute.ImpositionTemplateDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.ImpositionTemplateDefault, src.ImpositionTemplateDefault)]);
                if (src.ImpositionTemplateSupported != null)
                    dic.Add(PrinterAttribute.ImpositionTemplateSupported, src.ImpositionTemplateSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.ImpositionTemplateSupported, x)).ToArray());
                if (src.InsertCountSupported != null)
                    dic.Add(PrinterAttribute.InsertCountSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.InsertCountSupported, src.InsertCountSupported.Value)]);
                if (src.InsertSheetDefault != null)
                    dic.Add(PrinterAttribute.InsertSheetDefault, src.InsertSheetDefault.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.InsertSheetDefault)).ToArray());
                if (src.InsertSheetSupported != null)
                    dic.Add(PrinterAttribute.InsertSheetSupported, src.InsertSheetSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.InsertSheetSupported, x)).ToArray());
                if (src.JobAccountingOutputBinSupported != null)
                    dic.Add(PrinterAttribute.JobAccountingOutputBinSupported, src.JobAccountingOutputBinSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobAccountingOutputBinSupported, x)).ToArray());
                if (src.JobAccountingSheetsDefault != null)
                    dic.Add(PrinterAttribute.JobAccountingSheetsDefault, map.Map<IEnumerable<IppAttribute>>(src.JobAccountingSheetsDefault).ToBegCollection(PrinterAttribute.JobAccountingSheetsDefault).ToArray());
                if (src.JobAccountingSheetsSupported != null)
                    dic.Add(PrinterAttribute.JobAccountingSheetsSupported, src.JobAccountingSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobAccountingSheetsSupported, x)).ToArray());
                if (src.JobAccountingSheetsTypeSupported != null)
                    dic.Add(PrinterAttribute.JobAccountingSheetsTypeSupported, src.JobAccountingSheetsTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobAccountingSheetsTypeSupported, x)).ToArray());
                if (src.JobCompleteBeforeSupported != null)
                    dic.Add(PrinterAttribute.JobCompleteBeforeSupported, src.JobCompleteBeforeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobCompleteBeforeSupported, x)).ToArray());
                if (src.JobCompleteBeforeTimeSupported != null)
                    dic.Add(PrinterAttribute.JobCompleteBeforeTimeSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobCompleteBeforeTimeSupported, src.JobCompleteBeforeTimeSupported.Value)]);
                if (src.JobErrorSheetDefault != null)
                    dic.Add(PrinterAttribute.JobErrorSheetDefault, map.Map<IEnumerable<IppAttribute>>(src.JobErrorSheetDefault).ToBegCollection(PrinterAttribute.JobErrorSheetDefault).ToArray());
                if (src.JobErrorSheetSupported != null)
                    dic.Add(PrinterAttribute.JobErrorSheetSupported, src.JobErrorSheetSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobErrorSheetSupported, x)).ToArray());
                if (src.JobErrorSheetTypeSupported != null)
                    dic.Add(PrinterAttribute.JobErrorSheetTypeSupported, src.JobErrorSheetTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobErrorSheetTypeSupported, x)).ToArray());
                if (src.JobErrorSheetWhenSupported != null)
                    dic.Add(PrinterAttribute.JobErrorSheetWhenSupported, src.JobErrorSheetWhenSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobErrorSheetWhenSupported, x)).ToArray());
                if (src.JobMessageToOperatorSupported != null)
                    dic.Add(PrinterAttribute.JobMessageToOperatorSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobMessageToOperatorSupported, src.JobMessageToOperatorSupported.Value)]);
                if (src.JobPhoneNumberDefault != null)
                    dic.Add(PrinterAttribute.JobPhoneNumberDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.JobPhoneNumberDefault, src.JobPhoneNumberDefault)]);
                if (src.JobPhoneNumberSchemeSupported != null)
                    dic.Add(PrinterAttribute.JobPhoneNumberSchemeSupported, src.JobPhoneNumberSchemeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.JobPhoneNumberSchemeSupported, x)).ToArray());
                if (src.JobPhoneNumberSupported != null)
                    dic.Add(PrinterAttribute.JobPhoneNumberSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobPhoneNumberSupported, src.JobPhoneNumberSupported.Value)]);
                if (src.JobRecipientNameSupported != null)
                    dic.Add(PrinterAttribute.JobRecipientNameSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobRecipientNameSupported, src.JobRecipientNameSupported.Value)]);
                if (src.JobSheetMessageSupported != null)
                    dic.Add(PrinterAttribute.JobSheetMessageSupported, [new IppAttribute(Tag.Boolean, PrinterAttribute.JobSheetMessageSupported, src.JobSheetMessageSupported.Value)]);
                if (src.PresentationDirectionNumberUpDefault != null)
                    dic.Add(PrinterAttribute.PresentationDirectionNumberUpDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.PresentationDirectionNumberUpDefault, map.Map<string>(src.PresentationDirectionNumberUpDefault.Value))]);
                if (src.PresentationDirectionNumberUpSupported != null)
                    dic.Add(PrinterAttribute.PresentationDirectionNumberUpSupported, src.PresentationDirectionNumberUpSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PresentationDirectionNumberUpSupported, map.Map<string>(x))).ToArray());
                if (src.SeparatorSheetsDefault != null)
                    dic.Add(PrinterAttribute.SeparatorSheetsDefault, map.Map<IEnumerable<IppAttribute>>(src.SeparatorSheetsDefault).ToBegCollection(PrinterAttribute.SeparatorSheetsDefault).ToArray());
                if (src.SeparatorSheetsSupported != null)
                    dic.Add(PrinterAttribute.SeparatorSheetsSupported, src.SeparatorSheetsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.SeparatorSheetsSupported, x)).ToArray());
                if (src.SeparatorSheetsTypeSupported != null)
                    dic.Add(PrinterAttribute.SeparatorSheetsTypeSupported, src.SeparatorSheetsTypeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.SeparatorSheetsTypeSupported, x)).ToArray());
                if (src.XImagePositionDefault != null)
                    dic.Add(PrinterAttribute.XImagePositionDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.XImagePositionDefault, map.Map<string>(src.XImagePositionDefault.Value))]);
                if (src.XImagePositionSupported != null)
                    dic.Add(PrinterAttribute.XImagePositionSupported, src.XImagePositionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.XImagePositionSupported, map.Map<string>(x))).ToArray());
                if (src.XImageShiftDefault != null)
                    dic.Add(PrinterAttribute.XImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.XImageShiftDefault, src.XImageShiftDefault.Value)]);
                if (src.XImageShiftSupported != null)
                    dic.Add(PrinterAttribute.XImageShiftSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.XImageShiftSupported, src.XImageShiftSupported.Value)]);
                if (src.XSide1ImageShiftDefault != null)
                    dic.Add(PrinterAttribute.XSide1ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.XSide1ImageShiftDefault, src.XSide1ImageShiftDefault.Value)]);
                if (src.XSide2ImageShiftDefault != null)
                    dic.Add(PrinterAttribute.XSide2ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.XSide2ImageShiftDefault, src.XSide2ImageShiftDefault.Value)]);
                if (src.YImagePositionDefault != null)
                    dic.Add(PrinterAttribute.YImagePositionDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.YImagePositionDefault, map.Map<string>(src.YImagePositionDefault.Value))]);
                if (src.YImagePositionSupported != null)
                    dic.Add(PrinterAttribute.YImagePositionSupported, src.YImagePositionSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.YImagePositionSupported, map.Map<string>(x))).ToArray());
                if (src.YImageShiftDefault != null)
                    dic.Add(PrinterAttribute.YImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.YImageShiftDefault, src.YImageShiftDefault.Value)]);
                if (src.YImageShiftSupported != null)
                    dic.Add(PrinterAttribute.YImageShiftSupported, [new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.YImageShiftSupported, src.YImageShiftSupported.Value)]);
                if (src.YSide1ImageShiftDefault != null)
                    dic.Add(PrinterAttribute.YSide1ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.YSide1ImageShiftDefault, src.YSide1ImageShiftDefault.Value)]);
                if (src.YSide2ImageShiftDefault != null)
                    dic.Add(PrinterAttribute.YSide2ImageShiftDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.YSide2ImageShiftDefault, src.YSide2ImageShiftDefault.Value)]);
                return dic;
            });
    }
}
