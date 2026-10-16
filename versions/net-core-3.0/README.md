# .NET Core 3.0 — the major STS step before 3.1 LTS

- **Released:** September 23, 2019.
- **Release type:** STS (called the “Current” release in contemporary
  materials); support ended March 3, 2020.
- **SDK / runtime at launch:** .NET Core SDK 3.0.100 included the .NET Core
  Runtime 3.0.0. Visual Studio 2019 16.3 GA was required to use the release's
  full feature set.
- **Application target:** `netcoreapp3.0`.
- **Default language:** C# 8.0 for .NET Core 3.x targets when not overridden;
  this is a compiler/target-framework default, not runtime syntax.
- **Related products:** ASP.NET Core 3.0 and EF Core 3.0 were separate product
  releases with their own packages, release documentation, and migration
  concerns.

> **Historical status:** .NET Core 3.0 is out of support and receives no
> security or quality updates. This milestone is for historical learning;
> it is not a supported deployment target or a recommendation to install it
> for production.

## Why this milestone matters

.NET Core 3.0 was a substantial platform release between the .NET Core 2.x
line and the smaller, long-term-support 3.1 refinement. It brought Windows
Forms and WPF to .NET Core on Windows, implemented .NET Standard 2.1, enabled
tiered compilation by default, and introduced broad runtime, library, and
SDK changes. It also arrived with C# 8.0 as the default language version for
`netcoreapp3.0` projects. The release is a useful case study in why a platform
upgrade is not just a target-framework edit: deployment behavior, Windows
Desktop project setup, web hosting, package boundaries, language/compiler
defaults, and ORM query behavior all need to be understood separately.

## At a glance

| Question | Answer |
| --- | --- |
| Lifecycle | Released September 23, 2019; STS/current; support ended March 3, 2020. Unsupported today. |
| SDK, runtime, and TFM | SDK 3.0.100 included runtime 3.0.0; application TFM `netcoreapp3.0`. SDK servicing versions and runtime patches are distinct version numbers. |
| C# | C# 8.0 default for .NET Core 3.x, unless overridden. Nullable reference types were introduced but are opt-in; the runtime does not add C# syntax. |
| Platform | .NET Standard 2.1 implementation, runtime and library evolution, and new deployment/tooling options. |
| Desktop | WPF and Windows Forms became available on .NET Core for Windows only; this did not make either UI stack cross-platform. |
| ASP.NET Core | ASP.NET Core 3.0 introduced a major web framework and hosting transition; consult its own release and migration docs. |
| EF Core | EF Core 3.0 is a separately versioned ORM. Its query translation and package/tooling changes are not runtime changes. |
| Verification | This documentation does not provide a runnable 3.0 project and makes no claim that an application was built or tested with the historical SDK. |

## Place in the 2.1 → 2.2 → 3.0 → 3.1 sequence

The sequence helps distinguish an LTS baseline, interim releases, and the
next LTS release. It is historical context, not a requirement to deploy each
unsupported intermediate version.

| Platform | Lifecycle | Historical role |
| --- | --- | --- |
| .NET Core 2.1 | LTS; support ended August 21, 2021 | The prior LTS baseline. |
| .NET Core 2.2 | STS/current; support ended December 23, 2019 | Interim runtime and tooling evolution, including diagnostics/events, Windows ARM32, and startup hooks. |
| .NET Core 3.0 | STS/current; support ended March 3, 2020 | Major platform boundary: Windows Desktop, .NET Standard 2.1, C# 8 defaults, and extensive web/ORM transitions in separate products. |
| .NET Core 3.1 | LTS; support ended December 13, 2022 | Serviced/refined 3.0 and supplied the next LTS window; it did not make 3.0 itself LTS. |

ASP.NET Core and EF Core had corresponding 2.1, 2.2, 3.0, and 3.1 product
generations, but their version labels do not mean identical release dates,
package contents, APIs, or support lifecycles. Starting with 3.0, EF Core
packages and providers were no longer included in the ASP.NET Core shared
framework. The lifecycle dates above describe the .NET Core platform only;
this milestone does not infer separate ASP.NET Core or EF Core end-of-support
dates from the platform date. Check each product's own documentation for its
support terms.

## Windows Desktop boundary

Windows Forms and WPF were newly supported on .NET Core in 3.0, but both
remain Windows desktop technologies. The Windows Desktop SDK and Windows
targeting are distinct from the cross-platform .NET Core runtime. A project
targeting a desktop workload should not be described or treated as a
cross-platform UI application. Porting from .NET Framework can also expose
API, designer, native dependency, and platform-assumption differences; use
the Windows Desktop migration guidance and test the actual application.

## Study path

1. Read [what changed](whats-new.md) for platform/runtime evolution and the
   separate ASP.NET Core and EF Core release histories.
2. Review [breaking changes](breaking-changes.md), particularly EF Core query
   translation and ASP.NET Core hosting/package changes.
3. Use the [migration guide](migration-guide.md) as a historical 2.2-to-3.0
   checklist, not as current upgrade or deployment advice.
4. Continue to [.NET Core 3.1](../net-core-3.1/README.md) to understand the
   next LTS refinement, then consult Microsoft's live support policy for
   present-day work.

## Official sources

### .NET Core, lifecycle, and C#

- [.NET Core 3.0 release notes (September 23, 2019; SDK 3.0.100)](https://github.com/dotnet/core/blob/main/release-notes/3.0/3.0.0/3.0.0.md)
- [What's new in .NET Core 3.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [C# language versioning and target-framework defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [C# 8.0 what's new](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8)

### ASP.NET Core

- [What's new in ASP.NET Core 3.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-3.0)
- [Migrate from ASP.NET Core 2.2 to 3.0](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)

### EF Core

- [New features in EF Core 3.x (including the 3.0 features)](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/)
- [Breaking changes in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes)
- [EF Core 3.0 announcement](https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-3-0/)
