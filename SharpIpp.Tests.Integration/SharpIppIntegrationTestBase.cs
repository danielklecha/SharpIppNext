using Moq;
using Moq.Protected;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharpIpp.Tests.Integration;

[ExcludeFromCodeCoverage]
public abstract class SharpIppIntegrationTestBase
{
    protected static T GetSystemOperationAttributes<T>() where T : SystemOperationAttributes, new()
    {
        return new T
        {
            AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://127.0.0.1:8631/printer"),
            RequestingUserName = "integration-user",
            RequestingUserUri = new Uri("mailto:integration-user@example.com"),
            SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
            PrinterId = 77,
            NotifyPrinterIds = [77, 78],
            NotifyResourceId = 1001,
            RestartGetInterval = 5,
            WhichPrinters = WhichPrinters.All,
            NotifySystemUpTime = 12345,
            NotifySystemUri = new Uri("ipp://127.0.0.1:8631/system")
        };
    }

    protected static Mock<HttpMessageHandler> GetMockOfHttpMessageHandler(Func<Stream, CancellationToken, Task<HttpResponseMessage>> func)
    {
        Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns(async (HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                if (request.Content == null)
                {
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
                }

                using var stream = await request.Content.ReadAsStreamAsync(cancellationToken);
                return await func.Invoke(stream, cancellationToken);
            });

        return handlerMock;
    }

    protected static DocumentMetadata GetTestDocumentMetadata()
    {
        var metadata = new DocumentMetadata
        {
            // Dublin Core Elements
            Contributor = "test-contributor",
            Coverage = "test-coverage",
            Creator = "test-creator",
            Date = "test-date",
            Description = "test-description",
            Format = "test-format",
            Identifier = "test-identifier",
            Language = "test-language",
            Publisher = "test-publisher",
            Relation = "test-relation",
            Rights = "test-rights",
            Source = "test-source",
            Subject = "test-subject",
            Title = "test-title",
            Type = "test-type",

            // Dublin Core Terms
            Abstract = "test-abstract",
            AccessRights = "test-accessRights",
            AccrualMethod = "test-accrualMethod",
            AccrualPeriodicity = "test-accrualPeriodicity",
            AccrualPolicy = "test-accrualPolicy",
            Alternative = "test-alternative",
            Audience = "test-audience",
            Available = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero),
            BibliographicCitation = "test-bibliographicCitation",
            ConformsTo = "test-conformsTo",
            Created = new DateTimeOffset(2026, 1, 2, 12, 0, 0, TimeSpan.Zero),
            DateAccepted = new DateTimeOffset(2026, 1, 3, 12, 0, 0, TimeSpan.Zero),
            DateCopyrighted = new DateTimeOffset(2026, 1, 4, 12, 0, 0, TimeSpan.Zero),
            DateSubmitted = new DateTimeOffset(2026, 1, 5, 12, 0, 0, TimeSpan.Zero),
            EducationLevel = "test-educationLevel",
            Extent = "test-extent",
            HasFormat = "test-hasFormat",
            HasPart = new Uri("http://example.com/hasPart"),
            HasVersion = new Uri("http://example.com/hasVersion"),
            InstructionalMethod = "test-instructionalMethod",
            IsFormatOf = "test-isFormatOf",
            IsPartOf = "test-isPartOf",
            IsReferencedBy = "test-isReferencedBy",
            IsReplacedBy = "test-isReplacedBy",
            IsRequiredBy = "test-isRequiredBy",
            Issued = new DateTimeOffset(2026, 1, 6, 12, 0, 0, TimeSpan.Zero),
            IsVersionOf = "test-isVersionOf",
            License = "test-license",
            Mediator = "test-mediator",
            Medium = "test-medium",
            Modified = new DateTimeOffset(2026, 1, 7, 12, 0, 0, TimeSpan.Zero),
            Provenance = "test-provenance",
            References = "test-references",
            Replaces = "test-replaces",
            Requires = "test-requires",
            RightsHolder = "test-rightsHolder",
            Spatial = "test-spatial",
            TableOfContents = "test-tableOfContents",
            Temporal = new DateTimeOffset(2026, 1, 8, 12, 0, 0, TimeSpan.Zero),
            Valid = new DateTimeOffset(2026, 1, 9, 12, 0, 0, TimeSpan.Zero)
        };
        metadata.AddCustom("x-custom-meta", "custom-value");
        return metadata;
    }
}

