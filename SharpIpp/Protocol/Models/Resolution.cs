using System;

namespace SharpIpp.Protocol.Models;

public readonly struct Resolution : IEquatable<Resolution>
{
    public int Width { get; }

    public int Height { get; }

    public ResolutionUnit Units { get; }

    public Resolution(int width, int height, ResolutionUnit units)
    {
        Width = width;
        Height = height;
        Units = units;
    }

    public override string ToString() =>
        $"{Width}x{Height} ({(Units == ResolutionUnit.DotsPerInch ? "dpi" : Units == ResolutionUnit.DotsPerCm ? "dpcm" : "unknown")})";

    public bool Equals(Resolution other) => Width == other.Width && Height == other.Height && Units == other.Units;

    public override bool Equals(object? obj) => obj is Resolution other && Equals(other);

    public override int GetHashCode() => (Width, Height, Units).GetHashCode();

    public void Deconstruct(out int width, out int height, out ResolutionUnit units)
    {
        width = Width;
        height = Height;
        units = Units;
    }

    public static bool operator ==(Resolution left, Resolution right) => left.Equals(right);

    public static bool operator !=(Resolution left, Resolution right) => !left.Equals(right);
}
