using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class ValidateDocumentProfileTest
{
    [TestMethod]
    public void Map_ValidateDocumentRequestToIppRequestMessage_ShouldSetValidateDocumentOperationAndMapDocumentAttributes()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        var request = new ValidateDocumentRequest
        {
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentFormat = "application/pdf",
                DocumentMetadata = ["x-meta-1=val-1"],
            },
            DocumentTemplateAttributes = new()
            {
                Copies = 2,
            }
        };

        var result = mapper.Map<ValidateDocumentRequest, IppRequestMessage>(request);

        result.IppOperation.Should().Be(IppOperation.ValidateDocument);
        result.DocumentAttributes.Should().NotBeEmpty();
        result.OperationAttributes.Should().Contain(x => x.Name == IppAttributeNames.DocumentFormat);
        result.OperationAttributes.Should().Contain(x => x.Name == IppAttributeNames.DocumentMetadata);
    }

    [TestMethod]
    public void Map_IppRequestMessageToValidateDocumentRequest_ShouldNotThrow()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        var request = new IppRequestMessage
        {
            IppOperation = IppOperation.ValidateDocument,
        };
        request.OperationAttributes.Add(new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"));
        request.OperationAttributes.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/"));

        Action act = () => mapper.Map<IIppRequestMessage, ValidateDocumentRequest>(request);

        act.Should().NotThrow();
    }
}
