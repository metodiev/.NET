# .NET Core 1.0 — the cross-platform foundation

**Released:** June 27, 2016
**Release type:** The current support-policy table does not assign an LTS/STS
label to this historical row. Do not retroactively call it LTS.
**Support:** Ended June 27, 2019; unsupported as of October 16, 2026. Do not
deploy it for new production workloads.
**Associated products:** ASP.NET Core 1.0 and EF Core 1.0 launched alongside
the platform, but each is a separate product history.
**C# version:** Do not assign a single language default to the .NET Core 1.0
platform. Early SDK/tooling updates affected the compiler experience. The
current Microsoft language-version table begins its .NET Core defaults at
2.x (C# 7.3) and 3.x (C# 8.0).
**SDK and target:** The original release shipped a .NET Core 1.0 SDK and
`netcoreapp1.0` target. These are historical only; the runnable equivalent in
this repository targets `net9.0` and is not a .NET Core 1.0 runtime
reproduction.

## Why this release matters

.NET Core 1.0 established a cross-platform, open-source, modular .NET platform
for console apps, libraries, web apps, and microservices. Its June 2016 launch
included the runtime, libraries, tools, ASP.NET Core, and EF Core. The
important architectural shift was a platform that could be developed and run
on Windows, macOS, and Linux, alongside—not as an in-place replacement for—
.NET Framework and Xamarin.

This release is an excellent historical starting point for learning the
platform's shape, but it is not a safe production target. The
[modern-equivalent first-app sample](../../examples/platform/net-core-1.0-first-app/README.md)
demonstrates a simple console application using a supported, stable SDK.

## Version facts

| Question | Answer |
| --- | --- |
| Release date and support | June 27, 2016; support ended June 27, 2019. |
| LTS or STS? | Current lifecycle page does not label this historical release LTS or STS. Record it as historical/legacy rather than infer a label. |
| Currently supported? | No. |
| Default C# version? | Not stated as one platform-wide default in the current language-versioning table; it depended on SDK/compiler tooling. |
| SDK and target framework? | Historical SDK 1.0 / `netcoreapp1.0`; don't use it for new production deployments. |
| ASP.NET Core and EF Core? | Both 1.0 release trains launched alongside .NET Core 1.0; their APIs and lifecycle are separate. |
| What can I run today? | The repository's `net9.0` sample, which demonstrates the simple console/CLI workflow and classic `Main` syntax. It does not run the 1.0 runtime. |
| What should I learn next? | .NET Core 1.1, then 2.0 and 2.1 LTS; keep ASP.NET Core, EF Core, and C# timelines distinct. |

## Core changes to understand

- Cross-platform runtime and command-line development across Windows, macOS,
  and Linux.
- A modular platform and package-oriented libraries, instead of assuming one
  monolithic framework installation.
- A command-line workflow (`dotnet new`, `dotnet restore`, `dotnet run`) for
  creating, restoring, and running applications.
- ASP.NET Core as the new cross-platform web framework and EF Core as the
  corresponding next-generation data-access framework; neither should be
  mistaken for the runtime itself.
- .NET Standard as a shared API contract strategy across .NET implementations.
- A project-system/tooling evolution after the initial release. Historical
  `project.json` examples and later SDK-style MSBuild project files are not
  interchangeable; this milestone's runnable project uses modern `.csproj`.

Performance, GC, JIT, AOT, diagnostics, container, and cloud-native feature
comparisons require release-specific sourcing and will be expanded in later
milestones rather than guessed here.

## Compatibility, breaking changes, and migration

There is no previous .NET Core LTS release to compare against. .NET Core was a
new platform line, not a drop-in runtime replacement for .NET Framework.
Applications could encounter API gaps, different hosting and deployment
models, and distinct project tooling. ASP.NET Core and EF Core also represented
separate API and compatibility decisions.

See the [breaking-change notes](breaking-changes.md) for the historical
boundaries and the [migration notes](migration-guide.md) for safe learning
guidance. Neither guide recommends deploying .NET Core 1.0 today.

## Examples and validation

- [First app: historical CLI idea, modern executable project](../../examples/platform/net-core-1.0-first-app/README.md)
- [What's new in .NET Core 1.0](whats-new.md)

The executable sample uses the stable .NET 9 SDK/runtime available in the
workspace. It demonstrates classic C# `Main` code and a modern SDK-style
project, not the original runtime, old SDK, ASP.NET Core 1.0, or EF Core 1.0.

## Official references

- [.NET Core 1.0 announcement](https://devblogs.microsoft.com/dotnet/announcing-net-core-1-0/)
- [.NET Core 1.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/1.0/1.0.0.md)
- [ASP.NET Core 1.0 announcement](https://devblogs.microsoft.com/aspnet/announcing-asp-net-core-1-0/)
- [EF Core 1.0 announcement](https://devblogs.microsoft.com/dotnet/entity-framework-core-1-0-0-available/)
- [Support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
