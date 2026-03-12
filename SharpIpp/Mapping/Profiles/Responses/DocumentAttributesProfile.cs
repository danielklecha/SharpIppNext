using SharpIpp.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class DocumentAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentAttributes>((
            src,
            dst,
            map) =>
        {
            dst ??= new DocumentAttributes();
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentNumber);
            dst.DocumentState = map.MapFromDicNullable<DocumentState?>(src, DocumentAttribute.DocumentState);
            dst.DocumentStateReasons = map.MapFromDicSetNullable<DocumentStateReason[]?>(src, DocumentAttribute.DocumentStateReasons);
            dst.DocumentStateMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentStateMessage);
            dst.AttributesCharset = map.MapFromDicNullable<string?>(src, DocumentAttribute.AttributesCharset);
            dst.AttributesNaturalLanguage = map.MapFromDicNullable<string?>(src, DocumentAttribute.AttributesNaturalLanguage);
            dst.CurrentPageOrder = map.MapFromDicNullable<CurrentPageOrder?>(src, DocumentAttribute.CurrentPageOrder);
            dst.DateTimeAtCompleted = map.MapFromDicNullable<DateTimeOffset?>(src, DocumentAttribute.DateTimeAtCompleted);
            dst.DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, DocumentAttribute.DateTimeAtCreation);
            dst.DateTimeAtProcessing = map.MapFromDicNullable<DateTimeOffset?>(src, DocumentAttribute.DateTimeAtProcessing);
            dst.DetailedStatusMessages = map.MapFromDicSetNullable<string[]?>(src, DocumentAttribute.DetailedStatusMessages);
            dst.DocumentAccessErrors = map.MapFromDicSetNullable<string[]?>(src, DocumentAttribute.DocumentAccessErrors);
            dst.DocumentCharset = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentCharset);
            dst.DocumentFormat = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentFormat);
            dst.DocumentFormatDetected = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentFormatDetected);
            dst.DocumentJobId = map.MapFromDicNullable<int?>(src, DocumentAttribute.DocumentJobId);
            dst.DocumentJobUri = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentJobUri);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentMessage);
            dst.DocumentName = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentName);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentNaturalLanguage);
            dst.DocumentPrinterUri = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentPrinterUri);
            dst.DocumentUri = map.MapFromDicNullable<string?>(src, DocumentAttribute.DocumentUri);
            dst.Impressions = map.MapFromDicNullable<int?>(src, DocumentAttribute.Impressions);
            dst.ImpressionsCompleted = map.MapFromDicNullable<int?>(src, DocumentAttribute.ImpressionsCompleted);
            dst.KOctets = map.MapFromDicNullable<int?>(src, DocumentAttribute.KOctets);
            dst.KOctetsProcessed = map.MapFromDicNullable<int?>(src, DocumentAttribute.KOctetsProcessed);
            dst.LastDocument = map.MapFromDicNullable<bool?>(src, DocumentAttribute.LastDocument);
            dst.MediaSheets = map.MapFromDicNullable<int?>(src, DocumentAttribute.MediaSheets);
            dst.MediaSheetsCompleted = map.MapFromDicNullable<int?>(src, DocumentAttribute.MediaSheetsCompleted);
            dst.MoreInfo = map.MapFromDicNullable<string?>(src, DocumentAttribute.MoreInfo);
            dst.OutputDeviceAssigned = map.MapFromDicNullable<string?>(src, DocumentAttribute.OutputDeviceAssigned);
            dst.PrinterUpTime = map.MapFromDicNullable<int?>(src, DocumentAttribute.PrinterUpTime);
            dst.TimeAtCompleted = map.MapFromDicNullable<int?>(src, DocumentAttribute.TimeAtCompleted);
            dst.TimeAtCreation = map.MapFromDicNullable<int?>(src, DocumentAttribute.TimeAtCreation);
            dst.TimeAtProcessing = map.MapFromDicNullable<int?>(src, DocumentAttribute.TimeAtProcessing);
            dst.PrintContentOptimize = map.MapFromDicNullable<PrintContentOptimize?>(src, DocumentAttribute.PrintContentOptimize);
            dst.PrintContentOptimizeSupported = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, DocumentAttribute.PrintContentOptimizeSupported);
            dst.PrinterStateReasons = map.MapFromDicSetNullable<PrinterStateReason[]?>(src, DocumentAttribute.PrinterStateReasons);
            return dst;
        });

        mapper.CreateMap<DocumentAttributes, IDictionary<string, IppAttribute[]>>((
            src,
            dst,
            map) =>
        {
            dst ??= new Dictionary<string, IppAttribute[]>();
            if (src.DocumentNumber != null)
                dst.Add(DocumentAttribute.DocumentNumber, [new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.DocumentNumber.Value)]);
            if (src.DocumentState != null)
                dst.Add(DocumentAttribute.DocumentState, [new IppAttribute(Tag.Enum, DocumentAttribute.DocumentState, (int)src.DocumentState.Value)]);
            if (src.DocumentStateReasons != null)
                dst.Add(DocumentAttribute.DocumentStateReasons, src.DocumentStateReasons.Select(x => new IppAttribute(Tag.Keyword, DocumentAttribute.DocumentStateReasons, map.Map<string>(x))).ToArray());
            if (src.DocumentStateMessage != null)
                dst.Add(DocumentAttribute.DocumentStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentStateMessage, src.DocumentStateMessage)]);
            if (src.AttributesCharset != null)
                dst.Add(DocumentAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, DocumentAttribute.AttributesCharset, src.AttributesCharset)]);
            if (src.AttributesNaturalLanguage != null)
                dst.Add(DocumentAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, DocumentAttribute.AttributesNaturalLanguage, src.AttributesNaturalLanguage)]);
            if (src.CurrentPageOrder != null)
                dst.Add(DocumentAttribute.CurrentPageOrder, [new IppAttribute(Tag.Keyword, DocumentAttribute.CurrentPageOrder, map.Map<string>(src.CurrentPageOrder.Value))]);
            if (src.DateTimeAtCompleted != null)
                dst.Add(DocumentAttribute.DateTimeAtCompleted, [new IppAttribute(Tag.DateTime, DocumentAttribute.DateTimeAtCompleted, src.DateTimeAtCompleted.Value)]);
            if (src.DateTimeAtCreation != null)
                dst.Add(DocumentAttribute.DateTimeAtCreation, [new IppAttribute(Tag.DateTime, DocumentAttribute.DateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtProcessing != null)
                dst.Add(DocumentAttribute.DateTimeAtProcessing, [new IppAttribute(Tag.DateTime, DocumentAttribute.DateTimeAtProcessing, src.DateTimeAtProcessing.Value)]);
            if (src.DetailedStatusMessages != null)
                dst.Add(DocumentAttribute.DetailedStatusMessages, src.DetailedStatusMessages.Select(x => new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DetailedStatusMessages, x)).ToArray());
            if (src.DocumentAccessErrors != null)
                dst.Add(DocumentAttribute.DocumentAccessErrors, src.DocumentAccessErrors.Select(x => new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentAccessErrors, x)).ToArray());
            if (src.DocumentCharset != null)
                dst.Add(DocumentAttribute.DocumentCharset, [new IppAttribute(Tag.Charset, DocumentAttribute.DocumentCharset, src.DocumentCharset)]);
            if (src.DocumentFormat != null)
                dst.Add(DocumentAttribute.DocumentFormat, [new IppAttribute(Tag.MimeMediaType, DocumentAttribute.DocumentFormat, src.DocumentFormat)]);
            if (src.DocumentFormatDetected != null)
                dst.Add(DocumentAttribute.DocumentFormatDetected, [new IppAttribute(Tag.MimeMediaType, DocumentAttribute.DocumentFormatDetected, src.DocumentFormatDetected)]);
            if (src.DocumentJobId != null)
                dst.Add(DocumentAttribute.DocumentJobId, [new IppAttribute(Tag.Integer, DocumentAttribute.DocumentJobId, src.DocumentJobId.Value)]);
            if (src.DocumentJobUri != null)
                dst.Add(DocumentAttribute.DocumentJobUri, [new IppAttribute(Tag.Uri, DocumentAttribute.DocumentJobUri, src.DocumentJobUri)]);
            if (src.DocumentMessage != null)
                dst.Add(DocumentAttribute.DocumentMessage, [new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentMessage, src.DocumentMessage)]);
            if (src.DocumentName != null)
                dst.Add(DocumentAttribute.DocumentName, [new IppAttribute(Tag.NameWithoutLanguage, DocumentAttribute.DocumentName, src.DocumentName)]);
            if (src.DocumentNaturalLanguage != null)
                dst.Add(DocumentAttribute.DocumentNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, DocumentAttribute.DocumentNaturalLanguage, src.DocumentNaturalLanguage)]);
            if (src.DocumentPrinterUri != null)
                dst.Add(DocumentAttribute.DocumentPrinterUri, [new IppAttribute(Tag.Uri, DocumentAttribute.DocumentPrinterUri, src.DocumentPrinterUri)]);
            if (src.DocumentUri != null)
                dst.Add(DocumentAttribute.DocumentUri, [new IppAttribute(Tag.Uri, DocumentAttribute.DocumentUri, src.DocumentUri)]);
            if (src.Impressions != null)
                dst.Add(DocumentAttribute.Impressions, [new IppAttribute(Tag.Integer, DocumentAttribute.Impressions, src.Impressions.Value)]);
            if (src.ImpressionsCompleted != null)
                dst.Add(DocumentAttribute.ImpressionsCompleted, [new IppAttribute(Tag.Integer, DocumentAttribute.ImpressionsCompleted, src.ImpressionsCompleted.Value)]);
            if (src.KOctets != null)
                dst.Add(DocumentAttribute.KOctets, [new IppAttribute(Tag.Integer, DocumentAttribute.KOctets, src.KOctets.Value)]);
            if (src.KOctetsProcessed != null)
                dst.Add(DocumentAttribute.KOctetsProcessed, [new IppAttribute(Tag.Integer, DocumentAttribute.KOctetsProcessed, src.KOctetsProcessed.Value)]);
            if (src.LastDocument != null)
                dst.Add(DocumentAttribute.LastDocument, [new IppAttribute(Tag.Boolean, DocumentAttribute.LastDocument, src.LastDocument.Value)]);
            if (src.MediaSheets != null)
                dst.Add(DocumentAttribute.MediaSheets, [new IppAttribute(Tag.Integer, DocumentAttribute.MediaSheets, src.MediaSheets.Value)]);
            if (src.MediaSheetsCompleted != null)
                dst.Add(DocumentAttribute.MediaSheetsCompleted, [new IppAttribute(Tag.Integer, DocumentAttribute.MediaSheetsCompleted, src.MediaSheetsCompleted.Value)]);
            if (src.MoreInfo != null)
                dst.Add(DocumentAttribute.MoreInfo, [new IppAttribute(Tag.Uri, DocumentAttribute.MoreInfo, src.MoreInfo)]);
            if (src.OutputDeviceAssigned != null)
                dst.Add(DocumentAttribute.OutputDeviceAssigned, [new IppAttribute(Tag.NameWithoutLanguage, DocumentAttribute.OutputDeviceAssigned, src.OutputDeviceAssigned)]);
            if (src.PrinterUpTime != null)
                dst.Add(DocumentAttribute.PrinterUpTime, [new IppAttribute(Tag.Integer, DocumentAttribute.PrinterUpTime, src.PrinterUpTime.Value)]);
            if (src.TimeAtCompleted != null)
                dst.Add(DocumentAttribute.TimeAtCompleted, [new IppAttribute(Tag.Integer, DocumentAttribute.TimeAtCompleted, src.TimeAtCompleted.Value)]);
            if (src.TimeAtCreation != null)
                dst.Add(DocumentAttribute.TimeAtCreation, [new IppAttribute(Tag.Integer, DocumentAttribute.TimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtProcessing != null)
                dst.Add(DocumentAttribute.TimeAtProcessing, [new IppAttribute(Tag.Integer, DocumentAttribute.TimeAtProcessing, src.TimeAtProcessing.Value)]);
            if (src.PrintContentOptimize != null)
                dst.Add(DocumentAttribute.PrintContentOptimize, [new IppAttribute(Tag.Keyword, DocumentAttribute.PrintContentOptimize, map.Map<string>(src.PrintContentOptimize.Value))]);
            if (src.PrintContentOptimizeSupported != null)
                dst.Add(DocumentAttribute.PrintContentOptimizeSupported, src.PrintContentOptimizeSupported.Select(x => new IppAttribute(Tag.Keyword, DocumentAttribute.PrintContentOptimizeSupported, map.Map<string>(x))).ToArray());
            if (src.PrinterStateReasons != null)
                dst.Add(DocumentAttribute.PrinterStateReasons, src.PrinterStateReasons.Select(x => new IppAttribute(Tag.Keyword, DocumentAttribute.PrinterStateReasons, map.Map<string>(x))).ToArray());
            return dst;
        });
    }
}
