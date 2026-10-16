# Historical migration guide: .NET Core 2.1 to 3.1

> **Status warning:** .NET Core 3.1 support ended December 13, 2022. This
> checklist is for understanding the historical upgrade, not for selecting a
> production target. Do not deploy a new or continuing production application
> on .NET Core 3.1. For real upgrades, choose a currently supported target and
> use Microsoft's current porting guidance.

## Understand the path first

.NET Core 2.1 was LTS; 2.2 and 3.0 were intermediate short-term releases; 3.1
was the next LTS release. The transition includes the major .NET Core 3.0
changes, so it must not be treated as a simple 2.1-to-3.1 patch update:

| Step | Platform TFM | What to account for |
| --- | --- | --- |
| Starting point | `netcoreapp2.1` | The previous LTS platform. Inventory dependencies, APIs, and deployment assumptions. |
| Intermediate history | `netcoreapp2.2` | Review runtime diagnostics and other 2.2 changes, plus the separate ASP.NET Core/EF Core 2.2 release notes. This release is also unsupported. |
| Major compatibility boundary | `netcoreapp3.0` | Review the substantial runtime, SDK, project, ASP.NET Core, and EF Core changes, including the breaking changes in the next section. 3.0 is unsupported. |
| Historical destination | `netcoreapp3.1` | Use an appropriate 3.1 SDK/package set; account for 3.1 fixes, SameSite behavior, and Windows Forms/macOS appHost changes. 3.1 is unsupported. |

An application can upgrade directly to the later TFM rather than deploying
each unsupported intermediate release. The intermediate milestones still
matter for locating breaking changes. For a production migration, target a
currently supported release (the support policy snapshot in this repository
identifies .NET 10 LTS as current as of October 16, 2026) and follow its
official guidance instead.

## Historical upgrade checklist

### 1. Record the original application

Before editing, record:

- `TargetFramework`, SDK version, any `global.json`, and any explicit
  `LangVersion`.
- NuGet package and native dependency versions, especially ASP.NET Core and
  EF Core providers.
- Web host startup, middleware order, endpoint/routing configuration,
  authentication and cookies.
- EF Core LINQ queries, migrations, generated SQL, provider behavior, and
  `dotnet ef` usage.
- Runtime identifiers, Docker base images, publish options, appHost/script
  assumptions, and operating-system-specific features.

This inventory is a preparation step; it does not imply that this repository
has built or run a historical application.

### 2. Select the matching historical SDK and TFM

The initial .NET Core 3.1 release shipped SDK 3.1.100. Microsoft documented
Visual Studio 2019 16.4 as the required Visual Studio version for full 3.1
support. Update a pinned `global.json` only if that SDK is installed, and
change an application TFM from `netcoreapp2.1` to `netcoreapp3.1` for the
historical exercise. SDK versions are serviced independently; use the exact
version required by the exercise rather than assuming the TFM determines
every tool version.

The default language version for `netcoreapp3.x` is C# 8.0, unless the project
overrides it. This is a compiler/target-framework choice, not a runtime
feature.

### 3. Update framework and package references

For the ASP.NET Core 3.0 transition, inspect the migration guide for obsolete
package references and the shared framework model. Web SDK projects
implicitly reference `Microsoft.AspNetCore.App`; the 3.0-era project format
removed many old package references and changed the integration approach for
non-Web SDK projects. ASP.NET Core 3.1 package references should use compatible
3.1 servicing versions.

EF Core is no longer supplied by the ASP.NET Core shared framework from 3.0.
Add the specific EF Core provider/runtime packages your application needs.
EF Core 3.1's packages target .NET Standard 2.0, whereas EF Core 3.0 targeted
.NET Standard 2.1. Install `dotnet-ef` as a local or global tool at a
compatible version; do not expect it to be bundled in the SDK.

### 4. Audit 3.0 and 3.1 behavior changes

- Rewrite untranslatable EF Core query expressions or explicitly materialize
  a suitably filtered result before client-side processing. EF Core 3.x
  throws when non-projection expressions cannot be translated.
- Review queries that previously generated multiple SQL statements and
  validate the single-statement behavior introduced in EF Core 3.x.
- Review ASP.NET Core hosting, package references, routing, and middleware
  using the official 2.2-to-3.0 migration checklist.
- Test SameSite cookies, especially cross-site sign-in flows. ASP.NET Core
  3.1 changed `SameSiteMode.None` and introduced `Unspecified` behavior.
- If using Windows Forms, replace removed legacy controls and APIs with
  supported controls.
- If using macOS SDK 3.1, verify whether the disabled-by-default appHost is
  expected by scripts or end users; set `UseAppHost` where necessary.
- If using Blazor, separate the stable ASP.NET Core 3.1 features from the
  then-preview Blazor WebAssembly templates.

See [breaking changes](breaking-changes.md) for a compact issue list and the
official catalogs for exhaustive details.

### 5. Validate in a deliberately isolated environment

For a historical reproduction, use a disposable, isolated environment with
archived SDKs, runtimes, and package feeds as appropriate. Do not install
unsupported components on a production host. If you run a legacy workload,
record the actual SDK/runtime versions, test commands, results, target OS,
and any environment constraints.

No historical .NET Core 3.1 project is supplied or validated by this
curriculum milestone. This guide makes no claim that a sample builds, runs,
or passes tests against .NET Core 3.1.

## Production migration today

Do not choose .NET Core 3.1, 2.1, 2.2, 3.0, .NET 5, or .NET 6 as a production
destination: each is out of support. Check Microsoft's live [support
policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core),
select a currently supported release, then use the official [.NET porting
guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/) and the
target release's breaking-change documents. At the repository's
October 16, 2026 lifecycle snapshot, .NET 10 is the current LTS release.

## Next learning steps

Continue the curriculum chronologically through the .NET 5 unification
milestone and .NET 6 LTS. Then compare the historical work with current
supported .NET guidance; do not assume historical upgrade steps alone are
sufficient for a present-day migration.

## Official references

- [.NET Core 3.1 release notes and SDK 3.1.100](https://github.com/dotnet/core/blob/main/release-notes/3.1/3.1.0/3.1.0.md)
- [ASP.NET Core: migrate from 2.2 to 3.0](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)
- [ASP.NET Core: migrate from 3.0 to 3.1](https://learn.microsoft.com/en-us/aspnet/core/migration/30-to-31)
- [ASP.NET Core: .NET Core 3.1 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-3.1)
- [EF Core 3.1 announcement, packaging, and CLI tool](https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-3-1-and-entity-framework-6-4/)
- [EF Core 3.x breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET porting guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/)
