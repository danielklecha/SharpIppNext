namespace SharpIpp.Protocol.Models;

/// <summary>
/// The "page-order-received" Job Template attribute (section 6.3.1) specifies the order of pages
/// recorded in the Document data.
/// </summary>
public enum PageOrderReceived
{
    /// <summary>
    /// The Document data contains pages in the order 1, 2, 3, ...
    /// </summary>
    OneToNOrder = 3,

    /// <summary>
    /// The Document data contains pages in the order n, n-1, n-2, ...
    /// </summary>
    NTo1Order = 4
}
