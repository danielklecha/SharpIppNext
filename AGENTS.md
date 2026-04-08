# Agent Integration Notes for SharpIpp

This file is the human-readable integration guide for AI/code agents working in this repository.
It summarizes verified specification coverage, highlights known parity gaps, and defines a safe workflow
for implementing missing IPP/PWG features.

## Scope and source of truth

- Primary runtime API surface: `SharpIpp/ISharpIppClient.cs` and `SharpIpp/ISharpIppServer.cs`
- Operation identifiers: `SharpIpp/Protocol/Models/IppOperation.cs`
- Request/response wiring: `SharpIpp/Mapping/Profiles/Requests` and `SharpIpp/Mapping/Profiles/Responses`
- Server dispatch: `SharpIpp/SharpIppServer.cs`
- Validation tests:
  - unit: `SharpIpp.Tests.Unit`
  - integration: `SharpIpp.Tests.Integration`
- Specification document corpus: `docs/Content/Specification` (includes RFCs and PWG specs through 5100.22-2025)

## Verified specification coverage snapshot

- Legacy baseline remains RFC 2911 + CUPS operations (see `README.md`).
- Expanded spec corpus is present under DocFX, including:
  - RFC 2911, RFC 8010, RFC 8011
  - PWG 5100.x families including 5100.5, 5100.7, 5100.18, and 5100.22
- IPP System Service support from PWG 5100.22-2025 is broadly implemented in client models and mappings.

## PWG 5100.22-2025 implementation notes

Implemented and wired in the client contract include System Service operations such as:

- `AllocatePrinterResourcesAsync`, `DeallocatePrinterResourcesAsync`
- `CreatePrinterAsync`, `DeletePrinterAsync`, `GetPrintersAsync`, `GetPrinterResourcesAsync`
- `GetSystemAttributesAsync`, `GetSystemSupportedValuesAsync`, `SetSystemAttributesAsync`
- `DisableAllPrintersAsync`, `EnableAllPrintersAsync`, `PauseAllPrintersAsync`, `ResumeAllPrintersAsync`
- `ShutdownOnePrinterAsync`, `StartupOnePrinterAsync`, `ShutdownAllPrintersAsync`, `StartupAllPrintersAsync`
- `RestartSystemAsync`, `RestartOnePrinterAsync`
- `RegisterOutputDeviceAsync`
- System/resource subscription operations (`CreateSystemSubscriptionsAsync`, `CreateResourceSubscriptionsAsync`, etc.)

Additional 5100.22 model/mapping support verified:

- `ResourceStatusAttributes.ResourceUuid`
- strong typed collections for system/resource and power-policy/state objects
- collection mapping in `CollectionProfiles` and related response profiles

## Known consistency gaps to watch

- `SharpIpp/SharpIppServer.cs` request dispatch currently does not include a branch for
  `IppOperation.RestartOnePrinter` in `ReceiveRequestAsync`.
- `docs/Content/Client/Operations.md` currently under-reports implemented coverage
  (lists RFC 2911 + PWG 5100.5 + CUPS, but not the full 5100.22 surface).
- Some XML summary references still contain placeholder section text (for example `6.3.??`),
  and should be normalized during doc maintenance.

## Agent workflow for spec-parity changes

When adding or correcting a spec operation/attribute, treat parity as end-to-end:

1. Add/confirm operation id in `IppOperation`.
2. Add request/response model types.
3. Add client contract method in `ISharpIppClient` and implementation in `SharpIppClient.*.cs`.
4. Add request/response profile wiring.
5. Add server dispatch mapping in `SharpIppServer.ReceiveRequestAsync`.
6. Add or update protocol attribute/profile mappings for any new fields.
7. Add tests:
   - unit mapping tests
   - unit client request-operation mapping tests
   - integration request/response roundtrip tests
8. Update docs:
  - `docs/Content/Client/Operations.md`
   - any relevant specification or API pages

## Practical guidance for agents

- Prefer small, focused patches and keep naming aligned with existing IPP operation style.
- Preserve bidirectional mapping symmetry for new attributes.
- For enum arrays in IPP attributes, ensure both read and write mapping paths are covered.
- Do not assume DocFX pages are fully current; verify behavior from code + tests first.
