---
name: code-review
description: Reviews changes to PnP PowerShell, a .NET 8 module of 750+ cmdlets for Microsoft 365. Knows the cmdlet base classes, how errors reach the user, the documentation and changelog conventions, and the failure modes this repository actually ships.
---

# Reviewing PnP PowerShell

Cmdlets here run unattended, against production tenants, often with tenant-wide permissions. A cmdlet
that quietly does the wrong thing is worse than one that fails, because nobody finds out until the
tenant is already changed. Weight findings accordingly.

Verify before reporting. A claim about behaviour that nobody ran is a guess; say so when it is one.

## Layout

- `src/Commands/` — cmdlet implementations, one folder per feature area
- `src/Commands/Base/` — base classes, `PipeBinds/` for parameter binding types
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
- A dropped value that *widens* what the cmdlet does deserves an error, not a warning. An empty
  handler list means "all handlers", so one unrecognised handler name would otherwise turn a scoped
  operation into a full one

### How errors reach the user

`PnPConnectedCmdlet.ProcessRecord` catches everything. Under the default error action it rethrows as
`PSInvalidOperationException` with the original as inner; under `-ErrorAction Stop` it goes through
`LoggingUtility.Error`, which builds an `ErrorRecord` around a **new bare `Exception` carrying only
the message**. Exception types and inner exceptions do not survive that path, so anything the user
needs must be in the message text. Do not rely on a custom exception type being observable.

### Cmdlet conventions

- `Verb-PnPNoun`, approved verbs, correct base class (`PnPWebCmdlet`,
  `PnPSharePointOnlineAdminCmdlet`, `PnPGraphCmdlet`, …)
- `ParameterSpecified(nameof(X))` distinguishes "not supplied" from "supplied as default"
- Reference-typed parameters that are dereferenced in `ExecuteCmdlet` need `[ValidateNotNull]`,
  otherwise `-Param $null` is a `NullReferenceException`
- Permission attributes (`RequiredApiDelegatedOrApplicationPermissions` and siblings) must match the
  APIs the cmdlet actually calls
- Destructive or overwriting behaviour needs `ShouldProcess`/`ShouldContinue` and `-Force`

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
in the same PR.

- `## PARAMETERS` sections carry only the platyPS ` ```yaml ` metadata blocks. Other fenced blocks
  there risk the help build; put examples under `## EXAMPLES` with ` ```powershell ` fences
- Conceptual content belongs in `pages/articles/` with front matter, registered in `toc.yml`, not in
  `documentation/`, which is cmdlet reference only

`CHANGELOG.md` entries go under `[Current nightly]` in `Added`, `Changed` or `Fixed`, each linking
its PR. A change in behaviour belongs under `Changed` even when it fixes a bug, so it appears in the
release notes people read before upgrading. The file header says it is owner maintained; maintainers
do add entries, so do not raise that as a blocker.

## Breaking changes

Ask one question: **does any correct usage behave differently?**

If only previously-broken usage changes — a configuration that was never honoured, an invocation that
already threw — it is a fix, and it belongs in a minor release with a `Changed` entry. Reserve a major
release for changes that break usage which was working as documented. Say which of the two a PR is,
and name the invocation that changes, rather than labelling it breaking on the strength of a diff.

## Reporting

Lead with the finding, not the file tour. For each one give the location, one sentence on the defect,
and a concrete failure scenario: the input, and what the user gets instead of what they expected.
Rank by consequence. If a check could not be run, say what would settle it.
