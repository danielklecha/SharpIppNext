namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     https://tools.ietf.org/html/rfc2911#section-4.4.11
    /// </summary>
    public enum PrinterState
    {
        Idle = 3,
        Processing = 4,
        Stopped = 5,
    }
}
