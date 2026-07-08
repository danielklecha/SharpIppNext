namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the finisher device type.
/// See: PWG 5100.13-2023 Section 7.1
/// </summary>
public readonly record struct FinisherType(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly FinisherType Other = new("other");
    public static readonly FinisherType Unknown = new("unknown");
    public static readonly FinisherType Stitcher = new("stitcher");
    public static readonly FinisherType Folder = new("folder");
    public static readonly FinisherType Binder = new("binder");
    public static readonly FinisherType Trimmer = new("trimmer");
    public static readonly FinisherType DieCutter = new("dieCutter");
    public static readonly FinisherType Puncher = new("puncher");
    public static readonly FinisherType Wrapper = new("wrapper");
    public static readonly FinisherType Stacker = new("stacker");
    public static readonly FinisherType Inserter = new("inserter");
    public static readonly FinisherType Coiler = new("coiler");
    public static readonly FinisherType SpiralBinder = new("spiralBinder");
    public static readonly FinisherType PlasticCombBinder = new("plasticCombBinder");

    public override string ToString() => Value;
    public static implicit operator string(FinisherType bin) => bin.Value;
    public static implicit operator FinisherType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
