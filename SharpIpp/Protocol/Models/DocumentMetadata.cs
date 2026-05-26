using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents Dublin Core or vendor-specific document metadata keywords and their values.
/// See: PWG 5100.13-2023 Section 6.1.1
/// </summary>
public class DocumentMetadata : IppStructuredString
{
    public DocumentMetadata() : base(StringComparer.Ordinal)
    {
    }

    public DocumentMetadata(IDictionary<string, string> dictionary) : base(dictionary, StringComparer.Ordinal)
    {
    }

    public void AddCustom(string key, string value)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));
        if (!key.StartsWith("x-", StringComparison.Ordinal))
            throw new ArgumentException("Custom metadata keyword must start with 'x-'.", nameof(key));
        if (!IsValidDocumentMetadataKeyword(key))
            throw new ArgumentException($"Custom metadata keyword '{key}' is invalid.", nameof(key));
        Dictionary[key] = value;
    }

    // Dublin Core Elements
    public string? Contributor { get => Get("contributor"); set => Set("contributor", value); }
    public string? Coverage { get => Get("coverage"); set => Set("coverage", value); }
    public string? Creator { get => Get("creator"); set => Set("creator", value); }
    public string? Date { get => Get("date"); set => Set("date", value); }
    public string? Description { get => Get("description"); set => Set("description", value); }
    public string? Format { get => Get("format"); set => Set("format", value); }
    public string? Identifier { get => Get("identifier"); set => Set("identifier", value); }
    public string? Language { get => Get("language"); set => Set("language", value); }
    public string? Publisher { get => Get("publisher"); set => Set("publisher", value); }
    public string? Relation { get => Get("relation"); set => Set("relation", value); }
    public string? Rights { get => Get("rights"); set => Set("rights", value); }
    public string? Source { get => Get("source"); set => Set("source", value); }
    public string? Subject { get => Get("subject"); set => Set("subject", value); }
    public string? Title { get => Get("title"); set => Set("title", value); }
    public string? Type { get => Get("type"); set => Set("type", value); }

    // Dublin Core Terms
    public string? Abstract { get => Get("abstract"); set => Set("abstract", value); }
    public string? AccessRights { get => Get("accessRights"); set => Set("accessRights", value); }
    public string? AccrualMethod { get => Get("accrualMethod"); set => Set("accrualMethod", value); }
    public string? AccrualPeriodicity { get => Get("accrualPeriodicity"); set => Set("accrualPeriodicity", value); }
    public string? AccrualPolicy { get => Get("accrualPolicy"); set => Set("accrualPolicy", value); }
    public string? Alternative { get => Get("alternative"); set => Set("alternative", value); }
    public string? Audience { get => Get("audience"); set => Set("audience", value); }
    public DateTimeOffset? Available { get => GetDateTimeOffset("available"); set => SetDateTimeOffset("available", value); }
    public string? BibliographicCitation { get => Get("bibliographicCitation"); set => Set("bibliographicCitation", value); }
    public string? ConformsTo { get => Get("conformsTo"); set => Set("conformsTo", value); }
    public DateTimeOffset? Created { get => GetDateTimeOffset("created"); set => SetDateTimeOffset("created", value); }
    public DateTimeOffset? DateAccepted { get => GetDateTimeOffset("dateAccepted"); set => SetDateTimeOffset("dateAccepted", value); }
    public DateTimeOffset? DateCopyrighted { get => GetDateTimeOffset("dateCopyrighted"); set => SetDateTimeOffset("dateCopyrighted", value); }
    public DateTimeOffset? DateSubmitted { get => GetDateTimeOffset("dateSubmitted"); set => SetDateTimeOffset("dateSubmitted", value); }
    public string? EducationLevel { get => Get("educationLevel"); set => Set("educationLevel", value); }
    public string? Extent { get => Get("extent"); set => Set("extent", value); }
    public string? HasFormat { get => Get("hasFormat"); set => Set("hasFormat", value); }
    public Uri? HasPart { get => GetUri("hasPart"); set => SetUri("hasPart", value); }
    public Uri? HasVersion { get => GetUri("hasVersion"); set => SetUri("hasVersion", value); }
    public string? InstructionalMethod { get => Get("instructionalMethod"); set => Set("instructionalMethod", value); }
    public string? IsFormatOf { get => Get("isFormatOf"); set => Set("isFormatOf", value); }
    public string? IsPartOf { get => Get("isPartOf"); set => Set("isPartOf", value); }
    public string? IsReferencedBy { get => Get("isReferencedBy"); set => Set("isReferencedBy", value); }
    public string? IsReplacedBy { get => Get("isReplacedBy"); set => Set("isReplacedBy", value); }
    public string? IsRequiredBy { get => Get("isRequiredBy"); set => Set("isRequiredBy", value); }
    public DateTimeOffset? Issued { get => GetDateTimeOffset("issued"); set => SetDateTimeOffset("issued", value); }
    public string? IsVersionOf { get => Get("isVersionOf"); set => Set("isVersionOf", value); }
    public string? License { get => Get("license"); set => Set("license", value); }
    public string? Mediator { get => Get("mediator"); set => Set("mediator", value); }
    public string? Medium { get => Get("medium"); set => Set("medium", value); }
    public DateTimeOffset? Modified { get => GetDateTimeOffset("modified"); set => SetDateTimeOffset("modified", value); }
    public string? Provenance { get => Get("provenance"); set => Set("provenance", value); }
    public string? References { get => Get("references"); set => Set("references", value); }
    public string? Replaces { get => Get("replaces"); set => Set("replaces", value); }
    public string? Requires { get => Get("requires"); set => Set("requires", value); }
    public string? RightsHolder { get => Get("rightsHolder"); set => Set("rightsHolder", value); }
    public string? Spatial { get => Get("spatial"); set => Set("spatial", value); }
    public string? TableOfContents { get => Get("tableOfContents"); set => Set("tableOfContents", value); }
    public DateTimeOffset? Temporal { get => GetDateTimeOffset("temporal"); set => SetDateTimeOffset("temporal", value); }
    public DateTimeOffset? Valid { get => GetDateTimeOffset("valid"); set => SetDateTimeOffset("valid", value); }

    public override HashSet<string> StandardKeys { get; } = new(StringComparer.Ordinal)
    {
        // dc-elements
        "contributor", "coverage", "creator", "date", "description", "format", "identifier", "language", "publisher", "relation", "rights", "source", "subject", "title", "type",
        // dc-terms
        "abstract", "accessRights", "accrualMethod", "accrualPeriodicity", "accrualPolicy", "alternative", "audience", "available", "bibliographicCitation", "conformsTo", "created", "dateAccepted", "dateCopyrighted", "dateSubmitted", "educationLevel", "extent", "hasFormat", "hasPart", "hasVersion", "instructionalMethod", "isFormatOf", "isPartOf", "isReferencedBy", "isReplacedBy", "isRequiredBy", "issued", "isVersionOf", "license", "mediator", "medium", "modified", "provenance", "references", "replaces", "requires", "rightsHolder", "spatial", "tableOfContents", "temporal", "valid"
    };

    private static readonly DocumentMetadata DummyInstance = new();

    /// <summary>
    /// Validates that all keywords in the metadata are either standard Dublin Core keywords
    /// or custom keywords starting with "x-", and that all values contain no control characters.
    /// Throws <see cref="ValidationException"/> if validation fails.
    /// </summary>
    public override void Validate()
    {
        foreach (var kvp in Dictionary)
        {
            var key = kvp.Key;
            var val = kvp.Value;

            if (string.IsNullOrEmpty(key))
            {
                throw new ValidationException("has an empty keyword.");
            }

            if (!IsValidDocumentMetadataKeyword(key))
            {
                throw new ValidationException($"has invalid keyword '{key}'.");
            }

            var entry = $"{key}={val}";
            if (!IsValidUtf8String(entry))
            {
                throw new ValidationException($"has invalid value for keyword '{key}': must be valid UTF-8 and contain no control characters.");
            }
        }
    }

    internal static bool IsValidDocumentMetadataKeyword(string keyword)
    {
        if (DummyInstance.StandardKeys.Contains(keyword))
            return true;

        if (keyword.StartsWith("x-", StringComparison.Ordinal))
        {
            var suffix = keyword.Substring(2);
            if (suffix.Length == 0)
                return false;
            foreach (var c in suffix)
            {
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '.' || c == '-' || c == '_'))
                    return false;
            }
            return true;
        }

        return false;
    }
}
