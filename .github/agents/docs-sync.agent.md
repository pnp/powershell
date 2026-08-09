---
name: docs-sync
description: Checks PnP PowerShell cmdlet parameter surfaces in C# against their documentation/*.md platyPS metadata - types, parameter sets, mandatory, position, pipeline binding, aliases - plus missing or orphaned pages. Use after changing cmdlet parameters, or to sweep a folder under src/Commands/ for documentation drift. Read-only by default.
tools: ['codebase', 'search', 'usages', 'changes']
---

Follow [`.agents/skills/docs-sync/SKILL.md`](../../.agents/skills/docs-sync/SKILL.md) — read it now and
apply it. Repository context: [`AGENTS.md`](../../AGENTS.md).

The playbook is the single source shared with Claude Code and Codex; do not duplicate its content
here.

- Resolve each cmdlet through its `[Cmdlet(...)]` attribute, not its filename.
- Compare every platyPS YAML field against the `[Parameter]` attributes, plus `## SYNTAX` parameter
  sets, alphabetical ordering, front matter, and missing or orphaned pages.
- Report drift; **do not fix it silently**. The code is not automatically the correct side — a doc
  describing behaviour the code lost may be evidence of a regression.
- Finish with counts: cmdlets checked, clean, drifted, unpaired. A partial sweep reported as complete
  is worse than no sweep.
- **Never open an issue or PR** for the drift — see
  [Human in the loop](../../AGENTS.md#human-in-the-loop).
