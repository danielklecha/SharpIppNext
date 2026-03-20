using System;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Base operation attributes for IPP System Service requests that target a System object.
/// See: PWG 5100.22-2025
/// </summary>
public abstract class SystemOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The URI of the target System object. Used instead of <c>printer-uri</c> for System Service operations.
    /// See: PWG 5100.22-2025 Section 7.1.26
    /// </summary>
    /// <code>system-uri</code>
    public Uri? SystemUri { get; set; }
}
