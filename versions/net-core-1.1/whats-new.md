# What's new in .NET Core 1.1

.NET Core 1.1 was a focused update to the 1.0 platform. It added APIs,
performance improvements, and operating-system support; it did not introduce
.NET Standard 2.0 or make every .NET Framework API available.

## Runtime, libraries, and platform support

- **More than 1,300 API additions.** Microsoft's release announcement
  counts 1,380 APIs added from 1.0 to 1.1. Many supported the platform and
  tooling; consult the API-diff document for exact members rather than
  treating the number as a measure of app-level functionality.
- **.NET Standard 1.6.** .NET Core 1.1 supported this contract. .NET Standard
  2.0 was explicitly described as future work, not a 1.1 feature.
- **Broader OS coverage.** The release notes added supported distributions
  and operating-system versions, including newer Linux distributions and
  Windows Server 2016. Use the archived supported-OS matrix for a particular
  deployment; “cross-platform” did not mean every distribution or version
  was supported.
- **Performance work.** The announcement described profile-guided
  optimization and reported a 15% improvement for ASP.NET MusicStore in
  Microsoft's lab. That is a workload- and test-specific result, not a
  performance guarantee for arbitrary applications.

## ASP.NET Core 1.1 (separate product)

The ASP.NET Core release added URL-rewriting and response-caching middleware,
view components as Tag Helpers, middleware as MVC filters, a cookie-based
TempData provider, Azure App Service logging and Key Vault configuration
providers, Azure/Redis Data Protection key repositories, WebListener, and
WebSockets support. These are ASP.NET Core features, not .NET Core runtime
features. Review the ASP.NET release notes and migration instructions when
working on an application that uses the web stack.

## EF Core 1.1 (separate product)

The EF Core release added backing-field mapping, SQL Server
memory-optimized-table mapping, additional change-tracking APIs (including
`Reload` and database-value retrieval), explicit loading, `DbSet.Find`,
connection resiliency, and easier service replacement. These are EF Core
capabilities; do not assume parity with Entity Framework 6 or apply their
migration requirements to the runtime alone.

## Tooling and language boundaries

The original announcement used `project.json`, `netcoreapp1.1`, and the
`Microsoft.NETCore.App` 1.1.0 package. SDK Preview 3 also introduced
MSBuild-style `.csproj` tooling during this period, so the exact project
format depends on which SDK/tooling path is being studied. SDK and runtime
version numbers were not interchangeable.

The current official C# defaults table does not assign a default language
version for .NET Core 1.x. The compiler experience was tied to the installed
SDK/compiler; this page therefore does not assert a single C# default.

## Sources

- [.NET Core 1.1 announcement](https://devblogs.microsoft.com/dotnet/announcing-net-core-1-1/) — API count, .NET Standard 1.6, performance, SDK and target details.
- [.NET Core 1.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/1.1/1.1.md) — OS support and API-diff links.
- [ASP.NET Core 1.1 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-1.1)
- [EF Core 1.1 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-1.1)
- [C# language-versioning defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning#defaults)
