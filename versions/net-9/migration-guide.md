# Migration guide: .NET 8 → .NET 9 → .NET 10

This is a controlled compatibility path, not advice to make .NET 9 a
long-lived production stop. On **October 16, 2026**, both .NET 8 and .NET 9
are supported but end support on **November 10, 2026**. .NET 9 is STS and
has only 25 days remaining. For a production destination that needs runway,
plan for a currently supported LTS such as .NET 10, after accounting for all
intermediate changes. Recheck the live Microsoft support policy before
making a deployment decision.

The general sequence helps isolate compatibility differences. An
organization does **not** have to release production on .NET 9 before moving
from .NET 8 to .NET 10.

## Phase 0: inventory, baseline, and choose the destination

Before changing code or deployment:

1. Record each project's TFM, `global.json` SDK selection, runtime and
   hosting model, build agent/IDE, deployment runtime, OS and architecture.
2. Inventory framework and package versions: ASP.NET Core integrations,
   `Microsoft.Extensions.*`, EF Core, EF providers/tools, test runners,
   analyzers, source generators, Aspire, MAUI workloads, and native
   dependencies.
3. For data systems, record database engine/version and compatibility level,
   schema migration history, query plans for critical queries, and how
   migrations/seeding are applied in production.
4. Record publish features: framework-dependent or self-contained, trimming,
   Native AOT, single-file, ReadyToRun, runtime identifiers, containers and
   image variants.
5. Capture a passing build/test and operational baseline, including API
   contracts, startup, telemetry, auth, latency, memory, error rates, and
   rollback/restore procedures.
6. Decide the actual production target based on support runway and dependency
   readiness. On the snapshot date, do not plan a new long-lived .NET 9
   deployment.

Keep an application rollback independent from a database rollback: a
reverted binary may not be compatible with a schema already changed by a
forward migration.

## Phase 1 (optional diagnostic checkpoint): .NET 8 to .NET 9

Use this step to isolate .NET 9-specific changes if it helps diagnose a
direct .NET 8 → 10 migration. Keep it time-bounded.

1. Install a stable, currently serviced .NET 9 SDK supported by the team's
   IDE/build agents. Pin it consistently in developer environments and CI.
   Don't assume the SDK number, runtime patch, and `net9.0` are the same.
2. Change applicable project TFMs to `net9.0`; review every target in
   multi-targeted libraries. The compiler defaults to C# 13 for this TFM.
   Remove unnecessary `LangVersion` overrides rather than enabling
   unsupported preview/newer syntax.
3. Update version-coupled packages intentionally. ASP.NET Core 9 is the web
   generation; EF Core 9 is a separately versioned product that can target
   .NET 8 or .NET 9. Align each EF provider and tooling version with EF9 and
   verify provider/database compatibility. Don't assume every package should
   have its major version changed.
4. Review the [.NET 9 compatibility checklist](breaking-changes.md) and the
   full [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0),
   [ASP.NET Core 9](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/9/overview?view=aspnetcore-9.0),
   and [EF Core 9](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes)
   catalogs. If coming from before .NET 8/EF8, include each skipped major
   release's catalog too.
5. Resolve source issues and test environment-variable runtime selection,
   C# overload binding, keyed DI, dev service validation, container-native
   dependencies, generated API contracts, and production-specific behavior.
6. For EF, generate migrations and review them. Fix pending model changes,
   deterministic model seeding and explicit-transaction migration patterns;
   verify query translation, data, provider and DB behavior using integration
   tests and production-like data.
7. Deploy only to a representative test/staging environment when this
   checkpoint is necessary. Treat a passing build as a milestone, not proof
   of security, performance or operational suitability.

In October 2026, an application already on .NET 8 generally should not pause
on .NET 9 as its final production upgrade. The support window is nearly over
for both.

## Phase 2: .NET 9 to .NET 10 (or direct .NET 8 to .NET 10)

For the forward production migration, use a stable .NET 10 SDK/runtime and
read the .NET 10-specific compatibility and product documentation. If
moving directly from .NET 8, review both .NET 9 and .NET 10 changes:

1. Install a stable .NET 10 SDK and align `global.json`, CI agents, SDK
   container images, IDEs and build scripts. Do not use an old preview SDK
   for a production release.
2. Move project TFMs to `net10.0` where appropriate and review all targets.
   C# 14 is the target default. Keep multi-targeting intentional and use
   supported language versions per target.
3. Upgrade framework packages coherently: ASP.NET Core 10 for web workloads,
   EF Core 10 plus compatible provider/tool versions for ORM workloads, and
   compatible third-party packages. Each framework/provider has its own
   support and compatibility details.
4. Read the [.NET 10 compatibility catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10),
   [ASP.NET Core 10 changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/overview?view=aspnetcore-10.0),
   and [EF Core 10 changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes),
   along with the .NET 9/ASP.NET Core 9/EF Core 9 differences relevant to
   your starting version.
5. Review generated migrations and SQL in a controlled branch. Check provider
   support, schema changes, data conversions, indexes, query plans, and a
   restore/rollback path before running against production.
6. Test published artifacts (including trimming/AOT if used), clean container
   builds, startup/health checks, certificate and native package availability,
   auth and proxy paths, telemetry, and every supported RID/OS.
7. Roll out progressively with monitoring and an explicit rollback or
   roll-forward plan. Keep the application runtime, frameworks, providers,
   SDK, base images, and database patch policies separate and current.

Microsoft's
[ASP.NET Core 9-to-10 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/90-to-100?view=aspnetcore-10.0)
is a framework-specific reference. It does not replace .NET runtime or EF
Core migration review.

## Production safety gates

- **Support:** verify release status and latest required patches on the day
  of rollout. A supported major on an old patch may itself be out of
  servicing compliance.
- **Compatibility:** build and test using the exact stable SDK and TFM that
  CI and release use. Test changes in externally visible behavior, not only
  source compilation.
- **Persistence:** inspect all EF migrations and generated SQL before
  execution. Back up and rehearse restore; schema rollback is not guaranteed
  just because application artifacts can be redeployed.
- **Dependencies:** confirm vendor support for the chosen runtime and
  framework versions, including database drivers, native libraries,
  telemetry agents, authentication libraries and build tools.
- **Deployment:** test the actual OS/container variant and architecture,
  least-privilege permissions, startup, certificates, secrets, health checks,
  logging and observability.
- **Performance/security:** compare representative loads and execute
  application security/regression tests on patched production-equivalent
  infrastructure. Don't treat platform feature availability as evidence
  that a workload is faster, secure, or AOT-compatible.

## Verification limits for this milestone

This milestone adds documentation only; it does not contain a runnable .NET
project. No .NET SDK, build, test, migration, or executable feature sample
was run as part of creating these pages. The lifecycle and release facts
were checked against official Microsoft documentation, including the support
policy dated October 13, 2026; that live policy can change after the
snapshot. Validate the exact SDK, runtime, application, providers, database,
OS and deployment mode before using this guide operationally.

## Official transition sources

- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET 9 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0)
- [.NET 10 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10)
- [ASP.NET Core 9-to-10 migration](https://learn.microsoft.com/en-us/aspnet/core/migration/90-to-100?view=aspnetcore-10.0)
- [ASP.NET Core 9 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/9/overview?view=aspnetcore-9.0)
- [ASP.NET Core 10 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/overview?view=aspnetcore-10.0)
- [EF Core 9 what's new and support](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew)
- [EF Core 9 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes)
- [EF Core 10 what's new and requirements](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
- [EF Core 10 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
