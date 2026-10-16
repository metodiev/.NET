# C# and .NET Learning Academy

A version-aware learning repository for understanding how C# and .NET evolved,
how to use their features, and how to make sound engineering and migration
decisions. The curriculum keeps the histories of the language, runtime, SDK,
ASP.NET Core, and Entity Framework Core distinct while showing where they meet.

## Start here

1. Read the [learning roadmap](learning-roadmap.md).
2. Use the [version index](versions/index.md) to choose a release.
3. Check the dated [support lifecycle snapshot](versions/support-lifecycle.md)
   before choosing a production target.
4. Browse the [C# language history](versions/csharp-history.md).
5. Run the first [historical .NET Core 1.0 milestone](versions/net-core-1.0/README.md)
   and its [safe modern equivalent](examples/platform/net-core-1.0-first-app/README.md).

## Repository map

- `versions/` — release history, support status, compatibility, and per-release
  learning material.
- `examples/` — small runnable projects grouped by the platform or language
  feature they teach.
- `docs/` — conventions for tutorials, examples, and validation.
- `CsharpIntro/` — pre-existing introductory exercises; retained as-is and not
  yet audited or integrated into the versioned curriculum.

## Accuracy and validation policy

Release dates, lifecycle status, and version relationships are based on the
linked Microsoft sources. Lifecycle status is a dated snapshot, not a promise
that a release remains supported indefinitely. Historical tutorials explain
past platforms; they are not deployment recommendations. Preview releases and
features stay outside the stable curriculum.

The first runnable sample uses the installed stable .NET 9 SDK and runtime for
validation. .NET 10 is the current LTS baseline in the curriculum, but this
workspace only has a .NET 10 preview SDK/runtime installed; stable .NET 10
examples will be added or verified when a stable SDK is available. See the
[validation record](examples/platform/net-core-1.0-first-app/README.md#validation).

## Official starting points

- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [C# language history](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history)
- [.NET documentation](https://learn.microsoft.com/en-us/dotnet/)
- [ASP.NET Core documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [EF Core documentation](https://learn.microsoft.com/en-us/ef/core/)

