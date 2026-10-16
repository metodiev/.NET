# .NET 6 compatibility and breaking-change notes

This is an impact-oriented review checklist, not an exhaustive compatibility
catalog. .NET 6, ASP.NET Core 6, and EF Core 6 each have distinct changes.
The target is now unsupported, so use this page for historical study or
upgrade analysis—not as a reason to deploy it.

## .NET runtime, SDK, and libraries

- **`System.Drawing.Common` is supported only on Windows.** Applications
  using it on Linux or macOS must select a supported cross-platform imaging
  library and test image formats, rendering, and deployment. The temporary
  compatibility switch available in .NET 6 did not make this a long-term
  cross-platform solution.
- **File I/O behavior changed.** `FileStream` locking and file-offset
  synchronization differ on Unix, and `Position` updates after asynchronous
  reads/writes complete. Test concurrent access, stream wrappers, offsets,
  and code that observes stream state during asynchronous operations.
- **`Environment.ProcessorCount` on Windows can reflect processor limits.**
  Review worker-pool sizing and application code that assumes it always
  equals the machine's total logical processor count.
- **Unhandled exceptions from `BackgroundService` now stop the host by
  default.** Previously an exception could be logged while the host
  continued. Make the desired failure policy explicit, and ensure health
  checks and orchestrator restart behavior agree with it.
- **Obsolete APIs and nullable annotations produce new diagnostics.**
  Review compiler/analyzer warnings; do not suppress them wholesale. In
  particular, `WebRequest`, `WebClient`, and `ServicePoint` are obsolete in
  favor of newer networking APIs.
- **`DeflateStream`, `GZipStream`, and `CryptoStream` may return partial
  reads.** Stream consumers must continue reading until the requested data
  has been consumed or end-of-stream is reached; one `Read` is not
  guaranteed to fill the buffer.
- **Container console logging format changed** for .NET 6 container images.
  Check log parsers, structured log ingestion, and test assertions that
  depend on the former default formatting.
- Other changes affect Unix file replacement and locking, numeric parsing,
  globalization, networking, serialization, SDK output, publish behavior,
  and API obsoletions. Consult the full catalog for the application's
  operating systems and publish modes.

## ASP.NET Core 6

Review the complete [ASP.NET Core 6 breaking-change list](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/6/overview?view=aspnetcore-6.0).
Selected changes with application impact include:

- **Action results can set status code 200.** `ActionResult<T>` behavior
  changed for some result construction paths. Check controller responses and
  tests that expected a different or unset status code.
- **MVC no longer buffers `IAsyncEnumerable<T>` when using System.Text.Json.**
  Response serialization can stream and execute database queries
  concurrently with response writing. Check resource lifetimes, transaction
  boundaries, consistency expectations, and clients that depend on a
  fully-buffered response.
- **Razor generated views changed assembly and type shape.** The compiler no
  longer emits a separate Views assembly; generated types are internal and
  live in `AspNetCoreGeneratedDocument`. Remove reflection or tooling
  assumptions about the previous implementation.
- **Some assemblies were removed from the `Microsoft.AspNetCore.App` shared
  framework.** Add explicit package references for assemblies your
  application or library actually requires.
- **HTTPS redirection throws when HTTPS ports are ambiguous.** Configure a
  clear HTTPS port when the middleware is used; check reverse-proxy and
  hosting configuration.
- **Identity UI's default Bootstrap version changed**, and minimal API
  endpoint metadata/binding validation behaviors also changed. Review the
  detailed entries if those features are used.

The change from the Startup-based template to minimal hosting is a template
and hosting-model shift, not a mandatory rewrite. Preserve application
behavior and middleware ordering when adopting the new `Program` style.

## EF Core 6

EF Core 6 is a separately versioned ORM. Review the full
[EF Core 6 breaking-change catalog](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/breaking-changes)
and the change list for the exact provider. High-impact items include:

- **Nested optional dependents sharing a table can be rejected** when the
  model has no required non-shared property to identify whether the
  dependent exists. Older behavior could silently lose dependent data when
  materializing and saving. Make the dependent required or add an identifying
  required property, and verify existing data.
- **Changing the owner of an owned entity now throws.** Rework entity
  ownership changes rather than relying on reparenting an existing owned
  instance.
- **SQLite connections are pooled.** Code that depends on a new physical
  connection for every context/connection open must be reconsidered and
  tested.
- **EF Core scaffolding adds join entity types** for many-to-many
  relationships without mapped join entities. Review generated code and
  reverse-engineering diffs.
- **`DbSet` no longer implements `IAsyncEnumerable`.** Update code that
  consumes `DbSet` directly as an async enumerable to use the supported
  query APIs.

The catalog also lists service-lifetime changes, query ordering changes,
Cosmos DB behavior, migration/design-time behavior, and API changes.
Provider authors and extensions should review the catalog in detail.

## Compatibility review checklist

1. Identify the exact source and destination runtime, SDK, target framework,
   ASP.NET Core packages, EF Core packages/provider, OS, and database
   versions. Do not conflate the platform release with web or ORM packages.
2. Read the complete .NET 6 and applicable ASP.NET Core / EF Core catalogs.
   For an upgrade beyond .NET 6, also review every intervening major
   release's changes.
3. Test cross-platform drawing, stream reads and locks, worker counts,
   background-service failure handling, HTTPS redirection, API response
   codes, streaming JSON, and logging where applicable.
4. For EF Core, validate the model, generated SQL, provider behavior,
   migrations, stored data, transaction boundaries, and query results against
   a representative copy of the production database.
5. For production, target a currently supported .NET release, update to its
   latest patch, deploy to representative staging, and verify monitoring,
   health checks, rollback, and data recovery. Never use unsupported .NET 6
   as the long-term production destination.

No application-specific compatibility test or migration was executed for
this documentation milestone.

## Official change catalogs

- [.NET 6 compatibility catalog](https://learn.microsoft.com/en-us/dotnet/core/compatibility/6.0)
- [ASP.NET Core 6 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/6/overview?view=aspnetcore-6.0)
- [EF Core 6 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/breaking-changes)
- [.NET releases, patches, and support](https://learn.microsoft.com/en-us/dotnet/core/releases-and-support)
