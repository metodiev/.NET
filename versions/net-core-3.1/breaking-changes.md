# .NET Core 3.1 compatibility and breaking-change notes

This is a historical migration reference, not a recommendation to deploy
.NET Core 3.1. It is out of support. The largest compatibility risks for a
3.1-targeted application usually come from the 2.1-to-3.0 transition;
3.1 itself was mainly a refinement release, but it did include notable
ASP.NET Core and Windows Forms changes.

## Platform and application changes

- **Target and SDK:** an application moving from the earlier line must use a
  compatible 3.1 SDK and target `netcoreapp3.1`. For a pinned SDK,
  `global.json` must name an installed SDK version. Visual Studio support
  requires Visual Studio 2019 16.4 or later.
- **Framework compatibility:** .NET Core 3.x is not a drop-in replacement for
  .NET Framework. API availability, deployment, native dependencies, and
  operating-system assumptions must be checked against the actual target.
- **Windows Desktop controls:** .NET Core 3.1 removed older Windows Forms
  controls and APIs that had long been unavailable in the Visual Studio
  Designer Toolbox. For example, replace `DataGrid` with `DataGridView`,
  `ToolBar` with `ToolStrip`, and `MenuItem` with `ToolStripMenuItem`. Check
  the full removal table in the 3.1 what's-new document before porting.
- **macOS appHost:** the notarized macOS 3.1 SDK changed the default to
  disable appHost. Framework-dependent apps without appHost are launched
  using `dotnet <app>.dll`; self-contained publishing creates an appHost.
  Review scripts and user-facing launch instructions that assume a native
  executable.
- **3.0 deployment/tooling changes:** the appHost executable behavior,
  single-file deployment, trimming options, and SDK project conventions
  changed during the 3.0 release. Their behavior varies by deployment mode
  and application type; use the versioned migration guidance rather than
  copying an old publish command.

## ASP.NET Core changes

- **2.2 to 3.0 was a major migration, not a patch upgrade.** ASP.NET Core
  3.0 changed project/package conventions and hosting. Web SDK projects
  implicitly reference the `Microsoft.AspNetCore.App` shared framework;
  the old explicit metapackage/package references often need to be removed
  or replaced with the correct framework/package reference. Follow the full
  ASP.NET Core migration guide.
- **ASP.NET Core 3.0 no longer includes EF Core in the shared framework.**
  Add the intended EF Core provider and packages explicitly. This decouples
  EF Core servicing from ASP.NET Core.
- **SameSite cookies changed in 3.0 and again in 3.1.** ASP.NET Core 3.1
  aligned with newer browser behavior: `SameSiteMode.None` emits
  `SameSite=None`, and `SameSiteMode.Unspecified` omits the attribute.
  Cross-site authentication flows such as OpenID Connect and WS-Federation
  can be affected. Older browser clients also require consideration.
- **Blazor WebAssembly status:** the WebAssembly templates and runtime
  experience accompanying 3.1 were still preview; do not treat those preview
  components as a supported 3.1 GA feature.

## EF Core 3.x changes

The following are EF Core changes, not .NET runtime changes:

- **Client evaluation is restricted.** Before EF Core 3.0, expressions EF
  could not translate were often evaluated on the client, with warnings in
  some cases. Starting in 3.0, client evaluation is limited to the top-level
  projection; untranslatable expressions elsewhere throw at runtime. Rewrite
  the query or explicitly cross into LINQ-to-Objects after narrowing the
  result set.
- **One SQL statement per LINQ query.** EF Core 3.x generates a single SQL
  statement for each LINQ query. Patterns that cannot be translated that
  way can now throw instead of producing several statements. Inspect SQL and
  test performance and result semantics.
- **Explicit packages and CLI tool.** Starting in 3.0, EF Core and providers
  are not part of the ASP.NET Core shared framework, and `dotnet ef` is not
  included in the .NET Core SDK. Reference provider/runtime packages
  explicitly and install a matching `dotnet-ef` tool.
- **Other model/provider changes:** EF Core's official 3.x breaking-change
  catalog lists numerous changes, including key generation, SQLite storage,
  backing-field conventions, and relational model behavior. Review the
  complete catalog for the exact providers and APIs in use.

EF Core 3.1 restored .NET Standard 2.0 targeting after 3.0's .NET Standard
2.1 target. That can widen the runtime compatibility of the library, but does
not remove the EF Core 3.x query and API compatibility changes.

## Changes in language defaults

The default C# version for a `netcoreapp3.x` target is C# 8.0. This is
compiler behavior derived from the target framework; it is not a runtime
change. Review nullable-reference-type annotations and warnings as well as
any project-specific `LangVersion` configuration. Do not assume that changing
only the runtime changes the compiler's accepted syntax.

## How to investigate a real historical app

1. Inventory all target frameworks, SDK pinning, packages, providers,
   publish settings, and platform-specific code.
2. Identify which behaviors changed in 3.0 versus 3.1; don't attribute 3.0
   compatibility issues to the 3.1 patch/minor release.
3. Test EF Core queries against representative data and inspect generated
   SQL; specifically check previously client-evaluated expressions and
   multi-query patterns.
4. Test authentication cookies across browsers, Blazor behaviors, Windows
   Forms designer/control use, appHost launch scripts, and deployment
   containers as applicable.
5. Consult the linked per-product migration and breaking-change catalogs.
   This summary is not exhaustive.

## Official references

- [.NET Core 3.1 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/3.1)
- [What's new in .NET Core 3.1 (including Windows Forms removals)](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1)
- [ASP.NET Core 2.2-to-3.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)
- [ASP.NET Core 3.0-to-3.1 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/30-to-31)
- [ASP.NET Core SameSite cookie guidance](https://learn.microsoft.com/en-us/aspnet/core/security/samesite)
- [EF Core 3.x breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
