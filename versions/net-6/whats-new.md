# What's new in .NET 6

.NET 6 completes the unification plan begun with .NET 5: a shared SDK,
runtime, and base class libraries for cloud, desktop, mobile, and IoT
development. This is a curated learning overview; each product's own release
notes remain authoritative for full details.

## C# 10 and project templates

The compiler defaults to C# 10 for projects targeting `net6.0`. C# 10
features include:

- **Global using directives**, including SDK-provided implicit global usings
  in templates, to reduce repetitive imports.
- **File-scoped namespaces**, a compact namespace declaration for a file
  containing one namespace.
- **Record structs**, extending record-style value-based data modeling to
  structs.
- **Constant interpolated strings** and **interpolated string handlers**,
  enabling compile-time constants where possible and optimized formatting
  scenarios.
- **Improvements to lambdas, attributes, and caller argument expressions.**

The .NET 6 SDK templates were modernized to use features such as top-level
statements, file-scoped namespaces, implicit global usings, and nullable
reference types. That is a template change, not a source rewrite imposed on
existing applications when their TFM changes. Review generated templates
and project properties rather than inferring code compatibility from a new
SDK. The target framework determines the recommended C# default; avoid
forcing a later language version than the target supports.

## Runtime and base libraries

### Performance, I/O, and architecture

- **`FileStream` was substantially rewritten**, with performance and
  reliability improvements, particularly for asynchronous I/O on Windows.
  Changes to file offsets, locking, and async completion behavior are also
  compatibility considerations; see the .NET 6 breaking-change catalog
  before relying on implementation details.
- **Dynamic profile-guided optimization (PGO)** was introduced. In .NET 6 it
  is opt-in, using tiered compilation and runtime observations to optimize
  frequently used code paths. Results are application- and environment-
  dependent; measure before changing capacity assumptions.
- **Crossgen2** replaced Crossgen as the ahead-of-time compilation tool for
  ReadyToRun scenarios.
- The release added native support for **Windows Arm64 and macOS Arm64
  (Apple Silicon)** alongside continued x64 support. Validate the runtime
  identifier, native dependencies, and deployment architecture actually used.

### APIs and libraries

- **`DateOnly` and `TimeOnly`** model calendar dates and times of day without
  implying a date-time or time-zone.
- **`PriorityQueue<TElement,TPriority>`** provides a general-purpose
  priority queue in the base libraries.
- **System.Text.Json source generation** can improve throughput, reduce
  memory use, and help trimming scenarios; .NET 6 also introduced a writable
  JSON DOM (`JsonNode`, `JsonObject`, `JsonArray`, and `JsonValue`) and
  `IAsyncEnumerable<T>` serialization support.
- Additional runtime and base-library work improved areas such as regular
  expressions, networking, cryptography, globalization, and diagnostics.
  Consult the release notes for exact APIs and OS-specific details.

These additions are capabilities rather than automatic application
improvements. Benchmark workloads and check serialization compatibility,
target frameworks, and platform-specific dependencies.

## SDK and developer tooling

- **Hot Reload** lets supported .NET apps apply some source edits while
  running, through Visual Studio 2022 and `dotnet watch`. It does not apply
  every change, replace a clean build, or establish that a production deploy
  is correct.
- **Optional SDK workloads** move some application stacks out of the base
  SDK installation. `dotnet workload` commands search, install, list,
  update, uninstall, and repair workloads. Workloads are tied to SDK/tooling
  management; CI should provision and pin the needed SDK and workloads
  deliberately.
- SDK templates use modern C# features. SDK changes also include tooling and
  build improvements; use an installed, supported SDK in reproducible build
  systems rather than treating the runtime version as a compiler selector.

## ASP.NET Core 6

ASP.NET Core 6 is its own web-framework release, not just a synonym for the
.NET 6 runtime.

- **Minimal APIs** provide an endpoint-oriented way to build HTTP APIs with
  fewer files and dependencies. They suit small services and microservices;
  controller-based APIs remain available. Consider binding, validation,
  filters, endpoint metadata, and testability when choosing a model.
- **Minimal hosting** simplifies the default startup shape and project
  templates. It changes how configuration and middleware are commonly
  composed, but adopting the simplified template style is not required for
  every application.
- **Razor compilation uses source generators** and generated view/page types
  are compiled into the application assembly rather than a separate Views
  assembly. This can affect reflection and tools that depended on generated
  implementation details.
- **Blazor, SignalR, Kestrel, and the broader HTTP stack** received feature
  and performance updates. Validate each application's hosting and
  protocol-specific behavior against the framework release notes.

ASP.NET Core's own migration article covers framework package updates and
project steps. Its breaking-change catalog is separate from the .NET runtime
catalog.

## EF Core 6

EF Core 6 (EF6) is a separate ORM release, shipped as NuGet packages and
requiring .NET 6 to build and run. It is not EF6 (the older Entity Framework
product); “EF Core 6” is the shorthand for Entity Framework Core 6.0.

- **SQL Server temporal tables:** configure temporal tables through the model
  and migrations, query historical rows, and restore earlier data. SQL
  Server maintains UTC period values and history records; deployment and
  schema behavior depend on SQL Server support.
- **Compiled models** can reduce model initialization costs for large
  models, in exchange for generated artifacts and a build-time workflow.
  Benchmark and maintain the generated model as part of the application's
  source/build process.
- **LINQ query translation** improved, including `GroupBy` scenarios.
  Translateability and SQL still depend on the provider; inspect generated
  queries and test with the actual database.
- Other improvements span migrations, SQLite, SQL Server, Cosmos DB,
  change tracking, and model building.

EF Core providers and database engines have their own compatibility limits.
Do not infer that a feature works identically across providers or that an
EF Core package is automatically upgraded by changing `TargetFramework`.

## Use the feature history responsibly

This milestone has no runnable sample project, and no build, test,
benchmark, or database migration was run as part of its documentation work.
Feature descriptions summarize the Microsoft release documentation; they
are not validation results for a particular application.

## Official references

- [.NET 6 what's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)
- [C# 10 feature history](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10)
- [C# language version defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [ASP.NET Core 6 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- [ASP.NET Core minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
- [EF Core 6 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew)
- [EF Core 6 release plan and platform requirements](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/plan)
