# What's new in .NET 5

## The headline: unification after .NET Core 3.1

.NET 5 shipped on November 10, 2020 with SDK 5.0.100. It is the next step
after .NET Core 3.1 and the first release under the unified .NET name. The
version number skipped 4 to avoid confusion with .NET Framework 4.x. It
continued the cross-platform .NET Core foundation; it did not turn .NET
Framework into a cross-platform framework or promise that every Framework
application could be moved unchanged.

.NET 5 was an **STS** release, supported until May 10, 2022. It is now
unsupported. STS and LTS describe the support window, not relative product
quality. The current support policy is the source of truth for current
deployments; historical release pages explain what .NET 5 was.

## Platform, runtime, and base libraries

The .NET 5 release combines a new runtime, base libraries, SDK, and
application target. Important themes in Microsoft's release material include:

- **Runtime and performance:** ongoing CoreCLR, JIT, garbage collection, and
  throughput improvements. These are runtime changes; measure application
  behavior rather than assuming a universal performance gain.
- **Platform and architecture:** a unified cross-platform runtime and SDK
  across supported Windows, Linux, and macOS environments, with support
  varying by OS version and architecture. Windows Forms and WPF remain
  Windows desktop technologies.
- **Deployment and tooling:** continued work on single-file applications,
  app trimming, and crossgen2. The details and constraints depend on runtime
  identifier, app type, and publish mode; the release notes should be
  consulted before reusing a historical deployment command.
- **Base libraries:** additions and refinements across the BCL, including
  `System.Text.Json` capabilities for reference preservation, immutable and
  record types, fields, custom converters, and HTTP client extensions.
- **Compiler tooling:** source generators can inspect compilation inputs and
  contribute generated source. They are a compile-time capability, distinct
  from runtime reflection or a .NET runtime feature.

The product's feature set is larger than this short orientation. The official
.NET 5 what's-new article and versioned release notes enumerate APIs,
platform-specific details, and applicable limitations.

## C# 9 belongs to the language/compiler track

The language-version table maps .NET 5 targets to **C# 9.0 by default**. The
compiler selects that default from the target framework; an explicit
`LangVersion` can override it. The runtime does not itself provide C# syntax,
and compiling newer syntax against an older target can fail or rely on
unavailable runtime types and behavior.

C# 9 highlights include:

- **Records** for concise reference types with value-based equality and
  `with`-expression non-destructive mutation.
- **Top-level statements** to simplify an application's entry point.
- **Relational and logical patterns** (`<`, `>`, `and`, `or`, `not`) for
  pattern matching.
- **Init-only properties** for initialization-time assignment.
- **Target-typed `new`**, improved target-typed conditional expressions, and
  covariant return types.
- **Function pointers** and other advanced language/compiler additions.

These are language features whose use is mediated by the compiler and target
framework. They are not each a new runtime subsystem.

## ASP.NET Core 5 is a separate framework release

ASP.NET Core 5 is the web framework associated with .NET 5, with its own
release notes and 3.1-to-5.0 migration guide. Its highlights include
improvements to Blazor (including CSS isolation and component virtualization),
SignalR, MVC, gRPC, and web API/OpenAPI templates. For historical ASP.NET
Core 5 web API templates, OpenAPI support was enabled by default through
Swashbuckle integration.

These are web-framework or template features, not generic .NET runtime
features. When moving a web project, read the ASP.NET Core migration guide and
check the API/package details against the actual app.

## EF Core 5 is a separate ORM release

EF Core 5 shipped its own 5.0 NuGet packages and feature documentation. Major
features included:

- **Many-to-many relationships** without requiring an explicit join-entity
  type for common cases.
- **Table-per-type (TPT) inheritance mapping.**
- **Split queries** for selected collection-loading scenarios, with an
  explicit trade-off: additional database round trips may reduce some query
  performance issues, while separate statements can observe different data
  unless an appropriate transaction is used.
- **Filtered `Include`**, table-valued functions, and improved query
  translation.
- **Cosmos DB provider improvements**, many-to-many support, and better model
  and query diagnostics such as debug views.

EF Core is an ORM library, not the .NET runtime and not an implicit part of
the ASP.NET Core shared framework. Select the EF Core provider and tooling
packages appropriate to the database and target, and consult EF Core's own
breaking-change page; providers can have their own compatibility constraints.

## Version relationships at a glance

| Concern | .NET Core 3.1 | .NET 5 |
| --- | --- | --- |
| Application TFM | `netcoreapp3.1` | `net5.0` |
| SDK at initial release | 3.1.100 | 5.0.100 |
| Default C# version | C# 8.0 | C# 9.0 |
| ASP.NET Core generation | ASP.NET Core 3.1 | ASP.NET Core 5.0 |
| EF Core generation | EF Core 3.1 | EF Core 5.0 |
| Platform support | LTS release, now unsupported | STS release, now unsupported |

Each row describes related but distinct versioning systems. In particular,
the SDK version is not the TFM, the C# version is not the runtime, and the
ASP.NET Core / EF Core histories are maintained separately.

## Historical learning and current use

This is a source-based historical summary. No runnable example is included,
and no build or test against a .NET 5 SDK/runtime is claimed. For safe
historical study, prefer documentation, archived release notes, and source
inspection. If reproducing old behavior is necessary, isolate it in a
disposable environment without exposing an unsupported runtime or application
to untrusted traffic or data.

Do not select .NET 5 for a current production deployment. Check Microsoft's
live support policy, select a supported target, and use the target's current
migration documentation. See [migration guide](migration-guide.md) for the
historical transition and present-day caveats.

## Official sources

- [.NET 5.0.0 release notes and SDK 5.0.100](https://github.com/dotnet/core/blob/main/release-notes/5.0/5.0.0/5.0.0.md)
- [Announcing .NET 5.0](https://devblogs.microsoft.com/dotnet/announcing-net-5-0/)
- [What's new in .NET 5](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- [C# language versioning and target-framework defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [What's new in ASP.NET Core in .NET 5](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-5.0)
- [What's new in EF Core 5.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew)
- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
