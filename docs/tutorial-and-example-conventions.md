# Tutorial and example conventions

These conventions keep historical accuracy, executable code, and production
guidance separate.

## Release pages

Each completed release page should state its release date, release type,
support window and current status (with an "as of" date), default C# version,
SDK train, target framework, and distinct ASP.NET Core and EF Core versions.
Cover the platform/runtime, libraries, tooling, web framework, data framework,
compatibility, migration path, runnable examples, and next learning step where
relevant. Cite specific official release notes and lifecycle sources.

Do not guess a relationship from matching version numbers. Say "not verified"
and leave the affected entry incomplete until a suitable official source is
found. Keep SDK version and runtime version distinct.

## Language-feature tutorials

For each significant feature, include:

1. C# version and release date, compiler, target-framework, and runtime/API
   requirements.
2. The problem it solves and the older approach.
3. Correct before-and-after syntax, a minimal runnable example, and a realistic
   example where useful.
4. An explanation of the source and compiler/runtime behavior.
5. Advantages, trade-offs, common mistakes, when to use it, and when not to.
6. Related features, exercises with solutions, and specific Microsoft
   documentation.

Distinguish syntax accepted by the compiler from APIs supplied by a target
framework and behavior supplied by the runtime. Avoid calling new syntax
automatically better.

## Runnable examples

Every sample should include a project file, source, README, required SDK and
target-framework information, build/run commands, deterministic expected
output, explanation, and a tutorial link. Use the simplest stable compatible
target. Never require preview compilers for a stable sample. If a newer
language feature is demonstrated on an older TFM, verify and explain the
compiler and API requirements.

Legacy examples must not force users to install or deploy an unsupported
runtime. Prefer a modern equivalent for execution, explicitly state what it
does and does not reproduce, and record which examples were actually verified.

## Validation records

Record exact SDK/runtime versions and commands used. Distinguish build,
execution, tests, link checks, and static-only checks. A successful build is
not evidence that an example's historical behavior was reproduced. Historical
toolchains that cannot run on current hosted agents should use a documented
isolation or archived-verification strategy rather than silently making CI
fail.

