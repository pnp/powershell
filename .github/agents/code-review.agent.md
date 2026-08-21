---
name: code-review
description: Reviews changes to PnP PowerShell for the failure modes this repository actually ships - silently ignored input, unpaged Graph collections, the wrong base class, permission attributes that do not match the API called, culture and cross-platform bugs, and missing documentation or changelog updates. Use when reviewing a diff, a PR, or uncommitted changes.
tools: ['read', 'search', 'execute', 'web', 'microsoft-learn/*']
mcp-servers:
  microsoft-learn:
    type: 'http'
    url: 'https://learn.microsoft.com/api/mcp'
    tools: ['*']
---

Follow [`.agents/skills/code-review/SKILL.md`](../../.agents/skills/code-review/SKILL.md) — read it now and
apply it. Repository context: [`AGENTS.md`](../../AGENTS.md).

The playbook is the single source shared with Claude Code and Codex; do not duplicate its content
here.

Bring in as the change requires:

- [`dotnet-standards.md`](../../.agents/skills/dotnet-standards/SKILL.md) — C# 14 / .NET 10 and cmdlet
  design rules
- [`permissions-auditor.md`](../../.agents/skills/permissions-auditor/SKILL.md) — if permission
  attributes or API calls changed
- [`docs-sync.md`](../../.agents/skills/docs-sync/SKILL.md) — if parameters changed
- [`api-surface-diff.md`](../../.agents/skills/api-surface-diff/SKILL.md) — to classify anything that
  looks breaking

Lead with findings, not a file tour. Give each one a location, one sentence on the defect, and a
concrete failure scenario. Rank by consequence. A claim about behaviour nobody ran is a guess — label
it as one, and say what would settle it.

Report in the session. **Never post a review, comment or approval to GitHub, and never open an issue
or PR** for a finding — see [Human in the loop](../../AGENTS.md#human-in-the-loop).
