using System;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     <para>Type flags for requesting specific types of printers.</para>
    ///     <para><see href="https://www.cups.org/doc/spec-ipp.html#printer-type">See here</see> for docs</para>
    /// </summary>
    [Flags]
    public enum PrinterType
    {
        PrinterClass = 0x1,
        RemoteDestination = 0x2,
        PrintsBlack = 0x4,
        PrintsColor = 0x8,
        TwoSidedPrinting = 0x10,
        Stapler = 0x20,
        FastCopies = 0x40,
        FastCopyCollation = 0x80,
        HolePunch = 0x100,
        Cover = 0x200,
        Binding = 0x400,
        Sorting = 0x800,
        MediaToUsLegalA4 = 0x1000,
        MediaLegalToIsOcA2 = 0x2000,
        MediaOverA2 = 0x4000,
        MediaUserDefined = 0x8000,
        ImplicitClass = 0x10000,
        DefaultPrinter = 20000,
        FacsimileDevice = 0x40000,
        RejectingJobs = 0x80000,
        DeleteQueue = 0x100000,
        QueueNotShared = 0x200000,
        RequiresAuthentication = 0x400000,
        SupportsCUPSCommandFiles = 0x800000,
        AutomaticallyDescovered = 0x1000000,
        ScannerNoPrint = 0x2000000,
        ScannerPrints = 0x4000000,
        PrinterIs3D = 80000000,
    }
}
