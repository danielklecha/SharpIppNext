using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class JobTemplateAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<JobTemplateAttributes, IppRequestMessage>((src, dst, map) =>
        {
            var job = dst.JobAttributes;

            if (src.JobPriority != null)
            {
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.JobPriority, src.JobPriority.Value));
            }

            if (src.JobHoldUntil != null)
            {
                job.Add(new IppAttribute(Tag.NameWithoutLanguage,
                    JobAttribute.JobHoldUntil,
                    map.Map<string>(src.JobHoldUntil.Value)));
            }

            if (src.MultipleDocumentHandling != null)
            {
                job.Add(new IppAttribute(Tag.Keyword,
                    JobAttribute.MultipleDocumentHandling,
                    map.Map<string>(src.MultipleDocumentHandling.Value)));
            }

            if (src.JobSheets != null)
            {
                job.Add(new IppAttribute(Tag.Keyword,
                    JobAttribute.JobSheets,
                    map.Map<string>(src.JobSheets.Value)));
            }

            if (src.Copies != null)
            {
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, src.Copies.Value));
            }

            if (src.Finishings != null)
            {
                job.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)src.Finishings.Value));
            }

            if (src.PageRanges != null)
            {
                job.AddRange(src.PageRanges.Select(pageRange =>
                    new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, pageRange)));
            }

            if (src.Sides != null)
            {
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, map.Map<string>(src.Sides.Value)));
            }

            if (src.NumberUp != null)
            {
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.NumberUp, src.NumberUp.Value));
            }

            if (src.OrientationRequested != null)
            {
                job.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)src.OrientationRequested.Value));
            }

            if (src.Media != null)
            {
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, src.Media));
            }

            if (src.PrinterResolution != null)
            {
                job.Add(new IppAttribute(Tag.Resolution, JobAttribute.PrinterResolution, src.PrinterResolution.Value));
            }

            if (src.PrintQuality != null)
            {
                job.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)src.PrintQuality.Value));
            }

            if (src.PrintScaling != null)
            {
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintScaling, map.Map<string>(src.PrintScaling.Value)));
            }

            if (src.PrintColorMode != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintColorMode, map.Map<string>(src.PrintColorMode.Value)));

            if (src.MediaCol != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(JobAttribute.MediaCol));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, JobTemplateAttributes>( ( src, dst, map ) =>
        {
            var jobDict = src.JobAttributes.ToIppDictionary();
            dst.JobPriority = map.MapFromDicNullable<int?>(jobDict, JobAttribute.JobPriority);
            dst.JobHoldUntil = map.MapFromDicNullable<JobHoldUntil?>(jobDict, JobAttribute.JobHoldUntil);
            dst.MultipleDocumentHandling = map.MapFromDicNullable<MultipleDocumentHandling?>(jobDict, JobAttribute.MultipleDocumentHandling);
            dst.JobSheets = map.MapFromDicNullable<JobSheets?>(jobDict, JobAttribute.JobSheets);
            dst.Copies = map.MapFromDicNullable<int?>(jobDict, JobAttribute.Copies);
            dst.Finishings = map.MapFromDicNullable<Finishings?>(jobDict, JobAttribute.Finishings);
            dst.PageRanges = map.MapFromDicSetNullable<Range[]?>(jobDict, JobAttribute.PageRanges);
            dst.Sides = map.MapFromDicNullable<Sides?>(jobDict, JobAttribute.Sides);
            dst.NumberUp = map.MapFromDicNullable<int?>(jobDict, JobAttribute.NumberUp);
            dst.OrientationRequested = map.MapFromDicNullable<Orientation?>(jobDict, JobAttribute.OrientationRequested);
            dst.Media = map.MapFromDicNullable<string?>(jobDict, JobAttribute.Media);
            dst.PrinterResolution = map.MapFromDicNullable<Resolution?>(jobDict, JobAttribute.PrinterResolution);
            dst.PrintQuality = map.MapFromDicNullable<PrintQuality?>(jobDict, JobAttribute.PrintQuality);
            dst.PrintScaling = map.MapFromDicNullable<PrintScaling?>(jobDict, JobAttribute.PrintScaling);
            dst.PrintColorMode = map.MapFromDicNullable<PrintColorMode?>(jobDict, JobAttribute.PrintColorMode);
            if(jobDict.ContainsKey(JobAttribute.MediaCol))
                dst.MediaCol = map.Map<MediaCol>(jobDict[JobAttribute.MediaCol].FromBegCollection().ToIppDictionary());
            return dst;
        } );
    }
}
