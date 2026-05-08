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
                SystemState = map.MapFromDicNullable<PrinterState?>(src, SystemAttribute.SystemState),
                SystemStateMessage = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemStateMessage),
                SystemStateReasons = map.MapFromDicSetNullable<SystemStateReason[]?>(src, SystemAttribute.SystemStateReasons),
                SystemStateChangeTime = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemStateChangeTime),
                SystemStateChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, SystemAttribute.SystemStateChangeDateTime),
                SystemUpTime = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemUpTime),
                SystemTimeSourceConfigured = map.MapFromDicNullable<SystemTimeSourceConfigured?>(src, SystemAttribute.SystemTimeSourceConfigured),
                SystemUuid = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUuid),
                XriAuthenticationSupported = map.MapFromDicSetNullable<UriAuthentication[]?>(src, SystemAttribute.XriAuthenticationSupported),
                XriSecuritySupported = map.MapFromDicSetNullable<UriSecurity[]?>(src, SystemAttribute.XriSecuritySupported),
                XriUriSchemeSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, SystemAttribute.XriUriSchemeSupported)
            };
            if (src.ContainsKey(SystemAttribute.SystemConfiguredPrinters))
                dst.SystemConfiguredPrinters = src[SystemAttribute.SystemConfiguredPrinters].GroupBegCollection().Select(x => map.Map<SystemConfiguredPrinter>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(SystemAttribute.SystemConfiguredResources))
                dst.SystemConfiguredResources = src[SystemAttribute.SystemConfiguredResources].GroupBegCollection().Select(x => map.Map<SystemConfiguredResource>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(SystemAttribute.PowerLogCol))
                dst.PowerLogCol = src[SystemAttribute.PowerLogCol].GroupBegCollection().Select(x => map.Map<PowerLogEntry>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(SystemAttribute.PowerStateCapabilitiesCol))
                dst.PowerStateCapabilitiesCol = src[SystemAttribute.PowerStateCapabilitiesCol].GroupBegCollection().Select(x => map.Map<PowerStateCapability>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(SystemAttribute.PowerStateCountersCol))
                dst.PowerStateCountersCol = src[SystemAttribute.PowerStateCountersCol].GroupBegCollection().Select(x => map.Map<PowerStateCounter>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(SystemAttribute.PowerStateMonitorCol))
                dst.PowerStateMonitorCol = src[SystemAttribute.PowerStateMonitorCol].GroupBegCollection().Select(x => map.Map<PowerStateMonitor>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.ContainsKey(SystemAttribute.PowerStateTransitionsCol))
                dst.PowerStateTransitionsCol = src[SystemAttribute.PowerStateTransitionsCol].GroupBegCollection().Select(x => map.Map<PowerStateTransition>(x.FromBegCollection().ToIppDictionary())).ToArray();

            return dst;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemDescriptionAttributes>((src, map) =>
        {
            return new SystemDescriptionAttributes
            {
                SystemConfigChanges = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemConfigChanges),
                SystemConfiguredPrinters = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemConfiguredPrinters),
                SystemConfiguredResources = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemConfiguredResources),
                SystemStringsLanguagesSupported = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemStringsLanguagesSupported),
                SystemStringsUri = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemStringsUri),
                SystemSerialNumber = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemSerialNumber),
                SystemImpressionsCompleted = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemImpressionsCompleted),
                SystemImpressionsCompletedCol = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemImpressionsCompletedCol),
                SystemMediaSheetsCompleted = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemMediaSheetsCompleted),
                SystemMediaSheetsCompletedCol = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemMediaSheetsCompletedCol),
                SystemPagesCompleted = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemPagesCompleted),
                SystemPagesCompletedCol = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemPagesCompletedCol),
                CharsetConfigured = map.MapFromDicNullable<string?>(src, SystemAttribute.CharsetConfigured),
                CharsetSupported = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.CharsetSupported),
                DocumentFormatSupported = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.DocumentFormatSupported),
                GeneratedNaturalLanguageSupported = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.GeneratedNaturalLanguageSupported),
                IppFeaturesSupported = map.MapFromDicSetNullable<IppFeature[]?>(src, SystemAttribute.IppFeaturesSupported),
                IppVersionsSupported = src.ContainsKey(SystemAttribute.IppVersionsSupported)
                    ? src[SystemAttribute.IppVersionsSupported].Select(x => new IppVersion(x.Value.ToString()!)).ToArray()
                    : null,
                MultipleDocumentPrintersSupported = map.MapFromDicNullable<bool?>(src, SystemAttribute.MultipleDocumentPrintersSupported),
                NaturalLanguageConfigured = map.MapFromDicNullable<string?>(src, SystemAttribute.NaturalLanguageConfigured),
                OperationsSupported = src.ContainsKey(SystemAttribute.OperationsSupported)
                    ? src[SystemAttribute.OperationsSupported].Select(x => (IppOperation)Enum.Parse(typeof(IppOperation), x.Value.ToString()!)).ToArray()
                    : null,
                IppGetEventLife = map.MapFromDicNullable<int?>(src, SystemAttribute.IppGetEventLife),
                OutputDeviceX509TypeSupported = map.MapFromDicSetNullable<X509Type[]?>(src, SystemAttribute.OutputDeviceX509TypeSupported),
                PowerCalendarPolicyCol = src.ContainsKey(SystemAttribute.PowerCalendarPolicyCol) ? src[SystemAttribute.PowerCalendarPolicyCol].GroupBegCollection().Select(x => map.Map<PowerCalendarPolicy>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                PowerEventPolicyCol = src.ContainsKey(SystemAttribute.PowerEventPolicyCol) ? src[SystemAttribute.PowerEventPolicyCol].GroupBegCollection().Select(x => map.Map<PowerEventPolicy>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                PowerTimeoutPolicyCol = src.ContainsKey(SystemAttribute.PowerTimeoutPolicyCol) ? src[SystemAttribute.PowerTimeoutPolicyCol].GroupBegCollection().Select(x => map.Map<PowerTimeoutPolicy>(x.FromBegCollection().ToIppDictionary())).ToArray() : null,
                PrinterCreationAttributesSupported = map.MapFromDicSetNullable<PrinterCreationAttribute[]?>(src, SystemAttribute.PrinterCreationAttributesSupported),
                PrinterServiceTypeSupported = map.MapFromDicSetNullable<PrinterServiceType[]?>(src, SystemAttribute.PrinterServiceTypeSupported),
                ResourceFormatSupported = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.ResourceFormatSupported),
                ResourceTypeSupported = map.MapFromDicSetNullable<ResourceType[]?>(src, SystemAttribute.ResourceTypeSupported),
                ResourceSettableAttributesSupported = map.MapFromDicSetNullable<ResourceSettableAttribute[]?>(src, SystemAttribute.ResourceSettableAttributesSupported),
                NotifyAttributesSupported = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.NotifyAttributesSupported),
                NotifyEventsDefault = map.MapFromDicSetNullable<NotifyEvent[]?>(src, SystemAttribute.NotifyEventsDefault),
                NotifyEventsSupported = map.MapFromDicSetNullable<NotifyEvent[]?>(src, SystemAttribute.NotifyEventsSupported),
                NotifyLeaseDurationDefault = map.MapFromDicNullable<int?>(src, SystemAttribute.NotifyLeaseDurationDefault),
                NotifyLeaseDurationSupported = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.NotifyLeaseDurationSupported),
                NotifyMaxEventsSupported = map.MapFromDicNullable<int?>(src, SystemAttribute.NotifyMaxEventsSupported),
                NotifyPullMethodSupported = map.MapFromDicSetNullable<NotifyPullMethod[]?>(src, SystemAttribute.NotifyPullMethodSupported),
                NotifySchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, SystemAttribute.NotifySchemesSupported),
                SystemConfigChangeTime = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemConfigChangeTime),
                SystemConfigChangeDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, SystemAttribute.SystemConfigChangeDateTime),
                SystemUpTime = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemUpTime),
                SystemUuid = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUuid),
                SystemGeoLocation = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemGeoLocation),
                SystemAssetTag = map.MapFromDicNullable<byte[]?>(src, SystemAttribute.SystemAssetTag),
                SystemCurrentTime = map.MapFromDicNullable<DateTimeOffset?>(src, SystemAttribute.SystemCurrentTime),
                SystemContactCol = src.ContainsKey(SystemAttribute.SystemContactCol)
                    ? src[SystemAttribute.SystemContactCol].GroupBegCollection().Select(x => map.Map<SystemContact>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                SystemServiceContactCol = src.ContainsKey(SystemAttribute.SystemServiceContactCol)
                    ? src[SystemAttribute.SystemServiceContactCol].GroupBegCollection().Select(x => map.Map<SystemContact>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                SystemXriSupported = src.ContainsKey(SystemAttribute.SystemXriSupported)
                    ? src[SystemAttribute.SystemXriSupported].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray()
                    : null,
                SystemInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemInfo),
                SystemLocation = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemLocation),
                SystemMakeAndModel = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemMakeAndModel),
                SystemMessageFromOperator = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemMessageFromOperator),
                SystemName = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemName),
                SystemDefaultPrinterId = map.MapFromDicNullable<int?>(src, SystemAttribute.SystemDefaultPrinterId),
                SystemDnsSdName = map.MapFromDicNullable<string?>(src, SystemAttribute.SystemDnsSdName),
                SystemMandatoryPrinterAttributes = map.MapFromDicSetNullable<SystemMandatoryPrinterAttribute[]?>(src, SystemAttribute.SystemMandatoryPrinterAttributes),
                SystemMandatoryRegistrationAttributes = map.MapFromDicSetNullable<SystemMandatoryRegistrationAttribute[]?>(src, SystemAttribute.SystemMandatoryRegistrationAttributes),
                SystemSettableAttributesSupported = map.MapFromDicSetNullable<SystemSettableAttribute[]?>(src, SystemAttribute.SystemSettableAttributesSupported),
                SystemFirmwareName = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemFirmwareName),
                SystemFirmwarePatches = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemFirmwarePatches),
                SystemFirmwareStringVersion = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemFirmwareStringVersion),
                SystemFirmwareVersion = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemFirmwareVersion),
                SystemResidentApplicationName = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemResidentApplicationName),
                SystemResidentApplicationPatches = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemResidentApplicationPatches),
                SystemResidentApplicationStringVersion = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemResidentApplicationStringVersion),
                SystemResidentApplicationVersion = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemResidentApplicationVersion),
                SystemUserApplicationName = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemUserApplicationName),
                SystemUserApplicationPatches = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemUserApplicationPatches),
                SystemUserApplicationStringVersion = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemUserApplicationStringVersion),
                SystemUserApplicationVersion = map.MapFromDicSetNullable<string[]?>(src, SystemAttribute.SystemUserApplicationVersion),
                SystemTimeSourceConfigured = map.MapFromDicNullable<SystemTimeSourceConfigured?>(src, SystemAttribute.SystemTimeSourceConfigured)
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
                    attrs.Add(new IppAttribute(Tag.Enum, SystemAttribute.SystemState, (int)src.SystemAttributes.SystemState.Value));
                if (src.SystemAttributes.SystemStateMessage != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemStateMessage, src.SystemAttributes.SystemStateMessage));
                if (src.SystemAttributes.SystemStateReasons != null)
                    attrs.AddRange(src.SystemAttributes.SystemStateReasons.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.SystemStateReasons, x.Value)));
                if (src.SystemAttributes.SystemStateChangeTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemStateChangeTime, src.SystemAttributes.SystemStateChangeTime.Value));
                if (src.SystemAttributes.SystemStateChangeDateTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.DateTime, SystemAttribute.SystemStateChangeDateTime, src.SystemAttributes.SystemStateChangeDateTime.Value));
                if (src.SystemAttributes.SystemUpTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemUpTime, src.SystemAttributes.SystemUpTime.Value));
                if (src.SystemAttributes.SystemTimeSourceConfigured != null)
                    attrs.Add(new IppAttribute(Tag.Keyword, SystemAttribute.SystemTimeSourceConfigured, src.SystemAttributes.SystemTimeSourceConfigured.Value.Value));
                if (src.SystemAttributes.SystemConfiguredPrinters != null)
                    attrs.AddRange(src.SystemAttributes.SystemConfiguredPrinters.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.SystemConfiguredPrinters)));
                if (src.SystemAttributes.SystemConfiguredResources != null)
                    attrs.AddRange(src.SystemAttributes.SystemConfiguredResources.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.SystemConfiguredResources)));
                if (src.SystemAttributes.PowerLogCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerLogCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerLogCol)));
                if (src.SystemAttributes.PowerStateCapabilitiesCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateCapabilitiesCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerStateCapabilitiesCol)));
                if (src.SystemAttributes.PowerStateCountersCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateCountersCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerStateCountersCol)));
                if (src.SystemAttributes.PowerStateMonitorCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateMonitorCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerStateMonitorCol)));
                if (src.SystemAttributes.PowerStateTransitionsCol != null)
                    attrs.AddRange(src.SystemAttributes.PowerStateTransitionsCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerStateTransitionsCol)));

                if (src.SystemAttributes.SystemUuid != null)
                    attrs.Add(new IppAttribute(Tag.Uri, SystemAttribute.SystemUuid, src.SystemAttributes.SystemUuid.ToString()));
                if (src.SystemAttributes.XriAuthenticationSupported != null)
                    attrs.AddRange(src.SystemAttributes.XriAuthenticationSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.XriAuthenticationSupported, x.ToString())));
                if (src.SystemAttributes.XriSecuritySupported != null)
                    attrs.AddRange(src.SystemAttributes.XriSecuritySupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.XriSecuritySupported, x.ToString())));
                if (src.SystemAttributes.XriUriSchemeSupported != null)
                    attrs.AddRange(src.SystemAttributes.XriUriSchemeSupported.Select(x => new IppAttribute(Tag.UriScheme, SystemAttribute.XriUriSchemeSupported, x.ToString())));

            }

            if (src.SystemDescriptionAttributes != null)
            {
                if (src.SystemDescriptionAttributes.SystemConfigChanges.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemConfigChanges, src.SystemDescriptionAttributes.SystemConfigChanges.Value));
                if (src.SystemDescriptionAttributes.SystemConfiguredPrinters.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemConfiguredPrinters, src.SystemDescriptionAttributes.SystemConfiguredPrinters.Value));
                if (src.SystemDescriptionAttributes.SystemConfiguredResources.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemConfiguredResources, src.SystemDescriptionAttributes.SystemConfiguredResources.Value));

                if (src.SystemDescriptionAttributes.SystemStringsLanguagesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemStringsLanguagesSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.SystemStringsLanguagesSupported, x)));
                if (src.SystemDescriptionAttributes.SystemStringsUri != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemStringsUri.Select(x => new IppAttribute(Tag.Uri, SystemAttribute.SystemStringsUri, x)));
                if (src.SystemDescriptionAttributes.SystemSerialNumber != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemSerialNumber, src.SystemDescriptionAttributes.SystemSerialNumber));
                if (src.SystemDescriptionAttributes.SystemImpressionsCompleted.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemImpressionsCompleted, src.SystemDescriptionAttributes.SystemImpressionsCompleted.Value));
                if (src.SystemDescriptionAttributes.SystemImpressionsCompletedCol.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemImpressionsCompletedCol, src.SystemDescriptionAttributes.SystemImpressionsCompletedCol.Value));
                if (src.SystemDescriptionAttributes.SystemMediaSheetsCompleted.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemMediaSheetsCompleted, src.SystemDescriptionAttributes.SystemMediaSheetsCompleted.Value));
                if (src.SystemDescriptionAttributes.SystemMediaSheetsCompletedCol.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemMediaSheetsCompletedCol, src.SystemDescriptionAttributes.SystemMediaSheetsCompletedCol.Value));
                if (src.SystemDescriptionAttributes.SystemPagesCompleted.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemPagesCompleted, src.SystemDescriptionAttributes.SystemPagesCompleted.Value));
                if (src.SystemDescriptionAttributes.SystemPagesCompletedCol.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemPagesCompletedCol, src.SystemDescriptionAttributes.SystemPagesCompletedCol.Value));
                if (src.SystemDescriptionAttributes.SystemConfigChangeTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemConfigChangeTime, src.SystemDescriptionAttributes.SystemConfigChangeTime.Value));
                if (src.SystemDescriptionAttributes.SystemConfigChangeDateTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.DateTime, SystemAttribute.SystemConfigChangeDateTime, src.SystemDescriptionAttributes.SystemConfigChangeDateTime.Value));
                if (src.SystemDescriptionAttributes.SystemUpTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemUpTime, src.SystemDescriptionAttributes.SystemUpTime.Value));
                if (src.SystemDescriptionAttributes.SystemUuid != null)
                    attrs.Add(new IppAttribute(Tag.Uri, SystemAttribute.SystemUuid, src.SystemDescriptionAttributes.SystemUuid.ToString()));
                if (src.SystemDescriptionAttributes.SystemGeoLocation != null)
                    attrs.Add(new IppAttribute(Tag.Uri, SystemAttribute.SystemGeoLocation, src.SystemDescriptionAttributes.SystemGeoLocation.ToString()));
                if (src.SystemDescriptionAttributes.SystemAssetTag != null)
                    attrs.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.SystemAssetTag, src.SystemDescriptionAttributes.SystemAssetTag));
                if (src.SystemDescriptionAttributes.SystemCurrentTime.HasValue)
                    attrs.Add(new IppAttribute(Tag.DateTime, SystemAttribute.SystemCurrentTime, src.SystemDescriptionAttributes.SystemCurrentTime.Value));
                if (src.SystemDescriptionAttributes.SystemContactCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemContactCol
                        .SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.SystemContactCol)));
                if (src.SystemDescriptionAttributes.SystemServiceContactCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemServiceContactCol
                        .SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.SystemServiceContactCol)));
                if (src.SystemDescriptionAttributes.SystemXriSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemXriSupported
                        .SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.SystemXriSupported)));
                if (src.SystemDescriptionAttributes.IppGetEventLife.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.IppGetEventLife, src.SystemDescriptionAttributes.IppGetEventLife.Value));
                if (src.SystemDescriptionAttributes.CharsetConfigured != null)
                    attrs.Add(new IppAttribute(Tag.Charset, SystemAttribute.CharsetConfigured, src.SystemDescriptionAttributes.CharsetConfigured));
                if (src.SystemDescriptionAttributes.CharsetSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.CharsetSupported.Select(x => new IppAttribute(Tag.Charset, SystemAttribute.CharsetSupported, x)));
                if (src.SystemDescriptionAttributes.DocumentFormatSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.DocumentFormatSupported.Select(x => new IppAttribute(Tag.MimeMediaType, SystemAttribute.DocumentFormatSupported, x)));
                if (src.SystemDescriptionAttributes.GeneratedNaturalLanguageSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.GeneratedNaturalLanguageSupported.Select(x => new IppAttribute(Tag.NaturalLanguage, SystemAttribute.GeneratedNaturalLanguageSupported, x)));
                if (src.SystemDescriptionAttributes.IppFeaturesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.IppFeaturesSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.IppFeaturesSupported, x.Value)));
                if (src.SystemDescriptionAttributes.IppVersionsSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.IppVersionsSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.IppVersionsSupported, x.ToString())));
                if (src.SystemDescriptionAttributes.MultipleDocumentPrintersSupported.HasValue)
                    attrs.Add(new IppAttribute(Tag.Boolean, SystemAttribute.MultipleDocumentPrintersSupported, src.SystemDescriptionAttributes.MultipleDocumentPrintersSupported.Value));
                if (src.SystemDescriptionAttributes.NaturalLanguageConfigured != null)
                    attrs.Add(new IppAttribute(Tag.NaturalLanguage, SystemAttribute.NaturalLanguageConfigured, src.SystemDescriptionAttributes.NaturalLanguageConfigured));
                if (src.SystemDescriptionAttributes.OperationsSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.OperationsSupported.Select(x => new IppAttribute(Tag.Enum, SystemAttribute.OperationsSupported, (int)x)));
                if (src.SystemDescriptionAttributes.OutputDeviceX509TypeSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.OutputDeviceX509TypeSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.OutputDeviceX509TypeSupported, x.Value)));
                if (src.SystemDescriptionAttributes.PowerCalendarPolicyCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PowerCalendarPolicyCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerCalendarPolicyCol)));
                if (src.SystemDescriptionAttributes.PowerEventPolicyCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PowerEventPolicyCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerEventPolicyCol)));
                if (src.SystemDescriptionAttributes.PowerTimeoutPolicyCol != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PowerTimeoutPolicyCol.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(SystemAttribute.PowerTimeoutPolicyCol)));
                if (src.SystemDescriptionAttributes.PrinterCreationAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PrinterCreationAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.PrinterCreationAttributesSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.PrinterServiceTypeSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.PrinterServiceTypeSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.PrinterServiceTypeSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.ResourceFormatSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.ResourceFormatSupported.Select(x => new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormatSupported, x)));
                if (src.SystemDescriptionAttributes.ResourceTypeSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.ResourceTypeSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.ResourceTypeSupported, x.Value)));
                if (src.SystemDescriptionAttributes.ResourceSettableAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.ResourceSettableAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.ResourceSettableAttributesSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.SystemInfo != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemInfo, src.SystemDescriptionAttributes.SystemInfo));
                if (src.SystemDescriptionAttributes.SystemLocation != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemLocation, src.SystemDescriptionAttributes.SystemLocation));
                if (src.SystemDescriptionAttributes.SystemMakeAndModel != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemMakeAndModel, src.SystemDescriptionAttributes.SystemMakeAndModel));
                if (src.SystemDescriptionAttributes.SystemMessageFromOperator != null)
                    attrs.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemMessageFromOperator, src.SystemDescriptionAttributes.SystemMessageFromOperator));
                if (src.SystemDescriptionAttributes.SystemName != null)
                    attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.SystemName, src.SystemDescriptionAttributes.SystemName));
                if (src.SystemDescriptionAttributes.SystemDefaultPrinterId.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.SystemDefaultPrinterId, src.SystemDescriptionAttributes.SystemDefaultPrinterId.Value));
                if (src.SystemDescriptionAttributes.SystemDnsSdName != null)
                    attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.SystemDnsSdName, src.SystemDescriptionAttributes.SystemDnsSdName));
                if (src.SystemDescriptionAttributes.SystemMandatoryPrinterAttributes != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemMandatoryPrinterAttributes.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.SystemMandatoryPrinterAttributes, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.SystemMandatoryRegistrationAttributes != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemMandatoryRegistrationAttributes.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.SystemMandatoryRegistrationAttributes, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.SystemSettableAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemSettableAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.SystemSettableAttributesSupported, map.Map<string>(x))));
                if (src.SystemDescriptionAttributes.NotifyAttributesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyAttributesSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.NotifyAttributesSupported, x)));
                if (src.SystemDescriptionAttributes.NotifyEventsDefault != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyEventsDefault.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.NotifyEventsDefault, x.Value)));
                if (src.SystemDescriptionAttributes.NotifyEventsSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyEventsSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.NotifyEventsSupported, x.Value)));
                if (src.SystemDescriptionAttributes.NotifyLeaseDurationDefault.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.NotifyLeaseDurationDefault, src.SystemDescriptionAttributes.NotifyLeaseDurationDefault.Value));
                if (src.SystemDescriptionAttributes.NotifyLeaseDurationSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyLeaseDurationSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.NotifyLeaseDurationSupported, x)));
                if (src.SystemDescriptionAttributes.NotifyMaxEventsSupported.HasValue)
                    attrs.Add(new IppAttribute(Tag.Integer, SystemAttribute.NotifyMaxEventsSupported, src.SystemDescriptionAttributes.NotifyMaxEventsSupported.Value));
                if (src.SystemDescriptionAttributes.NotifyPullMethodSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifyPullMethodSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.NotifyPullMethodSupported, x.Value)));
                if (src.SystemDescriptionAttributes.NotifySchemesSupported != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.NotifySchemesSupported.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.NotifySchemesSupported, x.ToString())));
                if (src.SystemDescriptionAttributes.SystemFirmwareName != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwareName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.SystemFirmwareName, x)));
                if (src.SystemDescriptionAttributes.SystemFirmwarePatches != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwarePatches.Select(x => new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemFirmwarePatches, x)));
                if (src.SystemDescriptionAttributes.SystemFirmwareStringVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwareStringVersion.Select(x => new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemFirmwareStringVersion, x)));
                if (src.SystemDescriptionAttributes.SystemFirmwareVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemFirmwareVersion.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.SystemFirmwareVersion, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationName != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.SystemResidentApplicationName, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationPatches != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationPatches.Select(x => new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemResidentApplicationPatches, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationStringVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationStringVersion.Select(x => new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemResidentApplicationStringVersion, x)));
                if (src.SystemDescriptionAttributes.SystemResidentApplicationVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemResidentApplicationVersion.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.SystemResidentApplicationVersion, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationName != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationName.Select(x => new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.SystemUserApplicationName, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationPatches != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationPatches.Select(x => new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemUserApplicationPatches, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationStringVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationStringVersion.Select(x => new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.SystemUserApplicationStringVersion, x)));
                if (src.SystemDescriptionAttributes.SystemUserApplicationVersion != null)
                    attrs.AddRange(src.SystemDescriptionAttributes.SystemUserApplicationVersion.Select(x => new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, SystemAttribute.SystemUserApplicationVersion, x)));
                if (src.SystemDescriptionAttributes.SystemTimeSourceConfigured != null)
                    attrs.Add(new IppAttribute(Tag.Keyword, SystemAttribute.SystemTimeSourceConfigured, src.SystemDescriptionAttributes.SystemTimeSourceConfigured.Value.Value));
            }

            if (attrs.Count > 0)
                dst.SystemAttributes.Add(attrs);

            return dst;
        });
    }
}
