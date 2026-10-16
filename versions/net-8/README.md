# .NET 8 — the LTS bridge from .NET 6 to .NET 10

**Released:** November 14, 2023
**Release type:** Long Term Support (LTS)
**End of support:** November 10, 2026
**Status on October 16, 2026:** Supported, in maintenance, and close to end of support. The Microsoft policy snapshot lists .NET 8.0.32, released October 13, 2026, as the latest patch. Supported deployments must stay current on patches.
**Default C#:** C# 12 when targeting `net8.0` with the .NET 8 SDK.
**SDK and target:** .NET SDK 8.0 feature bands; target framework moniker `net8.0`. SDK versions and runtime versions are distinct.
**Associated products:** ASP.NET Core 8 and EF Core 8 are related but separately documented release trains; neither product’s versioning or product-specific changes should be inferred solely from the .NET runtime.

## Why this release matters

.NET 8 is the LTS successor to .NET 6, with .NET 7 as the intervening
shorter-support release. It brought performance and runtime changes, new
framework APIs, C# 12, improvements across the web and data stacks, and
updates to build and publish tooling. It is a useful compatibility and
architecture milestone—not a new greenfield recommendation on the snapshot
date: the end-of-support date is only 25 days after this curriculum snapshot.
For production planning on October 16, 2026, evaluate the supported .NET 10
LTS release instead of starting a new .NET 8 deployment.

This milestone keeps the timelines separate:

- The **.NET runtime, base libraries, and SDK** are governed by the .NET
  release and support policy.
- **C# 12** is a language/compiler release. The .NET 8 SDK supplies the
  compiler and makes C# 12 the default for `net8.0`; the runtime itself does
  not dictate source-language syntax.
- **ASP.NET Core 8** is the web framework generation shipped with .NET 8 on
  November 14, 2023. It is included in the .NET support-policy coverage, but
  its release notes, migration instructions, and breaking-change guidance
  have framework-specific content.
- **EF Core 8 (EF8)** is a separately documented LTS data-access release
  announced November 14, 2023. EF8's own what's-new page says support lasts
  until November 10, 2026; it requires .NET 8 to build and run. Its provider
  support and behavior changes must be checked independently.

The shared major number does not make ASP.NET Core and EF Core the same
product history. In particular, do not infer provider compatibility, APIs,
or framework-specific migration steps from the .NET runtime version alone.

## Version facts

| Question | Answer |
| --- | --- |
| When was it released? | November 14, 2023, according to the Microsoft .NET support policy and .NET 8 launch announcement. |
| LTS or STS? | LTS. LTS/STS is a support-duration distinction, not a quality ranking. |
| End of support and status? | November 10, 2026. On October 16, 2026, it remains supported in maintenance. The latest patch recorded in the policy is 8.0.32 (October 13, 2026); keep production systems current. |
| What is the default C#? | C# 12 for projects targeting .NET 8. The compiler chooses a default based on the target framework; avoid manually selecting a newer unsupported language version. |
| What are the SDK and target? | Install a .NET 8 SDK (8.0.x feature band as appropriate) and target `net8.0`. A runtime alone is not an SDK, and an SDK version is not the target framework. |
| What versions are ASP.NET Core and EF Core? | ASP.NET Core 8 is the web framework paired with .NET 8; ASP.NET-specific migration and compatibility docs apply. EF Core 8 is a separate LTS product supported until November 10, 2026, and specifically requires .NET 8. |
| Is it a good new production target on this snapshot date? | No. Although still supported on October 16, its November 10 end-of-support date is imminent. Plan toward a currently supported LTS rather than creating a new .NET 8 dependency. |

## What to learn

1. **C# 12:** primary constructors for classes and structs, collection
   expressions, optional lambda parameters, aliasing any type, inline arrays,
   and `ref readonly` parameters. Interceptors remain experimental; do not
   treat them as a generally available production feature.
2. **Runtime and base libraries:** dynamic profile-guided optimization
   (Dynamic PGO) is enabled by default; the JIT, Arm64, and SIMD work improved;
   AVX-512 support was added; and the GC gained a way to refresh a process
   memory limit. These are workload-dependent capabilities, not a promise
   that every application becomes faster without measurement.
3. **SDK/tooling:** terminal build logging, MSBuild property/item/target-result
   queries, simplified artifact output, workload cleanup, and restore security
   auditing can improve local development and CI. Check implications before
   changing output layouts or build pipelines.
4. **ASP.NET Core:** Blazor's Blazor Web App model brings static server
   rendering and selectable interactivity modes; minimal APIs and the server
   stack also gained features. New framework behavior can require code or
   deployment changes.
5. **EF Core:** complex types, primitive collections, JSON mapping, improved
   SQL translation, and `HierarchyId` support broaden the modeling and query
   options. Actual translation and support depend on the provider and database.

See [What's new](whats-new.md) for a categorized tour, [breaking changes](breaking-changes.md)
for compatibility risks, and the [migration guide](migration-guide.md) for a
staged 6 → 7 → 8 path.

## Practical tradeoffs

- **Support horizon:** .NET 8 is still supported on October 16, 2026, but
  support ends November 10, 2026. A migration can be a necessary intermediate
  step, but a production rollout that cannot move again before that date has
  very little remaining support runway. Do not confuse “LTS” with “currently
  supported indefinitely.”
- **Patch discipline:** the support policy requires the latest patch. Validate
  runtime, ASP.NET Core, EF Core, OS/container base image, and deployment
  updates as a coherent servicing plan.
- **API and behavior compatibility:** major runtime/framework upgrades can
  change source compatibility and behavior. Run tests against real databases,
  proxy/load-balancer paths, containers, trimming/AOT, and supported operating
  systems as applicable.
- **Product scope:** ASP.NET Core and EF Core have their own upgrade notes.
  The .NET target-framework change alone does not upgrade every package or
  validate provider-specific SQL.
- **Language modernization:** adopting C# 12 syntax is optional. Keep `LangVersion`
  at the target-framework default unless a deliberate, supported compatibility
  decision requires otherwise.
- **Performance claims:** Dynamic PGO and other JIT improvements may benefit
  workloads differently. Benchmark representative workloads under production-
  like settings rather than relying on headline numbers.

## Learning sequence and examples

The recommended historical comparison is .NET 6 → .NET 7 → .NET 8, even when
the organizational goal was to move directly to the .NET 8 LTS. Inspect the
release-specific changes at each step so that changes are not misattributed
to .NET 8. Then choose the current supported target for the actual production
destination.

This milestone is documentation-only. It does not add runnable projects or
claim that sample code, builds, tests, or migrations were verified. At the
time of inspection, the machine has no .NET 8 SDK; it does have .NET 8 runtime
8.0.18 (including ASP.NET Core and Windows Desktop runtimes), .NET 9 SDKs, and
a .NET 10 preview SDK. A runtime installation cannot compile a `net8.0`
project. No .NET 8 build or executable validation was performed.

## Official references

- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) — release date, LTS classification, latest patch, support phase, and end of support.
- [.NET 8 release announcement](https://devblogs.microsoft.com/dotnet/announcing-dotnet-8/) — launch context and platform highlights.
- [.NET 8 what's new overview](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview)
- [C# 12 what's new](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12) and [language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [ASP.NET Core 8 announcement](https://devblogs.microsoft.com/dotnet/announcing-asp-net-core-in-dotnet-8/), [what's new](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0), and [breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/8/overview)
- [EF Core 8 announcement](https://devblogs.microsoft.com/dotnet/announcing-ef8/), [what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew), and [breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes)
- [.NET SDK/runtime releases and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support)
