---
name: ipp-spec-parity
description: "Use when: comparing an IPP/PWG/RFC specification to SharpIppNext and implementing missing operations/attributes end-to-end (models + mappings + client/server wiring + tests + DocFX)."
---

# IPP Spec Parity (SharpIppNext)

This skill is a repeatable workflow for bringing SharpIppNext to parity with an IPP-related specification (PWG, RFC, etc.). It is optimized for specs like the Markdown files under `docfx/Content/Specification`.

## Inputs to ask for

- Spec document(s): file path(s) in `Specification/` or pasted excerpt(s)
- Target scope: **operations**, **attributes**, or both
- Required deliverables: tests, DocFX pages, public API surface

## Repository conventions (SharpIppNext)

- **Protocol constants**: attribute-name strings live under `SharpIpp/Protocol/Models/*Attribute.cs`.
- **Operations enum**: operation ids live in `SharpIpp/Protocol/Models/IppOperation.cs`.
- **Requests/Responses**: strongly typed models live under:
  - Requests: `SharpIpp/Models/Requests/`
  - Responses: `SharpIpp/Models/Responses/`
- **Mapping**: uses `SimpleMapper` profiles discovered via `FillFromAssembly`.
  - Operation attribute mapping is centralized in `SharpIpp/Mapping/Profiles/Requests/OperationAttributesRequestProfile.cs`.
  - Collection mapping for `IIppCollection` models is in `SharpIpp/Mapping/Profiles/CollectionProfiles.cs`.
- **Collections**: represent IPP collections as `IIppCollection` models serialized with `BegCollection/EndCollection` and `MemberAttrName`.
- **Enums**:
  - Spec `keyword`/`enum`-like values typically use the existing “smart enum” pattern used throughout `SharpIpp/Protocol/Models/`.
  - Integer ranges that have a fixed meaning can be plain C# `enum` (e.g., 3..6).
- **Docs**: client operation examples live under `docfx/Content/Client/*.md`, registered in `docfx/Content/Client/toc.yml` and summarized in `docfx/Content/Client/Operations.md`.
- **Tests**:
  - Unit tests: `SharpIpp.Tests.Unit/`
  - Integration tests: `SharpIpp.Tests.Integration/`

## Workflow

### 1) Spec → gap list

1. Locate the spec sections that define:
   - new/updated **operation ids**
   - request/response **operation attributes**
   - new **job/printer/document** attributes
   - any new collections (`collection` / `1setOf collection`)
2. Produce a concrete checklist:
   - attribute name (exact wire name)
   - value syntax (`integer`, `keyword`, `uri`, `1setOf keyword`, `collection`, …)
   - applicability (which operation(s)/object(s))
   - “required vs optional” and any defaulting rules

### 2) Implement protocol surface (constants + enums)

- Add missing attribute-name constants to the appropriate `*Attribute.cs`.
- Add missing operation ids to `IppOperation`.
- If a new `keyword` type is introduced:
  - create a smart-enum type following existing patterns in `SharpIpp/Protocol/Models/`.

### 3) Implement typed models

- Add properties to existing request/response models when the spec extends existing operations.
- For new operations:
  - add `...Request` + `...Response` types
  - add an operation-attributes model type when needed
- For collections:
  - create `IIppCollection` model(s) under `SharpIpp/Protocol/Models/`

Every new property should include an XML doc comment that:
- includes the exact wire attribute name in a `<code>...</code>` block
- cites the spec and section when available

### 4) Implement mapping

- Update `OperationAttributesRequestProfile.cs`:
  - dictionary → typed mapping (read)
  - typed → `List<IppAttribute>` mapping (write)
- Update `CollectionProfiles.cs` for any new `IIppCollection`.

Guidance:
- `1setOf X`: map to arrays (e.g., `int[]`, `string[]`) and emit one `IppAttribute` per value.
- `collection`: use `BegCollection/MemberAttrName/EndCollection` patterns.
- `1setOf collection`: group `BegCollection..EndCollection` blocks and map each to a model instance.

### 5) Wire through server/client

- Server dispatch: add the new `IppOperation` cases in `SharpIpp/SharpIppServer.cs`.
- Client API:
  - add method(s) to `SharpIpp/ISharpIppClient.cs`
  - implement in `SharpIpp/SharpIppClient.V11.cs` using the existing `SendAsync<TReq,TRes>` pipeline

### 6) Tests (required)

Add tests that cover the new mapping and wiring:

- Unit tests:
  - mapping for new operation attributes (dictionary ↔ typed ↔ attributes)
  - collection mapping for new `IIppCollection` models
- Integration tests:
  - round-trip typed request → raw request → server mapped request, asserting equivalence

Run:

- `dotnet test .\\SharpIpp.sln -c Release`

### 7) DocFX pages for new operations

For each new operation:

- Add `docfx/Content/Client/<OperationName>.md` with a minimal example
- Register in `docfx/Content/Client/toc.yml`
- Add to `docfx/Content/Client/Operations.md`

## Done criteria

- All new operations compile, map correctly, and are dispatched by server/client.
- Unit + integration tests pass.
- DocFX pages exist for each new operation and are referenced in the TOC.
