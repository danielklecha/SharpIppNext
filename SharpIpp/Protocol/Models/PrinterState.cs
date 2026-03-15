namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// See: RFC 2911 Section 4.4.11
    /// </summary>
    public enum PrinterState
    {
        Idle = 3,
        Processing = 4,
        Stopped = 5,
    }
}
