# .NET Core 3.1 — the second LTS release

- **Released:** December 3, 2019
- **Release type:** Long-term support (LTS), originally supported for three
  years.
- **End of support:** December 13, 2022. .NET Core 3.1 is unsupported; it no
  longer receives security or quality updates. Do not use it for new or
  continuing production deployments.
- **SDK / target framework:** .NET Core SDK 3.1.100 / `netcoreapp3.1` at
  launch.
- **Default C#:** C# 8.0 for `netcoreapp3.x` projects. This is a compiler and
  target-framework default, not a feature of the runtime.
- **Associated products:** ASP.NET Core 3.1 and EF Core 3.1 were separate
  products with their own release notes and package histories.

## Why this milestone matters

.NET Core 3.1 was a modest refinement of .NET Core 3.0 and, more importantly,
an LTS release. It retained the major 3.0 platform changes: the first
cross-platform .NET Core support for Windows Forms and WPF (Windows only),
.NET Standard 2.1, C# 8 support, runtime and performance improvements, and a
new default executable app host. The 3.1 release concentrated on fixes and
refinements, especially for Blazor and Windows Desktop, and added C++/CLI
project support in Visual Studio on Windows.

The key historical lesson is to keep the platform, language, SDK, and related
product version numbers distinct. A project targeting `netcoreapp3.1` normally
used C# 8 with an appropriate compiler; the runtime itself did not introduce
C# syntax. ASP.NET Core 3.1 and EF Core 3.1 aligned with this platform release,
but they were not interchangeable components and did not share every
packaging or support detail.

## At a glance

| Question | Answer |
| --- | --- |
| Release date and support | December 3, 2019; LTS, initially supported for three years; support ended December 13, 2022. |
| Supported today? | No. This is a historical learning milestone, not a supported deployment target. |
| Default language? | C# 8.0 for .NET Core 3.x targets, selected by compiler defaults for the target framework. |
| SDK and application TFM? | SDK 3.1.100 at release; applications target `netcoreapp3.1`. SDK/runtime patch versions are serviced separately. |
| Runtime and libraries? | Mostly 3.0's runtime, library, and performance work, refined by 3.1 servicing; .NET Core 3.0 implemented .NET Standard 2.1. |
| ASP.NET Core? | ASP.NET Core 3.1, with a separately documented 3.0-to-3.1 upgrade and release history. |
| EF Core? | EF Core 3.1, distributed as NuGet packages. EF Core 3.1 targeted .NET Standard 2.0, unlike EF Core 3.0's .NET Standard 2.1 target. |
| Repository examples? | No .NET Core 3.1 runnable example or validation is provided by this milestone. Read the history as documentation; do not infer an executed sample. |
| What next? | Study the .NET 5 transition and the .NET 6 LTS line, then use the current support policy when planning any real migration. .NET 5 and .NET 6 are also out of support. |

## Place in the release sequence

The comparison path to 3.1 runs through two significant stages after the
previous LTS, not just a direct point upgrade:

| Platform | Lifecycle | Important context |
| --- | --- | --- |
| .NET Core 2.1 | LTS; support ended August 21, 2021 | The previous LTS baseline. Introduced a broad set of runtime, library, and ASP.NET Core improvements; see the 2.1 release notes for the complete set. |
| .NET Core 2.2 | Short-term release; support ended December 23, 2019 | Added runtime diagnostics and runtime events, Windows ARM32 support, and startup hooks; ASP.NET Core and EF Core also had their own 2.2 releases. |
| .NET Core 3.0 | Short-term release; support ended March 3, 2020 | Major platform step: Windows Desktop, .NET Standard 2.1, C# 8 support, runtime/performance changes, and significant ASP.NET Core and EF Core changes. |
| .NET Core 3.1 | LTS; support ended December 13, 2022 | Refinements to 3.0, LTS servicing, Windows Forms changes, and ASP.NET Core/Blazor improvements. |

All four releases are unsupported now. The detailed changes and compatibility
risks are summarized in [What's new](whats-new.md) and
[breaking changes](breaking-changes.md).

## Keep product histories separate

The release trains below identify the corresponding product generation. They
do not imply identical dates, packages, APIs, or lifecycles.

| .NET Core | ASP.NET Core | EF Core |
| --- | --- | --- |
| 2.1 (May 30, 2018) | 2.1 | 2.1 |
| 2.2 (December 4, 2018) | 2.2 | 2.2 |
| 3.0 (September 23, 2019) | 3.0 | 3.0 |
| 3.1 (December 3, 2019) | 3.1 | 3.1 |

ASP.NET Core 3.1's release history covers web hosting, MVC, Razor components,
Blazor, SignalR, and other web-specific behavior. EF Core 3.1 is an
independently versioned ORM available through NuGet; starting with 3.0, EF
Core and its providers were no longer included in the ASP.NET Core shared
framework. EF Core 3.1 restored .NET Standard 2.0 targeting and polished the
EF Core 3.0 feature set rather than introducing a new major feature set.

## Examples and verification status

There is no .NET Core 3.1 sample in this milestone, and no project has been
built or run against the 3.1 SDK/runtime as part of this documentation. The
version facts and feature summaries below are based on the linked Microsoft
release notes and product migration documentation. A modern project that
resembles an older example would not validate the historical 3.1 runtime.

## Suggested next steps

1. Read [What's new](whats-new.md) to distinguish the 2.1 → 2.2 → 3.0 → 3.1
   progression and the .NET, ASP.NET Core, EF Core, and C# timelines.
2. Review [breaking changes](breaking-changes.md) before interpreting old
   applications or code samples.
3. Use the [migration guide](migration-guide.md) only as a historical
   migration checklist; it explicitly does not endorse deploying 3.1.
4. Continue the curriculum through .NET 5 and .NET 6, then select a currently
   supported release for actual work. Check Microsoft's live policy because
   support status changes over time.

## Official references

### .NET Core and C#

- [.NET Core 3.1 release notes (SDK 3.1.100, December 3, 2019)](https://github.com/dotnet/core/blob/main/release-notes/3.1/3.1.0/3.1.0.md)
- [Announcing .NET Core 3.1](https://devblogs.microsoft.com/dotnet/announcing-net-core-3-1/)
- [What's new in .NET Core 3.1](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1)
- [What's new in .NET Core 3.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0)
- [.NET Core 2.2 announcement](https://devblogs.microsoft.com/dotnet/announcing-net-core-2-2/)
- [.NET Core 2.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.1/2.1.0.md)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)

### ASP.NET Core

- [ASP.NET Core updates in .NET Core 3.1](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-core-3-1/)
- [What's new in ASP.NET Core 3.1](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-3.1)
- [Migrate from ASP.NET Core 3.0 to 3.1](https://learn.microsoft.com/en-us/aspnet/core/migration/30-to-31)
- [Migrate from ASP.NET Core 2.2 to 3.0](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)

### EF Core

- [Announcing EF Core 3.1 and EF 6.4](https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-3-1-and-entity-framework-6-4/)
- [New features in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/)
- [EF Core 2.2 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-2.2)
- [Breaking changes in EF Core 3.x](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes)
