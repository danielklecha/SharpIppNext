namespace SharpIpp.Protocol.Models
{
    public enum PrintQuality
    {
        Unsupported,

        /// <summary>
        ///     lowest quality available on the printer
        /// </summary>
        Draft = 3,

        /// <summary>
        ///     normal or intermediate quality on the printer
        /// </summary>
        Normal = 4,

        /// <summary>
        ///     highest quality available on the printer
        /// </summary>
        High = 5,
    }
}
