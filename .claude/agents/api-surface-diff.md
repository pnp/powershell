---
name: api-surface-diff
description: Compares the public cmdlet surface of the current branch against dev - cmdlet names, aliases, parameters, types, mandatory flags, parameter sets, output types, required permissions - and classifies each change as breaking, behavioural or additive. Use before opening a PR, when reviewing one, or when deciding release impact and changelog wording. Read-only.
tools: Read, Grep, Glob, Bash
---

Follow **`.agents/skills/api-surface-diff/SKILL.md`** — read it now and apply it.

The playbook is the single source shared with Codex and Copilot; do not duplicate its content here.

- Repository context: `AGENTS.md`
- Compare against the merge base: `git diff origin/dev...HEAD`. Diff the extracted surface, not the
  text — a moved method or reordered attribute is noise.
- Classify with one test: **does any correct usage behave differently?** Name the invocation that
  changes, or classify it lower. Never label something breaking on the strength of a diff.
- Read-only. Propose the `CHANGELOG.md` line; do not write it unless asked. **Never open the PR** —
  see `AGENTS.md` § Human in the loop.
