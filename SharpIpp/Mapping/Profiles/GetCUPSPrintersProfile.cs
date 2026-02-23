using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

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
            var dst = new CUPSGetPrintersResponse { PrintersAttributes = map.Map<List<List<IppAttribute>>, PrinterDescriptionAttributes[]>(src.PrinterAttributes) };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<CUPSGetPrintersResponse, IppResponseMessage>( ( src, map ) =>
        {
            var dst = new IppResponseMessage();
            if(src.PrintersAttributes != null)
                dst.PrinterAttributes.AddRange( map.Map<PrinterDescriptionAttributes[], List<List<IppAttribute>>>( src.PrintersAttributes ) );
            map.Map<IIppResponse, IppResponseMessage>( src, dst );
            return dst;
        } );

        mapper.CreateMap<List<List<IppAttribute>>, PrinterDescriptionAttributes[]>((src, map) =>
            src.Select(x => map.Map<PrinterDescriptionAttributes>(x.ToIppDictionary()))
                .ToArray());

        mapper.CreateMap<PrinterDescriptionAttributes[], List<List<IppAttribute>>>( (src, map) =>
        {
            return src.Select(x =>
            {
                var attrs = new List<IppAttribute>();
                attrs.AddRange( map.Map<IDictionary<string, IppAttribute[]>>( x ).Values.SelectMany( v => v ) );
                return attrs;
            }).ToList();
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>((src, map) =>
            new PrinterDescriptionAttributes
            {
                CharsetConfigured = map.MapFromDicNullable<string?>(src, PrinterAttribute.CharsetConfigured),
                CharsetSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.CharsetSupported),
                ColorSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.ColorSupported),
                CompressionSupported = map.MapFromDicSetNullable<Compression[]?>(src, PrinterAttribute.CompressionSupported),
                DocumentFormatDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.DocumentFormatDefault),
                DocumentFormatSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.DocumentFormatSupported),
                GeneratedNaturalLanguageSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.GeneratedNaturalLanguageSupported),
                IppVersionsSupported = map.MapFromDicSetNullable<IppVersion[]?>(src, PrinterAttribute.IppVersionsSupported),
                JobImpressionsSupported = map.MapFromDicNullable<Range?>(src, PrinterAttribute.JobImpressionsSupported),
                JobKOctetsSupported = map.MapFromDicNullable<Range?>(src, PrinterAttribute.JobKOctetsSupported),
                JpegKOctetsSupported = map.MapFromDicNullable<Range?>(src, PrinterAttribute.JpegKOctetsSupported),
                PdfKOctetsSupported = map.MapFromDicNullable<Range?>(src, PrinterAttribute.PdfKOctetsSupported),
                JobMediaSheetsSupported = map.MapFromDicNullable<Range?>(src, PrinterAttribute.JobMediaSheetsSupported),
                MultipleDocumentJobsSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.MultipleDocumentJobsSupported),
                MultipleOperationTimeOut = map.MapFromDicNullable<int?>(src, PrinterAttribute.MultipleOperationTimeOut),
                NaturalLanguageConfigured = map.MapFromDicNullable<string?>(src, PrinterAttribute.NaturalLanguageConfigured),
                OperationsSupported = map.MapFromDicSetNullable<IppOperation[]?>(src, PrinterAttribute.OperationsSupported),
                PagesPerMinute = map.MapFromDicNullable<int?>(src, PrinterAttribute.PagesPerMinute),
                PdlOverrideSupported = map.MapFromDicNullable<string?>(src, PrinterAttribute.PdlOverrideSupported),
                PagesPerMinuteColor = map.MapFromDicNullable<int?>(src, PrinterAttribute.PagesPerMinuteColor),
                PrinterCurrentTime = map.MapFromDicNullable<DateTimeOffset?>(src, PrinterAttribute.PrinterCurrentTime),
                PrinterDriverInstaller = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterDriverInstaller),
                PrinterInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterInfo),
                PrinterIsAcceptingJobs = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PrinterIsAcceptingJobs),
                PrinterLocation = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterLocation),
                PrinterMakeAndModel = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMakeAndModel),
                PrinterMessageFromOperator = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMessageFromOperator),
                PrinterMoreInfo = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMoreInfo),
                PrinterMoreInfoManufacturer = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterMoreInfoManufacturer),
                PrinterName = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterName),
                PrinterState = map.MapFromDicNullable<PrinterState?>(src, PrinterAttribute.PrinterState),
                PrinterStateMessage = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterStateMessage),
                PrinterStateReasons = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterStateReasons),
                PrinterUpTime = map.MapFromDicNullable<int?>(src, PrinterAttribute.PrinterUpTime),
                PrinterUriSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.PrinterUriSupported),
                PrintScalingDefault = map.MapFromDicNullable<PrintScaling?>(src, PrinterAttribute.PrintScalingDefault),
                PrintScalingSupported = map.MapFromDicSetNullable<PrintScaling[]?>(src, PrinterAttribute.PrintScalingSupported),
                QueuedJobCount = map.MapFromDicNullable<int?>(src, PrinterAttribute.QueuedJobCount),
                ReferenceUriSchemesSupported = map.MapFromDicSetNullable<UriScheme[]?>(src, PrinterAttribute.ReferenceUriSchemesSupported),
                UriAuthenticationSupported = map.MapFromDicSetNullable<UriAuthentication[]?>(src, PrinterAttribute.UriAuthenticationSupported),
                UriSecuritySupported = map.MapFromDicSetNullable<UriSecurity[]?>(src, PrinterAttribute.UriSecuritySupported),
                MediaDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.MediaDefault),
                MediaSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.MediaSupported),
                SidesDefault = map.MapFromDicNullable<Sides?>(src, PrinterAttribute.SidesDefault),
                SidesSupported = map.MapFromDicSetNullable<Sides[]?>(src, PrinterAttribute.SidesSupported),
                FinishingsDefault = map.MapFromDicNullable<Finishings?>(src, PrinterAttribute.FinishingsDefault),
                FinishingsSupported = map.MapFromDicSetNullable<Finishings[]?>(src, PrinterAttribute.FinishingsSupported),
                PrinterResolutionDefault = map.MapFromDicNullable<Resolution?>(src, PrinterAttribute.PrinterResolutionDefault),
                PrinterResolutionSupported = map.MapFromDicSetNullable<Resolution[]?>(src, PrinterAttribute.PrinterResolutionSupported),
                PrintQualityDefault = map.MapFromDicNullable<PrintQuality?>(src, PrinterAttribute.PrintQualityDefault),
                PrintQualitySupported = map.MapFromDicSetNullable<PrintQuality[]?>(src, PrinterAttribute.PrintQualitySupported),
                JobPriorityDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobPriorityDefault),
                JobPrioritySupported = map.MapFromDicNullable<int?>(src, PrinterAttribute.JobPrioritySupported),
                CopiesDefault = map.MapFromDicNullable<int?>(src, PrinterAttribute.CopiesDefault),
                CopiesSupported = map.MapFromDicNullable<Range?>(src, PrinterAttribute.CopiesSupported),
                OrientationRequestedDefault = map.MapFromDicNullable<Orientation?>(src, PrinterAttribute.OrientationRequestedDefault),
                OrientationRequestedSupported = map.MapFromDicSetNullable<Orientation[]?>(src, PrinterAttribute.OrientationRequestedSupported),
                PageRangesSupported = map.MapFromDicNullable<bool?>(src, PrinterAttribute.PageRangesSupported),
                JobHoldUntilDefault = map.MapFromDicNullable<JobHoldUntil?>(src, PrinterAttribute.JobHoldUntilDefault),
                JobHoldUntilSupported = map.MapFromDicSetNullable<JobHoldUntil[]?>(src, PrinterAttribute.JobHoldUntilSupported),
                OutputBinDefault = map.MapFromDicNullable<string?>(src, PrinterAttribute.OutputBinDefault),
                OutputBinSupported = map.MapFromDicSetNullable<string[]?>(src, PrinterAttribute.OutputBinSupported),
                MediaColDefault = src.ContainsKey(PrinterAttribute.MediaColDefault) ? map.Map<MediaCol>(src[PrinterAttribute.MediaColDefault].FromBegCollection().ToIppDictionary()) : null,
                PrintColorModeDefault = map.MapFromDicNullable<PrintColorMode?>(src, PrinterAttribute.PrintColorModeDefault),
                PrintColorModeSupported = map.MapFromDicSetNullable<PrintColorMode[]?>(src, PrinterAttribute.PrintColorModeSupported),
                WhichJobsSupported = map.MapFromDicSetNullable<WhichJobs[]?>(src, PrinterAttribute.WhichJobsSupported),
                PrinterUUID = map.MapFromDicNullable<string?>(src, PrinterAttribute.PrinterUUID),
            });

        mapper.CreateMap<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>( ( src, map ) =>
            {
                var dic = new Dictionary<string, IppAttribute[]>();
                if(src.CharsetConfigured != null)
                    dic.Add(PrinterAttribute.CharsetConfigured, new IppAttribute[] { new IppAttribute( Tag.Charset, PrinterAttribute.CharsetConfigured, src.CharsetConfigured ) });
                if ( src.CharsetSupported!= null )
                    dic.Add(PrinterAttribute.CharsetSupported, src.CharsetSupported.Select( x => new IppAttribute( Tag.Charset, PrinterAttribute.CharsetSupported, x ) ).ToArray());
                if( src.ColorSupported != null )
                    dic.Add( PrinterAttribute.ColorSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.ColorSupported, src.ColorSupported.Value ) } );
                if ( src.CompressionSupported!= null )
                    dic.Add( PrinterAttribute.CompressionSupported, src.CompressionSupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.CompressionSupported, map.Map<string>( x ) ) ).ToArray() );
                if ( src.DocumentFormatDefault != null )
                    dic.Add( PrinterAttribute.DocumentFormatDefault, new IppAttribute[] { new IppAttribute( Tag.MimeMediaType, PrinterAttribute.DocumentFormatDefault, src.DocumentFormatDefault ) } );
                if ( src.DocumentFormatSupported!= null )
                    dic.Add( PrinterAttribute.DocumentFormatSupported, src.DocumentFormatSupported.Select( x => new IppAttribute( Tag.MimeMediaType, PrinterAttribute.DocumentFormatSupported, x ) ).ToArray() );
                if ( src.GeneratedNaturalLanguageSupported!= null )
                    dic.Add( PrinterAttribute.GeneratedNaturalLanguageSupported, src.GeneratedNaturalLanguageSupported.Select( x => new IppAttribute( Tag.NaturalLanguage, PrinterAttribute.GeneratedNaturalLanguageSupported, x ) ).ToArray() );
                if ( src.IppVersionsSupported!= null )
                    dic.Add( PrinterAttribute.IppVersionsSupported, src.IppVersionsSupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.IppVersionsSupported, x.ToString() ) ).ToArray() );
                if ( src.JobImpressionsSupported != null )
                    dic.Add( PrinterAttribute.JobImpressionsSupported, new IppAttribute[] { new IppAttribute( Tag.RangeOfInteger, PrinterAttribute.JobImpressionsSupported, src.JobImpressionsSupported.Value ) } );
                if ( src.JobKOctetsSupported != null )
                    dic.Add( PrinterAttribute.JobKOctetsSupported, new IppAttribute[] { new IppAttribute( Tag.RangeOfInteger, PrinterAttribute.JobKOctetsSupported, src.JobKOctetsSupported.Value ) } );
                if (src.JpegKOctetsSupported != null)
                    dic.Add(PrinterAttribute.JpegKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.JpegKOctetsSupported, src.JpegKOctetsSupported.Value) });
                if (src.PdfKOctetsSupported != null)
                    dic.Add(PrinterAttribute.PdfKOctetsSupported, new IppAttribute[] { new IppAttribute(Tag.RangeOfInteger, PrinterAttribute.PdfKOctetsSupported, src.PdfKOctetsSupported.Value) });
                if ( src.JobMediaSheetsSupported != null )
                    dic.Add( PrinterAttribute.JobMediaSheetsSupported, new IppAttribute[] { new IppAttribute( Tag.RangeOfInteger, PrinterAttribute.JobMediaSheetsSupported, src.JobMediaSheetsSupported.Value ) } );
                if ( src.MultipleDocumentJobsSupported != null )
                    dic.Add( PrinterAttribute.MultipleDocumentJobsSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.MultipleDocumentJobsSupported, src.MultipleDocumentJobsSupported.Value ) } );
                if ( src.MultipleOperationTimeOut != null )
                    dic.Add( PrinterAttribute.MultipleOperationTimeOut, new IppAttribute[] { new IppAttribute(Tag.Integer, PrinterAttribute.MultipleOperationTimeOut, src.MultipleOperationTimeOut.Value ) } );
                if ( src.NaturalLanguageConfigured != null )
                    dic.Add( PrinterAttribute.NaturalLanguageConfigured, new IppAttribute[] { new IppAttribute( Tag.NaturalLanguage, PrinterAttribute.NaturalLanguageConfigured, src.NaturalLanguageConfigured ) } );
                if ( src.OperationsSupported!= null )
                    dic.Add( PrinterAttribute.OperationsSupported, src.OperationsSupported.Select( x => new IppAttribute(Tag.Enum, PrinterAttribute.OperationsSupported, (int)x ) ).ToArray() );
                if ( src.PagesPerMinute != null )
                    dic.Add( PrinterAttribute.PagesPerMinute, new IppAttribute[] { new IppAttribute(Tag.Integer, PrinterAttribute.PagesPerMinute, src.PagesPerMinute.Value ) } );
                if ( src.PdlOverrideSupported != null )
                    dic.Add( PrinterAttribute.PdlOverrideSupported, new IppAttribute[] { new IppAttribute( Tag.Keyword, PrinterAttribute.PdlOverrideSupported, src.PdlOverrideSupported ) } );
                if ( src.PagesPerMinuteColor != null )
                    dic.Add( PrinterAttribute.PagesPerMinuteColor, [new IppAttribute(Tag.Integer, PrinterAttribute.PagesPerMinuteColor, src.PagesPerMinuteColor.Value )] );
                if ( src.PrinterCurrentTime != null )
                    dic.Add( PrinterAttribute.PrinterCurrentTime, new IppAttribute[] { new IppAttribute(Tag.DateTime, PrinterAttribute.PrinterCurrentTime, src.PrinterCurrentTime.Value ) } );
                if ( src.PrinterDriverInstaller != null )
                    dic.Add( PrinterAttribute.PrinterDriverInstaller, new IppAttribute[] { new IppAttribute( Tag.Uri, PrinterAttribute.PrinterDriverInstaller, src.PrinterDriverInstaller ) } );
                if ( src.PrinterInfo != null )
                    dic.Add( PrinterAttribute.PrinterInfo, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, PrinterAttribute.PrinterInfo, src.PrinterInfo ) } );
                if ( src.PrinterIsAcceptingJobs != null )
                    dic.Add( PrinterAttribute.PrinterIsAcceptingJobs, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.PrinterIsAcceptingJobs, src.PrinterIsAcceptingJobs.Value ) } );
                if ( src.PrinterLocation != null )
                    dic.Add( PrinterAttribute.PrinterLocation, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, PrinterAttribute.PrinterLocation, src.PrinterLocation ) } );
                if ( src.PrinterMakeAndModel != null )
                    dic.Add( PrinterAttribute.PrinterMakeAndModel, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, PrinterAttribute.PrinterMakeAndModel, src.PrinterMakeAndModel ) } );
                if ( src.PrinterMessageFromOperator != null )
                    dic.Add( PrinterAttribute.PrinterMessageFromOperator, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, PrinterAttribute.PrinterMessageFromOperator, src.PrinterMessageFromOperator ) } );
                if ( src.PrinterMoreInfo != null )
                    dic.Add( PrinterAttribute.PrinterMoreInfo, new IppAttribute[] { new IppAttribute( Tag.Uri, PrinterAttribute.PrinterMoreInfo, src.PrinterMoreInfo ) } );
                if ( src.PrinterMoreInfoManufacturer != null )
                    dic.Add( PrinterAttribute.PrinterMoreInfoManufacturer, new IppAttribute[] { new IppAttribute( Tag.Uri, PrinterAttribute.PrinterMoreInfoManufacturer, src.PrinterMoreInfoManufacturer ) } );
                if ( src.PrinterName != null )
                    dic.Add( PrinterAttribute.PrinterName, new IppAttribute[] { new IppAttribute( Tag.NameWithoutLanguage, PrinterAttribute.PrinterName, src.PrinterName ) } );
                if ( src.PrinterState != null )
                    dic.Add( PrinterAttribute.PrinterState, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.PrinterState, (int)src.PrinterState.Value ) } );
                if ( src.PrinterStateMessage != null )
                    dic.Add( PrinterAttribute.PrinterStateMessage, new IppAttribute[] { new IppAttribute( Tag.TextWithoutLanguage, PrinterAttribute.PrinterStateMessage, src.PrinterStateMessage ) } );
                if ( src.PrinterStateReasons!= null )
                    dic.Add( PrinterAttribute.PrinterStateReasons, src.PrinterStateReasons.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.PrinterStateReasons, x ) ).ToArray() );
                if ( src.PrinterUpTime != null )
                    dic.Add( PrinterAttribute.PrinterUpTime, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterUpTime, src.PrinterUpTime.Value )] );
                if ( src.PrinterUriSupported!= null )
                    dic.Add( PrinterAttribute.PrinterUriSupported, src.PrinterUriSupported.Select( x => new IppAttribute( Tag.Uri, PrinterAttribute.PrinterUriSupported, x ) ).ToArray() );
                if ( src.PrintScalingDefault != null )
                    dic.Add( PrinterAttribute.PrintScalingDefault, new IppAttribute[] { new IppAttribute( Tag.Keyword, PrinterAttribute.PrintScalingDefault, map.Map<string>( src.PrintScalingDefault ) ) } );
                if ( src.PrintScalingSupported != null )
                    dic.Add( PrinterAttribute.PrintScalingSupported, src.PrintScalingSupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.PrintScalingSupported, map.Map<string>( x ) ) ).ToArray() );
                if ( src.QueuedJobCount != null )
                    dic.Add( PrinterAttribute.QueuedJobCount, [new IppAttribute(Tag.Integer, PrinterAttribute.QueuedJobCount, src.QueuedJobCount.Value )] );
                if ( src.ReferenceUriSchemesSupported!= null )
                    dic.Add( PrinterAttribute.ReferenceUriSchemesSupported, src.ReferenceUriSchemesSupported.Select( x => new IppAttribute( Tag.UriScheme, PrinterAttribute.ReferenceUriSchemesSupported, map.Map<string>( x ) ) ).ToArray() );
                if ( src.UriAuthenticationSupported!= null )
                    dic.Add( PrinterAttribute.UriAuthenticationSupported, src.UriAuthenticationSupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.UriAuthenticationSupported, map.Map<string>( x ) ) ).ToArray() );
                if ( src.UriSecuritySupported!= null )
                    dic.Add( PrinterAttribute.UriSecuritySupported, src.UriSecuritySupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.UriSecuritySupported, map.Map<string>( x ) ) ).ToArray() );
                if( src.MediaDefault != null )
                    dic.Add( PrinterAttribute.MediaDefault, new IppAttribute[] { new IppAttribute( Tag.Keyword, PrinterAttribute.MediaDefault, src.MediaDefault ) } );
                if ( src.MediaSupported!= null )
                    dic.Add( PrinterAttribute.MediaSupported, src.MediaSupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.MediaSupported, x ) ).ToArray() );
                if ( src.SidesDefault != null )
                    dic.Add( PrinterAttribute.SidesDefault, new IppAttribute[] { new IppAttribute( Tag.Keyword, PrinterAttribute.SidesDefault, map.Map<string>( src.SidesDefault ) ) } );
                if ( src.SidesSupported!= null )
                    dic.Add( PrinterAttribute.SidesSupported, src.SidesSupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.SidesSupported, map.Map<string>( x ) ) ).ToArray() );
                if ( src.FinishingsDefault != null )
                    dic.Add( PrinterAttribute.FinishingsDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.FinishingsDefault, (int)src.FinishingsDefault.Value ) } );
                if (src.FinishingsSupported!= null)
                    dic.Add(PrinterAttribute.FinishingsSupported, src.FinishingsSupported.Select(x => new IppAttribute(Tag.Enum, PrinterAttribute.FinishingsSupported, (int)x)).ToArray());
                if ( src.PrinterResolutionDefault != null )
                    dic.Add( PrinterAttribute.PrinterResolutionDefault, new IppAttribute[] { new IppAttribute( Tag.Resolution, PrinterAttribute.PrinterResolutionDefault, src.PrinterResolutionDefault.Value ) } );
                if ( src.PrinterResolutionSupported!= null )
                    dic.Add( PrinterAttribute.PrinterResolutionSupported, src.PrinterResolutionSupported.Select( x => new IppAttribute( Tag.Resolution, PrinterAttribute.PrinterResolutionSupported, x ) ).ToArray() );
                if ( src.PrintQualityDefault != null )
                    dic.Add( PrinterAttribute.PrintQualityDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.PrintQualityDefault, (int)src.PrintQualityDefault.Value ) } );
                if ( src.PrintQualitySupported!= null )
                    dic.Add( PrinterAttribute.PrintQualitySupported, src.PrintQualitySupported.Select( x => new IppAttribute(Tag.Enum, PrinterAttribute.PrintQualitySupported, (int)x ) ).ToArray() );
                if ( src.JobPriorityDefault != null )
                    dic.Add( PrinterAttribute.JobPriorityDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.JobPriorityDefault, src.JobPriorityDefault.Value )] );
                if ( src.JobPrioritySupported != null )
                    dic.Add( PrinterAttribute.JobPrioritySupported, [new IppAttribute(Tag.Integer, PrinterAttribute.JobPrioritySupported, src.JobPrioritySupported.Value )] );
                if ( src.CopiesDefault != null )
                    dic.Add( PrinterAttribute.CopiesDefault, [new IppAttribute(Tag.Integer, PrinterAttribute.CopiesDefault, src.CopiesDefault.Value )] );
                if ( src.CopiesSupported != null )
                    dic.Add( PrinterAttribute.CopiesSupported, new IppAttribute[] { new IppAttribute( Tag.RangeOfInteger, PrinterAttribute.CopiesSupported, src.CopiesSupported.Value ) } );
                if ( src.OrientationRequestedDefault != null )
                    dic.Add( PrinterAttribute.OrientationRequestedDefault, new IppAttribute[] { new IppAttribute(Tag.Enum, PrinterAttribute.OrientationRequestedDefault, (int)src.OrientationRequestedDefault.Value ) } );
                if ( src.OrientationRequestedSupported!= null )
                    dic.Add( PrinterAttribute.OrientationRequestedSupported, src.OrientationRequestedSupported.Select( x => new IppAttribute(Tag.Enum, PrinterAttribute.OrientationRequestedSupported, (int)x ) ).ToArray() );
                if ( src.PageRangesSupported != null )
                    dic.Add( PrinterAttribute.PageRangesSupported, new IppAttribute[] { new IppAttribute(Tag.Boolean, PrinterAttribute.PageRangesSupported, src.PageRangesSupported.Value ) } );
                if (src.JobHoldUntilDefault != null)
                    dic.Add( PrinterAttribute.JobHoldUntilDefault, new IppAttribute[] { new IppAttribute( Tag.Keyword, PrinterAttribute.JobHoldUntilDefault, map.Map<string>( src.JobHoldUntilDefault.Value ) ) } );
                if (src.JobHoldUntilSupported!= null)
                    dic.Add( PrinterAttribute.JobHoldUntilSupported, src.JobHoldUntilSupported.Select( x => new IppAttribute( Tag.Keyword, PrinterAttribute.JobHoldUntilSupported, map.Map<string>( x ) ) ).ToArray() );
                if (src.OutputBinDefault != null)
                    dic.Add(PrinterAttribute.OutputBinDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.OutputBinDefault, src.OutputBinDefault) });
                if (src.OutputBinSupported!= null)
                    dic.Add(PrinterAttribute.OutputBinSupported, src.OutputBinSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.OutputBinSupported, x)).ToArray());
                if (src.MediaColDefault != null)
                    dic.Add(PrinterAttribute.MediaColDefault, map.Map<IEnumerable<IppAttribute>>(src.MediaColDefault).ToBegCollection(PrinterAttribute.MediaColDefault).ToArray());
                if(src.PrintColorModeDefault != null)
                    dic.Add(PrinterAttribute.PrintColorModeDefault, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrintColorModeDefault, map.Map<string>(src.PrintColorModeDefault.Value)) });
                if (src.PrintColorModeSupported!= null)
                    dic.Add(PrinterAttribute.PrintColorModeSupported, src.PrintColorModeSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.PrintColorModeSupported, map.Map<string>(x))).ToArray());
                if (src.WhichJobsSupported!= null)
                    dic.Add(PrinterAttribute.WhichJobsSupported, src.WhichJobsSupported.Select(x => new IppAttribute(Tag.Keyword, PrinterAttribute.WhichJobsSupported, map.Map<string>(x))).ToArray());
                if (src.PrinterUUID != null)
                    dic.Add(PrinterAttribute.PrinterUUID, new IppAttribute[] { new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterUUID, src.PrinterUUID) });
                return dic;
            } );
    }
}
