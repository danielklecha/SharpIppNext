namespace SharpIpp.Protocol.Models;

/// <summary>
/// Identifies the natural language of the supplied document data or operation attributes.
/// </summary>
public readonly record struct NaturalLanguage(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>
    /// en
    /// </summary>
    public static readonly NaturalLanguage En = new("en");

    /// <summary>
    /// en-us
    /// </summary>
    public static readonly NaturalLanguage EnUs = new("en-us");

    /// <summary>
    /// en-gb
    /// </summary>
    public static readonly NaturalLanguage EnGb = new("en-gb");

    /// <summary>
    /// pl-PL
    /// </summary>
    public static readonly NaturalLanguage PlPl = new("pl-PL");

    /// <summary>
    /// fr-FR
    /// </summary>
    public static readonly NaturalLanguage FrFr = new("fr-FR");

    /// <summary>
    /// es-ES
    /// </summary>
    public static readonly NaturalLanguage EsEs = new("es-ES");

    /// <summary>
    /// de-DE
    /// </summary>
    public static readonly NaturalLanguage DeDe = new("de-DE");

    public override string ToString() => Value;
    public static implicit operator string(NaturalLanguage bin) => bin.Value;
    public static implicit operator NaturalLanguage(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
