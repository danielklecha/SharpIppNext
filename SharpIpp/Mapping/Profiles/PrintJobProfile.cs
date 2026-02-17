using System;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class PrintJobProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<PrintJobRequest, IppRequestMessage>((src, map) =>
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (src.Document == null)
                {
                    throw new ArgumentException($"{nameof(src.Document)} must be set");
                }

                var dst = new IppRequestMessage { IppOperation = IppOperation.PrintJob, Document = src.Document };
                map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);

                if (src.JobTemplateAttributes != null)
                {
                    map.Map(src.JobTemplateAttributes, dst);
                }
                if(src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, PrintJobRequest>( ( src, map ) =>
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if ( src.Document == null )
                {
                    throw new ArgumentException( $"{nameof( src.Document )} must be set" );
                }
                var dst = new PrintJobRequest { Document = src.Document, JobTemplateAttributes = new JobTemplateAttributes() };
                map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
                map.Map( src, dst.JobTemplateAttributes );
                dst.OperationAttributes = PrintJobOperationAttributes.Create<PrintJobOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, PrintJobResponse>((src, map) =>
            {
                var dst = new PrintJobResponse();
                map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<PrintJobResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppJobResponse, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
