---
name: code-review
description: Review changes to PnP PowerShell for the failure modes this repository actually ships - silently ignored input, unpaged Graph collections, the wrong base class, permission attributes that do not match the API called, culture and cross-platform bugs, and missing documentation or changelog updates. Use when reviewing a diff, a PR, or uncommitted changes.
---

# Playbook: code-review

Review changes to PnP PowerShell for the failure modes this repository actually ships.

Cmdlets here run unattended, against production tenants, often with tenant-wide permissions. A cmdlet
that quietly does the wrong thing is worse than one that fails, because nobody finds out until the
tenant is already changed. Weight findings accordingly.

Verify before reporting. A claim about behaviour that nobody ran is a guess; say so when it is one.

Language and API rules live in [`dotnet-standards`](../dotnet-standards/SKILL.md). This playbook is about
what goes wrong here specifically.

> Report to the user, in the session. **Never post a review, a comment, or an approval to GitHub**,
> and never open an issue for a finding — see
> [Human in the loop](../../../AGENTS.md#human-in-the-loop).

## Layout

- `src/Commands/` — cmdlet implementations, one folder per feature area
- `src/Commands/Base/` — base classes, `PipeBinds/` for parameter binding types
- `src/Commands/Attributes/` — permission and behaviour attributes
- `src/ALC/` — assembly load context that isolates private dependencies
- `documentation/<Verb-PnPNoun>.md` — the reference page for every cmdlet, one file each
- `pages/articles/` — conceptual articles, listed in `pages/articles/toc.yml`
- `pages/_site/` — generated site output, never edited by hand
- `build/` — build and generator scripts
- `CHANGELOG.md` — release notes

Much of the provisioning behaviour lives in **PnP Framework**, a separate repository. When a root
cause sits there, say so rather than accepting a workaround layered on top here, and do not let a PR
claim to fix an issue whose cause it never touched.

## What to look for first

### Silence

The defect this repository has shipped most often is input accepted and then ignored.

- `catch { }` or `catch { return null; }` — swallowing turns a user's mistake into wrong output
- A parameter parsed into "no value given", after which the cmdlet proceeds with its default
  behaviour. Ignoring a `-Configuration` that could not be read once meant extracting an entire site
  instead of the one list asked for
- `System.Text.Json` ignores unknown members by default, so a misspelled property silently has no
  effect. Custom enum converters here drop values they cannot parse, case sensitively
- An unrecognised resource prefix in a permission attribute is silently classified as **SharePoint**,
  so a typo'd `"garph/…"` declares a bogus SharePoint scope while the real Graph requirement goes
  undeclared — see [`permissions-auditor`](../permissions-auditor/SKILL.md)
- A dropped value that *widens* what the cmdlet does deserves an error, not a warning. An empty
  handler list means "all handlers", so one unrecognised handler name would otherwise turn a scoped
  operation into a full one

### Unpaged collections

`GraphRequestHelper.GetResultCollection` follows `@odata.nextLink`. `GraphRequestHelper.Get` does
not — pointed at a collection endpoint it returns the **first page only, with no error**. The same
applies to `RequestHelper`. This presents to users as "the cmdlet misses items in large tenants",
which is invisible in any tenant small enough to develop against. Check every new collection fetch.

The CSOM equivalent: a query returning more than the list view threshold, or a loop that pages
manually and drops the last page.

### Base class

Check the base class actually matches what the cmdlet does — a tenant-admin operation on
`PnPWebCmdlet`, or a Graph call from a SharePoint cmdlet, gets the wrong context and the wrong
permission flavour. It compiles, and it fails in someone's tenant. The table in
[`new-cmdlet`](../new-cmdlet/SKILL.md) is the reference.

### How errors reach the user

`PnPConnectedCmdlet.ProcessRecord` rethrows `PipelineStoppedException` untouched
(`src/Commands/Base/PnPConnectedCmdlet.cs:57-60`) and catches everything else. Two paths, and the
difference is the finding:

- **`WriteError` / `ThrowTerminatingError`** — under `-ErrorAction Stop` these surface as a pipeline
  stop, rethrown unchanged. The `ErrorRecord`, its `ErrorCategory` and its target object all reach
  the user intact.
- **A raw `throw`** — hits the generic catch. Default error action: rethrown as
  `PSInvalidOperationException` with the original as inner. Under `-ErrorAction Stop` or
  `SilentlyContinue`: `LogError` → `LoggingUtility.Error` → `WriteError(new ErrorRecord(new
  Exception(message), source, ErrorCategory.NotSpecified, null))`. Type, inner exception, category
  and target object are **all discarded**, so everything the user needs must be in the message text.
  Under `-ErrorAction Ignore` the `LogError` call is skipped altogether
  (`PnPConnectedCmdlet.cs:112-119`), so the failure is **swallowed with no record at all** — worth
  remembering when a user reports a cmdlet that "does nothing and says nothing".

So a raw `throw` carrying a custom exception type the caller is meant to inspect is a finding — the
type is not observable on that path. So is a fatal condition signalled with `WriteWarning` and then
continuing, and a `throw` where `ThrowTerminatingError` with a real `ErrorCategory` and target object
would have told the user which object failed.

### Cmdlet conventions

- `Verb-PnPNoun`, approved verbs, correct base class
- `ParameterSpecified(nameof(X))` distinguishes "not supplied" from "supplied as default"
- Reference-typed parameters that are dereferenced in `ExecuteCmdlet` need `[ValidateNotNull]`,
  otherwise `-Param $null` is a `NullReferenceException`
- Permission attributes must match the APIs the cmdlet actually calls, in both directions —
  over-declaring forces users to grant access the cmdlet never uses
- Destructive or overwriting behaviour needs `ShouldProcess`, with `-Force` bypassing only a
  secondary `ShouldContinue`. **`Force || ShouldProcess(...)` is a defect**: `-Force` short-circuits
  the `||`, `ShouldProcess` is never called, and `-Force -WhatIf` performs the operation instead of
  simulating it. `Force || ShouldContinue(...)` is the correct, repo-standard form
- A renamed cmdlet keeps its old name as `[Alias]`

### Cross-platform

.NET 8 and PowerShell 7.4+ on Windows, Linux and macOS.

- No Windows-only path assumptions, no backslash string surgery
- `Environment.NewLine` (what `StringBuilder.AppendLine` writes) mixed with hardcoded `\r\n` makes
  generated files churn purely from changing OS
- Format dates and numbers with `CultureInfo.InvariantCulture`. A custom format string like
  `"yyyy-MM-ddTHH:mm:ssZ"` takes its separators from the current culture and produces
  `13.53.41` under some locales, which is not a valid `xsd:dateTime`
- New package references have ALC consequences: the module assembly and CSOM live in `Core`,
  every other dependency is private and goes to `Common`

## Documentation and changelog

A parameter added, renamed, or changed in behaviour requires its `documentation/<Cmdlet>.md` updated
in the same PR. A new cmdlet requires a new page; a removed cmdlet requires its page deleted.

- `## PARAMETERS` sections carry only the platyPS ` ```yaml ` metadata blocks. Other fenced blocks
  there risk the help build; put examples under `## EXAMPLES` with ` ```powershell ` fences
- Parameter subsections are listed alphabetically
- Conceptual content belongs in `pages/articles/` with front matter, registered in `toc.yml`, not in
  `documentation/`, which is cmdlet reference only

`CHANGELOG.md` entries go under `[Current nightly]` in `Added`, `Changed`, `Fixed` or `Removed`,
each naming the affected cmdlets in backticks and linking its PR. A change in behaviour belongs
under `Changed` even when it fixes a bug, so it appears in the release notes people read before
upgrading. A PR with no changelog entry is an incomplete PR — but the file header says it is owner
maintained and maintainers do add entries, so raise it as a gap, not a blocker.

## Breaking changes

Ask one question: **does any correct usage behave differently?**

If only previously-broken usage changes — a configuration that was never honoured, an invocation that
already threw — it is a fix, and it belongs in a minor release with a `Changed` entry. Reserve a major
release for changes that break usage which was working as documented. Say which of the two a PR is,
and name the invocation that changes, rather than labelling it breaking on the strength of a diff.
[`api-surface-diff`](../api-surface-diff/SKILL.md) has the full classification.

## Reporting

Lead with the finding, not the file tour. For each one give the location, one sentence on the defect,
and a concrete failure scenario: the input, and what the user gets instead of what they expected.
Rank by consequence. If a check could not be run, say what would settle it.
