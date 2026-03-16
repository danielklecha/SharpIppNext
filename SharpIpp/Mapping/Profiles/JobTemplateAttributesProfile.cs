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
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, map.Map<string>(src.Media.Value)));
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

            if (src.FinishingsCol != null)
                job.AddRange(src.FinishingsCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.FinishingsCol)));

            if (src.OutputBin != null)
            {
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.OutputBin, map.Map<string>(src.OutputBin.Value)));
            }

            if (src.JobAccountId != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobAccountId, src.JobAccountId));

            if (src.JobAccountingUserId != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobAccountingUserId, src.JobAccountingUserId));

            if (src.JobCancelAfter != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.JobCancelAfter, src.JobCancelAfter.Value));

            if (src.JobDelayOutputUntil != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobDelayOutputUntil, map.Map<string>(src.JobDelayOutputUntil.Value)));

            if (src.JobDelayOutputUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobDelayOutputUntilTime, src.JobDelayOutputUntilTime.Value));

            if (src.JobHoldUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobHoldUntilTime, src.JobHoldUntilTime.Value));

            if (src.JobRetainUntil != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobRetainUntil, map.Map<string>(src.JobRetainUntil.Value)));

            if (src.JobRetainUntilInterval != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.JobRetainUntilInterval, src.JobRetainUntilInterval.Value));

            if (src.JobRetainUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobRetainUntilTime, src.JobRetainUntilTime.Value));

            if (src.JobSheetMessage != null)
                job.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobSheetMessage, src.JobSheetMessage));

            if (src.OutputDevice != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputDevice, src.OutputDevice));

            if (src.PrintContentOptimize != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintContentOptimize, map.Map<string>(src.PrintContentOptimize.Value)));

            if (src.JobPagesPerSet != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.JobPagesPerSet, src.JobPagesPerSet.Value));

            if (src.CoverBack != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverBack).ToBegCollection(JobAttribute.CoverBack));

            if (src.CoverFront != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverFront).ToBegCollection(JobAttribute.CoverFront));

            if (src.ForceFrontSide != null)
                job.AddRange(src.ForceFrontSide.Select(x => new IppAttribute(Tag.Integer, JobAttribute.ForceFrontSide, x)));

            if (src.ImageOrientation != null)
                job.Add(new IppAttribute(Tag.Enum, JobAttribute.ImageOrientation, (int)src.ImageOrientation.Value));

            if (src.ImpositionTemplate != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.ImpositionTemplate, map.Map<string>(src.ImpositionTemplate.Value)));

            if (src.InsertSheet != null)
                job.AddRange(src.InsertSheet.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.InsertSheet)));

            if (src.JobAccountingSheets != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobAccountingSheets).ToBegCollection(JobAttribute.JobAccountingSheets));

            if (src.JobCompleteBefore != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobCompleteBefore, map.Map<string>(src.JobCompleteBefore.Value)));

            if (src.JobCompleteBeforeTime != null)
                job.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobCompleteBeforeTime, src.JobCompleteBeforeTime.Value));

            if (src.JobErrorSheet != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobErrorSheet).ToBegCollection(JobAttribute.JobErrorSheet));

            if (src.JobMessageToOperator != null)
                job.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobMessageToOperator, src.JobMessageToOperator));

            if (src.JobPhoneNumber != null)
                job.Add(new IppAttribute(Tag.Uri, JobAttribute.JobPhoneNumber, src.JobPhoneNumber));

            if (src.JobRecipientName != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobRecipientName, src.JobRecipientName));

            if (src.MediaInputTrayCheck != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.MediaInputTrayCheck, map.Map<string>(src.MediaInputTrayCheck.Value)));

            if (src.PageDelivery != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.PageDelivery, map.Map<string>(src.PageDelivery.Value)));

            if (src.PresentationDirectionNumberUp != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.PresentationDirectionNumberUp, map.Map<string>(src.PresentationDirectionNumberUp.Value)));

            if (src.SeparatorSheets != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.SeparatorSheets).ToBegCollection(JobAttribute.SeparatorSheets));

            if (src.XImagePosition != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.XImagePosition, map.Map<string>(src.XImagePosition.Value)));

            if (src.XImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.XImageShift, src.XImageShift.Value));

            if (src.XSide1ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.XSide1ImageShift, src.XSide1ImageShift.Value));

            if (src.XSide2ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.XSide2ImageShift, src.XSide2ImageShift.Value));

            if (src.YImagePosition != null)
                job.Add(new IppAttribute(Tag.Keyword, JobAttribute.YImagePosition, map.Map<string>(src.YImagePosition.Value)));

            if (src.YImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.YImageShift, src.YImageShift.Value));

            if (src.YSide1ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.YSide1ImageShift, src.YSide1ImageShift.Value));

            if (src.YSide2ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, JobAttribute.YSide2ImageShift, src.YSide2ImageShift.Value));

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
            dst.Media = map.MapFromDicNullable<Media?>(jobDict, JobAttribute.Media);
            dst.PrinterResolution = map.MapFromDicNullable<Resolution?>(jobDict, JobAttribute.PrinterResolution);
            dst.PrintQuality = map.MapFromDicNullable<PrintQuality?>(jobDict, JobAttribute.PrintQuality);
            dst.PrintScaling = map.MapFromDicNullable<PrintScaling?>(jobDict, JobAttribute.PrintScaling);
            dst.PrintColorMode = map.MapFromDicNullable<PrintColorMode?>(jobDict, JobAttribute.PrintColorMode);
            if (jobDict.ContainsKey(JobAttribute.MediaCol))
                dst.MediaCol = map.Map<MediaCol>(jobDict[JobAttribute.MediaCol].FromBegCollection().ToIppDictionary());
            if (jobDict.ContainsKey(JobAttribute.FinishingsCol))
                dst.FinishingsCol = jobDict[JobAttribute.FinishingsCol].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.OutputBin = map.MapFromDicNullable<OutputBin?>(jobDict, JobAttribute.OutputBin);
            dst.JobAccountId = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobAccountId);
            dst.JobAccountingUserId = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobAccountingUserId);
            dst.JobCancelAfter = map.MapFromDicNullable<int?>(jobDict, JobAttribute.JobCancelAfter);
            dst.JobDelayOutputUntil = map.MapFromDicNullable<JobHoldUntil?>(jobDict, JobAttribute.JobDelayOutputUntil);
            dst.JobDelayOutputUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, JobAttribute.JobDelayOutputUntilTime);
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, JobAttribute.JobHoldUntilTime);
            dst.JobRetainUntil = map.MapFromDicNullable<JobHoldUntil?>(jobDict, JobAttribute.JobRetainUntil);
            dst.JobRetainUntilInterval = map.MapFromDicNullable<int?>(jobDict, JobAttribute.JobRetainUntilInterval);
            dst.JobRetainUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, JobAttribute.JobRetainUntilTime);
            dst.JobSheetMessage = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobSheetMessage);
            dst.OutputDevice = map.MapFromDicNullable<string?>(jobDict, JobAttribute.OutputDevice);
            dst.PrintContentOptimize = map.MapFromDicNullable<PrintContentOptimize?>(jobDict, JobAttribute.PrintContentOptimize);
            dst.JobPagesPerSet = map.MapFromDicNullable<int?>(jobDict, JobAttribute.JobPagesPerSet);
            if (jobDict.ContainsKey(JobAttribute.CoverBack))
                dst.CoverBack = map.Map<Cover>(jobDict[JobAttribute.CoverBack].FromBegCollection().ToIppDictionary());
            if (jobDict.ContainsKey(JobAttribute.CoverFront))
                dst.CoverFront = map.Map<Cover>(jobDict[JobAttribute.CoverFront].FromBegCollection().ToIppDictionary());
            dst.ForceFrontSide = map.MapFromDicSetNullable<int[]?>(jobDict, JobAttribute.ForceFrontSide);
            dst.ImageOrientation = map.MapFromDicNullable<Orientation?>(jobDict, JobAttribute.ImageOrientation);
            dst.ImpositionTemplate = map.MapFromDicNullable<ImpositionTemplate?>(jobDict, JobAttribute.ImpositionTemplate);
            if (jobDict.ContainsKey(JobAttribute.InsertSheet))
                dst.InsertSheet = jobDict[JobAttribute.InsertSheet].GroupBegCollection().Select(x => map.Map<InsertSheet>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (jobDict.ContainsKey(JobAttribute.JobAccountingSheets))
                dst.JobAccountingSheets = map.Map<JobAccountingSheets>(jobDict[JobAttribute.JobAccountingSheets].FromBegCollection().ToIppDictionary());
            dst.JobCompleteBefore = map.MapFromDicNullable<JobHoldUntil?>(jobDict, JobAttribute.JobCompleteBefore);
            dst.JobCompleteBeforeTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, JobAttribute.JobCompleteBeforeTime);
            if (jobDict.ContainsKey(JobAttribute.JobErrorSheet))
                dst.JobErrorSheet = map.Map<JobErrorSheet>(jobDict[JobAttribute.JobErrorSheet].FromBegCollection().ToIppDictionary());
            dst.JobMessageToOperator = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobMessageToOperator);
            dst.JobPhoneNumber = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobPhoneNumber);
            dst.JobRecipientName = map.MapFromDicNullable<string?>(jobDict, JobAttribute.JobRecipientName);
            dst.MediaInputTrayCheck = map.MapFromDicNullable<MediaInputTrayCheck?>(jobDict, JobAttribute.MediaInputTrayCheck);
            dst.PageDelivery = map.MapFromDicNullable<PageDelivery?>(jobDict, JobAttribute.PageDelivery);
            dst.PresentationDirectionNumberUp = map.MapFromDicNullable<PresentationDirectionNumberUp?>(jobDict, JobAttribute.PresentationDirectionNumberUp);
            if (jobDict.ContainsKey(JobAttribute.SeparatorSheets))
                dst.SeparatorSheets = map.Map<SeparatorSheets>(jobDict[JobAttribute.SeparatorSheets].FromBegCollection().ToIppDictionary());
            dst.XImagePosition = map.MapFromDicNullable<XImagePosition?>(jobDict, JobAttribute.XImagePosition);
            dst.XImageShift = map.MapFromDicNullable<int?>(jobDict, JobAttribute.XImageShift);
            dst.XSide1ImageShift = map.MapFromDicNullable<int?>(jobDict, JobAttribute.XSide1ImageShift);
            dst.XSide2ImageShift = map.MapFromDicNullable<int?>(jobDict, JobAttribute.XSide2ImageShift);
            dst.YImagePosition = map.MapFromDicNullable<YImagePosition?>(jobDict, JobAttribute.YImagePosition);
            dst.YImageShift = map.MapFromDicNullable<int?>(jobDict, JobAttribute.YImageShift);
            dst.YSide1ImageShift = map.MapFromDicNullable<int?>(jobDict, JobAttribute.YSide1ImageShift);
            dst.YSide2ImageShift = map.MapFromDicNullable<int?>(jobDict, JobAttribute.YSide2ImageShift);
            return dst;
        });
    }
}
