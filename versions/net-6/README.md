# .NET 6 — the LTS release after .NET 5

- **Released:** November 8, 2021
- **Release type:** Long-term support (LTS)
- **End of support:** November 12, 2024
- **Status on October 16, 2026:** Unsupported. Microsoft no longer provides .NET 6 security or quality updates or technical support. Do not select it for a new or continuing production deployment.
- **Default C#:** C# 10 for projects targeting `net6.0`.
- **SDK and target:** The initial release shipped .NET SDK 6.0.100; applications target the `net6.0` TFM. SDK, runtime, and target framework versions are related but distinct.
- **Related products:** ASP.NET Core 6 and EF Core 6 are separate product releases with their own release notes and compatibility guidance.

## Why this milestone matters

.NET 6 completed the unification begun with .NET 5: a common SDK, runtime,
and base libraries for cloud, desktop, mobile, and IoT workloads. It was the
first long-term-support release after .NET Core 3.1 in the unified product
line. Between those LTS endpoints, **.NET 5 was an STS release**. The upgrade
story is therefore not simply “3.1 plus a new runtime”: .NET 5 renamed the
platform and introduced a broad set of changes, and .NET 6 added another
major release's compatibility surface.

The milestone is useful for learning the platform's modern shape—minimal
hosting and APIs, hot reload, C# 10, updated runtime and library APIs, SDK
workloads, and matching web/data-framework generations. It is not a current
production recommendation: support ended in 2024. As of this curriculum's
October 16, 2026 snapshot, select a currently supported target (the
repository's lifecycle snapshot identifies .NET 10 LTS) for real deployments
and review all intervening compatibility changes.

## At a glance

| Question | Answer |
| --- | --- |
| Release date? | November 8, 2021. |
| LTS or STS? | LTS, with a three-year support window. |
| End of support? | November 12, 2024. The actual end date aligns to Patch Tuesday; the release was originally announced November 8, 2021. |
| Supported today? | No. The runtime and SDK line are out of support. |
| Default language? | C# 10 for `net6.0` when using a compatible current compiler. It is a compiler/TFM default, not a runtime feature. |
| SDK and TFM? | Initial SDK 6.0.100; target `net6.0`. Use a .NET 6 SDK for historical builds. The SDK version is not the TFM or runtime version. |
| Web stack? | ASP.NET Core 6, including minimal APIs and the minimal hosting model. See its independent release and breaking-change notes. |
| Data stack? | EF Core 6 (EF6), a separate LTS ORM release requiring .NET 6. Its provider and breaking changes need their own review. |
| Production use? | Do not deploy .NET 6 or its associated unsupported product versions. Move to a currently supported .NET release and patch it. |

Microsoft's policy says LTS releases receive support for three years and
require the latest patch to remain supported. Since November 12, 2024, .NET
6 has received no servicing fixes. The current unsupported status is not
changed by retaining an old SDK/runtime installer or by building successfully
with a newer SDK.

The .NET support policy covers the runtime, SDK, ASP.NET Core, and Entity
Framework Core. ASP.NET Core 6 shipped as the web-framework generation for
.NET 6. EF Core 6's own release plan says it requires .NET 6 and aligns its
LTS release with .NET 6. Accordingly, these are related but distinct
products whose .NET 6 support window ended in November 2024; package,
provider, and breaking-change details remain product-specific.

## Keep the version numbers separate

- **.NET runtime and base libraries** provide execution and framework APIs.
- **.NET SDK** supplies build tools, MSBuild, CLI tooling, and compilers; its
  feature-band version can change independently of runtime servicing.
- **`net6.0`** is the target framework moniker (TFM) declared by a project.
- **C# 10** is the language version selected by the compiler for `net6.0` by
  default. The SDK/compiler determines accepted syntax; the runtime does not.
- **ASP.NET Core 6** is the web framework generation that shipped alongside
  .NET 6. It has specific changes in hosting, routing, MVC, Razor, Blazor,
  SignalR, and Kestrel.
- **EF Core 6** is the ORM generation. It is distributed as packages, has
  provider-specific behavior, and requires .NET 6 rather than .NET Standard
  or .NET Framework.

Matching “6” numbers identify a release generation, not interchangeable
components or identical lifecycles. In particular, EF Core is not implicitly
provided by ASP.NET Core's shared framework: select and service the relevant
EF Core provider packages and `dotnet-ef` tooling explicitly.

## From .NET Core 3.1 through .NET 5

| Release | Lifecycle | What changed in the sequence |
| --- | --- | --- |
| .NET Core 3.1 | LTS; support ended December 13, 2022 | Previous LTS baseline, with the `netcoreapp3.1` TFM and C# 8 default. |
| .NET 5 | STS; support ended May 10, 2022 | Dropped “Core” from the platform name, skipped version 4 to avoid confusion with .NET Framework 4.x, and continued platform unification. It brought C# 9, single-file deployment and app trimming, Windows Arm64 support, and runtime/library changes. ASP.NET Core 5 and EF Core 5 retained “Core” in their names. |
| .NET 6 | LTS; support ended November 12, 2024 | Continued unification with SDK 6.0, C# 10, runtime/library and tool improvements, ASP.NET Core 6, and EF Core 6. |

.NET 5's short support window means it is a compatibility waypoint, not a
safe place to pause a present-day production upgrade. The
[migration guide](migration-guide.md) separates a direct target-framework
move from the need to account for intermediate release changes.

## What's in this milestone

1. **C# 10 and templates:** global using directives, file-scoped namespaces,
   record structs, interpolated string handlers, and other language
   refinements. SDK templates adopt nullable reference types, implicit global
   usings, top-level statements, and other conveniences; existing source is
   not automatically rewritten by changing the TFM.
2. **Runtime and libraries:** performance and reliability work in areas such
   as `FileStream`, dynamic PGO (opt-in), Crossgen2, and Arm64; APIs such as
   `DateOnly`, `TimeOnly`, and `PriorityQueue<TElement,TPriority>`; and
   System.Text.Json source generation and a writable DOM.
3. **Developer experience:** Hot Reload in supported tooling and `dotnet
   watch`; optional SDK workloads managed through `dotnet workload` commands.
4. **ASP.NET Core 6:** minimal APIs, a simplified minimal hosting model,
   Razor compiler source generators, and web-stack performance improvements.
5. **EF Core 6:** SQL Server temporal-table mapping/querying, compiled models,
   query translation improvements (including `GroupBy` patterns), and
   enhancements across supported providers.

The curated feature list and compatibility risks are expanded in
[What's new](whats-new.md) and [Breaking changes](breaking-changes.md).

## Compatibility and production-safe use

A major version can change behavior even when source code compiles. Review
the full .NET, ASP.NET Core, and EF Core catalogs that apply to the
application. Inventory framework/package versions, target frameworks,
operating systems, native dependencies, provider/database versions, and
publish configuration. Exercise authentication, serialization, file and
stream I/O, query translation, migrations, and deployment behaviors that
matter to the service.

Do not read a successful build as proof that an unsupported runtime is safe
to run. A production transition should target a supported release, use its
latest servicing update, test on representative OS/container and database
configurations, and have a deployment and rollback plan. If the goal is a
modern supported target in the October 2026 snapshot, assess .NET 10 and
review every applicable intermediate breaking-change set rather than
deploying .NET 6 as a stepping stone.

This is a documentation-only milestone. It adds no runnable project, and no
build, test, executable sample, migration, or performance validation is
claimed as part of this work.

## Official references

### .NET and C#

- [.NET 6 release notes (6.0.0 and SDK 6.0.100, November 8, 2021)](https://github.com/dotnet/core/blob/main/release-notes/6.0/6.0.0/6.0.0.md)
- [What's new in .NET 6](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)
- [.NET 5 overview and changes from .NET Core 3.1](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [.NET 6 compatibility and breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/6.0)

### ASP.NET Core

- [What's new in ASP.NET Core in .NET 6](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- [ASP.NET Core 6 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/6/overview?view=aspnetcore-6.0)
- [Migrate ASP.NET Core 3.1 to .NET 6](https://learn.microsoft.com/en-us/aspnet/core/migration/31-to-60?view=aspnetcore-6.0)
- [Migrate ASP.NET Core in .NET 5 to .NET 6](https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60?view=aspnetcore-6.0)

### EF Core

- [What's new in EF Core 6](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew)
- [EF Core 6 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/breaking-changes)
- [EF Core 6 release plan and platform requirements](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/plan)
