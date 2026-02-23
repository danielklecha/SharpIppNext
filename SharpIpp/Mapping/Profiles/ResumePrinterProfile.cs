using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class ResumePrinterProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<ResumePrinterRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.ResumePrinter };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, ResumePrinterRequest>( ( src, map ) =>
        {
            var dst = new ResumePrinterRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
            dst.OperationAttributes = ResumePrinterOperationAttributes.Create<ResumePrinterOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
            return dst;
        } );

        mapper.CreateMap<IppResponseMessage, ResumePrinterResponse>((src, map) =>
        {
            var dst = new ResumePrinterResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<ResumePrinterResponse, IppResponseMessage>( ( src, map ) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>( src, dst );
            return dst;
        } );
    }
}
