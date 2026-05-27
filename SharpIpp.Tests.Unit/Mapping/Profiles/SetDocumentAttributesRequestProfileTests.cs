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
                AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
                AttributesNaturalLanguage = "en",
                PrinterUri = new Uri("ipp://127.0.0.1:631/printers/p1"),
                JobId = 123,
                DocumentNumber = 1
            },
            DocumentDescriptionAttributes = new DocumentDescriptionAttributes
            {
                DocumentName = "Updated Document Name"
            },
            DocumentTemplateAttributes = new DocumentTemplateAttributes
            {
                Copies = 2
            }
        };

        var ippRequest = _mapper.Map<SetDocumentAttributesRequest, IppRequestMessage>(request);

        ippRequest.DocumentAttributes.Should().ContainSingle(x => x.Name == IppAttributeNames.DocumentName);
        var documentName = ippRequest.DocumentAttributes.Single(x => x.Name == IppAttributeNames.DocumentName);
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
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/printers/p1"),
            new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123),
            new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, 1)
        ]);
        ippRequest.DocumentAttributes.AddRange(
        [
            new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.DocumentName, "Updated Document Name"),
            new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 2)
        ]);

        var request = _mapper.Map<IIppRequestMessage, SetDocumentAttributesRequest>(ippRequest);

        request.DocumentDescriptionAttributes.Should().NotBeNull();
        request.DocumentDescriptionAttributes!.DocumentName.Should().Be("Updated Document Name");
        request.DocumentTemplateAttributes.Should().NotBeNull();
        request.DocumentTemplateAttributes!.Copies.Should().Be(2);
    }
}
