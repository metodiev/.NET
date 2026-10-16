# Historical migration guide: .NET Core 2.2 to 3.0

> **Status warning:** .NET Core 3.0 is out of support; its lifecycle ended
> March 3, 2020. This guide is historical learning material, not advice to
> deploy 3.0 or an endorsement of using unsupported software. For a real
> migration, select a currently supported target and use Microsoft's current
> porting and migration guidance.

## Understand the release sequence

.NET Core 2.1 was LTS, 2.2 and 3.0 were STS/current releases, and 3.1 became
the following LTS release. A historical application could move across the
major 3.0 compatibility boundary without deploying every intermediate
version. The chronology matters because it identifies where behavior
changed:

| Milestone | Lifecycle | Migration context |
| --- | --- | --- |
| .NET Core 2.1 | LTS; support ended August 21, 2021 | Previous LTS baseline. |
| .NET Core 2.2 | STS/current; support ended December 23, 2019 | Interim release; review its runtime and companion product changes if the application depended on them. |
| .NET Core 3.0 | STS/current; support ended March 3, 2020 | Major runtime, SDK, Windows Desktop, ASP.NET Core, and EF Core transition. |
| .NET Core 3.1 | LTS; support ended December 13, 2022 | Successor LTS refinement, not an extension of 3.0 support. |

## Historical 2.2-to-3.0 checklist

### 1. Record the original application

Inventory:

- project files, target frameworks, SDK version, `global.json`, and explicit
  `LangVersion`;
- NuGet references, ASP.NET Core packages, EF Core providers, and any
  third-party SDK/provider compatibility statements;
- web host/startup configuration, routing, middleware order, authentication,
  and serialization;
- EF Core queries, migrations, generated SQL, provider, and `dotnet ef`
  invocations;
- Windows Forms/WPF controls, designer use, native dependencies, platform
  assumptions, runtime identifiers, and publish scripts.

### 2. Select compatible historical tooling

The .NET Core 3.0 GA release shipped SDK 3.0.100, which included runtime
3.0.0; applications target `netcoreapp3.0`. Visual Studio 2019 16.3 GA was
required to take full advantage of the release. SDK, runtime, compiler, and
target-framework versions are related but not interchangeable. For a
historical exercise, pin only tooling actually available in that isolated
exercise environment.

The compiler's default for a .NET Core 3.x target is C# 8.0 unless
overridden. Review `LangVersion` and nullable-reference-type settings
explicitly; moving to the new TFM is not a substitute for understanding
compiler configuration.

### 3. Update project and package structure

Use the full official ASP.NET Core migration guide for changes to Web SDK
projects, shared framework references, hosting, routing, and middleware.
Web SDK applications moved to an implicit `Microsoft.AspNetCore.App`
framework reference; many old explicit package/metapackage references no
longer applied.

EF Core is no longer supplied by the ASP.NET Core shared framework. Add
explicit NuGet references for EF Core and the intended provider, then
install a compatible `dotnet-ef` tool separately. EF Core 3.0 targeted
.NET Standard 2.1, whereas EF Core 3.1 returned to .NET Standard 2.0; check
provider and consumer support rather than assuming 3.x packages have
identical target compatibility.

### 4. Audit behavior changes

- Review ASP.NET Core 3.0's Generic Host transition, endpoint routing,
  middleware order, and any change in framework/package references.
- Treat Blazor WebAssembly as preview in the 3.0 release timeframe.
- For EF Core, test queries that previously relied on client evaluation.
  Expressions outside the top-level projection that cannot translate now
  throw. Also review query shapes affected by the one-SQL-statement behavior.
- Review EF provider compatibility, model changes, generated SQL, and
  migration output against the official EF Core breaking-change list.
- For Windows desktop applications, remember that Windows Forms and WPF
  are Windows-only; check APIs, designer use, native dependencies, and
  platform-specific behavior.
- Review SDK publish and appHost assumptions against the exact project type
  and deployment mode. Single-file and trimming-related features have
  constraints; don't copy an old publish command as general deployment
  guidance.

See [breaking changes](breaking-changes.md) for an overview and the linked
official catalogs for complete product-specific details.

### 5. Validate any historical reproduction

Use a deliberately isolated environment appropriate to historical tooling.
Record the actual SDK, runtime, package/provider versions, target operating
system, command, and result. Test real query workloads, web flows, desktop
behavior, and publish artifacts as applicable. No historical application is
provided or built/tested as part of this documentation.

## Production migration now

Do not use .NET Core 3.0, 3.1, 2.2, or 2.1 as a production destination:
they are out of support. Check Microsoft's live [support
policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core),
choose a supported target, and use current [.NET porting
guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/) and that
target's compatibility documentation. Historical 3.0 migration steps alone
are insufficient for a present-day migration.

## Official references

- [.NET Core 3.0 release notes (SDK 3.0.100)](https://github.com/dotnet/core/blob/main/release-notes/3.0/3.0.0/3.0.0.md)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [What's new in .NET Core 3.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0)
- [.NET Core 3.0 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/3.0)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [ASP.NET Core: migrate from 2.2 to 3.0](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)
- [What's new in ASP.NET Core 3.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-3.0)
- [New features in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/)
- [EF Core 3.x breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes)
