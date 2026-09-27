using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>materials-col</c> member collection.
/// See: PWG 5100.21-2019 Section 8.1.3
/// </summary>
[IppAttribute(IppAttributeNames.MaterialsCol)]
public class Material : IIppCollection
{
    [Range(0, int.MaxValue)]
    public IppValue<int>? MaterialAmount { get; set; }
    public IppValue<MaterialColor>? MaterialColor { get; set; }
    [Range(0, int.MaxValue)]
    public IppValue<int>? MaterialDiameter { get; set; }
    /// <summary>
    /// The material-fill-density member attribute.
    /// See: PWG 5100.21-2019 Section 8.1.3.4
    /// </summary>
    [Range(0, 100)]
    public IppValue<int>? MaterialFillDensity { get; set; }
    public IppValue<MaterialKey>? MaterialKey { get; set; }
    public IppValue<string>? MaterialName { get; set; }
    public IppValue<MaterialPurpose[]>? MaterialPurpose { get; set; }
    [Range(1, int.MaxValue)]
    public IppValue<int>? MaterialRate { get; set; }
    public IppValue<MaterialRateUnits>? MaterialRateUnits { get; set; }
    [Range(0, int.MaxValue)]
    public IppValue<int>? MaterialShellThickness { get; set; }
    [Range(-273, int.MaxValue)]
    public IppValue<int>? MaterialTemperature { get; set; }
    public IppValue<MaterialType>? MaterialType { get; set; }
}
