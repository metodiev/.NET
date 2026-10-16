# Migration guide: .NET 8 → .NET 9 → .NET 10

This guide frames the two major transitions in the .NET 8-to-10 learning path.
The intermediate .NET 9 release is useful for understanding changes and
isolating regressions, but it is not a required long-term production stop.
As of October 16, 2026, both .NET 8 and .NET 9 reach end of support on
November 10, 2026; plan to land on supported .NET 10 rather than extend an
unsupported deployment.

## Before changing the target

Record the current application and production baseline:

- SDK selection (`global.json`), target framework(s), runtime and hosting
  model, build agents, IDE/compiler, and deploy-time runtime.
- Package inventory, including ASP.NET Core integrations, EF Core and
  providers, source generators, analyzers, test framework/platform, and tools.
- Database engine/version and compatibility level; migration history and
  generated SQL; container base images and native OS packages.
- Publish features such as self-contained deployment, trimming, Native AOT,
  single-file, ReadyToRun, and runtime identifiers.
- Operational baselines for startup, throughput, memory, telemetry,
  authentication, and rollback.

Keep a passing baseline and deployable artifact before upgrading. Plan a
roll-forward/rollback strategy and database compatibility approach separately;
rolling back application bits may not reverse a destructive schema migration.

## Transition 1: .NET 8 to .NET 9

Use the .NET 9 release and compatibility notes as an explicit checkpoint:

1. Install and pin a stable .NET 9 SDK in a controlled branch and update the
   app/library target to `net9.0` where appropriate.
2. Upgrade coherent package families—particularly ASP.NET Core integrations,
   EF Core and its providers—against their .NET 9 release documentation.
   Do not update only the .NET runtime and assume third-party packages are
   compatible.
3. Compile under C# 13's target-framework default and remove obsolete APIs or
   warnings that block the next transition.
4. Review the [.NET 9 compatibility catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0),
   plus the ASP.NET Core 9 and EF Core 9 release notes/breaking changes for
   products the app uses.
5. Run unit/integration tests, deployment tests, database migrations, and
   representative performance checks. Preserve the findings as a reference
   for the next step.

Treat this stage as a diagnostic waypoint. Since .NET 9 is STS and shares the
November 10, 2026 end-of-support date with .NET 8, avoid making it a new
long-lived production destination.

## Transition 2: .NET 9 to .NET 10

1. Install a **stable** .NET 10 SDK in the build environment and make
   `global.json`, SDK images, CI agents, and developer tooling agree. The
   initial GA SDK was 10.0.100; select a current servicing SDK compatible
   with your environment rather than pinning an old preview build.
2. Change the target framework to `net10.0`. Review every target in
   multi-targeted projects and keep library targets intentional. C# 14 is
   selected by default for `net10.0`; remove unnecessary explicit
   `LangVersion` values, especially preview/latest values that could create
   inconsistent builds.
3. Upgrade Microsoft packages in compatible release families: ASP.NET Core
   10.0 for web applications and EF Core 10.0 plus matching providers/tools
   for EF applications. Review package vendors' support statements and do
   not infer provider compatibility from EF's major number.
4. Review .NET, ASP.NET Core, EF Core, and C# changes in the
   [breaking-change checklist](breaking-changes.md). Prioritize container
   image OS, ASP.NET authentication/API behavior, Blazor streaming, EF
   generated SQL and migrations, and SDK/CI scripts.
5. Inspect generated migrations and SQL before applying them. For
   multi-targeted projects, update EF tool invocations to specify the target
   framework. Compare plans and latency for parameterized collection queries
   and validate any native SQL Server JSON migration against the exact server
   and compatibility level.
6. Test release-mode publish artifacts for each supported RID and deployment
   style. Validate trimming/AOT if used, native dependencies, startup,
   readiness/liveness, telemetry propagation, authentication/authorization,
   background services, and rollback.
7. Deploy progressively with observability and a rollback/roll-forward plan.
   Keep runtime, ASP.NET Core, EF/provider, database, and image patch policies
   on their own update calendar; verify the supported latest patch before
   release.

## Optional direct move from .NET 8

The chronology is 8 → 9 → 10, and the source catalogs for 9 remain valuable
when moving directly from 8. Teams need not deploy a durable .NET 9 version:
they can target .NET 10 directly after inventorying changes in both .NET 9
and .NET 10 and any framework/provider releases they cross. If using a
temporary .NET 9 build to isolate a defect, keep that stage time-bounded and
do not leave it past support end.

For an application using ASP.NET Core, Microsoft's detailed
[ASP.NET Core 9-to-10 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/90-to-100?view=aspnetcore-10.0)
covers SDK selection and framework-specific changes. It is a focused guide,
not a substitute for the general .NET 10 or EF Core migration review.

## Verification and workspace caveat

This repository milestone is documentation-only. The workspace has only a
.NET 10 preview SDK, so no claim is made that the stable .NET 10 SDK, runtime,
ASP.NET Core 10, EF Core 10, or any .NET 10 executable sample was installed,
built, tested, or run here. Before declaring migration complete, validate
with stable .NET 10 SDK/runtime and the application's actual database,
operating systems, deployment images, and dependencies.

## Official transition references

- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) — as-of support and servicing status.
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support) — SDK/runtime and patching guidance.
- [.NET 9 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0)
- [.NET 10 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10)
- [ASP.NET Core 9-to-10 migration](https://learn.microsoft.com/en-us/aspnet/core/migration/90-to-100?view=aspnetcore-10.0)
- [ASP.NET Core 10 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/overview?view=aspnetcore-10.0)
- [EF Core 10 what's new and requirements](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
- [EF Core 10 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
