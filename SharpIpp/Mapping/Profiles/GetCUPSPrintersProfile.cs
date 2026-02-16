using System.Collections.Generic;
using System.Linq;

using SharpIpp.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class GetCUPSPrintersProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<CUPSGetPrintersRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.GetCUPSPrinters };
                map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
                if(src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, CUPSGetPrintersRequest>( ( src, map ) =>
            {
                var dst = new CUPSGetPrintersRequest();
                map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
                dst.OperationAttributes = CUPSGetPrintersOperationAttributes.Create<CUPSGetPrintersOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, CUPSGetPrintersResponse>((src, map) =>
            {
                var dst = new CUPSGetPrintersResponse { Jobs = map.Map<List<IppSection>, JobDescriptionAttributes[]>(src.Sections) };
                map.Map<IppResponseMessage, IIppResponseMessage>(src, dst);
                return dst;
            });

            mapper.CreateMap<CUPSGetPrintersResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                dst.Sections.AddRange( map.Map<JobDescriptionAttributes[], List<IppSection>>( src.Jobs ) );
                map.Map<IIppResponseMessage, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
