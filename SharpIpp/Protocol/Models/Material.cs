namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>materials-col</c> member collection.
/// See: PWG 5100.21-2019 Section 6.8.11
/// </summary>
public class Material : IIppCollection
{
    public bool IsNoValue { get; set; }
    public int? MaterialAmount { get; set; }
    public string? MaterialColor { get; set; }
    public int? MaterialDiameter { get; set; }
    public int? MaterialFillDensity { get; set; }
    public string? MaterialKey { get; set; }
    public string? MaterialName { get; set; }
    public string? MaterialPurpose { get; set; }
    public int? MaterialRate { get; set; }
    public string? MaterialRateUnits { get; set; }
    public int? MaterialShellThickness { get; set; }
    public int? MaterialTemperature { get; set; }
    public string? MaterialType { get; set; }
}
