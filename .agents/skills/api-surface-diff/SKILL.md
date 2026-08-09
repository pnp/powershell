---
name: api-surface-diff
description: Compare the public cmdlet surface of the current branch against dev - names, aliases, parameters, types, mandatory flags, parameter sets, output types, permissions - and classify each change as breaking, behavioural or additive. Use before opening a PR, when reviewing one, or when deciding release impact and changelog wording.
---

# Playbook: api-surface-diff

Compare the public cmdlet surface of a branch against `dev` and classify what changed.

## Why

For a PowerShell module the public surface *is* the contract, and it is spread over 800 files, so a
breaking change arrives as an innocuous-looking one-line diff. Renaming a parameter, tightening a
type, or adding `Mandatory = true` breaks every script in the wild that used it. Nothing in CI
catches this; a human diff review reliably misses it.

The output feeds two decisions: whether the change belongs in a major release, and what the
`CHANGELOG.md` entry must say.

> Read-only. Propose the changelog line and the release call; **never open the PR** — see
> [Human in the loop](../../../AGENTS.md#human-in-the-loop).

## The surface

For every cmdlet class, extract:

- **Cmdlet name** — from `[Cmdlet(Verbs*.X, "PnPY")]`
- **Aliases** — `[Alias(...)]` on the class
- **Output type** — `[OutputType(typeof(T))]`
- **Parameter sets** — every distinct `ParameterSetName`, and `DefaultParameterSetName`
- **Per parameter**: name, type, `Mandatory`, `Position`, `ValueFromPipeline`,
  `ValueFromPipelineByPropertyName`, parameter set membership, `[Alias]`, `[ValidateSet]` values
- **Permission attributes** — a narrowed scope is a surface change too: a connection that worked
  before may now be rejected

Build this for `dev` and for the branch, then diff the two structures. Diff the *extracted surface*,
not the text — a moved method or reordered attribute is noise.

```
git fetch origin dev
git diff --name-only origin/dev...HEAD -- 'src/Commands/**/*.cs'
```

Use the three-dot form so you compare against the merge base, not a moving `dev`.

## Classification

Apply one test, from the repository's own review standard:

> **Does any correct usage behave differently?**

Correct usage means an invocation that was working as documented. If only previously-broken usage
changes — a parameter that was never honoured, an invocation that already threw — it is a fix.

**Breaking** (major release)
- Cmdlet or parameter removed or renamed without an `[Alias]` preserving the old name
- Parameter becomes `Mandatory`, or moves out of the default parameter set
- Parameter type narrowed, or `[ValidateSet]` values removed
- Positional parameter renumbered, or positional binding removed
- Parameter sets restructured so a previously valid combination no longer binds
- Output type changed such that a property scripts read is gone
- Required permissions widened — an existing app registration stops being sufficient

**Behavioural** (`Changed` in the changelog, minor release)
- Same signature, different result, warning, or error for the same input
- Default value changed
- A new confirmation prompt (`ShouldProcess`) on a path that used to run unattended — call this out
  explicitly, it breaks automation without changing the signature

**Additive** (`Added`)
- New cmdlet, new optional parameter, new parameter set that does not disturb existing binding
- New alias

A rename **with** an `[Alias]` for the old name is additive, and this repository requires that alias.
A rename without one is breaking; say so and name the alias that would fix it.

## Reporting

Three sections — Breaking, Behavioural, Additive — most consequential first. For each entry:

- `Verb-PnPNoun`, the member, `file.cs:line`
- One sentence on what changed
- **The invocation that changes**, concretely:
  `Get-PnPFoo -Bar "x"` — bound positionally before, now requires `-Bar` by name
- The suggested `CHANGELOG.md` line, in this repo's style: cmdlet names in backticks, ending with a
  link to the PR or issue

Then state the release implication in one line: additive only, or a `Changed` entry, or a genuine
major-release break.

Do not label something breaking on the strength of a diff. Name the usage that breaks, or classify
it lower.
