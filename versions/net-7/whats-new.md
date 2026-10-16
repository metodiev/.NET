# What's new in .NET 7

.NET 7 was Microsoft's November 2022 STS release and the successor to .NET 6. This curated overview separates the .NET platform, C# language, ASP.NET Core, and EF Core histories. It is not a complete feature catalog; use the linked product documentation for details and limitations.

## C# 11

The .NET 7 SDK includes the C# 11 compiler and selects C# 11 by default for `net7.0`. The language version is a compiler/target-framework default, not a runtime feature. Principal C# 11 features include:

- **Raw string literals** for easier multi-line text, JSON, and regular expressions without escape-heavy source.
- **UTF-8 string literals** using the `u8` suffix, producing UTF-8 data for APIs that accept it.
- **List patterns** to match sequences and their elements using pattern syntax.
- **Required members** to require initialization of selected fields or properties.
- **Generic attributes**, **static abstract interface members**, and **file-local types**.
- **Newlines in interpolated strings** and improved method-group conversion.

Some language constructs require supporting runtime or library types. Keep the default language version for the target framework unless there is a deliberate compatibility reason to override it; compiling new syntax does not automatically make an older target runtime support the necessary APIs.

## .NET runtime and libraries

### Performance and deployment

- **On-stack replacement (OSR)** lets the runtime replace code in a long-running method while it is executing. .NET 7 also extended profile-guided optimization (PGO), including its interaction with OSR and delegates.
- **Arm64 code generation** and Mono performance improved. Performance depends on workload and hardware; benchmark the application rather than assuming a universal gain.
- **Native AOT was introduced in .NET 7** for a constrained set of scenarios, focused on trimmed console applications. It publishes native code with fast startup and no JIT, but trimming can remove code discovered dynamically and the feature has compatibility/platform constraints. Review warnings and dependencies before adoption.

### Libraries and application capabilities

- **Generic math** adds static abstract members to interfaces such as numeric interfaces, enabling algorithms to operate over types that implement the required operations.
- **`System.Text.Json`** adds contract customization, polymorphic serialization, and support for required members. Validate wire formats and deserialization assumptions.
- **Regular expressions** gain a non-backtracking engine for supported patterns, a source generator for compile-time-specialized patterns, span-oriented APIs, and performance work. The non-backtracking engine has feature restrictions; verify pattern semantics before switching.
- **Time/date APIs** add microsecond and nanosecond members to relevant types; use exact precision appropriate to the stored data and note that underlying date-time tick resolution still governs representable values.
- **Streams and archives** gain `ReadExactly`/`ReadAtLeast` APIs and `System.Formats.Tar`.
- **Rate limiting** primitives provide policy building blocks for controlling resource traffic. The ASP.NET Core integration is covered below.
- **Unix file permissions**, memory-cache metrics, `StringSyntaxAttribute`, JavaScript/WebAssembly interop APIs, and generated P/Invoke marshalling broaden cross-platform and observability options.

## .NET SDK and tooling

- **Container publishing:** the SDK can publish container images directly with `dotnet publish`; a Dockerfile is not required for every application workflow. Image choice, registry authentication, ports, non-root behavior, and production hardening still require explicit review.
- **NuGet Central Package Management:** `Directory.Packages.props` can centralize package versions across projects, easing consistency but changing where package versions are declared and reviewed.
- **`dotnet new` improvements:** command completion, revised subcommands, and template constraints improve template discovery and authoring.
- **P/Invoke source generation:** `[LibraryImport]` generates marshalling code at compile time for supported methods, avoiding runtime-generated stubs in applicable cases.

These SDK features and any CI assumptions are separate from runtime features. Pin the intended SDK in `global.json` when reproducibility requires it, and check scripts or tooling for assumptions about SDK arguments, output, and generated files.

## ASP.NET Core 7 — separate web-framework history

ASP.NET Core 7 shipped with .NET 7 on November 8, 2022. Its release notes describe framework-specific features beyond the base runtime:

- **Minimal API filters** provide pre/post endpoint execution hooks for cross-cutting validation or behavior.
- **Output caching** adds server-controlled response caching, with endpoint policies and invalidation behavior to review.
- **Rate-limiting middleware** integrates the .NET rate-limiting primitives with HTTP endpoints.
- **Blazor** added `@bind:get`/`@bind:set`, improved forms and rendering, and changed WebAssembly authentication redirect handling to use navigation history state rather than the earlier query-string mechanism.
- MVC/API improvements include nullable view models, `IParsable<T>.TryParse` binding, configurable cookie-consent values, JSON property names in validation errors, and implicit dependency-injection binding for API controller parameters.
- Kestrel and hosting behavior changed; notably, the old default HTTPS binding was removed, so deployments must configure endpoints deliberately.

The release's breaking-change catalog identifies cases where inferred controller or SignalR parameter sources, authentication defaults, environment-variable precedence, fallback files, output-caching API names, or the default HTTPS binding can affect existing apps. Treat ASP.NET Core 7 as its own migration, not merely a consequence of setting `TargetFramework`.

## EF Core 7 — separate ORM history

EF Core 7 (EF7) was released on November 8, 2022. Microsoft documents that EF7 targets .NET 6 and can be used with either .NET 6 or .NET 7; it does not run on .NET Framework. EF7 is a NuGet-based ORM release with provider-dependent support; it is not the same product or target as the .NET runtime.

Important EF7 capabilities include:

- **JSON columns:** provider-agnostic modeling support with an implementation for SQL Server, including querying into mapped aggregates. SQL syntax and capabilities depend on provider and database.
- **Bulk updates and deletes:** `ExecuteUpdate` and `ExecuteDelete` execute set-based operations without loading entities and saving each tracked change. They bypass normal change tracking and per-entity behavior, so check concurrency, events, and auditing expectations.
- **Table-per-concrete-type (TPC) inheritance** and expanded mapping flexibility.
- **Stored procedure mapping for updates**, temporal-table improvements, and query translation/performance changes.

Do not infer that a feature is supported by every EF provider. Read the EF7 provider's own documentation, run representative SQL against the production database engine, and review the EF-specific breaking changes before changing package or provider versions.

## The .NET 6 → 7 → 8 learning path

The most useful way to study this release is to compare it at both edges:

| Layer | .NET 6 baseline | .NET 7 step | .NET 8 next step |
| --- | --- | --- | --- |
| Lifecycle | LTS, now unsupported | STS, now unsupported | LTS, but near end of support on October 16, 2026 |
| Language | C# 10 | C# 11 | C# 12 |
| Platform/runtime | Minimal APIs and .NET 6 performance/library work | OSR/PGO, generic math, Native AOT preview, JSON/regex additions | Dynamic PGO enabled by default, further runtime and library changes |
| Web | ASP.NET Core 6 | ASP.NET Core 7 minimal API filters, output caching, rate limiting, Blazor changes | ASP.NET Core 8 Blazor Web App and additional web features |
| Data | EF Core 6 | EF Core 7 JSON columns, set-based DML, TPC | EF Core 8 complex types, primitive collections, JSON and query changes |

These are highlights, not a compatibility guarantee. Review each product's own 6→7 and 7→8 catalogs and migration guides for features actually used.

## Official references

- [.NET 7 what's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7)
- [C# 11 language history](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-11)
- [ASP.NET Core 7 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-7.0)
- [ASP.NET Core 7 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/7/overview)
- [EF Core 7 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew)
- [Announcing EF Core 7](https://devblogs.microsoft.com/dotnet/announcing-ef7/) — release announcement and supported target runtimes.
- [EF Core 7 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
