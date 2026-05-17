using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SystemOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>((src, dst, map) =>
        {
            dst ??= new SystemOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, JobAttribute.PrinterId);
            dst.NotifyPrinterIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.NotifyPrinterIds);
            dst.NotifyResourceId = map.MapFromDicNullable<int?>(src, SystemAttribute.NotifyResourceId);
            dst.NotifySystemUpTime = map.MapFromDicNullable<int?>(src, SystemAttribute.NotifySystemUpTime);
            dst.NotifySystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.NotifySystemUri);
            dst.RestartGetInterval = map.MapFromDicNullable<int?>(src, SystemAttribute.RestartGetInterval);
            dst.WhichPrinters = map.MapFromDicNullable<WhichPrinters?>(src, SystemAttribute.WhichPrinters);
            dst.NotifySubscriptionId = map.MapFromDicNullable<int?>(src, SystemAttribute.NotifySubscriptionId);
            dst.NotifyPullMethod = map.MapFromDicNullable<string?>(src, SystemAttribute.NotifyPullMethod);
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
                dst.Add(new IppAttribute(Tag.Keyword, SystemAttribute.WhichPrinters, src.WhichPrinters.Value.Value));
            if (src.NotifySubscriptionId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, SystemAttribute.NotifySubscriptionId, src.NotifySubscriptionId.Value));
            if (src.NotifyPullMethod != null)
                dst.Add(new IppAttribute(Tag.Keyword, SystemAttribute.NotifyPullMethod, src.NotifyPullMethod));
            return dst;
        });
    }
}
