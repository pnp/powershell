---
name: issue-triage
description: Take a PnP PowerShell GitHub issue, find the cmdlet that owns it, trace the code path, and decide whether the cause is in this repo or in PnP Framework, PnP Core SDK or the service. Use when starting work from an issue number or a bug report. Produces a diagnosis, not a fix.
---

# Playbook: issue-triage

Take a GitHub issue, find the cmdlet that owns it, trace the code path, and produce a hypothesis.

## What this produces

**A hypothesis and a reading of the code — not a fix.** The bug reproduces against a live Microsoft
365 tenant that you do not have. You can establish what the code does; you cannot establish that it
is what the user hit. Say which is which, every time.

The work this saves is the search: mapping an issue to the ~15 lines that matter across 800 cmdlets.
That is the genuine time sink, and it is fully doable statically.

> **Never create an issue, a PR, or a comment** — see
> [Human in the loop](../../../AGENTS.md#human-in-the-loop). This playbook reads GitHub and writes a
> diagnosis. Everything that reaches the tracker is the maintainer's to post.

## Step 1 — read the issue properly

Fetch the issue **and its comments**. Maintainer replies frequently contain the actual diagnosis, and
the reporter often corrects the title further down.

Use the GitHub MCP server if it is connected; otherwise the `gh` CLI, which is usually installed and
authenticated:

```bash
gh issue view <number> --comments
gh issue list --search "<cmdlet name> in:title" --state all --limit 20   # prior reports and fixes
gh api repos/pnp/powershell/commits?path=<the cmdlet file>               # when it regressed
```

Read-only subcommands only. If neither is available, fall back to web search and say so in the
report.

Extract: cmdlet name and exact invocation, PnP PowerShell version, PowerShell version, OS,
authentication mode (**delegated vs app-only matters constantly**), the verbatim error, and whether
it is a regression — if so, the last working version.

## Step 2 — find the owning cmdlet

The class filename does not reliably match the cmdlet name. Resolve through the attribute:

```
grep -rn 'Cmdlet(Verbs[A-Za-z]*\.[A-Za-z]*, "PnPTheNoun")' src/Commands --include=*.cs
```

If nothing matches, the user may be naming an **alias** — check `[Alias(...)]` on classes:

```
grep -rn '\[Alias(' src/Commands --include=*.cs
```

Then read `documentation/<Verb-PnPNoun>.md` for what the cmdlet is *supposed* to do. Half of the
issues in this repository are behaviour disagreeing with its own documented description, and the doc
is the specification.

## Step 3 — decide which layer owns the bug

**Do this before reading code in depth.** A large share of issues filed here are not fixable here.

| Symptom | Likely owner |
|---|---|
| Parameter parsing, binding, cmdlet-local logic, output shaping | **This repo** |
| Provisioning, templates, `Get-/Invoke-PnPSiteTemplate`, `Apply-`/handler behaviour | **PnP Framework** |
| PnP Core SDK model objects, modern page internals | **PnP Core SDK** |
| CSOM throwing from `ExecuteQueryRetry` with a server-side message | **SharePoint service or CSOM** |
| 401/403, consent, missing scope | Permissions metadata — see [`permissions-auditor`](../permissions-auditor/SKILL.md) |
| Throttling, transient 429/503 | Service behaviour; check retry handling is actually used |

Say which layer you landed on and why. A PR that layers a workaround here over a root cause in PnP
Framework should be named as such rather than accepted.

## Step 4 — trace the path

From `ExecuteCmdlet()` forward. Check, in this order — these are the causes that recur:

1. **Is the input reaching the API at all?** The defect this repo ships most often is a parameter
   accepted and then ignored. `ParameterSpecified(nameof(X))` distinguishes "not supplied" from
   "supplied as the default value"; its absence is a frequent root cause.
2. **Is something swallowed?** `catch { }`, `catch { return null; }`, a parse that yields "no value"
   and proceeds with default behaviour.
3. **Delegated vs app-only.** Different code path, different scopes, different tenant behaviour.
4. **Paging.** A Graph collection fetched with `GraphRequestHelper.Get` instead of
   `GetResultCollection` returns only the first page. Presents as "missing items" and is almost
   always this.
5. **Retry.** `ExecuteQuery()` instead of `ExecuteQueryRetry()` presents as intermittent failure.
6. **Culture and platform.** A custom date format string takes separators from the current culture;
   path handling that assumes Windows. Presents as "only fails on my machine / on Linux".
7. **Null dereference.** A reference-typed parameter without `[ValidateNotNull]`, passed `$null`.

Note the error path too: `PnPConnectedCmdlet.ProcessRecord` catches everything, and under
`-ErrorAction Stop` the exception is rebuilt as a bare `Exception` carrying only the message. Types
and inner exceptions do not survive. If the user needs to know something, it must be in the message
text — and a report of "unhelpful error" is often exactly this.

## Step 5 — report

```
Issue:      #NNNN — one-line restatement
Cmdlet:     Verb-PnPNoun  (src/Commands/Area/File.cs:LINE)
Layer:      this repo | PnP Framework | PnP Core SDK | service | metadata
Confidence: high | medium | low

Root cause
  What the code does, with line references, and why that produces the reported symptom.

Established vs assumed
  Established: read directly from the code.
  Assumed:     inferred, needs a tenant to confirm.

Repro to confirm
  The exact invocation and connection type a maintainer should run.

Proposed fix
  The smallest change that addresses the cause, and what it would break.

Release classification
  Fixed / Changed — see api-surface-diff.md. A behaviour change belongs under Changed
  even when it fixes a bug.

Changelog entry
  Draft line in this repo's style, linking the issue.
```

Where the evidence does not support a single cause, give the two candidates and the one observation
that would separate them. A confident wrong diagnosis costs a maintainer more than an honest gap.
