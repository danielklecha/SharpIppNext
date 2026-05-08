using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class FinishingsColProfile : IProfile
{public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, FinishingsCol>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<FinishingsCol>();

            var finishingsCol = new FinishingsCol
            {
                FinishingTemplate = map.MapFromDicNullable<string, FinishingTemplate?>(src, "finishing-template", (attribute, value) => new FinishingTemplate(value, attribute.Tag == Tag.Keyword)),
                ImpositionTemplate = map.MapFromDicNullable<string, ImpositionTemplate?>(src, "imposition-template", (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword)),
                MediaSheetsSupported = map.MapFromDicNullable<Range?>(src, "media-sheets-supported"),
                MediaSizeName = map.MapFromDicNullable<string, Media?>(src, "media-size-name", (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
            };
            if (src.ContainsKey("media-size"))
                finishingsCol.MediaSize = map.Map<MediaSize>(src["media-size"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("baling"))
                finishingsCol.Baling = map.Map<Baling>(src["baling"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("binding"))
                finishingsCol.Binding = map.Map<Binding>(src["binding"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("coating"))
                finishingsCol.Coating = map.Map<Coating>(src["coating"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("covering"))
                finishingsCol.Covering = map.Map<Covering>(src["covering"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("folding"))
                finishingsCol.Folding = src["folding"].GroupBegCollection().Select(x => map.Map<Folding>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey("laminating"))
                finishingsCol.Laminating = map.Map<Laminating>(src["laminating"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("punching"))
                finishingsCol.Punching = map.Map<Punching>(src["punching"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("stitching"))
                finishingsCol.Stitching = map.Map<Stitching>(src["stitching"].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey("trimming"))
                finishingsCol.Trimming = src["trimming"].GroupBegCollection().Select(x => map.Map<Trimming>(x.FromBegCollection().ToIppDictionary())).ToArray();
            return finishingsCol;
        });

        mapper.CreateMap<FinishingsCol, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return [new IppAttribute(Tag.NoValue, JobAttribute.FinishingsCol, NoValue.Instance)];

            var attributes = new List<IppAttribute>();
            if (src.FinishingTemplate.HasValue)
            {
                var finishingTemplateValue = src.FinishingTemplate.Value;
                var finishingTemplate = map.Map<string>(finishingTemplateValue);
                var finishingTemplateTag = finishingTemplateValue.ToIppTag();
                attributes.Add(new IppAttribute(finishingTemplateTag, "finishing-template", finishingTemplate));
            }
            if (src.ImpositionTemplate.HasValue)
            {
                var impositionTemplateValue = src.ImpositionTemplate.Value;
                var impositionTemplate = map.Map<string>(impositionTemplateValue);
                var impositionTemplateTag = impositionTemplateValue.ToIppTag();
                attributes.Add(new IppAttribute(impositionTemplateTag, "imposition-template", impositionTemplate));
            }
            if (src.MediaSheetsSupported != null)
                attributes.Add(new IppAttribute(Tag.RangeOfInteger, "media-sheets-supported", src.MediaSheetsSupported.Value));
            if (src.MediaSizeName.HasValue)
            {
                var mediaSizeNameValue = src.MediaSizeName.Value;
                var mediaSizeName = map.Map<string>(mediaSizeNameValue);
                var mediaSizeNameTag = mediaSizeNameValue.ToIppTag();
                attributes.Add(new IppAttribute(mediaSizeNameTag, "media-size-name", mediaSizeName));
            }
            if (src.MediaSize != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaSize).ToBegCollection("media-size"));
            if (src.Baling != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.Baling).ToBegCollection("baling"));
            if (src.Binding != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.Binding).ToBegCollection("binding"));
            if (src.Coating != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.Coating).ToBegCollection("coating"));
            if (src.Covering != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.Covering).ToBegCollection("covering"));
            if (src.Folding != null)
                attributes.AddRange(src.Folding.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection("folding")));
            if (src.Laminating != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.Laminating).ToBegCollection("laminating"));
            if (src.Punching != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.Punching).ToBegCollection("punching"));
            if (src.Stitching != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.Stitching).ToBegCollection("stitching"));
            if (src.Trimming != null)
                attributes.AddRange(src.Trimming.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection("trimming")));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Baling>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Baling>();

            return new Baling
            {
                BalingType = map.MapFromDicNullable<string, BalingType?>(src, "baling-type", (attribute, value) => new BalingType(value, attribute.Tag == Tag.Keyword)),
                BalingWhen = map.MapFromDicNullable<BalingWhen?>(src, "baling-when")
            };
        });

        mapper.CreateMap<Baling, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "baling", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.BalingType != null)
            {
                var balingTypeValue = src.BalingType.Value;
                var balingType = map.Map<string>(balingTypeValue);
                var balingTypeTag = balingTypeValue.ToIppTag();
                attributes.Add(new IppAttribute(balingTypeTag, "baling-type", balingType));
            }
                
            if (src.BalingWhen.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "baling-when", map.Map<string>(src.BalingWhen.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Binding>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Binding>();

            return new Binding
            {
                BindingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "binding-reference-edge"),
                BindingType = map.MapFromDicNullable<string, BindingType?>(src, "binding-type", (attribute, value) => new BindingType(value, attribute.Tag == Tag.Keyword))
            };
        });

        mapper.CreateMap<Binding, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "binding", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.BindingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "binding-reference-edge", map.Map<string>(src.BindingReferenceEdge.Value)));
            if (src.BindingType != null)
            {
                var bindingTypeValue = src.BindingType.Value;
                var bindingType = map.Map<string>(bindingTypeValue);
                var bindingTypeTag = bindingTypeValue.ToIppTag();
                attributes.Add(new IppAttribute(bindingTypeTag, "binding-type", bindingType));
            }
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Coating>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Coating>();

            return new Coating
            {
                CoatingSides = map.MapFromDicNullable<CoatingSides?>(src, "coating-sides"),
                CoatingType = map.MapFromDicNullable<string, CoatingType?>(src, "coating-type", (attribute, value) => new CoatingType(value, attribute.Tag == Tag.Keyword))
            };
        });

        mapper.CreateMap<Coating, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "coating", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CoatingSides.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "coating-sides", map.Map<string>(src.CoatingSides.Value)));
            if (src.CoatingType != null)
            {
                var coatingTypeValue = src.CoatingType.Value;
                var coatingType = map.Map<string>(coatingTypeValue);
                var coatingTypeTag = coatingTypeValue.ToIppTag();
                attributes.Add(new IppAttribute(coatingTypeTag, "coating-type", coatingType));
            }
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Covering>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Covering>();

            return new Covering
            {
                CoveringName = map.MapFromDicNullable<string, CoveringName?>(src, "covering-name", (attribute, value) => new CoveringName(value, attribute.Tag == Tag.Keyword))
            };
        });

        mapper.CreateMap<Covering, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "covering", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CoveringName != null)
            {
                var coveringNameValue = src.CoveringName.Value;
                var coveringName = map.Map<string>(coveringNameValue);
                var coveringNameTag = coveringNameValue.ToIppTag();
                attributes.Add(new IppAttribute(coveringNameTag, "covering-name", coveringName));
            }
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Folding>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Folding>();

            return new Folding
            {
                FoldingDirection = map.MapFromDicNullable<FoldingDirection?>(src, "folding-direction"),
                FoldingOffset = map.MapFromDicNullable<int?>(src, "folding-offset"),
                FoldingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "folding-reference-edge")
            };
        });

        mapper.CreateMap<Folding, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "folding", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.FoldingDirection.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "folding-direction", map.Map<string>(src.FoldingDirection.Value)));
            if (src.FoldingOffset.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "folding-offset", src.FoldingOffset.Value));
            if (src.FoldingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "folding-reference-edge", map.Map<string>(src.FoldingReferenceEdge.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Laminating>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Laminating>();

            return new Laminating
            {
                LaminatingSides = map.MapFromDicNullable<CoatingSides?>(src, "laminating-sides"),
                LaminatingType = map.MapFromDicNullable<string, LaminatingType?>(src, "laminating-type", (attribute, value) => new LaminatingType(value, attribute.Tag == Tag.Keyword))
            };
        });

        mapper.CreateMap<Laminating, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "laminating", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.LaminatingSides.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "laminating-sides", map.Map<string>(src.LaminatingSides.Value)));
            if (src.LaminatingType != null)
            {
                var laminatingTypeValue = src.LaminatingType.Value;
                var laminatingType = map.Map<string>(laminatingTypeValue);
                var laminatingTypeTag = laminatingTypeValue.ToIppTag();
                attributes.Add(new IppAttribute(laminatingTypeTag, "laminating-type", laminatingType));
            }
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Punching>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Punching>();

            return new Punching
            {
                PunchingLocations = map.MapFromDicSetNullable<int[]?>(src, "punching-locations"),
                PunchingOffset = map.MapFromDicNullable<int?>(src, "punching-offset"),
                PunchingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "punching-reference-edge")
            };
        });

        mapper.CreateMap<Punching, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "punching", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.PunchingLocations != null) attributes.AddRange(src.PunchingLocations.Select(x => new IppAttribute(Tag.Integer, "punching-locations", x)));
            if (src.PunchingOffset.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "punching-offset", src.PunchingOffset.Value));
            if (src.PunchingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "punching-reference-edge", map.Map<string>(src.PunchingReferenceEdge.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Stitching>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Stitching>();

            return new Stitching
            {
                StitchingAngle = map.MapFromDicNullable<int?>(src, "stitching-angle"),
                StitchingLocations = map.MapFromDicSetNullable<int[]?>(src, "stitching-locations"),
                StitchingMethod = map.MapFromDicNullable<StitchingMethod?>(src, "stitching-method"),
                StitchingOffset = map.MapFromDicNullable<int?>(src, "stitching-offset"),
                StitchingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "stitching-reference-edge")
            };
        });

        mapper.CreateMap<Stitching, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "stitching", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.StitchingAngle.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "stitching-angle", src.StitchingAngle.Value));
            if (src.StitchingLocations != null) attributes.AddRange(src.StitchingLocations.Select(x => new IppAttribute(Tag.Integer, "stitching-locations", x)));
            if (src.StitchingMethod.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "stitching-method", map.Map<string>(src.StitchingMethod.Value)));
            if (src.StitchingOffset.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "stitching-offset", src.StitchingOffset.Value));
            if (src.StitchingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "stitching-reference-edge", map.Map<string>(src.StitchingReferenceEdge.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Trimming>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Trimming>();

            return new Trimming
            {
                TrimmingOffset = map.MapFromDicSetNullable<int[]?>(src, "trimming-offset"),
                TrimmingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "trimming-reference-edge"),
                TrimmingType = map.MapFromDicNullable<string, TrimmingType?>(src, "trimming-type", (attribute, value) => new TrimmingType(value, attribute.Tag == Tag.Keyword)),
                TrimmingWhen = map.MapFromDicNullable<TrimmingWhen?>(src, "trimming-when")
            };
        });

        mapper.CreateMap<Trimming, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "trimming", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.TrimmingOffset != null) attributes.AddRange(src.TrimmingOffset.Select(x => new IppAttribute(Tag.Integer, "trimming-offset", x)));
            if (src.TrimmingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "trimming-reference-edge", map.Map<string>(src.TrimmingReferenceEdge.Value)));
            if (src.TrimmingType.HasValue)
            {
                var trimmingTypeValue = src.TrimmingType.Value;
                var trimmingType = map.Map<string>(trimmingTypeValue);
                var trimmingTypeTag = trimmingTypeValue.ToIppTag();
                attributes.Add(new IppAttribute(trimmingTypeTag, "trimming-type", trimmingType));
            }
            if (src.TrimmingWhen.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "trimming-when", map.Map<string>(src.TrimmingWhen.Value)));
            return attributes;
        });
    }
}
