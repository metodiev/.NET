# .NET support lifecycle snapshot

**Snapshot verified: October 16, 2026.** Microsoft updates the live policy;
recheck it before using this table for a production decision.

The current primary baseline is **.NET 10 LTS**, released November 11, 2025
and supported through November 14, 2028. The policy page lists patch
10.0.13, released October 13, 2026. A supported release must remain current
on servicing updates.

## Unified .NET lifecycle

| Release | Type | Released | End of support | Status on October 16, 2026 |
| --- | --- | --- | --- | --- |
| .NET Core 1.0 | Legacy; current policy does not classify this out-of-support row as LTS/STS | June 27, 2016 | June 27, 2019 | Unsupported |
| .NET Core 1.1 | Legacy; current policy does not classify this out-of-support row as LTS/STS | November 16, 2016 | June 27, 2019 | Unsupported |
| .NET Core 2.0 | Legacy; current policy does not classify this out-of-support row as LTS/STS | August 14, 2017 | October 1, 2018 | Unsupported |
| .NET Core 2.1 | LTS | May 30, 2018 | August 21, 2021 | Unsupported |
| .NET Core 2.2 | STS/current release | December 4, 2018 | December 23, 2019 | Unsupported |
| .NET Core 3.0 | STS/current release | September 23, 2019 | March 3, 2020 | Unsupported |
| .NET Core 3.1 | LTS | December 3, 2019 | December 13, 2022 | Unsupported |
| .NET 5 | STS | November 10, 2020 | May 10, 2022 | Unsupported |
| .NET 6 | LTS | November 8, 2021 | November 12, 2024 | Unsupported |
| .NET 7 | STS | November 8, 2022 | May 14, 2024 | Unsupported |
| .NET 8 | LTS | November 14, 2023 | November 10, 2026 | Supported; maintenance |
| .NET 9 | STS | November 12, 2024 | November 10, 2026 | Supported; maintenance |
| .NET 10 | LTS | November 11, 2025 | November 14, 2028 | Supported; active |

LTS and STS describe support duration, not quality. Microsoft describes the
current cadence as annual: even-numbered major releases are LTS and
odd-numbered major releases are STS. Older releases are shown using historical
labels only where the cited policy supports them; "legacy" avoids retroactively
assigning a modern label absent from the current historical table.

## Preview boundary

The policy lists **.NET 11 RC2**, released October 13, 2026, with support
through November 10, 2026. It is a go-live release, not a stable production
baseline in this curriculum. Preview APIs and language features remain
separate until final release and documentation verification.

## Product boundaries and source of truth

The .NET policy covers related technologies, but their version numbers and
servicing rules are not interchangeable:

- The SDK has feature bands and a version distinct from the runtime. See
  [Microsoft's SDK/runtime versioning guidance](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support#feature-bands-sdk-only).
- C# defaults are determined by the target framework and compiler; see
  [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning).
- ASP.NET Core and EF Core version mappings and support details must be checked
  against their own release notes and support documentation. The [version
  index](index.md) records paired release trains, not a claim that all product
  support rules are identical.
- .NET Framework has a separate lifecycle; consult
  [Microsoft .NET Framework Lifecycle](https://learn.microsoft.com/en-us/lifecycle/products/microsoft-net-framework).

## Sources

- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) — supported and out-of-support release tables; page updated October 13, 2026.
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support) — release cadence, servicing, SDK feature bands, and runtime selection.
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning) — target-framework defaults and compatibility notes.

