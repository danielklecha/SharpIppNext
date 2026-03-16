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
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo) && clientInfo.GroupBegCollection().Any())
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(JobAttribute.DocumentFormatDetails, out var documentFormatDetails) && documentFormatDetails.GroupBegCollection().Any())
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.JobName = map.MapFromDicNullable<string?>(src, JobAttribute.JobName);
            dst.JobMediaSheets = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheets);
            dst.JobKOctets = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctets);
            dst.IppAttributeFidelity = map.MapFromDicNullable<bool?>(src, JobAttribute.IppAttributeFidelity);
            dst.JobImpressions = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressions);
            return dst;
        });

        mapper.CreateMap<CreateJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobMandatoryAttributes != null)
                dst.AddRange(src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobMandatoryAttributes, x)));
            if (src.ClientInfo != null)
                dst.AddRange(src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.ClientInfo)));
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
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage)); if (src.DocumentCharset != null) dst.Add(new IppAttribute(Tag.Charset, JobAttribute.DocumentCharset, src.DocumentCharset));
            if (src.DocumentMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentMessage, src.DocumentMessage));
            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintUriOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PrintUriOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, PrintJobOperationAttributes>(src, dst);
            dst.DocumentUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.DocumentUri);
            return dst;
        });

        mapper.CreateMap<PrintUriOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<PrintJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, src.DocumentUri.ToString()));
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
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo))
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
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
            if (src.ClientInfo != null)
                dst.AddRange(src.ClientInfo.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.ClientInfo)));
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
            return dst;
        });

        mapper.CreateMap<SendUriOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SendDocumentOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, src.DocumentUri.ToString()));
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
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage)); if (src.DocumentCharset != null) dst.Add(new IppAttribute(Tag.Charset, JobAttribute.DocumentCharset, src.DocumentCharset));
            return dst;
        });
    }
}
