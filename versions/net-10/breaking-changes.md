# .NET 10 compatibility and breaking-change review

This checklist highlights changes worth investigating when moving to .NET 10.
It is intentionally not a complete catalog. Microsoft labels the general
.NET 10 breaking-change page as a work in progress, and the ASP.NET Core and
EF Core catalogs are separate. Always read the current official pages and
their linked individual change notes for the exact old/new behavior,
mitigation, and applicability.

## Platform, runtime, SDK, and libraries

- **Container images use Ubuntu by default.** Review Dockerfiles, image
  inheritance, package-manager commands, native dependencies, CA certificates,
  and security-scanner baselines. A build or production script that assumed a
  Debian image family may need explicit image selection or adjustment.
- **Default trace-context propagation follows W3C.** If telemetry crosses
  services or depends on legacy propagation formats, check trace continuity,
  parent/span relationships, and collector configuration.
- **`ActivitySource` sampling behavior changed.** Recheck listeners and
  instrumentation that relies on activity creation/sampling semantics; verify
  that traces are neither unexpectedly omitted nor over-produced.
- **`BackgroundService` executes all of `ExecuteAsync` as a task.** Review
  hosted-service initialization, cancellation, exception behavior, startup
  ordering, and the test assumptions around long-running work.
- **Browser HTTP response streaming is enabled by default.** In Blazor
  WebAssembly, `ReadAsStreamAsync` can return a browser stream rather than a
  `MemoryStream`; synchronous reads are unsupported. Use asynchronous
  consumption, explicitly copy to memory when justified, or opt out after
  reviewing the documented settings.
- **`System.Linq.AsyncEnumerable` is now in the core libraries.** Source
  projects that also reference packages defining overlapping APIs may face
  ambiguous names or changed binding. Check compile errors and package
  references.
- **`dotnet new sln` defaults to the `.slnx` format.** Check scripts and
  integrations that assume a `.sln` filename, parse solution files, or use
  tooling that doesn't yet accept `.slnx`.
- **NuGet audit and CLI defaults changed.** Transitive dependencies are
  audited; local tool installs can create a manifest; `dotnet package list`
  performs restore; and CLI output may be written to stderr. Pipeline scripts
  should distinguish warnings/errors and stdout/stderr, and teams should
  account for newly surfaced vulnerability advisories.
- **NuGet requires explicit package versions in `PackageReference`.** Resolve
  previously unversioned direct references and review package central
  management and restore behavior.
- **HTTP/3 can be disabled when publishing trimmed apps.** If an application
  depends on HTTP/3, verify the trimmed publish configuration and protocol
  availability rather than infer it from an untrimmed development run.
- **Other areas to assess include** stricter `System.Text.Json` property-name
  conflict checks, stricter email address validation, cryptography API and
  environment-variable changes, Linux `DriveFormat` results, and
  `BufferedStream.WriteByte` no longer flushing implicitly.

See Microsoft's [full .NET 10 compatibility catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10)
for change type, affected technology, and migration-specific details.

## ASP.NET Core 10

ASP.NET Core lists these behavioral/source changes separately:

- **Cookie authentication no longer redirects known API endpoints to login
  or access-denied pages.** Clients should receive API-appropriate status
  behavior, not depend on browser-oriented redirects. Verify endpoint
  metadata, clients, and any custom authentication events.
- **`WithOpenApi` and MVC API analyzers are deprecated.** Update OpenAPI
  generation/configuration rather than treating the APIs/analyzers as a
  stable long-term integration.
- **Razor runtime compilation is obsolete.** Review development workflows
  that depend on runtime compilation and move toward supported build-time
  approaches.
- **`IActionContextAccessor`, `WebHostBuilder`, `IWebHost`, and `WebHost`
  are obsolete.** Identify compile-time warnings and older hosting setup
  before they become forced migrations.
- **Forwarded-header network configuration APIs are obsolete.** Review
  `KnownNetworks` configuration and proxy trust lists; do not remove
  network restrictions without an equivalent, verified proxy policy.
- **Blazor response streaming and navigation behavior changed.** In addition
  to the browser stream type above, same-page `NavigateTo` no longer scrolls
  to the top by default, and `NavLinkMatch.All` ignores query strings and
  fragments. Check UI tests and applications that depended on prior scroll
  or matching behavior.

The complete, versioned list is in
[Breaking changes in ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/overview?view=aspnetcore-10.0).

## EF Core 10

EF10 upgrade review should include model tooling, generated SQL, and schema
effects—not just application compilation:

- **Multi-targeted projects require `--framework` for EF tools.** For
  `dotnet ef` operations on a project with `TargetFrameworks`, explicitly
  select the intended TFM. Update migration and CI scripts.
- **Connection string `Application Name` is injected when absent.** If EF
  and another data-access technology share a database under a
  `TransactionScope`, differing connection strings can produce distinct
  SqlClient pools and may trigger transaction escalation. Set a deliberate
  application name and test mixed-access transactions.
- **SQL Server JSON mapping can change to the native `json` column type** for
  Azure SQL or compatibility level 170+. Inspect generated migrations,
  server compatibility, indexing/query support, rollout time, and rollback
  implications. You can preserve `nvarchar(max)` explicitly or configure a
  lower compatibility level where appropriate.
- **Parameterized collections use multiple scalar parameters by default.**
  A LINQ `Contains` query can now yield an `IN` list of parameters rather than
  the previous JSON-array parameter translation. Benchmark representative
  collection sizes and query plans; do not assume the new shape is faster
  for every database/workload.
- **Default constraint naming and complex-type column naming can change
  migrations.** Enabling automatic default-constraint naming may rename
  existing constraints in the next migration; new unique/full-path names for
  complex properties may also generate schema changes.
- **Some EF APIs and diagnostic interfaces changed.** Review
  `ExecuteUpdateAsync`, conventions, relational diagnostics, and SQL parameter
  names if the application has custom providers, conventions, or diagnostic
  integrations.

See [Breaking changes in EF Core 10](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes)
for the complete list and specific mitigation guidance. EF Core 10's
documented support window ends November 10, 2028; this is a separate product
date from .NET 10 runtime support through November 14, 2028.

## Review method

For each application and library:

1. Inventory every TFM, SDK pin, package/provider version, hosting model,
   publish option, database, and deployment image.
2. Sort changes into source/binary incompatibilities and behavioral changes;
   read the linked page for the exact condition and mitigation.
3. Test externally visible behavior, including auth redirects, telemetry,
   background services, HTTP streaming/protocols, and CLI scripts.
4. Generate migrations in a review branch and inspect SQL, query plans, and
   schema changes against a production-like database.
5. Re-run integration, load, security, publish/trimming, and container tests
   on stable .NET 10 and the actual supported operating systems before
   production rollout.

## Official references

- [.NET 10 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10)
- [ASP.NET Core 10 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/overview?view=aspnetcore-10.0)
- [EF Core 10 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes)
- [C# 14 what's new and language-version defaults](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)
