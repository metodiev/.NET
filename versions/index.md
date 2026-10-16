# Version index and compatibility matrix

This is the curriculum's high-level map. Lifecycle dates are verified against
the [support snapshot](support-lifecycle.md); product-version associations
should be checked in the linked release notes before relying on them. "Paired
train" describes the corresponding product generation, not identical release
dates, patches, or support terms.

**Current stable baseline (October 16, 2026): .NET 10 LTS, C# 14.** The
machine used for the first sample has no stable .NET 10 SDK, so the first
verified sample uses stable .NET 9 and does not claim .NET 10 verification.

| .NET release | Type / status | Released / end of support | Default C# | SDK train | ASP.NET Core / EF Core paired train | Official release information | Migration guide | Example coverage |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| .NET Core 1.0 | Legacy / unsupported | 2016-06-27 / 2019-06-27 | SDK-dependent; exact default not asserted here | 1.0 | 1.0 / 1.0 | [Release notes](https://github.com/dotnet/core/blob/main/release-notes/1.0/1.0.0.md) | [Milestone notes](net-core-1.0/migration-guide.md) | Modern equivalent runnable |
| .NET Core 1.1 | Legacy / unsupported | 2016-11-16 / 2019-06-27 | SDK-dependent; verify against selected tooling | 1.1 | 1.1 / 1.1 | [Milestone](net-core-1.1/README.md) · [Release notes](https://github.com/dotnet/core/blob/main/release-notes/1.1/1.1.0.md) | [Historical migration notes](net-core-1.1/migration-guide.md) | No historical runnable example |
| .NET Core 2.0 | Legacy / unsupported | 2017-08-14 / 2018-10-01 | C# 7.3 default for .NET Core 2.x per current language-version table | 2.0 | 2.0 / 2.0 | [Milestone](net-core-2.x/README.md) · [Release notes](https://github.com/dotnet/core/blob/main/release-notes/2.0/2.0.0.md) | [Historical migration notes](net-core-2.x/migration-guide.md) | No historical runnable example |
| .NET Core 2.1 | LTS / unsupported | 2018-05-30 / 2021-08-21 | C# 7.3 | 2.1 | 2.1 / 2.1 | [Milestone](net-core-2.x/README.md) · [Release notes](https://github.com/dotnet/core/blob/main/release-notes/2.1/2.1.0.md) | [Historical migration notes](net-core-2.x/migration-guide.md) | No historical runnable example |
| .NET Core 2.2 | STS / unsupported | 2018-12-04 / 2019-12-23 | C# 7.3 | 2.2 | 2.2 / 2.2 | [Milestone](net-core-2.x/README.md) · [Release notes](https://github.com/dotnet/core/blob/main/release-notes/2.2/2.2.0.md) | [Historical migration notes](net-core-2.x/migration-guide.md) | No historical runnable example |
| .NET Core 3.0 | STS / unsupported | 2019-09-23 / 2020-03-03 | C# 8.0 | 3.0 | 3.0 / 3.0 | [Milestone](net-core-3.0/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0) | [Historical migration notes](net-core-3.0/migration-guide.md) | No historical runnable example |
| .NET Core 3.1 | LTS / unsupported | 2019-12-03 / 2022-12-13 | C# 8.0 | 3.1 | 3.1 / 3.1 | [Milestone](net-core-3.1/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1) | [Historical migration notes](net-core-3.1/migration-guide.md) | No historical runnable example |
| .NET 5 | STS / unsupported | 2020-11-10 / 2022-05-10 | C# 9.0 | 5.0 | 5 / 5 | [Milestone](net-5/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5) | [Historical migration notes](net-5/migration-guide.md) | No historical runnable example |
| .NET 6 | LTS / unsupported | 2021-11-08 / 2024-11-12 | C# 10 | 6.0 | 6 / 6 | [Milestone](net-6/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6) | [.NET Core 3.1 → .NET 5 → .NET 6 guide](net-6/migration-guide.md) | No historical runnable example |
| .NET 7 | STS / unsupported | 2022-11-08 / 2024-05-14 | C# 11 | 7.0 | 7 / 7 | [Milestone](net-7/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7) | [.NET 6 → 7 → 8 guide](net-7/migration-guide.md) | No historical runnable example |
| .NET 8 | LTS / unsupported after 2026-11-10 | 2023-11-14 / 2026-11-10 | C# 12 | 8.0 | 8 / 8 | [Milestone](net-8/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview) | [.NET 6 → .NET 8 guide](net-8/migration-guide.md) | No runnable sample |
| .NET 9 | STS / unsupported after 2026-11-10 | 2024-11-12 / 2026-11-10 | C# 13 | 9.0 | 9 / 9 | [Milestone](net-9/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview) | [.NET 8 → 9 → 10 guide](net-9/migration-guide.md) | No version-specific sample |
| .NET 10 | LTS / supported | 2025-11-11 / 2028-11-14 | C# 14 | 10.0 | 10 / 10 | [Milestone](net-10/README.md) · [What's new](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview) | [.NET 8 → .NET 10 guide](net-10/migration-guide.md) | No stable-SDK runnable sample |

### Interpreting this matrix

- C# defaults are official language-version defaults, not claims that the
  runtime introduced the language. The official table explicitly lists .NET
  Core 2.x and later; early 1.x tooling is treated as SDK-dependent here.
- "SDK train" is the major/minor SDK line, not an exact SDK patch or feature
  band. Pin a concrete SDK separately when a reproducible build requires it.
- ASP.NET Core and EF Core values identify the corresponding release train.
  Read that product's own release documentation for APIs, support, and patch
  lifecycle; do not infer them from the .NET number alone.
- Future releases enter the stable curriculum only after general availability,
  released language mapping, and support dates are confirmed.

## Learning sequence

Start with [the .NET Core 1.0 milestone](net-core-1.0/README.md), then proceed
through the releases chronologically. Planned LTS comparisons are .NET Core
3.1 → .NET 6, .NET 6 → .NET 8, and .NET 8 → .NET 10. Each guide must cover
the intervening STS releases rather than skipping directly between LTS
endpoints; these guides have not been created yet.
