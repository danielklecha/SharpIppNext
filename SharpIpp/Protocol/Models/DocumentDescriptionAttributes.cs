using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Document Description attributes that can be set or queried.
/// See: PWG 5100.5-2024 Section 6.1
/// </summary>
public class DocumentDescriptionAttributes
{
    /// <summary>
    /// The document-name IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.1.1
    /// </summary>
    /// <code>document-name</code>
    public string? DocumentName { get; set; }
}
