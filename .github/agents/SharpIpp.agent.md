# SharpIpp Copilot/Agent Metadata

## Purpose
Provide project-specific guidance for assistant/agent interactions in the `SharpIppNext` repository.

## Scope
- Target: PWG 5100.22-2025 IPP Protocol support addition (system service, restart printer, resource/system attributes, collections).
- Context: this is a C# .NET library for IPP client/server interactions.

## Key files
- `SharpIpp/Protocol/Models/IppOperation.cs`
- `SharpIpp/ISharpIppClient.cs`
- `SharpIpp/Models/Requests/SystemServiceOperations.cs`
- `SharpIpp/Models/Responses/SystemServiceResponses.cs`
- `SharpIpp/Protocol/Models/ResourceStatusAttributes.cs`
- `SharpIpp/Protocol/Models/SystemStatusAttributes.cs`
- `SharpIpp/Mapping/Profiles/Responses/GetSystemAttributesResponseProfile.cs`
- `SharpIpp/Mapping/Profiles/Responses/GetResourceAttributesResponseProfile.cs`
- `SharpIpp/Mapping/Profiles/CollectionProfiles.cs`
- Unit tests: `SharpIpp.Tests.Unit/Mapping/Profiles/CollectionProfilesTests.cs`, `SharpIpp.Tests.Unit/SharpIppClientTests.cs`

## Agent instructions

### Basic workflow
1. Read spec sections when asked (PWG 5100.22-2025, especially 7.5 and 7.9).  
2. Check enums in `IppOperation` for operation coverage.  
3. Add missing operation constants and map to client service methods.  
4. Add or adjust model property names for spec attributes and collections.  
5. Update mapping profiles (`GetSystemAttributes...`, etc.).  
6. Add/extend unit tests covering object mappers and client request building.

### Style constraints
- Use nullable reference types consistent across model classes.
- Keep existing naming patterns (`PascalCase` for C# properties, attribute constant names mapped to IPP names in comments).  
- Avoid breaking existing API surface if possible (prefer additive non-breaking changes).

### Test strategy
- Use MSTest + FluentAssertions in `SharpIpp.Tests.Unit`.  
- Add `DataRow` for future ops in `SharpIppClientTests`.  
- Add mapping tests in `CollectionProfilesTests`.

### Tool chain for CI
- `.NET 8` (as specified in `global.json`)  
- `dotnet test SharpIpp.Tests.Unit/SharpIpp.Tests.Unit.csproj`  
- `dotnet test SharpIpp.Tests.Integration/SharpIpp.Tests.Integration.csproj` (as needed)

## Known gaps implemented
- `IppOperation.RestartOnePrinter` support
- `ISharpIppClient.RestartOnePrinterAsync` + request/response types
- `ResourceStatusAttributes.ResourceUuid` + `SystemStatusAttributes` typed collections
- Deep collection models (`SystemConfiguredPrinter`, etc.) and mapping profiles

## Quick check commands
- `dotnet test ./SharpIpp.Tests.Unit/SharpIpp.Tests.Unit.csproj --filter "FullyQualifiedName~CollectionProfilesTests|FullyQualifiedName~SharpIppClientTests"`
- `dotnet test ./SharpIpp.Tests.Integration/SharpIpp.Tests.Integration.csproj --filter "FullyQualifiedName~GetSystemAttributesTests"`
