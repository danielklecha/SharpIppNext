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
            var dst = new List<IppAttribute>();
            if (src.Copies.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, src.Copies.Value));
            if (src.CoverBack != null) dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverBack).ToBegCollection(JobAttribute.CoverBack));
            if (src.CoverFront != null) dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverFront).ToBegCollection(JobAttribute.CoverFront));
            if (src.Finishings.HasValue) dst.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)src.Finishings.Value));
            if (src.FinishingsCol != null) dst.AddRange(src.FinishingsCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.FinishingsCol)));
            if (src.ForceFrontSide != null) dst.AddRange(src.ForceFrontSide.Select(x => new IppAttribute(Tag.Integer, JobAttribute.ForceFrontSide, x)));
            if (src.ImpositionTemplate != null) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.ImpositionTemplate, src.ImpositionTemplate));
            if (src.Media != null) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, src.Media));
            if (src.MediaCol != null) dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(JobAttribute.MediaCol));
            if (src.MediaInputTrayCheck != null) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.MediaInputTrayCheck, map.Map<string>(src.MediaInputTrayCheck)));
            if (src.NumberUp.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.NumberUp, src.NumberUp.Value));
            if (src.OrientationRequested.HasValue) dst.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)src.OrientationRequested.Value));
            if (src.OutputBin != null) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.OutputBin, src.OutputBin));
            if (src.PageDelivery.HasValue) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.PageDelivery, map.Map<string>(src.PageDelivery.Value)));
            if (src.PageOrderReceived.HasValue) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.PageOrderReceived, map.Map<string>(src.PageOrderReceived.Value)));
            if (src.PageRanges != null) dst.AddRange(src.PageRanges.Select(x => new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, x)));
            if (src.PresentationDirectionNumberUp.HasValue) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.PresentationDirectionNumberUp, map.Map<string>(src.PresentationDirectionNumberUp.Value)));
            if (src.PrintQuality.HasValue) dst.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)src.PrintQuality.Value));
            if (src.PrinterResolution != null) dst.Add(new IppAttribute(Tag.Resolution, JobAttribute.PrinterResolution, src.PrinterResolution));
            if (src.Sides.HasValue) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, map.Map<string>(src.Sides.Value)));
            if (src.XImagePosition.HasValue) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.XImagePosition, map.Map<string>(src.XImagePosition.Value)));
            if (src.XImageShift.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.XImageShift, src.XImageShift.Value));
            if (src.XSide1ImageShift.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.XSide1ImageShift, src.XSide1ImageShift.Value));
            if (src.XSide2ImageShift.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.XSide2ImageShift, src.XSide2ImageShift.Value));
            if (src.YImagePosition.HasValue) dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.YImagePosition, map.Map<string>(src.YImagePosition.Value)));
            if (src.YImageShift.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.YImageShift, src.YImageShift.Value));
            if (src.YSide1ImageShift.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.YSide1ImageShift, src.YSide1ImageShift.Value));
            if (src.YSide2ImageShift.HasValue) dst.Add(new IppAttribute(Tag.Integer, JobAttribute.YSide2ImageShift, src.YSide2ImageShift.Value));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentTemplateAttributes>((src, dst, map) =>
        {
            dst ??= new DocumentTemplateAttributes();
            dst.Copies = map.MapFromDicNullable<int?>(src, JobAttribute.Copies);
            if (src.ContainsKey(JobAttribute.CoverBack))
                dst.CoverBack = map.Map<Cover>(src[JobAttribute.CoverBack].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey(JobAttribute.CoverFront))
                dst.CoverFront = map.Map<Cover>(src[JobAttribute.CoverFront].FromBegCollection().ToIppDictionary());
            dst.Finishings = map.MapFromDicNullable<Finishings?>(src, JobAttribute.Finishings);
            if (src.ContainsKey(JobAttribute.FinishingsCol))
                dst.FinishingsCol = src[JobAttribute.FinishingsCol].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.ForceFrontSide = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.ForceFrontSide);
            dst.ImpositionTemplate = map.MapFromDicNullable<string?>(src, JobAttribute.ImpositionTemplate);
            dst.Media = map.MapFromDicNullable<string?>(src, JobAttribute.Media);
            if (src.ContainsKey(JobAttribute.MediaCol))
                dst.MediaCol = map.Map<MediaCol>(src[JobAttribute.MediaCol].FromBegCollection().ToIppDictionary());
            dst.MediaInputTrayCheck = map.MapFromDicNullable<MediaInputTrayCheck?>(src, JobAttribute.MediaInputTrayCheck);
            dst.NumberUp = map.MapFromDicNullable<int?>(src, JobAttribute.NumberUp);
            dst.OrientationRequested = map.MapFromDicNullable<Orientation?>(src, JobAttribute.OrientationRequested);
            dst.OutputBin = map.MapFromDicNullable<string?>(src, JobAttribute.OutputBin);
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
            return dst;
        });
    }
}
