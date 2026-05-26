using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class SystemConfiguredPrinterProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemConfiguredPrinter>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<SystemConfiguredPrinter>();

            var dst = new SystemConfiguredPrinter
            {
                PrinterId = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterId),
                PrinterInfo = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterInfo),
                PrinterIsAcceptingJobs = map.MapFromDicNullable<bool?>(src, IppAttributeNames.PrinterIsAcceptingJobs),
                PrinterName = map.MapFromDicNullable<string?>(src, IppAttributeNames.PrinterName),
                PrinterServiceType = map.MapFromDicNullable<PrinterServiceType?>(src, IppAttributeNames.PrinterServiceType),
                PrinterState = map.MapFromDicNullable<PrinterState?>(src, IppAttributeNames.PrinterState),
                PrinterStateReasons = map.MapFromDicSetNullable<PrinterStateReason[]?>(src, IppAttributeNames.PrinterStateReasons)
            };
            if (src.ContainsKey(IppAttributeNames.PrinterXriSupported))
                dst.PrinterXriSupported = src[IppAttributeNames.PrinterXriSupported].GroupBegCollection().Select(x => map.Map<SystemXri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            return dst;
        });

        mapper.CreateMap<SystemConfiguredPrinter, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.SystemConfiguredPrinters, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.PrinterId.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PrinterId, src.PrinterId.Value));
            if (src.PrinterInfo != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterInfo, src.PrinterInfo));
            if (src.PrinterIsAcceptingJobs.HasValue)
                attributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.PrinterIsAcceptingJobs, src.PrinterIsAcceptingJobs.Value));
            if (src.PrinterName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.PrinterName, src.PrinterName));
            if (src.PrinterServiceType != null)
                attributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterServiceType, map.Map<string>(src.PrinterServiceType.Value)));
            if (src.PrinterState.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.PrinterState, (int)src.PrinterState.Value));
            if (src.PrinterStateReasons != null)
                attributes.AddRange(src.PrinterStateReasons.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterStateReasons, x.Value)));
            if (src.PrinterXriSupported != null)
                attributes.AddRange(src.PrinterXriSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.PrinterXriSupported)));
            return attributes;
        });
    }
}
