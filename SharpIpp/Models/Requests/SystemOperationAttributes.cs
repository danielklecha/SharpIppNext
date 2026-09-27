using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Operation attributes for IPP System Service requests that target a System object.
/// See: PWG 5100.22-2025
/// </summary>
[IppAttribute]
public class SystemOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The URI of the target System object. Used instead of <c>printer-uri</c> for System Service operations.
    /// See: PWG 5100.22-2025 Section 7.1.26
    /// </summary>
    /// <code>system-uri</code>
    [IppAttribute(IppAttributeNames.SystemUri, Tag = Tag.Uri)]
    public IppValue<Uri>? SystemUri { get; set; }

    /// <summary>
    /// The ID of the target Printer object (for single-printer operations such as Shutdown/Startup/Restart).
    /// See: PWG 5100.22-2025 Section 7.1.5
    /// </summary>
    /// <code>printer-id</code>
    [Range(1, 65535)]
    [IppAttribute(IppAttributeNames.PrinterId, Tag = Tag.Integer)]
    public IppValue<int>? PrinterId { get; set; }

    /// <summary>
    /// notify-printer-ids
    /// See: PWG 5100.22-2025 Section 7.1.1
    /// </summary>
    [Range(1, 65535)]
    [IppAttribute(IppAttributeNames.NotifyPrinterIds, Tag = Tag.Integer)]
    public IppValue<int[]>? NotifyPrinterIds { get; set; }

    /// <summary>
    /// notify-resource-id
    /// See: PWG 5100.22-2025 Section 7.1.2
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.NotifyResourceId, Tag = Tag.Integer)]
    public IppValue<int>? NotifyResourceId { get; set; }

    /// <summary>
    /// restart-get-interval
    /// See: PWG 5100.22-2025 Section 7.1.25
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.RestartGetInterval, Tag = Tag.Integer)]
    public IppValue<int>? RestartGetInterval { get; set; }

    /// <summary>
    /// which-printers
    /// See: PWG 5100.22-2025 Section 7.1.27
    /// </summary>
    [IppAttribute(IppAttributeNames.WhichPrinters, Tag = Tag.Keyword)]
    public IppValue<WhichPrinters>? WhichPrinters { get; set; }

    /// <summary>
    /// notify-system-up-time
    /// See: PWG 5100.22-2025 Section 7.11.2
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.NotifySystemUpTime, Tag = Tag.Integer)]
    public IppValue<int>? NotifySystemUpTime { get; set; }

    /// <summary>
    /// notify-system-uri
    /// See: PWG 5100.22-2025 Sections 7.10.2 and 7.11.3
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifySystemUri, Tag = Tag.Uri)]
    public IppValue<Uri>? NotifySystemUri { get; set; }

    /// <summary>
    /// notify-subscription-id — the subscription identifier for subscription-targeted operations.
    /// See: RFC 3995 Section 5.1
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.NotifySubscriptionId, Tag = Tag.Integer)]
    public IppValue<int>? NotifySubscriptionId { get; set; }

    /// <summary>
    /// notify-pull-method — the pull method for Get-Notifications; must be "ippget".
    /// See: RFC 3996 Section 5.1
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifyPullMethod, Tag = Tag.Keyword)]
    public IppValue<NotifyPullMethod>? NotifyPullMethod { get; set; }
}
