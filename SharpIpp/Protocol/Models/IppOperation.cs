using System;
namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// See: RFC 2911 Section 4.4.15
    /// </summary>
    public enum IppOperation : short
    {
        /// <summary>
        /// See: RFC 8011 Section 4.1.2
        /// </summary>
        Reserved1 = 0x0000,
        /// <summary>
        /// See: RFC 8011 Section 4.1.2
        /// </summary>
        Reserved2 = 0x0001,
        /// <summary>
        /// See: RFC 2911 Section 3.2.1
        /// </summary>
        PrintJob = 0x0002,
        /// <summary>
        /// See: RFC 2911 Section 3.2.2
        /// </summary>
        [Obsolete("The 'Print-URI' operation is deprecated.")]
        PrintUri = 0x0003,
        /// <summary>
        /// See: RFC 2911 Section 3.2.3
        /// </summary>
        ValidateJob = 0x0004,
        /// <summary>
        /// See: RFC 2911 Section 3.2.4
        /// </summary>
        CreateJob = 0x0005,
        /// <summary>
        /// See: RFC 2911 Section 3.3.1
        /// </summary>
        SendDocument = 0x0006,
        /// <summary>
        /// See: RFC 2911 Section 3.3.2
        /// </summary>
        [Obsolete("The 'Send-URI' operation is deprecated.")]
        SendUri = 0x0007,
        /// <summary>
        /// See: RFC 2911 Section 3.3.3
        /// </summary>
        CancelJob = 0x0008,
        /// <summary>
        /// See: RFC 2911 Section 3.3.4
        /// </summary>
        GetJobAttributes = 0x0009,
        /// <summary>
        /// See: RFC 2911 Section 3.2.6
        /// </summary>
        GetJobs = 0x000A,
        /// <summary>
        /// See: RFC 2911 Section 3.2.5
        /// </summary>
        GetPrinterAttributes = 0x000B,
        /// <summary>
        /// See: RFC 2911 Section 3.3.5
        /// </summary>
        HoldJob = 0x000C,
        /// <summary>
        /// See: RFC 2911 Section 3.3.6
        /// </summary>
        ReleaseJob = 0x000D,
        /// <summary>
        /// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as the Restart-Job operation (Deprecated in RFC 8011).
        /// See: RFC 2911 Section 3.3.7
        /// </summary>
        [Obsolete("The 'Restart-Job' operation is deprecated. See RFC 8011 Section 4.3.7.")]
        RestartJob = 0x000E,
        /// <summary>
        /// See: RFC 8011 Section 4.1.2
        /// </summary>
        ReservedForAFutureOperation = 0x000F,
        /// <summary>
        /// See: RFC 2911 Section 3.2.7
        /// </summary>
        PausePrinter = 0x0010,
        /// <summary>
        /// See: RFC 2911 Section 3.2.8
        /// </summary>
        ResumePrinter = 0x0011,
        /// <summary>
        /// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as the Purge-Jobs operation (Deprecated in RFC 8011).
        /// See: RFC 2911 Section 3.2.9
        /// </summary>
        [Obsolete("The 'Purge-Jobs' operation is deprecated. See RFC 8011 Section 4.2.9.")]
        PurgeJobs = 0x0012,
        /// <summary>
        /// See: RFC 3380 Section 4.1
        /// </summary>
        SetPrinterAttributes = 0x0013,
        /// <summary>
        /// See: RFC 3380 Section 4.2
        /// </summary>
        SetJobAttributes = 0x0014,
        /// <summary>
        /// Get-Printer-Supported-Values Operation
        /// See: RFC 3380 Section 4.3
        /// </summary>
        GetPrinterSupportedValues = 0x0015,
        /// <summary>
        /// See: RFC 3995 Section 5.1
        /// </summary>
        CreatePrinterSubscriptions = 0x0016,
        /// <summary>
        /// See: RFC 3995 Section 5.1
        /// </summary>
        CreateJobSubscriptions = 0x0017,
        /// <summary>
        /// See: RFC 3995 Section 5.1
        /// </summary>
        GetSubscriptionAttributes = 0x0018,
        /// <summary>
        /// See: RFC 3995 Section 5.1
        /// </summary>
        GetSubscriptions = 0x0019,
        /// <summary>
        /// See: RFC 3995 Section 5.1
        /// </summary>
        RenewSubscription = 0x001A,
        /// <summary>
        /// See: RFC 3995 Section 5.1
        /// </summary>
        CancelSubscription = 0x001B,
        /// <summary>
        /// See: RFC 3995 Section 5.1
        /// </summary>
        GetNotifications = 0x001C,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.2.3
        /// </summary>
        GetResourceAttributes = 0x001E,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.7
        /// </summary>
        GetResources = 0x0020,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        EnablePrinter = 0x0022,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        DisablePrinter = 0x0023,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        PausePrinterAfterCurrentJob = 0x0024,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        HoldNewJobs = 0x0025,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        ReleaseHeldNewJobs = 0x0026,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        DeactivatePrinter = 0x0027,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        ActivatePrinter = 0x0028,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        RestartPrinter = 0x0029,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        ShutdownPrinter = 0x002A,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        StartupPrinter = 0x002B,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        CancelCurrentJob = 0x002C,
        /// <summary>
        /// Reprocess-Job Operation.
        /// OBSOLETE.
        /// See: PWG 5100.7-2023 Section 6
        /// </summary>
        [Obsolete("The 'Reprocess-Job' operation is obsolete. See PWG 5100.7-2023 Section 6.")]
        ReprocessJob = 0x002D,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        SuspendCurrentJob = 0x002E,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        ResumeJob = 0x002F,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        PromoteJob = 0x0030,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        ScheduleJobAfter = 0x0031,
        /// <summary>
        /// See: PWG 5100.5-2024 Section 5.1.1
        /// </summary>
        CancelDocument = 0x0033,
        /// <summary>
        /// See: PWG 5100.5-2024 Section 5.1.2
        /// </summary>
        GetDocumentAttributes = 0x0034,
        /// <summary>
        /// See: PWG 5100.5-2024 Section 5.2.1
        /// </summary>
        GetDocuments = 0x0035,
        /// <summary>
        /// Delete-Document Operation.
        /// OBSOLETE.
        /// See: PWG 5100.5-2024 and PWG 5100.18-2025.
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        [Obsolete("The 'Delete-Document' operation is obsolete. See PWG 5100.5-2024 and PWG 5100.18-2025.")]
        DeleteDocument = 0x0036,
        /// <summary>
        /// See: PWG 5100.5-2024 Section 5.1.3
        /// </summary>
        SetDocumentAttributes = 0x0037,
        /// <summary>
        /// See: PWG 5100.7-2023 Section 5.1
        /// </summary>
        CancelJobs = 0x0038,
        /// <summary>
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        CancelMyJobs = 0x0039,
        /// <summary>
        /// See: PWG 5100.7-2023 Section 5.4
        /// </summary>
        ResubmitJob = 0x003A,
        /// <summary>
        /// See: PWG 5100.7-2023 Section 5.3
        /// </summary>
        CloseJob = 0x003B,
        /// <summary>
        /// See: PWG 5100.13-2023 Section 5.1
        /// </summary>
        IdentifyPrinter = 0x003C,
        /// <summary>
        /// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as Validate-Document (Deprecated in PWG 5100.13-2023).
        /// See: PWG 5100.13-2023 Section 5.2
        /// </summary>
        [Obsolete("The 'Validate-Document' operation is deprecated. See PWG 5100.13-2023 Section 5.2.")]
        ValidateDocument = 0x003D,
        /// <summary>
        /// See: PWG 5100.15-2013 Section 4.2
        /// </summary>
        AddDocumentImages = 0x003E,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.1
        /// </summary>
        AcknowledgeDocument = 0x003F,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.2
        /// </summary>
        AcknowledgeIdentifyPrinter = 0x0040,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.3
        /// </summary>
        AcknowledgeJob = 0x0041,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.5
        /// </summary>
        FetchDocument = 0x0042,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.6
        /// </summary>
        FetchJob = 0x0043,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 6.1
        /// </summary>
        GetOutputDeviceAttributes = 0x0044,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.7
        /// </summary>
        UpdateActiveJobs = 0x0045,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.4
        /// </summary>
        DeregisterOutputDevice = 0x0046,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.8
        /// </summary>
        UpdateDocumentStatus = 0x0047,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.9
        /// </summary>
        UpdateJobStatus = 0x0048,
        /// <summary>
        /// See: PWG 5100.18-2025 Section 5.10
        /// </summary>
        UpdateOutputDeviceAttributes = 0x0049,
        /// <summary>
        /// See: PWG 5100.17-2014 Section 6.1
        /// </summary>
        GetNextDocumentData = 0x004A,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.1
        /// </summary>
        AllocatePrinterResources = 0x004B,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.1
        /// </summary>
        CreatePrinter = 0x004C,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.2
        /// </summary>
        DeallocatePrinterResources = 0x004D,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.3
        /// </summary>
        DeletePrinter = 0x004E,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.4
        /// </summary>
        GetPrinters = 0x004F,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.7
        /// </summary>
        ShutdownOnePrinter = 0x0050,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.8
        /// </summary>
        StartupOnePrinter = 0x0051,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.2.1
        /// </summary>
        CancelResource = 0x0052,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.2
        /// </summary>
        CreateResource = 0x0053,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.2.4
        /// </summary>
        InstallResource = 0x0054,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.2.5
        /// </summary>
        SendResourceData = 0x0055,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.2.6
        /// </summary>
        SetResourceAttributes = 0x0056,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.2.2
        /// </summary>
        CreateResourceSubscriptions = 0x0057,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.3
        /// </summary>
        CreateSystemSubscriptions = 0x0058,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.5
        /// </summary>
        DisableAllPrinters = 0x0059,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.6
        /// </summary>
        EnableAllPrinters = 0x005A,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.8
        /// </summary>
        GetSystemAttributes = 0x005B,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.9
        /// </summary>
        GetSystemSupportedValues = 0x005C,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.10
        /// </summary>
        PauseAllPrinters = 0x005D,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.11
        /// </summary>
        PauseAllPrintersAfterCurrentJob = 0x005E,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.12
        /// </summary>
        RegisterOutputDevice = 0x005F,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.13
        /// </summary>
        RestartSystem = 0x0060,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.14
        /// </summary>
        ResumeAllPrinters = 0x0061,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.15
        /// </summary>
        SetSystemAttributes = 0x0062,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.16
        /// </summary>
        ShutdownAllPrinters = 0x0063,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.3.17
        /// </summary>
        StartupAllPrinters = 0x0064,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.5
        /// </summary>
        GetPrinterResources = 0x0065,
        /// <summary>
        /// See: PWG 5100.22-2025 Section 6.1.6
        /// </summary>
        RestartOnePrinter = 0x0067,
        /// <summary>
        /// See: PWG 5100.11-2024 Section 5.1
        /// </summary>
        GetUserPrinterAttributes = 0x0066,
        /// <summary>
        /// See: CUPS Extensions
        /// </summary>
        GetCUPSPrinters = 0x4002,
    }
}
