# Migration guide: .NET 6 → .NET 7 → .NET 8

This is a practical historical upgrade path, not a production-target
recommendation for October 16, 2026. .NET 8 is still in support on that date,
but reaches end of support on November 10, 2026. For a new deployment or a
migration that cannot move again promptly, choose a currently supported LTS
target. If .NET 8 is required as an intermediate or for compatibility work,
finish the upgrade quickly and keep every production component patched.

## Why preserve the .NET 7 step?

.NET 7 was the STS release between the .NET 6 LTS and .NET 8 LTS. Its support
ended May 14, 2024; .NET 6 support ended November 12, 2024. Do not deploy or
pause on either as an operational destination in 2026. But the .NET 7
breaking-change set is part of the upgrade path: changes can occur at the
runtime, BCL, SDK, ASP.NET Core, containers, and EF Core layers. Record and
test the changes at both edges instead of attributing all differences to
.NET 8.

## Phase 0: inventory and plan

1. Record every project TFM, SDK selection (`global.json`), package and tool
   version, deployment runtime, OS/container image, architecture, and
   third-party/native dependency.
2. Inventory ASP.NET Core separately from the base runtime and identify each
   EF Core provider, database engine/version, and database compatibility
   level.
3. Capture a baseline: build and test results, API/behavior contracts,
   migration/schema state, query plans for critical queries, deployment
   manifests, startup/health checks, and representative performance data.
4. Check whether dependencies support .NET 8 and the target OS. Identify
   packages that are version-coupled (including Microsoft.Extensions,
   ASP.NET Core, EF Core, and tooling) and distinguish them from independently
   versioned dependencies.
5. Agree on the real production destination. On the snapshot date, .NET 8's
   remaining support interval is too short for a new long-lived production
   commitment.

## Phase 1: .NET 6 → .NET 7 compatibility pass

Use the .NET 7 SDK in an isolated branch/build environment to reveal changes
at the first transition. Update the SDK selector and target to `net7.0` only
for this analysis step; align framework packages and tools when needed to
build that state. Do not release or maintain .NET 7 as a production stopping
point.

Review the [.NET 7 breaking-change catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0)
and the dedicated ASP.NET Core/EF Core 7 catalogs for relevant changes. For
example, .NET 7 made BinaryFormatter APIs compiler errors and changed
behavior in areas including date/time arithmetic precision, regular
expression ranges, numeric format precision, and reflection/stream behavior.
Do not assume any example is relevant until checked against actual APIs used.

Keep the source tree/build green at this intermediate point where practical;
that isolates first-step changes and simplifies regression diagnosis. If a
direct intermediate build is infeasible, record why and use source references
plus targeted tests to account for the skipped delta.

## Phase 2: .NET 7 → .NET 8 upgrade

1. **Install and pin the SDK.** Update `global.json` only to an installed
   .NET 8 SDK version/feature band used by the team and CI. The SDK is not the
   runtime and SDK version numbers are not TFM values.
2. **Change target framework.** Update project TFMs to `net8.0`. Multi-targeted
   libraries should retain only targets they intend to support and verify
   APIs against each target.
3. **Align framework packages and tools.** Update applicable
   `Microsoft.AspNetCore.*`, `Microsoft.EntityFrameworkCore.*`,
   `Microsoft.Extensions.*`, and related `System.Net.Http.Json` references
   to compatible 8.0 releases. Avoid blanket-upgrading unrelated dependencies
   without checking their own compatibility.
4. **Review C# language settings.** The target's default is C# 12. Remove
   unnecessary `LangVersion` overrides; check analyzers/generators and
   shared libraries that compile under older SDKs before using new syntax.
5. **Review breaking changes by layer.** Use this milestone's
   [compatibility notes](breaking-changes.md), then read the complete
   [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0),
   [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/8/overview),
   and [EF Core 8](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes)
   catalogs.
6. **Exercise hosting and deployment.** Confirm trusted forwarded proxies,
   antiforgery for minimal API uploads, rate-limiter/logging service
   registration, container port 8080, non-root file access, native packages,
   health checks, and ingress/service ports.
7. **Validate persistence.** Check EF Core SQL/provider compatibility,
   SQL Server database compatibility level 130+ for relevant `Contains`
   translations, enum JSON representation, date/time mappings, generated
   migrations, and rollback/restore procedures.
8. **Test publish modes.** If using trimming, Native AOT, single-file, or
   containers, inspect diagnostics and validate reflection, serializers,
   startup, OS/CPU architecture, secrets, certificates, and runtime identity.
9. **Service and deploy.** Run tests on the exact .NET 8 patch and SDK in CI,
   deploy to a representative staging environment, monitor, and roll out
   incrementally. Patch runtime and framework packages throughout the support
   period.

Microsoft's ASP.NET Core migration article gives the project mechanics:
[ASP.NET Core 6 → 7](https://learn.microsoft.com/en-us/aspnet/core/migration/60-to-70)
and [ASP.NET Core 7 → 8](https://learn.microsoft.com/en-us/aspnet/core/migration/70-to-80).
Follow those product-specific instructions for Blazor, hosting, templates,
and framework packages.

## Phase 3: test and choose a supported destination

Retest unit/integration/contract tests and all deployment-specific paths.
For performance-sensitive systems, compare baseline and upgraded behavior
using the same datasets, hardware, runtime configuration, and realistic
traffic. Treat test outcomes as evidence for this application, not a general
claim that an untested sample or migration works.

Given the October 16, 2026 snapshot, schedule the next migration before
November 10, 2026; prefer planning directly for the current supported LTS
when feasible. A supported release requires current patches. Review the
[support lifecycle snapshot](../support-lifecycle.md) and live Microsoft
policy before deployment; support data can change.

## Verification limits for this curriculum

This repository adds no runnable .NET 8 sample. At the time of inspection,
the machine had .NET 8 runtimes (8.0.18) but no stable .NET 8 SDK; it had
.NET 9 SDKs and a .NET 10 preview SDK. Therefore no .NET 8 build, test,
migration, or executable example was run or validated here. Runtime presence
alone cannot compile a project or establish production compatibility. Recheck
the local SDK and runtime setup before attempting the steps above.

## Sources

- [Microsoft support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET 6 → 7 ASP.NET Core migration](https://learn.microsoft.com/en-us/aspnet/core/migration/60-to-70)
- [.NET 7 → 8 ASP.NET Core migration](https://learn.microsoft.com/en-us/aspnet/core/migration/70-to-80)
- [.NET 7 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0)
- [.NET 8 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0)
- [EF Core 8 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes)
