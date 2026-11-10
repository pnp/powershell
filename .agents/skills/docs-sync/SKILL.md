---
name: docs-sync
description: Check PnP PowerShell cmdlet parameter surfaces in C# against their documentation/*.md platyPS metadata - types, parameter sets, mandatory, position, pipeline binding, aliases - plus missing or orphaned pages. Use after changing cmdlet parameters, or to sweep src/Commands/ for documentation drift.
---

# Playbook: docs-sync

Verify that every cmdlet's parameter surface in C# matches its `documentation/<Verb-PnPNoun>.md`.

## Why this is a defect, not hygiene

`documentation/*.md` is **the shipped product**. It builds the published site at
<https://pnp.github.io/powershell/> and the external help file (`PnP.PowerShell.dll-Help.xml`) that
backs `Get-Help` in the user's console. A parameter documented as optional but declared mandatory
sends the user into an error they cannot explain from the docs. Drift here is user-facing.

There are ~849 documentation files and ~850 cmdlets. This is a fan-out job: mechanical, exact, and
far too large to do by hand.

> Report drift in the session. **Never open an issue or PR for it** — see
> [Human in the loop](../../../AGENTS.md#human-in-the-loop). A sweep across 850 cmdlets could otherwise
> produce a great many of them.

## The two surfaces being compared

**C# side** — attributes on each public property/field of the cmdlet class:

```csharp
[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "ByName")]
[Alias("Name")]
[ValidateNotNull]
public ListPipeBind Identity { get; set; }
```

**Markdown side** — a platyPS YAML block per parameter under `## PARAMETERS`:

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:
Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### Field mapping

| YAML key | C# source | Notes |
|---|---|---|
| `Type` | Property/field type | Use the PowerShell-facing name: `String[]`, `SwitchParameter`, `PnPConnection`, the PipeBind type. |
| `Parameter Sets` | `ParameterSetName` | `(All)` when the parameter appears in every set or none is named. Otherwise the exact set names, comma separated. |
| `Aliases` | `[Alias]` | Empty when absent. |
| `Required` | `Mandatory` | Per parameter set — a parameter mandatory in one set only must not read `True` for `(All)`. |
| `Position` | `Position` | `Named` when unset; otherwise the integer. |
| `Accept pipeline input` | `ValueFromPipeline` / `ValueFromPipelineByPropertyName` | `True (ByValue)`, `True (ByPropertyName)`, `True (ByValue, ByPropertyName)`, or `False`. |
| `Accept wildcard characters` | `[SupportsWildcards]` | `False` unless declared. |
| `Default value` | Initialiser | `None` when there is no meaningful default. |

## What to check

**Per parameter**

- Present in both. A parameter in code but not in the doc is invisible to users; a parameter in the
  doc but not in code is a promise the module does not keep.
- Every YAML field agrees with the attributes, per the mapping above.
- Multiple parameter sets: `## SYNTAX` has one fenced `powershell` block per set, and the sets named
  there match `ParameterSetName` values in the class. A restructured parameter set that `## SYNTAX`
  still describes the old way is a common and confusing drift.

**Per file**

- Parameter subsections under `## PARAMETERS` are **listed alphabetically**.
- The boilerplate `-Connection` and `-Verbose` sections are present for connected cmdlets, with the
  standard wording used by sibling files.
- Front matter is intact and consistent:
  ```
  Module Name: PnP.PowerShell
  title: <Verb-PnPNoun>
  schema: 2.0.0
  applicable: <workloads>
  external help file: PnP.PowerShell.dll-Help.xml
  online version: https://pnp.github.io/powershell/cmdlets/<Verb-PnPNoun>.html
  ```
  `title` and the `online version` slug must both equal the real cmdlet name.
- **`## PARAMETERS` contains only ` ```yaml ` fenced blocks.** Any other fenced block there risks the
  external help build. Examples belong under `## EXAMPLES` with ` ```powershell ` fences. This is a
  build-breaking rule, so report it as high severity.
- `## RELATED LINKS` present.

**Per repository**

- A documentation file for every cmdlet, and a cmdlet for every documentation file. Removed cmdlets
  leaving orphan pages is a recurring drift.
- Filename equals the cmdlet name exactly, including case.
- Cmdlets carrying `[Alias("Old-PnPName")]` — the alias should be discoverable from the page, and no
  separate stale page should exist for the old name.

## Finding the pair

The class filename does not reliably match the cmdlet name. Resolve through the attribute — search
for the pattern `\[Cmdlet\(` across `src/Commands/**/*.cs` with your search tool. This sweep needs no
shell; the agent profiles for it deliberately grant only read and search tools.

`[Cmdlet(VerbsCommon.Get, "PnPList")]` → `Get-PnPList` → `documentation/Get-PnPList.md`. Map the
`Verbs*` constant to its verb (`VerbsCommon.Get` → `Get`, `VerbsLifecycle.Request` → `Request`, and
so on) rather than assuming the class name carries it.

## Procedure

1. Take a scope: changed files, one folder under `src/Commands/`, or the whole repository.
2. Build the cmdlet → doc pairing. Report unpaired items on both sides first.
3. For each pair, extract both parameter tables and diff them field by field.
4. Report drift. **Do not fix silently** unless asked — the correct side is not always the code. A
   doc describing behaviour the code lost may be evidence of a regression.

## Reporting

Group by cmdlet, then by parameter. For each: the field, the C# value, the markdown value, and which
side you believe is wrong with a reason. Finish with counts — cmdlets checked, clean, drifted,
unpaired — so a partial run cannot be mistaken for a full one.

When asked to fix, change only what you can justify from the code, keep the surrounding prose and
wording style of the file, and never reformat an untouched YAML block.
