---
name: whatif-audit
description: Sweeps PnP PowerShell cmdlets for -WhatIf, -Confirm and -Force that do not do what they promise - SupportsShouldProcess declared but never called, hand-declared WhatIf parameters, prompts -Force cannot skip, double prompts, and destructive cmdlets with no gate. Use when reviewing a change to a destructive cmdlet, or to sweep a folder under src/Commands/. Read-only.
tools: ['read', 'search']
---

Follow [`.agents/skills/whatif-audit/SKILL.md`](../../.agents/skills/whatif-audit/SKILL.md) — read it
now and apply it. Repository context: [`AGENTS.md`](../../AGENTS.md).

The playbook is the single source shared with Claude Code and Codex; do not duplicate its content
here.

- Resolve each cmdlet through its `[Cmdlet(...)]` attribute, not its filename.
- Read the helpers a cmdlet delegates to before reporting it. A gate inside a helper is still a gate.
- Name the invocation that misbehaves for every finding.
- Report `no-gate` cmdlets as an inventory. Do not recommend adding a prompt where none exists.
- Read-only. **Never open an issue or PR** for a finding — see
  [Human in the loop](../../AGENTS.md#human-in-the-loop).
- Finish with counts: cmdlets checked, clean, and findings per kind.
