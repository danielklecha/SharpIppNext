using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class DocumentTemplateAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<DocumentTemplateAttributes, List<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new List<IppAttribute> { new IppAttribute(Tag.NoValue, "document-template-attributes", NoValue.Instance) };

            var dst = new List<IppAttribute>();
            if (src.Copies.HasValue) dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Copies, src.Copies.Value));
            if (src.CoverBack != null) dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverBack).ToBegCollection(IppAttributeNames.CoverBack));
            if (src.CoverFront != null) dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverFront).ToBegCollection(IppAttributeNames.CoverFront));
            if (src.Finishings != null)
            {
                var finishings = src.Finishings;
                if (finishings.Length > 1)
                    finishings = finishings.Where(x => x != Finishings.None).ToArray();

                dst.AddRange(finishings.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)x)));
            }
            if (src.FinishingsCol != null)
            {
                foreach (var finishingsCol in src.FinishingsCol)
                {
                    dst.AddRange(map.Map<IEnumerable<IppAttribute>>(finishingsCol).ToBegCollection(IppAttributeNames.FinishingsCol));
                }
            }
            if (src.ForceFrontSide != null)
                dst.AddRange(src.ForceFrontSide.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ForceFrontSide, x)));
            if (src.ImpositionTemplate != null)
            {
                var impositionTemplateValue = src.ImpositionTemplate.Value;
                var impositionTemplate = map.Map<string>(impositionTemplateValue);
                var impositionTemplateTag = impositionTemplateValue.ToIppTag();
                dst.Add(new IppAttribute(impositionTemplateTag, IppAttributeNames.ImpositionTemplate, impositionTemplate));
            }
            if (src.Media != null)
            {
                var mediaValue = src.Media.Value;
                var media = map.Map<string>(mediaValue);
                var mediaTag = mediaValue.ToIppTag();
                dst.Add(new IppAttribute(mediaTag, IppAttributeNames.Media, media));
            }
            if (src.MediaCol != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(IppAttributeNames.MediaCol));
            if (src.MediaInputTrayCheck != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MediaInputTrayCheck, map.Map<string>(src.MediaInputTrayCheck)));
            if (src.NumberUp.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.NumberUp, src.NumberUp.Value));
            if (src.OrientationRequested.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, IppAttributeNames.OrientationRequested, (int)src.OrientationRequested.Value));
            if (src.OutputBin != null)
            {
                var outputBinValue = src.OutputBin.Value;
                var outputBin = map.Map<string>(outputBinValue);
                var outputBinTag = outputBinValue.ToIppTag();
                dst.Add(new IppAttribute(outputBinTag, IppAttributeNames.OutputBin, outputBin));
            }
            if (src.PageDelivery.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PageDelivery, map.Map<string>(src.PageDelivery.Value)));
            if (src.PageOrderReceived.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PageOrderReceived, map.Map<string>(src.PageOrderReceived.Value)));
            if (src.PageRanges != null)
                dst.AddRange(src.PageRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PageRanges, x)));
            if (src.PresentationDirectionNumberUp.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PresentationDirectionNumberUp, map.Map<string>(src.PresentationDirectionNumberUp.Value)));
            if (src.PrintQuality.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, IppAttributeNames.PrintQuality, (int)src.PrintQuality.Value));
            if (src.PrinterResolution != null)
                dst.Add(new IppAttribute(Tag.Resolution, IppAttributeNames.PrinterResolution, src.PrinterResolution.Value));
            if (src.Sides.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Sides, map.Map<string>(src.Sides.Value)));
            if (src.XImagePosition.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.XImagePosition, map.Map<string>(src.XImagePosition.Value)));
            if (src.XImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.XImageShift, src.XImageShift.Value));
            if (src.XSide1ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.XSide1ImageShift, src.XSide1ImageShift.Value));
            if (src.XSide2ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.XSide2ImageShift, src.XSide2ImageShift.Value));
            if (src.YImagePosition.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.YImagePosition, map.Map<string>(src.YImagePosition.Value)));
            if (src.YImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.YImageShift, src.YImageShift.Value));
            if (src.YSide1ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.YSide1ImageShift, src.YSide1ImageShift.Value));
            if (src.YSide2ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.YSide2ImageShift, src.YSide2ImageShift.Value));
            if (src.InputAutoExposure.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.InputAutoExposure, src.InputAutoExposure.Value));
            if (src.InputAutoScaling.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.InputAutoScaling, src.InputAutoScaling.Value));
            if (src.InputAutoSkewCorrection.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.InputAutoSkewCorrection, src.InputAutoSkewCorrection.Value));
            if (src.InputBrightness.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.InputBrightness, src.InputBrightness.Value));
            if (src.InputColorMode != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.InputColorMode, map.Map<string>(src.InputColorMode.Value)));
            if (src.InputContentType != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.InputContentType, map.Map<string>(src.InputContentType.Value)));
            if (src.InputContrast.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.InputContrast, src.InputContrast.Value));
            if (src.InputFilmScanMode != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.InputFilmScanMode, map.Map<string>(src.InputFilmScanMode.Value)));
            if (src.InputImagesToTransfer.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.InputImagesToTransfer, src.InputImagesToTransfer.Value));
            if (src.InputMedia != null)
            {
                var inputMediaValue = src.InputMedia.Value;
                var inputMedia = map.Map<string>(inputMediaValue);
                var inputMediaTag = inputMediaValue.ToIppTag();
                dst.Add(new IppAttribute(inputMediaTag, IppAttributeNames.InputMedia, inputMedia));
            }
            if (src.InputOrientationRequested.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, IppAttributeNames.InputOrientationRequested, (int)src.InputOrientationRequested.Value));
            if (src.InputQuality.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, IppAttributeNames.InputQuality, (int)src.InputQuality.Value));
            if (src.InputResolution != null)
                dst.Add(new IppAttribute(Tag.Resolution, IppAttributeNames.InputResolution, src.InputResolution.Value));
            if (src.InputScalingHeight.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.InputScalingHeight, src.InputScalingHeight.Value));
            if (src.InputScalingWidth.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.InputScalingWidth, src.InputScalingWidth.Value));
            if (src.InputSharpness.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.InputSharpness, src.InputSharpness.Value));
            if (src.InputSides.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.InputSides, map.Map<string>(src.InputSides.Value)));
            if (src.InputSource != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.InputSource, map.Map<string>(src.InputSource.Value)));
            if (src.DocumentCharset != null)
                dst.Add(new IppAttribute(Tag.Charset, IppAttributeNames.DocumentCharset, src.DocumentCharset.Value));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, src.DocumentFormat.Value));
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(IppAttributeNames.DocumentFormatDetails));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentMessage, src.DocumentMessage));
            if (src.DocumentMetadata != null)
                dst.AddRange(src.DocumentMetadata.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString(x))));
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.DocumentName, src.DocumentName));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.DocumentNaturalLanguage, src.DocumentNaturalLanguage.Value));
            if (src.DocumentPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentPassword, src.DocumentPassword.Value));
            if (src.DocumentUri != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.DocumentUri, src.DocumentUri.ToString()));
            if (src.LastDocument.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, src.LastDocument.Value));
            if (src.JobPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.JobPassword, src.JobPassword.Value));
            if (src.JobPasswordEncryption != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobPasswordEncryption, map.Map<string>(src.JobPasswordEncryption.Value)));
            if (src.ChamberHumidity.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidity, src.ChamberHumidity.Value));
            if (src.ChamberTemperature.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperature, src.ChamberTemperature.Value));
            if (src.MaterialsCol != null)
                dst.AddRange(src.MaterialsCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MaterialsCol)));
            if (src.MultipleObjectHandling != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleObjectHandling, map.Map<string>(src.MultipleObjectHandling.Value)));
            if (src.PlatformTemperature.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PlatformTemperature, src.PlatformTemperature.Value));
            if (src.PrintAccuracy != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.PrintAccuracy).ToBegCollection(IppAttributeNames.PrintAccuracy));
            if (src.PrintBase != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBase, map.Map<string>(src.PrintBase.Value)));
            if (src.PrintObjects != null)
                dst.AddRange(src.PrintObjects.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrintObjects)));
            if (src.PrintSupports != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupports, map.Map<string>(src.PrintSupports.Value)));
            return dst;
        });

        mapper.CreateMap<DocumentTemplateAttributes, IEnumerable<IppAttribute>>((src, map) => map.Map<DocumentTemplateAttributes, List<IppAttribute>>(src));

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentTemplateAttributes>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DocumentTemplateAttributes>();

            var dst = new DocumentTemplateAttributes();
            dst.Copies = map.MapFromDicNullable<int?>(src, IppAttributeNames.Copies);
            if (src.ContainsKey(IppAttributeNames.CoverBack))
                dst.CoverBack = map.Map<Cover>(src[IppAttributeNames.CoverBack].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey(IppAttributeNames.CoverFront))
                dst.CoverFront = map.Map<Cover>(src[IppAttributeNames.CoverFront].FromBegCollection().ToIppDictionary());
            dst.Finishings = map.MapFromDicSetNullable<Finishings[]?>(src, IppAttributeNames.Finishings);
            if (src.ContainsKey(IppAttributeNames.FinishingsCol))
                dst.FinishingsCol = src[IppAttributeNames.FinishingsCol].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.ForceFrontSide = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ForceFrontSide);
            dst.ImpositionTemplate = map.MapFromDicNullable<string, ImpositionTemplate?>(src, IppAttributeNames.ImpositionTemplate, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword));
            dst.Media = map.MapFromDicNullable<string, Media?>(src, IppAttributeNames.Media, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword));
            if (src.ContainsKey(IppAttributeNames.MediaCol))
                dst.MediaCol = map.Map<MediaCol>(src[IppAttributeNames.MediaCol].FromBegCollection().ToIppDictionary());
            dst.MediaInputTrayCheck = map.MapFromDicNullable<MediaInputTrayCheck?>(src, IppAttributeNames.MediaInputTrayCheck);
            dst.NumberUp = map.MapFromDicNullable<int?>(src, IppAttributeNames.NumberUp);
            dst.OrientationRequested = map.MapFromDicNullable<Orientation?>(src, IppAttributeNames.OrientationRequested);
            dst.OutputBin = map.MapFromDicNullable<string, OutputBin?>(src, IppAttributeNames.OutputBin, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword));
            dst.PageDelivery = map.MapFromDicNullable<PageDelivery?>(src, IppAttributeNames.PageDelivery);
            dst.PageOrderReceived = map.MapFromDicNullable<PageOrderReceived?>(src, IppAttributeNames.PageOrderReceived);
            dst.PageRanges = map.MapFromDicSetNullable<Range[]?>(src, IppAttributeNames.PageRanges);
            dst.PresentationDirectionNumberUp = map.MapFromDicNullable<PresentationDirectionNumberUp?>(src, IppAttributeNames.PresentationDirectionNumberUp);
            dst.PrintQuality = map.MapFromDicNullable<PrintQuality?>(src, IppAttributeNames.PrintQuality);
            dst.PrinterResolution = map.MapFromDicNullable<Resolution?>(src, IppAttributeNames.PrinterResolution);
            dst.Sides = map.MapFromDicNullable<Sides?>(src, IppAttributeNames.Sides);
            dst.XImagePosition = map.MapFromDicNullable<XImagePosition?>(src, IppAttributeNames.XImagePosition);
            dst.XImageShift = map.MapFromDicNullable<int?>(src, IppAttributeNames.XImageShift);
            dst.XSide1ImageShift = map.MapFromDicNullable<int?>(src, IppAttributeNames.XSide1ImageShift);
            dst.XSide2ImageShift = map.MapFromDicNullable<int?>(src, IppAttributeNames.XSide2ImageShift);
            dst.YImagePosition = map.MapFromDicNullable<YImagePosition?>(src, IppAttributeNames.YImagePosition);
            dst.YImageShift = map.MapFromDicNullable<int?>(src, IppAttributeNames.YImageShift);
            dst.YSide1ImageShift = map.MapFromDicNullable<int?>(src, IppAttributeNames.YSide1ImageShift);
            dst.YSide2ImageShift = map.MapFromDicNullable<int?>(src, IppAttributeNames.YSide2ImageShift);
            dst.InputAutoExposure = map.MapFromDicNullable<bool?>(src, IppAttributeNames.InputAutoExposure);
            dst.InputAutoScaling = map.MapFromDicNullable<bool?>(src, IppAttributeNames.InputAutoScaling);
            dst.InputAutoSkewCorrection = map.MapFromDicNullable<bool?>(src, IppAttributeNames.InputAutoSkewCorrection);
            dst.InputBrightness = map.MapFromDicNullable<int?>(src, IppAttributeNames.InputBrightness);
            dst.InputColorMode = map.MapFromDicNullable<InputColorMode?>(src, IppAttributeNames.InputColorMode);
            dst.InputContentType = map.MapFromDicNullable<InputContentType?>(src, IppAttributeNames.InputContentType);
            dst.InputContrast = map.MapFromDicNullable<int?>(src, IppAttributeNames.InputContrast);
            dst.InputFilmScanMode = map.MapFromDicNullable<InputFilmScanMode?>(src, IppAttributeNames.InputFilmScanMode);
            dst.InputImagesToTransfer = map.MapFromDicNullable<int?>(src, IppAttributeNames.InputImagesToTransfer);
            dst.InputMedia = map.MapFromDicNullable<string, Media?>(src, IppAttributeNames.InputMedia, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword));
            dst.InputOrientationRequested = map.MapFromDicNullable<Orientation?>(src, IppAttributeNames.InputOrientationRequested);
            dst.InputQuality = map.MapFromDicNullable<PrintQuality?>(src, IppAttributeNames.InputQuality);
            dst.InputResolution = map.MapFromDicNullable<Resolution?>(src, IppAttributeNames.InputResolution);
            dst.InputScalingHeight = map.MapFromDicNullable<int?>(src, IppAttributeNames.InputScalingHeight);
            dst.InputScalingWidth = map.MapFromDicNullable<int?>(src, IppAttributeNames.InputScalingWidth);
            dst.InputSharpness = map.MapFromDicNullable<int?>(src, IppAttributeNames.InputSharpness);
            dst.InputSides = map.MapFromDicNullable<Sides?>(src, IppAttributeNames.InputSides);
            dst.InputSource = map.MapFromDicNullable<InputSource?>(src, IppAttributeNames.InputSource);
            dst.DocumentCharset = map.MapFromDicNullable<Charset?>(src, IppAttributeNames.DocumentCharset);
            dst.DocumentFormat = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormat);
            if (src.TryGetValue(IppAttributeNames.DocumentFormatDetails, out var documentFormatDetails) && documentFormatDetails.GroupBegCollection().Any())
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentMessage);
            dst.DocumentMetadata = map.MapFromDicSetNullable<DocumentMetadata?>(src, IppAttributeNames.DocumentMetadata);
            dst.DocumentName = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentName);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<NaturalLanguage?>(src, IppAttributeNames.DocumentNaturalLanguage);
            dst.DocumentPassword = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.DocumentPassword);
            dst.DocumentUri = map.MapFromDicNullable<string, System.Uri?>(src, IppAttributeNames.DocumentUri, (_, value) => new System.Uri(value));
            dst.LastDocument = map.MapFromDicNullable<bool?>(src, IppAttributeNames.LastDocument);
            dst.JobPassword = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.JobPassword);
            dst.JobPasswordEncryption = map.MapFromDicNullable<JobPasswordEncryption?>(src, IppAttributeNames.JobPasswordEncryption);
            dst.ChamberHumidity = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberHumidity);
            dst.ChamberTemperature = map.MapFromDicNullable<int?>(src, IppAttributeNames.ChamberTemperature);
            if (src.ContainsKey(IppAttributeNames.MaterialsCol))
                dst.MaterialsCol = src[IppAttributeNames.MaterialsCol].GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.MultipleObjectHandling = map.MapFromDicNullable<MultipleObjectHandling?>(src, IppAttributeNames.MultipleObjectHandling);
            dst.PlatformTemperature = map.MapFromDicNullable<int?>(src, IppAttributeNames.PlatformTemperature);
            if (src.ContainsKey(IppAttributeNames.PrintAccuracy))
                dst.PrintAccuracy = map.Map<PrintAccuracy>(src[IppAttributeNames.PrintAccuracy].GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.PrintBase = map.MapFromDicNullable<PrintBase?>(src, IppAttributeNames.PrintBase);
            if (src.ContainsKey(IppAttributeNames.PrintObjects))
                dst.PrintObjects = src[IppAttributeNames.PrintObjects].GroupBegCollection().Select(x => map.Map<PrintObject>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.PrintSupports = map.MapFromDicNullable<PrintSupports?>(src, IppAttributeNames.PrintSupports);
            return dst;
        });
    }

}
