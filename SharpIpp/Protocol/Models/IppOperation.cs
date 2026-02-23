namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     https://tools.ietf.org/html/rfc2911#section-4.4.15
    /// </summary>
    public enum IppOperation : short
    {
        Reserved1 = 0x0000,
        Reserved2 = 0x0001,
        PrintJob = 0x0002,
        PrintUri = 0x0003,
        ValidateJob = 0x0004,
        CreateJob = 0x0005,
        SendDocument = 0x0006,
        SendUri = 0x0007,
        CancelJob = 0x0008,
        GetJobAttributes = 0x0009,
        GetJobs = 0x000A,
        GetPrinterAttributes = 0x000B,
        HoldJob = 0x000C,
        ReleaseJob = 0x000D,
        RestartJob = 0x000E,
        ReservedForAFutureOperation = 0x000F,
        PausePrinter = 0x0010,
        ResumePrinter = 0x0011,
        PurgeJobs = 0x0012,
        GetCUPSPrinters = 0x4002,
    }
}
