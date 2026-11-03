# Agent Integration Notes for SharpIpp

This file provides architectural pointers and development workflows for AI and code agents working in this repository.

## Documentation & Specifications

To avoid duplication, full documentation is maintained in the [`docs`](docs) directory:
- **Client Operations & Examples:** See [`docs/Content/Client/Operations.md`](docs/Content/Client/Operations.md) and individual operation guides in [`docs/Content/Client/`](docs/Content/Client/).
- **Server Implementation:** See [`docs/Content/Server/index.md`](docs/Content/Server/index.md).
- **Client Nuances & Best Practices:** See [`docs/Content/Client/ConfigurationAndBestPractices.md`](docs/Content/Client/ConfigurationAndBestPractices.md).
- **Specification Corpus:** Full RFCs and PWG specifications (through PWG 5100.22-2025) are located in [`docs/Content/Specification/`](docs/Content/Specification/).

## Core Source of Truth

- **Client Contract:** [`SharpIpp/ISharpIppClient.cs`](SharpIpp/ISharpIppClient.cs) and `SharpIppClient.*.cs`
- **Server Contract & Dispatch:** [`SharpIpp/ISharpIppServer.cs`](SharpIpp/ISharpIppServer.cs) and [`SharpIpp/SharpIppServer.cs`](SharpIpp/SharpIppServer.cs)
- **Operation Enumeration:** [`SharpIpp/Protocol/Models/IppOperation.cs`](SharpIpp/Protocol/Models/IppOperation.cs)
- **Mapping Profiles:** `SharpIpp/Mapping/Profiles/Requests` and `SharpIpp/Mapping/Profiles/Responses`
- **Tests:** `SharpIpp.Tests.Unit` and `SharpIpp.Tests.Integration`

## Agent Workflow for Spec-Parity Changes

When adding or updating an IPP operation or attribute, implement parity end-to-end:

1. Confirm or add the operation ID in [`IppOperation.cs`](SharpIpp/Protocol/Models/IppOperation.cs).
2. Create request and response model types in `SharpIpp.Models`.
3. Add client contract method in [`ISharpIppClient`](SharpIpp/ISharpIppClient.cs) and implementation in `SharpIppClient.*.cs`.
4. Add request/response AutoMapper profiles.
5. Add server dispatch mapping in [`SharpIppServer.ReceiveRequestAsync`](SharpIpp/SharpIppServer.cs).
6. Add unit and integration tests (mapping, validation, and round-trip tests).
7. Update documentation in [`docs/Content/Client/`](docs/Content/Client/) and [`docs/Content/Client/Operations.md`](docs/Content/Client/Operations.md).

## Practical Guidance

- Prefer small, focused patches and preserve naming consistency with existing IPP models.
- Maintain bidirectional mapping symmetry (read and write) for all protocol attributes.
- **Avoid Lambdas & Closures on Hot Paths**: In message parsing, mapping, and validation pipelines, avoid lambdas, `Func<...>`, `Action<...>`, and closure allocations:
  - Prefer the non-allocating `Try*` pattern (`if (!TryValidate*(..., out var error)) throw ...`) over callback-based validation methods.
  - On the happy path, validation and mapping should incur zero delegate or closure allocations to maintain optimal performance and compatibility with UWP, .NET Native, and NativeAOT.
- Verify behavior against unit and integration tests before finishing.
