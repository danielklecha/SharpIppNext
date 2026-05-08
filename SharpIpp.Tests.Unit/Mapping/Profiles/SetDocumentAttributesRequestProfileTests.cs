using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class SetDocumentAttributesRequestProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_SetDocumentAttributesRequest_WithDocumentName_MapsToDocumentAttributes()
    {
        var request = new SetDocumentAttributesRequest
        {
            OperationAttributes = new SetDocumentAttributesOperationAttributes
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                PrinterUri = new Uri("ipp://127.0.0.1:631/printers/p1"),
                JobId = 123,
                DocumentNumber = 1
            },
            DocumentName = "Updated Document Name",
            DocumentTemplateAttributes = new DocumentTemplateAttributes
            {
                Copies = 2
            }
        };

        var ippRequest = _mapper.Map<SetDocumentAttributesRequest, IppRequestMessage>(request);

        ippRequest.DocumentAttributes.Should().ContainSingle(x => x.Name == DocumentAttribute.DocumentName);
        var documentName = ippRequest.DocumentAttributes.Single(x => x.Name == DocumentAttribute.DocumentName);
        documentName.Tag.Should().Be(Tag.NameWithoutLanguage);
        documentName.Value.Should().Be("Updated Document Name");
    }

    [TestMethod]
    public void Map_IppRequestMessage_WithDocumentName_MapsToSetDocumentAttributesRequest()
    {
        var ippRequest = new IppRequestMessage
        {
            IppOperation = IppOperation.SetDocumentAttributes
        };
        ippRequest.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/printers/p1"),
            new IppAttribute(Tag.Integer, JobAttribute.JobId, 123),
            new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 1)
        ]);
        ippRequest.DocumentAttributes.AddRange(
        [
            new IppAttribute(Tag.NameWithoutLanguage, DocumentAttribute.DocumentName, "Updated Document Name"),
            new IppAttribute(Tag.Integer, JobAttribute.Copies, 2)
        ]);

        var request = _mapper.Map<IIppRequestMessage, SetDocumentAttributesRequest>(ippRequest);

        request.DocumentName.Should().Be("Updated Document Name");
        request.DocumentTemplateAttributes.Should().NotBeNull();
        request.DocumentTemplateAttributes!.Copies.Should().Be(2);
    }
}
