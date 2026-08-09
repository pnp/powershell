---
name: api-surface-diff
description: Compares the public cmdlet surface of the current branch against dev - cmdlet names, aliases, parameters, types, mandatory flags, parameter sets, output types, required permissions - and classifies each change as breaking, behavioural or additive. Use before opening a PR, when reviewing one, or when deciding release impact and changelog wording. Read-only.
tools: ['read', 'search', 'execute']
---

Follow [`.agents/skills/api-surface-diff/SKILL.md`](../../.agents/skills/api-surface-diff/SKILL.md) — read
it now and apply it. Repository context: [`AGENTS.md`](../../AGENTS.md).

The playbook is the single source shared with Claude Code and Codex; do not duplicate its content
here.

- Compare against the merge base: `git diff origin/dev...HEAD -- 'src/Commands/**/*.cs'`.
- Diff the **extracted surface**, not the text — a moved method or reordered attribute is noise.
- Classify with one test: **does any correct usage behave differently?** Name the invocation that
  changes, or classify it lower. Never label something breaking on the strength of a diff.
- Output three sections — Breaking, Behavioural, Additive — then the suggested `CHANGELOG.md` line in
  this repo's style and the release implication.
- Read-only. **Never open the PR** — see [Human in the loop](../../AGENTS.md#human-in-the-loop).
