# What's new in .NET 10

This milestone summarizes released .NET 10 and related product features from
Microsoft's documentation. Examples below are **descriptions of real
scenarios**, not executable code samples. No feature example has been built
against stable .NET 10 in this workspace; its installed .NET 10 SDK is preview.

## Runtime and performance

The .NET 10 JIT continues work on inlining, devirtualization, stack allocation,
struct argument code generation, and loop optimization. For example, a
performance-sensitive method passing a small value-type record can benefit
when the JIT promotes its fields into registers instead of spilling values
through memory. A service with loops and virtual dispatch may likewise benefit
from improved optimization decisions. Those are opportunities, not guaranteed
speedups: measure the real workload on the actual runtime, hardware, and
deployment configuration before setting performance expectations.

Native AOT and runtime code generation also receive improvements. These matter
to applications whose deployment choices favor ahead-of-time compilation,
reduced startup time, or a smaller footprint, but require validating
trimming/AOT compatibility, startup, diagnostics, and the target platform.

## Base libraries

.NET 10 adds and improves APIs across cryptography, globalization, numerics,
serialization, collections, diagnostics, and ZIP handling. Some concrete
examples:

- JSON deserialization can be configured to reject duplicate properties,
  support stricter serialization settings, and consume `PipeReader` input.
  A service processing untrusted JSON can choose stricter duplicate-property
  handling to avoid ambiguous payload interpretation; it should still apply
  its own schema, authorization, and input-size validation.
- Certificate tooling can search by a selected thumbprint algorithm rather
  than being limited to SHA-1-oriented lookup patterns. The PEM APIs also
  support finding encoded data directly from UTF-8 bytes, avoiding an
  unnecessary text conversion in an appropriate file-processing pipeline.
- New networking and process APIs include `WebSocketStream`, TLS 1.3 support
  for macOS clients, and Windows process-group support. These can simplify a
  streaming WebSocket integration or improve isolation when supervising
  child processes, subject to platform and protocol requirements.
- Post-quantum cryptography APIs have expanded. Their presence does not by
  itself make an application quantum-safe: algorithm selection, protocol
  support, key management, interoperability, and migration strategy remain
  security design decisions.

## SDK and developer tooling

The .NET 10 SDK adds capabilities across CLI, testing, containers, tools, and
file-based apps:

- **Testing:** `dotnet test` can integrate with Microsoft.Testing.Platform.
  Teams may standardize a test platform and reporting pipeline, but should
  confirm runner and CI compatibility before changing existing test
  infrastructure.
- **Containers:** console applications can create container images natively,
  and projects can choose an image format. This lets a worker or command-line
  service publish as a container without adopting a web framework. Review
  base-image OS, image provenance, native dependencies, and scanning policy.
- **Tool execution:** `dotnet tool exec` and the `dnx` script support one-shot
  tool invocation, useful for ephemeral CI tasks without making a tool a
  permanent global installation. Pin package versions and sources in
  reproducible or security-sensitive pipelines.
- **File-based applications:** the SDK expands file-based app workflows,
  including publishing and Native AOT support. This can make a small utility
  easier to distribute, but a script-like source file still needs deployment
  and runtime testing for its intended platforms.
- **CLI workflow:** command ordering is standardized, shell completion
  generation is improved, and `--cli-schema` supports CLI introspection.

SDK behavior changes can affect automation even if application code is
unchanged; inspect the [SDK breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10)
when scripts parse command output or rely on defaults.

## C# 14

C# 14 is the default language version for projects targeting `net10.0`.
Notable features include:

- **Field-backed properties:** a property can gain custom validation or
  normalization without declaring a separate backing-field member. A
  user-name property, for example, could normalize whitespace or reject an
  invalid value in its setter while retaining a compiler-synthesized field.
- **Extension members:** new `extension` blocks can express extension
  properties and static extension members alongside instance extension
  methods. A library can expose a derived property such as an `IsEmpty`
  convenience on a collection abstraction, while keeping the collection
  implementation unchanged.
- **Null-conditional assignment:** `?.` can guard an assignment when a
  receiver may be absent, avoiding a separate null check for an optional
  object graph.
- **Span conversions:** more implicit conversions make `Span<T>` and
  `ReadOnlySpan<T>` easier to use in APIs that process slices of data without
  allocating a new array or string for each operation.
- **Other additions:** `nameof` supports unbound generic types; lambda
  parameters can use modifiers such as `ref`, `in`, or `out` without
  explicitly stating their types; partial constructors and events are
  supported; and user-defined compound assignment and increment/decrement
  operators are available.

Prefer the framework-selected C# version. For libraries targeting older TFMs,
forcing C# 14 can produce unsupported code or rely on framework/runtime types
that are unavailable to those targets.

## ASP.NET Core 10

ASP.NET Core has its own release notes and breaking-change catalog, even
though the major release is paired with .NET 10.

- **Blazor:** the release improves WebAssembly preloading and diagnostics,
  provides enhanced form-validation and navigation behavior, and updates
  static-asset handling. A Blazor application can show a tailored row style
  in QuickGrid or use improved reconnection UI when a server connection drops.
- **Identity:** passkey support gives an application a passwordless
  authentication option. Teams still need to design enrollment, recovery,
  authenticator lifecycle, and account-security operations.
- **OpenAPI and APIs:** the release includes OpenAPI enhancements and
  minimal-API updates. API teams can improve descriptions and generated
  documentation, but should account for the documented obsolescence of
  `WithOpenApi` and related analyzer changes during upgrades.
- **Static web assets:** Blazor scripts are served as static web assets with
  compression and fingerprinting. This changes assumptions about where the
  script is sourced and how caching works; confirm custom asset pipelines.

These descriptions are deliberately selective. Consult the complete
[ASP.NET Core 10 what's-new article](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0)
for implementation details and version-specific behavior.

## EF Core 10

EF Core 10 (EF10) is a separate LTS release train and requires .NET 10 to
build and run. Its features depend on the database and provider:

- **SQL Server/Azure SQL vectors:** with supported Azure SQL or SQL Server
  2025 capabilities, EF can map vector embeddings and translate vector
  distance ordering. A retrieval/search application can rank records by
  similarity; production results still depend on database version, indexing,
  embedding generation, and query cost.
- **SQL Server JSON type:** when configured for Azure SQL or compatibility
  level 170 or higher, EF uses SQL Server's native `json` type for eligible
  JSON mappings. EF can translate JSON property predicates into SQL. This is
  not a general claim that every SQL Server version supports the new type.
- **Cosmos DB search:** EF supports full-text search and hybrid search that
  combines lexical text relevance with vector similarity. This supports
  retrieval scenarios in which semantic and exact-term matching complement
  each other.
- **Named query filters:** multiple filters can be declared for an entity
  type and selectively disabled, useful for cross-cutting rules such as
  tenant isolation and soft deletion. Verify that bypass controls cannot
  accidentally disable security boundaries.
- **Queries and migrations:** EF10 changes parameterized collection SQL
  translation and adds custom naming for default constraints. Both can alter
  query plans or migration diffs, so compare generated SQL and database
  changes in representative environments.

Provider support, server capability, migrations, and production SQL behavior
must be verified independently. See [EF Core 10 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
and the [EF10 breaking-change catalog](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes).

## Official references

- [.NET 10 overview](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview)
- [.NET 10 runtime](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/runtime)
- [.NET 10 libraries](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/libraries)
- [.NET 10 SDK and tooling](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk)
- [What's new in C# 14](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)
- [C# language versioning and defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [What's new in ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0)
- [What's new in EF Core 10](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
