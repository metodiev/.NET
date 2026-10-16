# .NET Core 2.x — broader APIs, performance, and diagnostics

This milestone follows .NET Core 2.0, 2.1, and 2.2 in release order. It is
historical learning material, not a deployment recommendation.

| Release | Released | Lifecycle label | End of support | SDK train / target | Default C# |
| --- | --- | --- | --- | --- | --- |
| .NET Core 2.0 | August 14, 2017 | Current at release; the current lifecycle table does not retroactively label this row LTS or STS | October 1, 2018 | 2.0 / `netcoreapp2.0` | C# 7.3 |
| .NET Core 2.1 | May 30, 2018 | LTS | August 21, 2021 | 2.1 / `netcoreapp2.1` | C# 7.3 |
| .NET Core 2.2 | December 4, 2018 | STS | December 23, 2019 | 2.2 / `netcoreapp2.2` | C# 7.3 |

The release dates and end-of-support dates are from Microsoft's [.NET Core
support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core).
The official [C# language-versioning table](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning#defaults)
assigns C# 7.3 to .NET Core 2.x. This is a compiler default associated with
the target framework, not a language feature introduced by each runtime.
For the 2.0 GA date, this page uses the dated 2.0.0 release notes; the
historical 2.0 release overview contains inconsistent date text.

## Keep the product versions separate

“.NET Core” here means the runtime and base libraries. The SDK is a separate
toolchain (the SDK version may have a different patch or feature-band number);
the SDK installs runtimes but does not make their version numbers identical.
The target framework monikers above select a framework reference/API surface.

ASP.NET Core and Entity Framework Core also had 2.0, 2.1, and 2.2 product
trains, but they are separate products with their own APIs, release notes, and
compatibility concerns. A .NET Core release date or lifecycle label should
not be treated as a complete support statement for every associated product.
See the separate [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
and [EF Core](https://learn.microsoft.com/en-us/ef/core/) documentation.

## Study path

1. Read [what changed across 2.x](whats-new.md), comparing the platform
   improvements chronologically.
2. Review the [compatibility boundaries](breaking-changes.md), especially the
   documented .NET Core 2.1 path-validation behavior change.
3. Use the [migration guide](migration-guide.md) to plan a historical exercise
   without reviving unsupported tooling or targeting an unsupported runtime.

These releases are all out of support. Use the dated
[lifecycle snapshot](../support-lifecycle.md) and Microsoft's live policy
before making a current production decision. For modern work, target a
currently supported .NET release and use its matching SDK, compatibility
guidance, and servicing updates.

## Official references

- [.NET Core 2.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.0/2.0.0.md)
- [.NET Core 2.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.1/2.1.0.md)
- [.NET Core 2.1 release history and LTS status](https://github.com/dotnet/core/blob/main/release-notes/2.1/README.md)
- [.NET Core 2.2 release notes and end of support](https://github.com/dotnet/core/blob/main/release-notes/2.2/README.md)
- [What's new in .NET Core 2.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-2-0)
- [What's new in .NET Core 2.1](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-2-1)
- [What's new in .NET Core 2.2](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-2-2)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
