using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the printer-output-tray collection attribute.
/// Each element describes one output tray installed in the printer.
/// See: PWG 5100.13-2023 Section 6.6.10
/// </summary>
[IppAttribute(IppAttributeNames.PrinterOutputTray)]
public class PrinterOutputTray : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type — keyword describing the output tray type.
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    public IppValue<OutputTrayType>? Type { get; set; }

    /// <summary>
    /// level — current fill level of the output tray (integer, -2 = unknown, -3 = no-value).
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    public IppValue<int>? Level { get; set; }

    /// <summary>
    /// status — current status of the output tray (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    [IppAttribute("status", Tag = Tag.Keyword)]
    public IppValue<string>? Status { get; set; }

    /// <summary>
    /// unit — unit of measure for level (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    public IppValue<CapacityUnit>? Unit { get; set; }

    /// <summary>
    /// stackingorder — stacking order of output (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    [IppAttribute("stackingorder")]
    public IppValue<StackingOrder>? StackingOrder { get; set; }

    /// <summary>
    /// pagedelivery — page delivery orientation (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    [IppAttribute("pagedelivery")]
    public IppValue<PageDelivery>? PageDelivery { get; set; }
}
