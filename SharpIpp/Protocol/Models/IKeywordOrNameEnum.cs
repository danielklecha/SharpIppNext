namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents a strongly-typed IPP attribute that can be either a standard keyword (<see cref="Tag.Keyword"/>)
/// or a site- or vendor-defined name (<see cref="Tag.NameWithoutLanguage"/>).
/// </summary>
public interface IKeywordOrNameEnum : IKeywordEnum
{
    /// <summary>
    /// Gets a value indicating whether this instance represents an IPP keyword (<see cref="Tag.Keyword"/>)
    /// as opposed to an IPP name (<see cref="Tag.NameWithoutLanguage"/>).
    /// </summary>
    bool IsKeyword { get; }
}
