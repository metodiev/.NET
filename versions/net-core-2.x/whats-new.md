# What's new across .NET Core 2.x

The sequence moved from a much broader compatible API surface in 2.0, through
performance and memory-oriented additions in 2.1, to runtime diagnostics and
hosting hooks in 2.2. These are platform/runtime and SDK developments, not
three C# language versions: the documented default for all .NET Core 2.x
targets is C# 7.3.

## .NET Core 2.0 — broaden the API surface

- **.NET Standard 2.0 support.** .NET Standard 2.0 added more than 20,000 APIs
  compared with 1.6, substantially increasing the APIs available to portable
  libraries. This made reuse of existing libraries easier, but does not mean
  every .NET Framework API or package became compatible; check the actual API
  and dependency targets.
- **More platform and SDK flexibility.** The release expanded supported Linux
  distributions and allowed SDKs to be installed independently of Visual
  Studio, supporting side-by-side SDK installations. Distinguish the installed
  SDK from the runtime and target framework selected by a project.
- **Runtime store and hosting changes.** Release notes describe side-by-side
  hosting bundles to preserve optimized runtime-store assets for apps built
  against different Core versions. Deployment details remained version- and
  host-specific.

## .NET Core 2.1 — performance, memory, and tools

- **`Span<T>` and related APIs** provide ways to work with contiguous data
  without requiring a separate allocation for every slice. Their use is
  constrained by their lifetime/ref-safety rules; learn those constraints
  rather than treating them as a universal replacement for arrays.
- **`SocketsHttpHandler`** became the default HTTP handler, with an opt-out
  path to the earlier handler documented in the release notes. Handler choice
  can affect networking behavior, so test the protocols, proxies, TLS, and
  operating systems your app actually uses.
- **Tiered compilation** was introduced as an opt-in runtime feature. It
  compiles quickly for startup and can optimize frequently used methods later.
  The trade-off is workload-dependent: measure startup and steady-state
  behavior rather than assume a universal improvement.
- **Global .NET tools** could be installed and invoked from the CLI, and
  `dotnet --info` reported more installed-component details. These are SDK
  workflow changes, not runtime APIs.

## .NET Core 2.2 — diagnostics and hosting

- **Runtime events through `EventListener`.** Applications could consume
  CoreCLR events for services such as GC, JIT, ThreadPool, and interop on
  platforms where Windows ETW is unavailable. This is useful for diagnostics,
  but instrumentation and telemetry volume should be designed and measured.
- **Startup hooks** allowed host-provided code to run before `Main`, for
  deployment-time policy, tracing, or telemetry without recompiling the
  application. That power also means deployed behavior can depend on
  environment configuration outside the application's entry point.
- **Tiered compilation remained opt-in in the final 2.2 release.** It was
  enabled by default in a 2.2 preview and then reverted before final release;
  do not infer the final behavior from the preview.
- **Access-token support in `SqlConnection`** enabled Azure Active Directory
  authentication scenarios with the applicable SQL client package. This is
  a data-provider API, not a runtime-wide authentication facility.

## Product boundary

The 2.0–2.2 release cycle also included important ASP.NET Core and EF Core
work, but those changes belong to their respective products. For example, the
2.2 announcement explicitly described ASP.NET Core 2.2 and EF Core 2.2 as
separate releases and said that the biggest improvements were in ASP.NET Core.
Consult their own release material before applying framework or ORM guidance
to a project.

## Sources

- [.NET Core 2.0 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.0/2.0.0.md)
- [What's new in .NET Core 2.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-2-0)
- [.NET Core 2.1 release notes](https://github.com/dotnet/core/blob/main/release-notes/2.1/2.1.0.md)
- [What's new in .NET Core 2.1](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-2-1)
- [.NET Core 2.2 release announcement](https://devblogs.microsoft.com/dotnet/announcing-net-core-2-2/)
- [What's new in .NET Core 2.2](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-2-2)
- [C# language-version defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning#defaults)
- [ASP.NET Core 2.2 release announcement](https://devblogs.microsoft.com/dotnet/announcing-asp-net-core-2-2/)
- [EF Core 2.2 release announcement](https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-2-2/)
