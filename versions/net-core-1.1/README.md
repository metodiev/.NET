# .NET Core 1.1 — the first “Current” release

- **Released:** November 16, 2016
- **End of support:** June 27, 2019
- **Status on October 16, 2026:** Unsupported; do not use for new production
  workloads.

## What “LTS” means for this milestone

Microsoft's November 2016 announcement called .NET Core 1.1 the first
**“Current”** release. In its June 2019 end-of-life notice, Microsoft later
listed .NET Core 1.1 among the **LTS** releases and stated that it shared
1.0's June 27, 2019 end-of-support date. The current support-policy table
retains dates for these out-of-support releases but does not show their
release-type labels. These descriptions are not perfectly consistent across
time, so this milestone preserves both primary-source descriptions instead
of assigning a retrospective STS label or presenting the launch label as the
whole story.

## Version facts

| Item | Historical fact |
| --- | --- |
| Runtime / target | .NET Core 1.1; `netcoreapp1.1` |
| Initial SDK | .NET Core SDK 1.0.0 Preview 2.1; the SDK and runtime had different version numbers. Later servicing paired runtime 1.1.13 with SDK 1.1.14. |
| Project format | The initial 1.1 announcement showed `project.json`; an SDK Preview 3 using MSBuild-style `.csproj` was also available during the transition. Project format is an SDK/tooling concern, not a runtime target name. |
| C# default | Microsoft's current C# defaults table covers .NET Core 2.x and later, not `netcoreapp1.x`. Do not infer a single default C# version for 1.1; it depended on the specific SDK/compiler. |
| .NET Standard | .NET Core 1.1 supported .NET Standard 1.6. .NET Standard 2.0 was not included. |
| Related release trains | ASP.NET Core 1.1 and EF Core 1.1 are separate products with their own APIs, release information, and migration considerations. Their version number does not make them the runtime or establish identical lifecycle terms. |

## Why it matters

This was a follow-on to .NET Core 1.0, not the later .NET Core 2.0 API
expansion. Its notable changes included performance work, a wider set of
supported operating-system distributions, and more than 1,300 additions to
the .NET Core API surface. ASP.NET Core 1.1 added web and hosting features;
EF Core 1.1 added data-access capabilities. Details and caveats are in
[what's new](whats-new.md).

## Compatibility and safe study

Microsoft's 1.0-to-1.1 instructions described updating the
`netcoreapp1.0` target to `netcoreapp1.1` and the `Microsoft.NETCore.App`
package from 1.0.x to 1.1.0 in `project.json`. They also noted that .NET
Standard library projects did not need an update just because the runtime
changed. This is historical guidance, not a present-day deployment path.

The 1.1 runtime and SDK are out of support. Do not deploy them or treat an
old toolchain as a safe way to reproduce a current application. For study,
compare the archived release notes with a currently supported SDK/runtime
(the October 16, 2026 lifecycle lists .NET 10 LTS) and use modern project
files; clearly label any differences from the historical setup. For a
production migration, choose a currently supported destination and assess
the full source-to-destination path rather than stopping on an obsolete
intermediate runtime. See [migration guidance](migration-guide.md) and
[compatibility notes](breaking-changes.md).

No .NET Core 1.1 build, application execution, or compatibility validation is
claimed by these notes.

## Microsoft references

- [.NET Core 1.1 release announcement](https://devblogs.microsoft.com/dotnet/announcing-net-core-1-1/)
- [.NET Core 1.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/1.1/1.1.md)
- [.NET Core 1.1 support and end-of-life notice](https://devblogs.microsoft.com/dotnet/net-core-1-0-and-1-1-will-reach-end-of-life-on-june-27-2019/)
- [Current .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [C# language-versioning defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning#defaults)
- [What's new in ASP.NET Core 1.1](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-1.1)
- [What's new in EF Core 1.1](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-1.1)
