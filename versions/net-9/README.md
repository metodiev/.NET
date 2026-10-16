# .NET 9 — the STS step between .NET 8 and .NET 10

**Status snapshot: October 16, 2026.** .NET 9 is a stable, generally
available **Standard Term Support (STS)** release, released November 12,
2024. It remains supported, but is in its final maintenance phase and reaches
end of support on **November 10, 2026**—25 days after this snapshot. The
Microsoft support policy lists **9.0.21**, released October 13, 2026, as its
latest runtime patch. Production installations must remain on the latest
servicing patch to qualify for support.

This is a researched learning milestone, not a production recommendation or a
claim that this workspace has compiled or run .NET 9.

## At a glance

| Topic | .NET 9 |
| --- | --- |
| General availability | November 12, 2024 |
| Release type and lifecycle | STS; two years of support |
| End of support | November 10, 2026 |
| State on October 16, 2026 | Supported, maintenance phase, near end of support |
| Latest runtime patch in the dated policy | 9.0.21, October 13, 2026 |
| Target framework moniker | `net9.0` |
| SDK | .NET 9 SDK train; initial GA SDK was 9.0.100. Choose a currently serviced SDK/feature band compatible with CI and IDEs. |
| Default C# | C# 13 when targeting `net9.0` with a current .NET SDK |
| Web framework | ASP.NET Core 9.0; its release notes and breaking changes are framework-specific |
| Data access | EF Core 9 (EF9), a separate STS product; supported through November 10, 2026 |

An SDK is a build toolchain, while a runtime executes applications and a TFM
such as `net9.0` selects an API contract. Their version numbers are related
but not interchangeable. C# is selected by the compiler based on the target
framework: C# 13 is the default for `net9.0`, not a feature intrinsic to the
runtime. Avoid forcing a newer language version than the target supports.

## Why this release matters in the curriculum

The sequence is **.NET 8 LTS → .NET 9 STS → .NET 10 LTS**. .NET 9 is the
compatibility waypoint between the C# 12/.NET 8 generation and
C# 14/.NET 10. It gives learners a place to study the annual release cadence,
servicing changes, and the feature and compatibility deltas at each edge.

As of October 16, 2026, both .NET 8 and .NET 9 have the same November 10,
2026 end-of-support date. .NET 9 is therefore a poor new long-lived
production destination: it has only 25 days of support remaining. Teams
upgrading from .NET 8 can use .NET 9 as a short-lived diagnostic/build
checkpoint, or assess the .NET 9 delta while moving directly to a supported
LTS. Do not mistake a sequential learning timeline for a requirement to
operate production on every intermediate version. For production decisions,
recheck the live support policy and plan for a supported target with adequate
servicing runway.

LTS and STS describe support duration, not quality. Microsoft states that
release quality is the same. STS receives two years of support; supported
systems must keep up with released patches. The policy classifies the final
six months as maintenance, during which releases address security
vulnerabilities rather than adding functional capabilities.

## Keep the product release trains distinct

| Product | .NET 8 generation | .NET 9 generation | .NET 10 generation | What to track separately |
| --- | --- | --- | --- | --- |
| .NET runtime, libraries, SDK | .NET 8 / `net8.0` | .NET 9 / `net9.0` | .NET 10 / `net10.0` | Platform support policy, SDK servicing, TFM compatibility |
| C# default | C# 12 | C# 13 | C# 14 | Compiler and target-framework language defaults |
| ASP.NET Core | 8.0 | 9.0 | 10.0 | Framework release notes, migration docs, and breaking changes; ASP.NET Core is covered by the .NET support policy |
| EF Core | EF Core 8 | EF Core 9 | EF Core 10 | EF release/support statements, provider versions, database compatibility, generated SQL and migrations |

The shared major number signals a release generation; it does not make
ASP.NET Core or EF Core the same product as the runtime. For example, EF9
targets .NET 8 and can run on .NET 8 or .NET 9, while EF9's own support
statement ends November 10, 2026. Check each product's requirements,
providers, and support terms rather than deriving them from a project TFM.
ASP.NET Core 9 has its own feature and breaking-change documentation and
follows the .NET support policy.

## What to learn

1. **C# 13:** `params` collections, the `System.Threading.Lock`-aware
   `lock` pattern, `\e`, improved natural types for method groups, implicit
   indexer access in object initializers, `ref` locals and `unsafe` contexts
   in iterators and async methods, interfaces/type parameters involving
   `ref struct`, partial properties/indexers, and overload-resolution
   priority. Some changes expand expressiveness but also affect overload
   selection and compiler behavior. The `field` contextual-keyword feature
   was preview-only at the .NET 9 release; do not treat it as an ordinary
   C# 13 production feature.
2. **Runtime and base libraries:** dynamic adaptation of GC behavior to
   application size, JIT and Arm64 optimizations, and new APIs in JSON,
   LINQ, cryptography, reflection emit, and time handling. Benefits depend on
   workload and deployment; measure instead of assuming a performance gain.
3. **SDK and build tools:** workload sets, improved test/MSBuild
   integration, Terminal Logger enabled by default, MSBuild build checks,
   SDK/MSBuild version-mismatch handling, and workload installation history.
   These changes can affect CI scripts and local toolchains even if source
   code is unchanged.
4. **ASP.NET Core 9:** optimized build/publish-time static asset delivery,
   Blazor rendering and authentication improvements, built-in OpenAPI
   generation support, and expanded Native AOT support. These are web
   framework features, not guarantees that an existing app's hosting or
   publish configuration needs no changes.
5. **EF Core 9:** improved Cosmos DB query and partition-key handling,
   relational query and JSON improvements, seeding APIs, and progress toward
   precompiled queries/AOT. Provider behavior, database features and upgrade
   risks need their own review.

Detailed curated tours are in [What's new](whats-new.md),
[Compatibility and breaking changes](breaking-changes.md), and the
[migration guide](migration-guide.md).

## Safe use on the snapshot date

- Do not start a new long-lived production deployment on .NET 9: its support
  ends November 10, 2026. For applications that cannot complete a migration
  before then, choose a currently supported target and organize a staged
  transition with a rollback plan.
- For an already-running .NET 9 service, deploy the latest available
  servicing update and plan migration now. A version number alone is not
  evidence of patch compliance.
- Treat runtime, SDK, ASP.NET Core, EF Core, database providers, OS images,
  and third-party libraries as related but independently versioned
  components. Verify their supported combinations.
- Read all applicable .NET 8 and .NET 9 change catalogs when upgrading from
  .NET 8, and account for EF/database schema changes separately from
  application rollback.
- Test the exact production publish mode, operating systems, architecture,
  database engine/provider, proxies, and container image. Compilation is
  necessary but insufficient evidence for production readiness.

## Official Microsoft sources

- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) — release dates, STS/LTS, current patch, support phase, end-of-support, and servicing rules. The live page's October 13, 2026 snapshot was used for this milestone.
- [.NET 9 release notes](https://github.com/dotnet/core/blob/main/release-notes/9.0/9.0.0/9.0.0.md) — GA and initial SDK/runtime details.
- [What's new in .NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview) — runtime, libraries, SDK, MAUI, AI, ASP.NET Core, and EF Core overview.
- [C# 13](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13) and [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning) — features, default, and supported target-framework relationship.
- [ASP.NET Core 9 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0?view=aspnetcore-9.0)
- [EF Core 9 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew) — EF9 requirements, features, and its support end.
- [.NET 9 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0), [ASP.NET Core 9 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/9/overview?view=aspnetcore-9.0), and [EF Core 9 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes).
- [What's new in .NET 10](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview) — forward context for the next LTS generation.
