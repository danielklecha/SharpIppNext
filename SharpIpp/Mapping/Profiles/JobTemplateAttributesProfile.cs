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
            dst ??= new IppRequestMessage();
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

            if (src.OutputBin != null)
            {
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.OutputBin, src.OutputBin));
            }

            if (src.JobAccountId != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobAccountId, src.JobAccountId));

            if (src.JobAccountingUserId != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobAccountingUserId, src.JobAccountingUserId));

            if (src.JobCancelAfter != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.JobCancelAfter, src.JobCancelAfter.Value));

            if (src.JobDelayOutputUntil != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobDelayOutputUntil, src.JobDelayOutputUntil));

            if (src.JobDelayOutputUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobDelayOutputUntilTime, src.JobDelayOutputUntilTime.Value));

            if (src.JobHoldUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobHoldUntilTime, src.JobHoldUntilTime.Value));

            if (src.JobRetainUntil != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobRetainUntil, src.JobRetainUntil));

            if (src.JobRetainUntilInterval != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.JobRetainUntilInterval, src.JobRetainUntilInterval.Value));

            if (src.JobRetainUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobRetainUntilTime, src.JobRetainUntilTime.Value));

            if (src.JobSheetMessage != null)
                job.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobSheetMessage, src.JobSheetMessage));

            if (src.OutputDevice != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputDevice, src.OutputDevice));

            if (src.PrintContentOptimize != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintContentOptimize, src.PrintContentOptimize));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, JobTemplateAttributes>((src, dst, map) =>
        {
            dst ??= new JobTemplateAttributes();
            var jobDict = src.JobAttributes.ToIppDictionary();
            dst.JobPriority = map.MapFromDicNullable<int?>(jobDict, JobAttribute.JobPriority);
            dst.JobHoldUntil = map.MapFromDicNullable<JobHoldUntil?>(jobDict, JobAttribute.JobHoldUntil);
            dst.MultipleDocumentHandling = map.MapFromDicNullable<MultipleDocumentHandling?>(jobDict, JobAttribute.MultipleDocumentHandling);
            dst.JobSheets = map.MapFromDicNullable<JobSheets?>(jobDict, JobAttribute.JobSheets);
            dst.Copies = map.MapFromDicNullable<int?>(jobDict, JobAttribute.Copies);
            dst.Finishings = map.MapFromDicNullable<Finishings?>(jobDict, JobAttribute.Finishings);
            dst.PageRanges = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(jobDict, JobAttribute.PageRanges);
            dst.Sides = map.MapFromDicNullable<Sides?>(jobDict, JobAttribute.Sides);
            dst.NumberUp = map.MapFromDicNullable<int?>(jobDict, JobAttribute.NumberUp);
            dst.OrientationRequested = map.MapFromDicNullable<Orientation?>(jobDict, JobAttribute.OrientationRequested);
            dst.Media = map.MapFromDicNullable<string?>(jobDict, JobAttribute.Media);
            dst.PrinterResolution = map.MapFromDicNullable<Resolution?>(jobDict, JobAttribute.PrinterResolution);
            dst.PrintQuality = map.MapFromDicNullable<PrintQuality?>(jobDict, JobAttribute.PrintQuality);
            dst.PrintScaling = map.MapFromDicNullable<PrintScaling?>(jobDict, JobAttribute.PrintScaling);
            dst.PrintColorMode = map.MapFromDicNullable<PrintColorMode?>(jobDict, JobAttribute.PrintColorMode);
            if (jobDict.ContainsKey(JobAttribute.MediaCol))
                dst.MediaCol = map.Map<MediaCol>(jobDict[JobAttribute.MediaCol].FromBegCollection().ToIppDictionary());
            dst.OutputBin = map.MapFromDicNullable<string?>(jobDict, JobAttribute.OutputBin);
            dst.JobAccountId = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobAccountId);
            dst.JobAccountingUserId = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobAccountingUserId);
            dst.JobCancelAfter = map.MapFromDicNullable<int?>(jobDict, JobAttribute.JobCancelAfter);
            dst.JobDelayOutputUntil = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobDelayOutputUntil);
            dst.JobDelayOutputUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, JobAttribute.JobDelayOutputUntilTime);
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, JobAttribute.JobHoldUntilTime);
            dst.JobRetainUntil = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobRetainUntil);
            dst.JobRetainUntilInterval = map.MapFromDicNullable<int?>(jobDict, JobAttribute.JobRetainUntilInterval);
            dst.JobRetainUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, JobAttribute.JobRetainUntilTime);
            dst.JobSheetMessage = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobSheetMessage);
            dst.OutputDevice = map.MapFromDicNullable<string?>(jobDict, JobAttribute.OutputDevice);
            dst.PrintContentOptimize = map.MapFromDicNullable<string?>(jobDict, JobAttribute.PrintContentOptimize);
            return dst;
        });
    }
}
