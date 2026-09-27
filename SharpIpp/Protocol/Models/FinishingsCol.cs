using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies detailed finishing instructions that cannot be expressed
/// by the "finishings" Job Template attribute.
/// See: PWG 5100.1-2022 Section 5.2
/// </summary>
[IppAttribute(IppAttributeNames.FinishingsCol)]
public class FinishingsCol : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.5
    /// </summary>
    public IppValue<FinishingTemplate>? FinishingTemplate { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.1
    /// </summary>
    public IppValue<Baling>? Baling { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.2
    /// </summary>
    public IppValue<Binding>? Binding { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.3
    /// </summary>
    public IppValue<Coating>? Coating { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.4
    /// </summary>
    public IppValue<Covering>? Covering { get; set; }

    /// <summary>
    /// 1setOf collection
    /// See: PWG 5100.1-2022 Section 5.2.6
    /// </summary>
    public IppValue<Folding[]>? Folding { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.7
    /// </summary>
    public IppValue<Laminating>? Laminating { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.8
    /// </summary>
    public IppValue<Punching>? Punching { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 5.2.9
    /// </summary>
    public IppValue<Stitching>? Stitching { get; set; }

    /// <summary>
    /// 1setOf collection
    /// See: PWG 5100.1-2022 Section 5.2.10
    /// </summary>
    public IppValue<Trimming[]>? Trimming { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 6.9.1
    /// </summary>
    public IppValue<ImpositionTemplate>? ImpositionTemplate { get; set; }

    /// <summary>
    /// rangeOfInteger(1:MAX)
    /// See: PWG 5100.1-2022 Section 6.9.2
    /// </summary>
    [IppAttribute("media-sheets-supported", Tag = Tag.RangeOfInteger)]
    public IppValue<Range>? MediaSheetsSupported { get; set; }

    /// <summary>
    /// collection
    /// See: PWG 5100.1-2022 Section 6.9.3
    /// </summary>
    public IppValue<MediaSize>? MediaSize { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 6.9.4
    /// </summary>
    public IppValue<Media>? MediaSizeName { get; set; }
}
