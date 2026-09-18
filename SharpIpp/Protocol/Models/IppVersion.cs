using System;
using System.Linq;

namespace SharpIpp.Protocol.Models;

public readonly struct IppVersion : IEquatable<IppVersion>, IComparable<IppVersion>, INoValue
{
    public byte Major { get; }
    public byte Minor { get; }
    public bool IsValue { get; }

    public IppVersion()
    {
        Major = 1;
        Minor = 1;
        IsValue = true;
    }

    public IppVersion( short int16BigEndian )
    {
        Major = (byte)(int16BigEndian >> 8);
        Minor = (byte)(int16BigEndian & 0xFF);
        IsValue = true;
    }

    public IppVersion( byte major, byte minor, bool isValue = true )
    {
        Major = major;
        Minor = minor;
        IsValue = isValue;
    }

    public IppVersion( string version )
    {
        if ( string.IsNullOrEmpty( version ) )
        {
            throw new ArgumentNullException( nameof(version) );
        }

        var parts = version.Split( '.' ).Select( byte.Parse ).ToList();
        Major = parts.FirstOrDefault();
        Minor = parts.Skip( 1 ).FirstOrDefault();
        IsValue = true;
    }

    public static IppVersion CUPS10 { get; } = new( 1, 2 );

    public override string ToString() => $"{Major}.{Minor}";

    public static explicit operator string(IppVersion version) => version.ToString();
    public static explicit operator IppVersion(string version) => new(version);

    public decimal ToDecimal() => Major + Minor / 100m;

    public short ToInt16BigEndian() => (short)((Major << 8) | Minor);

    public bool Equals( IppVersion other )
    {
        return Major == other.Major && Minor == other.Minor;
    }

    public override bool Equals( object? obj )
    {
        return obj is IppVersion other && Equals( other );
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Major.GetHashCode() * 397) ^ Minor.GetHashCode();
        }
    }

    public int CompareTo( IppVersion other )
    {
        int majorComparison = Major.CompareTo( other.Major );
        if ( majorComparison != 0 ) return majorComparison;
        return Minor.CompareTo( other.Minor );
    }

    public static bool operator ==( IppVersion left, IppVersion right )
    {
        return left.Equals( right );
    }

    public static bool operator !=( IppVersion left, IppVersion right )
    {
        return !left.Equals( right );
    }

    public static bool operator <( IppVersion left, IppVersion right )
    {
        return left.CompareTo( right ) < 0;
    }

    public static bool operator >( IppVersion left, IppVersion right )
    {
        return left.CompareTo( right ) > 0;
    }

    public static bool operator <=( IppVersion left, IppVersion right )
    {
        return left.CompareTo( right ) <= 0;
    }

    public static bool operator >=( IppVersion left, IppVersion right )
    {
        return left.CompareTo( right ) >= 0;
    }
}
