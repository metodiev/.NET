# .NET Core 1.0 compatibility and breaking-change notes

This is a historical baseline, so there is no prior .NET Core LTS release to
compare against. It is not a drop-in upgrade target for .NET Framework.

Key compatibility boundaries to investigate when studying a historical
port:

- **API surface:** .NET Core did not include every API available in .NET
  Framework. Check the exact target API and package support rather than assume
  source compatibility guarantees runtime compatibility.
- **Application model:** ASP.NET Core was a new web framework, not the old
  `System.Web` hosting model carried over unchanged.
- **Data access:** EF Core 1.0 was a distinct product generation. Do not assume
  feature parity or behavior compatibility with Entity Framework 6.
- **Project tooling:** early .NET Core project formats and tooling changed.
  Do not copy an old `project.json` project into a modern SDK project without
  understanding the migration and dependency changes.
- **Deployment and runtime:** the historical runtime is unsupported and should
  not be used for new production deployments.

This initial list is intentionally bounded. Any concrete migration must be
checked against Microsoft's version-specific release notes and migration
guidance; later milestones will add verified breaking changes for each
release.

