# Historical migration guide: .NET Core 3.1 → .NET 5 → .NET 6

> **Production warning:** .NET Core 3.1, .NET 5, and .NET 6 are all out of
> support. This guide explains the historical compatibility path; it does
> not recommend any of them as a current production destination. For
> production, select a currently supported .NET release, keep it patched,
> and use that release's official upgrade guidance.

## Why consider the intermediate release?

.NET Core 3.1 was the previous LTS line. **.NET 5 was an STS release** and
introduced the platform naming/unification transition: the platform name
dropped “Core,” C# 9 arrived, and runtime, libraries, single-file publishing,
trimming, and Arm64 support evolved. **.NET 6 was the next LTS release**,
with `net6.0`, C# 10, runtime/SDK changes, ASP.NET Core 6, and EF Core 6.

An application need not be deployed to .NET 5 before targeting a later
release. However, reviewing the .NET 5 compatibility changes helps locate
issues between 3.1 and 6 and prevents attributing every difference to .NET
6. In a current migration, add every later release between the original
application and the supported destination to the review.

| Historical step | TFM | Context |
| --- | --- | --- |
| Starting point | `netcoreapp3.1` | LTS; support ended December 13, 2022. |
| Intermediate compatibility boundary | `net5.0` | STS; support ended May 10, 2022. Renamed/unified platform, C# 9, and other platform and publish changes. Do not deploy as a stopping point. |
| Historical destination | `net6.0` | LTS; support ended November 12, 2024. Initial SDK 6.0.100; C# 10 default. Unsupported today. |

## Preparation and inventory

Before changing a real application, record:

- Each project TFM, selected SDK, `global.json`, and any explicit
  `LangVersion`; SDK and runtime versions are not the same value.
- NuGet dependencies, analyzers, source generators, native dependencies,
  runtime identifiers, and deployment model (framework-dependent,
  self-contained, single-file, trimmed, or container).
- ASP.NET Core packages, hosting model, middleware order, authentication,
  routing, logging, reverse-proxy setup, and health checks.
- EF Core packages, database provider and version, query patterns, migrations,
  database compatibility settings, and schema/data backup and recovery plan.
- Representative test results, API and serialization contracts, startup
  behavior, resource use, and operational metrics to compare after changes.

Capture a baseline in a safe environment. Do not edit production in place or
treat a compiler result as operational validation.

## Historical .NET 6 project changes

For a project specifically being brought to .NET 6 for historical analysis:

1. Install the exact .NET 6 SDK required by the exercise. The initial SDK
   was 6.0.100; later feature bands and servicing releases exist. If
   `global.json` pins an SDK, select an installed version compatible with
   the intended environment.
2. Change the project TFM from `netcoreapp3.1` or `net5.0` to `net6.0` only
   in the isolated migration branch. This is a target-framework change; it
   does not itself upgrade all packages.
3. Update framework-dependent package references and tooling deliberately.
   Use compatible ASP.NET Core / Microsoft.Extensions packages, EF Core 6
   packages, the exact provider package, and a matching `dotnet-ef` tool
   where needed. Check provider support and design-time tooling separately.
4. Review `LangVersion`. C# 10 is the default for `net6.0`; normally remove
   unnecessary overrides rather than using a language version newer than
   the target supports.
5. Read the .NET 5 and .NET 6 breaking-change catalogs and the ASP.NET Core
   and EF Core catalogs. Resolve actual warnings and behavior changes rather
   than silencing warning sets indiscriminately.

For ASP.NET Core applications, Microsoft maintains direct historical
migration instructions from both 3.1 and 5.0 to .NET 6. Follow their project
and package steps while separately checking platform and EF Core changes.
Do not infer a direct, generic .NET runtime migration solely from the
ASP.NET Core article.

## Test the changes that matter

- Exercise file and stream I/O on every supported OS; include asynchronous
  reads, seek/position behavior, locking, and `System.Drawing.Common` use.
- Verify startup and middleware order, HTTPS redirection, reverse-proxy
  behavior, response status codes, streamed API responses, cookies, and
  authentication flows.
- Confirm background-service exception policy, host shutdown/restart,
  readiness/liveness behavior, and log format in the actual container/runtime
  environment.
- For EF Core, test the exact provider/database pair, migrations, temporal
  table schema if adopted, nested optional dependents, owned entity
  lifetimes, query translation, generated SQL, and data restoration.
- Exercise packaging and publishing for each runtime identifier, native
  dependency, trimming/single-file setting, and CI pipeline. Check build
  scripts for assumptions about output paths or SDK workload installation.
- Compare behavior and performance using production-like workloads. Feature
  availability or release-note performance claims are not proof of gains in
  this application.

No historical .NET 6 project or database migration is supplied here, and no
build, test, publish, or runtime validation is claimed.

## Production-safe destination today

Do not deploy or pause on .NET Core 3.1, .NET 5, or .NET 6. Check Microsoft's
live support policy at planning time and select a currently supported target
with an adequate support horizon. For the repository's October 16, 2026
snapshot, .NET 10 is the current LTS baseline; verify the live policy and
latest patch again before acting.

Plan the actual upgrade from the source directly to the supported target
where dependencies permit, while reviewing relevant .NET 5, 6, 7, 8, 9, and
destination compatibility catalogs as applicable. Upgrade one logical layer
at a time where practical (SDK/TFM, application framework, data provider,
OS/container, deployment tooling), run tests in staging, and retain a
rollback or roll-forward strategy. Do not let an unsupported intermediate
version become a production landing zone.

Microsoft's .NET Upgrade Assistant documentation now marks that tool
deprecated and directs users to the Copilot modernization chat agent in
supported Visual Studio versions. Any automated modernization output still
requires review and application-specific validation.

## Official references

- [.NET 6 initial release and SDK 6.0.100](https://github.com/dotnet/core/blob/main/release-notes/6.0/6.0.0/6.0.0.md)
- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET 5 what's new / unification and platform history](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- [.NET 6 what's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [.NET 6 compatibility catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/6.0)
- [ASP.NET Core 3.1 to .NET 6 migration](https://learn.microsoft.com/en-us/aspnet/core/migration/31-to-60?view=aspnetcore-6.0)
- [ASP.NET Core 5 to .NET 6 migration](https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60?view=aspnetcore-6.0)
- [ASP.NET Core 6 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/6/overview?view=aspnetcore-6.0)
- [EF Core 6 release plan and .NET 6 requirement](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/plan)
- [EF Core 6 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew)
- [EF Core 6 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/breaking-changes)
- [.NET Upgrade Assistant status](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview)
