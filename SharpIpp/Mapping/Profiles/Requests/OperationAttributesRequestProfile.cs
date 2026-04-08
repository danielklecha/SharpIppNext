using SharpIpp.Models.Requests;
using ResponseOperationAttributes = SharpIpp.Models.Responses.OperationAttributes;
using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class OperationAttributesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OperationAttributes>((src, dst, map) =>
        {
            dst ??= new OperationAttributes();
            dst.AttributesCharset = map.MapFromDicNullable<string?>(src, JobAttribute.AttributesCharset) ?? "utf-8";
            dst.AttributesNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.AttributesNaturalLanguage) ?? "en";
            dst.RequestingUserName = map.MapFromDicNullable<string?>(src, JobAttribute.RequestingUserName);
            dst.RequestingUserUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.RequestingUserUri);
            dst.PrinterUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.PrinterUri);
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.JobHoldUntilTime);
            return dst;
        });

        mapper.CreateMap<OperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            if (src.AttributesCharset == null)
                throw new ArgumentNullException(nameof(src.AttributesCharset));
            dst.Add(new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, src.AttributesCharset));

            if (src.AttributesNaturalLanguage == null)
                throw new ArgumentNullException(nameof(src.AttributesNaturalLanguage));
            dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, src.AttributesNaturalLanguage));

            if (src.PrinterUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, src.PrinterUri.ToString()));

            if (src.RequestingUserName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, src.RequestingUserName));

            if (src.RequestingUserUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.RequestingUserUri, src.RequestingUserUri.ToString()));

            if (src.ClientInfo != null)
                dst.AddRange(src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.ClientInfo)));
            if (src.JobHoldUntilTime != null)
                dst.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobHoldUntilTime, src.JobHoldUntilTime.Value));

            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new JobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobId = map.MapFromDicNullable<int?>(src, JobAttribute.JobId);
            dst.JobUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.JobUri);
            return dst;
        });

        mapper.CreateMap<JobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobId != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobId, src.JobId.Value));
            if (src.JobUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.JobUri, src.JobUri.ToString()));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CreateJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobMandatoryAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobMandatoryAttributes);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo) && clientInfo.GroupBegCollection().Any())
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(JobAttribute.DocumentFormatDetails, out var documentFormatDetails) && documentFormatDetails.GroupBegCollection().Any())
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.JobName = map.MapFromDicNullable<string?>(src, JobAttribute.JobName);
            dst.JobMediaSheets = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheets);
            dst.JobKOctets = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctets);
            dst.IppAttributeFidelity = map.MapFromDicNullable<bool?>(src, JobAttribute.IppAttributeFidelity);
            dst.JobImpressions = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressions);
            dst.JobPassword = map.MapFromDicNullable<string?>(src, JobAttribute.JobPassword);
            dst.JobPasswordEncryption = map.MapFromDicNullable<JobPasswordEncryption?>(src, JobAttribute.JobPasswordEncryption);
            dst.JobReleaseAction = map.MapFromDicNullable<JobReleaseAction?>(src, JobAttribute.JobReleaseAction);
            dst.JobAuthorizationUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.JobAuthorizationUri);
            dst.JobImpressionsEstimated = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressionsEstimated);
            dst.ChargeInfoMessage = map.MapFromDicNullable<string?>(src, JobAttribute.ChargeInfoMessage);
            dst.ProofCopies = map.MapFromDicNullable<int?>(src, JobAttribute.ProofCopies);
            if (src.TryGetValue(JobAttribute.ProofPrint, out var proofPrint) && proofPrint.GroupBegCollection().Any())
                dst.ProofPrint = map.Map<ProofPrint>(proofPrint.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(JobAttribute.JobStorage, out var jobStorage) && jobStorage.GroupBegCollection().Any())
                dst.JobStorage = map.Map<JobStorage>(jobStorage.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(JobAttribute.CoverSheetInfo, out var coverSheetInfo) && coverSheetInfo.GroupBegCollection().Any())
                dst.CoverSheetInfo = map.Map<CoverSheetInfo>(coverSheetInfo.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(JobAttribute.DestinationUris, out var destinationUris) && destinationUris.GroupBegCollection().Any())
                dst.DestinationUris = destinationUris.GroupBegCollection().Select(x => map.Map<DestinationUri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(JobAttribute.OutputAttributes, out var outputAttributes) && outputAttributes.GroupBegCollection().Any())
                dst.OutputAttributes = map.Map<OutputAttributes>(outputAttributes.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<CreateJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobMandatoryAttributes != null)
                dst.AddRange(src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobMandatoryAttributes, x)));
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(JobAttribute.DocumentFormatDetails));
            if (src.JobName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobName, src.JobName));
            if (src.IppAttributeFidelity.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, src.IppAttributeFidelity.Value));
            if (src.JobImpressions.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobImpressions, src.JobImpressions.Value));
            if (src.JobMediaSheets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheets, src.JobMediaSheets.Value));
            if (src.JobKOctets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobKOctets, src.JobKOctets.Value));
            if (src.JobPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.JobPassword, src.JobPassword));
            if (src.JobPasswordEncryption != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobPasswordEncryption, map.Map<string>(src.JobPasswordEncryption.Value)));
            if (src.JobReleaseAction != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobReleaseAction, map.Map<string>(src.JobReleaseAction.Value)));
            if (src.JobAuthorizationUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.JobAuthorizationUri, src.JobAuthorizationUri.ToString()));
            if (src.JobImpressionsEstimated.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobImpressionsEstimated, src.JobImpressionsEstimated.Value));
            if (src.ChargeInfoMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.ChargeInfoMessage, src.ChargeInfoMessage));
            if (src.ProofCopies.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.ProofCopies, src.ProofCopies.Value));
            if (src.ProofPrint != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.ProofPrint).ToBegCollection(JobAttribute.ProofPrint));
            if (src.JobStorage != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobStorage).ToBegCollection(JobAttribute.JobStorage));
            if (src.CoverSheetInfo != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverSheetInfo).ToBegCollection(JobAttribute.CoverSheetInfo));
            if (src.DestinationUris != null)
                dst.AddRange(src.DestinationUris.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.DestinationUris)));
            if (src.OutputAttributes != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.OutputAttributes).ToBegCollection(JobAttribute.OutputAttributes));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.Message = map.MapFromDicNullable<string?>(src, JobAttribute.Message);
            return dst;
        });

        mapper.CreateMap<CancelJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.Message != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.Message, src.Message));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.JobIds);
            dst.Message = map.MapFromDicNullable<string?>(src, JobAttribute.Message);
            return dst;
        });

        mapper.CreateMap<CancelJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobIds != null)
                dst.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobIds, x)));
            if (src.Message != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.Message, src.Message));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelMyJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelMyJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.JobIds);
            dst.Message = map.MapFromDicNullable<string?>(src, JobAttribute.Message);
            return dst;
        });

        mapper.CreateMap<CancelMyJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobIds != null)
                dst.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobIds, x)));
            if (src.Message != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.Message, src.Message));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ResubmitJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ResubmitJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            if (src.TryGetValue(JobAttribute.DocumentFormatDetails, out var documentFormatDetails) && documentFormatDetails.GroupBegCollection().Any())
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.JobMandatoryAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobMandatoryAttributes);
            dst.IppAttributeFidelity = map.MapFromDicNullable<bool?>(src, JobAttribute.IppAttributeFidelity);
            return dst;
        });

        mapper.CreateMap<ResubmitJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(JobAttribute.DocumentFormatDetails));
            if (src.IppAttributeFidelity.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, src.IppAttributeFidelity.Value));
            if (src.JobMandatoryAttributes != null)
                dst.AddRange(src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobMandatoryAttributes, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CloseJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CloseJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<CloseJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SetJobAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SetJobAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<SetJobAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SetPrinterAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SetPrinterAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<SetPrinterAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });


        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CUPSGetPrintersOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CUPSGetPrintersOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.FirstPrinterName = map.MapFromDicNullable<string?>(src, JobAttribute.FirstPrinterName);
            dst.Limit = map.MapFromDicNullable<int?>(src, JobAttribute.Limit);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, JobAttribute.PrinterId);
            dst.PrinterLocation = map.MapFromDicNullable<string?>(src, JobAttribute.PrinterLocation);
            dst.PrinterType = map.MapFromDicNullable<PrinterType?>(src, JobAttribute.PrinterType);
            dst.PrinterTypeMask = map.MapFromDicNullable<PrinterType?>(src, JobAttribute.PrinterTypeMask);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<CUPSGetPrintersOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.FirstPrinterName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.FirstPrinterName, src.FirstPrinterName));
            if (src.Limit != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.Limit, src.Limit.Value));
            if (src.PrinterId != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.PrinterId, src.PrinterId.Value));
            if (src.PrinterLocation != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.PrinterLocation, src.PrinterLocation));
            if (src.PrinterType != null)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.PrinterType, (int)src.PrinterType.Value));
            if (src.PrinterTypeMask != null)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.PrinterTypeMask, (int)src.PrinterTypeMask.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedAttribute)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetJobAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetJobAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            var requestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            if (requestedAttributes?.Any() ?? false)
                dst.RequestedAttributes = requestedAttributes;
            return dst;
        });

        mapper.CreateMap<GetJobAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedAttribute)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetDocumentAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetDocumentAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentNumber) ?? 0;
            var requestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            if (requestedAttributes?.Any() ?? false)
                dst.RequestedAttributes = requestedAttributes;
            return dst;
        });

        mapper.CreateMap<GetDocumentAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedAttribute)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SetDocumentAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SetDocumentAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentNumber) ?? 0;
            return dst;
        });

        mapper.CreateMap<SetDocumentAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetDocumentsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetDocumentsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.Limit = map.MapFromDicNullable<int?>(src, JobAttribute.Limit);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetDocumentsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.Limit.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.Limit, src.Limit.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentNumber) ?? 0;
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentMessage);
            return dst;
        });

        mapper.CreateMap<CancelDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentMessage, src.DocumentMessage));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.Limit = map.MapFromDicNullable<int?>(src, JobAttribute.Limit);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.JobIds);
            var requestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            if (requestedAttributes?.Any() ?? false)
                dst.RequestedAttributes = requestedAttributes;
            var whichJobs = map.MapFromDicNullable<string?>(src, JobAttribute.WhichJobs);
            if (whichJobs != null)
                dst.WhichJobs = map.Map<string, WhichJobs>(whichJobs);
            var myJobs = map.MapFromDicNullable<bool?>(src, JobAttribute.MyJobs);
            if (myJobs != null)
                dst.MyJobs = myJobs.Value;
            return dst;
        });

        mapper.CreateMap<GetJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.Limit != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.Limit, src.Limit.Value));
            if (src.JobIds != null)
                dst.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobIds, x)));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedAttribute)));
            if (src.WhichJobs != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.WhichJobs, map.Map<string>(src.WhichJobs.Value)));
            if (src.MyJobs != null)
                dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.MyJobs, src.MyJobs.Value));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetPrinterAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetPrinterAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentFormat);
            var requestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            if (requestedAttributes?.Any() ?? false)
                dst.RequestedAttributes = requestedAttributes;
            return dst;
        });

        mapper.CreateMap<GetPrinterAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormat, src.DocumentFormat));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedAttribute)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, HoldJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new HoldJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, CancelJobOperationAttributes>(src, dst);
            dst.JobHoldUntil = map.MapFromDicNullable<JobHoldUntil?>(src, JobAttribute.JobHoldUntil);
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.JobHoldUntilTime);
            return dst;
        });

        mapper.CreateMap<HoldJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CancelJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobHoldUntil != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobHoldUntil, map.Map<string>(src.JobHoldUntil.Value)));
            if (src.JobHoldUntilTime != null)
                dst.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobHoldUntilTime, src.JobHoldUntilTime.Value));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PrintJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>(src, dst);
            dst.DocumentName = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, JobAttribute.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentNaturalLanguage);
            dst.DocumentCharset = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentCharset);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentMessage);
            return dst;
        });

        mapper.CreateMap<PrintJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CreateJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.DocumentName, src.DocumentName));
            if (src.Compression != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Compression, map.Map<string>(src.Compression.Value)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormat, src.DocumentFormat));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage));
            if (src.DocumentCharset != null)
                dst.Add(new IppAttribute(Tag.Charset, JobAttribute.DocumentCharset, src.DocumentCharset));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentMessage, src.DocumentMessage));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintUriOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PrintUriOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, PrintJobOperationAttributes>(src, dst);
            dst.DocumentUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.DocumentUri);
            if (src.TryGetValue(JobAttribute.DocumentAccess, out var documentAccess))
                dst.DocumentAccess = map.Map<DocumentAccess>(documentAccess.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<PrintUriOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<PrintJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, src.DocumentUri.ToString()));
            if (src.DocumentAccess != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentAccess).ToBegCollection(JobAttribute.DocumentAccess));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, RestartJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new RestartJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, CancelJobOperationAttributes>(src, dst);
            dst.JobHoldUntil = map.MapFromDicNullable<JobHoldUntil?>(src, JobAttribute.JobHoldUntil);
            return dst;
        });

        mapper.CreateMap<RestartJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CancelJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobHoldUntil != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobHoldUntil, map.Map<string>(src.JobHoldUntil.Value)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SendDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SendDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            if (src.TryGetValue(JobAttribute.DocumentFormatDetails, out var documentFormatDetails))
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.DocumentName = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, JobAttribute.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentNaturalLanguage);
            dst.DocumentCharset = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentCharset);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentMessage);
            var lastDocument = map.MapFromDicNullable<bool?>(src, JobAttribute.LastDocument);
            if (lastDocument.HasValue)
                dst.LastDocument = lastDocument.Value;
            return dst;
        });

        mapper.CreateMap<SendDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(JobAttribute.DocumentFormatDetails));
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.DocumentName, src.DocumentName));
            if (src.Compression != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Compression, map.Map<string>(src.Compression.Value)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormat, src.DocumentFormat));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage));
            if (src.DocumentCharset != null)
                dst.Add(new IppAttribute(Tag.Charset, JobAttribute.DocumentCharset, src.DocumentCharset));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentMessage, src.DocumentMessage));
            dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, src.LastDocument));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SendUriOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SendUriOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SendDocumentOperationAttributes>(src, dst);
            dst.DocumentUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.DocumentUri);
            if (src.TryGetValue(JobAttribute.DocumentAccess, out var documentAccess))
                dst.DocumentAccess = map.Map<DocumentAccess>(documentAccess.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<SendUriOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SendDocumentOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, src.DocumentUri.ToString()));
            if (src.DocumentAccess != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentAccess).ToBegCollection(JobAttribute.DocumentAccess));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PausePrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PausePrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<PausePrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, IdentifyPrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new IdentifyPrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.IdentifyActions = map.MapFromDicSetNullable<IdentifyAction[]?>(src, JobAttribute.IdentifyActions);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.JobId = map.MapFromDicNullable<int?>(src, JobAttribute.JobId);
            return dst;
        });

        mapper.CreateMap<IdentifyPrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.IdentifyActions != null)
                dst.AddRange(src.IdentifyActions.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.IdentifyActions, map.Map<string>(x))));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.JobId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobId, src.JobId.Value));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AcknowledgeDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AcknowledgeDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentNumber) ?? 0;
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.FetchStatusCode = map.MapFromDicNullable<IppStatusCode?>(src, JobAttribute.FetchStatusCode);
            dst.FetchStatusMessage = map.MapFromDicNullable<string?>(src, JobAttribute.FetchStatusMessage);
            return dst;
        });

        mapper.CreateMap<AcknowledgeDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.FetchStatusCode.HasValue)
                dst.Add(new IppAttribute(Tag.Enum, JobAttribute.FetchStatusCode, (int)src.FetchStatusCode.Value));
            if (src.FetchStatusMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.FetchStatusMessage, src.FetchStatusMessage));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AcknowledgeJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AcknowledgeJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.OutputDeviceJobStates = map.MapFromDicSetNullable<JobState[]?>(src, JobAttribute.OutputDeviceJobStates);
            return dst;
        });

        mapper.CreateMap<AcknowledgeJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.OutputDeviceJobStates != null)
                dst.AddRange(src.OutputDeviceJobStates.Select(x => new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, FetchDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new FetchDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentNumber) ?? 0;
            dst.CompressionAccepted = map.MapFromDicSetNullable<Compression[]?>(src, JobAttribute.CompressionAccepted);
            dst.DocumentFormatAccepted = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.DocumentFormatAccepted);
            return dst;
        });

        mapper.CreateMap<FetchDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber));
            if (src.CompressionAccepted != null)
                dst.AddRange(src.CompressionAccepted.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.CompressionAccepted, map.Map<string>(x))));
            if (src.DocumentFormatAccepted != null)
                dst.AddRange(src.DocumentFormatAccepted.Select(x => new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormatAccepted, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetOutputDeviceAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetOutputDeviceAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            return dst;
        });

        mapper.CreateMap<GetOutputDeviceAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, x)));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, UpdateActiveJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new UpdateActiveJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.OutputDeviceJobStates = map.MapFromDicSetNullable<JobState[]?>(src, JobAttribute.OutputDeviceJobStates);
            return dst;
        });

        mapper.CreateMap<UpdateActiveJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.OutputDeviceJobStates != null)
                dst.AddRange(src.OutputDeviceJobStates.Select(x => new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SystemOperationAttributes();
            dst.AttributesCharset = map.MapFromDicNullable<string?>(src, JobAttribute.AttributesCharset) ?? "utf-8";
            dst.AttributesNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.AttributesNaturalLanguage) ?? "en";
            dst.RequestingUserName = map.MapFromDicNullable<string?>(src, JobAttribute.RequestingUserName);
            dst.RequestingUserUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.RequestingUserUri);
            dst.PrinterUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.PrinterUri);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, JobAttribute.PrinterId);
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.JobHoldUntilTime);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.NotifyPrinterIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.NotifyPrinterIds);
            dst.NotifyResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.NotifyResourceId);
            dst.NotifySystemUpTime = map.MapFromDicNullable<int?>(src, SystemAttribute.NotifySystemUpTime);
            dst.NotifySystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.NotifySystemUri);
            dst.RestartGetInterval = map.MapFromDicNullable<int?>(src, SystemAttribute.RestartGetInterval);
            dst.WhichPrinters = map.MapFromDicNullable<string?>(src, SystemAttribute.WhichPrinters);
            return dst;
        });

        mapper.CreateMap<SystemOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.SystemUri != null)
                dst.Add(new IppAttribute(Tag.Uri, SystemAttribute.SystemUri, src.SystemUri.ToString()));
            if (src.PrinterId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.PrinterId, src.PrinterId.Value));
            if (src.NotifyPrinterIds != null)
                dst.AddRange(src.NotifyPrinterIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.NotifyPrinterIds, x)));
            if (src.NotifyResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.NotifyResourceId, src.NotifyResourceId.Value));
            if (src.NotifySystemUpTime.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.NotifySystemUpTime, src.NotifySystemUpTime.Value));
            if (src.NotifySystemUri != null)
                dst.Add(new IppAttribute(Tag.Uri, SystemAttribute.NotifySystemUri, src.NotifySystemUri.ToString()));
            if (src.RestartGetInterval.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.RestartGetInterval, src.RestartGetInterval.Value));
            if (src.WhichPrinters != null)
                dst.Add(new IppAttribute(Tag.Keyword, SystemAttribute.WhichPrinters, src.WhichPrinters));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelResourceOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelResourceOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId);
            return dst;
        });

        mapper.CreateMap<CancelResourceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CreateResourceOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CreateResourceOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceFormat = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceFormat);
            dst.ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceNaturalLanguage);
            dst.ResourceType = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceType);
            dst.ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName);
            dst.ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo);
            return dst;
        });

        mapper.CreateMap<CreateResourceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormat, src.ResourceFormat));
            if (src.ResourceNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, SystemAttribute.ResourceNaturalLanguage, src.ResourceNaturalLanguage));
            if (src.ResourceType != null)
                dst.Add(new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType));
            if (src.ResourceName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName));
            if (src.ResourceInfo != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, InstallResourceOperationAttributes>((src, dst, map) =>
        {
            dst ??= new InstallResourceOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId);
            return dst;
        });

        mapper.CreateMap<InstallResourceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SendResourceDataOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SendResourceDataOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId);
            dst.ResourceKOctets = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceKOctets);
            dst.ResourceSignature = map.MapFromDicSetNullable<byte[][]?>(src, SystemAttribute.ResourceSignature);
            return dst;
        });

        mapper.CreateMap<SendResourceDataOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            if (src.ResourceKOctets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceKOctets, src.ResourceKOctets.Value));
            if (src.ResourceSignature != null)
                dst.AddRange(src.ResourceSignature.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.ResourceSignature, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SetResourceAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SetResourceAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId);
            dst.ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName);
            dst.ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo);
            dst.ResourceNaturalLanguage = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceNaturalLanguage);
            dst.ResourcePatches = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourcePatches);
            dst.ResourceStringVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceStringVersion);
            dst.ResourceType = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceType);
            dst.ResourceVersion = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceVersion);
            return dst;
        });

        mapper.CreateMap<SetResourceAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            if (src.ResourceName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName));
            if (src.ResourceInfo != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo));
            if (src.ResourceNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, SystemAttribute.ResourceNaturalLanguage, src.ResourceNaturalLanguage));
            if (src.ResourcePatches != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourcePatches, src.ResourcePatches));
            if (src.ResourceStringVersion != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceStringVersion, src.ResourceStringVersion));
            if (src.ResourceType != null)
                dst.Add(new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType));
            if (src.ResourceVersion != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceVersion, src.ResourceVersion));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AllocatePrinterResourcesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AllocatePrinterResourcesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, JobAttribute.PrinterId);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            return dst;
        });

        mapper.CreateMap<AllocatePrinterResourcesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PrinterId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.PrinterId, src.PrinterId.Value));
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CreatePrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CreatePrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            dst.PrinterServiceType = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, PrinterAttribute.PrinterServiceType);
            if(src.ContainsKey(PrinterAttribute.PrinterXriRequested))
                dst.PrinterXriRequested = src[PrinterAttribute.PrinterXriRequested].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            return dst;
        });

        mapper.CreateMap<CreatePrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            if (src.PrinterServiceType != null)
                dst.AddRange(src.PrinterServiceType.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, map.Map<string>(x))));
            if (src.PrinterXriRequested != null)
                dst.AddRange(src.PrinterXriRequested.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.PrinterXriRequested)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetSystemAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetSystemAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetSystemAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetResourcesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetResourcesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetResourcesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetResourceAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetResourceAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetResourceAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetSystemSupportedValuesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetSystemSupportedValuesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetSystemSupportedValuesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, x)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, RegisterOutputDeviceOperationAttributes>((src, dst, map) =>
        {
            dst ??= new RegisterOutputDeviceOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            dst.OutputDeviceX509Certificate = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.OutputDeviceX509Certificate);
            dst.OutputDeviceX509Request = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.OutputDeviceX509Request);
            dst.PrinterServiceType = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, PrinterAttribute.PrinterServiceType);
            if(src.ContainsKey(PrinterAttribute.PrinterXriRequested))
                dst.PrinterXriRequested = src[PrinterAttribute.PrinterXriRequested].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            return dst;
        });

        mapper.CreateMap<RegisterOutputDeviceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.OutputDeviceX509Certificate != null)
                dst.AddRange(src.OutputDeviceX509Certificate.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.OutputDeviceX509Certificate, x)));
            if (src.OutputDeviceX509Request != null)
                dst.AddRange(src.OutputDeviceX509Request.Select(x => new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.OutputDeviceX509Request, x)));
            if (src.PrinterServiceType != null)
                dst.AddRange(src.PrinterServiceType.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, map.Map<string>(x))));
            if (src.PrinterXriRequested != null)
                dst.AddRange(src.PrinterXriRequested.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.PrinterXriRequested)));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetNextDocumentDataOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetNextDocumentDataOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentDataWait = map.MapFromDicNullable<bool?>(src, JobAttribute.DocumentDataWait);
            return dst;
        });

        mapper.CreateMap<GetNextDocumentDataOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentDataWait.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.DocumentDataWait, src.DocumentDataWait.Value));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetUserPrinterAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetUserPrinterAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, GetPrinterAttributesOperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetUserPrinterAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<GetPrinterAttributesOperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AddDocumentImagesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AddDocumentImagesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            if (src.ContainsKey(JobAttribute.InputAttributes))
                dst.InputAttributes = map.Map<DocumentTemplateAttributes>(src[JobAttribute.InputAttributes].FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<AddDocumentImagesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.InputAttributes != null)
                dst.AddRange(map.Map<List<IppAttribute>>(src.InputAttributes).ToBegCollection(JobAttribute.InputAttributes));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PurgeJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PurgeJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<PurgeJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ReleaseJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ReleaseJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<ReleaseJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ResumePrinterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ResumePrinterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            return dst;
        });

        mapper.CreateMap<ResumePrinterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ValidateJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ValidateJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobName = map.MapFromDicNullable<string?>(src, JobAttribute.JobName);
            dst.JobMediaSheets = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheets);
            dst.JobKOctets = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctets);
            dst.IppAttributeFidelity = map.MapFromDicNullable<bool?>(src, JobAttribute.IppAttributeFidelity);
            dst.JobImpressions = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressions);
            dst.DocumentName = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, JobAttribute.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentNaturalLanguage);
            dst.JobPassword = map.MapFromDicNullable<string?>(src, JobAttribute.JobPassword);
            dst.JobPasswordEncryption = map.MapFromDicNullable<JobPasswordEncryption?>(src, JobAttribute.JobPasswordEncryption);
            dst.JobReleaseAction = map.MapFromDicNullable<JobReleaseAction?>(src, JobAttribute.JobReleaseAction);
            dst.JobAuthorizationUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.JobAuthorizationUri);
            dst.JobImpressionsEstimated = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressionsEstimated);
            dst.ChargeInfoMessage = map.MapFromDicNullable<string?>(src, JobAttribute.ChargeInfoMessage);
            dst.ProofCopies = map.MapFromDicNullable<int?>(src, JobAttribute.ProofCopies);
            return dst;
        });

        mapper.CreateMap<ValidateJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobName, src.JobName));
            if (src.IppAttributeFidelity.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, src.IppAttributeFidelity.Value));
            if (src.JobImpressions.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobImpressions, src.JobImpressions.Value));
            if (src.JobMediaSheets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheets, src.JobMediaSheets.Value));
            if (src.JobKOctets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobKOctets, src.JobKOctets.Value));
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.DocumentName, src.DocumentName));
            if (src.Compression != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Compression, map.Map<string>(src.Compression.Value)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormat, src.DocumentFormat));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage));
            if (src.DocumentCharset != null)
                dst.Add(new IppAttribute(Tag.Charset, JobAttribute.DocumentCharset, src.DocumentCharset));
            if (src.JobPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.JobPassword, src.JobPassword));
            if (src.JobPasswordEncryption != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobPasswordEncryption, map.Map<string>(src.JobPasswordEncryption.Value)));
            if (src.JobReleaseAction != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobReleaseAction, map.Map<string>(src.JobReleaseAction.Value)));
            if (src.JobAuthorizationUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.JobAuthorizationUri, src.JobAuthorizationUri.ToString()));
            if (src.JobImpressionsEstimated.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobImpressionsEstimated, src.JobImpressionsEstimated.Value));
            if (src.ChargeInfoMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.ChargeInfoMessage, src.ChargeInfoMessage));
            if (src.ProofCopies.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.ProofCopies, src.ProofCopies.Value));
            return dst;
        });
    }
}
