using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetSystemAttributesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetSystemAttributesResponse>((src, map) =>
        {
            var dst = new GetSystemAttributesResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            if (src.SystemAttributes.Count > 0)
            {
                var systemAttrs = src.SystemAttributes.SelectMany(x => x).ToIppDictionary();
                dst.SystemAttributes = map.Map<IDictionary<string, IppAttribute[]>, SystemStatusAttributes>(systemAttrs);
                dst.SystemDescriptionAttributes = map.Map<IDictionary<string, IppAttribute[]>, SystemDescriptionAttributes>(systemAttrs);
            }
            return dst;
        });

        mapper.CreateMap<IppResponseMessage, SystemStatusAttributes>((src, map) =>
        {
            return map.Map<IDictionary<string, IppAttribute[]>, SystemStatusAttributes>(src.SystemAttributes.SelectMany(x => x).ToIppDictionary());
        });

        mapper.CreateMap<IppResponseMessage, SystemDescriptionAttributes>((src, map) =>
        {
            return map.Map<IDictionary<string, IppAttribute[]>, SystemDescriptionAttributes>(src.SystemAttributes.SelectMany(x => x).ToIppDictionary());
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemStatusAttributes>((src, map) =>
        {
            var dst = new SystemStatusAttributes
            {
                SystemState = map.MapFromDicNullable<PrinterState?>(src, IppAttributeNames.SystemState),
                SystemStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemStateMessage),
                SystemStateReasons = map.MapFromDicSetNullable<SystemStateReason[]?>(src, IppAttributeNames.SystemStateReasons),
                SystemStateChangeTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemStateChangeTime),
                SystemStateChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.SystemStateChangeDateTime),
                SystemUpTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemUpTime),
                SystemTimeSourceConfigured = map.MapFromDicNullable<SystemTimeSourceConfigured?>(src, IppAttributeNames.SystemTimeSourceConfigured),
                SystemUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemUuid),
                XriAuthenticationSupported = map.MapFromDicSetNullable<UriAuthentication[]?>(src, IppAttributeNames.XriAuthenticationSupported),
                XriSecuritySupported = map.MapFromDicSetNullable<UriSecurity[]?>(src, IppAttributeNames.XriSecuritySupported),
                XriUriSchemeSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, IppAttributeNames.XriUriSchemeSupported)
            };
            if (src.ContainsKey(IppAttributeNames.SystemConfiguredPrinters))
                dst.SystemConfiguredPrinters = src[IppAttributeNames.SystemConfiguredPrinters].GroupBegCollection().Select(x => map.Map<SystemConfiguredPrinter>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(IppAttributeNames.SystemConfiguredResources))
                dst.SystemConfiguredResources = src[IppAttributeNames.SystemConfiguredResources].GroupBegCollection().Select(x => map.Map<SystemConfiguredResource>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(IppAttributeNames.PowerLogCol))
                dst.PowerLogCol = src[IppAttributeNames.PowerLogCol].GroupBegCollection().Select(x => map.Map<PowerLogEntry>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(IppAttributeNames.PowerStateCapabilitiesCol))
                dst.PowerStateCapabilitiesCol = src[IppAttributeNames.PowerStateCapabilitiesCol].GroupBegCollection().Select(x => map.Map<PowerStateCapability>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(IppAttributeNames.PowerStateCountersCol))
                dst.PowerStateCountersCol = src[IppAttributeNames.PowerStateCountersCol].GroupBegCollection().Select(x => map.Map<PowerStateCounter>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(IppAttributeNames.PowerStateMonitorCol))
                dst.PowerStateMonitorCol = src[IppAttributeNames.PowerStateMonitorCol].GroupBegCollection().Select(x => map.Map<PowerStateMonitor>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(IppAttributeNames.PowerStateTransitionsCol))
                dst.PowerStateTransitionsCol = src[IppAttributeNames.PowerStateTransitionsCol].GroupBegCollection().Select(x => map.Map<PowerStateTransition>(x.FromBegCollection().ToIppDictionary())).ToArray();

            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemDescriptionAttributes>((src, map) =>
        {
            return new SystemDescriptionAttributes
            {
                SystemConfigChanges = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemConfigChanges),
                SystemConfiguredPrinters = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemConfiguredPrinters),
                SystemConfiguredResources = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemConfiguredResources),
                SystemStringsLanguagesSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemStringsLanguagesSupported),
                SystemStringsUri = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemStringsUri),
                SystemSerialNumber = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemSerialNumber),
                SystemImpressionsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemImpressionsCompleted),
                SystemImpressionsCompletedCol = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemImpressionsCompletedCol),
                SystemMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemMediaSheetsCompleted),
                SystemMediaSheetsCompletedCol = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemMediaSheetsCompletedCol),
                SystemPagesCompleted = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemPagesCompleted),
                SystemPagesCompletedCol = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemPagesCompletedCol),
                CharsetConfigured = map.MapFromDicNullable<string?>(src, IppAttributeNames.CharsetConfigured),
                CharsetSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.CharsetSupported),
                DocumentFormatSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.DocumentFormatSupported),
                GeneratedNaturalLanguageSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.GeneratedNaturalLanguageSupported),
                IppFeaturesSupported = map.MapFromDicSetNullable<IppFeature[]?>(src, IppAttributeNames.IppFeaturesSupported),
                IppVersionsSupported = src.ContainsKey(IppAttributeNames.IppVersionsSupported)
                    ? src[IppAttributeNames.IppVersionsSupported].Select(x => new IppVersion(x.Value.ToString()!)).ToArray()
                    : null,
                MultipleDocumentPrintersSupported = map.MapFromDicNullable<bool?>(src, IppAttributeNames.MultipleDocumentPrintersSupported),
                NaturalLanguageConfigured = map.MapFromDicNullable<string?>(src, IppAttributeNames.NaturalLanguageConfigured),
                OperationsSupported = src.ContainsKey(IppAttributeNames.OperationsSupported)
                    ? src[IppAttributeNames.OperationsSupported].Select(x => (IppOperation)Enum.Parse(typeof(IppOperation), x.Value.ToString()!)).ToArray()
                    : null,
                IppGetEventLife = map.MapFromDicNullable<int?>(src, IppAttributeNames.IppGetEventLife),
                OutputDeviceX509TypeSupported = map.MapFromDicSetNullable<X509Type[]?>(src, IppAttributeNames.OutputDeviceX509TypeSupported),
                PowerCalendarPolicyCol = src.ContainsKey(IppAttributeNames.PowerCalendarPolicyCol) ? src[IppAttributeNames.PowerCalendarPolicyCol].GroupBegCollection().Select(x => map.Map<PowerCalendarPolicy>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                PowerEventPolicyCol = src.ContainsKey(IppAttributeNames.PowerEventPolicyCol) ? src[IppAttributeNames.PowerEventPolicyCol].GroupBegCollection().Select(x => map.Map<PowerEventPolicy>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                PowerTimeoutPolicyCol = src.ContainsKey(IppAttributeNames.PowerTimeoutPolicyCol) ? src[IppAttributeNames.PowerTimeoutPolicyCol].GroupBegCollection().Select(x => map.Map<PowerTimeoutPolicy>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                PrinterCreationAttributesSupported = map.MapFromDicSetNullable<PrinterCreationAttribute[]?>(src, IppAttributeNames.PrinterCreationAttributesSupported),
                PrinterServiceTypeSupported = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, IppAttributeNames.PrinterServiceTypeSupported),
                ResourceFormatSupported = map.MapFromDicSetNullable<ResourceFormat[]?>(src, IppAttributeNames.ResourceFormatSupported),
                ResourceTypeSupported = map.MapFromDicSetNullable<ResourceType[]?>(src, IppAttributeNames.ResourceTypeSupported),
                ResourceSettableAttributesSupported = map.MapFromDicSetNullable<ResourceSettableAttribute[]?>(src, IppAttributeNames.ResourceSettableAttributesSupported),
                NotifyAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.NotifyAttributesSupported),
                NotifyEventsDefault = map.MapFromDicSetNullable<NotifyEvent[]?>(src, IppAttributeNames.NotifyEventsDefault),
                NotifyEventsSupported = map.MapFromDicSetNullable<NotifyEvent[]?>(src, IppAttributeNames.NotifyEventsSupported),
                NotifyLeaseDurationDefault = map.MapFromDicNullable<int?>(src, IppAttributeNames.NotifyLeaseDurationDefault),
                NotifyLeaseDurationSupported = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.NotifyLeaseDurationSupported),
                NotifyMaxEventsSupported = map.MapFromDicNullable<int?>(src, IppAttributeNames.NotifyMaxEventsSupported),
                NotifyPullMethodSupported = map.MapFromDicSetNullable<NotifyPullMethod[]?>(src, IppAttributeNames.NotifyPullMethodSupported),
                NotifySchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, IppAttributeNames.NotifySchemesSupported),
                SystemConfigChangeTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemConfigChangeTime),
                SystemConfigChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.SystemConfigChangeDateTime),
                SystemUpTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemUpTime),
                SystemUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemUuid),
                SystemGeoLocation = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemGeoLocation),
                SystemAssetTag = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.SystemAssetTag),
                SystemCurrentTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.SystemCurrentTime),
                SystemContactCol = src.ContainsKey(IppAttributeNames.SystemContactCol)
                    ? src[IppAttributeNames.SystemContactCol].GroupBegCollection().Select(x => map.Map<SystemContact>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                SystemServiceContactCol = src.ContainsKey(IppAttributeNames.SystemServiceContactCol)
                    ? src[IppAttributeNames.SystemServiceContactCol].GroupBegCollection().Select(x => map.Map<SystemContact>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                SystemXriSupported = src.ContainsKey(IppAttributeNames.SystemXriSupported)
                    ? src[IppAttributeNames.SystemXriSupported].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                SystemInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemInfo),
                SystemLocation = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemLocation),
                SystemMakeAndModel = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemMakeAndModel),
                SystemMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemMessageFromOperator),
                SystemName = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemName),
                SystemDefaultPrinterId = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemDefaultPrinterId),
                SystemDnsSdName = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemDnsSdName),
                SystemMandatoryPrinterAttributes = map.MapFromDicSetNullable<SystemMandatoryPrinterAttribute[]?>(src, IppAttributeNames.SystemMandatoryPrinterAttributes),
                SystemMandatoryRegistrationAttributes = map.MapFromDicSetNullable<SystemMandatoryRegistrationAttribute[]?>(src, IppAttributeNames.SystemMandatoryRegistrationAttributes),
                SystemSettableAttributesSupported = map.MapFromDicSetNullable<SystemSettableAttribute[]?>(src, IppAttributeNames.SystemSettableAttributesSupported),
                SystemFirmwareName = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemFirmwareName),
                SystemFirmwarePatches = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemFirmwarePatches),
                SystemFirmwareStringVersion = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemFirmwareStringVersion),
                SystemFirmwareVersion = map.MapFromDicSetNullable<OctetString[]?>(src, IppAttributeNames.SystemFirmwareVersion),
                SystemResidentApplicationName = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemResidentApplicationName),
                SystemResidentApplicationPatches = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemResidentApplicationPatches),
                SystemResidentApplicationStringVersion = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemResidentApplicationStringVersion),
                SystemResidentApplicationVersion = map.MapFromDicSetNullable<OctetString[]?>(src, IppAttributeNames.SystemResidentApplicationVersion),
                SystemUserApplicationName = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemUserApplicationName),
                SystemUserApplicationPatches = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemUserApplicationPatches),
                SystemUserApplicationStringVersion = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.SystemUserApplicationStringVersion),
                SystemUserApplicationVersion = map.MapFromDicSetNullable<OctetString[]?>(src, IppAttributeNames.SystemUserApplicationVersion),
                SystemTimeSourceConfigured = map.MapFromDicNullable<SystemTimeSourceConfigured?>(src, IppAttributeNames.SystemTimeSourceConfigured),
                SystemState = map.MapFromDicNullable<PrinterState?>(src, IppAttributeNames.SystemState),
                SystemStateReasons = map.MapFromDicSetNullable<SystemStateReason[]?>(src, IppAttributeNames.SystemStateReasons),
                SystemStateMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.SystemStateMessage),
                SystemStateChangeTime = map.MapFromDicNullable<int?>(src, IppAttributeNames.SystemStateChangeTime),
                SystemStateChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, IppAttributeNames.SystemStateChangeDateTime),
            };
        });

        mapper.CreateMap<GetSystemAttributesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);

            var attrs = new List<IppAttribute>();
            if (src.SystemAttributes != null)
            {
                if (src.SystemAttributes.SystemState.HasValue)
                    attrs.Add(new IppAttribute(Tag.Enum, IppAttributeNames.SystemState, (int)src.SystemAttributes.SystemState.Value));
                if (src.SystemAttributes.SystemStateMessage != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemStateMessage, src.SystemAttributes.SystemStateMessage));
                if (src.SystemAttributes.SystemStateReasons != null)
                    attrs.AddRange(src.SystemAttributes.SystemStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SystemStateReasons, x.Value)));
                if (src.SystemAttributes.SystemStateChangeTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemStateChangeTime, src.SystemAttributes.SystemStateChangeTime.Value));
                if (src.SystemAttributes.SystemStateChangeDateTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.SystemStateChangeDateTime, src.SystemAttributes.SystemStateChangeDateTime.Value));
                if (src.SystemAttributes.SystemUpTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemUpTime, src.SystemAttributes.SystemUpTime.Value));
                if (src.SystemAttributes.SystemTimeSourceConfigured != null)
                    attrs.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.SystemTimeSourceConfigured, src.SystemAttributes.SystemTimeSourceConfigured.Value.Value));
                if (src.SystemAttributes.SystemConfiguredPrinters != null)
                    attrs.AddRange(src.SystemAttributes.SystemConfiguredPrinters.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.SystemConfiguredPrinters)));
                if (src.SystemAttributes.SystemConfiguredResources != null)
                    attrs.AddRange(src.SystemAttributes.SystemConfiguredResources.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.SystemConfiguredResources)));
                if (src.SystemAttributes.PowerLogCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerLogCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerLogCol)));
                if (src.SystemAttributes.PowerStateCapabilitiesCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateCapabilitiesCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerStateCapabilitiesCol)));
                if (src.SystemAttributes.PowerStateCountersCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateCountersCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerStateCountersCol)));
                if (src.SystemAttributes.PowerStateMonitorCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateMonitorCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerStateMonitorCol)));
                if (src.SystemAttributes.PowerStateTransitionsCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateTransitionsCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerStateTransitionsCol)));

                if (src.SystemAttributes.SystemUuid != null)
                    attrs.Add(new IppAttribute(Tag.Uri, IppAttributeNames.SystemUuid, src.SystemAttributes.SystemUuid.ToString()));
                if (src.SystemAttributes.XriAuthenticationSupported != null)
                    attrs.AddRange(src.SystemAttributes.XriAuthenticationSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.XriAuthenticationSupported, x.ToString())));
                if (src.SystemAttributes.XriSecuritySupported != null)
                    attrs.AddRange(src.SystemAttributes.XriSecuritySupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.XriSecuritySupported, x.ToString())));
                if (src.SystemAttributes.XriUriSchemeSupported != null)
                    attrs.AddRange(src.SystemAttributes.XriUriSchemeSupported.Select(x => new IppAttribute(Tag.UriScheme, IppAttributeNames.XriUriSchemeSupported, x.ToString())));

            }

            if (src.SystemDescriptionAttributes != null)
            {
                if (src.SystemDescriptionAttributes.SystemConfigChanges.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemConfigChanges, src.SystemDescriptionAttributes.SystemConfigChanges.Value));
                if (src.SystemDescriptionAttributes.SystemConfiguredPrinters.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemConfiguredPrinters, src.SystemDescriptionAttributes.SystemConfiguredPrinters.Value));
                if (src.SystemDescriptionAttributes.SystemConfiguredResources.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemConfiguredResources, src.SystemDescriptionAttributes.SystemConfiguredResources.Value));

                if (src.SystemDescriptionAttributes.SystemStringsLanguagesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemStringsLanguagesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SystemStringsLanguagesSupported, x)));
                if (src.SystemDescriptionAttributes.SystemStringsUri != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemStringsUri.Select(x => new IppAttribute(Tag.Uri, IppAttributeNames.SystemStringsUri, x)));
                if (src.SystemDescriptionAttributes.SystemSerialNumber != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemSerialNumber, src.SystemDescriptionAttributes.SystemSerialNumber));
                if (src.SystemDescriptionAttributes.SystemImpressionsCompleted.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemImpressionsCompleted, src.SystemDescriptionAttributes.SystemImpressionsCompleted.Value));
                if (src.SystemDescriptionAttributes.SystemImpressionsCompletedCol.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemImpressionsCompletedCol, src.SystemDescriptionAttributes.SystemImpressionsCompletedCol.Value));
                if (src.SystemDescriptionAttributes.SystemMediaSheetsCompleted.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemMediaSheetsCompleted, src.SystemDescriptionAttributes.SystemMediaSheetsCompleted.Value));
                if (src.SystemDescriptionAttributes.SystemMediaSheetsCompletedCol.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemMediaSheetsCompletedCol, src.SystemDescriptionAttributes.SystemMediaSheetsCompletedCol.Value));
                if (src.SystemDescriptionAttributes.SystemPagesCompleted.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemPagesCompleted, src.SystemDescriptionAttributes.SystemPagesCompleted.Value));
                if (src.SystemDescriptionAttributes.SystemPagesCompletedCol.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemPagesCompletedCol, src.SystemDescriptionAttributes.SystemPagesCompletedCol.Value));
                if (src.SystemDescriptionAttributes.SystemConfigChangeTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemConfigChangeTime, src.SystemDescriptionAttributes.SystemConfigChangeTime.Value));
                if (src.SystemDescriptionAttributes.SystemConfigChangeDateTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.SystemConfigChangeDateTime, src.SystemDescriptionAttributes.SystemConfigChangeDateTime.Value));
                if (src.SystemDescriptionAttributes.SystemUpTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemUpTime, src.SystemDescriptionAttributes.SystemUpTime.Value));
                if (src.SystemDescriptionAttributes.SystemUuid != null)
                    attrs.Add(new IppAttribute(Tag.Uri, IppAttributeNames.SystemUuid, src.SystemDescriptionAttributes.SystemUuid.ToString()));
                if (src.SystemDescriptionAttributes.SystemGeoLocation != null)
                    attrs.Add(new IppAttribute(Tag.Uri, IppAttributeNames.SystemGeoLocation, src.SystemDescriptionAttributes.SystemGeoLocation.ToString()));
                if (src.SystemDescriptionAttributes.SystemAssetTag != null)
                    attrs.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.SystemAssetTag, src.SystemDescriptionAttributes.SystemAssetTag.Value));
                if (src.SystemDescriptionAttributes.SystemCurrentTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.SystemCurrentTime, src.SystemDescriptionAttributes.SystemCurrentTime.Value));
                if (src.SystemDescriptionAttributes.SystemContactCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemContactCol
                        .SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.SystemContactCol)));
                if (src.SystemDescriptionAttributes.SystemServiceContactCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemServiceContactCol
                        .SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.SystemServiceContactCol)));
                if (src.SystemDescriptionAttributes.SystemXriSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemXriSupported
                        .SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.SystemXriSupported)));
                if (src.SystemDescriptionAttributes.IppGetEventLife.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.IppGetEventLife, src.SystemDescriptionAttributes.IppGetEventLife.Value));
                if (src.SystemDescriptionAttributes.CharsetConfigured != null)
                    attrs.Add(new IppAttribute(Tag.Charset, IppAttributeNames.CharsetConfigured, src.SystemDescriptionAttributes.CharsetConfigured));
                if (src.SystemDescriptionAttributes.CharsetSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.CharsetSupported.Select(x => new IppAttribute(Tag.Charset, IppAttributeNames.CharsetSupported, x)));
                if (src.SystemDescriptionAttributes.DocumentFormatSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.DocumentFormatSupported.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormatSupported, x)));
                if (src.SystemDescriptionAttributes.GeneratedNaturalLanguageSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.GeneratedNaturalLanguageSupported.Select(x => new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.GeneratedNaturalLanguageSupported, x)));
                if (src.SystemDescriptionAttributes.IppFeaturesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.IppFeaturesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.IppFeaturesSupported, x.Value)));
                if (src.SystemDescriptionAttributes.IppVersionsSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.IppVersionsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.IppVersionsSupported, x.ToString())));
                if (src.SystemDescriptionAttributes.MultipleDocumentPrintersSupported.HasValue)
                    attrs.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.MultipleDocumentPrintersSupported, src.SystemDescriptionAttributes.MultipleDocumentPrintersSupported.Value));
                if (src.SystemDescriptionAttributes.NaturalLanguageConfigured != null)
                    attrs.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.NaturalLanguageConfigured, src.SystemDescriptionAttributes.NaturalLanguageConfigured));
                if (src.SystemDescriptionAttributes.OperationsSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.OperationsSupported.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.OperationsSupported, (int)x)));
                if (src.SystemDescriptionAttributes.OutputDeviceX509TypeSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.OutputDeviceX509TypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.OutputDeviceX509TypeSupported, x.Value)));
                if (src.SystemDescriptionAttributes.PowerCalendarPolicyCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PowerCalendarPolicyCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerCalendarPolicyCol)));
                if (src.SystemDescriptionAttributes.PowerEventPolicyCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PowerEventPolicyCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerEventPolicyCol)));
                if (src.SystemDescriptionAttributes.PowerTimeoutPolicyCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PowerTimeoutPolicyCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PowerTimeoutPolicyCol)));
                if (src.SystemDescriptionAttributes.PrinterCreationAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PrinterCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterCreationAttributesSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.PrinterServiceTypeSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PrinterServiceTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterServiceTypeSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.ResourceFormatSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.ResourceFormatSupported.Select(x => new IppAttribute(Tag.MimeMediaType, IppAttributeNames.ResourceFormatSupported, x.Value)));
                if (src.SystemDescriptionAttributes.ResourceTypeSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.ResourceTypeSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceTypeSupported, x.Value)));
                if (src.SystemDescriptionAttributes.ResourceSettableAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.ResourceSettableAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.ResourceSettableAttributesSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.SystemInfo != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemInfo, src.SystemDescriptionAttributes.SystemInfo));
                if (src.SystemDescriptionAttributes.SystemLocation != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemLocation, src.SystemDescriptionAttributes.SystemLocation));
                if (src.SystemDescriptionAttributes.SystemMakeAndModel != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemMakeAndModel, src.SystemDescriptionAttributes.SystemMakeAndModel));
                if (src.SystemDescriptionAttributes.SystemMessageFromOperator != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemMessageFromOperator, src.SystemDescriptionAttributes.SystemMessageFromOperator));
                if (src.SystemDescriptionAttributes.SystemName != null)
                    attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.SystemName, src.SystemDescriptionAttributes.SystemName));
                if (src.SystemDescriptionAttributes.SystemDefaultPrinterId.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemDefaultPrinterId, src.SystemDescriptionAttributes.SystemDefaultPrinterId.Value));
                if (src.SystemDescriptionAttributes.SystemDnsSdName != null)
                    attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.SystemDnsSdName, src.SystemDescriptionAttributes.SystemDnsSdName));
                if (src.SystemDescriptionAttributes.SystemMandatoryPrinterAttributes != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemMandatoryPrinterAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SystemMandatoryPrinterAttributes, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.SystemMandatoryRegistrationAttributes != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemMandatoryRegistrationAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SystemMandatoryRegistrationAttributes, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.SystemSettableAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemSettableAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SystemSettableAttributesSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.NotifyAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyAttributesSupported, x)));
                if (src.SystemDescriptionAttributes.NotifyEventsDefault != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyEventsDefault.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyEventsDefault, x.Value)));
                if (src.SystemDescriptionAttributes.NotifyEventsSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyEventsSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyEventsSupported, x.Value)));
                if (src.SystemDescriptionAttributes.NotifyLeaseDurationDefault.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.NotifyLeaseDurationDefault, src.SystemDescriptionAttributes.NotifyLeaseDurationDefault.Value));
                if (src.SystemDescriptionAttributes.NotifyLeaseDurationSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyLeaseDurationSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyLeaseDurationSupported, x)));
                if (src.SystemDescriptionAttributes.NotifyMaxEventsSupported.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.NotifyMaxEventsSupported, src.SystemDescriptionAttributes.NotifyMaxEventsSupported.Value));
                if (src.SystemDescriptionAttributes.NotifyPullMethodSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyPullMethodSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyPullMethodSupported, x.Value)));
                if (src.SystemDescriptionAttributes.NotifySchemesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifySchemesSupported.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.NotifySchemesSupported, x.ToString())));
                if (src.SystemDescriptionAttributes.SystemFirmwareName != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwareName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.SystemFirmwareName, x)));
                if (src.SystemDescriptionAttributes.SystemFirmwarePatches != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwarePatches.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemFirmwarePatches, x)));
                if (src.SystemDescriptionAttributes.SystemFirmwareStringVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwareStringVersion.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemFirmwareStringVersion, x)));
                if (src.SystemDescriptionAttributes.SystemFirmwareVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwareVersion.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.SystemFirmwareVersion, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationName != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.SystemResidentApplicationName, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationPatches != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationPatches.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemResidentApplicationPatches, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationStringVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationStringVersion.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemResidentApplicationStringVersion, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationVersion.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.SystemResidentApplicationVersion, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationName != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.SystemUserApplicationName, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationPatches != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationPatches.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemUserApplicationPatches, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationStringVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationStringVersion.Select(x => new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemUserApplicationStringVersion, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationVersion.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.SystemUserApplicationVersion, x)));
                if (src.SystemDescriptionAttributes.SystemTimeSourceConfigured != null)
                    attrs.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.SystemTimeSourceConfigured, src.SystemDescriptionAttributes.SystemTimeSourceConfigured.Value.Value));
                if (src.SystemDescriptionAttributes.SystemState.HasValue)
                    attrs.Add(new IppAttribute(Tag.Enum, IppAttributeNames.SystemState, (int)src.SystemDescriptionAttributes.SystemState.Value));
                if (src.SystemDescriptionAttributes.SystemStateReasons != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.SystemStateReasons, x.Value)));
                if (src.SystemDescriptionAttributes.SystemStateMessage != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.SystemStateMessage, src.SystemDescriptionAttributes.SystemStateMessage));
                if (src.SystemDescriptionAttributes.SystemStateChangeTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, IppAttributeNames.SystemStateChangeTime, src.SystemDescriptionAttributes.SystemStateChangeTime.Value));
                if (src.SystemDescriptionAttributes.SystemStateChangeDateTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.DateTime, IppAttributeNames.SystemStateChangeDateTime, src.SystemDescriptionAttributes.SystemStateChangeDateTime.Value));
            }

            if (attrs.Count > 0)
                dst.SystemAttributes.Add(attrs);

            return dst;
        });
    }
}
