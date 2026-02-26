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

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.Limit = map.MapFromDicNullable<int?>(src, JobAttribute.Limit);
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
            return dst;
        });

        mapper.CreateMap<HoldJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CancelJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobHoldUntil != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobHoldUntil, map.Map<string>(src.JobHoldUntil.Value)));
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
            dst.DocumentName = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentName);
            dst.Compression = map.MapFromDicNullable<Compression?>(src, JobAttribute.Compression);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentFormat);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, JobAttribute.DocumentNaturalLanguage);
            var lastDocument = map.MapFromDicNullable<bool?>(src, JobAttribute.LastDocument);
            if (lastDocument.HasValue)
                dst.LastDocument = lastDocument.Value;
            return dst;
        });

        mapper.CreateMap<SendDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.DocumentName, src.DocumentName));
            if (src.Compression != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.Compression, map.Map<string>(src.Compression.Value)));
            if (src.DocumentFormat != null)
                dst.Add(new IppAttribute(Tag.MimeMediaType, JobAttribute.DocumentFormat, src.DocumentFormat));
            if (src.DocumentNaturalLanguage != null)
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage));
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
                dst.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage));
            return dst;
        });
    }
}
