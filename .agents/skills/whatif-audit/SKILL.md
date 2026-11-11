---
name: whatif-audit
description: Sweep PnP PowerShell cmdlets for -WhatIf, -Confirm and -Force that do not do what they promise - SupportsShouldProcess declared but never called, hand-declared WhatIf parameters, prompts -Force cannot skip, double prompts, and destructive cmdlets with no gate. Use when reviewing a change to a destructive cmdlet, or to sweep a folder under src/Commands/.
---

# Playbook: whatif-audit

Find cmdlets whose `-WhatIf`, `-Confirm` or `-Force` does not do what it promises.

## Why

A user runs `Remove-PnPSomething -WhatIf` to see what would happen. If the cmdlet declares
`SupportsShouldProcess` and never calls `ShouldProcess`, PowerShell accepts the switch and the cmdlet
deletes the object anyway. Nothing in the build catches this, and the user finds out in their tenant.
A prompt that `-Force` cannot skip is the same kind of defect for automation: the script hangs, or
fails, in a pipeline with nobody there to answer.

All of this is decidable from the source, across every cmdlet.

> Read-only. Report findings in the session. **Never open an issue or PR for one** — see
> [Human in the loop](../../../AGENTS.md#human-in-the-loop).

The rules themselves are in [`dotnet-standards`](../dotnet-standards/SKILL.md) under **Safety**. This
playbook is the sweep.

## What to collect per cmdlet

Resolve each cmdlet through its `[Cmdlet(...)]` attribute, not its filename. For each one, record:

- `SupportsShouldProcess` and `ConfirmImpact` on the attribute
- every `ShouldProcess(` and `ShouldContinue(` call the cmdlet can reach
- whether it has a `Force` parameter, and where that parameter is read
- any parameter the class itself declares named `WhatIf` or `Confirm`

**Follow the delegation before reporting.** Some cmdlets gate inside a helper they call rather than
in their own class: recycle bin utilities, the copy and move proxy cmdlets, and callbacks passed to
utility classes. A cmdlet that hands `this` or a callback to a helper is not a finding until you have
read the helper. Check whether `src/Commands/Base/BasePSCmdlet.cs` defines gate helpers of its own,
and count a call to one of them as a call to `ShouldProcess`.

## Findings, most serious first

| Kind | What it is | What the user gets |
|---|---|---|
| `whatif-ignored` | `SupportsShouldProcess = true`, and no reachable `ShouldProcess` call | `-WhatIf` performs the operation |
| `force-bypasses-whatif` | `Force \|\| ShouldProcess(...)` | `-Force -WhatIf` performs the operation |
| `hand-rolled` | The class declares its own `WhatIf` or `Confirm` parameter | `-WhatIf:$false` and `$WhatIfPreference` do not behave as PowerShell documents |
| `change-before-gate` | A write, or a CSOM call queued for `ExecuteQueryRetry`, happens before the gate | Part of the change is made under `-WhatIf`, or after the user answers No |
| `unskippable-prompt` | `ShouldContinue` on a cmdlet with no `Force` parameter, or one that does not read it | An unattended script waits at a prompt |
| `double-prompt` | `ShouldProcess` and then `ShouldContinue` on the same path | `-Confirm` asks the same question twice |
| `confirm-unreachable` | `ConfirmImpact.None` with `SupportsShouldProcess` | `-Confirm` never prompts |
| `no-gate` | Destructive verb (Remove, Clear, Reset, Uninstall, Unregister, Disable, Stop) with neither `ShouldProcess` nor `ShouldContinue` | No way to preview or confirm |
| `doc-gap` | `SupportsShouldProcess` cmdlet whose page has no `-WhatIf` or `-Confirm` section | The switch is undiscoverable |

`Force || ShouldContinue(...)` is the correct, repository-standard form and is not a finding.

`no-gate` is an inventory, not a defect list. Many cmdlets in this module have always run without a
prompt, and adding one breaks unattended scripts — see
[`api-surface-diff`](../api-surface-diff/SKILL.md). Report it separately and do not recommend adding
a prompt.

## Procedure

1. Scope the run: one cmdlet, a changed file set, a folder under `src/Commands/`, or the whole
   repository. State the scope in the report.
2. Search for `SupportsShouldProcess`, `ShouldProcess(`, `ShouldContinue(` and `SwitchParameter WhatIf`
   across the scope to build the candidate list. This sweep needs no shell.
3. Read each candidate class, and the helpers it delegates to, before classifying it.
4. For each finding, name the invocation that misbehaves.

## Reporting

One row per finding, ordered by the table above:

| Field | Content |
|---|---|
| Cmdlet | `Verb-PnPNoun` and `file.cs:line` |
| Kind | From the table |
| Evidence | The attribute and the call, or the missing call |
| Invocation | The command that misbehaves, and what it does — "`Remove-PnPPlannerRoster -WhatIf` deletes the roster" |
| Fix | The smallest change, and whether it alters behaviour for a script that works today |

You cannot run the cmdlets, so every row is a reading of the code. Say which delegated helpers you
read and which you could not resolve.

Finish with counts — cmdlets checked, clean, and findings per kind — so a partial sweep cannot be
mistaken for a full one.
