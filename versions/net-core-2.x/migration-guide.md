# Learning or migrating from .NET Core 2.x

All .NET Core 2.x releases are out of support. This guide is for historical
study and migration planning; it does not recommend deploying or continuing
to operate an application on a 2.x runtime.

## Historical learning exercise

1. Record the actual starting point: target framework, installed SDK,
   runtime-dependent or self-contained deployment, operating systems, and
   direct/transitive package versions.
2. Follow the chronology: 2.0 → 2.1 → 2.2. At each step, review the
   [what's-new summary](whats-new.md), the official release notes, and the
   applicable [breaking-change notes](breaking-changes.md). The chronology is
   a study aid; it is not a claim that an intermediate runtime must be used
   for a present-day upgrade.
3. Check the exact framework, API, package, and platform support. In
   particular, investigate the .NET Core 2.1 path-validation change if the
   application relied on exceptions from path APIs.
4. Treat SDK, runtime, and target framework as separate choices. Confirm the
   SDK can build the chosen target, then verify the runtime and deployment
   model independently.
5. Keep ASP.NET Core and EF Core workstreams separate. Their version-specific
   migrations and behavior changes are not covered by a runtime target
   framework update alone.

## For a production application today

Do not select .NET Core 2.x as a new production target: 2.0, 2.1, and 2.2
reached end of support as follows:

| Release | End of support |
| --- | --- |
| .NET Core 2.0 | October 1, 2018 |
| .NET Core 2.1 | August 21, 2021 |
| .NET Core 2.2 | December 23, 2019 |

Use the current [support lifecycle snapshot](../support-lifecycle.md) and live
[Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
to select a supported destination.

Plan the move to the current supported .NET release, rather than stopping on
an unsupported 2.x version. Assess dependencies and platform-specific APIs,
then review breaking changes across the complete source-to-destination path.
Microsoft's [upgrade .NET apps overview](https://learn.microsoft.com/en-us/dotnet/core/porting/)
covers assessment, upgrade paths, and validation. Web applications and
data-access layers need their own ASP.NET Core and EF Core upgrade plans.

## No execution or migration claim

This page contains no runnable historical examples. No .NET Core 2.x
application was built, run, or migrated as part of this curriculum milestone;
no compatibility validation is claimed.

## Official references

- [.NET Core 2.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.0/2.0.0.md)
- [.NET Core 2.1 release notes and LTS history](https://github.com/dotnet/core/blob/main/release-notes/2.1/README.md)
- [.NET Core 2.2 release notes and end of support](https://github.com/dotnet/core/blob/main/release-notes/2.2/README.md)
- [.NET Core 2.1 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/2.1)
- [.NET app upgrade overview](https://learn.microsoft.com/en-us/dotnet/core/porting/)
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support)
