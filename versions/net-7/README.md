# .NET 7 — the STS step between .NET 6 and .NET 8

- **Released:** November 8, 2022
- **Release type:** Standard Term Support (STS)
- **End of support:** May 14, 2024
- **Status on October 16, 2026:** Unsupported. .NET 7 no longer receives Microsoft security or quality updates. Do not use it as a production target.
- **Target framework:** `net7.0`
- **SDK at launch:** .NET SDK 7.0.100 (the 7.0.1xx feature band); install a suitable 7.0 SDK to build `net7.0` projects.
- **Default C#:** C# 11 when using the .NET 7 SDK and targeting `net7.0`.
- **Related product trains:** ASP.NET Core 7 and EF Core 7, each with its own release notes, APIs, and migration considerations.

## Why this milestone matters

.NET 7 is the short-support release between .NET 6 LTS and .NET 8 LTS. Its platform work concentrated on performance, cross-platform unification, developer productivity, and cloud-native application development. This is a useful historical compatibility checkpoint: an upgrade from .NET 6 to .NET 8 crosses both the .NET 7 and .NET 8 change sets, even if the application never ships on .NET 7.

Do not conflate the related versions. The runtime executes applications; the SDK supplies the compiler, MSBuild, and CLI; `net7.0` is the target framework moniker; C# 11 is the language version normally selected by the .NET 7 SDK for that target. ASP.NET Core and EF Core have separate histories and their own change catalogs. In particular, EF Core 7 targets .NET 6 and can run on .NET 6 or .NET 7; it is not synonymous with the .NET 7 runtime.

## At a glance

| Question | Answer |
| --- | --- |
| Lifecycle? | STS; released November 8, 2022 and out of support since May 14, 2024. |
| SDK and TFM? | .NET 7 SDK train (7.0.x; launch SDK 7.0.100) and `net7.0`. SDK and runtime versions are related but not interchangeable. |
| C# version? | C# 11 by default for a `net7.0` project built with the .NET 7 SDK. The compiler/TFM defaults, not the runtime alone, determine the language version. |
| Platform scope? | .NET 7 is a cross-platform .NET release. Actual OS, architecture, and workload support depends on the supported-OS policy and product (for example, the .NET runtime versus .NET MAUI). Check the exact platform/version matrix before deployment. |
| ASP.NET Core? | ASP.NET Core 7 shipped as its own web-framework generation. Its web-specific features, breaking changes, and migration instructions are documented separately. |
| EF Core? | EF Core 7 (EF7) was a separately released ORM generation. It targets .NET 6 and runs on .NET 6 or .NET 7; provider capabilities and compatibility vary. |
| Safe destination today? | No. .NET 7 is unsupported. .NET 8 is also near end of support on this snapshot date (November 10, 2026), so for a new or long-lived deployment assess a currently supported LTS such as .NET 10. |

## Historical place in the .NET 6 → 7 → 8 path

| Release | Lifecycle | Useful context |
| --- | --- | --- |
| .NET 6 | LTS; released November 8, 2021; support ended November 12, 2024 | Introduced C# 10, minimal APIs, and a broad performance and library update. It was the previous LTS baseline, but is now unsupported. |
| .NET 7 | STS; released November 8, 2022; support ended May 14, 2024 | C# 11, runtime/performance work, generic math, JSON contract customization, Native AOT preview, SDK container publishing, and new ASP.NET Core and EF Core capabilities. |
| .NET 8 | LTS; released November 14, 2023; support ends November 10, 2026 | C# 12 and another major runtime, SDK, web and data release. It is still supported on October 16, 2026, but its remaining support window is short. |

The transition isn't just a TFM edit. Review the .NET 7 delta and then the .NET 8 delta, including the ASP.NET Core and EF Core catalogs when those products are used. In EF Core's case, EF7 can be run on .NET 6, so upgrading the base TFM does not by itself require adopting EF7.

## Platform and runtime notes

.NET 7 continued the unified, cross-platform .NET platform, with the runtime and libraries evolving across desktop, server, cloud, mobile, and WebAssembly scenarios. Platform support is specific to operating-system releases, architecture, and workload; it is not a promise that every feature is available identically everywhere. Use Microsoft's supported-OS policy for the exact OS version and product you deploy.

Runtime highlights include on-stack replacement (OSR), expanded profile-guided optimization, Arm64 code-generation improvements, and performance work in Mono, which is used by Blazor WebAssembly and mobile workloads. Native AOT arrived in .NET 7 as a focused preview for trimmed console applications: it produces a native executable without IL or a JIT, but carries trimming and compatibility constraints. Treat AOT as an explicit deployment choice, not an automatic migration step.

Notable library updates include generic math interfaces, `System.Text.Json` contract customization and polymorphic serialization, a non-backtracking regular-expression engine and regex source generator, `TimeSpan`/date-time microsecond and nanosecond APIs, `System.Formats.Tar`, stream `ReadExactly`/`ReadAtLeast`, rate-limiting primitives, Unix file-mode APIs, and P/Invoke source generation. These additions can improve performance or expressiveness, but assess their platform availability, semantics, and serialization contracts before adopting them.

## Sources

- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) — release date, STS designation, latest historical patch, and end-of-support date.
- [.NET 7 release overview](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7) — platform, runtime, libraries, and SDK features.
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support) — SDK feature bands, servicing, and runtime behavior.
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning) — target-framework default language mapping and guidance.
- [.NET supported OS policy](https://github.com/dotnet/core/blob/main/os-lifecycle-policy.md) — current OS support details.
- [ASP.NET Core 7 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-7.0) and [EF Core 7 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew) — distinct product histories.

See [what's new](whats-new.md), [breaking changes](breaking-changes.md), and the historical [migration guide](migration-guide.md) for more detail.
