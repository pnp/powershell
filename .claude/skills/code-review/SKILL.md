---
name: code-review
description: Reviews changes to PnP PowerShell, a .NET 8 module of 800+ cmdlets for Microsoft 365. Knows the cmdlet base classes, how errors reach the user, unpaged Graph collections, the documentation and changelog conventions, and the failure modes this repository actually ships. Use when reviewing a diff, a PR, or uncommitted changes.
---

# Reviewing PnP PowerShell

Follow **[`.agents/skills/code-review/SKILL.md`](../../../.agents/skills/code-review/SKILL.md)** — read it
now and apply it.

That playbook is the single source shared with Codex and Copilot. Do not duplicate its content here;
changes belong in the playbook.

Report in the session. The review does not authorize publishing an issue, PR, review, comment, or
approval. See [Human in the loop](../../../AGENTS.md#human-in-the-loop).

Supporting references, as the review needs them:

- [`.agents/skills/dotnet-standards/SKILL.md`](../../../.agents/skills/dotnet-standards/SKILL.md) — C# 12 /
  .NET 8 and PowerShell cmdlet design rules
- [`.agents/skills/permissions-auditor/SKILL.md`](../../../.agents/skills/permissions-auditor/SKILL.md) —
  if the change touches permission attributes or adds an API call
- [`.agents/skills/docs-sync/SKILL.md`](../../../.agents/skills/docs-sync/SKILL.md) — if the change touches
  parameters
- [`.agents/skills/api-surface-diff/SKILL.md`](../../../.agents/skills/api-surface-diff/SKILL.md) — to
  classify anything that looks breaking
- [`AGENTS.md`](../../../AGENTS.md) — repository context
