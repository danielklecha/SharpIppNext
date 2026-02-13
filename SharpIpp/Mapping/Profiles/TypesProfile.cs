using System;
using System.Linq;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    public class TypesProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateIppMap<int>();
            mapper.CreateIppMap<bool>();
            mapper.CreateIppMap<string>();
            mapper.CreateIppMap<DateTimeOffset>();
            mapper.CreateIppMap<Range>();
            mapper.CreateIppMap<Resolution>();
            mapper.CreateIppMap<StringWithLanguage>();

            var unixStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);
            mapper.CreateIppMap<int, DateTime>((src, map) => unixStartTime.AddSeconds(src));
            mapper.CreateIppMap<DateTime, int>( ( src, map ) => ( src - unixStartTime ).Seconds );
            mapper.CreateIppMap<int, IppOperation>((src, map) => (IppOperation)(short)src);
            mapper.CreateIppMap<int, Finishings>((src, map) => (Finishings)src);
            mapper.CreateIppMap<int, IppStatusCode>((src, map) => (IppStatusCode)src);
            mapper.CreateIppMap<int, JobState>((src, map) => (JobState)src);
            mapper.CreateIppMap<int, Orientation>((src, map) => (Orientation)src);
            mapper.CreateIppMap<int, PrinterState>((src, map) => (PrinterState)src);
            mapper.CreateIppMap<int, PrintQuality>((src, map) => (PrintQuality)src);
            mapper.CreateIppMap<int, ResolutionUnit>((src, map) => (ResolutionUnit)src);
            mapper.CreateIppMap<int, PrinterType>((src, map) => (PrinterType)src);
            mapper.CreateIppMap<string, IppVersion>( ( src, map ) => new IppVersion( src ) );
            mapper.CreateIppMap<NoValue, int>((src, map) => 0);
            mapper.CreateIppMap<NoValue, int?>( ( src, map ) => null );
            mapper.CreateIppMap<NoValue, bool>((src, map) => false);
            mapper.CreateIppMap<NoValue, bool?>((src, map) => null);
            mapper.CreateIppMap<NoValue, JobState>( ( src, jobs) => 0);

            //All name parameters can come as StringWithLanguage or string
            //mappers for string\language mapping 
            mapper.CreateIppMap<StringWithLanguage, string>((src, map) => src.Value);
            mapper.CreateIppMap<string, StringWithLanguage?>((src, map) => null);

            ConfigureKeyword( mapper, JobHoldUntil.Unsupported );
            ConfigureKeyword( mapper, MultipleDocumentHandling.Unsupported );
            ConfigureKeyword( mapper, Sides.Unsupported );
            ConfigureKeyword( mapper, JobSheets.Unsupported );
            ConfigureKeyword( mapper, Compression.Unsupported );
            ConfigureKeyword( mapper, PrintScaling.Unsupported );
            ConfigureKeyword( mapper, WhichJobs.Unsupported );
            ConfigureKeyword( mapper, JobStateReason.Unsupported );
            ConfigureKeyword( mapper, UriScheme.Unsupported );
            ConfigureKeyword( mapper, UriAuthentication.Unsupported );
            ConfigureKeyword( mapper, UriSecurity.Unsupported );
            ConfigureKeyword( mapper, MediaSource.Unsupported );
            ConfigureKeyword( mapper, MediaSourceFeedDirection.Unsupported );
            ConfigureKeyword( mapper, MediaCoating.Unsupported );
            ConfigureKeyword( mapper, MediaGrain.Unsupported );
            ConfigureKeyword( mapper, MediaPrePrinted.Unsupported );
            ConfigureKeyword( mapper, MediaRecycled.Unsupported );
            ConfigureKeyword( mapper, MediaTooth.Unsupported );
            ConfigureKeyword( mapper, PrintColorMode.Unsupported );
        }

        private static void ConfigureKeyword<T>( IMapperConstructor map, T defaultValue ) where T : struct, Enum
        {
            map.CreateIppMap<string, T>( ( src, ctx ) => Enum.TryParse(src.ConvertKebabCaseToCamelCase(), false, out T value ) ? value : defaultValue );
            map.CreateIppMap<T, string>( ( src, ctx ) => src.ToString().ConvertCamelCaseToKebabCase() );
        }
    }
}
