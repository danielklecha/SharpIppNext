using System;
using System.Collections.Generic;

using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public class GetPrinterAttributesResponse : IIppResponseMessage
    {
        /// <summary>
        ///     printer-uri-supported
        /// </summary>
        public string[]? PrinterUriSupported { get; set; }

        /// <summary>
        ///     uri-security-supported
        /// </summary>
        public UriSecurity[]? UriSecuritySupported { get; set; }

        /// <summary>
        ///     uri-authentication-supported
        /// </summary>
        public UriAuthentication[]? UriAuthenticationSupported { get; set; }

        /// <summary>
        ///     printer-name
        /// </summary>
        public string? PrinterName { get; set; }

        /// <summary>
        ///     printer-location
        /// </summary>
        public string? PrinterLocation { get; set; }

        /// <summary>
        ///     printer-info
        /// </summary>
        public string? PrinterInfo { get; set; }

        /// <summary>
        ///     printer-more-info
        /// </summary>
        public string? PrinterMoreInfo { get; set; }

        /// <summary>
        ///     printer-driver-installer
        /// </summary>
        public string? PrinterDriverInstaller { get; set; }

        /// <summary>
        ///     printer-make-and-model
        /// </summary>
        public string? PrinterMakeAndModel { get; set; }

        /// <summary>
        ///     printer-more-info-manufacturer
        /// </summary>
        public string? PrinterMoreInfoManufacturer { get; set; }

        /// <summary>
        ///     printer-state
        /// </summary>
        public PrinterState? PrinterState { get; set; }

        /// <summary>
        ///     printer-state-reasons
        /// </summary>
        public string[]? PrinterStateReasons { get; set; }

        /// <summary>
        ///     printer-state-message
        /// </summary>
        public string? PrinterStateMessage { get; set; }

        /// <summary>
        ///     ipp-versions-supported
        /// </summary>
        public IppVersion[]? IppVersionsSupported { get; set; }

        /// <summary>
        ///     operations-supported
        /// </summary>
        public IppOperation[]? OperationsSupported { get; set; }

        /// <summary>
        ///     multiple-document-jobs-supported
        /// </summary>
        public bool? MultipleDocumentJobsSupported { get; set; }

        /// <summary>
        ///     charset-configured
        /// </summary>
        public string? CharsetConfigured { get; set; }

        /// <summary>
        ///     charset-supported
        /// </summary>
        public string[]? CharsetSupported { get; set; }

        /// <summary>
        ///     natural-language-configured
        /// </summary>
        public string? NaturalLanguageConfigured { get; set; }

        /// <summary>
        ///     generated-natural-language-supported
        /// </summary>
        public string[]? GeneratedNaturalLanguageSupported { get; set; }

        /// <summary>
        ///     document-format-default
        /// </summary>
        public string? DocumentFormatDefault { get; set; }

        /// <summary>
        ///     document-format-supported
        /// </summary>
        public string[]? DocumentFormatSupported { get; set; }

        /// <summary>
        ///     printer-is-accepting-jobs
        /// </summary>
        public bool? PrinterIsAcceptingJobs { get; set; }

        /// <summary>
        ///     queued-job-count
        /// </summary>
        public int? QueuedJobCount { get; set; }

        /// <summary>
        ///     printer-message-from-operator
        /// </summary>
        public string? PrinterMessageFromOperator { get; set; }

        /// <summary>
        ///     color-supported
        /// </summary>
        public bool? ColorSupported { get; set; }

        /// <summary>
        ///     reference-uri-schemes-supported
        /// </summary>
        public UriScheme[]? ReferenceUriSchemesSupported { get; set; }

        /// <summary>
        ///     pdl-override-supported
        /// </summary>
        public string? PdlOverrideSupported { get; set; }

        /// <summary>
        ///     printer-up-time
        /// </summary>
        public int? PrinterUpTime { get; set; }

        /// <summary>
        ///     printer-current-time
        /// </summary>
        public DateTimeOffset? PrinterCurrentTime { get; set; }

        /// <summary>
        ///     multiple-operation-time-out
        /// </summary>
        public int? MultipleOperationTimeOut { get; set; }

        /// <summary>
        ///     compression-supported
        /// </summary>
        public Compression[]? CompressionSupported { get; set; }

        /// <summary>
        ///     job-k-octets-supported
        /// </summary>
        public Range? JobKOctetsSupported { get; set; }

        /// <summary>
        ///     jpeg-k-octets-supported
        /// </summary>
        public Range? JpegKOctetsSupported { get; set; }

        /// <summary>
        ///     pdf-k-octets-supported
        /// </summary>
        public Range? PdfKOctetsSupported { get; set; }

        /// <summary>
        ///     job-impressions-supported
        /// </summary>
        public Range? JobImpressionsSupported { get; set; }

        /// <summary>
        ///     job-media-sheets-supported
        /// </summary>
        public Range? JobMediaSheetsSupported { get; set; }

        /// <summary>
        ///     pages-per-minute
        /// </summary>
        public int? PagesPerMinute { get; set; }

        /// <summary>
        ///     pages-per-minute-color
        /// </summary>
        public int? PagesPerMinuteColor { get; set; }

        public PrintScaling? PrintScalingDefault { get; set; }

        public PrintScaling[]? PrintScalingSupported { get; set; }

        public IppVersion Version { get; set; } = IppVersion.V1_1;

        public IppStatusCode StatusCode { get; set; }

        public int RequestId { get; set; }

        public List<IppSection> Sections { get; } = new List<IppSection>();

        public string? MediaDefault { get; set; }

        public string[]? MediaSupported { get; set; }

        public Sides? SidesDefault { get; set; }

        public Sides[]? SidesSupported { get; set; }

        public Finishings? FinishingsDefault { get; set; }

        public Finishings[]? FinishingsSupported { get; set; }

        public Resolution? PrinterResolutionDefault { get; set; }

        public Resolution[]? PrinterResolutionSupported { get; set; }

        public PrintQuality? PrintQualityDefault { get; set; }

        public PrintQuality[]? PrintQualitySupported { get; set; }

        public int? JobPriorityDefault { get; set; }

        public int? JobPrioritySupported { get; set; }

        public int? CopiesDefault { get; set; }

        public Range? CopiesSupported { get; set; }

        public Orientation? OrientationRequestedDefault { get; set; }

        public Orientation[]? OrientationRequestedSupported { get; set; }

        public bool? PageRangesSupported { get; set; }
        public JobHoldUntil[]? JobHoldUntilSupported { get; set; }
        public JobHoldUntil? JobHoldUntilDefault { get; set; }

        public string? OutputBinDefault { get; set; }

        public string[]? OutputBinSupported { get; set; }
        public MediaCol? MediaColDefault { get; set; }

        public PrintColorMode? PrintColorModeDefault { get; set; }
        public PrintColorMode[]? PrintColorModeSupported { get; set; }

        public WhichJobs[]? WhichJobsSupported { get; set; }
    }
}
