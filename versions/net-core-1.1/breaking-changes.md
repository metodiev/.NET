# .NET Core 1.1 compatibility and breaking-change notes

This is a bounded historical guide, not a complete breaking-change
catalogue. The archived .NET Core 1.1 release notes and announcement
document the release, additions, and known issues, but do not provide a
comprehensive compatibility catalogue for every runtime API, SDK, package,
operating system, ASP.NET Core component, and EF Core provider.
Consequently, this page does not claim that the 1.0-to-1.1 upgrade had no
behavioral changes.

## .NET Core 1.0 to 1.1

- **Target and package versions changed.** Microsoft's contemporaneous
  instructions said to change `netcoreapp1.0` to `netcoreapp1.1` and update
  the `Microsoft.NETCore.App` package from 1.0.x to 1.1.0 in a
  `project.json` application. Review the whole dependency graph; changing
  only the target framework is not proof that packages support the new
  target.
- **Templates could follow the newest installed runtime.** The 1.1
  announcement warned that `dotnet new` selected the latest runtime on the
  machine, which could cause a newly generated project to target 1.1 even
  when the developer intended 1.0. Check the generated target and package
  versions explicitly when reproducing this workflow.
- **SDK and project format were moving independently.** The initial 1.1
  instructions used `project.json`; SDK Preview 3 also provided
  MSBuild-style `.csproj` tooling. Do not treat a project-format change as a
  runtime breaking change, or assume all 1.1 projects use the same format.
- **.NET Standard library projects did not need an automatic version bump.**
  Microsoft's announcement said there was no need to update those projects
  solely to use .NET Core 1.1. Check library APIs and package requirements
  independently.
- **API additions are not compatibility proof.** The announced 1,380 API
  additions do not guarantee that a particular app's behavior, dependencies,
  deployment, or supported operating systems are unchanged.

## Keep associated product changes separate

ASP.NET Core 1.1 and EF Core 1.1 had their own release notes, APIs, packages,
and migration concerns. A .NET Core target update alone does not migrate
ASP.NET middleware/configuration or establish EF provider compatibility.
Use the [ASP.NET Core 1.1 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-1.1)
and [EF Core 1.1 what's-new page](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-1.1)
for those product boundaries.

## Present-day compatibility warning

.NET Core 1.1 reached end of support on June 27, 2019. No runtime fixes or
security servicing should be expected. It is not a supported production
target. For a current application, use Microsoft's current
[breaking-change and porting guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/breaking-changes)
and test against a currently supported .NET release.

## References

- [.NET Core 1.1 release announcement and upgrade instructions](https://devblogs.microsoft.com/dotnet/announcing-net-core-1-1/)
- [.NET Core 1.1 release notes and known issues](https://github.com/dotnet/core/blob/main/release-notes/1.1/1.1.md)
- [.NET Core 1.1 end-of-life notice](https://devblogs.microsoft.com/dotnet/net-core-1-0-and-1-1-will-reach-end-of-life-on-june-27-2019/)
- [General .NET porting and breaking-change guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/breaking-changes)
