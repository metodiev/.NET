# First app: historical .NET Core 1.0 workflow, modern execution

This sample demonstrates the enduring idea behind the .NET Core 1.0 launch
example: create a console app, restore/build it, and run it with the `dotnet`
CLI. It intentionally targets `net9.0`, not `netcoreapp1.0`, because .NET Core
1.0 is unsupported. This sample does **not** reproduce the old runtime,
compiler, project format, ASP.NET Core, or EF Core.

## Requirements

- .NET 9 SDK (stable). The project targets `net9.0` and its `global.json`
  requests SDK `9.0.302` (allowing later patches in that feature band).
- A current .NET 9 runtime to execute the framework-dependent build.
- No preview compiler is required.

## Build and run

From this sample's directory:

```powershell
dotnet --version
dotnet run
```

Expected output:

```text
Hello from the modern .NET SDK.
```

The example uses classic `using`, `Program`, and `Main` syntax to keep the
source readable alongside older C# materials. The compiler version is still
selected by the project SDK and target framework; the source is not compiled
with a C# 1 compiler.

## Connection to the historical release

The .NET Core 1.0 announcement demonstrated `dotnet new`, `dotnet restore`,
and `dotnet run`. Current SDKs retain the CLI-centered workflow, while
templates, project files, framework APIs, and runtime behavior have changed.
For the historical context, read the
[.NET Core 1.0 milestone](../../../versions/net-core-1.0/README.md) and its
[what's-new notes](../../../versions/net-core-1.0/whats-new.md).

## Validation

Verified on October 16, 2026 with SDK `9.0.302` selected by this sample's
`global.json`:

- `dotnet --version` — reported `9.0.302`.
- `dotnet run` — passed; output matched exactly.
- Historical .NET Core 1.0 runtime/toolchain — not run.
- .NET 10 stable build — not run; the workspace has only a .NET 10 preview
  SDK/runtime, so this stable example does not use them.
- Automated tests, Markdown links, and repository-wide checks — not yet
  configured.
