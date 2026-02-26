namespace SharpIpp.Models.Responses;

public class OperationAttributes
{
    /// <summary>
    ///     The Printer object OPTIONALLY returns this attribute.
    ///     It contains a brief message describing the status of the operation.
    /// </summary>
    public string? StatusMessage { get; set; }

    /// <summary>
    ///     The Printer object OPTIONALLY returns this attribute.
    ///     It contains additional detailed and technical information about the operation.
    /// </summary>
    public string[]? DetailedStatusMessage { get; set; }

    /// <summary>
    ///     The Printer object OPTIONALLY returns this attribute.
    ///     It provides additional information about each document access error
    ///     encountered by the Printer in a Print-URI or Send-URI operation.
    /// </summary>
    public string? DocumentAccessError { get; set; }

    /// <summary>
    ///     The Printer object MUST return this attribute.
    ///     It identifies the charset used by any 'name' and 'text' attributes
    ///     that the Printer object is returning in this response.
    ///     Defaults to "utf-8".
    /// </summary>
    public string AttributesCharset { get; set; } = "utf-8";

    /// <summary>
    ///     The Printer object MUST return this attribute.
    ///     It identifies the natural language used by any 'name' and 'text'
    ///     attributes that the Printer object is returning in this response.
    ///     Defaults to "en".
    /// </summary>
    public string? AttributesNaturalLanguage { get; set; } = "en";
}
