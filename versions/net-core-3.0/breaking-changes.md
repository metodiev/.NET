# .NET Core 3.0 compatibility and breaking-change guide

> **Historical warning:** .NET Core 3.0 support ended March 3, 2020. It is
> unsupported and is not an appropriate production target. This page
> identifies compatibility risks for historical study; it is not exhaustive
> and does not substitute for the official per-product catalogs.

The largest compatibility boundary between .NET Core 2.2 and 3.0 is broader
than runtime APIs. The SDK/project model, ASP.NET Core, EF Core, language
defaults, and Windows Desktop porting concerns all changed. Treat the
products independently and verify the complete dependency graph.

## .NET platform and SDK

- **Target framework and toolchain:** 3.0 applications target
  `netcoreapp3.0`; SDK 3.0.100 shipped with runtime 3.0.0. Historical builds
  also required compatible tooling (Visual Studio 2019 16.3 GA for full
  feature support). A TFM does not pin every SDK, compiler, package, or
  runtime patch.
- **.NET Standard 2.1 compatibility:** libraries targeting `netstandard2.1`
  use APIs not available in .NET Standard 2.0 and .NET Framework. Confirm
  every intended consumer supports the library's chosen TFM before changing
  it.
- **AppHost and publish behavior:** 3.0 changed appHost and publish defaults
  and introduced single-file and trimming-related options. Launch scripts,
  output-file assumptions, native dependencies, and deployment processes
  need version- and application-specific review. The features had
  constraints; no general single-file or trimming deployment promise follows
  from this summary.
- **C# defaults:** `netcoreapp3.0` normally maps to C# 8.0, but project
  `LangVersion`, compiler version, and build configuration may override that
  default. Nullable reference analysis is opt-in and is not runtime
  enforcement.
- **Windows Desktop:** Windows Forms and WPF are available on .NET Core 3.0
  only for Windows. Porting a .NET Framework desktop application can surface
  API gaps, native dependencies, designer limitations, and Windows-specific
  assumptions; this is not a cross-platform UI migration.

## ASP.NET Core 2.2 to 3.0

ASP.NET Core's 3.0 transition should be treated as a major framework
migration, not as a runtime-only update:

- **Project/package model:** Web SDK projects implicitly reference the
  `Microsoft.AspNetCore.App` shared framework. The former explicit
  metapackage and many individual ASP.NET Core package references should be
  reviewed and updated according to the migration guide.
- **Hosting:** the Generic Host became the hosting model for web apps. Review
  host setup, configuration, dependency injection, logging, and hosted
  services.
- **Routing/middleware:** endpoint routing changed routing setup and
  middleware interactions. Recheck middleware ordering, route registration,
  authorization, and endpoint execution.
- **JSON and other framework packages:** features no longer included in the
  shared framework may require explicit package references. In particular,
  EF Core is no longer included there.
- **WebAssembly status:** Blazor WebAssembly was preview at the time of the
  3.0 release; do not treat the preview as a GA feature of ASP.NET Core 3.0.

Follow Microsoft's complete ASP.NET Core 2.2-to-3.0 migration checklist for
the exact project type and APIs in use.

## EF Core 3.0 query and package changes

The following are EF Core compatibility changes, not .NET runtime changes:

- **Client evaluation restricted:** EF Core 3.0 allows client evaluation in
  the top-level projection only. If an expression elsewhere in a query
  cannot be translated by the provider, execution throws rather than
  silently fetching rows for client-side evaluation. Rewrite the query or
  deliberately perform client-side work only after narrowing the server-side
  result.
- **Single SQL statement per LINQ query:** EF Core 3.0 changed behavior to
  generate a single SQL statement for a LINQ query. Query shapes which
  previously generated multiple statements can now throw. Review generated
  SQL, semantics, and performance with the real provider and data.
- **Reworked query pipeline/provider compatibility:** provider authors and
  applications may require compatible 3.0 provider versions; verify the
  provider's supported EF Core version instead of assuming package upgrades
  are interchangeable.
- **Package boundary:** EF Core and providers are separate NuGet packages,
  not members of ASP.NET Core's shared framework. Add only the packages and
  provider required by the application.
- **CLI tooling:** `dotnet-ef` is not included with the .NET Core SDK from
  EF Core 3.0 onward. Install a compatible tool explicitly and keep its
  version aligned with the EF Core packages.
- **Target framework:** EF Core 3.0 targeted .NET Standard 2.1, limiting
  supported consumers. EF Core 3.1 later returned to .NET Standard 2.0; this
  does not undo the EF Core 3.x query behavior changes.

The official EF Core 3.x catalog includes additional API, model, and
provider-specific breaking changes. This page is a shortlist, not a
substitute for that catalog.

## C# 8 and application behavior

C# 8 changed compiler capabilities rather than runtime syntax. Nullable
reference types can expose warnings and source-annotation work when enabled;
they do not insert runtime checks. Async streams, ranges/indices, and default
interface methods have API/runtime or target-framework considerations. Check
the official C# 8 docs and the actual compiler/TFM pairing rather than
raising `LangVersion` alone.

## Historical investigation checklist

1. Inventory project TFMs, SDK pinning, compiler and language-version
   settings, package/provider versions, native dependencies, and publish
   assumptions.
2. Separate .NET Core, ASP.NET Core, and EF Core behavior. Use each product's
   own migration and breaking-change documentation.
3. For EF Core, execute representative queries against the intended provider
   and data; check translation failures and inspect generated SQL.
4. For web applications, review Generic Host setup, routing, middleware,
   package references, authentication, and any preview-only functionality.
5. For desktop apps, check Windows-only assumptions, APIs, controls,
   designers, and native components on the actual supported Windows
   environment.
6. Record actual SDK/runtime/tool versions and test results for any
   historical reproduction. This curriculum does not claim to have built or
   tested an application against .NET Core 3.0.

## Official references

- [.NET Core 3.0 what's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0)
- [.NET Core compatibility and breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/3.0)
- [ASP.NET Core 2.2-to-3.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)
- [What's new in ASP.NET Core 3.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-3.0)
- [EF Core 3.x breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes)
- [New features in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
