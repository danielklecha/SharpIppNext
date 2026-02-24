using SharpIpp;
using SharpIpp.Models;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

try
{
    var client = new SharpIppClient();
    var filePath = @"C:\example.pdf";
    await using var stream = File.Open(filePath, FileMode.Open);
    var printJobRequest = new PrintJobRequest
    {
        Document = stream,
        OperationAttributes = new()
        {
            PrinterUri = new Uri("ipp://localhost:631/"),
            DocumentName = "Document Name",
            DocumentFormat = "application/octet-stream",
            Compression = Compression.None,
            DocumentNaturalLanguage = "en",
            JobName = "Test Job",
            IppAttributeFidelity = false
        },
        JobTemplateAttributes = new()
        {
            Copies = 1,
            MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsCollatedCopies,
            Finishings = Finishings.None,
            PageRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
            Sides = Sides.OneSided,
            NumberUp = 1,
            OrientationRequested = Orientation.Portrait,
            PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
            PrintQuality = PrintQuality.Normal
        }
    };
    var printJobresponse = await client.PrintJobAsync(printJobRequest);
    Console.WriteLine("Success!");
}
catch (Exception ex)
{
    Console.WriteLine( ex );
}
Console.ReadKey();
