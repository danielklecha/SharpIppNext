using System;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpIpp.Validation;

namespace SharpIpp.Models.Responses;

public class JobAttributes
{
    /// <summary>
    /// The job-uri IPP attribute.
    /// See: RFC 8011 Section 5.3.2
    /// </summary>
    /// <code>job-uri</code>
    [Obsolete("The 'job-uri' attribute is deprecated. See RFC 8011 Section 4.1.2.1.")]
    public Uri? JobUri { get; set; }
    /// <summary>
    /// The job-id IPP attribute.
    /// Type: integer(1:MAX)
    /// See: RFC 8011 Section 5.3.1
    /// </summary>
    /// <code>job-id</code>
    [Range(1, int.MaxValue)]
    public int JobId { get; set; }
    /// <summary>
    /// The job-state IPP attribute.
    /// See: RFC 8011 Section 5.3.7
    /// </summary>
    /// <code>job-state</code>
    public JobState JobState { get; set; }
    /// <summary>
    /// The job-state-reasons IPP attribute.
    /// See: RFC 8011 Section 5.3.8
    /// </summary>
    /// <code>job-state-reasons</code>
    public JobStateReason[]? JobStateReasons { get; set; }
    /// <summary>
    /// The job-state-message IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>job-state-message</code>
    public string? JobStateMessage { get; set; }
    /// <summary>
    /// The number-of-intervening-jobs IPP attribute.
    /// See: pwg5100.7-2023 Section 6.1.1
    /// </summary>
    /// <code>number-of-intervening-jobs</code>
    [Range(0, int.MaxValue)]
    public int? NumberOfInterveningJobs { get; set; }

    /// <summary>
    /// The client-info IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.7.1
    /// </summary>
    /// <code>client-info</code>
    public IReadOnlyCollection<ClientInfo>? ClientInfo { get; set; }

    /// <summary>
    /// The job-impressions-completed-col IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.7.2
    /// </summary>
    /// <code>job-impressions-completed-col</code>
    public JobCounter? JobImpressionsCompletedCol { get; set; }

    /// <summary>
    /// The job-media-sheets-completed-col IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.7.3
    /// </summary>
    /// <code>job-media-sheets-completed-col</code>
    public JobCounter? JobMediaSheetsCompletedCol { get; set; }

    /// <summary>
    /// The job-pages-completed IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.7.4
    /// </summary>
    /// <code>job-pages-completed</code>
    [Range(0, int.MaxValue)]
    public int? JobPagesCompleted { get; set; }

    /// <summary>
    /// The job-pages-completed-col IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.7.5
    /// </summary>
    /// <code>job-pages-completed-col</code>
    public JobCounter? JobPagesCompletedCol { get; set; }

    /// <summary>
    /// The job-processing-time IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.7.6
    /// </summary>
    /// <code>job-processing-time</code>
    [Range(0, int.MaxValue)]
    public int? JobProcessingTime { get; set; }

    /// <summary>
    /// The platform-temperature-actual IPP attribute.
    /// See: PWG 5100.21-2019 Section 8.2.6
    /// </summary>
    /// <code>platform-temperature-actual</code>
    [ItemRange(-273, int.MaxValue)]
    public int[]? PlatformTemperatureActual { get; set; }

    /// <summary>
    /// The chamber-humidity-actual IPP attribute.
    /// See: PWG 5100.21-2019 Section 8.2.1
    /// </summary>
    /// <code>chamber-humidity-actual</code>
    [ItemRange(0, 100)]
    public int[]? ChamberHumidityActual { get; set; }

    /// <summary>
    /// The chamber-temperature-actual IPP attribute.
    /// See: PWG 5100.21-2019 Section 8.2.2
    /// </summary>
    /// <code>chamber-temperature-actual</code>
    [ItemRange(-273, int.MaxValue)]
    public int[]? ChamberTemperatureActual { get; set; }

    /// <summary>
    /// The chamber-humidity-current IPP attribute.
    /// See: PWG 5100.21-2019 Section 8.4.1
    /// </summary>
    /// <code>chamber-humidity-current</code>
    public int? ChamberHumidityCurrent { get; set; }

    /// <summary>
    /// The chamber-temperature-current IPP attribute.
    /// See: PWG 5100.21-2019 Section 8.4.2
    /// </summary>
    /// <code>chamber-temperature-current</code>
    public int? ChamberTemperatureCurrent { get; set; }
}
