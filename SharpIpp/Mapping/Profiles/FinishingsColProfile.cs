using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class FinishingsColProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, FinishingsCol>((src, map) =>
        {
            if (src.Count == 1)
            {
                using var enumerator = src.GetEnumerator();
                enumerator.MoveNext();
                var first = enumerator.Current.Value;
                if (first != null && first.Length == 1 && first[0].Tag.IsOutOfBand())
                {
                    return NoValue.GetNoValue<FinishingsCol>();
                }
            }

            var finishingsCol = new FinishingsCol
            {
                FinishingTemplate = map.MapFromDicNullable<string?>(src, "finishing-template"),
                ImpositionTemplate = map.MapFromDicNullable<string?>(src, "imposition-template"),
                MediaSheetsSupported = map.MapFromDicNullable<Range?>(src, "media-sheets-supported"),
                MediaSizeName = map.MapFromDicNullable<string?>(src, "media-size-name"),
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
            var attributes = new List<IppAttribute>();
            if (src.FinishingTemplate != null)
                attributes.Add(new IppAttribute(Tag.Keyword, "finishing-template", src.FinishingTemplate));
            if (src.ImpositionTemplate != null)
                attributes.Add(new IppAttribute(Tag.Keyword, "imposition-template", src.ImpositionTemplate));
            if (src.MediaSheetsSupported != null)
                attributes.Add(new IppAttribute(Tag.RangeOfInteger, "media-sheets-supported", src.MediaSheetsSupported.Value));
            if (src.MediaSizeName != null)
                attributes.Add(new IppAttribute(Tag.Keyword, "media-size-name", src.MediaSizeName));
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

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Baling>((src, map) => new Baling
        {
            BalingType = map.MapFromDicNullable<string?>(src, "baling-type"),
            BalingWhen = map.MapFromDicNullable<BalingWhen?>(src, "baling-when")
        });

        mapper.CreateMap<Baling, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.BalingType != null) attributes.Add(new IppAttribute(Tag.Keyword, "baling-type", src.BalingType));
            if (src.BalingWhen.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "baling-when", map.Map<string>(src.BalingWhen.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Binding>((src, map) => new Binding
        {
            BindingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "binding-reference-edge"),
            BindingType = map.MapFromDicNullable<string?>(src, "binding-type")
        });

        mapper.CreateMap<Binding, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.BindingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "binding-reference-edge", map.Map<string>(src.BindingReferenceEdge.Value)));
            if (src.BindingType != null) attributes.Add(new IppAttribute(Tag.Keyword, "binding-type", src.BindingType));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Coating>((src, map) => new Coating
        {
            CoatingSides = map.MapFromDicNullable<CoatingSides?>(src, "coating-sides"),
            CoatingType = map.MapFromDicNullable<string?>(src, "coating-type")
        });

        mapper.CreateMap<Coating, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.CoatingSides.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "coating-sides", map.Map<string>(src.CoatingSides.Value)));
            if (src.CoatingType != null) attributes.Add(new IppAttribute(Tag.Keyword, "coating-type", src.CoatingType));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Covering>((src, map) => new Covering
        {
            CoveringName = map.MapFromDicNullable<string?>(src, "covering-name")
        });

        mapper.CreateMap<Covering, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.CoveringName != null) attributes.Add(new IppAttribute(Tag.Keyword, "covering-name", src.CoveringName));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Folding>((src, map) => new Folding
        {
            FoldingDirection = map.MapFromDicNullable<FoldingDirection?>(src, "folding-direction"),
            FoldingOffset = map.MapFromDicNullable<int?>(src, "folding-offset"),
            FoldingLocation = map.MapFromDicNullable<int?>(src, "folding-location"),
            FoldingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "folding-reference-edge")
        });

        mapper.CreateMap<Folding, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.FoldingDirection.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "folding-direction", map.Map<string>(src.FoldingDirection.Value)));
            if (src.FoldingOffset.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "folding-offset", src.FoldingOffset.Value));
            if (src.FoldingLocation.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "folding-location", src.FoldingLocation.Value));
            if (src.FoldingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "folding-reference-edge", map.Map<string>(src.FoldingReferenceEdge.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Laminating>((src, map) => new Laminating
        {
            LaminatingSides = map.MapFromDicNullable<CoatingSides?>(src, "laminating-sides"),
            LaminatingType = map.MapFromDicNullable<string?>(src, "laminating-type")
        });

        mapper.CreateMap<Laminating, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.LaminatingSides.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "laminating-sides", map.Map<string>(src.LaminatingSides.Value)));
            if (src.LaminatingType != null) attributes.Add(new IppAttribute(Tag.Keyword, "laminating-type", src.LaminatingType));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Punching>((src, map) => new Punching
        {
            PunchingLocations = map.MapFromDicSetNullable<int[]?>(src, "punching-locations"),
            PunchingOffset = map.MapFromDicNullable<int?>(src, "punching-offset"),
            PunchingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "punching-reference-edge")
        });

        mapper.CreateMap<Punching, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.PunchingLocations != null) attributes.AddRange(src.PunchingLocations.Select(x => new IppAttribute(Tag.Integer, "punching-locations", x)));
            if (src.PunchingOffset.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "punching-offset", src.PunchingOffset.Value));
            if (src.PunchingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "punching-reference-edge", map.Map<string>(src.PunchingReferenceEdge.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Stitching>((src, map) => new Stitching
        {
            StitchingAngle = map.MapFromDicNullable<int?>(src, "stitching-angle"),
            StitchingLocations = map.MapFromDicSetNullable<int[]?>(src, "stitching-locations"),
            StitchingMethod = map.MapFromDicNullable<StitchingMethod?>(src, "stitching-method"),
            StitchingOffset = map.MapFromDicNullable<int?>(src, "stitching-offset"),
            StitchingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "stitching-reference-edge")
        });

        mapper.CreateMap<Stitching, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.StitchingAngle.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "stitching-angle", src.StitchingAngle.Value));
            if (src.StitchingLocations != null) attributes.AddRange(src.StitchingLocations.Select(x => new IppAttribute(Tag.Integer, "stitching-locations", x)));
            if (src.StitchingMethod.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "stitching-method", map.Map<string>(src.StitchingMethod.Value)));
            if (src.StitchingOffset.HasValue) attributes.Add(new IppAttribute(Tag.Integer, "stitching-offset", src.StitchingOffset.Value));
            if (src.StitchingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "stitching-reference-edge", map.Map<string>(src.StitchingReferenceEdge.Value)));
            return attributes;
        });

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, Trimming>((src, map) => new Trimming
        {
            TrimmingOffset = map.MapFromDicSetNullable<int[]?>(src, "trimming-offset"),
            TrimmingReferenceEdge = map.MapFromDicNullable<FinishingReferenceEdge?>(src, "trimming-reference-edge"),
            TrimmingType = map.MapFromDicNullable<TrimmingType?>(src, "trimming-type"),
            TrimmingWhen = map.MapFromDicNullable<TrimmingWhen?>(src, "trimming-when")
        });

        mapper.CreateMap<Trimming, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.TrimmingOffset != null) attributes.AddRange(src.TrimmingOffset.Select(x => new IppAttribute(Tag.Integer, "trimming-offset", x)));
            if (src.TrimmingReferenceEdge.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "trimming-reference-edge", map.Map<string>(src.TrimmingReferenceEdge.Value)));
            if (src.TrimmingType.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "trimming-type", map.Map<string>(src.TrimmingType.Value)));
            if (src.TrimmingWhen.HasValue) attributes.Add(new IppAttribute(Tag.Keyword, "trimming-when", map.Map<string>(src.TrimmingWhen.Value)));
            return attributes;
        });
    }
}

