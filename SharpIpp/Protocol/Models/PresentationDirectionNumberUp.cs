namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the presentation-direction-number-up.
/// See: PWG 5100.1-2022 Section 6.19
/// </summary>
public readonly record struct PresentationDirectionNumberUp(string Value)
{
    public static readonly PresentationDirectionNumberUp TobackLtr = new("toback-ltr");
    public static readonly PresentationDirectionNumberUp TobackRtl = new("toback-rtl");
    public static readonly PresentationDirectionNumberUp TofrontLtr = new("tofront-ltr");
    public static readonly PresentationDirectionNumberUp TofrontRtl = new("tofront-rtl");
    public static readonly PresentationDirectionNumberUp TorightBtt = new("toright-btt");
    public static readonly PresentationDirectionNumberUp TorightTtb = new("toright-ttb");
    public static readonly PresentationDirectionNumberUp ToleftBtt = new("toleft-btt");
    public static readonly PresentationDirectionNumberUp ToleftTtb = new("toleft-ttb");
    public static readonly PresentationDirectionNumberUp TobottomToleft = new("tobottom-toleft");
    public static readonly PresentationDirectionNumberUp TobottomToright = new("tobottom-toright");
    public static readonly PresentationDirectionNumberUp TotopToleft = new("totop-toleft");
    public static readonly PresentationDirectionNumberUp TotopToright = new("totop-toright");

    public override string ToString() => Value;
    public static implicit operator string(PresentationDirectionNumberUp bin) => bin.Value;
    public static explicit operator PresentationDirectionNumberUp(string value) => new(value);
}
