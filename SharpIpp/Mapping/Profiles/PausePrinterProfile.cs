using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class PausePrinterProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<PausePrinterRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.PausePrinter };
                map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
                if (src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, PausePrinterRequest>( ( src, map ) =>
            {
                var dst = new PausePrinterRequest();
                map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
                dst.OperationAttributes = PausePrinterOperationAttributes.Create<PausePrinterOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, PausePrinterResponse>((src, map) =>
            {
                var dst = new PausePrinterResponse();
                map.Map<IppResponseMessage, IIppResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<PausePrinterResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppResponse, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
