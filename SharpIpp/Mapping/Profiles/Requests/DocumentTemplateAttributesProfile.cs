using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class DocumentTemplateAttributesProfile : IProfile
{
    private static bool IsOutOfBandNoValue(IDictionary<string, IppAttribute[]> src)
    {
        return src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand();
    }

    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<DocumentTemplateAttributes, List<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new List<IppAttribute> { new IppAttribute(Tag.NoValue, "document-template-attributes", NoValue.Instance) };

            var dst = new List<IppAttribute>();
            if (src.Copies.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, src.Copies.Value));
            if (src.CoverBack != null) dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverBack).ToBegCollection(JobAttribute.CoverBack));
            if (src.CoverFront != null) dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverFront).ToBegCollection(JobAttribute.CoverFront));
            if (src.Finishings != null)
            {
                var finishings = src.Finishings;
                if (finishings.Length > 1)
                    finishings = finishings.Where(x => x != Finishings.None).ToArray();

                dst.AddRange(finishings.Select(x => new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)x)));
            }
            if (src.FinishingsCol != null)
            {
                foreach (var finishingsCol in src.FinishingsCol)
                {
                    dst.AddRange(map.Map<IEnumerable<IppAttribute>>(finishingsCol).ToBegCollection(JobAttribute.FinishingsCol));
                }
            }
            if (src.ForceFrontSide != null)
                dst.AddRange(src.ForceFrontSide.Select(x => new IppAttribute(Tag.Integer, JobAttribute.ForceFrontSide, x)));
            if (src.ImpositionTemplate != null)
            {
                var impositionTemplateValue = src.ImpositionTemplate.Value;
                var impositionTemplate = map.Map<string>(impositionTemplateValue);
                var impositionTemplateTag = impositionTemplateValue.ToIppTag();
                dst.Add(new IppAttribute(impositionTemplateTag, JobAttribute.ImpositionTemplate, impositionTemplate));
            }
            if (src.Media != null)
            {
                var mediaValue = src.Media.Value;
                var media = map.Map<string>(mediaValue);
                var mediaTag = mediaValue.ToIppTag();
                dst.Add(new IppAttribute(mediaTag, JobAttribute.Media, media));
            }
            if (src.MediaCol != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(JobAttribute.MediaCol));
            if (src.MediaInputTrayCheck != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.MediaInputTrayCheck, map.Map<string>(src.MediaInputTrayCheck)));
            if (src.NumberUp.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.NumberUp, src.NumberUp.Value));
            if (src.OrientationRequested.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)src.OrientationRequested.Value));
            if (src.OutputBin != null)
            {
                var outputBinValue = src.OutputBin.Value;
                var outputBin = map.Map<string>(outputBinValue);
                var outputBinTag = outputBinValue.ToIppTag();
                dst.Add(new IppAttribute(outputBinTag, JobAttribute.OutputBin, outputBin));
            }
            if (src.PageDelivery.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.PageDelivery, map.Map<string>(src.PageDelivery.Value)));
            if (src.PageOrderReceived.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.PageOrderReceived, map.Map<string>(src.PageOrderReceived.Value)));
            if (src.PageRanges != null)
                dst.AddRange(src.PageRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, x)));
            if (src.PresentationDirectionNumberUp.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.PresentationDirectionNumberUp, map.Map<string>(src.PresentationDirectionNumberUp.Value)));
            if (src.PrintQuality.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)src.PrintQuality.Value));
            if (src.PrinterResolution != null)
                dst.Add(new IppAttribute(Tag.Resolution, JobAttribute.PrinterResolution, src.PrinterResolution));
            if (src.Sides.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, map.Map<string>(src.Sides.Value)));
            if (src.XImagePosition.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.XImagePosition, map.Map<string>(src.XImagePosition.Value)));
            if (src.XImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.XImageShift, src.XImageShift.Value));
            if (src.XSide1ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.XSide1ImageShift, src.XSide1ImageShift.Value));
            if (src.XSide2ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.XSide2ImageShift, src.XSide2ImageShift.Value));
            if (src.YImagePosition.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.YImagePosition, map.Map<string>(src.YImagePosition.Value)));
            if (src.YImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.YImageShift, src.YImageShift.Value));
            if (src.YSide1ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.YSide1ImageShift, src.YSide1ImageShift.Value));
            if (src.YSide2ImageShift.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.YSide2ImageShift, src.YSide2ImageShift.Value));
            if (src.InputAutoExposure.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, "input-auto-exposure", src.InputAutoExposure.Value));
            if (src.InputAutoScaling.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, "input-auto-scaling", src.InputAutoScaling.Value));
            if (src.InputAutoSkewCorrection.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, "input-auto-skew-correction", src.InputAutoSkewCorrection.Value));
            if (src.InputBrightness.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, "input-brightness", src.InputBrightness.Value));
            if (src.InputColorMode != null)
                dst.Add(new IppAttribute(Tag.Keyword, "input-color-mode", map.Map<string>(src.InputColorMode.Value)));
            if (src.InputContentType != null)
                dst.Add(new IppAttribute(Tag.Keyword, "input-content-type", map.Map<string>(src.InputContentType.Value)));
            if (src.InputContrast.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, "input-contrast", src.InputContrast.Value));
            if (src.InputFilmScanMode != null)
                dst.Add(new IppAttribute(Tag.Keyword, "input-film-scan-mode", map.Map<string>(src.InputFilmScanMode.Value)));
            if (src.InputImagesToTransfer.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, "input-images-to-transfer", src.InputImagesToTransfer.Value));
            if (src.InputMedia != null)
            {
                var inputMediaValue = src.InputMedia.Value;
                var inputMedia = map.Map<string>(inputMediaValue);
                var inputMediaTag = inputMediaValue.ToIppTag();
                dst.Add(new IppAttribute(inputMediaTag, "input-media", inputMedia));
            }
            if (src.InputOrientationRequested.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, "input-orientation-requested", (int)src.InputOrientationRequested.Value));
            if (src.InputQuality.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, "input-quality", (int)src.InputQuality.Value));
            if (src.InputResolution != null)
                dst.Add(new IppAttribute(Tag.Resolution, "input-resolution", src.InputResolution));
            if (src.InputScalingHeight.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, "input-scaling-height", src.InputScalingHeight.Value));
            if (src.InputScalingWidth.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, "input-scaling-width", src.InputScalingWidth.Value));
            if (src.InputSharpness.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, "input-sharpness", src.InputSharpness.Value));
            if (src.InputSides.HasValue)
                dst.Add(new IppAttribute(Tag.Keyword, "input-sides", map.Map<string>(src.InputSides.Value)));
            if (src.InputSource != null)
                dst.Add(new IppAttribute(Tag.Keyword, "input-source", map.Map<string>(src.InputSource.Value)));
            return dst;
        });

        mapper.CreateMap<DocumentTemplateAttributes, IEnumerable<IppAttribute>>((src, map) => map.Map<DocumentTemplateAttributes, List<IppAttribute>>(src));

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentTemplateAttributes>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<DocumentTemplateAttributes>();

            var dst = new DocumentTemplateAttributes();
            dst.Copies = map.MapFromDicNullable<int?>(src, JobAttribute.Copies);
            if (src.ContainsKey(JobAttribute.CoverBack))
                dst.CoverBack = map.Map<Cover>(src[JobAttribute.CoverBack].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey(JobAttribute.CoverFront))
                dst.CoverFront = map.Map<Cover>(src[JobAttribute.CoverFront].FromBegCollection().ToIppDictionary());
            dst.Finishings = map.MapFromDicSetNullable<Finishings[]?>(src, JobAttribute.Finishings);
            if (src.ContainsKey(JobAttribute.FinishingsCol))
                dst.FinishingsCol = src[JobAttribute.FinishingsCol].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.ForceFrontSide = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.ForceFrontSide);
            dst.ImpositionTemplate = map.MapFromDicNullable<string, ImpositionTemplate?>(src, JobAttribute.ImpositionTemplate, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword));
            dst.Media = map.MapFromDicNullable<string, Media?>(src, JobAttribute.Media, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword));
            if (src.ContainsKey(JobAttribute.MediaCol))
                dst.MediaCol = map.Map<MediaCol>(src[JobAttribute.MediaCol].FromBegCollection().ToIppDictionary());
            dst.MediaInputTrayCheck = map.MapFromDicNullable<MediaInputTrayCheck?>(src, JobAttribute.MediaInputTrayCheck);
            dst.NumberUp = map.MapFromDicNullable<int?>(src, JobAttribute.NumberUp);
            dst.OrientationRequested = map.MapFromDicNullable<Orientation?>(src, JobAttribute.OrientationRequested);
            dst.OutputBin = map.MapFromDicNullable<string, OutputBin?>(src, JobAttribute.OutputBin, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword));
            dst.PageDelivery = map.MapFromDicNullable<PageDelivery?>(src, JobAttribute.PageDelivery);
            dst.PageOrderReceived = map.MapFromDicNullable<PageOrderReceived?>(src, JobAttribute.PageOrderReceived);
            dst.PageRanges = map.MapFromDicSetNullable<Range[]?>(src, JobAttribute.PageRanges);
            dst.PresentationDirectionNumberUp = map.MapFromDicNullable<PresentationDirectionNumberUp?>(src, JobAttribute.PresentationDirectionNumberUp);
            dst.PrintQuality = map.MapFromDicNullable<PrintQuality?>(src, JobAttribute.PrintQuality);
            dst.PrinterResolution = map.MapFromDicNullable<Resolution?>(src, JobAttribute.PrinterResolution);
            dst.Sides = map.MapFromDicNullable<Sides?>(src, JobAttribute.Sides);
            dst.XImagePosition = map.MapFromDicNullable<XImagePosition?>(src, JobAttribute.XImagePosition);
            dst.XImageShift = map.MapFromDicNullable<int?>(src, JobAttribute.XImageShift);
            dst.XSide1ImageShift = map.MapFromDicNullable<int?>(src, JobAttribute.XSide1ImageShift);
            dst.XSide2ImageShift = map.MapFromDicNullable<int?>(src, JobAttribute.XSide2ImageShift);
            dst.YImagePosition = map.MapFromDicNullable<YImagePosition?>(src, JobAttribute.YImagePosition);
            dst.YImageShift = map.MapFromDicNullable<int?>(src, JobAttribute.YImageShift);
            dst.YSide1ImageShift = map.MapFromDicNullable<int?>(src, JobAttribute.YSide1ImageShift);
            dst.YSide2ImageShift = map.MapFromDicNullable<int?>(src, JobAttribute.YSide2ImageShift);
            dst.InputAutoExposure = map.MapFromDicNullable<bool?>(src, "input-auto-exposure");
            dst.InputAutoScaling = map.MapFromDicNullable<bool?>(src, "input-auto-scaling");
            dst.InputAutoSkewCorrection = map.MapFromDicNullable<bool?>(src, "input-auto-skew-correction");
            dst.InputBrightness = map.MapFromDicNullable<int?>(src, "input-brightness");
            dst.InputColorMode = map.MapFromDicNullable<InputColorMode?>(src, "input-color-mode");
            dst.InputContentType = map.MapFromDicNullable<InputContentType?>(src, "input-content-type");
            dst.InputContrast = map.MapFromDicNullable<int?>(src, "input-contrast");
            dst.InputFilmScanMode = map.MapFromDicNullable<InputFilmScanMode?>(src, "input-film-scan-mode");
            dst.InputImagesToTransfer = map.MapFromDicNullable<int?>(src, "input-images-to-transfer");
            dst.InputMedia = map.MapFromDicNullable<string, Media?>(src, "input-media", (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword));
            dst.InputOrientationRequested = map.MapFromDicNullable<Orientation?>(src, "input-orientation-requested");
            dst.InputQuality = map.MapFromDicNullable<PrintQuality?>(src, "input-quality");
            dst.InputResolution = map.MapFromDicNullable<Resolution?>(src, "input-resolution");
            dst.InputScalingHeight = map.MapFromDicNullable<int?>(src, "input-scaling-height");
            dst.InputScalingWidth = map.MapFromDicNullable<int?>(src, "input-scaling-width");
            dst.InputSharpness = map.MapFromDicNullable<int?>(src, "input-sharpness");
            dst.InputSides = map.MapFromDicNullable<Sides?>(src, "input-sides");
            dst.InputSource = map.MapFromDicNullable<InputSource?>(src, "input-source");
            return dst;
        });
    }

}
