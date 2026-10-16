# What's new in .NET 8

.NET 8 is the LTS platform release that shipped November 14, 2023. This page
separates language, runtime, SDK, web, and data features because they have
different owners and compatibility considerations. It is a curated overview,
not a complete API inventory.

## C# 12

The .NET 8 SDK includes the C# 12 compiler, and `net8.0` projects default to
C# 12. The compiler default is tied to the target framework and SDK; it is
not a runtime setting. The main language features include:

- **Primary constructors for classes and structs.** Constructor parameters
  become available throughout the type body. Unlike record primary
  constructors, ordinary class/struct parameters do not automatically become
  public properties; declare storage/properties where needed.
- **Collection expressions.** Bracket syntax such as `[1, 2, 3]` initializes
  supported collection targets, with `..` spread elements. The target type
  provides important context, so do not assume the syntax can infer every
  arbitrary collection type.
- **Optional parameters in lambda expressions.**
- **Alias any type**, not just named types.
- **Inline arrays** for fixed-size storage scenarios.
- **`ref readonly` parameters** for parameter passing where readonly-reference
  semantics are useful.
- **Experimental feature marker.** The `Experimental` attribute supports
  diagnostics for experimental APIs.
- **Interceptors:** an experimental compiler feature, not a general-purpose
  stable language feature to adopt casually.

Prefer the default language version for the TFM. Microsoft's guidance warns
that selecting a newer C# version than the one associated with the target
framework is unsupported and can expose missing runtime or library support.

## Runtime and base class libraries

### Performance and code generation

- **Dynamic PGO is enabled by default.** It can use runtime observations to
  optimize hot paths alongside tiered compilation. Gains depend on the
  workload, profile quality, and deployment conditions; benchmark before
  making capacity or latency assumptions.
- **JIT and Native AOT code generation improvements** include Arm64 and SIMD
  work, AVX-512 support, loop/vectorization improvements, and better handling
  of structs. CPU instruction-set availability remains hardware-dependent.
- .NET 8 continues performance work across the runtime and libraries. Do not
  attribute every improvement to a single API or assume identical gains across
  operating systems and architectures.

### Garbage collection and resource limits

The GC can be notified that a process memory limit changed at runtime through
`GC.RefreshMemoryLimit()`. This is useful in elastic/container scenarios,
but it can fail if a new limit leaves insufficient room for the GC. Account
for platform limitations, validate status/errors, and test limit changes
under realistic memory pressure before using it as autoscaling control.

### Libraries and interop

The release includes new APIs and improvements across the base class
libraries, including UTF-8-oriented parsing/formatting interfaces,
`SearchValues<T>` for optimized search, time-provider abstractions, and
collection and globalization improvements. Source generators were added or
improved for scenarios such as COM interop and configuration binding. These
APIs can reduce allocations or boilerplate, but the appropriate choice
depends on consumers, target frameworks, and deployment constraints.

The runtime also added a globalization mode for mobile applications. It is a
deployment-size and locale-capability tradeoff, not a universal replacement
for full globalization data.

## SDK and developer tooling

- **Terminal Logger:** `dotnet build --tl` provides a more structured,
  real-time build view; it is a presentation option, not a build-semantics
  change.
- **CLI MSBuild evaluation:** commands can query properties, items, and
  target results (for example, `--getProperty`, `--getItem`, and
  `--getTargetResult`) for automation and CI.
- **Simplified artifact output:** a configurable common output layout makes
  build products easier for tooling to locate. Changing output conventions
  can affect scripts and CI caches.
- **`dotnet workload clean`:** removes orphaned workload packs or, with
  `--all`, more broadly cleans the current SDK's workload installation. Use
  the broader option deliberately, especially on machines also managed by
  Visual Studio.
- **Restore security auditing:** NuGet restore can surface known
  vulnerabilities in dependencies. Treat these as input to a review and
  remediation process, not an automatic guarantee that a package is safe.
- The SDK adds other template, publish/pack, and source-build improvements.
  Pin and test the SDK feature band in reproducible build systems.

## ASP.NET Core 8

ASP.NET Core 8's most visible UI shift is **Blazor Web App**, a unified
starting point for component-based web applications. Rendering and
interactivity can be selected per component:

- static server-side rendering (static SSR);
- interactive server rendering;
- interactive WebAssembly rendering; and
- interactive auto rendering, which can start on the server and subsequently
  use WebAssembly in the browser.

The template consolidates earlier Blazor Server and hosted WebAssembly
scenarios. Existing projects remain supported; adopting the new model is an
optional migration and may involve changes to routing, render modes,
authentication state, forms, and caching.

Other ASP.NET Core 8 work includes minimal API form binding and validation,
Native AOT support improvements, SignalR, Kestrel and HTTP.sys, authentication
and authorization, and middleware enhancements. Review the framework's
release and migration notes rather than expecting a .NET runtime upgrade to
make application behavior identical.

## EF Core 8 (EF8)

EF Core 8 is its own LTS release. The significant changes include:

- **Complex types** for structured values without entity identity, usable as
  part of an entity model (not as independent `DbSet` entities).
- **Primitive collections** and expanded JSON column capabilities, subject
  to provider support and translation differences.
- **`HierarchyId`** support for SQL Server hierarchy data.
- More SQL translation, raw SQL querying, lazy-loading, model-building, and
  tracked-entity improvements.
- SQL Server scaffolding now maps database `date` and `time` to `DateOnly`
  and `TimeOnly`.

EF8 requires the .NET 8 SDK to build and .NET 8 runtime to run; it does not
run on earlier .NET versions or .NET Framework. A feature in the EF Core
product does not guarantee every database provider implements it in the same
way. Confirm provider version, SQL generation, database compatibility, and
migration behavior against the production database.

## Choosing what to adopt

| Capability | Useful when | Validate before adoption |
| --- | --- | --- |
| C# 12 syntax | It improves readability and supported targets all use compatible compilers. | Shared libraries, analyzers, generated code, and downstream compiler versions. |
| Dynamic PGO / SIMD | Hot paths have a measurable opportunity for runtime optimization. | Representative performance tests and target CPU/runtime configuration. |
| Blazor Web App render modes | A component/page-level mix of SSR and interactivity fits the UX. | Existing hosting, authentication, caching, state and deployment model. |
| EF8 complex types / JSON / primitive collections | The model maps naturally to provider-supported database shapes. | Provider/version-specific SQL, schema migrations, query plans, and null/enum behavior. |
| SDK artifact output or CLI queries | Build infrastructure can consume a more predictable layout or metadata. | CI scripts, caching, packaging, IDE workflows, and pinned SDK versions. |

## Sources

- [.NET 8 overview](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview)
- [.NET 8 runtime changes](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/runtime)
- [.NET 8 SDK and tooling changes](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/sdk)
- [C# 12](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12) and [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [ASP.NET Core 8 announcement](https://devblogs.microsoft.com/dotnet/announcing-asp-net-core-in-dotnet-8/) and [release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0)
- [EF Core 8 announcement](https://devblogs.microsoft.com/dotnet/announcing-ef8/) and [what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew)
