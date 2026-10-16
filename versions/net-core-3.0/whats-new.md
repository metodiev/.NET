# What's new in .NET Core 3.0

## A major platform step, not only a runtime update

.NET Core 3.0 shipped on September 23, 2019 with SDK 3.0.100 and runtime
3.0.0. It was the STS/current release following 2.2 and preceding the 3.1
LTS release. Its feature set spans the runtime, base libraries, SDK, C#
compiler defaults, Windows Desktop, and separately released ASP.NET Core
and EF Core products. Keep those boundaries clear when attributing changes.

## .NET Core platform and tooling

- **.NET Standard 2.1:** .NET Core 3.0 implemented .NET Standard 2.1. This is
  a library API contract, not the application's TFM: apps target
  `netcoreapp3.0`, while libraries may target `netstandard2.1`. The broader
  API surface can make a library incompatible with consumers limited to
  .NET Standard 2.0, including .NET Framework.
- **Runtime and performance:** tiered compilation was enabled by default,
  alongside JIT, garbage collection, and other runtime performance work.
  Consult versioned documentation before assuming a runtime option or
  performance result applies to another release.
- **Libraries:** the release added `System.Text.Json` and API/library
  improvements. The official what's-new guide catalogs changes by area;
  this summary is not an exhaustive API inventory.
- **SDK and publishing:** the SDK changed project and publishing behavior.
  Framework-dependent applications gained a platform-specific appHost by
  default, and 3.0 introduced single-file publishing and trimming-related
  options. These features have application-type, platform, and deployment
  constraints; do not extrapolate a publish command or deployment guarantee
  from a feature headline.
- **Application model:** the default target for applications was
  `netcoreapp3.0`. SDK 3.0.100 included the 3.0.0 runtime. Visual Studio
  2019 16.3 GA was required to take full advantage of the release.

## C# 8 and `netcoreapp3.0`

Microsoft's language-version table associates .NET Core 3.x targets with
C# 8.0. The compiler selects a default based on the target framework; an
explicit `LangVersion`, older compiler, or other build configuration can
change the effective language version. C# is not implemented as syntax in
the .NET Core runtime.

C# 8 features include nullable reference types, asynchronous streams
(`IAsyncEnumerable<T>` and `await foreach`), ranges and indices, switch
expressions, recursive patterns, using declarations, and default interface
methods. Nullable reference types are opt-in annotations and compiler
analysis, not a guarantee of runtime null checking. Some language features
also depend on framework/runtime APIs; using a newer language version with
an older target can compile incorrectly or fail at runtime.

## Windows Desktop: Windows only

.NET Core 3.0 added Windows Forms and WPF to the .NET Core platform. These
remain Windows-only frameworks. The Windows Desktop SDK, Windows-specific
targeting, and application APIs do not turn the base runtime into a
cross-platform desktop UI stack. Windows Forms and WPF migrations should be
evaluated against actual platform dependencies, designer use, APIs, and
native components.

## ASP.NET Core 3.0 is its own framework release

ASP.NET Core 3.0 accompanied the platform but had a substantial, separately
documented release history:

- Endpoint routing became a central routing model; middleware order and
  endpoint configuration must be reviewed.
- Web apps moved toward the Generic Host.
- The Web SDK's implicit `Microsoft.AspNetCore.App` framework reference
  replaced many old explicit metapackage/package-reference conventions.
- gRPC support arrived, and Blazor Server was released; Blazor WebAssembly
  was still preview at the time.
- EF Core was removed from the ASP.NET Core shared framework, so applications
  selected EF Core packages explicitly.

These are ASP.NET Core behaviors, not runtime features. See the 2.2-to-3.0
migration document for project, hosting, routing, and package changes.

## EF Core 3.0 is a separate ORM release

EF Core 3.0 was distributed through NuGet and was not included in the
ASP.NET Core shared framework. Its package/provider and `dotnet-ef` tooling
history is separate from the .NET SDK:

- A reworked LINQ query pipeline and stricter server translation changed
  previously accepted queries.
- EF Core restricted client evaluation to the top-level projection and
  generally throws for untranslatable expressions elsewhere.
- Each LINQ query was designed to produce a single SQL statement; query
  shapes that previously caused multiple statements can now fail.
- EF Core added a Cosmos DB provider and interception capabilities, among
  other changes.
- EF Core 3.0 targeted .NET Standard 2.1, constraining the compatible
  runtime/platform set compared with the later 3.1 package line.
- `dotnet-ef` was no longer included in the .NET Core SDK and needed explicit
  tool installation.

Treat these as EF Core changes. Provider authors and application developers
need to inspect the official 3.x breaking-change catalog, not infer query
semantics from the .NET Core runtime version.

## Chronological context: 2.1 → 2.2 → 3.0 → 3.1

| Milestone | Contribution to the transition |
| --- | --- |
| .NET Core 2.1 (LTS) | Previous supported baseline before the interim 2.2 and 3.0 lines. |
| .NET Core 2.2 (STS/current) | Diagnostics, runtime events, Windows ARM32, and startup hooks; ASP.NET Core and EF Core 2.2 had their own changes. |
| .NET Core 3.0 (STS/current) | The major platform and compatibility boundary summarized above. |
| .NET Core 3.1 (LTS) | Refined the 3.0 line and became the next LTS release; it did not extend 3.0 support. |

All of these .NET Core releases are unsupported now. The sequence explains
historical compatibility; it is not advice to deploy old interim releases.

## Official sources

- [.NET Core 3.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/3.0/3.0.0/3.0.0.md)
- [What's new in .NET Core 3.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [What's new in C# 8.0](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8)
- [What's new in ASP.NET Core 3.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-3.0)
- [ASP.NET Core 2.2-to-3.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)
- [New features in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/)
- [Breaking changes in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
