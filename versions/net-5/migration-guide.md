# Historical migration guide: .NET Core 3.1 to .NET 5

> **Status warning:** .NET 5 support ended May 10, 2022. This is a historical
> learning checklist, not a recommendation to deploy .NET 5. For a real
> upgrade, choose a currently supported target and follow its current
> migration guidance.

## Understand the transition

.NET 5 is the successor to .NET Core 3.1 and the first release in the unified
.NET product line. It was an STS release, unlike .NET Core 3.1, which was LTS.
The .NET 5 TFM is `net5.0`; the initial SDK was 5.0.100; the C# default moves
from C# 8.0 on `netcoreapp3.x` to C# 9.0 on .NET 5 targets. The compiler
default, application target, SDK, runtime, ASP.NET Core, and EF Core are
related but distinct versioning systems.

An application does not have to deploy every intervening unsupported runtime
to move to a newer supported platform. For historical study, the 3.1-to-5
transition is useful because it shows how a TFM, SDK, and product packages
move together. For present-day production, do not choose .NET 5 or .NET Core
3.1 as the destination.

## Historical upgrade checklist

### 1. Inventory the existing application

Record:

- Every target framework and SDK selection, including `global.json`,
  `Directory.Build.*`, and project-level `LangVersion` settings.
- NuGet dependencies, native dependencies, test tools, build agents, and
  package-source assumptions.
- Runtime identifiers, operating-system/architecture requirements, and
  whether deployment is framework-dependent, self-contained, single-file, or
  trimmed.
- Any Windows Forms/WPF use, web framework and hosting setup, EF Core
  providers, database schema, authentication flow, and generated SQL.

Do not infer current compatibility merely from the original project
compiling under its old environment.

### 2. Set target and SDK deliberately

For a historical .NET 5 exercise, change the application target from
`netcoreapp3.1` to `net5.0` and use an appropriate .NET 5 SDK. The first
release shipped SDK 5.0.100; later SDK feature bands and servicing versions
are separate SDK versions. A `global.json` pin must match an installed SDK.

The C# 9.0 default follows the `net5.0` target when `LangVersion` is
unspecified. Review explicit language settings and compiler/analyzer
versions. Do not treat changing the TFM as equivalent to upgrading every
compiler or dependency.

### 3. Review project and package changes by product

- **.NET / SDK:** consult the .NET 5 release notes and compatibility
  catalog for SDK behavior, APIs, runtime changes, and deployment.
- **ASP.NET Core:** follow the dedicated
  [3.1-to-.NET 5 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/31-to-50).
  Verify framework references and package versions, hosting, middleware,
  routing, authentication, and deployment details used by the app.
- **EF Core:** align EF Core packages and the selected provider deliberately.
  Review the EF Core 5 feature and breaking-change pages, and confirm the
  provider's support and migrations. EF Core is not supplied implicitly by
  the ASP.NET Core shared framework.
- **Desktop and other libraries:** check the precise API surface and platform
  support for Windows Forms, WPF, native dependencies, and third-party
  libraries.

Keep ASP.NET Core and EF Core release histories distinct from the runtime.
Matching major version numbers do not make their packages interchangeable.

### 4. Audit behavior and deployment

Use the versioned breaking-change catalog to identify application-relevant
changes; do not try to apply every catalog item indiscriminately. Exercise
representative paths for the project's APIs, serialization, networking,
globalization, compiler warnings, and runtime behavior. For data applications,
test EF queries and migrations against representative data and inspect
provider-generated SQL.

Validate the actual publish mode and target OS. Trimming, single-file
publishing, appHost, runtime identifiers, and framework-dependent versus
self-contained deployment have different constraints. An old publish
command or deployment image should not be copied without checking its
versioned documentation.

### 5. Validate historically only in isolation

If the goal is to reproduce a .NET 5-era build, use an isolated,
disposable environment with the appropriate historical SDK and dependencies.
Record the actual SDK, runtime, OS, package sources, commands, and results.
Do not expose an unsupported runtime or application to untrusted traffic or
data.

No runnable .NET 5 project is included in this milestone, and no historical
build or test has been performed or claimed.

## Production migration today

The runtime support policy lists .NET 5 as out of support since May 10, 2022.
Choose a currently supported .NET release and then use its current official
porting and migration documentation. As of the repository's
[October 16, 2026 lifecycle snapshot](../support-lifecycle.md), .NET 10 is
the current LTS release; check Microsoft's live [support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
before acting because lifecycle status changes.

Microsoft's .NET Upgrade Assistant documentation currently marks that tool
deprecated and points to the GitHub Copilot modernization chat agent for
supported upgrade workflows in qualifying Visual Studio versions. Tool
availability and suitability depend on the development environment; use the
current official guidance and validate every automated change. No migration
tool can replace application-specific testing or product-provider
compatibility review.

## Official references

- [.NET 5 release notes and SDK 5.0.100](https://github.com/dotnet/core/blob/main/release-notes/5.0/5.0.0/5.0.0.md)
- [Announcing .NET 5.0](https://devblogs.microsoft.com/dotnet/announcing-net-5-0/)
- [ASP.NET Core 3.1-to-.NET 5 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/31-to-50)
- [ASP.NET Core 5 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-5.0)
- [.NET 5 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/5.0)
- [EF Core 5 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew)
- [EF Core 5 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/breaking-changes)
- [.NET porting guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/)
- [.NET Upgrade Assistant overview and current deprecation notice](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview)
- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
