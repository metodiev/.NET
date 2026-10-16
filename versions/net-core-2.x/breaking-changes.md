# .NET Core 2.x compatibility and breaking-change notes

This page highlights a small number of verified compatibility boundaries. It
is not a complete breaking-change catalog. Before changing a real application,
review the official release notes and compatibility documentation for every
framework, SDK, package, and operating system in its upgrade path.

## .NET Core 1.x to 2.0

- **A larger API surface is not universal compatibility.** .NET Core 2.0
  supports .NET Standard 2.0 and includes a much broader set of APIs, but a
  library that targets .NET Standard or a package that references .NET
  Framework-only APIs may still be incompatible. Check target frameworks,
  dependency support, and actual API usage.
- **Treat project/tooling updates separately.** Select an SDK that can build
  the target framework, and review project and package changes rather than
  assuming that installing the 2.0 SDK upgrades a project or its dependencies.

## .NET Core 2.0 to 2.1

- **Path APIs no longer reject invalid path characters up front.** In .NET
  Core 1.0–2.0, a set of `System.IO` APIs threw `ArgumentException` for
  invalid path characters. Starting in 2.1, those APIs stopped checking for
  them, allowing the operating system to determine the outcome. If validation
  was part of an application's security or input-handling logic, preserve that
  policy explicitly; do not rely on the former exception as validation.
- **The HTTP handler changed.** The 2.1 release notes document
  `SocketsHttpHandler` and an opt-out to the older handler. Test networking
  behavior and handler-specific configuration when moving from 2.0.

See Microsoft's [breaking changes in .NET Core 2.1](https://learn.microsoft.com/en-us/dotnet/core/compatibility/2.1)
for affected APIs and recommended action, and the [.NET Core 2.1 release
notes](https://github.com/dotnet/core/blob/main/release-notes/2.1/2.1.0.md)
for the handler and opt-out details.

## .NET Core 2.1 to 2.2

The official [2.2 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.2/README.md)
and [What's new in .NET Core 2.2](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-2-2)
are the verified starting points for this transition. I did not find a
versioned .NET Core 2.2 compatibility page corresponding to Microsoft's 2.1
page; its absence here is not evidence that no breaking changes or behavioral
differences existed. Review patch notes, dependency notes, and your own
application behavior rather than infer a clean transition.

## Keep sibling products separate

ASP.NET Core and EF Core have their own compatibility boundaries. Do not
apply the .NET Core runtime notes above as if they were a complete web-stack
or data-access migration guide. For a later ASP.NET Core upgrade, Microsoft's
[ASP.NET Core 2.2 to 3.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/22-to-30)
documents changes in that product. EF Core publishes separate release and
breaking-change documentation, such as the [EF Core 3.x breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes).

## Sources

- [.NET Core 2.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.0/2.0.0.md)
- [Breaking changes in .NET Core 2.1](https://learn.microsoft.com/en-us/dotnet/core/compatibility/2.1)
- [.NET Core 2.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.1/2.1.0.md)
- [.NET Core 2.2 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.2/README.md)
- [General .NET porting and breaking-change guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/breaking-changes)
