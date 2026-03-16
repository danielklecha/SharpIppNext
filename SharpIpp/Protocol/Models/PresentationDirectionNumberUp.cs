namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>presentation-direction-number-up</c> values.
/// See: PWG 5100.3-2023 Section 5.2.15
/// </summary>
public readonly record struct PresentationDirectionNumberUp(string Value)
{
    public static readonly PresentationDirectionNumberUp TorightTobottom = new("toright-tobottom");
    public static readonly PresentationDirectionNumberUp TobottomToright = new("tobottom-toright");
    public static readonly PresentationDirectionNumberUp ToleftTobottom = new("toleft-tobottom");
    public static readonly PresentationDirectionNumberUp TobottomToleft = new("tobottom-toleft");
    public static readonly PresentationDirectionNumberUp TorightTotop = new("toright-totop");
    public static readonly PresentationDirectionNumberUp TotopToright = new("totop-toright");
    public static readonly PresentationDirectionNumberUp ToleftTotop = new("toleft-totop");
    public static readonly PresentationDirectionNumberUp TotopToleft = new("totop-toleft");

    public override string ToString() => Value;
    public static implicit operator string(PresentationDirectionNumberUp bin) => bin.Value;
    public static explicit operator PresentationDirectionNumberUp(string value) => new(value);
}
