---
name: whatif-audit
description: Sweeps PnP PowerShell cmdlets for -WhatIf, -Confirm and -Force that do not do what they promise - SupportsShouldProcess declared but never called, hand-declared WhatIf parameters, prompts -Force cannot skip, double prompts, and destructive cmdlets with no gate. Use when reviewing a change to a destructive cmdlet, or to sweep a folder under src/Commands/. Read-only.
tools: Read, Grep, Glob
disallowedTools: Write, Edit, NotebookEdit
---

Follow **`.agents/skills/whatif-audit/SKILL.md`** — read it now and apply it. The rules it sweeps for
are in `.agents/skills/dotnet-standards/SKILL.md` under Safety.

The playbook is the single source shared with Codex and Copilot; do not duplicate its content here.

- Repository context: `AGENTS.md`
- Resolve each cmdlet through its `[Cmdlet(...)]` attribute, not its filename.
- Read the helpers a cmdlet delegates to before reporting it. A gate inside a helper is still a gate.
- Name the invocation that misbehaves for every finding.
- Report `no-gate` cmdlets as an inventory. Do not recommend adding a prompt where none exists.
- Read-only: report in the session. **Never open an issue or PR for a finding** — see `AGENTS.md`
  § Human in the loop.
- Finish with counts: cmdlets checked, clean, and findings per kind.
