---
name: cmdlet-scaffolder
description: Generate a new PnP PowerShell cmdlet modelled on an existing sibling - class with the right base class and permission attributes, the documentation page, and the changelog entry. Use when adding a cmdlet for a Graph or CSOM API. Output is a draft needing verification against a real tenant.
---

# Playbook: cmdlet-scaffolder

Generate a new cmdlet — class, attributes, documentation page and changelog entry — modelled on an
existing sibling.

> **The output is a draft.** You cannot call the API or run the cmdlet. Everything about the request
> shape, the response fields and the permission scopes is inferred. Hand it over labelled as such,
> with the inferences listed. A plausible, well-formed, subtly wrong cmdlet is the specific failure
> mode this playbook exists to avoid.

Conventions live in [`new-cmdlet`](../new-cmdlet/SKILL.md); language rules in
[`dotnet-standards`](../dotnet-standards/SKILL.md). This playbook is the procedure.

> **Never commit, push, or open a PR.** Leave the work in the tree and hand it over — see
> [Human in the loop](../../../AGENTS.md#human-in-the-loop).

## 1. Establish the request

Before writing anything, pin down: cmdlet name (`Verb-PnPNoun`, approved verb, singular noun), the
API being wrapped, the parameters, and the output shape. If the verb or noun is unsettled, ask —
renaming later requires an `[Alias]` and a changelog entry.

Check the name is not already taken, including as an alias:

```
grep -rn 'Cmdlet(Verbs[A-Za-z]*\.[A-Za-z]*, "PnPTheNoun")' src/Commands --include=*.cs
grep -rn '\[Alias(' src/Commands --include=*.cs
```

## 2. Choose the sibling

**This is the most important step.** Find the closest existing cmdlet: same folder, same base class,
same API, same verb. Prefer a recently modified one — helper signatures have evolved and old call
shapes survive in the tree.

```
git log --diff-filter=M --name-only -20 -- src/Commands/<Area>
```

Read the sibling's class *and* its `documentation/*.md` page in full. You are matching a house style,
not producing generic C#. Say which sibling you used.

## 3. Resolve the API

Use the **Microsoft Learn MCP server** for the endpoint, its request/response shape and its
least-privilege permissions, delegated and application. Do not answer from memory — this is where
generated cmdlets go wrong, and it is exactly what the permission attributes encode.

Record the Learn URL for each claim; it goes in the handover.

## 4. Write the class

Per [`new-cmdlet`](../new-cmdlet/SKILL.md): correct base class, `[Cmdlet]`, `[OutputType]`, permission
attributes, PipeBind parameters with validation attributes, `ExecuteCmdlet()` override.

- Reuse existing PipeBinds and models. Only add a new model if nothing fits, one type per file,
  enums under `src/Commands/Enums/`.
- Graph collections: `GetResultCollection`, not `Get`.
- CSOM: `ExecuteQueryRetry()`.
- Destructive or overwriting: `SupportsShouldProcess` plus an actual `ShouldProcess` call and
  `-Force`.
- Error messages into `Resources.resx`, not string literals.

## 5. Write the documentation page

`documentation/<Verb-PnPNoun>.md`, structure copied from the sibling page:

- Front matter — `Module Name`, `title`, `schema: 2.0.0`, `applicable`, `external help file`,
  `online version` slug matching the cmdlet name exactly
- `## SYNOPSIS` — a **Required Permissions** block stating the same scopes as the attributes with the
  same delegated/application split, then a one-line summary. **Omit the block entirely** if the
  cmdlet carries `ApiPermissionsNotRequired`; adding one there contradicts the attribute and the
  existing pages
- `## SYNTAX` — one ` ```powershell ` block per parameter set
- `## DESCRIPTION`
- `## EXAMPLES` — at least one, realistic, each followed by a sentence explaining it
- `## PARAMETERS` — **alphabetical**, one ` ```yaml ` block per parameter and **nothing else fenced
  in this section**, including the standard `-Connection` and `-Verbose` blocks copied verbatim from
  the sibling
- `## RELATED LINKS`

The YAML fields must agree with the attributes exactly — see [`docs-sync`](../docs-sync/SKILL.md) for the
field mapping.

## 6. Changelog

One line under `[Current nightly]` → `Added`, cmdlet name in backticks, ending with a link to the PR
or issue. Match the surrounding entries' tone: what it does and why someone would use it, not "added
new cmdlet".

## 7. Build

```
dotnet build src/PnP.PowerShell.sln
```

Warning-clean. Do not touch `src/Tests`.

## 8. Hand over

Report:

- Files created or changed
- **The sibling you modelled on**
- **Inferred, not verified** — every API shape, response field and permission scope, each with the
  Learn URL it came from
- **The invocation a maintainer should run against a tenant to verify**, including which connection
  type (delegated and app-only both, where the cmdlet supports both)
- Anything you could not resolve and what would settle it

Do not describe the result as tested, working, or verified. It has been compiled, and that is all.
