# Learning from .NET Core 1.0 without deploying it

This guide is for historical understanding, not a recommendation to migrate
or deploy an application on .NET Core 1.0. Its support ended June 27, 2019.

## For a historical porting exercise

1. Identify whether the original application uses .NET Framework-only APIs,
   ASP.NET (`System.Web`), or Entity Framework 6-specific behavior.
2. Check each API and package against the exact .NET Core 1.0 compatibility
   information and product release notes.
3. Treat ASP.NET Core and EF Core as separate migration projects, not automatic
   substitutions.
4. Record project-format, restore, build, publish, and deployment differences.
5. Isolate any old SDK/runtime; do not install it on a production host.

## For a production application today

Do not target .NET Core 1.0. Select a currently supported .NET release using
the dated [support lifecycle](../support-lifecycle.md), then use Microsoft's
current [porting guidance](https://learn.microsoft.com/en-us/dotnet/core/porting/)
and the release-specific breaking-change documents for the complete path.
The [version index](../index.md) identifies the LTS comparison guides that
will be built for this curriculum.

## Verification status

No historical .NET Core 1.0 application was migrated or executed in this
workspace. The runnable companion sample is a modern equivalent targeting
`net9.0`; it does not validate old runtime compatibility.

