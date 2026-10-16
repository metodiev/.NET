# What's new in .NET Core 3.1

## The short version: 3.1 refines 3.0

.NET Core 3.1 was released on December 3, 2019 as an LTS release. Microsoft
described it as a relatively small set of fixes and refinements over 3.0;
the major platform changes learners associate with this milestone mostly
arrived in .NET Core 3.0. Both versions are now out of support. This page
places 3.1 in that progression rather than assigning every 3.0 change to
3.1.

## Platform changes from the 2.1 LTS line

### .NET Core 2.1 → 2.2

2.2 was an interim release after the 2.1 LTS baseline, not another LTS
milestone. Notable platform additions included runtime diagnostics and
`EventListener` access to CoreCLR events, Windows ARM32 support, and startup
hooks that allow a host to run code before the application's `Main` method.
Tiered compilation remained opt-in in the final 2.2 release; it became enabled
by default in 3.0.

ASP.NET Core and EF Core also advanced to 2.2 on their own product schedules.
Among their respective additions were endpoint routing and related web
improvements in ASP.NET Core, and spatial data support, owned-entity
collections, and query tags in EF Core. These are product-level features, not
features of the .NET runtime.

### .NET Core 2.2 → 3.0

3.0 was the substantive platform change on the road to 3.1:

- **Windows Desktop on .NET Core:** Windows Forms and WPF became available on
  .NET Core for Windows. They are not cross-platform UI frameworks.
- **.NET Standard 2.1:** .NET Core 3.0 implemented .NET Standard 2.1. This is
  distinct from the application TFM `netcoreapp3.0` (later
  `netcoreapp3.1`); a class library targeting `netstandard2.1` has a different
  compatibility contract.
- **Runtime and performance:** 3.0 enabled tiered compilation by default and
  included broad JIT, GC, and runtime performance work. The 2.2 runtime
  diagnostics/event work also formed part of the platform's evolution.
- **Application deployment/tooling:** framework-dependent applications
  gained platform-specific appHost executables by default. .NET Core 3.0
  added support for single-file deployment (with platform-specific
  constraints) and changed SDK/project behaviors; consult the release notes
  for deployment details rather than assuming all app types publish alike.
- **Language/compiler:** .NET Core 3.x targets default to C# 8.0 when the
  project does not explicitly override the language version. C# 8 features
  include nullable reference types, async streams, ranges and indices, and
  switch expressions. These are language/compiler features; .NET Core 3.0
  provides APIs needed by some features, but the runtime did not define C#.
- **Libraries:** 3.0 added `System.Text.Json` and further API/library
  improvements. It also made the .NET Standard 2.1 API set available on
  .NET Core.

ASP.NET Core 3.0 introduced major web-hosting and framework changes. Among
them were endpoint routing, the Generic Host for web apps, a shared framework
model with an implicit framework reference for Web SDK projects, and a
significant change to how EF Core packages and tools are obtained. See
Microsoft's ASP.NET Core 2.2-to-3.0 migration guide before treating 3.0 as a
drop-in update.

EF Core 3.0 was a separate major version. In addition to the new Cosmos DB
provider, query pipeline and interception features, it changed LINQ query
translation behavior: unsupported expressions outside the top-level
projection now throw instead of being silently evaluated on the client.
EF Core 3.0 also moved to one SQL statement per LINQ query and made
`dotnet-ef` an explicit tool installation rather than an SDK-included tool.
These are EF Core changes, not runtime changes.

### What 3.1 itself added

- **LTS servicing line:** 3.1 received a three-year support window, ending
  December 13, 2022. This support designation belongs to the .NET Core
  release; related products document their own release/support details.
- **Small platform refinements:** Microsoft characterized the release as
  fixes and refinements to the 3.0 base, with much of the focus on Blazor and
  Windows Desktop.
- **Windows Forms:** legacy controls that had already been superseded in the
  Windows Forms designer were removed from the .NET Core Desktop SDK. For
  example, `DataGrid` is replaced by `DataGridView`, and `MenuItem` by
  `ToolStripMenuItem`.
- **C++/CLI:** Visual Studio 2019 16.4 added support for creating C++/CLI
  projects targeting .NET Core. This is a Windows/Visual Studio tooling
  capability, not a cross-platform runtime feature.
- **macOS appHost:** the notarized .NET Core 3.1 macOS SDK disables appHost
  by default. Set `UseAppHost` when the project needs that native executable;
  self-contained publishing still creates an appHost.

## ASP.NET Core and EF Core 3.1 (separate product histories)

### ASP.NET Core 3.1

ASP.NET Core 3.1 released with .NET Core 3.1. Its updates include partial
classes and parameters for Razor components, a Component Tag Helper, Blazor
event default-prevention and propagation controls, improved development-time
Blazor errors, and shared request-queue support in HTTP.sys. The SameSite
cookie behavior changed to reflect browser/specification changes and can
affect remote authentication.

Blazor WebAssembly was still in preview alongside this release and was not
part of the stable ASP.NET Core 3.1 release. Do not describe the preview
templates as the shipped stable platform.

### EF Core 3.1

EF Core 3.1 released separately as NuGet packages. Its focus was polishing
EF Core 3.0 and fixing issues, rather than introducing major new features.
Importantly, EF Core 3.1 targets .NET Standard 2.0 again; EF Core 3.0 had
targeted .NET Standard 2.1. EF Core 3.1 does not come implicitly from the
ASP.NET Core shared framework. Applications select EF Core and provider
packages explicitly, and install the `dotnet-ef` tool separately.

## C# is not the runtime

Microsoft's C# language-version table maps `.NET Core 3.x` to a default of
C# 8.0. The default is determined by the compiler and target framework. It
does not mean the .NET Core runtime itself adds language syntax, and it does
not guarantee the same default if a project overrides `LangVersion` or uses a
different compiler/toolset.

## Source references

- [.NET Core 3.1 announcement and 3.0 recap](https://devblogs.microsoft.com/dotnet/announcing-net-core-3-1/)
- [.NET Core 3.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/3.1/3.1.0/3.1.0.md)
- [What's new in .NET Core 3.1](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1)
- [What's new in .NET Core 3.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0)
- [.NET Core 2.2 announcement](https://devblogs.microsoft.com/dotnet/announcing-net-core-2-2/)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [ASP.NET Core 3.1 updates](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-core-3-1/)
- [ASP.NET Core 2.2-to-3.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)
- [EF Core 2.2 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-2.2)
- [New features in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/)
- [EF Core 3.1 announcement](https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-3-1-and-entity-framework-6-4/)
