# Advanced Replacing Attributes

SharpIppNext translates strongly-typed request models into raw IPP requests (`IIppRequestMessage`) under the hood. In some advanced scenarios, you might need to modify the generated raw request before it is sent to the printer. This is useful when you want to change out an attribute that the client generates automatically.

## Example: Replacing `document-name`

By default, string attributes like `document-name` are often sent with the `nameWithoutLanguage` or `textWithoutLanguage` tag. If you need to send a `nameWithLanguage` or `textWithLanguage` instead, you can intercept the raw request, find the auto-generated attribute, remove it, and add your own `StringWithLanguage` version.

> [!NOTE]
> Replacing attributes with language-specific variants (like `nameWithLanguage` or `textWithLanguage`) might not be supported by most printers for all attributes. Check your target printer's documentation or the IPP specification. This approach, however, can be used for replacing any existing auto-generated attribute.

Here is an example demonstrating how to find the `document-name` attribute and replace it:

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();
var printerUri = new Uri("ipp://localhost:631/printers/my-printer");

// 1. Initialize a strongly-typed request
var sendDocumentRequest = new SendDocumentRequest
{
    PrinterUri = printerUri,
    JobId = 123,
    DocumentName = "My Invoice",
    // DocumentStream = ...
};

// 2. Create the raw request message
IIppRequestMessage rawRequest = client.CreateRawRequest(sendDocumentRequest);

// 3. Find the existing 'document-name' attribute and replace it in the same place
for (var i = 0; i < rawRequest.OperationAttributes.Count; i++)
{
    var attribute = rawRequest.OperationAttributes[i];
    if (attribute.Name == "document-name")
    {
        // 4. Replace the old auto-generated attribute with a specific language
        rawRequest.OperationAttributes[i] = new IppAttribute
        {
            Name = attribute.Name,
            Tag = attribute.Tag switch
            {
                IppTag.NameWithoutLanguage => IppTag.NameWithLanguage,
                IppTag.TextWithoutLanguage => IppTag.TextWithLanguage,
                _ => throw new InvalidOperationException("Unsupported tag: " + attribute.Tag)
            },
            Value = new StringWithLanguage("en-US", attribute.Value as string ?? string.Empty)
        };
        break;
    }
}

// 6. Send the raw request and get the raw response
IIppResponseMessage rawResponse = await client.SendAsync(printerUri, rawRequest);

// 7. (Optional) Read the standard response
var response = client.CreateResponse<SendDocumentResponse>(rawResponse);

Console.WriteLine($"Jobs processed: {response.JobId}");
```
