using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal abstract class AbstractPrinterRequestProfile : IProfile
{
    public abstract void CreateMaps(IMapperConstructor mapper);

    protected void AddPrinterMap<T, TOperationAttributes>(IMapperConstructor mapper, IppOperation operation)
        where T : IppRequest<TOperationAttributes>, IIppPrinterRequest, new()
        where TOperationAttributes : OperationAttributes, new()
    {
        mapper.CreateMap<T, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = operation };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<TOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, T>((src, map) =>
        {
            var dst = new T();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, TOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
