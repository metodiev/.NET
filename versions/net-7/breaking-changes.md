# .NET 7 compatibility and breaking-change notes

Major releases can introduce source, binary, and behavioral incompatibilities. This page highlights migration risks for .NET 7; it is not an exhaustive restatement of the Microsoft catalogs. Compatibility work for an upgrade from .NET 6 to .NET 8 must review the .NET 7 **and** .NET 8 changes. ASP.NET Core and EF Core have separate catalogs.

## .NET runtime, libraries, SDK, and deployment

### High-priority runtime and library checks

- **BinaryFormatter APIs are compiler errors.** Remove BinaryFormatter use and migrate persisted data/protocols to a supported serializer with an explicit compatibility and security plan. Do not try to enable unsafe BinaryFormatter serialization as a routine fix.
- **Date/time arithmetic precision changed.** `DateTime` addition methods have adjusted precision behavior. Check code that depends on exact tick-level rounding, boundaries, or serialized results.
- **Numeric format strings support higher precision.** Code that parses or compares formatted numeric text, or assumes the previous maximum format precision, needs boundary tests.
- **Regular-expression ranges were corrected.** Patterns containing ranges may match different characters than before; test the intended Unicode and boundary cases.
- **`System.Drawing.Common` configuration compatibility switch was removed.** Code relying on the old switch should be moved to a supported graphics library or a platform-appropriate implementation; `System.Drawing.Common` is Windows-specific in modern .NET.
- **Reflection invocation and cryptography behavior changed** in enumerated cases. If using reflection-based invocation or certificate-chain/envelope APIs, check the exact entry in Microsoft's catalog and test exception/value behavior.
- **ICU globalization APIs on Windows Server** can produce differences from older globalization behavior. Compare culture-sensitive sorting, formatting, casing, collation, and parsing where results are persisted or externally observable.
- **TLS renegotiation defaults to disallowed** for relevant networking APIs. Review custom TLS clients that rely on renegotiation instead of assuming the previous default.
- **Environment-variable prefixes in configuration** have changed behavior in listed cases. Test configuration sources and precedence in the real service/container environment.

### Trimming, runtime selection, and SDK / CLI

- **Trimming behavior changed for assemblies.** The .NET 7 compatibility catalog lists "all assemblies trimmed by default" for applicable trimming scenarios. Review linker warnings, reflection, serializers, plugin loading, and dynamically discovered types. Do not enable trimming/AOT broadly without validating the actual publish mode and dependencies.
- **Multi-level runtime lookup is disabled.** Test deployment/runtime resolution on machines with multiple runtime installations; don't rely on an app finding a shared framework via the former multi-level lookup behavior.
- **Automatic `RuntimeIdentifier` is publish-only** in the SDK. Review scripts/build targets that expected RID inference during non-publish operations.
- **Solution-level `--output` is not valid for solution-level commands** in the affected CLI scenarios. Audit CI commands; use explicit project output or an appropriate directory/build strategy.
- SDK tool-manifest search and root-folder behavior changed in some scenarios. Check repository-level `dotnet tool` workflows and scripts relying on implicit manifest discovery.

The precise scope, compatibility classification, and mitigation differ by change. Follow the entry in the linked .NET catalog rather than treating this list as a blanket description of every project.

## ASP.NET Core 7

Prioritize these framework-level checks when upgrading a web app:

- **Kestrel no longer supplies the previous default HTTPS binding.** Configure intended HTTP/HTTPS endpoints and certificates explicitly. Verify local development and production hosting separately; don't rely on an implicit HTTPS listener.
- **API controller action parameters can be inferred from dependency injection.** A registered complex type may bind from DI instead of the body. Inspect ambiguous action signatures; use explicit binding attributes or the documented opt-out when appropriate.
- **SignalR hub method parameters can be resolved from DI.** Check hub methods where a service type may also be intended as a client-supplied argument.
- **The default authentication scheme may be inferred** when only one scheme is registered. Validate authentication/authorization behavior, especially if schemes are added dynamically or libraries assume a particular default.
- **ASP.NET-prefixed environment variable precedence changed.** Audit environment-specific overrides and secret/configuration sources.
- **Blazor WebAssembly authentication redirects use navigation history state.** Custom code that expects `returnUrl` query strings may not return the user to the original URL.
- **Output-caching APIs changed names**, and fallback-file endpoints and middleware behavior have compatibility entries. Rebuild custom caching/endpoint configuration against the ASP.NET Core 7 APIs.

The complete ASP.NET Core list also includes auth-provider signatures, MVC model binding, logging event IDs, removed Libuv transport support, and other cases. Review it for the actual framework features in use.

## EF Core 7

EF Core's upgrade risks are separate from the runtime and ASP.NET Core. EF7 also changes some generated SQL and persistence behavior; provider support is not uniform.

- **SQL Server writes may use the `OUTPUT` clause.** Tables with triggers or certain computed-column arrangements can be incompatible with the generated command. Check the exact provider guidance for the table schema, generated SQL, and documented opt-out/configuration mechanism before deploying.
- **Provider and model changes can affect SQL, translation, generated migrations, and scaffolding.** Run integration tests against the real database engine and provider version; inspect migration scripts and test rollback/restore.
- **Set-based `ExecuteUpdate`/`ExecuteDelete` bypass change tracking.** If adopting them, account for concurrency tokens, save interceptors, entity events, and application-level audit behavior.

These are examples to focus review, not the full EF7 change list. EF Core providers may ship additional incompatibilities; inspect both the EF Core catalog and the chosen provider's release notes.

## Upgrade review checklist

1. Inventory each project's TFM, SDK selection, runtime deployment mode, OS/architecture, and package versions. Record ASP.NET Core and EF Core/provider versions separately.
2. Read the complete .NET 7 catalog and, for an eventual .NET 8 target, the .NET 8 catalog. Read product-specific ASP.NET Core and EF Core catalogs for every product used.
3. Search for BinaryFormatter, reflection-heavy serialization or plugins, custom date/numeric formatting, regex ranges, TLS renegotiation, globalization-sensitive behavior, and configuration prefix assumptions.
4. Verify publish settings, runtime selection, trimming, RID, container, and CLI/CI command behavior; treat trim/AOT diagnostics as issues to investigate rather than suppress wholesale.
5. Exercise web endpoint binding, auth defaults, HTTPS/listener configuration, environment configuration, Blazor auth redirects, and output-cache behavior.
6. For EF Core, test the exact provider/database combination, triggers/generated columns, SQL, query results, migrations, transaction/concurrency behavior, and recovery plan.
7. Run the application’s tests and representative production-like deployment checks on the target SDK/runtime and patch level. A successful compile alone does not establish behavioral or data compatibility.

## Official change catalogs

- [.NET 7 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0)
- [.NET 8 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0)
- [ASP.NET Core 7 breaking changes](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/7/overview)
- [EF Core 7 breaking changes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes)
- [Supported .NET deployment modes](https://learn.microsoft.com/en-us/dotnet/core/deploying/)
