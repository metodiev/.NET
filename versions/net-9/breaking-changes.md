# .NET 9 compatibility and breaking-change review

This is a selected, impact-oriented review guide, not a replacement for the
official compatibility catalogs. A major upgrade can produce source,
binary, and behavioral incompatibilities. Review changes from every version
crossed, and distinguish the base platform from ASP.NET Core, EF Core,
providers, containers, and SDK tooling.

## .NET runtime, libraries, SDK, and deployment

### Runtime and library behavior

- **Environment variables now take precedence over app runtime configuration
  settings in relevant deployment configuration.** Review
  `DOTNET_ROLL_FORWARD`, `DOTNET_ROLL_FORWARD_TO_PRERELEASE`, and
  `DOTNET_ROLL_FORWARD_ON_NO_CANDIDATE_FX` (including their runtime-config
  counterparts) wherever a host, container, or deployment pipeline sets
  roll-forward behavior. An environment-level setting can change which
  runtime is selected. Test the effective runtime in the actual process
  environment, and remove conflicting configuration instead of relying on
  undocumented precedence.
- **`String.Trim` overload resolution changed.** The C# 13 compiler prefers
  `params` span-type overloads in some cases, and a .NET 9
  `Trim(params ReadOnlySpan<char>)` overload was removed. Recompile and
  inspect ambiguous or overload-sensitive calls, especially source that
  passes `null`, arrays, or implicitly converted spans. Make the intended
  overload explicit and run behavior tests; do not assume the same call binds
  identically across SDK versions.
- **New `TimeSpan.From*` integer overloads can alter overload resolution.**
  This is a source-compatibility consideration for calls whose argument
  types could bind to old floating-point overloads or new integer overloads.
  Verify time calculations and boundary/rounding behavior after recompiling.
- **Keyed dependency injection is stricter.** `FromKeyedServicesAttribute`
  no longer injects a non-keyed service for a parameter marked as keyed.
  Check constructors and endpoint parameters that combine keyed and ordinary
  registrations; register the intended key explicitly and add resolution
  tests.
- **ZIP and binary parsing behavior has tightened/changed.** ZIP entry
  encoding respects the UTF-8 general-purpose flag, and `BinaryReader.ReadString`
  returns replacement characters for malformed sequences. Audit code that
  parses untrusted or legacy archives/data or depends on byte-for-byte
  round-tripping malformed encodings.
- **Other targeted areas:** changes include BigInteger input limits,
  `UnsafeAccessor` behavior for non-open generics, X.509 constructor
  obsoletions, changed private-key lifetime behavior on Windows, and API
  obsoletions with custom diagnostic IDs. Check the exact compatibility
  note for each API used rather than suppressing new diagnostics wholesale.

### Containers and publish

- .NET 9 container images no longer install `zlib` by default. If an
  application or native dependency expects that system package, add and
  maintain it intentionally in the image or select an appropriate image
  variant. Check image build stages and runtime-stage assumptions separately.
- .NET Monitor images use simplified version-only tags. Review image
  references, pinning and update policies for diagnostic sidecars.
- Deployment environment variables take precedence over runtime
  configuration settings (above). Check environment injection from
  orchestrators, App Service settings, and CI/CD templates.

### SDK and language

The SDK's Terminal Logger is enabled by default, and workload sets improve
version consistency. Build automation that scrapes output, depends on
stdout/stderr placement, expects previous logging formatting, or installs
workloads implicitly should be exercised under the selected SDK. New
`params` collection language rules can also change overload selection. Test
the source with the actual .NET 9 SDK/compiler used by CI, not only the IDE.

## ASP.NET Core 9

ASP.NET Core maintains a product-specific list of source, binary, and
behavioral changes. Among the entries to review:

- **HostBuilder enables `ValidateOnBuild` and `ValidateScopes` in
  development.** Dependency injection graphs that were invalid but previously
  failed only when resolved can now fail during development startup. Fix
  lifetime mismatches and incomplete registrations instead of disabling
  validation to conceal them.
- **Middleware with multiple constructors has changed constructor
  selection behavior.** Review custom middleware that exposes multiple
  public constructors; test activation through the actual middleware
  pipeline.
- **Development certificate export no longer creates a missing folder.**
  Ensure the target directory exists and is permissioned before exporting a
  certificate in scripts or developer setup.
- **Data-protection key-resolution semantics changed.** Review custom key
  management and rotation logic that uses
  `DefaultKeyResolution.ShouldGenerateNewKey`; test key expiration, startup,
  and persisted key-ring behavior.
- ASP.NET Core's ASP.NET 9 catalog includes legacy Mono/Emscripten API
  namespace changes and other targeted source/behavior changes; inspect the
  full list for Blazor/WebAssembly applications.

**Inherited risk, not a new .NET 9 change:** .NET 8 changed forwarded-header
handling so headers from unknown proxies are ignored by default. Applications
upgrading from .NET 8 should keep the deliberately configured trusted proxy
list and verify ingress/load-balancer paths. The ASP.NET Core 9 overview
references this earlier change in its navigation; it should not be
misattributed to the .NET 9 release.

Static asset optimization (`MapStaticAssets`) also changes delivery mechanics
if adopted. Confirm asset paths, cache/fingerprint expectations, dynamically
served files, CDN behavior, and publish output. It is an opt-in migration
choice, not a universal automatic behavior replacement for every use of
`UseStaticFiles`.

## EF Core 9

EF9 has a separate breaking-change catalog for EF Core 8 → 9. These are
particularly important:

- **Applying migrations with pending model changes now throws.** EF9 detects
  a model that differs from its latest migration snapshot when calling
  `dotnet ef database update`, `Migrate`, or `MigrateAsync`. Add and review
  the missing migration, repair a missing snapshot, or make model seeding
  deterministic. Dynamic values such as `DateTime.Now` or `Guid.NewGuid()`
  in model-managed `HasData` can make the model appear to change every run.
  Suppress the warning only after understanding and intentionally accepting
  the mismatch.
- **An external transaction around migration application now triggers an
  exception by default.** EF9 runs `Migrate`/`MigrateAsync` using its own
  transaction and execution strategy as needed. An outer transaction can
  prevent EF from acquiring its database lock and leave concurrent migration
  applications unprotected. Remove the external transaction; do not suppress
  the warning unless the concurrency and failure semantics are deliberately
  addressed.
- **Migration transaction behavior changed.** Pending migrations can now be
  applied in a single transaction where supported. Review long-running
  migrations, transaction-size/lock implications, provider capabilities and
  deployment windows.
- **EF tooling and design-time dependencies:** `Microsoft.EntityFrameworkCore.Design`
  resolution changes and EF tools no longer support .NET Framework projects.
  Review tool version alignment, startup-project/target-framework selection,
  and CI migration commands.
- **Cosmos provider changes are substantial.** The provider's query and
  document behavior was revised. Review the dedicated Cosmos breaking-change
  section, partition-key requirements, document IDs, discriminator handling,
  and query semantics before upgrading existing Cosmos applications.

Other entries cover primitive collection APIs, compiled models, SQL Server
`IndexOf` null propagation, JSON collection tracking, and provider/service
dependencies. EF Core providers are independently shipped; a successful
compile does not prove the provider supports the EF9 API set or translates
queries as expected.

## Review checklist

1. Record source versions for runtime, SDK, target frameworks, ASP.NET Core,
   EF Core, each EF provider, database, container image, and third-party
   integrations.
2. Read the complete .NET 9 catalog and earlier catalogs if upgrading from
   before .NET 8. Read ASP.NET Core and EF Core catalogs separately.
3. Recompile using the actual selected stable SDK. Review compiler and
   analyzer diagnostics, overload binding, DI activation, startup validation,
   and runtime selection under production environment variables.
4. Test container builds from clean layers; verify native/system packages,
   certificates, image tags, permissions, architecture, and runtime behavior.
5. For web apps, exercise proxy headers, auth, startup, static assets,
   generated OpenAPI, Blazor modes, and publish/AOT behavior that the app uses.
6. For EF, generate migrations on a branch; inspect model snapshots and SQL;
   test updates, lock/transaction behavior, seeding, rollback/restore, and
   query results/plans against a production-like copy of the exact database
   engine and provider.
7. Run application-level integration, security, load, and deployment checks
   on the patched runtime and actual target platforms. Keep rollback distinct
   from database schema reversal.

## Official Microsoft catalogs

- [.NET 9 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0)
- [Environment-variable precedence](https://learn.microsoft.com/en-us/dotnet/core/compatibility/deployment/9.0/environment-variable-precedence)
- [String.Trim overload removal](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/9.0/string-trim)
- [TimeSpan integer overloads](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/9.0/timespan-from-overloads)
- [Container images no longer install zlib](https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/9.0/no-zlib)
- [ASP.NET Core 9 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/9/overview?view=aspnetcore-9.0)
- [EF Core 9 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes)
- [EF Core 8 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes)
- [ASP.NET Core 8 forwarded-header change](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/8/forwarded-headers-unknown-proxies)
