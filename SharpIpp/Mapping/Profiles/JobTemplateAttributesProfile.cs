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
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobPriority, src.JobPriority.Value));
            }

            if (src.JobHoldUntil != null)
            {
                job.Add(new IppAttribute(Tag.NameWithoutLanguage,
                    IppAttributeNames.JobHoldUntil,
                    map.Map<string>(src.JobHoldUntil.Value)));
            }

            if (src.MultipleDocumentHandling != null)
            {
                job.Add(new IppAttribute(Tag.Keyword,
                    IppAttributeNames.MultipleDocumentHandling,
                    map.Map<string>(src.MultipleDocumentHandling.Value)));
            }

            if (src.JobSheets != null)
            {
                job.Add(new IppAttribute(Tag.Keyword,
                    IppAttributeNames.JobSheets,
                    map.Map<string>(src.JobSheets.Value)));
            }

            if (src.JobSheetsCol != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobSheetsCol).ToBegCollection(IppAttributeNames.JobSheetsCol));

            if (src.Copies != null)
            {
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Copies, src.Copies.Value));
            }

            if (src.Finishings != null)
            {
                var finishings = src.Finishings;
                if (finishings.Length > 1)
                    finishings = finishings.Where(x => x != Finishings.None).ToArray();

                job.AddRange(finishings.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)x)));
            }

            if (src.PageRanges != null)
            {
                job.AddRange(src.PageRanges.Select(pageRange =>
                    new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PageRanges, pageRange)));
            }

            if (src.Sides != null)
            {
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Sides, map.Map<string>(src.Sides.Value)));
            }

            if (src.NumberUp != null)
            {
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.NumberUp, src.NumberUp.Value));
            }

            if (src.OrientationRequested != null)
            {
                job.Add(new IppAttribute(Tag.Enum, IppAttributeNames.OrientationRequested, (int)src.OrientationRequested.Value));
            }

            if (src.Media != null)
            {
                var mediaValue = src.Media.Value;
                var media = map.Map<string>(mediaValue);
                var mediaTag = mediaValue.ToIppTag();
                job.Add(new IppAttribute(mediaTag, IppAttributeNames.Media, media));
            }

            if (src.PrinterResolution != null)
            {
                job.Add(new IppAttribute(Tag.Resolution, IppAttributeNames.PrinterResolution, src.PrinterResolution.Value));
            }

            if (src.PrintQuality != null)
            {
                job.Add(new IppAttribute(Tag.Enum, IppAttributeNames.PrintQuality, (int)src.PrintQuality.Value));
            }

            if (src.PrintScaling != null)
            {
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintScaling, map.Map<string>(src.PrintScaling.Value)));
            }

            if (src.PrintColorMode != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintColorMode, map.Map<string>(src.PrintColorMode.Value)));

            if (src.PrintRenderingIntent != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintRenderingIntent, map.Map<string>(src.PrintRenderingIntent.Value)));

            if (src.JobErrorAction != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobErrorAction, map.Map<string>(src.JobErrorAction.Value)));

            if (src.MediaCol != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(IppAttributeNames.MediaCol));

            if (src.FinishingsCol != null)
            {
                foreach (var finishingsCol in src.FinishingsCol)
                {
                    job.AddRange(map.Map<IEnumerable<IppAttribute>>(finishingsCol).ToBegCollection(IppAttributeNames.FinishingsCol));
                }
            }

            if (src.OutputBin != null)
            {
                var outputBinValue = src.OutputBin.Value;
                var outputBin = map.Map<string>(outputBinValue);
                var outputBinTag = outputBinValue.ToIppTag();
                job.Add(new IppAttribute(outputBinTag, IppAttributeNames.OutputBin, outputBin));
            }

            if (src.JobAccountId != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobAccountId, src.JobAccountId));

            if (src.JobAccountType != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobAccountType, map.Map<string>(src.JobAccountType.Value)));

            if (src.JobAccountingUserId != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobAccountingUserId, src.JobAccountingUserId));

            if (src.JobCancelAfter != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobCancelAfter, src.JobCancelAfter.Value));

            if (src.JobDelayOutputUntil != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobDelayOutputUntil, map.Map<string>(src.JobDelayOutputUntil.Value)));

            if (src.JobDelayOutputUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.JobDelayOutputUntilTime, src.JobDelayOutputUntilTime.Value));

            if (src.JobHoldUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.JobHoldUntilTime, src.JobHoldUntilTime.Value));

            if (src.JobRetainUntil != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobRetainUntil, map.Map<string>(src.JobRetainUntil.Value)));

            if (src.JobRetainUntilInterval != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobRetainUntilInterval, src.JobRetainUntilInterval.Value));

            if (src.JobRetainUntilTime != null)
                job.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.JobRetainUntilTime, src.JobRetainUntilTime.Value));

            if (src.JobSheetMessage != null)
                job.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobSheetMessage, src.JobSheetMessage));

            if (src.OutputDevice != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputDevice, src.OutputDevice));

            if (src.PrintContentOptimize != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintContentOptimize, map.Map<string>(src.PrintContentOptimize.Value)));

            if (src.JobPagesPerSet != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobPagesPerSet, src.JobPagesPerSet.Value));

            if (src.CoverBack != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverBack).ToBegCollection(IppAttributeNames.CoverBack));

            if (src.CoverFront != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverFront).ToBegCollection(IppAttributeNames.CoverFront));

            if (src.ForceFrontSide != null)
                job.AddRange(src.ForceFrontSide.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ForceFrontSide, x)));

            if (src.ImageOrientation != null)
                job.Add(new IppAttribute(Tag.Enum, IppAttributeNames.ImageOrientation, (int)src.ImageOrientation.Value));

            if (src.ImpositionTemplate != null)
            {
                var impositionTemplateValue = src.ImpositionTemplate.Value;
                var impositionTemplate = map.Map<string>(impositionTemplateValue);
                var impositionTemplateTag = impositionTemplateValue.ToIppTag();
                job.Add(new IppAttribute(impositionTemplateTag, IppAttributeNames.ImpositionTemplate, impositionTemplate));
            }

            if (src.InsertSheet != null)
                job.AddRange(src.InsertSheet.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.InsertSheet)));

            if (src.JobAccountingSheets != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobAccountingSheets).ToBegCollection(IppAttributeNames.JobAccountingSheets));

            if (src.JobCompleteBefore != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobCompleteBefore, map.Map<string>(src.JobCompleteBefore.Value)));

            if (src.JobCompleteBeforeTime != null)
                job.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.JobCompleteBeforeTime, src.JobCompleteBeforeTime.Value));

            if (src.JobErrorSheet != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobErrorSheet).ToBegCollection(IppAttributeNames.JobErrorSheet));

            if (src.JobMessageToOperator != null)
                job.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobMessageToOperator, src.JobMessageToOperator));

            if (src.JobPhoneNumber != null)
                job.Add(new IppAttribute(Tag.Uri, IppAttributeNames.JobPhoneNumber, src.JobPhoneNumber));

            if (src.JobRecipientName != null)
                job.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobRecipientName, src.JobRecipientName));

            if (src.MediaInputTrayCheck != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MediaInputTrayCheck, map.Map<string>(src.MediaInputTrayCheck.Value)));

            if (src.PageDelivery != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PageDelivery, map.Map<string>(src.PageDelivery.Value)));

            if (src.PresentationDirectionNumberUp != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PresentationDirectionNumberUp, map.Map<string>(src.PresentationDirectionNumberUp.Value)));

            if (src.SeparatorSheets != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.SeparatorSheets).ToBegCollection(IppAttributeNames.SeparatorSheets));

            if (src.XImagePosition != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.XImagePosition, map.Map<string>(src.XImagePosition.Value)));

            if (src.XImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.XImageShift, src.XImageShift.Value));

            if (src.XSide1ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.XSide1ImageShift, src.XSide1ImageShift.Value));

            if (src.XSide2ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.XSide2ImageShift, src.XSide2ImageShift.Value));

            if (src.YImagePosition != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.YImagePosition, map.Map<string>(src.YImagePosition.Value)));

            if (src.YImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.YImageShift, src.YImageShift.Value));

            if (src.YSide1ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.YSide1ImageShift, src.YSide1ImageShift.Value));

            if (src.YSide2ImageShift != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.YSide2ImageShift, src.YSide2ImageShift.Value));

            if (src.ConfirmationSheetPrint.HasValue)
                job.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.ConfirmationSheetPrint, src.ConfirmationSheetPrint.Value));

            if (src.NumberOfRetries.HasValue)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfRetries, src.NumberOfRetries.Value));

            if (src.RetryInterval.HasValue)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.RetryInterval, src.RetryInterval.Value));

            if (src.RetryTimeOut.HasValue)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.RetryTimeOut, src.RetryTimeOut.Value));

            if (src.ChamberHumidity.HasValue)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidity, src.ChamberHumidity.Value));

            if (src.ChamberTemperature.HasValue)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperature, src.ChamberTemperature.Value));

            if (src.MaterialsCol != null)
                job.AddRange(src.MaterialsCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.MaterialsCol)));

            if (src.MultipleObjectHandling != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleObjectHandling, map.Map<string>(src.MultipleObjectHandling.Value)));

            if (src.PlatformTemperature.HasValue)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PlatformTemperature, src.PlatformTemperature.Value));

            if (src.PrintAccuracy != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.PrintAccuracy).ToBegCollection(IppAttributeNames.PrintAccuracy));

            if (src.PrintBase != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBase, map.Map<string>(src.PrintBase.Value)));

            if (src.PrintObjects != null)
                job.AddRange(src.PrintObjects.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrintObjects)));

            if (src.PrintSupports != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupports, map.Map<string>(src.PrintSupports.Value)));

            if (src.Overrides != null)
                job.AddRange(src.Overrides.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.Overrides)));

            if (src.JobCopies != null)
                job.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobCopies, src.JobCopies.Value));

            if (src.JobCoverBack != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobCoverBack).ToBegCollection(IppAttributeNames.JobCoverBack));

            if (src.JobCoverFront != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobCoverFront).ToBegCollection(IppAttributeNames.JobCoverFront));

            if (src.JobFinishings != null)
            {
                var jobFinishings = src.JobFinishings;
                if (jobFinishings.Length > 1)
                    jobFinishings = jobFinishings.Where(x => x != Finishings.None).ToArray();
                job.AddRange(jobFinishings.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.JobFinishings, (int)x)));
            }

            if (src.JobFinishingsCol != null)
                job.AddRange(src.JobFinishingsCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.JobFinishingsCol)));

            if (src.JobPassword != null)
                job.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.JobPassword, src.JobPassword.Value));

            if (src.JobPasswordEncryption != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobPasswordEncryption, map.Map<string>(src.JobPasswordEncryption.Value)));

            if (src.SheetCollate != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.SheetCollate, src.SheetCollate.Value.Value));

            if (src.PageOverrides != null)
                job.AddRange(src.PageOverrides.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PageOverrides)));

            if (src.PagesPerSubset != null)
                job.AddRange(src.PagesPerSubset.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.PagesPerSubset, x)));

            if (src.DocumentOverrides != null)
                job.AddRange(src.DocumentOverrides.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.DocumentOverrides)));

            if (src.MediaSource != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MediaSource, map.Map<string>(src.MediaSource.Value)));

            if (src.MediaSourceFeedDirection != null)
                job.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MediaSourceFeedDirection, map.Map<string>(src.MediaSourceFeedDirection.Value)));

            if (src.MediaSourceFeedOrientation != null)
                job.Add(new IppAttribute(Tag.Enum, IppAttributeNames.MediaSourceFeedOrientation, (int)src.MediaSourceFeedOrientation.Value));

            if (src.RequestingUserUri != null)
                job.Add(new IppAttribute(Tag.Uri, IppAttributeNames.RequestingUserUri, src.RequestingUserUri.ToString()));

            if (src.JobMandatoryAttributes != null)
                job.AddRange(src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, x)));

            if (src.JobIds != null)
                job.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, x)));

            if (src.JobSaveDisposition != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobSaveDisposition).ToBegCollection(IppAttributeNames.JobSaveDisposition));

            if (src.PdlInitFile != null)
                job.AddRange(map.Map<IEnumerable<IppAttribute>>(src.PdlInitFile).ToBegCollection(IppAttributeNames.PdlInitFile));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, JobTemplateAttributes>((src, dst, map) =>
        {
            dst ??= new JobTemplateAttributes();
            var jobDict = src.JobAttributes.ToIppDictionary();
            dst.JobPriority = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.JobPriority);
            dst.JobHoldUntil = map.MapFromDicNullable<JobHoldUntil?>(jobDict, IppAttributeNames.JobHoldUntil);
            dst.MultipleDocumentHandling = map.MapFromDicNullable<MultipleDocumentHandling?>(jobDict, IppAttributeNames.MultipleDocumentHandling);
            dst.JobSheets = map.MapFromDicNullable<JobSheets?>(jobDict, IppAttributeNames.JobSheets);
            if (jobDict.ContainsKey(IppAttributeNames.JobSheetsCol))
                dst.JobSheetsCol = map.Map<JobSheetsCol>(jobDict[IppAttributeNames.JobSheetsCol].FromBegCollection().ToIppDictionary());
            dst.Copies = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.Copies);
            dst.Finishings = map.MapFromDicSetNullable<Finishings[]?>(jobDict, IppAttributeNames.Finishings);
            dst.PageRanges = map.MapFromDicSetNullable<Protocol.Models.Range[]?>(jobDict, IppAttributeNames.PageRanges);
            dst.Sides = map.MapFromDicNullable<Sides?>(jobDict, IppAttributeNames.Sides);
            dst.NumberUp = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.NumberUp);
            dst.OrientationRequested = map.MapFromDicNullable<Orientation?>(jobDict, IppAttributeNames.OrientationRequested);
            dst.Media = map.MapFromDicNullable<string, Media?>(jobDict, IppAttributeNames.Media, (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword));
            dst.PrinterResolution = map.MapFromDicNullable<Resolution?>(jobDict, IppAttributeNames.PrinterResolution);
            dst.PrintQuality = map.MapFromDicNullable<PrintQuality?>(jobDict, IppAttributeNames.PrintQuality);
            dst.PrintScaling = map.MapFromDicNullable<PrintScaling?>(jobDict, IppAttributeNames.PrintScaling);
            dst.PrintColorMode = map.MapFromDicNullable<PrintColorMode?>(jobDict, IppAttributeNames.PrintColorMode);
            dst.PrintRenderingIntent = map.MapFromDicNullable<PrintRenderingIntent?>(jobDict, IppAttributeNames.PrintRenderingIntent);
            dst.JobErrorAction = map.MapFromDicNullable<JobErrorAction?>(jobDict, IppAttributeNames.JobErrorAction);
            if (jobDict.ContainsKey(IppAttributeNames.MediaCol))
                dst.MediaCol = map.Map<MediaCol>(jobDict[IppAttributeNames.MediaCol].FromBegCollection().ToIppDictionary());
            if (jobDict.ContainsKey(IppAttributeNames.FinishingsCol))
                dst.FinishingsCol = jobDict[IppAttributeNames.FinishingsCol].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.OutputBin = map.MapFromDicNullable<string, OutputBin?>(jobDict, IppAttributeNames.OutputBin, (attribute, value) => new OutputBin(value, attribute.Tag == Tag.Keyword));
            dst.JobAccountId = map.MapFromDicNullable<string?>(jobDict, IppAttributeNames.JobAccountId);
            dst.JobAccountType = map.MapFromDicNullable<JobAccountType?>(jobDict, IppAttributeNames.JobAccountType);
            dst.JobAccountingUserId = map.MapFromDicNullable<string?>(jobDict, IppAttributeNames.JobAccountingUserId);
            dst.JobCancelAfter = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.JobCancelAfter);
            dst.JobDelayOutputUntil = map.MapFromDicNullable<JobHoldUntil?>(jobDict, IppAttributeNames.JobDelayOutputUntil);
            dst.JobDelayOutputUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, IppAttributeNames.JobDelayOutputUntilTime);
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, IppAttributeNames.JobHoldUntilTime);
            dst.JobRetainUntil = map.MapFromDicNullable<JobHoldUntil?>(jobDict, IppAttributeNames.JobRetainUntil);
            dst.JobRetainUntilInterval = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.JobRetainUntilInterval);
            dst.JobRetainUntilTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, IppAttributeNames.JobRetainUntilTime);
            dst.JobSheetMessage = map.MapFromDicNullable<string?>(jobDict, IppAttributeNames.JobSheetMessage);
            dst.OutputDevice = map.MapFromDicNullable<string?>(jobDict, IppAttributeNames.OutputDevice);
            dst.PrintContentOptimize = map.MapFromDicNullable<PrintContentOptimize?>(jobDict, IppAttributeNames.PrintContentOptimize);
            dst.JobPagesPerSet = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.JobPagesPerSet);
            if (jobDict.ContainsKey(IppAttributeNames.CoverBack))
                dst.CoverBack = map.Map<Cover>(jobDict[IppAttributeNames.CoverBack].FromBegCollection().ToIppDictionary());
            if (jobDict.ContainsKey(IppAttributeNames.CoverFront))
                dst.CoverFront = map.Map<Cover>(jobDict[IppAttributeNames.CoverFront].FromBegCollection().ToIppDictionary());
            dst.ForceFrontSide = map.MapFromDicSetNullable<int[]?>(jobDict, IppAttributeNames.ForceFrontSide);
            dst.ImageOrientation = map.MapFromDicNullable<Orientation?>(jobDict, IppAttributeNames.ImageOrientation);
            dst.ImpositionTemplate = map.MapFromDicNullable<string, ImpositionTemplate?>(jobDict, IppAttributeNames.ImpositionTemplate, (attribute, value) => new ImpositionTemplate(value, attribute.Tag == Tag.Keyword));
            if (jobDict.ContainsKey(IppAttributeNames.InsertSheet))
                dst.InsertSheet = jobDict[IppAttributeNames.InsertSheet].GroupBegCollection().Select(x => map.Map<InsertSheet>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (jobDict.ContainsKey(IppAttributeNames.JobAccountingSheets))
                dst.JobAccountingSheets = map.Map<JobAccountingSheets>(jobDict[IppAttributeNames.JobAccountingSheets].FromBegCollection().ToIppDictionary());
            dst.JobCompleteBefore = map.MapFromDicNullable<JobCompleteBefore?>(jobDict, IppAttributeNames.JobCompleteBefore);
            dst.JobCompleteBeforeTime = map.MapFromDicNullable<DateTimeOffset?>(jobDict, IppAttributeNames.JobCompleteBeforeTime);
            if (jobDict.ContainsKey(IppAttributeNames.JobErrorSheet))
                dst.JobErrorSheet = map.Map<JobErrorSheet>(jobDict[IppAttributeNames.JobErrorSheet].FromBegCollection().ToIppDictionary());
            dst.JobMessageToOperator = map.MapFromDicNullable<string?>(jobDict, IppAttributeNames.JobMessageToOperator);
            dst.JobPhoneNumber = map.MapFromDicNullable<string?>(jobDict, IppAttributeNames.JobPhoneNumber);
            dst.JobRecipientName = map.MapFromDicNullable<string?>(jobDict, IppAttributeNames.JobRecipientName);
            dst.MediaInputTrayCheck = map.MapFromDicNullable<MediaInputTrayCheck?>(jobDict, IppAttributeNames.MediaInputTrayCheck);
            dst.PageDelivery = map.MapFromDicNullable<PageDelivery?>(jobDict, IppAttributeNames.PageDelivery);
            dst.PresentationDirectionNumberUp = map.MapFromDicNullable<PresentationDirectionNumberUp?>(jobDict, IppAttributeNames.PresentationDirectionNumberUp);
            if (jobDict.ContainsKey(IppAttributeNames.SeparatorSheets))
                dst.SeparatorSheets = map.Map<SeparatorSheets>(jobDict[IppAttributeNames.SeparatorSheets].FromBegCollection().ToIppDictionary());
            dst.XImagePosition = map.MapFromDicNullable<XImagePosition?>(jobDict, IppAttributeNames.XImagePosition);
            dst.XImageShift = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.XImageShift);
            dst.XSide1ImageShift = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.XSide1ImageShift);
            dst.XSide2ImageShift = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.XSide2ImageShift);
            dst.YImagePosition = map.MapFromDicNullable<YImagePosition?>(jobDict, IppAttributeNames.YImagePosition);
            dst.YImageShift = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.YImageShift);
            dst.YSide1ImageShift = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.YSide1ImageShift);
            dst.YSide2ImageShift = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.YSide2ImageShift);
            dst.ConfirmationSheetPrint = map.MapFromDicNullable<bool?>(jobDict, IppAttributeNames.ConfirmationSheetPrint);
            dst.NumberOfRetries = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.NumberOfRetries);
            dst.RetryInterval = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.RetryInterval);
            dst.RetryTimeOut = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.RetryTimeOut);
            dst.ChamberHumidity = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.ChamberHumidity);
            dst.ChamberTemperature = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.ChamberTemperature);
            if (jobDict.ContainsKey(IppAttributeNames.MaterialsCol))
                dst.MaterialsCol = jobDict[IppAttributeNames.MaterialsCol].GroupBegCollection().Select(x => map.Map<Material>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.MultipleObjectHandling = map.MapFromDicNullable<MultipleObjectHandling?>(jobDict, IppAttributeNames.MultipleObjectHandling);
            dst.PlatformTemperature = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.PlatformTemperature);
            if (jobDict.ContainsKey(IppAttributeNames.PrintAccuracy))
                dst.PrintAccuracy = map.Map<PrintAccuracy>(jobDict[IppAttributeNames.PrintAccuracy].GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.PrintBase = map.MapFromDicNullable<PrintBase?>(jobDict, IppAttributeNames.PrintBase);
            if (jobDict.ContainsKey(IppAttributeNames.PrintObjects))
                dst.PrintObjects = jobDict[IppAttributeNames.PrintObjects].GroupBegCollection().Select(x => map.Map<PrintObject>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.PrintSupports = map.MapFromDicNullable<PrintSupports?>(jobDict, IppAttributeNames.PrintSupports);
            if (jobDict.ContainsKey(IppAttributeNames.Overrides))
                dst.Overrides = jobDict[IppAttributeNames.Overrides].GroupBegCollection().Select(x => map.Map<OverrideInstruction>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.JobCopies = map.MapFromDicNullable<int?>(jobDict, IppAttributeNames.JobCopies);
            if (jobDict.ContainsKey(IppAttributeNames.JobCoverBack))
                dst.JobCoverBack = map.Map<Cover>(jobDict[IppAttributeNames.JobCoverBack].FromBegCollection().ToIppDictionary());
            if (jobDict.ContainsKey(IppAttributeNames.JobCoverFront))
                dst.JobCoverFront = map.Map<Cover>(jobDict[IppAttributeNames.JobCoverFront].FromBegCollection().ToIppDictionary());
            dst.JobFinishings = map.MapFromDicSetNullable<Finishings[]?>(jobDict, IppAttributeNames.JobFinishings);
            if (jobDict.ContainsKey(IppAttributeNames.JobFinishingsCol))
                dst.JobFinishingsCol = jobDict[IppAttributeNames.JobFinishingsCol].GroupBegCollection().Select(x => map.Map<FinishingsCol>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.JobPassword = map.MapFromDicNullable<OctetString?>(jobDict, IppAttributeNames.JobPassword);
            dst.JobPasswordEncryption = map.MapFromDicNullable<JobPasswordEncryption?>(jobDict, IppAttributeNames.JobPasswordEncryption);
            dst.SheetCollate = map.MapFromDicNullable<SheetCollate?>(jobDict, IppAttributeNames.SheetCollate);
            if (jobDict.ContainsKey(IppAttributeNames.PageOverrides))
                dst.PageOverrides = jobDict[IppAttributeNames.PageOverrides].GroupBegCollection().Select(x => map.Map<OverrideInstruction>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.PagesPerSubset = map.MapFromDicSetNullable<int[]?>(jobDict, IppAttributeNames.PagesPerSubset);
            if (jobDict.ContainsKey(IppAttributeNames.DocumentOverrides))
                dst.DocumentOverrides = jobDict[IppAttributeNames.DocumentOverrides].GroupBegCollection().Select(x => map.Map<OverrideInstruction>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.MediaSource = map.MapFromDicNullable<MediaSource?>(jobDict, IppAttributeNames.MediaSource);
            dst.MediaSourceFeedDirection = map.MapFromDicNullable<MediaSourceFeedDirection?>(jobDict, IppAttributeNames.MediaSourceFeedDirection);
            dst.MediaSourceFeedOrientation = map.MapFromDicNullable<Orientation?>(jobDict, IppAttributeNames.MediaSourceFeedOrientation);
            dst.RequestingUserUri = map.MapFromDicNullable<string, Uri?>(jobDict, IppAttributeNames.RequestingUserUri, (_, value) => new Uri(value));
            dst.JobMandatoryAttributes = map.MapFromDicSetNullable<string[]?>(jobDict, IppAttributeNames.JobMandatoryAttributes);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(jobDict, IppAttributeNames.JobIds);
            if (jobDict.ContainsKey(IppAttributeNames.JobSaveDisposition))
                dst.JobSaveDisposition = map.Map<JobSaveDisposition>(jobDict[IppAttributeNames.JobSaveDisposition].FromBegCollection().ToIppDictionary());
            if (jobDict.ContainsKey(IppAttributeNames.PdlInitFile))
                dst.PdlInitFile = map.Map<PdlInitFile>(jobDict[IppAttributeNames.PdlInitFile].FromBegCollection().ToIppDictionary());
            return dst;
        });
    }
}
