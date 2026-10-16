# .NET 10 — the current LTS platform

**Status snapshot: October 16, 2026.** .NET 10 is a stable, generally
available LTS release, released November 11, 2025. Microsoft lists .NET 10
support through **November 14, 2028**. On the snapshot date its latest
servicing runtime is 10.0.13 (October 13, 2026). Supported deployments must
stay current on servicing updates.

This is a research milestone, not a claim that this workspace builds or runs
stable .NET 10. The available local SDK is a .NET 10 preview SDK. No executable
feature samples or validation results are presented as stable .NET 10 evidence.

## At a glance

| Topic | .NET 10 milestone |
| --- | --- |
| Release | Stable / GA, November 11, 2025 |
| Lifecycle | LTS; end of support November 14, 2028 |
| Current runtime patch on snapshot date | 10.0.13, released October 13, 2026 |
| SDK / target framework | .NET SDK 10.0 train; initial GA SDK was 10.0.100; target framework moniker is `net10.0` |
| Default C# language version | C# 14 for .NET 10 targets |
| Web framework | ASP.NET Core 10.0; covered by the .NET support policy through November 14, 2028 |
| Data-access framework | EF Core 10.0 (EF10), an LTS release; its stated support end is November 10, 2028 |
| Stable .NET 10 validation here | Not performed: only a preview SDK is available |

The SDK, runtime, compiler, ASP.NET Core, and EF Core are related but distinct
components. An SDK is a build toolchain and can contain a runtime; the
`net10.0` target selects a framework/API contract. Neither a preview SDK nor an
SDK's version proves stable runtime validation. The 10.0.13 value above is the
runtime patch from the dated support policy, not an assertion that the SDK
patch number is also 10.0.13.

## Why .NET 10 matters

.NET 10 continues the yearly .NET release cadence as an LTS platform. Its
support window makes it the forward-looking production choice over .NET 8
and .NET 9, whose support ends on the same date, November 10, 2026. LTS and
STS describe duration of support, not different quality levels. The platform
release brings coordinated runtime, libraries, SDK, and tooling changes; the
companion languages and frameworks have their own versioning and compatibility
decisions.

The [.NET 10 what's-new overview](whats-new.md) describes practical features
across the runtime, base libraries, SDK, C# 14, ASP.NET Core 10, and EF Core
10. It deliberately describes examples in prose rather than offering
unverified executable samples.

## Release-train boundaries

- **.NET runtime and SDK:** .NET 10 shipped November 11, 2025. The initial
  release included SDK 10.0.100 and the 10.0 runtime. Patch cadence and SDK
  feature-band numbering are distinct; pin the required SDK in `global.json`
  only after confirming it is installed and supported by your IDE and CI.
- **C# 14:** the compiler selects C# 14 by default for a `net10.0` target.
  C# is not the runtime: the compiler/toolset and TFM determine language
  availability. Microsoft's language-versioning guidance says C# 14 is
  supported only on .NET 10 and newer. Avoid forcing `LangVersion` above the
  target's supported default.
- **ASP.NET Core 10:** the 10.0 web framework release is documented as part
  of .NET 10. Its feature documentation and breaking-change catalog are
  separate from the platform's general .NET catalog.
- **EF Core 10:** EF10 is a distinct ORM/provider release, requires the .NET
  10 SDK to build and .NET 10 runtime to run, and has its own LTS period
  (through November 10, 2028). Do not infer EF's date or database-provider
  compatibility solely from the .NET runtime date.

ASP.NET Core and EF Core have had their own versioned release trains across
the .NET generations. The matching major number identifies a release
generation—not identical patch numbers, release histories, APIs, or lifecycle
dates. For this upgrade era the product sequence is ASP.NET Core 8.0, 9.0,
10.0 and EF Core 8.0, 9.0, 10.0, corresponding to the .NET 8, 9, and 10
generations. ASP.NET Core's support is covered by the .NET platform policy;
EF Core publishes release-specific lifecycle information. In particular,
EF Core 10's documented support end precedes the .NET 10/ASP.NET Core 10
platform-policy end by four days.

| Product history | Official release documentation |
| --- | --- |
| ASP.NET Core 8 → 9 → 10 | [8.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0?view=aspnetcore-8.0) · [9.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0?view=aspnetcore-9.0) · [10.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0) |
| EF Core 8 → 9 → 10 | [8.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew) · [9.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew) · [10.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew) |

## Upgrade context: .NET 8 → 9 → 10

The learning path is **.NET 8 (LTS) → .NET 9 (STS) → .NET 10 (LTS)**, with
each transition reviewed against its own release notes and compatibility
catalog. .NET 9 is an important compatibility waypoint: its C# 13 and platform
changes sit between .NET 8/C# 12 and .NET 10/C# 14. An organization does not
need to establish a prolonged production landing on .NET 9 to get to .NET 10;
the target should be .NET 10 when the application and dependencies are ready.

On October 16, 2026, both .NET 8 and .NET 9 are still supported but reach end
of support on **November 10, 2026**. Plan the production move promptly, and
do not confuse the runtime migration with independent ASP.NET Core, EF Core,
database-provider, deployment-image, or hosting changes. The
[migration guide](migration-guide.md) lays out a controlled sequence and links
to the official migration material.

## Compatibility, production, and operational implications

Major .NET releases can include source, binary, and behavioral changes. Review
the full [.NET 10 compatibility catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10),
the [ASP.NET Core 10 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/overview?view=aspnetcore-10.0),
and [EF Core 10 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes)
for every affected application. The selected
[breaking-change notes](breaking-changes.md) are an impact-oriented checklist,
not an exhaustive replacement for those catalogs.

Production readiness includes more than a successful compile: validate
authentication and authorization behavior, database-generated migrations,
SQL/query plans, container OS and native dependencies, observability,
trimming/AOT if used, and rollback/roll-forward plans. Review and test the
latest security and servicing patch before deployment. The .NET policy
requires supported installations to remain current on patch updates.

## Curriculum links

- [What's new in .NET 10](whats-new.md)
- [Breaking-change review checklist](breaking-changes.md)
- [Migration guide](migration-guide.md)

## Official sources

- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) — release dates, patch status, LTS/STS, end of support; policy updated October 13, 2026.
- [.NET 10.0.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/10.0/10.0.0/10.0.0.md) — GA date and initial SDK/runtime release.
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support) — servicing, feature bands, and runtime/SDK distinctions.
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning) — target defaults and supported language versions.
- [What's new in ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0)
- [What's new in EF Core 10](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
