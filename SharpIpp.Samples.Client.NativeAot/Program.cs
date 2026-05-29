using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

try
{
    SharpIppClient client = new();
    string filePath = args.Length > 0 ? args[0] : @"C:\example.pdf";
    if (!File.Exists(filePath))
    {
        Console.WriteLine($"Example file not found: {filePath}. Please provide a path to a PDF file.");
        return;
    }
    await using var stream = File.Open(filePath, FileMode.Open);
    PrintJobRequest printJobRequest = new()
    {
        Document = stream,
        OperationAttributes = new()
        {
            PrinterUri = new Uri("ipp://localhost:631/"),
            DocumentName = "Document Name",
            DocumentFormat = DocumentFormat.ApplicationOctetStream,
            Compression = Compression.None,
            DocumentNaturalLanguage = "en",
            JobName = "Test Job",
            IppAttributeFidelity = false
        },
        JobTemplateAttributes = new()
        {
            Copies = 1,
            MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsCollatedCopies,
            Finishings = new[] { Finishings.None },
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
    Console.WriteLine("An error occurred:");
    Console.WriteLine(ex);
}

if (!Console.IsInputRedirected)
{
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
