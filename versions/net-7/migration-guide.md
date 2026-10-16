# Historical migration guide: .NET 6 → .NET 7 → .NET 8

This guide preserves the intermediate compatibility step for learning and controlled migrations. It is **not** a recommendation to deploy .NET 7 today. On the October 16, 2026 snapshot, .NET 6 and .NET 7 are unsupported, and .NET 8 reaches end of support on November 10, 2026. For a new or long-lived production migration, evaluate a currently supported LTS target (for example, .NET 10) and review every intervening change set. If an existing system must pass through `net7.0` or `net8.0` to isolate changes, treat those as temporary test/build stages, not safe production stopping points.

## Keep the products and versions distinct

- **SDK:** the toolchain used to build and publish. The 7.0 SDK train uses 7.0.x SDK versions and feature bands; the launch SDK was 7.0.100.
- **TFM:** update the application target to `net7.0` for the intermediate build, then to `net8.0` (or the intended supported TFM).
- **Language:** the default C# version follows the SDK/target framework: C# 11 for `net7.0`, C# 12 for `net8.0`. Don't set `LangVersion` to `latest` to force language upgrades independently of the target.
- **ASP.NET Core:** update framework package references and apply that framework's migration/breaking-change instructions.
- **EF Core:** select a provider-compatible EF Core version separately. EF7 targets .NET 6 and can run on .NET 6 or 7; changing the application TFM does not itself require EF7. Test ORM/provider/database changes independently.

## Phase 0: inventory and baseline

1. Record all project TFMs and SDK selection (`global.json`), build scripts, analyzers, source generators, global/package references, and `dotnet tool` manifests.
2. Inventory OS versions, CPU architectures, hosting model, containers, native libraries, trimming/AOT, self-contained/framework-dependent publishing, and runtime selection.
3. List ASP.NET Core features in use: Kestrel bindings, endpoint routing, controllers, auth schemes, SignalR, output caching, Blazor, environment variables, and static/fallback files.
4. List EF Core and database provider versions, database engine/version/compatibility settings, migrations, triggers/computed columns, query hot paths, and provider-specific features.
5. Capture a baseline: unit/integration tests, API contracts, auth and redirect behavior, configuration precedence, representative SQL/results/plans, migration state, deployment manifests, startup probes, and performance measurements.
6. Verify third-party/native dependencies support the intended target and OS. Plan a destination that remains supported for the deployment's expected lifetime.

## Phase 1: .NET 6 → .NET 7 compatibility checkpoint

Use a branch or isolated build environment for this historical step. Install a .NET 7 SDK and pin an SDK version available to the team in `global.json`, then change project TFMs to `net7.0`. Align relevant version-coupled packages (such as `Microsoft.AspNetCore.*`, `Microsoft.Extensions.*`, `System.Net.Http.Json`, analyzers, and build tools) only when required by the target and framework.

Then:

1. Build and test with the .NET 7 SDK; resolve new compile diagnostics, including BinaryFormatter errors, rather than suppressing them wholesale.
2. Review the complete [.NET 7 breaking-change catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0), especially runtime behavior, trimming/runtime lookup, SDK CLI behavior, regex/date/numeric precision, globalization, and networking changes relevant to the app.
3. For ASP.NET Core, follow [Microsoft's 6.0-to-7.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/60-to-70) and [ASP.NET Core 7 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/7/overview). Validate Kestrel listeners, DI-based controller and SignalR parameter binding, authentication defaults, Blazor redirect state, and environment-variable precedence.
4. If adopting EF7, check [EF Core 7 what's new](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew) and its [breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes), align each provider, and exercise migrations and SQL Server `OUTPUT` behavior where applicable.
5. Run tests on the target deployment OS and architecture. Preserve the test results as evidence for this codebase only.

Do not release or leave a production system on .NET 7: support ended May 14, 2024.

## Phase 2: .NET 7 → .NET 8 (or supported destination)

If `net8.0` is an intentional compatibility intermediate, update SDK selection to an installed .NET 8 SDK and change each applicable TFM to `net8.0`. Update corresponding ASP.NET Core and EF Core packages/tools deliberately, check provider support, and review the .NET 8 breaking-change catalogs as well as any target beyond .NET 8. Avoid simultaneous unrelated dependency upgrades that make failures hard to attribute.

For an ASP.NET Core app, use Microsoft's [7.0-to-8.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/70-to-80) and [ASP.NET Core 8 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/8/overview). For EF, review the EF Core 8 provider-specific guidance and [EF Core 8 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes).

When the real target is a currently supported release newer than .NET 8, use the equivalent official migration guide and compatibility catalog for each additional major transition. Do not infer that jumping project TFMs skips the need to review intermediate changes.

Validate at this stage:

- build, test, publish, and deployment using the exact selected SDK, runtime, OS, and architecture;
- runtime/library behavior and serialization contracts with representative input, including edge cases;
- web endpoints, proxy/listener/security configuration, auth, uploads, and static assets;
- database migrations, generated SQL, provider compatibility, triggers, query semantics, concurrency, backups, and rollback;
- container images, ports, user permissions, file ownership, certificates, native packages, health checks, and deployment manifests if applicable;
- trimming, AOT, single-file, or other publish options only when deliberately enabled and all diagnostics and dependencies have been reviewed.

## Phase 3: release and service

Run full regression, integration, security, performance, and operational checks in staging. Deploy incrementally with monitoring and a rehearsed rollback/restore path. Stay current on the supported release's servicing updates. An intermediate successful build does not prove that the final target is secure, compatible, or supported.

## Verification note

These files are historical curriculum documentation only; no runnable .NET 7 project is supplied here and no SDK/build/test execution is claimed. Verify the installed SDK, runtime, OS support, current Microsoft support policy, and your application's own test results before applying migration steps.

## Microsoft migration and lifecycle references

- [ASP.NET Core 6 → 7 migration](https://learn.microsoft.com/en-us/aspnet/core/migration/60-to-70)
- [ASP.NET Core 7 → 8 migration](https://learn.microsoft.com/en-us/aspnet/core/migration/70-to-80)
- [.NET 7 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0)
- [.NET 8 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0)
- [ASP.NET Core 7 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/7/overview)
- [EF Core 7 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes)
- [Microsoft .NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
