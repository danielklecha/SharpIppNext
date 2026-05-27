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
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DocumentAttributes>();

            dst ??= new DocumentAttributes();
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentNumber);
            dst.DocumentState = map.MapFromDicNullable<DocumentState?>(src, IppAttributeNames.DocumentState);
            dst.DocumentStateReasons = map.MapFromDicSetNullable<DocumentStateReason[]?>(src, IppAttributeNames.DocumentStateReasons);
            dst.DocumentStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentStateMessage);
            dst.AttributesCharset = map.MapFromDicNullable<Charset?>(src, IppAttributeNames.AttributesCharset);
            dst.AttributesNaturalLanguage = map.MapFromDicNullable<NaturalLanguage?>(src, IppAttributeNames.AttributesNaturalLanguage);
            dst.CurrentPageOrder = map.MapFromDicNullable<CurrentPageOrder?>(src, IppAttributeNames.CurrentPageOrder);
            dst.DateTimeAtCompleted = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtCompleted);
            dst.DateTimeAtCreation = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtCreation);
            dst.DateTimeAtProcessing = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.DateTimeAtProcessing);
            dst.DetailedStatusMessages = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.DetailedStatusMessages);
            dst.DocumentAccessErrors = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.DocumentAccessErrors);
            dst.DocumentCharset = map.MapFromDicNullable<Charset?>(src, IppAttributeNames.DocumentCharset);
            dst.DocumentFormat = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormat);
            dst.DocumentFormatDetected = map.MapFromDicNullable<DocumentFormat?>(src, IppAttributeNames.DocumentFormatDetected);
            dst.DocumentFormatReady = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.DocumentFormatReady);
            dst.OutputDeviceDocumentState = map.MapFromDicNullable<DocumentState?>(src, IppAttributeNames.OutputDeviceDocumentState);
            dst.OutputDeviceDocumentStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.OutputDeviceDocumentStateMessage);
            dst.OutputDeviceDocumentStateReasons = map.MapFromDicSetNullable<DocumentStateReason[]?>(src, IppAttributeNames.OutputDeviceDocumentStateReasons);
            if (src.ContainsKey(IppAttributeNames.DocumentFormatDetails))
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(src[IppAttributeNames.DocumentFormatDetails].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey(IppAttributeNames.DocumentFormatDetailsDetected))
                dst.DocumentFormatDetailsDetected = map.Map<DocumentFormatDetails>(src[IppAttributeNames.DocumentFormatDetailsDetected].FromBegCollection().ToIppDictionary());
            dst.DocumentDigitalSignature = map.MapFromDicNullable<DocumentDigitalSignature?>(src, IppAttributeNames.DocumentDigitalSignature);
            dst.DocumentFormatVersion = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentFormatVersion);
            dst.DocumentFormatVersionDetected = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentFormatVersionDetected);
            dst.ErrorsCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.ErrorsCount);
            dst.WarningsCount = map.MapFromDicNullable<int?>(src, IppAttributeNames.WarningsCount);
            dst.PrintContentOptimizeActual = map.MapFromDicSetNullable<PrintContentOptimize[]?>(src, IppAttributeNames.PrintContentOptimizeActual);
            dst.DocumentJobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentJobId);
            dst.DocumentJobUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.DocumentJobUri);
            dst.DocumentMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentMessage);
            dst.DocumentName = map.MapFromDicNullable<string?>(src, IppAttributeNames.DocumentName);
            dst.DocumentResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.DocumentResourceIds);
            dst.DocumentNaturalLanguage = map.MapFromDicNullable<NaturalLanguage?>(src, IppAttributeNames.DocumentNaturalLanguage);
            dst.DocumentPrinterUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.DocumentPrinterUri);
            dst.DocumentUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.DocumentUri);
            dst.Impressions = map.MapFromDicNullable<int?>(src, IppAttributeNames.Impressions);
            dst.ImpressionsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.ImpressionsCompleted);
            dst.KOctets = map.MapFromDicNullable<int?>(src, IppAttributeNames.KOctets);
            dst.KOctetsProcessed = map.MapFromDicNullable<int?>(src, IppAttributeNames.KOctetsProcessed);
            dst.LastDocument = map.MapFromDicNullable<bool?>(src, IppAttributeNames.LastDocument);
            dst.MediaSheets = map.MapFromDicNullable<int?>(src, IppAttributeNames.MediaSheets);
            dst.MediaSheetsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.MediaSheetsCompleted);
            dst.MoreInfo = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.MoreInfo);
            dst.OutputDeviceAssigned = map.MapFromDicNullable<string?>(src, IppAttributeNames.OutputDeviceAssigned);
            dst.PrinterUpTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterUpTime);
            dst.TimeAtCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtCompleted);
            dst.TimeAtCreation = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtCreation);
            dst.TimeAtProcessing = map.MapFromDicNullable<int?>(src, IppAttributeNames.TimeAtProcessing);
            dst.Pages = map.MapFromDicNullable<int?>(src, IppAttributeNames.Pages);
            dst.PagesCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.PagesCompleted);
            dst.PrintContentOptimize = map.MapFromDicNullable<PrintContentOptimize?>(src, IppAttributeNames.PrintContentOptimize);
            if (src.ContainsKey(IppAttributeNames.InputAttributesActual))
                dst.InputAttributesActual = map.Map<DocumentTemplateAttributes>(src[IppAttributeNames.InputAttributesActual].FromBegCollection().ToIppDictionary());
            dst.DocumentMetadata = map.MapFromDicSetNullable<DocumentMetadata?>(src, IppAttributeNames.DocumentMetadata);
            return dst;
        });

        mapper.CreateMap<DocumentAttributes, IDictionary<string, IppAttribute[]>>((
            src,
            dst,
            map) =>
        {
            dst ??= new Dictionary<string, IppAttribute[]>();
            if (src.DocumentNumber != null)
                dst.Add(IppAttributeNames.DocumentNumber, [new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, src.DocumentNumber.Value)]);
            if (src.DocumentState != null)
                dst.Add(IppAttributeNames.DocumentState, [new IppAttribute(Tag.Enum, IppAttributeNames.DocumentState, (int)src.DocumentState.Value)]);
            if (src.DocumentStateReasons != null)
                dst.Add(IppAttributeNames.DocumentStateReasons, src.DocumentStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.DocumentStateReasons, map.Map<string>(x))).ToArray());
            if (src.DocumentStateMessage != null)
                dst.Add(IppAttributeNames.DocumentStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentStateMessage, src.DocumentStateMessage)]);
            if (src.AttributesCharset != null)
                dst.Add(IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, src.AttributesCharset.Value)]);
            if (src.AttributesNaturalLanguage != null)
                dst.Add(IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, src.AttributesNaturalLanguage.Value)]);
            if (src.CurrentPageOrder != null)
                dst.Add(IppAttributeNames.CurrentPageOrder, [new IppAttribute(Tag.Keyword, IppAttributeNames.CurrentPageOrder, map.Map<string>(src.CurrentPageOrder.Value))]);
            if (src.DateTimeAtCompleted != null)
                dst.Add(IppAttributeNames.DateTimeAtCompleted, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCompleted, src.DateTimeAtCompleted.Value)]);
            if (src.DateTimeAtCreation != null)
                dst.Add(IppAttributeNames.DateTimeAtCreation, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCreation, src.DateTimeAtCreation.Value)]);
            if (src.DateTimeAtProcessing != null)
                dst.Add(IppAttributeNames.DateTimeAtProcessing, [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtProcessing, src.DateTimeAtProcessing.Value)]);
            if (src.DetailedStatusMessages != null)
                dst.Add(IppAttributeNames.DetailedStatusMessages, src.DetailedStatusMessages.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DetailedStatusMessages, x)).ToArray());
            if (src.DocumentAccessErrors != null)
                dst.Add(IppAttributeNames.DocumentAccessErrors, src.DocumentAccessErrors.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentAccessErrors, x)).ToArray());
            if (src.DocumentCharset != null)
                dst.Add(IppAttributeNames.DocumentCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.DocumentCharset, src.DocumentCharset.Value)]);
            if (src.DocumentFormat != null)
                dst.Add(IppAttributeNames.DocumentFormat, [new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, src.DocumentFormat.Value)]);
            if (src.DocumentFormatDetected != null)
                dst.Add(IppAttributeNames.DocumentFormatDetected, [new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormatDetected, src.DocumentFormatDetected.Value)]);
            if (src.DocumentFormatReady != null)
                dst.Add(IppAttributeNames.DocumentFormatReady, src.DocumentFormatReady.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormatReady, x)).ToArray());
            if (src.OutputDeviceDocumentState != null)
                dst.Add(IppAttributeNames.OutputDeviceDocumentState, [new IppAttribute(Tag.Enum, IppAttributeNames.OutputDeviceDocumentState, (int)src.OutputDeviceDocumentState.Value)]);
            if (src.OutputDeviceDocumentStateMessage != null)
                dst.Add(IppAttributeNames.OutputDeviceDocumentStateMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.OutputDeviceDocumentStateMessage, src.OutputDeviceDocumentStateMessage)]);
            if (src.OutputDeviceDocumentStateReasons != null)
                dst.Add(IppAttributeNames.OutputDeviceDocumentStateReasons, src.OutputDeviceDocumentStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.OutputDeviceDocumentStateReasons, map.Map<string>(x))).ToArray());
            if (src.DocumentFormatDetails != null)
                dst.Add(IppAttributeNames.DocumentFormatDetails, map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(IppAttributeNames.DocumentFormatDetails).ToArray());
            if (src.DocumentFormatDetailsDetected != null)
                dst.Add(IppAttributeNames.DocumentFormatDetailsDetected, map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetailsDetected).ToBegCollection(IppAttributeNames.DocumentFormatDetailsDetected).ToArray());
            if (src.DocumentDigitalSignature != null)
                dst.Add(IppAttributeNames.DocumentDigitalSignature, [new IppAttribute(Tag.Keyword, IppAttributeNames.DocumentDigitalSignature, map.Map<string>(src.DocumentDigitalSignature.Value))]);
            if (src.DocumentFormatVersion != null)
                dst.Add(IppAttributeNames.DocumentFormatVersion, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentFormatVersion, src.DocumentFormatVersion)]);
            if (src.DocumentFormatVersionDetected != null)
                dst.Add(IppAttributeNames.DocumentFormatVersionDetected, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentFormatVersionDetected, src.DocumentFormatVersionDetected)]);
            if (src.ErrorsCount != null)
                dst.Add(IppAttributeNames.ErrorsCount, [new IppAttribute(Tag.Integer, IppAttributeNames.ErrorsCount, src.ErrorsCount.Value)]);
            if (src.WarningsCount != null)
                dst.Add(IppAttributeNames.WarningsCount, [new IppAttribute(Tag.Integer, IppAttributeNames.WarningsCount, src.WarningsCount.Value)]);
            if (src.PrintContentOptimizeActual != null)
                dst.Add(IppAttributeNames.PrintContentOptimizeActual, src.PrintContentOptimizeActual.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrintContentOptimizeActual, map.Map<string>(x))).ToArray());
            if (src.DocumentJobId != null)
                dst.Add(IppAttributeNames.DocumentJobId, [new IppAttribute(Tag.Integer, IppAttributeNames.DocumentJobId, src.DocumentJobId.Value)]);
            if (src.DocumentJobUri != null)
                dst.Add(IppAttributeNames.DocumentJobUri, [new IppAttribute(Tag.Uri, IppAttributeNames.DocumentJobUri, src.DocumentJobUri.ToString())]);
            if (src.DocumentMessage != null)
                dst.Add(IppAttributeNames.DocumentMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentMessage, src.DocumentMessage)]);
            if (src.DocumentName != null)
                dst.Add(IppAttributeNames.DocumentName, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.DocumentName, src.DocumentName)]);
            if (src.DocumentResourceIds != null)
                dst.Add(IppAttributeNames.DocumentResourceIds, src.DocumentResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.DocumentResourceIds, x)).ToArray());
            if (src.DocumentNaturalLanguage != null)
                dst.Add(IppAttributeNames.DocumentNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.DocumentNaturalLanguage, src.DocumentNaturalLanguage.Value)]);
            if (src.DocumentPrinterUri != null)
                dst.Add(IppAttributeNames.DocumentPrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.DocumentPrinterUri, src.DocumentPrinterUri.ToString())]);
            if (src.DocumentUri != null)
                dst.Add(IppAttributeNames.DocumentUri, [new IppAttribute(Tag.Uri, IppAttributeNames.DocumentUri, src.DocumentUri.ToString())]);
            if (src.Impressions != null)
                dst.Add(IppAttributeNames.Impressions, [new IppAttribute(Tag.Integer, IppAttributeNames.Impressions, src.Impressions.Value)]);
            if (src.ImpressionsCompleted != null)
                dst.Add(IppAttributeNames.ImpressionsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.ImpressionsCompleted, src.ImpressionsCompleted.Value)]);
            if (src.KOctets != null)
                dst.Add(IppAttributeNames.KOctets, [new IppAttribute(Tag.Integer, IppAttributeNames.KOctets, src.KOctets.Value)]);
            if (src.KOctetsProcessed != null)
                dst.Add(IppAttributeNames.KOctetsProcessed, [new IppAttribute(Tag.Integer, IppAttributeNames.KOctetsProcessed, src.KOctetsProcessed.Value)]);
            if (src.LastDocument != null)
                dst.Add(IppAttributeNames.LastDocument, [new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, src.LastDocument.Value)]);
            if (src.MediaSheets != null)
                dst.Add(IppAttributeNames.MediaSheets, [new IppAttribute(Tag.Integer, IppAttributeNames.MediaSheets, src.MediaSheets.Value)]);
            if (src.MediaSheetsCompleted != null)
                dst.Add(IppAttributeNames.MediaSheetsCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.MediaSheetsCompleted, src.MediaSheetsCompleted.Value)]);
            if (src.MoreInfo != null)
                dst.Add(IppAttributeNames.MoreInfo, [new IppAttribute(Tag.Uri, IppAttributeNames.MoreInfo, src.MoreInfo.ToString())]);
            if (src.OutputDeviceAssigned != null)
                dst.Add(IppAttributeNames.OutputDeviceAssigned, [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputDeviceAssigned, src.OutputDeviceAssigned)]);
            if (src.PrinterUpTime != null)
                dst.Add(IppAttributeNames.PrinterUpTime, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterUpTime, src.PrinterUpTime.Value)]);
            if (src.TimeAtCompleted != null)
                dst.Add(IppAttributeNames.TimeAtCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCompleted, src.TimeAtCompleted.Value)]);
            if (src.TimeAtCreation != null)
                dst.Add(IppAttributeNames.TimeAtCreation, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCreation, src.TimeAtCreation.Value)]);
            if (src.TimeAtProcessing != null)
                dst.Add(IppAttributeNames.TimeAtProcessing, [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtProcessing, src.TimeAtProcessing.Value)]);
            if (src.Pages != null)
                dst.Add(IppAttributeNames.Pages, [new IppAttribute(Tag.Integer, IppAttributeNames.Pages, src.Pages.Value)]);
            if (src.PagesCompleted != null)
                dst.Add(IppAttributeNames.PagesCompleted, [new IppAttribute(Tag.Integer, IppAttributeNames.PagesCompleted, src.PagesCompleted.Value)]);
            if (src.PrintContentOptimize != null)
                dst.Add(IppAttributeNames.PrintContentOptimize, [new IppAttribute(Tag.Keyword, IppAttributeNames.PrintContentOptimize, map.Map<string>(src.PrintContentOptimize.Value))]);
            if (src.InputAttributesActual != null)
                dst.Add(IppAttributeNames.InputAttributesActual, map.Map<IEnumerable<IppAttribute>>(src.InputAttributesActual).ToBegCollection(IppAttributeNames.InputAttributesActual).ToArray());
            if (src.DocumentMetadata != null)
                dst.Add(IppAttributeNames.DocumentMetadata, src.DocumentMetadata.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString(x))).ToArray());
            return dst;
        });

        mapper.CreateMap<DocumentAttributes, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "document-attributes", NoValue.Instance) };

            var dict = map.Map<IDictionary<string, IppAttribute[]>>(src);
            return dict.Values.SelectMany(x => x);
        });
    }

}
