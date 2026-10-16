# Learning roadmap

Learn the platform in two dimensions: proficiency and release history. Reading
release notes is useful context, but it does not replace building software,
testing assumptions, or exercising engineering judgment.

## Proficiency levels

| Level | Focus | Evidence of progress |
| --- | --- | --- |
| 1. Beginner | Basic syntax, types, control flow, methods, simple programs, and running examples. | Complete small exercises and explain the code in your own words. |
| 2. Intermediate | OOP, collections, LINQ, generics, exceptions, and asynchronous programming. | Build a small tested application and compare a legacy and modern idiom. |
| 3. Advanced | Modern C#, runtime behavior, performance, libraries, testing, and diagnostics. | Measure behavior and justify API and language choices. |
| 4. Senior Engineer | Maintainability, compatibility, migration risk, production readiness, and regression testing. | Produce a migration plan with tests, rollback considerations, and operational checks. |
| 5. Staff Engineer | Cross-release architecture, reusable patterns, platform strategy, and cross-team standards. | Lead a design review and communicate trade-offs across teams. |
| 6. Principal Engineer | Evidence-based decisions about support lifecycle, architecture, compatibility, and long-term technical debt. | Demonstrate judgment through real projects, assessments, design reviews, and outcomes—not reading alone. |

## Release-history path

1. Establish the historical foundations: .NET Core 1.x and 2.x, then the first
   LTS milestones.
2. Follow each stable platform release in chronological order, including STS
   releases between LTS releases.
3. Track the C# language separately, including minor language versions and
   features that do not require a new runtime.
4. Compare the technologies in runnable before-and-after examples.
5. For each LTS transition, evaluate the full intervening path and make a
   testable migration plan.
6. Use the [support lifecycle](versions/support-lifecycle.md) when discussing
   production support; use historical releases only as learning material.

## Milestones

| Milestone | Scope | Status |
| --- | --- | --- |
| Foundation | Repository conventions, official-source policy, current lifecycle snapshot, version index, language-history index, roadmap, and introductory milestones. | Initial roadmap and indexes in place; detailed C# feature tutorials and automated validation remain. |
| Historical platform | .NET Core 1.1 through .NET Core 3.1, with ASP.NET Core and EF Core tracked independently. | Milestone docs exist for .NET Core 1.0, 1.1, 2.x, 3.0, and 3.1; richer feature tutorials and runnable examples remain. |
| Unified .NET | .NET 5 through .NET 10, including every intervening STS and the LTS transitions. | Milestone docs exist for .NET 5 through .NET 10; deeper feature examples and cross-LTS assessment remain. |
| C# feature curriculum | Dedicated tutorials for significant stable language features, with compatibility and runnable examples. | Planned; linked to the historical index. |
| Verification | Build/test automation, Markdown link checks, metadata checks, and an explicit historical-example strategy. | Planned; the first sample is verified manually and its status is recorded. |

## Practical assessments

At each level, complete a small project, write tests, and explain a trade-off.
Senior-and-above assessments should additionally include compatibility review,
support-policy evidence, migration strategy, and a peer/design review. Do not
equate familiarity with syntax or release history with seniority.
