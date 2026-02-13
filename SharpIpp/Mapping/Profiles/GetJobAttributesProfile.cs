using System;
using System.Collections.Generic;
using System.Linq;

using SharpIpp.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class GetJobAttributesProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<GetJobAttributesRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.GetJobAttributes };
                map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
                if (src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                dst.JobAttributes.Populate(src.AdditionalJobAttributes);
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, GetJobAttributesRequest>( ( src, map ) =>
            {
                var dst = new GetJobAttributesRequest();
                map.Map<IIppRequestMessage, IIppJobRequest>( src, dst );
                dst.OperationAttributes = GetJobAttributesOperationAttributes.Create<GetJobAttributesOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                var additionalJobAttributes = src.JobAttributes.Where( x => !JobAttribute.GetAttributes( src.Version ).Contains( x.Name ) ).ToList();
                if (additionalJobAttributes.Any())
                    dst.AdditionalJobAttributes = additionalJobAttributes;
                return dst;
            } );

            //https://tools.ietf.org/html/rfc2911#section-4.4
            mapper.CreateMap<IppResponseMessage, GetJobAttributesResponse>((src, map) =>
            {
                var dst = new GetJobAttributesResponse { JobAttributes = map.Map<JobDescriptionAttributes>(src.AllAttributes()) };
                map.Map<IppResponseMessage, IIppResponseMessage>(src, dst);
                return dst;
            });

            mapper.CreateMap<GetJobAttributesResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppResponseMessage, IppResponseMessage>( src, dst );
                var section = new IppSection { Tag = SectionTag.JobAttributesTag };
                section.Attributes.AddRange( map.Map<IDictionary<string, IppAttribute[]>>( src.JobAttributes ).Values.SelectMany( x => x ) );
                dst.Sections.Add(section );
                return dst;
            } );

            mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobDescriptionAttributes>((src, map) => new JobDescriptionAttributes
            {
                DateTimeAtCompleted = map.MapFromDic<DateTimeOffset?>(src, JobAttribute.DateTimeAtCompleted),
                DateTimeAtCreation = map.MapFromDic<DateTimeOffset?>(src, JobAttribute.DateTimeAtCreation),
                DateTimeAtProcessing = map.MapFromDic<DateTimeOffset?>(src, JobAttribute.DateTimeAtProcessing),
                JobId = map.MapFromDic<int?>(src, JobAttribute.JobId),
                JobUri = map.MapFromDic<string?>(src, JobAttribute.JobUri),
                JobImpressionsCompleted = map.MapFromDic<int?>(src, JobAttribute.JobImpressionsCompleted),
                JobMediaSheetsCompleted = map.MapFromDic<int?>(src, JobAttribute.JobMediaSheetsCompleted),
                JobOriginatingUserName = map.MapFromDic<string?>(src, JobAttribute.JobOriginatingUserName),
                JobPrinterUpTime = map.MapFromDic<int?>(src, JobAttribute.JobPrinterUpTime),
                JobPrinterUri = map.MapFromDic<string?>(src, JobAttribute.JobPrinterUri),
                JobState = map.MapFromDic<JobState?>(src, JobAttribute.JobState),
                JobStateMessage = map.MapFromDic<string?>(src, JobAttribute.JobStateMessage),
                JobStateReasons = map.MapFromDicSetNull<JobStateReason[]?>(src, JobAttribute.JobStateReasons),
                TimeAtCompleted = map.MapFromDic<int?>(src, JobAttribute.TimeAtCompleted),
                TimeAtCreation = map.MapFromDic<int?>(src, JobAttribute.TimeAtCreation),
                TimeAtProcessing = map.MapFromDic<int?>(src, JobAttribute.TimeAtProcessing),
                JobName = map.MapFromDic<string?>(src, JobAttribute.JobName),
                JobKOctetsProcessed = map.MapFromDic<int?>(src, JobAttribute.JobKOctetsProcessed),
                JobImpressions = map.MapFromDic<int?>(src, JobAttribute.JobImpressions),
                JobMediaSheets = map.MapFromDic<int?>(src, JobAttribute.JobMediaSheets),
                JobMoreInfo = map.MapFromDic<string?>(src, JobAttribute.JobMoreInfo),
                NumberOfDocuments = map.MapFromDic<int?>(src, JobAttribute.NumberOfDocuments),
                NumberOfInterveningJobs = map.MapFromDic<int?>(src, JobAttribute.NumberOfInterveningJobs),
                OutputDeviceAssigned = map.MapFromDic<string?>(src, JobAttribute.OutputDeviceAssigned),
                JobKOctets = map.MapFromDic<int?>(src, JobAttribute.JobKOctets),
                JobDetailedStatusMessages = map.MapFromDicSetNull<string[]?>(src, JobAttribute.JobDetailedStatusMessages),
                JobDocumentAccessErrors = map.MapFromDicSetNull<string[]?>(src, JobAttribute.JobDocumentAccessErrors),
                JobMessageFromOperator = map.MapFromDic<string?>(src, JobAttribute.JobMessageFromOperator)
            });

            mapper.CreateMap<JobDescriptionAttributes, IDictionary<string, IppAttribute[]>>( ( src, map ) =>
            {
                var dic = new Dictionary<string, IppAttribute[]>();
                if (src.DateTimeAtCompleted != null)
                {
                    if (src.DateTimeAtCompleted.Value > DateTimeOffset.MinValue)
                        dic.Add(JobAttribute.DateTimeAtCompleted, new IppAttribute[] { new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCompleted, src.DateTimeAtCompleted.Value) });
                    else
                        dic.Add(JobAttribute.DateTimeAtCompleted, new IppAttribute[] { new IppAttribute(Tag.NoValue, JobAttribute.DateTimeAtCompleted, NoValue.Instance) });
                }
                if (src.DateTimeAtCreation != null)
                {
                    if (src.DateTimeAtCreation.Value > DateTimeOffset.MinValue)
                        dic.Add(JobAttribute.DateTimeAtCreation, new IppAttribute[] { new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCreation, src.DateTimeAtCreation.Value) });
                    else
                        dic.Add(JobAttribute.DateTimeAtCreation, new IppAttribute[] { new IppAttribute(Tag.NoValue, JobAttribute.DateTimeAtCreation, NoValue.Instance) });
                }
                if (src.DateTimeAtProcessing != null)
                {
                    if (src.DateTimeAtProcessing.Value > DateTimeOffset.MinValue)
                        dic.Add(JobAttribute.DateTimeAtProcessing, new IppAttribute[] { new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtProcessing, src.DateTimeAtProcessing.Value) });
                    else
                        dic.Add(JobAttribute.DateTimeAtProcessing, new IppAttribute[] { new IppAttribute(Tag.NoValue, JobAttribute.DateTimeAtProcessing, NoValue.Instance) });
                }
                if ( src.JobId != null )
                    dic.Add( JobAttribute.JobId, new IppAttribute[] { new IppAttribute( Tag.Integer, JobAttribute.JobId, src.JobId.Value ) } );
                if( src.JobUri != null )
                    dic.Add( JobAttribute.JobUri, new IppAttribute[] { new IppAttribute( Tag.Uri, JobAttribute.JobUri, src.JobUri ) } );
                if ( src.JobImpressionsCompleted != null )
                    dic.Add( JobAttribute.JobImpressionsCompleted, new IppAttribute[] { new IppAttribute( Tag.Integer, JobAttribute.JobImpressionsCompleted, src.JobImpressionsCompleted.Value ) } );
                if ( src.JobMediaSheetsCompleted != null )
                    dic.Add( JobAttribute.JobMediaSheetsCompleted, new IppAttribute[] { new IppAttribute( Tag.Integer, JobAttribute.JobMediaSheetsCompleted, src.JobMediaSheetsCompleted.Value ) } );
                if ( src.JobOriginatingUserName != null )
                    dic.Add( JobAttribute.JobOriginatingUserName, new IppAttribute[] { new IppAttribute( Tag.NameWithoutLanguage, JobAttribute.JobOriginatingUserName, src.JobOriginatingUserName ) } );
                if ( src.JobPrinterUpTime != null )
                    dic.Add( JobAttribute.JobPrinterUpTime, new IppAttribute[] { new IppAttribute( Tag.Integer, JobAttribute.JobPrinterUpTime, src.JobPrinterUpTime.Value ) } );
                if ( src.JobPrinterUri != null )
                    dic.Add( JobAttribute.JobPrinterUri, new IppAttribute[] { new IppAttribute( Tag.Uri, JobAttribute.JobPrinterUri, src.JobPrinterUri ) } );
                if ( src.JobState != null )
                    dic.Add( JobAttribute.JobState, new IppAttribute[] { new IppAttribute( Tag.Enum, JobAttribute.JobState, (int)src.JobState.Value ) } );
                if ( src.JobStateMessage != null )
                    dic.Add( JobAttribute.JobStateMessage, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, JobAttribute.JobStateMessage, src.JobStateMessage ) } );
                if (src.JobStateReasons?.Any() ?? false)
                    dic.Add( JobAttribute.JobStateReasons, src.JobStateReasons.Select( x => new IppAttribute( Tag.Keyword, JobAttribute.JobStateReasons, map.Map<string>( x ) ) ).ToArray() );
                if (src.TimeAtCompleted != null)
                {
                    if (src.TimeAtCompleted.Value >= 0)
                        dic.Add(JobAttribute.TimeAtCompleted, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.TimeAtCompleted, src.TimeAtCompleted.Value) });
                    else
                        dic.Add(JobAttribute.TimeAtCompleted, new IppAttribute[] { new IppAttribute(Tag.NoValue, JobAttribute.TimeAtCompleted, NoValue.Instance) });
                }
                if (src.TimeAtCreation != null)
                {
                    if (src.TimeAtCreation.Value >= 0)
                        dic.Add(JobAttribute.TimeAtCreation, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.TimeAtCreation, src.TimeAtCreation.Value) });
                    else
                        dic.Add(JobAttribute.TimeAtCreation, new IppAttribute[] { new IppAttribute(Tag.NoValue, JobAttribute.TimeAtCreation, NoValue.Instance) });
                }
                if (src.TimeAtProcessing != null)
                {
                    if (src.TimeAtProcessing.Value >= 0)
                        dic.Add(JobAttribute.TimeAtProcessing, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.TimeAtProcessing, src.TimeAtProcessing.Value) });
                    else
                        dic.Add(JobAttribute.TimeAtProcessing, new IppAttribute[] { new IppAttribute(Tag.NoValue, JobAttribute.TimeAtProcessing, NoValue.Instance) });
                }
                if (src.JobName != null)
                    dic.Add(JobAttribute.JobName, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobName, src.JobName) });
                if (src.JobKOctetsProcessed != null)
                    dic.Add(JobAttribute.JobKOctetsProcessed, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.JobKOctetsProcessed, src.JobKOctetsProcessed.Value) });
                if (src.JobImpressions != null)
                    dic.Add(JobAttribute.JobImpressions, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.JobImpressions, src.JobImpressions.Value) });
                if (src.JobMediaSheets != null)
                    dic.Add(JobAttribute.JobMediaSheets, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheets, src.JobMediaSheets.Value) });
                if (src.JobMoreInfo != null)
                    dic.Add(JobAttribute.JobMoreInfo, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobMoreInfo, src.JobMoreInfo) });
                if (src.NumberOfDocuments != null)
                    dic.Add(JobAttribute.NumberOfDocuments, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.NumberOfDocuments, src.NumberOfDocuments.Value) });
                if (src.NumberOfInterveningJobs != null)
                    dic.Add(JobAttribute.NumberOfInterveningJobs, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.NumberOfInterveningJobs, src.NumberOfInterveningJobs.Value) });
                if (src.OutputDeviceAssigned != null)
                    dic.Add(JobAttribute.OutputDeviceAssigned, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputDeviceAssigned, src.OutputDeviceAssigned) });
                if (src.JobKOctets != null)
                    dic.Add(JobAttribute.JobKOctets, new IppAttribute[] { new IppAttribute(Tag.Integer, JobAttribute.JobKOctets, src.JobKOctets.Value) });
                if (src.JobDetailedStatusMessages != null)
                    dic.Add(JobAttribute.JobDetailedStatusMessages, src.JobDetailedStatusMessages.Select(x => new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobDetailedStatusMessages, x)).ToArray());
                if (src.JobDocumentAccessErrors != null)
                    dic.Add(JobAttribute.JobDocumentAccessErrors, src.JobDocumentAccessErrors.Select(x => new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobDocumentAccessErrors, x)).ToArray());

                if (src.JobMessageFromOperator != null)
                    dic.Add(JobAttribute.JobMessageFromOperator, new IppAttribute[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobMessageFromOperator, src.JobMessageFromOperator) });
                return dic;
            } );
        }
    }
}
