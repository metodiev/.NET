# Studying or migrating from .NET Core 1.1

.NET Core 1.1 is out of support. This guide is for historical study and
planning only; it does not recommend deploying or continuing to operate an
application on the 1.1 runtime.

## Historical .NET Core 1.0-to-1.1 exercise

For a source-level study of the original upgrade:

1. Record the original SDK, project format, target framework, package graph,
   operating systems, and deployment model. Do not assume the SDK version
   equals the runtime version.
2. For a `project.json` app, compare the target
   `netcoreapp1.0` → `netcoreapp1.1` and `Microsoft.NETCore.App` 1.0.x →
   1.1.0 changes from Microsoft's announcement. Verify package compatibility
   and lock down the intended versions; the announcement warned that
   `dotnet new` could select the newest runtime installed.
3. Check whether the project uses the Preview 3 MSBuild-style `.csproj`
   tooling rather than `project.json`. Keep this SDK/project-system change
   distinct from the runtime upgrade.
4. If the application uses ASP.NET Core or EF Core, follow those products'
   own release guidance and test their middleware, configuration,
   providers, and application behavior separately.
5. Compare exact APIs, operating-system support, and deployment behavior
   against the archived release notes. Do not infer compatibility from the
   release's API-addition count.

The historical announcement states that .NET Standard library projects did
not need to be updated solely because an application moved to .NET Core 1.1.
That does not remove the need to check the library's APIs and dependencies.

## Modern production migration

Do not choose .NET Core 1.1 as a new production target or as a stopping point
for a current upgrade. Microsoft's end-of-life notice placed the 1.x upgrade
path through .NET Core 2.1 or 2.2 at that time; both are also unsupported
today. Select a currently supported destination from the live
[.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core),
then assess the complete source-to-destination path, dependencies, operating
systems, and product-specific migrations. As of this milestone's October 16,
2026 snapshot, .NET 10 is the current LTS release; recheck Microsoft's live
policy before making a production decision.

For safe historical learning, compare the 1.1 sources and project structure
with a modern supported SDK project, or use an isolated archival environment
only when examining old tooling is necessary. Do not expose unsupported
runtime installations to production systems, and clearly distinguish a
modern re-expression from an actual 1.1 execution.

## Verification caveat

No historical application has been migrated, built, or run here. These notes
make no runnable-validation or compatibility-test claim.

## Microsoft references

- [.NET Core 1.1 announcement and 1.0 upgrade instructions](https://devblogs.microsoft.com/dotnet/announcing-net-core-1-1/)
- [.NET Core 1.0 and 1.1 end-of-life notice and historical upgrade path](https://devblogs.microsoft.com/dotnet/net-core-1-0-and-1-1-will-reach-end-of-life-on-june-27-2019/)
- [.NET Core 1.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/1.1/1.1.md)
- [ASP.NET Core 1.1 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-1.1)
- [EF Core 1.1 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-1.1)
- [.NET app porting overview](https://learn.microsoft.com/en-us/dotnet/core/porting/)
