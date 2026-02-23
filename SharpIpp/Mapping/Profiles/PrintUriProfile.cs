using System;
using System.Linq;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class PrintUriProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<PrintUriRequest, IppRequestMessage>((src, map) =>
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (src.OperationAttributes?.DocumentUri == null)
                {
                    throw new ArgumentException($"{nameof(JobAttribute.DocumentUri)} must be set");
                }

                var dst = new IppRequestMessage { IppOperation = IppOperation.PrintUri };
                map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
                if(src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));

                if (src.JobTemplateAttributes != null)
                {
                    map.Map(src.JobTemplateAttributes, dst);
                }

                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, PrintUriRequest>( ( src, map ) =>
            {
                var dst = new PrintUriRequest
                {
                    JobTemplateAttributes = new JobTemplateAttributes()
                };
                map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
                map.Map( src, dst.JobTemplateAttributes );
                dst.OperationAttributes = PrintUriOperationAttributes.Create<PrintUriOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, PrintUriResponse>((src, map) =>
            {
                var dst = new PrintUriResponse();
                map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<PrintUriResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppJobResponse, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
