---
name: docs-sync
description: Checks PnP PowerShell cmdlet parameter surfaces in C# against their documentation/*.md platyPS metadata - types, parameter sets, mandatory, position, pipeline binding, aliases - plus missing or orphaned pages. Use after changing cmdlet parameters, or to sweep a folder under src/Commands/ for documentation drift. Read-only by default.
tools: Read, Grep, Glob
---

Follow **`.agents/skills/docs-sync/SKILL.md`** — read it now and apply it.

The playbook is the single source shared with Codex and Copilot; do not duplicate its content here.

- Repository context: `AGENTS.md`
- Resolve each cmdlet through its `[Cmdlet(...)]` attribute, not its filename.
- Report drift; **do not fix it silently**. The code is not automatically the correct side — a doc
  describing behaviour the code lost may be evidence of a regression.
- Always finish with counts: cmdlets checked, clean, drifted, unpaired. A partial sweep reported as
  complete is worse than no sweep.
- **Never open an issue or PR** for the drift you find — report it in the session. See `AGENTS.md`
  § Human in the loop.
