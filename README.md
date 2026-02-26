# SharpIppNext

[![NuGet](https://img.shields.io/nuget/v/SharpIppNext.svg)](https://www.nuget.org/packages/SharpIppNext)
[![NuGet downloads](https://img.shields.io/nuget/dt/SharpIppNext.svg)](https://www.nuget.org/packages/SharpIppNext)
[![Docs](https://img.shields.io/badge/docs-Documentation-green)](https://danielklecha.github.io/SharpIppNext/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://github.com/danielklecha/SharpIppNext/blob/master/LICENSE.txt)
[![Coverage Status](https://coveralls.io/repos/github/danielklecha/SharpIppNext/badge.svg?branch=master)](https://coveralls.io/github/danielklecha/SharpIppNext?branch=master)

A .NET Standard library that can be used as an Internet Printing Protocol (IPP) client and IPP server.

## Features

- Client and server support all operations of [Internet Printing Protocol/1.1](https://tools.ietf.org/html/rfc2911).
- Client supports [CUPS-Get-Printers operation](http://www.cups.org/doc/spec-ipp.html#CUPS_GET_PRINTERS).

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

## Getting started

- An example of the IPP client is included in the solution (`SharpIpp.Samples.Client`).
- An example of the IPP server is available in the [SharpIppNextServer repository](https://github.com/danielklecha/SharpIppNextServer).

## Documentation

For full documentation and API reference, please visit [https://danielklecha.github.io/SharpIppNext/](https://danielklecha.github.io/SharpIppNext/).

## Contributing

Contributions are welcome! Please refer to the [CONTRIBUTING.md](CONTRIBUTING.md) file for more information on how to get started.

## License

`SharpIppNext` is provided as-is under the [MIT license](LICENSE.txt).
