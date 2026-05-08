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
                PrinterId = map.MapFromDicNullable<int?>(src, JobAttribute.PrinterId),
                PrinterInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterInfo),
                PrinterIsAcceptingJobs = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PrinterIsAcceptingJobs),
                PrinterName = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterName),
                PrinterServiceType = map.MapFromDicNullable<PrinterServiceType?>(src, PrinterAttribute.PrinterServiceType),
                PrinterState = map.MapFromDicNullable<PrinterState?>(src, PrinterAttribute.PrinterState),
                PrinterStateReasons = map.MapFromDicSetNullable<PrinterStateReason[]?>(src, PrinterAttribute.PrinterStateReasons)
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
                attributes.AddRange(src.PrinterStateReasons.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterStateReasons, x.Value)));
            if (src.PrinterXriSupported != null)
                attributes.AddRange(src.PrinterXriSupported.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(PrinterAttribute.PrinterXriSupported)));
            return attributes;
        });
    }
}
