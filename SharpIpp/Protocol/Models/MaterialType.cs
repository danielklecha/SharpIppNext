namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-type</c> member attribute.
/// See: PWG 5100.21-2019 Section 8.1.3.16
/// </summary>
public readonly record struct MaterialType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>ABS (Acrylonitrile Butadiene Styrene) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Abs = new("abs");
    /// <summary>ABS+ (enhanced ABS) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType AbsPlus = new("abs-plus");
    /// <summary>ASA (Acrylonitrile Styrene Acrylate) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Asa = new("asa");
    /// <summary>Chocolate material. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Chocolate = new("chocolate");
    /// <summary>Gold material. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Gold = new("gold");
    /// <summary>HDPE (High-Density Polyethylene) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Hdpe = new("hdpe");
    /// <summary>HIPS (High Impact Polystyrene) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Hips = new("hips");
    /// <summary>Nylon filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Nylon = new("nylon");
    /// <summary>Nylon flexible (FL) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType NylonFl = new("nylon-fl");
    /// <summary>PEI (Polyetherimide) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Pei = new("pei");
    /// <summary>PETG (Polyethylene Terephthalate Glycol) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Petg = new("petg");
    /// <summary>PETT (Polyethylene co-Trimethylene Terephthalate) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Pett = new("pett");
    /// <summary>Photopolymer resin. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Photopolymer = new("photopolymer");
    /// <summary>Castable photopolymer resin. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PhotopolymerCastable = new("photopolymer-castable");
    /// <summary>Dental photopolymer resin. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PhotopolymerDental = new("photopolymer-dental");
    /// <summary>Flexible photopolymer resin. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PhotopolymerFlexible = new("photopolymer-flexible");
    /// <summary>Rigid photopolymer resin. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PhotopolymerRigid = new("photopolymer-rigid");
    /// <summary>Transparent photopolymer resin. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PhotopolymerTransparent = new("photopolymer-transparent");
    /// <summary>PLA (Polylactic Acid) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Pla = new("pla");
    /// <summary>PLA calculated filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PlaCalculated = new("pla-calculated");
    /// <summary>Conductive PLA filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PlaConductive = new("pla-conductive");
    /// <summary>PLA flexible (FL) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PlaFl = new("pla-fl");
    /// <summary>High-temperature PLA filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PlaHigh = new("pla-high");
    /// <summary>Metallic PLA filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PlaMetallic = new("pla-metallic");
    /// <summary>Magnetic PLA filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PlaMagnetic = new("pla-magnetic");
    /// <summary>Transparent PLA filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PlaTransparent = new("pla-transparent");
    /// <summary>Polycarbonate filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Polycarbonate = new("polycarbonate");
    /// <summary>Polycarbonate+ (enhanced polycarbonate) filament. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType PolycarbonatePlus = new("polycarbonate-plus");
    /// <summary>Silver material. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Silver = new("silver");
    /// <summary>Steel material. See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType Steel = new("steel");
    /// <summary>Wood-fill filament (PLA with wood fiber). See: PWG 5100.21-2019 Section 8.1.3.16</summary>
    public static readonly MaterialType WoodFill = new("wood-fill");

    public override string ToString() => Value;
    public static implicit operator string(MaterialType value) => value.Value;
    public static implicit operator MaterialType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
