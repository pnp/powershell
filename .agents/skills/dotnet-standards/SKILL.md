---
name: dotnet-standards
description: C# 12 / .NET 8 and PowerShell cmdlet design rules for this repository - naming, output and error channels, parameter validation, ShouldProcess, async, culture, cross-platform and ALC constraints. Use when writing or reviewing any C# in this repo, cmdlet or otherwise.
---

# Playbook: dotnet-standards

C# 12 / .NET 8 and PowerShell cmdlet design rules for this repository. Referenced by
[`code-review`](../code-review/SKILL.md), [`new-cmdlet`](../new-cmdlet/SKILL.md) and
[`cmdlet-scaffolder`](../cmdlet-scaffolder/SKILL.md).

Two rule sets apply at once, and where they disagree the **PowerShell** one wins: this is a module
whose users are shell users, not a class library.

---

## PowerShell cmdlet design

These come from Microsoft's cmdlet development guidelines. Violations are user-facing.

**Naming**
- `Verb-PnPNoun`, one approved verb (`Get-Verb`), **singular** noun even when returning many objects.
- `PnP` prefix always. New cmdlet names should not collide with other modules' nouns.
- Parameter names should match the ones used by comparable cmdlets in this module before inventing a
  new one — `-Identity`, `-Connection`, `-Force`, `-Includes`, `-Batch` have established meanings.

**Output**
- Emit **objects**, never formatted text. `WriteObject(x)` — and `WriteObject(collection, true)` to
  enumerate, so the pipeline sees items rather than one array.
- `[OutputType(typeof(T))]` on every cmdlet that returns something.
- Never `Console.WriteLine`. Channels: `WriteObject` (data), `WriteWarning` (recoverable), 
  `WriteVerbose` (diagnostics), `WriteDebug`, `WriteProgress` (long operations).
- Do not return `null` to mean "not found" where the user asked for a specific item — write an
  error. Returning nothing for a filter that matched nothing is correct.

**Input**
- `[Parameter]` per parameter with deliberate `Mandatory`, `Position`, `ValueFromPipeline`.
  Positional only for the one obvious parameter; everything else named.
- Use **PipeBind** types (`ListPipeBind`, `SitePipeBind`, …) so a name, an ID or an object all bind.
- `ParameterSpecified(nameof(X))` to distinguish "not supplied" from "supplied as the default".
- `[ValidateNotNull]` on any reference-typed parameter dereferenced in `ExecuteCmdlet` — otherwise
  `-Param $null` is a `NullReferenceException` rather than a message.
- `[ValidateSet]` / `[ValidateRange]` / `[ValidateCount]` rather than hand-rolled checks in the body.
- Prefer `SwitchParameter` over `bool` for flags. A `bool` parameter forces `-Flag $true` and reads
  wrong in PowerShell.

**Safety**
- Anything destructive or overwriting: `SupportsShouldProcess = true`, then actually call
  `ShouldProcess`. Adding a prompt where none existed breaks unattended scripts — see
  [`api-surface-diff`](../api-surface-diff/SKILL.md).
- **`-Force` may bypass `ShouldContinue`, never `ShouldProcess`.** `ShouldProcess` is what implements
  `-WhatIf` and `-Confirm`, so short-circuiting it breaks simulation:

  ```csharp
  if (Force || ShouldContinue($"Remove {Identity}?", Resources.Confirm))   // correct — the repo's pattern
  if (Force || ShouldProcess($"{target}", "Remove"))                       // WRONG — -Force -WhatIf deletes
  ```

  In the second form `-Force` short-circuits the `||`, `ShouldProcess` is never called, and
  `-Force -WhatIf` performs the operation instead of simulating it. Where both are wanted, gate on
  `ShouldProcess` first and use `Force ||` only on the inner `ShouldContinue`. This is a live defect
  in `src/Commands/Apps/RemoveEntraIDServicePrincipalAppRoleAssignment.cs:52` — flag it if you touch
  that file, and never copy that line as a model.
- Never hardcode credentials, tenant names or endpoints.

**Errors**
- `ThrowTerminatingError(new ErrorRecord(...))` for fatal errors, with a meaningful
  `ErrorCategory` and the object that caused it. Prefer it over a bare `throw` — and this is not
  only style, it changes what the user receives:
  - `WriteError` and `ThrowTerminatingError` surface under `-ErrorAction Stop` as a pipeline stop,
    which `PnPConnectedCmdlet.ProcessRecord` rethrows untouched
    (`src/Commands/Base/PnPConnectedCmdlet.cs:57-60`). Your `ErrorRecord`, its category and its
    target object survive intact.
  - A raw `throw` reaches the generic catch. Under the default error action it becomes
    `PSInvalidOperationException` with the original as inner; under `-ErrorAction Stop`, `Ignore` or
    `SilentlyContinue` it becomes `new ErrorRecord(new Exception(message), source,
    ErrorCategory.NotSpecified, null)` — **type, inner exception, category and target object are all
    lost**, so everything the user needs must be in the message text.
- Error messages belong in `Resources.resx`, referenced as `Resources.MessageName`.

---

## C# 12 / .NET 8

`EnforceCodeStyleInBuild` and `EnableNETAnalyzers` are on. Warnings are findings.

**Style**
- **4 spaces, not tabs.** Braces on their own line (Allman), as the existing files do.
- PascalCase for types/methods/properties; camelCase for locals and parameters; `_camelCase` for
  private fields; `I` prefix on interfaces.
- `var` where the type is evident from the right-hand side.
- One type per file. Enums go in `src/Commands/Enums/`, models in their own files — do not group
  several types in one model file.
- XML doc comments on utility classes, models and enums. Cmdlet classes do not need them; the
  markdown documentation is their reference.
- File-scoped namespaces and collection expressions (`[a, b]`) are fine in new code; do not churn
  existing files to adopt them.

**Correctness**
- `?.` and `??` over nested null checks; do not use `!` to silence a nullable warning you have not
  actually reasoned about.
- `using`/`await using` for everything `IDisposable`. `HttpClient` is not per-call disposable — use
  the connection's existing client rather than newing one up.
- `CultureInfo.InvariantCulture` on every `ToString`/`Parse` of a date or number that crosses a wire
  or a file. A format string like `"yyyy-MM-ddTHH:mm:ssZ"` takes its separators from the current
  culture and yields `13.53.41` under some locales.
- `StringComparison.OrdinalIgnoreCase` for identifiers, URLs and property names. Never
  culture-sensitive comparison for machine-readable strings.
- Prefer LINQ for collection work, but not inside a loop that re-enumerates a remote collection.

**Async**
- The cmdlet pipeline is synchronous. The established pattern here is
  `SomethingAsync(...).GetAwaiter().GetResult()`. Do not use `.Result` or `.Wait()`, and do not
  introduce `async void`.
- Do not add `ConfigureAwait` churn to existing call sites.

**Cross-platform** — .NET 8 on Windows, Linux and macOS
- `Path.Combine`, `Path.DirectorySeparatorChar`. No backslash string surgery, no drive letters.
- Case-sensitive file systems: filename casing must match exactly.
- Do not mix `Environment.NewLine` (what `StringBuilder.AppendLine` writes) with hardcoded `\r\n` in
  generated files — the output then churns purely from changing OS.
- File permissions: anything written containing a private key must be owner-only on Unix.

**Dependencies and the ALC**
- New package references have assembly-load-context consequences. The module assembly and CSOM live
  in `Core`; **every other dependency is private and goes to `Common`**. Adding a reference without
  placing it correctly breaks loading at runtime, not at build.
- Do not add a dependency for something the existing helpers already do.

**Performance**
- Request only the properties needed — `DefaultRetrievalExpressions` / `EnsureProperties`, `$select`
  on Graph. Do not fetch a full field collection to read one field.
- `ExecuteQueryRetry()`, never `ExecuteQuery()`.
- Batch where the API supports it rather than calling per item in a loop.
- Graph collections: `GraphRequestHelper.GetResultCollection` follows `@odata.nextLink`;
  `Get` does not and silently returns the first page only.

---

## Build

```
dotnet build src/PnP.PowerShell.sln
```

Must be warning-clean. `src/Tests` is off limits — do not add or modify files there.

A clean build is where your work stops. **Never commit, push, or open a PR** — see
[Human in the loop](../../../AGENTS.md#human-in-the-loop).
