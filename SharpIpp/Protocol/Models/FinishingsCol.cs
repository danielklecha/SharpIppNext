namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies detailed finishing instructions that cannot be expressed
/// by the "finishings" Job Template attribute.
/// See: PWG 5100.1-2022 Section 5.2
/// </summary>
public class FinishingsCol : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsNoValue { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.5
    /// </summary>
    public FinishingTemplate? FinishingTemplate { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.1
    /// </summary>
    public Baling? Baling { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.2
    /// </summary>
    public Binding? Binding { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.3
    /// </summary>
    public Coating? Coating { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.4
    /// </summary>
    public Covering? Covering { get; set; }

    /// <summary>
    /// 1setOf collection
    /// See: PWG 5100.1-2022 Section 5.2.6
    /// </summary>
    public Folding[]? Folding { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.7
    /// </summary>
    public Laminating? Laminating { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.8
    /// </summary>
    public Punching? Punching { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.9
    /// </summary>
    public Stitching? Stitching { get; set; }

    /// <summary>
    /// 1setOf collection
    /// See: PWG 5100.1-2022 Section 5.2.10
    /// </summary>
    public Trimming[]? Trimming { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 6.9.1
    /// </summary>
    public ImpositionTemplate? ImpositionTemplate { get; set; }

    /// <summary>
    /// rangeOfInteger(1:MAX)
    /// See: PWG 5100.1-2022 Section 6.9.2
    /// </summary>
    public Range? MediaSheetsSupported { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 6.9.3
    /// </summary>
    public MediaSize? MediaSize { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 6.9.4
    /// </summary>
    public Media? MediaSizeName { get; set; }
}

