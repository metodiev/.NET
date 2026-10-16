# .NET 5 compatibility and breaking-change notes

> **Historical reference only:** .NET 5 support ended May 10, 2022. Do not
> use this unsupported release as a deployment target. The notes below
> identify where to investigate compatibility; Microsoft's versioned
> catalogs are authoritative and more complete.

## Separate the platform and product changes

A transition from .NET Core 3.1 to .NET 5 can cross several compatibility
boundaries at once:

1. **.NET runtime and base libraries:** API and behavior changes documented
   in the .NET 5 compatibility catalog.
2. **SDK and build:** the SDK version, `net5.0` target, analyzers, publish
   behavior, and SDK defaults.
3. **C# compiler:** the default changes from C# 8.0 for `netcoreapp3.x` to
   C# 9.0 for `net5.0`; explicit language-version settings can differ.
4. **ASP.NET Core:** a distinct 3.1-to-5.0 web-framework migration with
   framework-specific compatibility notes.
5. **EF Core and providers:** separate ORM and database-provider package
   versions and breaking changes.

Do not assign a change to the runtime merely because it appeared in the same
release generation. In particular, ASP.NET Core and EF Core changes are not
automatically .NET runtime changes.

## .NET runtime, libraries, and SDK

Before changing the target, check the [official .NET 5 breaking-change
catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/5.0).
It separates changes by area, such as core libraries, networking, runtime,
and SDK. Review the individual entries for the APIs, operating systems, and
deployment modes used by the application; a high-level summary cannot replace
that inventory.

Also verify:

- The project and any `global.json` select an SDK available in the intended
  historical environment. The application TFM is `net5.0`; it is not an SDK
  version.
- A C# 9 default may expose compiler or analyzer diagnostics that were not
  present under a C# 8 default. Inspect `LangVersion` rather than assuming
  the target change alone sets every compiler option.
- Single-file publishing, trimming, appHost behavior, and runtime identifiers
  are publish-mode-dependent. Check the .NET 5 release notes and deployment
  documentation before relying on old scripts or assumptions.
- Native dependencies, ICU/globalization behavior, OS and architecture
  support, and framework-dependent versus self-contained runtime selection
  are part of the compatibility review.

## ASP.NET Core 5

Use Microsoft's [ASP.NET Core 3.1-to-.NET 5 migration
guide](https://learn.microsoft.com/en-us/aspnet/core/migration/31-to-50)
and release notes for web-specific changes. Verify the target web framework
and package references together; do not assume that targeting `net5.0` alone
upgrades every ASP.NET Core dependency.

Review routing and middleware behavior, authentication and authorization,
hosting and deployment configuration, static assets, MVC/Razor/Blazor
behavior, SignalR clients, gRPC dependencies, and OpenAPI/template changes
where applicable. The official migration guide's versioned checklist is the
source for concrete changes; an application's use of a feature determines
whether a catalog entry affects it.

ASP.NET Core 5 has its own product history. It shares the generation number
with .NET 5 but is neither identical to the runtime nor a replacement for
reading the web migration guide.

## EF Core 5 and provider compatibility

EF Core 5 is an independently versioned ORM, distributed through NuGet. Its
[breaking-change catalog](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/breaking-changes)
and [what's-new documentation](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew)
must be reviewed separately from the .NET and ASP.NET Core catalogs.

For an application using EF Core, inventory and test:

- EF Core runtime and `dotnet-ef` tool versions, provider package versions,
  and provider support for the selected database and target framework.
- LINQ query translation, generated SQL, loading strategy, and result
  semantics. In particular, understand the performance and consistency
  trade-offs of split queries if adopting them.
- Model mapping, migrations, value generation, and database-specific behavior.
  New ORM features such as TPT or implicit many-to-many relationships can
  affect model and schema assumptions.
- The versioned provider release notes. An EF Core version does not guarantee
  that every third-party provider has identical features or servicing.

These are ORM and provider compatibility concerns, not changes to the .NET
runtime. EF Core is not implicitly supplied by the ASP.NET Core shared
framework.

## Safe investigation approach

1. Inventory target frameworks, SDK pins, compiler settings, packages,
   runtime identifiers, native dependencies, and deployment modes.
2. Identify the exact compatibility entry and owning product (runtime,
   SDK/compiler, ASP.NET Core, EF Core, or provider).
3. Use representative tests and data to check application behavior, generated
   SQL, authentication flows, and published artifacts where relevant.
4. Read the complete official migration and breaking-change guides; this
   summary is not exhaustive.
5. For historical reproduction only, isolate unsupported components in a
   disposable environment. Do not expose an unsupported application to
   untrusted network traffic or use it as a production service.

## Official references

- [.NET 5 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/5.0)
- [.NET 5 release notes](https://github.com/dotnet/core/blob/main/release-notes/5.0/5.0.0/5.0.0.md)
- [What's new in .NET 5](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [ASP.NET Core 3.1-to-.NET 5 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/31-to-50)
- [ASP.NET Core 5 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-5.0)
- [EF Core 5 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/breaking-changes)
- [EF Core 5 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew)
- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
