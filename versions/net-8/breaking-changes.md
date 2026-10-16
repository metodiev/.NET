# .NET 8 compatibility and breaking-change notes

Major releases can be source-incompatible, binary-incompatible, or
behaviorally different. This page highlights risks worth prioritizing for an
upgrade; it is not a substitute for the complete .NET, ASP.NET Core, and
EF Core breaking-change catalogs. If upgrading from .NET 6, review .NET 7
changes too. The product-specific lists are separate.

## .NET runtime, base libraries, SDK, and containers

### Runtime and library behavior

- **`Enumerable.Sum` can now throw `OverflowException` for some inputs.**
  Check code that relied on overflow/wraparound behavior and test boundary
  values.
- **Unix path handling changed for backslashes.** Audit code that constructs,
  normalizes, compares, or accepts user-provided paths across Windows and
  Unix. A string that happened to be treated as an ordinary filename on Unix
  may be interpreted differently.
- **Some API and serialization behavior changed**, including API
  obsoletions, `Complex.ToString()` formatting, behavior for certain enum
  shapes, and other BCL changes. Treat new warnings as actionable, and check
  consumers that parse formatted values or depend on undocumented behavior.
- Review the official list for runtime-specific compatibility concerns around
  streams, pipes, time zones, reflection, globalization, and other APIs used
  by the application. The complete list classifies behavior, source, and
  binary compatibility by change.

### Container image changes

The .NET 8 Linux container images include changes that can affect builds and
runtime assumptions:

- ASP.NET Core container images use port **8080** by default rather than
  80. Update published container ports, probes, service definitions, and
  application configuration together.
- Linux images run with a non-root `app` user by default. Check file
  ownership, writable directories, startup scripts, capabilities, and
  orchestrator security contexts.
- Debian-based images moved to Debian 12; packages including certificates,
  Kerberos, and `libintl` differ by image/distribution. Review native
  dependencies and package installation instructions.
- Multi-platform image tags and Alpine/Debian package availability changed.
  Validate the exact image variant and architecture used in deployment.

### Build and publish

SDK features such as simplified artifact output can require CI scripts,
package steps, and local tools to stop assuming the former `bin`/`obj` layout.
Web SDK trimming defaults and Native AOT constraints can also reveal source
or dependency issues. Do not enable trimming/AOT as an incidental side effect
of a framework upgrade; inspect warnings and validate reflection-heavy
libraries, serializers, and runtime code generation.

## ASP.NET Core 8

Review the full ASP.NET Core list; high-impact items include:

- **Forwarded headers from unknown proxies are ignored by default.** Configure
  known proxies/networks deliberately. This security-oriented change can
  affect HTTPS redirection, client IPs, host/scheme interpretation, and
  authentication when a load balancer or ingress was not trusted explicitly.
- **Minimal API `IFormFile` binding requires antiforgery checks.** Ensure
  upload endpoints satisfy the antiforgery requirements and that the intended
  authorization/CSRF strategy is in place.
- **Rate-limiting and HTTP-logging middleware require explicit service
  registration** (`AddRateLimiter()` / `AddHttpLogging()`) before
  `UseRateLimiter()` / `UseHttpLogging()`.
- **`ISystemClock` is obsolete** in favor of the time-provider abstractions;
  update custom authentication integrations when convenient and verify
  time-dependent tests.
- Additional changes affect security-token event types, Problem Details
  converters, concurrency-limiter obsolescence, and trimming defaults.

## EF Core 8

EF Core's official breaking-change list covers a specific EF Core 7 → 8
upgrade. If coming from EF Core 6 or earlier, review every intermediate
version's list as well.

### High-impact query and storage changes

- **Parameterized `Contains` in SQL Server LINQ queries** can now translate
  to `OPENJSON`. This improves plan reuse in many cases, but requires SQL
  Server 2016 or later and database compatibility level 130 or later. Older
  SQL Server instances or databases retaining an older compatibility level
  may fail. Check actual database compatibility—not just the server product
  version—and examine query performance for representative workloads.
- **Enums stored in JSON default to integer representation rather than
  strings.** Review existing JSON documents, consumers, serialization
  expectations, and any data conversion plan before changing model/provider
  versions.
- **SQL Server `date` and `time` reverse engineering** now uses `DateOnly`
  and `TimeOnly`. Regenerated entity types and application code may need
  updates.
- **Boolean columns with database-generated values** are no longer scaffolded
  as nullable in the same way. Check generated models and nullability
  assumptions.
- **SQLite `Math` translations** can now execute in SQL where older versions
  may have evaluated differently or failed to translate. Verify SQL results
  and query plans.

Other changes include APIs and metadata types, compiled-model generation,
Cosmos integer-key persistence, discriminator sizing, SQL Server key
comparison behavior, and migration/scaffolding behavior. Providers can have
their own changes; validate generated SQL and schema updates for the exact
provider release in use.

## Upgrade review checklist

1. Identify the exact source runtime, SDK, ASP.NET Core, EF Core, and database
   provider versions; don't treat them as one implicit version.
2. Review the official .NET 6 and .NET 7 catalogs as applicable, then .NET 8.
3. Build against the new SDK and fix reviewed warnings and errors rather than
   suppressing them wholesale.
4. Test file/path handling, numeric boundaries, serialization contracts,
   auth, proxy headers, uploads, rate limiting, and middleware registration.
5. Validate container ports, runtime identity, writable paths, base-image
   packages, architecture, health checks, and deployment manifests.
6. For EF Core, run integration tests against the production database engine
   and compatibility level. Compare SQL, query results, schema migrations,
   JSON documents, scaffolding output, and execution plans.
7. Run the complete application's regression, security, performance, and
   deployment checks on the exact patched runtime and SDK that will ship.

## Official change catalogs

- [.NET 8 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0)
- [.NET 7 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0)
- [.NET 6 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/6.0)
- [ASP.NET Core 8 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/8/overview)
- [EF Core 8 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes)
