# What's new in .NET Core 1.0

## Cross-platform .NET platform

.NET Core 1.0 delivered a cross-platform, open-source, modular runtime and
libraries for Windows, macOS, and Linux. The runtime, libraries, and tools
were released together as a platform for console applications, libraries,
web applications, and microservices.

This is a platform-level change, not a C# language feature. Language syntax
and compiler versions followed their own schedule and depended on installed
tooling.

## Command-line first workflow

The launch announcement showed a basic flow:

```text
dotnet new
dotnet restore
dotnet run
```

The CLI made project creation, dependency restoration, and execution accessible
across operating systems. Modern SDKs retain these basic operations, although
templates, project files, and command behavior have evolved. See the
[modern equivalent sample](../../examples/platform/net-core-1.0-first-app/README.md).

## ASP.NET Core and EF Core released as distinct products

ASP.NET Core 1.0 provided a cross-platform web framework. EF Core 1.0 provided
a new data-access stack. Both were released alongside .NET Core, but neither
is the runtime or a guarantee of full API compatibility with its predecessor.
Follow their own release notes and support details.

## What this milestone does not claim

This page is a verified introductory summary, not a complete performance or
API catalog. It does not attribute later features (such as modern .NET
container tooling, Native AOT, or later C# syntax) to .NET Core 1.0.

## Sources

- [.NET Core 1.0 announcement and quick-start commands](https://devblogs.microsoft.com/dotnet/announcing-net-core-1-0/)
- [.NET Core 1.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/1.0/1.0.0.md)
- [ASP.NET Core 1.0 announcement](https://devblogs.microsoft.com/aspnet/announcing-asp-net-core-1-0/)
- [EF Core 1.0 announcement](https://devblogs.microsoft.com/dotnet/entity-framework-core-1-0-0-available/)
