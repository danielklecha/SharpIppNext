namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-type</c> member attribute.
/// See: PWG 5100.21-2019 Section 6.8.11
/// </summary>
public readonly record struct MaterialType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly MaterialType Abs = new("abs");
    public static readonly MaterialType AbsPlus = new("abs-plus");
    public static readonly MaterialType Asa = new("asa");
    public static readonly MaterialType Chocolate = new("chocolate");
    public static readonly MaterialType Gold = new("gold");
    public static readonly MaterialType Hdpe = new("hdpe");
    public static readonly MaterialType Hips = new("hips");
    public static readonly MaterialType Nylon = new("nylon");
    public static readonly MaterialType NylonFl = new("nylon-fl");
    public static readonly MaterialType Pei = new("pei");
    public static readonly MaterialType Petg = new("petg");
    public static readonly MaterialType Pett = new("pett");
    public static readonly MaterialType Photopolymer = new("photopolymer");
    public static readonly MaterialType PhotopolymerCastable = new("photopolymer-castable");
    public static readonly MaterialType PhotopolymerDental = new("photopolymer-dental");
    public static readonly MaterialType PhotopolymerFlexible = new("photopolymer-flexible");
    public static readonly MaterialType PhotopolymerRigid = new("photopolymer-rigid");
    public static readonly MaterialType PhotopolymerTransparent = new("photopolymer-transparent");
    public static readonly MaterialType Pla = new("pla");
    public static readonly MaterialType PlaCalculated = new("pla-calculated");
    public static readonly MaterialType PlaConductive = new("pla-conductive");
    public static readonly MaterialType PlaFl = new("pla-fl");
    public static readonly MaterialType PlaHigh = new("pla-high");
    public static readonly MaterialType PlaMetallic = new("pla-metallic");
    public static readonly MaterialType PlaMagnetic = new("pla-magnetic");
    public static readonly MaterialType PlaTransparent = new("pla-transparent");
    public static readonly MaterialType Polycarbonate = new("polycarbonate");
    public static readonly MaterialType PolycarbonatePlus = new("polycarbonate-plus");
    public static readonly MaterialType Silver = new("silver");
    public static readonly MaterialType Steel = new("steel");
    public static readonly MaterialType WoodFill = new("wood-fill");

    public override string ToString() => Value;
    public static implicit operator string(MaterialType value) => value.Value;
    public static explicit operator MaterialType(string value) => new(value);
}
