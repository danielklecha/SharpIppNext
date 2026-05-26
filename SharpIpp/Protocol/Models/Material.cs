namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>materials-col</c> member collection.
/// See: PWG 5100.21-2019 Section 8.1.3
/// </summary>
public class Material : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public int? MaterialAmount { get; set; }
    public MaterialColor? MaterialColor { get; set; }
    public int? MaterialDiameter { get; set; }
    /// <summary>
    /// The material-fill-density member attribute.
    /// See: PWG 5100.21-2019 Section 8.1.3.4
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(0, 100)]
    public int? MaterialFillDensity { get; set; }
    public MaterialKey? MaterialKey { get; set; }
    public string? MaterialName { get; set; }
    public MaterialPurpose[]? MaterialPurpose { get; set; }
    public int? MaterialRate { get; set; }
    public MaterialRateUnits? MaterialRateUnits { get; set; }
    public int? MaterialShellThickness { get; set; }
    public int? MaterialTemperature { get; set; }
    public MaterialType? MaterialType { get; set; }
}
