# What's new in .NET 9

This selective tour follows the platform components rather than treating
“.NET 9” as one indivisible product. Feature names and compatibility details
are summarized from the official Microsoft release documentation; consult
those pages for API signatures, prerequisites, and provider/platform limits.
No executable sample or feature benchmark is validated by this curriculum.

## Runtime and performance

.NET 9 continues the focus on performance and cloud-native applications.
Runtime changes include a feature-switch attribute model with trimming
support and dynamic adaptation of garbage collection to application size.
The runtime overview describes this GC adaptation as used by default in
place of Server GC; workload selection still needs measurement and testing.
The JIT includes loop, inlining and Arm64 code-generation/vectorization
improvements.

For a service, the useful lesson is not “upgrading makes it faster” but
“runtime changes can alter the performance profile.” Compare realistic
traffic, startup, allocation, tail latency, and memory use on the target
runtime and hardware before changing capacity assumptions.

## Base libraries and AI building blocks

- **System.Text.Json:** nullable-reference-type annotations for generated
  serialization metadata, JSON Schema export from types, configurable output
  indentation, and support for multiple top-level JSON values from a stream.
  Check serialized contracts and inputs with representative payloads;
  schema generation does not replace API/schema governance.
- **LINQ:** `CountBy` and `AggregateBy` provide keyed aggregation without
  materializing the intermediate groupings associated with common
  `GroupBy`-based approaches. Confirm semantics and benchmark against the
  actual collection and use case.
- **Collections and time:** `PriorityQueue<TElement,TPriority>` can remove an
  item while returning its priority, useful when updating scheduling
  priorities. New integer-taking `TimeSpan.From*` overloads avoid precision
  loss from floating-point inputs; their overload resolution is also a
  compatibility consideration (see the breaking-change notes).
- **Cryptography and reflection:** one-shot hashing, KMAC classes, and
  `PersistedAssemblyBuilder` with PDB support for saved emitted assemblies
  broaden available building blocks. They do not select a cryptographic
  protocol or make dynamic code compatible with trimming/AOT automatically.
- **AI abstractions:** `Microsoft.Extensions.AI` and
  `Microsoft.Extensions.VectorData` provide shared abstractions for model,
  embedding, vector-store and middleware integrations. They are packages,
  not a hosted model or a guarantee of provider interchangeability. The
  `System.Numerics.Tensors` package includes `TensorPrimitives` and
  `Tensor<T>`; the latter is experimental in .NET 9.

## SDK, builds, and developer workflow

The .NET 9 SDK adds **workload sets**, which keep installed workloads on a
specific set version until explicitly updated. This is useful for
reproducible mobile/workload builds, but teams should manage workload updates
and SDK feature bands deliberately. The SDK also improves test integration
with MSBuild and parallel test execution, enables Terminal Logger by default,
adds MSBuild build checks, detects SDK/MSBuild version mismatches, and
provides `dotnet workload history`.

Audit automation that parses build output, manages workload versions, or
assumes a particular SDK/MSBuild pairing. Pin a supported SDK for
reproducibility, but avoid pinning a stale vulnerable SDK; keep the chosen
feature band and servicing version current through a controlled update
process. SDK release/servicing versions are not the same as `net9.0`.

## C# 13

C# 13 is the compiler default for a project targeting `net9.0`. Notable
features include:

- `params` collections, including spans and supported collection types,
  rather than only arrays.
- `System.Threading.Lock` with lock-aware `lock` semantics.
- The `\e` escape sequence, improved method-group natural types, and
  implicit indexer access in object initializers.
- `ref` locals and `unsafe` contexts in iterators and async methods, and
  support for `ref struct` types implementing interfaces and participating
  in generic type arguments under the language's restrictions.
- Partial properties and indexers, and overload-resolution priority for
  library authors.

The C# 13 page identifies `field` as a preview feature during the .NET 9
release period. Treat preview-only behavior as such and do not quietly rely
on it in stable production builds. New language features are optional; the
framework-selected default is generally the safest setting, particularly
for libraries targeting multiple frameworks.

## ASP.NET Core 9 (its own framework release history)

ASP.NET Core 9 is the web framework generation documented separately from
the runtime. The major feature groups include:

- **Static assets:** `MapStaticAssets` uses build/publish-time processing,
  fingerprinted assets, compression, content-based ETags and optimized
  delivery. It is designed for assets known at build/publish time; keep
  `UseStaticFiles` or another appropriate mechanism for files served from
  external/dynamic locations. Check caching, CDN and deployment assumptions
  before swapping middleware/endpoints.
- **Blazor:** new MAUI Blazor Hybrid/Web app templates; runtime component
  render-mode information; a refreshed reconnection experience; simplified
  authentication-state serialization; and support for static SSR pages in a
  globally interactive app. Static SSR can be appropriate for request/response
  behaviors such as cookie handling, but use interactive rendering where
  it fits the page.
- **OpenAPI and APIs:** built-in OpenAPI document-generation support via
  `Microsoft.AspNetCore.OpenAPI`, with additional support for API development
  and Native AOT scenarios. Validate the emitted document and generated
  clients as part of an API contract test.
- **Security, hosting and observability:** additional authentication and
  authorization APIs, Linux development-certificate support, and continued
  monitoring/tracing improvements. Review authentication flows and deployment
  configuration rather than assuming a framework upgrade preserves every
  hosting detail.

ASP.NET Core 9 is covered by the .NET support policy; its detailed release
and migration instructions remain in the ASP.NET Core documentation. See
[ASP.NET Core 9 what's new](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0?view=aspnetcore-9.0)
and the [ASP.NET Core 9 breaking-change catalog](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/9/overview?view=aspnetcore-9.0).

## EF Core 9 (a separate ORM/provider history)

EF Core 9 was released in November 2024 as an STS release and is supported
through November 10, 2026. Unlike ASP.NET Core 9, EF9 targets **.NET 8**,
so it can be used by applications targeting .NET 8 or .NET 9. EF9 and .NET 9
have independently versioned packages and distinct compatibility questions;
check each database provider's supported EF version and database features.

Important EF9 areas include:

- **Azure Cosmos DB for NoSQL:** provider query improvements recognize
  partition-key comparisons more effectively and can optimize point reads
  when both partition key and document ID are provided. Query shape can
  change RU consumption and cross-partition behavior; inspect provider logs,
  actual query plans/requests, and costs against representative data.
- **Migrations and seeding:** new `UseSeeding` and `UseAsyncSeeding` hooks
  centralize initialization/seeding around EF's migration lock. EF9 also
  detects pending model changes when applying migrations and can throw.
  Seeding and production schema deployment should be deliberately designed,
  not triggered blindly at every application startup.
- **Queries and SQL:** improvements include more translation opportunities,
  such as some aggregate and JSON scenarios, and provider-specific
  optimizations. Translation and SQL shape remain provider-dependent; compare
  query plans and results, not just LINQ source.
- **AOT and compiled queries:** EF9 makes progress toward ahead-of-time
  compilation and precompiled queries. These capabilities do not mean every
  provider or application is automatically AOT-compatible; assess warnings,
  provider support and generated output.

The full feature set and detailed provider-specific notes are in
[What's new in EF Core 9](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew).

## .NET MAUI and Aspire

The .NET 9 overview also highlights MAUI performance/reliability work,
updated iOS/Mac Catalyst `CollectionView`/`CarouselView`, the Windows
`TitleBar`, `HybridWebView`, and trimming/AOT improvements. `MainPage` is
deprecated in favor of window creation APIs. MAUI targets and workloads have
platform-specific constraints; use the separate MAUI migration and platform
support documentation before changing app lifecycle or shipping targets.

Aspire 9 supports both .NET 8 and .NET 9 apps and evolves its dashboard,
resource lifecycle, integrations and APIs separately. Do not infer that an
Aspire package's support cadence is identical to the runtime's; pin and
upgrade Aspire packages on their own documented schedule.

## Official Microsoft sources

- [.NET 9 overview](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview)
- [.NET 9 runtime](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/runtime)
- [.NET 9 libraries](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/libraries)
- [.NET 9 SDK](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/sdk)
- [C# 13 features](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13)
- [C# language versioning and defaults](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning)
- [ASP.NET Core 9 release notes](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0?view=aspnetcore-9.0)
- [EF Core 9 what's new and support statement](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew)
- [.NET MAUI 9 what's new](https://learn.microsoft.com/en-us/dotnet/maui/whats-new/dotnet-9)
- [Aspire 9 what's new](https://aspire.dev/whats-new/aspire-9/)
