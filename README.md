# SharpIppNext

[![NuGet](https://img.shields.io/nuget/v/SharpIppNext.svg)](https://www.nuget.org/packages/SharpIppNext)
[![NuGet downloads](https://img.shields.io/nuget/dt/SharpIppNext.svg)](https://www.nuget.org/packages/SharpIppNext)
[![Docs](https://img.shields.io/badge/project-docs-blue)](https://danielklecha.github.io/SharpIppNext/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://github.com/danielklecha/SharpIppNext/blob/master/LICENSE.txt)
[![Coverage Status](https://coveralls.io/repos/github/danielklecha/SharpIppNext/badge.svg?branch=master)](https://coveralls.io/github/danielklecha/SharpIppNext?branch=master)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/danielklecha/SharpIppNext)

A .NET Standard library for building Internet Printing Protocol (IPP) clients and servers.

## Features

- **IPP Version Support**: Comprehensive client and server implementation supporting IPP versions **1.0, 1.1, 2.0, 2.1, and 2.2**.
- **Specification Conformance**: Full compliance with core IPP models, binary encoding rules, and transport semantics.
- **Client & Server SDK**: Strongly-typed interfaces to build both client applications (`ISharpIppClient`) and print servers (`ISharpIppServer`).
- **Over 70 Supported Operations**: Out-of-the-box methods for standard IPP operations, plus support for sending custom/extended requests via raw message mapping.
- **System Service Support**: Broad support for IPP System Service operations defined in PWG 5100.22-2025, including system and printer resource management, power policies, and subscription operations.
- **CUPS Compatibility**: Full client-side support for CUPS-specific operations (e.g., `CUPS-Get-Printers`).
- **Two-Tier Validation**: Recursive model-level checks using standard .NET DataAnnotations alongside strict low-level RFC 8011 attribute and octet-limit validation.
- **Native AOT Compatible**: Optimized and verified for Ahead-of-Time compilation with zero reflection overhead (AOT sample projects included).
- **Strong-Named Assembly**: Available in both standard and strong-named NuGet packages.

## Installation

Install the library via NuGet:

```bash
dotnet add package SharpIppNext
```

### Strong-named version

[![NuGet](https://img.shields.io/nuget/v/SharpIppNext.StrongName.svg)](https://www.nuget.org/packages/SharpIppNext.StrongName)
[![NuGet downloads](https://img.shields.io/nuget/dt/SharpIppNext.StrongName.svg)](https://www.nuget.org/packages/SharpIppNext.StrongName)

```bash
dotnet add package SharpIppNext.StrongName
```

## Getting Started

### Quick Start

Initialize the client and send an IPP request:

```csharp
using SharpIpp;
using SharpIpp.Models.Requests;

// 1. Initialize the client
using SharpIppClient client = new();

// 2. Create a request
var request = new GetPrinterAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

// 3. Send the request
var response = await client.GetPrinterAttributesAsync(request);
Console.WriteLine($"Printer State: {response.PrinterAttributes?.PrinterState}");
```

### Examples

- Examples of the IPP client are included in the solution ([SharpIpp.Samples.Client](https://github.com/danielklecha/SharpIppNext/tree/master/SharpIpp.Samples.Client) and [SharpIpp.Samples.Client.NativeAot](https://github.com/danielklecha/SharpIppNext/tree/master/SharpIpp.Samples.Client.NativeAot)).
- An example of the IPP server is available in the [SharpIppNextServer repository](https://github.com/danielklecha/SharpIppNextServer).

### Setting the IPP Version

By default, operations use IPP version `1.1`. To target another version (such as IPP `2.0`, `2.1`, or `2.2`), set the `Version` property on your request object.

## Documentation

For full documentation and API reference, please visit [https://danielklecha.github.io/SharpIppNext/](https://danielklecha.github.io/SharpIppNext/).

## Contributing

Contributions are welcome! Please refer to the [CONTRIBUTING.md](CONTRIBUTING.md) file for more information on how to get started.

## License

`SharpIppNext` is provided as-is under the [MIT license](LICENSE.txt).

For details on third-party dependencies and their licenses, see [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt).
