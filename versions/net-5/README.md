# .NET 5 — the first unified .NET release

- **Released:** November 10, 2020
- **Release type:** Standard Term Support (STS); supported for two years.
- **End of support:** May 10, 2022. .NET 5 is unsupported and no longer
  receives security or quality updates.
- **SDK / target framework:** .NET SDK 5.0.100 at launch; applications target
  `net5.0`.
- **Default C#:** C# 9.0 for .NET 5 targets, unless a project overrides the
  compiler setting.
- **Related product lines:** ASP.NET Core 5 and EF Core 5 shipped as separately
  versioned products with their own release notes, packages, and migration
  guidance.

## Why this milestone matters

.NET 5 was the first release in the unified .NET product line after .NET Core
3.1. The version number intentionally skipped 4 to avoid confusion with
.NET Framework 4.x. It continued the .NET Core, open-source, cross-platform
runtime and SDK, while bringing together the implementation and tools that
had been developed in the .NET Core line. This was a naming and platform
unification milestone, not a claim that .NET 5 was simply a new version of
.NET Framework or that every old .NET Framework application could be moved
without changes.

The most important learning distinction is between the unified platform
release and its companion products. The .NET 5 runtime, SDK, and base
libraries were released together; ASP.NET Core 5 has web-framework features
and a separate 3.1-to-5.0 migration guide; EF Core 5 has its own package and
ORM feature history. C# 9 is a compiler/language version associated with the
`net5.0` target, not a capability switched on by the runtime alone.

## At a glance

| Question | Answer |
| --- | --- |
| Release and lifecycle | November 10, 2020; STS; support ended May 10, 2022. It is unsupported today. |
| SDK / application TFM | SDK 5.0.100 at launch; `net5.0` for applications. SDK servicing and runtime patches have their own version numbers. |
| Language | C# 9.0 is the default for .NET 5 targets when not overridden. |
| Platform and runtime | Cross-platform unified .NET built on the .NET Core lineage, with runtime, library, and performance improvements. OS and architecture support is version-specific; consult the historical support matrix rather than inferring it from “cross-platform.” |
| Libraries and tooling | Includes base-library updates, `System.Text.Json` additions, source-generator support in the compiler/tooling ecosystem, and SDK improvements. |
| Desktop UI | Windows Forms and WPF are supported on Windows; unification did not make them cross-platform. |
| ASP.NET Core | ASP.NET Core 5 is the related web-framework release, documented and migrated separately from the runtime. |
| EF Core | EF Core 5 is the related ORM release, distributed through NuGet and documented separately. EF Core is not part of the ASP.NET Core shared framework. |
| Examples / validation | This milestone supplies no runnable sample. No historical SDK/runtime build or execution is claimed here. |

## The path from .NET Core 3.1

The relevant platform upgrade path is **.NET Core 3.1 → .NET 5**. It is a
direct successor relationship in the .NET product line, not a requirement to
install or deploy every historical release between them. A `netcoreapp3.1`
application could move to `net5.0`, but the destination changed the target
framework identity, compiler default, SDK, and runtime. Applications still
needed to review dependency compatibility, API behavior, deployment settings,
and the separate ASP.NET Core and EF Core migration notes.

The transition also did not mean .NET Framework disappeared. .NET Framework
4.x remains a distinct product line with Windows-specific technologies and
its own lifecycle. A migration from .NET Framework to modern .NET is a
porting project whose effort depends on APIs, libraries, and app model; it is
not made automatic by the .NET 5 version number.

| Milestone | What to remember |
| --- | --- |
| .NET Core 3.1 (December 3, 2019) | Previous LTS baseline; target was `netcoreapp3.1`; default C# 8.0. |
| .NET 5 (November 10, 2020) | Unified .NET naming and successor platform; target `net5.0`; default C# 9.0; STS. |
| .NET 6 (November 8, 2021) | Next platform release and LTS milestone; separate migration and feature history. |

Both .NET Core 3.1 and .NET 5 are now out of support. See the repository's
[support lifecycle snapshot](../support-lifecycle.md), and verify Microsoft's
live policy before making a present-day support decision.

## Product boundaries

| Platform generation | ASP.NET Core line | EF Core line |
| --- | --- | --- |
| .NET Core 3.1 | ASP.NET Core 3.1 | EF Core 3.1 |
| .NET 5 | ASP.NET Core 5.0 | EF Core 5.0 |

The aligned version numbers signal the companion product generation, not one
shared release artifact or identical support terms. ASP.NET Core is the web
framework and has its own release notes and migration documentation. EF Core
is an independently packaged ORM; applications choose provider and tooling
packages explicitly. Use each product's official documentation for its
features and compatibility changes.

## Suggested learning path

1. Read [What's new](whats-new.md) for the platform and its separately
   versioned ASP.NET Core 5 and EF Core 5 companions.
2. Review [breaking changes](breaking-changes.md) before interpreting or
   porting historical code.
3. Use the [migration guide](migration-guide.md) as a historical
   .NET Core 3.1-to-.NET 5 checklist, not as a recommendation to deploy .NET 5.
4. For actual work, select a currently supported .NET release and follow its
   current official porting and migration guidance.

## Official references

### .NET platform and C#

- [.NET 5 release notes: .NET 5.0.0 and SDK 5.0.100](https://github.com/dotnet/core/blob/main/release-notes/5.0/5.0.0/5.0.0.md)
- [Announcing .NET 5.0](https://devblogs.microsoft.com/dotnet/announcing-net-5-0/)
- [What's new in .NET 5](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- [.NET 5 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/5.0)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support)

### ASP.NET Core 5

- [What's new in ASP.NET Core in .NET 5](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-5.0)
- [Migrate from ASP.NET Core 3.1 to .NET 5](https://learn.microsoft.com/en-us/aspnet/core/migration/31-to-50)

### EF Core 5

- [What's new in EF Core 5.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew)
- [Breaking changes in EF Core 5.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/breaking-changes)
