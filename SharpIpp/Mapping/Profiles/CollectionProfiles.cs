using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class CollectionProfiles : IProfile
{
    private static bool IsOutOfBandNoValue(IDictionary<string, IppAttribute[]> src)
    {
        return src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand();
    }

    public void CreateMaps(IMapperConstructor mapper)
    {
        // Cover
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, Cover>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<Cover>();

            var dst = new Cover
            {
                CoverType = map.MapFromDicNullable<CoverType?>(src, nameof(Cover.CoverType).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(Cover.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<Cover, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "cover", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CoverType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(Cover.CoverType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.CoverType.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(Cover.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // InsertSheet
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, InsertSheet>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<InsertSheet>();

            var dst = new InsertSheet
            {
                InsertAfterPageNumber = map.MapFromDicNullable<int?>(src, nameof(InsertSheet.InsertAfterPageNumber).ConvertCamelCaseToKebabCase()),
                InsertCount = map.MapFromDicNullable<int?>(src, nameof(InsertSheet.InsertCount).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<InsertSheet, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "insert-sheet", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.InsertAfterPageNumber.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(InsertSheet.InsertAfterPageNumber).ConvertCamelCaseToKebabCase(), src.InsertAfterPageNumber.Value));
            if (src.InsertCount.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(InsertSheet.InsertCount).ConvertCamelCaseToKebabCase(), src.InsertCount.Value));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // JobAccountingSheets
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobAccountingSheets>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<JobAccountingSheets>();

            var dst = new JobAccountingSheets
            {
                JobAccountingOutputBin = map.MapFromDicNullable<OutputBin?>(src, nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase()),
                JobAccountingSheetsType = map.MapFromDicNullable<JobSheetsType?>(src, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<JobAccountingSheets, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobAccountingSheets, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobAccountingOutputBin != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.JobAccountingOutputBin).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobAccountingOutputBin)));
            if (src.JobAccountingSheetsType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.JobAccountingSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobAccountingSheetsType.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobAccountingSheets.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobAccountingSheets.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // JobErrorSheet
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobErrorSheet>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<JobErrorSheet>();

            var dst = new JobErrorSheet
            {
                JobErrorSheetType = map.MapFromDicNullable<JobSheetsType?>(src, nameof(JobErrorSheet.JobErrorSheetType).ConvertCamelCaseToKebabCase()),
                JobErrorSheetWhen = map.MapFromDicNullable<JobErrorSheetWhen?>(src, nameof(JobErrorSheet.JobErrorSheetWhen).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<JobErrorSheet, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobErrorSheet, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobErrorSheetType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.JobErrorSheetType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobErrorSheetType.Value)));
            if (src.JobErrorSheetWhen.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.JobErrorSheetWhen).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobErrorSheetWhen.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobErrorSheet.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobErrorSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        // SeparatorSheets
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SeparatorSheets>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<SeparatorSheets>();

            var dst = new SeparatorSheets
            {
                Media = map.MapFromDicNullable<Media?>(src, nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase()),
                SeparatorSheetsType = map.MapFromDicSetNullable<SeparatorSheetsType[]?>(src, nameof(SeparatorSheets.SeparatorSheetsType).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<SeparatorSheets, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.SeparatorSheets, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()));
            if (src.SeparatorSheetsType != null)
                attributes.AddRange(src.SeparatorSheetsType.Select(x => new IppAttribute(Tag.Keyword, nameof(SeparatorSheets.SeparatorSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(x))));
            return attributes;
        });

        // SystemContact
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemContact>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<SystemContact>();

            return new SystemContact
            {
                ContactName = map.MapFromDicNullable<string?>(src, "contact-name"),
                ContactUri = map.MapFromDicNullable<Uri?>(src, "contact-uri"),
                ContactVcard = map.MapFromDicSetNullable<string[]?>(src, "contact-vcard")
            };
        });

        mapper.CreateMap<SystemContact, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "system-contact-col", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ContactName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "contact-name", src.ContactName));
            if (src.ContactUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "contact-uri", src.ContactUri.ToString()));
            if (src.ContactVcard != null)
                attributes.AddRange(src.ContactVcard.Select(x => new IppAttribute(Tag.TextWithoutLanguage, "contact-vcard", x)));
            return attributes;
        });

        // SystemXri
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemXri>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<SystemXri>();

            return new SystemXri
            {
                XriUri = map.MapFromDicNullable<Uri?>(src, "xri-uri"),
                XriAuthentication = map.MapFromDicNullable<string?>(src, "xri-authentication"),
                XriSecurity = map.MapFromDicNullable<string?>(src, "xri-security")
            };
        });

        mapper.CreateMap<SystemXri, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.SystemXriSupported, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.XriUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "xri-uri", src.XriUri.ToString()));
            if (src.XriAuthentication != null)
                attributes.Add(new IppAttribute(Tag.Keyword, "xri-authentication", src.XriAuthentication));
            if (src.XriSecurity != null)
                attributes.Add(new IppAttribute(Tag.Keyword, "xri-security", src.XriSecurity));
            return attributes;
        });

        // SystemConfiguredPrinter
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemConfiguredPrinter>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<SystemConfiguredPrinter>();

            var dst = new SystemConfiguredPrinter
            {
                PrinterId = map.MapFromDicNullable<int?>(src, JobAttribute.PrinterId),
                PrinterInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterInfo),
                PrinterIsAcceptingJobs = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PrinterIsAcceptingJobs),
                PrinterName = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterName),
                PrinterServiceType = map.MapFromDicNullable<PrinterServiceType?>(src, PrinterAttribute.PrinterServiceType),
                PrinterState = map.MapFromDicNullable<PrinterState?>(src, PrinterAttribute.PrinterState),
                PrinterStateReasons = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterStateReasons)
            };
            if (src.ContainsKey(PrinterAttribute.PrinterXriSupported))
                dst.PrinterXriSupported = src[PrinterAttribute.PrinterXriSupported].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            return dst;
        });

        mapper.CreateMap<SystemConfiguredPrinter, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.SystemConfiguredPrinters, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.PrinterId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, JobAttribute.PrinterId, src.PrinterId.Value));
            if (src.PrinterInfo != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterInfo, src.PrinterInfo));
            if (src.PrinterIsAcceptingJobs.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, PrinterAttribute.PrinterIsAcceptingJobs, src.PrinterIsAcceptingJobs.Value));
            if (src.PrinterName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, PrinterAttribute.PrinterName, src.PrinterName));
            if (src.PrinterServiceType != null)
                attributes.Add(new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, map.Map<string>(src.PrinterServiceType.Value)));
            if (src.PrinterState.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, PrinterAttribute.PrinterState, (int)src.PrinterState.Value));
            if (src.PrinterStateReasons != null)
                attributes.AddRange(src.PrinterStateReasons.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterStateReasons, x)));
            if (src.PrinterXriSupported != null)
                attributes.AddRange(src.PrinterXriSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.PrinterXriSupported)));
            return attributes;
        });

        // SystemConfiguredResource
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemConfiguredResource>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<SystemConfiguredResource>();

            return new SystemConfiguredResource
            {
                ResourceFormat = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceFormat),
                ResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.ResourceId),
                ResourceInfo = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceInfo),
                ResourceName = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceName),
                ResourceState = map.MapFromDicNullable<ResourceState?>(src, SystemAttribute.ResourceState),
                ResourceStateReasons = map.MapFromDicSetNullable<ResourceStateReason[]?>(src, SystemAttribute.ResourceStateReasons),
                ResourceType = map.MapFromDicNullable<string?>(src, SystemAttribute.ResourceType)
            };
        });

        mapper.CreateMap<SystemConfiguredResource, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.SystemConfiguredResources, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ResourceFormat != null)
                attributes.Add(new IppAttribute(Tag.MimeMediaType, SystemAttribute.ResourceFormat, src.ResourceFormat));
            if (src.ResourceId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, src.ResourceId.Value));
            if (src.ResourceInfo != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, SystemAttribute.ResourceInfo, src.ResourceInfo));
            if (src.ResourceName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, src.ResourceName));
            if (src.ResourceState.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, SystemAttribute.ResourceState, (int)src.ResourceState.Value));
            if (src.ResourceStateReasons != null)
                attributes.AddRange(src.ResourceStateReasons.Select(x => new IppAttribute(Tag.Keyword, SystemAttribute.ResourceStateReasons, x.ToString())));
            if (src.ResourceType != null)
                attributes.Add(new IppAttribute(Tag.Keyword, SystemAttribute.ResourceType, src.ResourceType));
            return attributes;
        });

        // PowerLogEntry mapping
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerLogEntry>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerLogEntry>();

            return new PowerLogEntry
            {
                LogId = map.MapFromDicNullable<int?>(src, "log-id"),
                PowerState = map.MapFromDicNullable<PowerState?>(src, "power-state"),
                PowerStateDateTime = map.MapFromDicNullable<DateTimeOffset?>(src, "power-state-date-time"),
                PowerStateMessage = map.MapFromDicNullable<string?>(src, "power-state-message")
            };
        });

        mapper.CreateMap<PowerLogEntry, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerLogCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.LogId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "log-id", src.LogId.Value));
            if (src.PowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "power-state", map.Map<string>(src.PowerState.Value)));
            if (src.PowerStateDateTime.HasValue)
                attributes.Add(new IppAttribute(Tag.DateTime, "power-state-date-time", src.PowerStateDateTime.Value));
            if (src.PowerStateMessage != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", src.PowerStateMessage));
            return attributes;
        });

        // PowerStateCapability
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateCapability>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerStateCapability>();

            return new PowerStateCapability
            {
                CanAcceptJobs = map.MapFromDicNullable<bool?>(src, "can-accept-jobs"),
                CanProcessJobs = map.MapFromDicNullable<bool?>(src, "can-process-jobs"),
                PowerActiveWatts = map.MapFromDicNullable<int?>(src, "power-active-watts"),
                PowerInactiveWatts = map.MapFromDicNullable<int?>(src, "power-inactive-watts"),
                PowerState = map.MapFromDicNullable<PowerState?>(src, "power-state")
            };
        });

        mapper.CreateMap<PowerStateCapability, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateCapabilitiesCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CanAcceptJobs.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "can-accept-jobs", src.CanAcceptJobs.Value));
            if (src.CanProcessJobs.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "can-process-jobs", src.CanProcessJobs.Value));
            if (src.PowerActiveWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "power-active-watts", src.PowerActiveWatts.Value));
            if (src.PowerInactiveWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "power-inactive-watts", src.PowerInactiveWatts.Value));
            if (src.PowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "power-state", map.Map<string>(src.PowerState.Value)));
            return attributes;
        });

        // PowerStateCounter
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateCounter>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerStateCounter>();

            return new PowerStateCounter
            {
                HibernateTransitions = map.MapFromDicNullable<int?>(src, "hibernate-transitions"),
                OnTransitions = map.MapFromDicNullable<int?>(src, "on-transitions"),
                StandbyTransitions = map.MapFromDicNullable<int?>(src, "standby-transitions"),
                SuspendTransitions = map.MapFromDicNullable<int?>(src, "suspend-transitions")
            };
        });

        mapper.CreateMap<PowerStateCounter, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateCountersCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.HibernateTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "hibernate-transitions", src.HibernateTransitions.Value));
            if (src.OnTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "on-transitions", src.OnTransitions.Value));
            if (src.StandbyTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "standby-transitions", src.StandbyTransitions.Value));
            if (src.SuspendTransitions.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "suspend-transitions", src.SuspendTransitions.Value));
            return attributes;
        });

        // PowerStateMonitor
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateMonitor>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerStateMonitor>();

            var dst = new PowerStateMonitor
            {
                CurrentMonthKwh = map.MapFromDicNullable<int?>(src, "current-month-kwh"),
                CurrentWatts = map.MapFromDicNullable<int?>(src, "current-watts"),
                LifetimeKwh = map.MapFromDicNullable<int?>(src, "lifetime-kwh"),
                MetersAreActual = map.MapFromDicNullable<bool?>(src, "meters-are-actual"),
                PowerState = map.MapFromDicNullable<PowerState?>(src, "power-state"),
                PowerStateMessage = map.MapFromDicNullable<string?>(src, "power-state-message"),
                PowerUsageIsRmsWatts = map.MapFromDicNullable<bool?>(src, "power-usage-is-rms-watts")
            };
            if (src.ContainsKey("valid-request-power-state"))
                dst.ValidRequestPowerStates = src["valid-request-power-state"].Select(x => (IppOperation)Enum.Parse(typeof(IppOperation), x.Value.ToString()!)).ToArray();
            return dst;
        });

        mapper.CreateMap<PowerStateMonitor, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateMonitorCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CurrentMonthKwh.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "current-month-kwh", src.CurrentMonthKwh.Value));
            if (src.CurrentWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "current-watts", src.CurrentWatts.Value));
            if (src.LifetimeKwh.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "lifetime-kwh", src.LifetimeKwh.Value));
            if (src.MetersAreActual.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "meters-are-actual", src.MetersAreActual.Value));
            if (src.PowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "power-state", map.Map<string>(src.PowerState.Value)));
            if (src.PowerStateMessage != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", src.PowerStateMessage));
            if (src.PowerUsageIsRmsWatts.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "power-usage-is-rms-watts", src.PowerUsageIsRmsWatts.Value));
            if (src.ValidRequestPowerStates != null)
                attributes.AddRange(src.ValidRequestPowerStates.Select(x => new IppAttribute(Tag.Enum, "valid-request-power-state", (int)x)));
            return attributes;
        });

        // PowerStateTransition
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerStateTransition>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerStateTransition>();

            return new PowerStateTransition
            {
                EndPowerState = map.MapFromDicNullable<PowerState?>(src, "end-power-state"),
                StartPowerState = map.MapFromDicNullable<PowerState?>(src, "start-power-state"),
                StateTransitionSeconds = map.MapFromDicNullable<int?>(src, "state-transition-seconds")
            };
        });

        mapper.CreateMap<PowerStateTransition, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerStateTransitionsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.EndPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "end-power-state", map.Map<string>(src.EndPowerState.Value)));
            if (src.StartPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "start-power-state", map.Map<string>(src.StartPowerState.Value)));
            if (src.StateTransitionSeconds.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "state-transition-seconds", src.StateTransitionSeconds.Value));
            return attributes;
        });

        // PowerCalendarPolicy
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerCalendarPolicy>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerCalendarPolicy>();

            return new PowerCalendarPolicy
            {
                CalendarId = map.MapFromDicNullable<int?>(src, "calendar-id"),
                DayOfMonth = map.MapFromDicNullable<int?>(src, "day-of-month"),
                DayOfWeek = map.MapFromDicNullable<int?>(src, "day-of-week"),
                Hour = map.MapFromDicNullable<int?>(src, "hour"),
                Minute = map.MapFromDicNullable<int?>(src, "minute"),
                Month = map.MapFromDicNullable<int?>(src, "month"),
                RequestPowerState = map.MapFromDicNullable<PowerState?>(src, "request-power-state"),
                RunOnce = map.MapFromDicNullable<bool?>(src, "run-once")
            };
        });

        mapper.CreateMap<PowerCalendarPolicy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerCalendarPolicyCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CalendarId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "calendar-id", src.CalendarId.Value));
            if (src.DayOfMonth.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "day-of-month", src.DayOfMonth.Value));
            if (src.DayOfWeek.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "day-of-week", src.DayOfWeek.Value));
            if (src.Hour.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "hour", src.Hour.Value));
            if (src.Minute.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "minute", src.Minute.Value));
            if (src.Month.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "month", src.Month.Value));
            if (src.RequestPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "request-power-state", map.Map<string>(src.RequestPowerState.Value)));
            if (src.RunOnce.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, "run-once", src.RunOnce.Value));
            return attributes;
        });

        // PowerEventPolicy
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerEventPolicy>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerEventPolicy>();

            return new PowerEventPolicy
            {
                EventId = map.MapFromDicNullable<int?>(src, "event-id"),
                EventName = map.MapFromDicNullable<string?>(src, "event-name"),
                RequestPowerState = map.MapFromDicNullable<PowerState?>(src, "request-power-state")
            };
        });

        mapper.CreateMap<PowerEventPolicy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerEventPolicyCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.EventId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "event-id", src.EventId.Value));
            if (src.EventName != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "event-name", src.EventName));
            if (src.RequestPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "request-power-state", map.Map<string>(src.RequestPowerState.Value)));
            return attributes;
        });

        // PowerTimeoutPolicy
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PowerTimeoutPolicy>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PowerTimeoutPolicy>();

            return new PowerTimeoutPolicy
            {
                RequestPowerState = map.MapFromDicNullable<PowerState?>(src, "request-power-state"),
                StartPowerState = map.MapFromDicNullable<PowerState?>(src, "start-power-state"),
                TimeoutId = map.MapFromDicNullable<int?>(src, "timeout-id"),
                TimeoutPredicate = map.MapFromDicNullable<string?>(src, "timeout-predicate"),
                TimeoutSeconds = map.MapFromDicNullable<int?>(src, "timeout-seconds")
            };
        });

        mapper.CreateMap<PowerTimeoutPolicy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.PowerTimeoutPolicyCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.RequestPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "request-power-state", map.Map<string>(src.RequestPowerState.Value)));
            if (src.StartPowerState.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, "start-power-state", map.Map<string>(src.StartPowerState.Value)));
            if (src.TimeoutId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "timeout-id", src.TimeoutId.Value));
            if (src.TimeoutPredicate != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "timeout-predicate", src.TimeoutPredicate));
            if (src.TimeoutSeconds.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, "timeout-seconds", src.TimeoutSeconds.Value));
            return attributes;
        });

        // Additional Power* mapping can be added here.

        // ClientInfo
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ClientInfo>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<ClientInfo>();

            var dst = new ClientInfo
            {
                ClientName = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientName).ConvertCamelCaseToKebabCase()),
                ClientPatches = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientPatches).ConvertCamelCaseToKebabCase()),
                ClientStringVersion = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientStringVersion).ConvertCamelCaseToKebabCase()),
                ClientVersion = map.MapFromDicNullable<string?>(src, nameof(ClientInfo.ClientVersion).ConvertCamelCaseToKebabCase()),
            };

            var clientType = map.MapFromDicNullable<int?>(src, nameof(ClientInfo.ClientType).ConvertCamelCaseToKebabCase());
            if (clientType.HasValue)
                dst.ClientType = (ClientType)clientType.Value;

            return dst;
        });
        mapper.CreateMap<ClientInfo, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.ClientInfo, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ClientName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(ClientInfo.ClientName).ConvertCamelCaseToKebabCase(), src.ClientName));
            if (src.ClientPatches != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(ClientInfo.ClientPatches).ConvertCamelCaseToKebabCase(), src.ClientPatches));
            if (src.ClientStringVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(ClientInfo.ClientStringVersion).ConvertCamelCaseToKebabCase(), src.ClientStringVersion));
            if (src.ClientType.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, nameof(ClientInfo.ClientType).ConvertCamelCaseToKebabCase(), (int)src.ClientType.Value));
            if (src.ClientVersion != null)
                attributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, nameof(ClientInfo.ClientVersion).ConvertCamelCaseToKebabCase(), src.ClientVersion));
            return attributes;
        });

        // DocumentFormatDetails
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentFormatDetails>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<DocumentFormatDetails>();

            var dst = new DocumentFormatDetails
            {
                DocumentSourceApplicationName = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceApplicationName).ConvertCamelCaseToKebabCase()),
                DocumentSourceApplicationVersion = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceApplicationVersion).ConvertCamelCaseToKebabCase()),
                DocumentSourceOsName = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceOsName).ConvertCamelCaseToKebabCase()),
                DocumentSourceOsVersion = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceOsVersion).ConvertCamelCaseToKebabCase()),
            };
            return dst;
        });
        mapper.CreateMap<DocumentFormatDetails, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.DocumentFormatDetails, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DocumentSourceApplicationName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceApplicationName).ConvertCamelCaseToKebabCase(), src.DocumentSourceApplicationName));
            if (src.DocumentSourceApplicationVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceApplicationVersion).ConvertCamelCaseToKebabCase(), src.DocumentSourceApplicationVersion));
            if (src.DocumentSourceOsName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceOsName).ConvertCamelCaseToKebabCase(), src.DocumentSourceOsName));
            if (src.DocumentSourceOsVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceOsVersion).ConvertCamelCaseToKebabCase(), src.DocumentSourceOsVersion));
            return attributes;
        });

        // JobCounter
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobCounter>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<JobCounter>();

            return new JobCounter
            {
                Blank = map.MapFromDicNullable<int?>(src, nameof(JobCounter.Blank).ConvertCamelCaseToKebabCase()),
                BlankTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.BlankTwoSided).ConvertCamelCaseToKebabCase()),
                FullColor = map.MapFromDicNullable<int?>(src, nameof(JobCounter.FullColor).ConvertCamelCaseToKebabCase()),
                FullColorTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.FullColorTwoSided).ConvertCamelCaseToKebabCase()),
                HighlightColor = map.MapFromDicNullable<int?>(src, nameof(JobCounter.HighlightColor).ConvertCamelCaseToKebabCase()),
                HighlightColorTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.HighlightColorTwoSided).ConvertCamelCaseToKebabCase()),
                Monochrome = map.MapFromDicNullable<int?>(src, nameof(JobCounter.Monochrome).ConvertCamelCaseToKebabCase()),
                MonochromeTwoSided = map.MapFromDicNullable<int?>(src, nameof(JobCounter.MonochromeTwoSided).ConvertCamelCaseToKebabCase()),
            };
        });
        mapper.CreateMap<JobCounter, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "job-counter", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.Blank.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.Blank).ConvertCamelCaseToKebabCase(), src.Blank.Value));
            if (src.BlankTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.BlankTwoSided).ConvertCamelCaseToKebabCase(), src.BlankTwoSided.Value));
            if (src.FullColor.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.FullColor).ConvertCamelCaseToKebabCase(), src.FullColor.Value));
            if (src.FullColorTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.FullColorTwoSided).ConvertCamelCaseToKebabCase(), src.FullColorTwoSided.Value));
            if (src.HighlightColor.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.HighlightColor).ConvertCamelCaseToKebabCase(), src.HighlightColor.Value));
            if (src.HighlightColorTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.HighlightColorTwoSided).ConvertCamelCaseToKebabCase(), src.HighlightColorTwoSided.Value));
            if (src.Monochrome.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.Monochrome).ConvertCamelCaseToKebabCase(), src.Monochrome.Value));
            if (src.MonochromeTwoSided.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(JobCounter.MonochromeTwoSided).ConvertCamelCaseToKebabCase(), src.MonochromeTwoSided.Value));
            return attributes;
        });

        // JobSheetsCol
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobSheetsCol>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<JobSheetsCol>();

            var dst = new JobSheetsCol
            {
                JobSheets = map.MapFromDicNullable<JobSheets?>(src, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase()),
            };

            if (src.ContainsKey(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());

            return dst;
        });
        mapper.CreateMap<JobSheetsCol, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobSheetsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobSheets.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobSheetsCol.JobSheets).ConvertCamelCaseToKebabCase(), map.Map<string>(src.JobSheets.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobSheetsCol.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(JobSheetsCol.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ProofPrint>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<ProofPrint>();

            var dst = new ProofPrint
            {
                ProofPrintCopies = map.MapFromDicNullable<int?>(src, nameof(ProofPrint.ProofPrintCopies).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<Media?>(src, nameof(ProofPrint.Media).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<ProofPrint, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.ProofPrint, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ProofPrintCopies.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(ProofPrint.ProofPrintCopies).ConvertCamelCaseToKebabCase(), src.ProofPrintCopies.Value));
            if (src.Media != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(ProofPrint.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobStorage>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<JobStorage>();

            return new JobStorage
            {
                JobStorageAccess = map.MapFromDicNullable<string?>(src, nameof(JobStorage.JobStorageAccess).ConvertCamelCaseToKebabCase()),
                JobStorageDisposition = map.MapFromDicNullable<string?>(src, nameof(JobStorage.JobStorageDisposition).ConvertCamelCaseToKebabCase()),
                JobStorageGroup = map.MapFromDicNullable<string?>(src, nameof(JobStorage.JobStorageGroup).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<JobStorage, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.JobStorage, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.JobStorageAccess != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobStorage.JobStorageAccess).ConvertCamelCaseToKebabCase(), src.JobStorageAccess));
            if (src.JobStorageDisposition != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(JobStorage.JobStorageDisposition).ConvertCamelCaseToKebabCase(), src.JobStorageDisposition));
            if (src.JobStorageGroup != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(JobStorage.JobStorageGroup).ConvertCamelCaseToKebabCase(), src.JobStorageGroup));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentAccess>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<DocumentAccess>();

            return new DocumentAccess
            {
                AccessOAuthToken = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessOAuthToken).ConvertCamelCaseToKebabCase()),
                AccessOAuthUri = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessOAuthUri).ConvertCamelCaseToKebabCase()),
                AccessPassword = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessPassword).ConvertCamelCaseToKebabCase()),
                AccessPin = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessPin).ConvertCamelCaseToKebabCase()),
                AccessUserName = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessUserName).ConvertCamelCaseToKebabCase()),
                AccessX509Certificate = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessX509Certificate).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<DocumentAccess, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.DocumentAccess, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.AccessOAuthToken != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessOAuthToken).ConvertCamelCaseToKebabCase(), src.AccessOAuthToken));
            if (src.AccessOAuthUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, nameof(DocumentAccess.AccessOAuthUri).ConvertCamelCaseToKebabCase(), src.AccessOAuthUri));
            if (src.AccessPassword != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessPassword).ConvertCamelCaseToKebabCase(), src.AccessPassword));
            if (src.AccessPin != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessPin).ConvertCamelCaseToKebabCase(), src.AccessPin));
            if (src.AccessUserName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentAccess.AccessUserName).ConvertCamelCaseToKebabCase(), src.AccessUserName));
            if (src.AccessX509Certificate != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessX509Certificate).ConvertCamelCaseToKebabCase(), src.AccessX509Certificate));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CoverSheetInfo>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<CoverSheetInfo>();

            return new CoverSheetInfo
            {
                FromName = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.FromName).ConvertCamelCaseToKebabCase()),
                Logo = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.Logo).ConvertCamelCaseToKebabCase()),
                Message = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.Message).ConvertCamelCaseToKebabCase()),
                OrganizationName = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.OrganizationName).ConvertCamelCaseToKebabCase()),
                Subject = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.Subject).ConvertCamelCaseToKebabCase()),
                ToName = map.MapFromDicNullable<string?>(src, nameof(CoverSheetInfo.ToName).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<CoverSheetInfo, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.CoverSheetInfo, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.FromName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(CoverSheetInfo.FromName).ConvertCamelCaseToKebabCase(), src.FromName));
            if (src.Logo != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Logo).ConvertCamelCaseToKebabCase(), src.Logo));
            if (src.Message != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Message).ConvertCamelCaseToKebabCase(), src.Message));
            if (src.OrganizationName != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.OrganizationName).ConvertCamelCaseToKebabCase(), src.OrganizationName));
            if (src.Subject != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(CoverSheetInfo.Subject).ConvertCamelCaseToKebabCase(), src.Subject));
            if (src.ToName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(CoverSheetInfo.ToName).ConvertCamelCaseToKebabCase(), src.ToName));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DestinationUri>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<DestinationUri>();

            return new DestinationUri
            {
                DestinationUriValue = map.MapFromDicNullable<string?>(src, "destination-uri"),
                PostDialString = map.MapFromDicNullable<string?>(src, nameof(DestinationUri.PostDialString).ConvertCamelCaseToKebabCase()),
                PreDialString = map.MapFromDicNullable<string?>(src, nameof(DestinationUri.PreDialString).ConvertCamelCaseToKebabCase()),
                T33Subaddress = map.MapFromDicNullable<string?>(src, nameof(DestinationUri.T33Subaddress).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<DestinationUri, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.DestinationUris, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DestinationUriValue != null) attributes.Add(new IppAttribute(Tag.Uri, "destination-uri", src.DestinationUriValue));
            if (src.PostDialString != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DestinationUri.PostDialString).ConvertCamelCaseToKebabCase(), src.PostDialString));
            if (src.PreDialString != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DestinationUri.PreDialString).ConvertCamelCaseToKebabCase(), src.PreDialString));
            if (src.T33Subaddress != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DestinationUri.T33Subaddress).ConvertCamelCaseToKebabCase(), src.T33Subaddress));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OutputAttributes>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<OutputAttributes>();

            return new OutputAttributes
            {
                NoiseRemoval = map.MapFromDicNullable<bool?>(src, nameof(OutputAttributes.NoiseRemoval).ConvertCamelCaseToKebabCase()),
                OutputCompressionQualityFactor = map.MapFromDicNullable<int?>(src, nameof(OutputAttributes.OutputCompressionQualityFactor).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<OutputAttributes, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.OutputAttributes, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.NoiseRemoval.HasValue) attributes.Add(new IppAttribute(Tag.Boolean, nameof(OutputAttributes.NoiseRemoval).ConvertCamelCaseToKebabCase(), src.NoiseRemoval.Value));
            if (src.OutputCompressionQualityFactor.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(OutputAttributes.OutputCompressionQualityFactor).ConvertCamelCaseToKebabCase(), src.OutputCompressionQualityFactor.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, Material>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<Material>();

            return new Material
            {
                MaterialAmount = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialAmount).ConvertCamelCaseToKebabCase()),
                MaterialColor = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialColor).ConvertCamelCaseToKebabCase()),
                MaterialDiameter = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialDiameter).ConvertCamelCaseToKebabCase()),
            MaterialFillDensity = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialFillDensity).ConvertCamelCaseToKebabCase()),
            MaterialKey = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialKey).ConvertCamelCaseToKebabCase()),
            MaterialName = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialName).ConvertCamelCaseToKebabCase()),
            MaterialPurpose = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialPurpose).ConvertCamelCaseToKebabCase()),
            MaterialRate = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialRate).ConvertCamelCaseToKebabCase()),
            MaterialRateUnits = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialRateUnits).ConvertCamelCaseToKebabCase()),
            MaterialShellThickness = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialShellThickness).ConvertCamelCaseToKebabCase()),
            MaterialTemperature = map.MapFromDicNullable<int?>(src, nameof(Material.MaterialTemperature).ConvertCamelCaseToKebabCase()),
            MaterialType = map.MapFromDicNullable<string?>(src, nameof(Material.MaterialType).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<Material, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.MaterialsCol, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.MaterialAmount.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialAmount).ConvertCamelCaseToKebabCase(), src.MaterialAmount.Value));
            if (src.MaterialColor != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(Material.MaterialColor).ConvertCamelCaseToKebabCase(), src.MaterialColor));
            if (src.MaterialDiameter.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialDiameter).ConvertCamelCaseToKebabCase(), src.MaterialDiameter.Value));
            if (src.MaterialFillDensity.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialFillDensity).ConvertCamelCaseToKebabCase(), src.MaterialFillDensity.Value));
            if (src.MaterialKey != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialKey).ConvertCamelCaseToKebabCase(), src.MaterialKey));
            if (src.MaterialName != null) attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(Material.MaterialName).ConvertCamelCaseToKebabCase(), src.MaterialName));
            if (src.MaterialPurpose != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialPurpose).ConvertCamelCaseToKebabCase(), src.MaterialPurpose));
            if (src.MaterialRate.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialRate).ConvertCamelCaseToKebabCase(), src.MaterialRate.Value));
            if (src.MaterialRateUnits != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialRateUnits).ConvertCamelCaseToKebabCase(), src.MaterialRateUnits));
            if (src.MaterialShellThickness.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialShellThickness).ConvertCamelCaseToKebabCase(), src.MaterialShellThickness.Value));
            if (src.MaterialTemperature.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(Material.MaterialTemperature).ConvertCamelCaseToKebabCase(), src.MaterialTemperature.Value));
            if (src.MaterialType != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(Material.MaterialType).ConvertCamelCaseToKebabCase(), src.MaterialType));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintAccuracy>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PrintAccuracy>();

            return new PrintAccuracy
            {
                AccuracyUnits = map.MapFromDicNullable<string?>(src, nameof(PrintAccuracy.AccuracyUnits).ConvertCamelCaseToKebabCase()),
                XAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.XAccuracy).ConvertCamelCaseToKebabCase()),
            YAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.YAccuracy).ConvertCamelCaseToKebabCase()),
            ZAccuracy = map.MapFromDicNullable<int?>(src, nameof(PrintAccuracy.ZAccuracy).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<PrintAccuracy, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.PrintAccuracy, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.AccuracyUnits != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(PrintAccuracy.AccuracyUnits).ConvertCamelCaseToKebabCase(), src.AccuracyUnits));
            if (src.XAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.XAccuracy).ConvertCamelCaseToKebabCase(), src.XAccuracy.Value));
            if (src.YAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.YAccuracy).ConvertCamelCaseToKebabCase(), src.YAccuracy.Value));
            if (src.ZAccuracy.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintAccuracy.ZAccuracy).ConvertCamelCaseToKebabCase(), src.ZAccuracy.Value));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintObject>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<PrintObject>();

            return new PrintObject
            {
                DocumentNumber = map.MapFromDicNullable<int?>(src, nameof(PrintObject.DocumentNumber).ConvertCamelCaseToKebabCase()),
                PrintObjectsSource = map.MapFromDicNullable<string?>(src, "print-objects-source"),
            TransformationMatrix = map.MapFromDicSetNullable<int[]?>(src, nameof(PrintObject.TransformationMatrix).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<PrintObject, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.PrintObjects, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DocumentNumber.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(PrintObject.DocumentNumber).ConvertCamelCaseToKebabCase(), src.DocumentNumber.Value));
            if (src.PrintObjectsSource != null) attributes.Add(new IppAttribute(Tag.Uri, "print-objects-source", src.PrintObjectsSource));
            if (src.TransformationMatrix != null)
                attributes.AddRange(src.TransformationMatrix.Select(x => new IppAttribute(Tag.Integer, nameof(PrintObject.TransformationMatrix).ConvertCamelCaseToKebabCase(), x)));
            return attributes;
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, OverrideInstruction>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<OverrideInstruction>();

            return new OverrideInstruction
            {
                Pages = map.MapFromDicNullable<string?>(src, nameof(OverrideInstruction.Pages).ConvertCamelCaseToKebabCase()),
                DocumentNumbers = map.MapFromDicSetNullable<int[]?>(src, nameof(OverrideInstruction.DocumentNumbers).ConvertCamelCaseToKebabCase()),
            DocumentCopies = map.MapFromDicNullable<int?>(src, nameof(OverrideInstruction.DocumentCopies).ConvertCamelCaseToKebabCase())
            };
        });
        mapper.CreateMap<OverrideInstruction, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.Overrides, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.Pages != null) attributes.Add(new IppAttribute(Tag.Keyword, nameof(OverrideInstruction.Pages).ConvertCamelCaseToKebabCase(), src.Pages));
            if (src.DocumentNumbers != null) attributes.AddRange(src.DocumentNumbers.Select(x => new IppAttribute(Tag.Integer, nameof(OverrideInstruction.DocumentNumbers).ConvertCamelCaseToKebabCase(), x)));
            if (src.DocumentCopies.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(OverrideInstruction.DocumentCopies).ConvertCamelCaseToKebabCase(), src.DocumentCopies.Value));
            return attributes;
        });
    }
}
