using System;
using System.Linq;

namespace SharpIpp.Protocol.Models;

public readonly struct IppVersion : IEquatable<IppVersion>, IComparable<IppVersion>
{
    public byte Major { get; }
    public byte Minor { get; }

    public IppVersion()
    {
        Major = 1;
        Minor = 1;
    }

    public IppVersion( short int16BigEndian )
    {
        byte[] bytes = BitConverter.GetBytes( int16BigEndian );
        Major = bytes[ 1 ];
        Minor = bytes[ 0 ];
    }

    public IppVersion( byte major, byte minor )
    {
        Major = major;
        Minor = minor;
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
    }

    public static IppVersion CUPS10 { get; } = new( 1, 2 );

    public override string ToString() => $"{Major}.{Minor}";

    public decimal ToDecimal() => Major + Minor / 100m;

    public short ToInt16BigEndian() => BitConverter.ToInt16( [Minor, Major], 0 );

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
